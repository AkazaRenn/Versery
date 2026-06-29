using CommunityToolkit.Mvvm.ComponentModel;
using Model.Access;
using Model.Entities;

namespace ViewModel.Controls;

public sealed partial class Status: ObservableObject {
    private readonly Client client = Utilities.Services.Get<Client>();

    public string Id { get; }

    [ObservableProperty]
    public partial string PosterId { get; set; }
    [ObservableProperty]
    public partial string PosterDisplayName { get; set; }
    [ObservableProperty]
    public partial DateTime CreatedAt { get; set; }
    [ObservableProperty]
    public partial string Uri { get; set; }
    [ObservableProperty]
    public partial string TextContent { get; set; }
    [ObservableProperty]
    public partial bool Reblogged { get; set; }
    [ObservableProperty]
    public partial bool Favourited { get; set; }
    [ObservableProperty]
    public partial bool Bookmarked { get; set; }

    public Status(Timeline timeline) {
        var status = client.GetStatus(timeline.Id);
        if (status is null) {
            throw new ArgumentNullException(null, nameof(status));
        }
        var account = client.GetAccount(status.AccountId);
        if (account is null) {
            throw new ArgumentNullException(null, nameof(account));
        }

        Id = timeline.Id;

        PosterId = account.AccountName;
        PosterDisplayName = account.DisplayName;
        CreatedAt = status.CreatedAt;
        Uri = status.Uri;
        TextContent = status.Content;
        Reblogged = status.Reblogged;
        Favourited = status.Favourited;
        Bookmarked = status.Bookmarked;
    }

    public void LoadMore(uint index) {
        throw new NotImplementedException();
    }
}
