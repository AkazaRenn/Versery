using System.Collections.ObjectModel;
using ViewModel.Interfaces;

namespace ViewModel.Abstractions;

public abstract class PaginatedViewModelCollection<ViewModel, Entity>: Batch
    where ViewModel : IPaginatedViewModel<ViewModel, Entity>
    where Entity : Model.Interfaces.IPaginatedEntity {
    protected abstract Entity[] GetFromDatabase(string? afterId = null);
    protected abstract Task<Entity[]> GetFromServer();

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

        try {
            var entities = await Task.Run(async () => {
                return await GetFromServer();
            });

            if (entities.Length == 0) {
                return;
            }

            int currentVersion = ++version;

            // No overlapping, add all to the beginning
            if (entities.Last().FollowedByGap || ViewModels.Count == 0) {
                var newViewModels = await Task.Run(() => {
                    return ViewModel.FromEntities(entities, currentVersion).ToArray();
                });
                ViewModels.Clear();
                for (int i = 0; i < newViewModels.Length; i++) {
                    ViewModels.Insert(i, newViewModels[i]);
                }
                return;
            }

            {
                // Add new items to the beginning
                var newViewModels = await Task.Run(() => {
                    return ViewModel.FromEntities(entities.TakeWhile(x => x.Id != ViewModels[0].Id), currentVersion).ToArray();
                });
                for (int i = 0; i < newViewModels.Length; i++) {
                    ViewModels.Insert(i, newViewModels[i]);
                    ViewModels[i].Update(entities[i], currentVersion);
                }

                // Update existing items and remove any that are no longer present
                for (int i = newViewModels.Length; i < entities.Length;) {
                    if (entities[i].Id == ViewModels[i].Id) {
                        ViewModels[i].Update(entities[i], currentVersion);
                        i++;
                    } else {
                        ViewModels.RemoveAt(i);
                    }
                }

                // Clear the rest of the items
                while (ViewModels.Count > entities.Length) {
                    ViewModels.RemoveAt(ViewModels.Count - 1);
                }
            }
        } finally {
            loading = false;
        }
    }

    public async Task OnItemRealized(int index) {
        ViewModels[index].DownloadMedia();

        if ((index == ViewModels.Count - 1) && (!loading) && hasMoreEntitiesToLoad) {
            loading = true;
            var viewModels = await Task.Run(() => {
                var entities = GetFromDatabase(ViewModels.Last().Id);
                return ViewModel.FromEntities(entities, version).ToArray();
            });
            if (viewModels.Length == 0) {
                hasMoreEntitiesToLoad = false;
            } else {
                foreach (var viewModel in viewModels) {
                    ViewModels.Add(viewModel);
                }
            }
            loading = false;
        }
    }
}
