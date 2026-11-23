using CommunityToolkit.Mvvm.Messaging;
using Model;
using Model.Access;
using System.Collections.ObjectModel;
using ViewModel.Interfaces;

namespace ViewModel.Abstractions;

public abstract class PaginatedViewModelCollection<ViewModel, Entity>: Batch, IRecipient<Messages.SignInCompleted>
    where ViewModel : IPaginatedViewModel<ViewModel, Entity>
    where Entity : Model.Interfaces.IDatabaseEntity {
    public ObservableCollection<ViewModel> ViewModels { get; } = [];

    protected abstract Entity[] GetFromDatabase(string? afterId = null);
    protected abstract Task<Entity[]> GetFromServer(string? afterId = null);
    protected readonly Client client = Model.Services.Get<Client>();

    private bool loading = false;
    private bool hasMoreEntitiesToLoad = true;
    private bool CanLoad => client.SignedIn && !loading;

    public PaginatedViewModelCollection() {
        StrongReferenceMessenger.Default.Register<Messages.SignInCompleted>(this);
        _ = LoadInitialItems();
    }

    public void Receive(Messages.SignInCompleted message) {
        ViewModels.Clear();
        _ = LoadInitialItems();
    }

    private async Task LoadInitialItems() {
        if (!CanLoad) {
            return;
        }

        loading = true;

        var entities = GetFromDatabase();
        if (entities.Length == 0) {
            entities = await GetFromServer();
        }

        int currentVersion = ++version;

        var viewModels = ViewModel.FromEntities(entities, currentVersion);
        foreach (var viewModel in viewModels) {
            ViewModels.Add(viewModel);
        }

        loading = false;
    }

    public Func<Task> LoadLatestItems => LoadLatestItemsInternal;
    private async Task LoadLatestItemsInternal() {
        await LoadMoreItems(0);
    }

    public async Task OnItemRealized(int index) {
        ViewModels[index].DownloadMedia();
        if ((index == ViewModels.Count - 1) && hasMoreEntitiesToLoad) {
            await LoadMoreItems(index + 1, ViewModels[index].Id);
        }
    }

    private async Task LoadMoreItems(int startingIndex, string? afterId = null) {
        if (!CanLoad) {
            return;
        }

        loading = true;

        try {
            var entities = await Task.Run(async () => {
                return await GetFromServer(afterId);
            });

            if (entities.Length == 0) {
                hasMoreEntitiesToLoad = false;
                return;
            }

            int currentVersion = ++version;

            var viewModels = await Task.Run(() => {
                return ViewModel.FromEntities(entities, currentVersion).ToArray();
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
            loading = false;
        }
    }
}
