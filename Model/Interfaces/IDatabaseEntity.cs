namespace Model.Interfaces;

public interface IDatabaseEntity {
    public string Id { get; }
    public DateTimeOffset ExpiresAt { get; init; }
}