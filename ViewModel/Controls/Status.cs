using CommunityToolkit.Mvvm.ComponentModel;

namespace ViewModel.Controls; 
public partial class Status(Mastonet.Entities.Status status): ObservableObject {
    [ObservableProperty]
    private string dispalyName = status.Account.DisplayName;
    [ObservableProperty]
    private string id = status.Account.Id;
    [ObservableProperty]
    private DateTime createdAt = status.CreatedAt;
    [ObservableProperty]
    private string uri = status.Uri;
    [ObservableProperty]
    private string content = status.Content;
}
