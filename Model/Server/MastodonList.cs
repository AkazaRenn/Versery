namespace Model.Server;

public class MastodonList<T>: List<T> {
    public string? NextPageMaxId { get; set; }
    public string? PreviousPageSinceId { get; set; }
    public string? PreviousPageMinId { get; set; }
}