namespace View.Interfaces;

public interface INavigationPage {
    public static abstract Type Type { get; }

    public Task OnNavigationReInvoke();
}
