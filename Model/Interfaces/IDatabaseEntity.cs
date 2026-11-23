namespace Model.Interfaces;

public interface IDatabaseEntity {
    public string Id { get; }
    public string IndexId { get; }
    public DateTimeOffset ExpiresAt { get; init; }
}