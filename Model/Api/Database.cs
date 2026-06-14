using LiteDB;
using Microsoft.Extensions.DependencyInjection;
using Model.Database;
using Model.Database.Entities;

namespace Model.Api;

public sealed class Database(string userId) {
    public string UserId => userId;
    public Timeline HomeTimeline { get; } = new(userId, "home_timeline");
    public Timeline FederatedTimeline { get; } = new(userId, "federated_timeline");
    public Timeline LocalTimeline { get; } = new(userId, "local_timeline");

    private ILiteCollection<StatusEntry> statuses = Utilities.Services.Provider.GetRequiredService<LiteDatabase>().GetCollection<StatusEntry>($"{userId}:statuses");
}
