using CommunityToolkit.Mvvm.ComponentModel;

namespace ViewModel.Controls;
public sealed partial class Status(Mastonet.Entities.Status status): ObservableObject {
    [ObservableProperty]
    public partial string DisplayName { get; set; } = status.Account.DisplayName;
    [ObservableProperty]
    public partial string Id { get; set; } = status.Account.Id;
    [ObservableProperty]
    public partial DateTime CreatedAt { get; set; } = status.CreatedAt;
    [ObservableProperty]
    public partial string Uri { get; set; } = status.Uri;
    [ObservableProperty]
    public partial string Content { get; set; } = status.Content;

}
