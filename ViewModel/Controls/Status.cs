using CommunityToolkit.Mvvm.ComponentModel;
using Model.Entities;

namespace ViewModel.Controls;

public sealed partial class Status(Status status, Account account): ObservableObject {
    public string PosterId { get; } = account.AccountName;
    public string DisplayName { get; } = account.DisplayName;
    public DateTime CreatedAt { get; } = status.CreatedAt;
    public string Uri { get; } = status.Uri;
    public string Content { get; } = status.Content;
    public bool Reblogged { get; } = status.Reblogged;
    public bool Favourited { get; } = status.Favourited;
    public bool Bookmarked { get; } = status.Bookmarked;
}
