using System.Collections.ObjectModel;
using ViewModel.Interfaces;

namespace ViewModel.Abstractions;

public abstract class PaginatedViewModelCollection<ViewModel, Entity>: Batch
    where ViewModel : IPaginatedViewModel<ViewModel, Entity>
    where Entity : Model.Interfaces.IPaginatedEntity {
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
        loading = true;
        await LoadMoreItems(0);
        loading = false;
    }

    private async Task<bool> LoadMoreItems(int startingIndex, string? afterId = null, bool useDatabase = false) {
        bool usedDatabase = false;

        var entities = await Task.Run(async () => {
            var entities = await GetFromServer(afterId);
            if (useDatabase && entities.Length == 0) {
                entities = GetFromDatabase(afterId);
                usedDatabase = true;

            }
            return entities;
        });

        if (entities.Length == 0) {
            hasMoreEntitiesToLoad = false;
            return usedDatabase;
        }

        int currentVersion = ++version;

        // No overlapping, add all to the beginning
        if (entities.Last().FollowedByGap || ViewModels.Count == startingIndex) {
            var newViewModels = await Task.Run(() => {
                return ViewModel.FromEntities(entities, currentVersion).ToArray();
            });
            if (startingIndex == 0) {
                ViewModels.Clear();
            } else {
                for (int i = ViewModels.Count - 1; i >= startingIndex; i--) {
                    ViewModels.RemoveAt(i);
                }
            }
            for (int i = 0; i < newViewModels.Length; i++) {
                ViewModels.Insert(i + startingIndex, newViewModels[i]);
            }
            return usedDatabase;
        }

        {
            // Add new items to the beginning
            var newViewModels = await Task.Run(() => {
                return ViewModel.FromEntities(entities.TakeWhile(x => x.Id != ViewModels[startingIndex].Id), currentVersion).ToArray();
            });
            for (int i = 0; i < newViewModels.Length; i++) {
                ViewModels.Insert(i + startingIndex, newViewModels[i]);
                ViewModels[i + startingIndex].Update(entities[i], currentVersion);
            }

            // Update existing items and remove any that are no longer present
            for (int i = newViewModels.Length; i < entities.Length;) {
                if (entities[i].Id == ViewModels[i + startingIndex].Id) {
                    ViewModels[i + startingIndex].Update(entities[i], currentVersion);
                    i++;
                } else {
                    ViewModels.RemoveAt(i + startingIndex);
                }
            }

            // Clear the rest of the items
            for (int i = ViewModels.Count - 1; i >= entities.Length + startingIndex; i--) {
                ViewModels.RemoveAt(i);
            }
        }

        return usedDatabase;
    }

    public async Task OnItemRealized(int index) {
        ViewModels[index].DownloadMedia();

        if (((index == ViewModels.Count - 1) || ViewModels[index].FollowedByGap) && (!loading) && hasMoreEntitiesToLoad) {
            loading = true;
            bool usedDatabase = await LoadMoreItems(index + 1, ViewModels[index].Id, true);
            ViewModels[index].FollowedByGap = ViewModels[index].FollowedByGap && usedDatabase;
            loading = false;
        }
    }
}
