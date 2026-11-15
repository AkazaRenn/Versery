namespace Model.Interfaces;

public interface IDatabaseEntity {
    public DateTimeOffset ExpiresAt { get; init; }
}