using Model.Interfaces;

namespace ViewModel.Interfaces;

public interface IPaginatedViewModel<ViewModel, Entity>
    where ViewModel : IPaginatedViewModel<ViewModel, Entity>
    where Entity : IDatabaseEntity {
    public string Id { get; }
    public void DownloadMedia();
    public void Update(Entity entity, int version);
    public static abstract ViewModel Create(Entity entity, int version);
    public static abstract IEnumerable<ViewModel> FromEntities(IEnumerable<Entity> entities, int version);
}
