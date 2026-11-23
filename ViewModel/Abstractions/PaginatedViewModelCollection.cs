using System.Collections.ObjectModel;
using ViewModel.Interfaces;

namespace ViewModel.Abstractions;

public abstract class PaginatedViewModelCollection<ViewModel, Entity>: Batch
    where ViewModel : IPaginatedViewModel<ViewModel, Entity>
    where Entity : Model.Interfaces.IDatabaseEntity {
    protected abstract Entity[] GetFromDatabase(string? afterId = null);
    protected abstract Task<Entity[]> GetFromServer(string? afterId = null);

    private bool loading = false;
    private bool hasMoreEntitiesToLoad = true;

    public ObservableCollection<ViewModel> ViewModels { get; } = [];

    public PaginatedViewModelCollection() {
        _ = LoadInitialItems();
    }

    protected async Task LoadInitialItems() {
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
        if (loading) {
            return;
        }

        await LoadMoreItems(0);
    }

    public async Task OnItemRealized(int index) {
        ViewModels[index].DownloadMedia();

        if ((index == ViewModels.Count - 1) && (!loading) && hasMoreEntitiesToLoad) {
            await LoadMoreItems(index + 1, ViewModels[index].Id, true);
        }
    }

    private async Task LoadMoreItems(int startingIndex, string? afterId = null, bool useDatabase = false) {
        loading = true;

        try {
            var entities = await Task.Run(async () => {
                var entities = await GetFromServer(afterId);
                if (useDatabase && entities.Length == 0) {
                    entities = GetFromDatabase(afterId);

                }
                return entities;
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
                // Add new items to the beginning
                int overlappingStartingIndex = Array.FindIndex(entities, x => x.Id == ViewModels[startingIndex].Id);
                for (int i = 0; i < overlappingStartingIndex; i++) {
                    int targetIndex = i + startingIndex;
                    ViewModels.Insert(targetIndex, viewModels[i]);
                    ViewModels[targetIndex].Update(entities[i], currentVersion);
                }

                // Update existing items and remove any that are no longer present
                for (int i = overlappingStartingIndex; i < entities.Length;) {
                    int targetIndex = i + startingIndex;
                    if (targetIndex >= ViewModels.Count) {
                        foreach (var viewModel in viewModels.Skip(i)) {
                            ViewModels.Add(viewModel);
                        }
                        break;
                    }
                    if (entities[i].Id == ViewModels[targetIndex].Id) {
                        ViewModels[targetIndex].Update(entities[i], currentVersion);
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
