using CommunityToolkit.Mvvm.ComponentModel;

namespace ViewModel.Abstractions;

public abstract class PaginatedViewModel(string id): ObservableObject {
    public string Id { get; } = id;
    public bool Realized { get; set; } = false;

    public virtual string PaginateId => Id;
    public abstract void DownloadMedia();
    public abstract void Update(int version);
}
