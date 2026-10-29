namespace Model.Interfaces;

public interface IPaginatedEntity {
    public string Id { get; }
    public bool FollowedByGap { get; }
}