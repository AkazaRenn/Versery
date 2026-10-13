namespace Model.Interfaces;

public interface IPaginatedEntity {
    public string GetId();
    public bool IsFollowedByGap();
}