using LiteDB;
using Microsoft.Extensions.DependencyInjection;
using Model.Database.Entities;

namespace Model.Database;

internal sealed class Statuses {
    private readonly LiteDatabase db;
    private readonly ILiteCollection<StatusEntry> statuses;

    public Statuses(string account) {
        db = Utilities.Services.Provider.GetRequiredService<LiteDatabase>();
        statuses = db.GetCollection<StatusEntry>($"{account}:statuses");
    }

    public StatusEntry? Find(string id) {
        return statuses.FindById(id);
    }


}
