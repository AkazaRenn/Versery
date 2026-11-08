using Model.Interfaces;

namespace ViewModel.Interfaces;

public interface IPaginatedViewModel {
    public int Index { set; }
}

public interface IPaginatedViewModel<ViewModel, Entity>: IPaginatedViewModel
    where ViewModel : IPaginatedViewModel<ViewModel, Entity>
    where Entity : IPaginatedEntity {
    public string Id { get; }
    public void Update(Entity entity);
    public static abstract IEnumerable<ViewModel> FromEntities(IEnumerable<Entity> entities);
}
