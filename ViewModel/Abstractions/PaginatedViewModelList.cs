using System.Collections.ObjectModel;
using System.Collections.Specialized;
using ViewModel.Interfaces;

namespace ViewModel.Abstractions;

public abstract class PaginatedViewModelList<ViewModel, Entity>
    where ViewModel : IPaginatedViewModel<ViewModel, Entity>
    where Entity : Model.Interfaces.IPaginatedEntity {
    protected abstract void OnAddToViewModelsCollection(ViewModel viewModel);
    protected abstract Entity[] GetFromDatabase(string? afterId = null);
    protected abstract Task<Entity[]> GetFromServer();

    private bool loadingOldEntities = false;
    private bool hasMoreEntitiesToLoad = true;

    public ObservableCollection<ViewModel> ViewModels { get; } = [];

    public PaginatedViewModelList() {
        ViewModels.CollectionChanged += Items_CollectionChanged;

        _ = LoadInitialItems();
    }

    private void Items_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e) {
        switch (e.Action) {
        case NotifyCollectionChangedAction.Add:
        case NotifyCollectionChangedAction.Replace:
            if (e.NewItems is null) {
                return;
            }

            foreach (ViewModel viewModel in e.NewItems) {
                OnAddToViewModelsCollection(viewModel);
            }
            break;
        }
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
        if (entities.Last().IsFollowedByGap()) {
            var newEntities = await Task.Run(() => {
                return ViewModel.FromEntities(entities).ToArray();
            });
            for (int i = 0; i < newEntities.Length; i++) {
                ViewModels.Insert(i, newEntities[i]);
            }
            return;
        }

        { // Remove no-longer-existing ones, update overlapping ones
            var newEntityIds = entities.Select(t => t.GetId()).ToHashSet();
            var indexToRemove = new List<int>();
            for (int i = 0; i < ViewModels.Count; i++) {
                if (newEntityIds.Contains(ViewModels[i].Id)) {
                    // try to update the item
                } else {
                    indexToRemove.Add(i);
                }

                if (ViewModels[i].Id == entities[^1].GetId()) {
                    break;
                }
            }

            for (int i = indexToRemove.Count - 1; i >= 0; i--) {
                ViewModels.RemoveAt(indexToRemove[i]);
            }
        }

        { // Add the rest to the beginning
            var newEntities = await Task.Run(() => {
                var entitiesToAddEndIndex = Array.FindIndex(entities, x => x.GetId() == ViewModels[0].Id);
                Entity[] entitiesToAdd;
                if (entitiesToAddEndIndex < 0) {
                    entitiesToAdd = entities;
                } else {
                    entitiesToAdd = entities[..entitiesToAddEndIndex];
                }
                return ViewModel.FromEntities(entitiesToAdd).ToArray();
            });
            for (int i = 0; i < newEntities.Length; i++) {
                ViewModels.Insert(i, newEntities[i]);
            }
        }
    }

    public async Task OnItemRealized(int index) {
        if ((index < ViewModels.Count - 1) ||
            !ViewModels.Any() ||
            loadingOldEntities ||
            !hasMoreEntitiesToLoad) {
            return;
        }

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
