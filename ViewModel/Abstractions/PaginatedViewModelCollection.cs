using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using Model;
using Model.Access;
using System.Collections.ObjectModel;

namespace ViewModel.Abstractions;

public abstract partial class PaginatedViewModelCollection: ObservableObject {
    private static int version = 0;
    protected static int Version => ++version;

    [ObservableProperty]
    public partial bool IsLoading { get; protected set; } = false;
    [ObservableProperty]
    public partial bool HasMoreEntitiesToLoad { get; protected set; } = false;
}


public abstract class PaginatedViewModelCollection<ViewModel, Entity>: PaginatedViewModelCollection, IRecipient<Messages.SignInCompleted>
    where ViewModel : PaginatedViewModel
    where Entity : Model.Interfaces.IDatabaseEntity {
    public ObservableCollection<ViewModel> ViewModels { get; } = [];

    protected abstract Entity[] GetFromDatabase(uint count = itemsCountPerLoad, string? afterId = null);
    protected abstract Task<Entity[]> GetFromServer(uint count = itemsCountPerLoad, string? afterId = null);
    protected abstract ViewModel CreateViewModel(Entity entity);
    protected readonly Client client = Model.Services.Get<Client>();

    private bool CanLoad => client.SignedIn && !IsLoading;
    private const uint itemsCountPerLoad = Constants.ItemsCountPerLoad;

    public PaginatedViewModelCollection() {
        StrongReferenceMessenger.Default.Register<Messages.SignInCompleted>(this);
        _ = LoadLatestItems();
    }

    public void Receive(Messages.SignInCompleted message) {
        ViewModels.Clear();
        _ = LoadLatestItems();
    }

    public Func<Task> LoadLatestItems => LoadLatestItemsInternal;
    private async Task LoadLatestItemsInternal() {
        await LoadMoreItems(0);
    }

    public async Task OnItemRealized(int index, bool realized) {
        ViewModels[index].Realized = realized;
        if (!realized) {
            return;
        }
        ViewModels[index].DownloadMedia();
        if ((index == ViewModels.Count - 1) && HasMoreEntitiesToLoad) {
            await LoadMoreItems(index + 1, ViewModels[index].PaginateId);
        }
    }

    private async Task LoadMoreItems(int startingIndex, string? afterId = null) {
        if (!CanLoad) {
            return;
        }

        IsLoading = true;

        try {
            var entities = await Task.Run(async () => {
                Entity[] entities;
                if (ViewModels.Count == 0) {
                    entities = GetFromDatabase(itemsCountPerLoad, afterId);
                    if (entities.Length == 0) {
                        entities = await GetFromServer(itemsCountPerLoad, afterId);
                    }
                } else if (startingIndex == 0) {
                    entities = await GetFromServer(itemsCountPerLoad, afterId);
                } else {
                    entities = await GetFromServer(itemsCountPerLoad, afterId);
                    if (entities.Length == 0) {
                        entities = GetFromDatabase(itemsCountPerLoad, afterId);
                    }
                }
                return entities;
            });

            if (entities.Length == 0) {
                return;
            }

            HasMoreEntitiesToLoad = entities.Length >= itemsCountPerLoad;
            int currentVersion = Version;
            var viewModels = await Task.Run(() => {
                var result = new ViewModel[entities.Length];
                for (int i = 0; i < entities.Length; i++) {
                    result[i] = CreateViewModel(entities[i]);
                }
                return result;
            });

            // No overlapping, add all to the beginning
            if (ViewModels.Count == startingIndex) {
                if (startingIndex == 0) {
                    ViewModels.Clear();
                } else {
                    for (int i = ViewModels.Count - 1; i >= startingIndex; i--) {
                        ViewModels.RemoveAt(i);
                    }
                }
                for (int i = 0; i < viewModels.Length; i++) {
                    ViewModels.Insert(i + startingIndex, viewModels[i]);
                    viewModels[i].Update(currentVersion);
                }
                return;
            }

            {
                // Reduce the size of the collection to match the new items
                for (int i = ViewModels.Count - 1; i >= entities.Length + startingIndex; i--) {
                    ViewModels.RemoveAt(i);
                }

                // Add new items to the beginning
                int overlappingStartingIndex = Array.FindIndex(entities, x => x.Id == ViewModels[startingIndex].Id);
                for (int i = 0; i < overlappingStartingIndex; i++) {
                    int targetIndex = i + startingIndex;
                    ViewModels.Insert(targetIndex, viewModels[i]);
                    ViewModels[targetIndex].Update(currentVersion);
                }

                // Update existing items and remove any that is no longer present
                for (int i = overlappingStartingIndex; i < entities.Length;) {
                    int targetIndex = i + startingIndex;
                    // Getting extra item from sever response
                    // Rare case, just remove all of the rest and re-add
                    if (targetIndex >= ViewModels.Count) {
                        for (int j = i; j < entities.Length; j++) {
                            ViewModels.Add(viewModels[j]);
                            viewModels[j].Update(currentVersion);
                        }
                        break;
                    }
                    if (entities[i].Id == ViewModels[targetIndex].Id) {
                        ViewModels[targetIndex].Update(currentVersion);
                        i++;
                    } else {
                        ViewModels.RemoveAt(targetIndex);
                    }
                }

                // Clear the rest of the items
                for (int i = ViewModels.Count - 1; i >= entities.Length + startingIndex; i--) {
                    ViewModels.RemoveAt(i);
                }
            }
        } finally {
            IsLoading = false;
        }
    }
}
