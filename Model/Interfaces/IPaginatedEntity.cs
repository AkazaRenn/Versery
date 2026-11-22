namespace Model.Interfaces;

public interface IPaginatedEntity: IDatabaseEntity {
    public bool FollowedByGap { get; set; }
}
