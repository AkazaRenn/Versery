using System.Collections.ObjectModel;
using ViewModel.Interfaces;

namespace ViewModel.Abstractions;

public abstract class PaginatedViewModelCollection<ViewModel, Entity>
    where ViewModel : IPaginatedViewModel<ViewModel, Entity>
    where Entity : Model.Interfaces.IPaginatedEntity {
    protected abstract Entity[] GetFromDatabase(string? afterId = null);
    protected abstract Task<Entity[]> GetFromServer();

    private bool loadingOldEntities = false;
    private bool hasMoreEntitiesToLoad = true;

    public ObservableCollection<ViewModel> ViewModels { get; } = [];

    public PaginatedViewModelCollection() {
        _ = LoadInitialItems();
    }

    protected async Task LoadInitialItems() {
        var entities = GetFromDatabase();
        if (entities.Length == 0) {
            entities = await GetFromServer();
        }

        var viewModels = ViewModel.FromEntities(entities);
        foreach (var viewModel in viewModels) {
            ViewModels.Add(viewModel);
        }
    }

    public Func<Task> LoadLatestItems => LoadLatestItemsInternal;
    private async Task LoadLatestItemsInternal() {
        var entities = await Task.Run(async () => {
            return await GetFromServer();
        });

        if (entities.Length == 0) {
            return;
        }

        // No overlapping, add all to the beginning
        if (entities.Last().FollowedByGap || ViewModels.Count == 0) {
            var newViewModels = await Task.Run(() => {
                return ViewModel.FromEntities(entities).ToArray();
            });
            ViewModels.Clear();
            for (int i = 0; i < newViewModels.Length; i++) {
                ViewModels.Insert(i, newViewModels[i]);
            }
            return;
        }

        {
            // Add new items to the beginning
            var newEntities = new List<Entity>();
            for (int i = 0; i < entities.Length; i++) {
                if (entities[i].Id == ViewModels[0].Id) {
                    break;
                }
                newEntities.Add(entities[i]);
            }

            var newViewModels = await Task.Run(() => {
                return ViewModel.FromEntities(newEntities).ToArray();
            });
            for (int i = 0; i < newViewModels.Length; i++) {
                ViewModels.Insert(i, newViewModels[i]);
            }

            // Update existing items and remove any that are no longer present
            for (int i = newEntities.Count; i < entities.Length;) {
                if (entities[i].Id == ViewModels[i].Id) {
                    ViewModels[i].Update(entities[i]);
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
    }

    public async Task OnItemRealized(int index) {
        ViewModels[index].DownloadMedia();

        if ((index == ViewModels.Count - 1) && (!loadingOldEntities) && hasMoreEntitiesToLoad) {
            loadingOldEntities = true;
            var viewModels = await Task.Run(() => {
                var entities = GetFromDatabase(ViewModels.Last().Id);
                return ViewModel.FromEntities(entities).ToArray();
            });
            if (viewModels.Length == 0) {
                hasMoreEntitiesToLoad = false;
            } else {
                foreach (var viewModel in viewModels) {
                    ViewModels.Add(viewModel);
                }
            }
            loadingOldEntities = false;
        }
    }
}
