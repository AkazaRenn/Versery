namespace Utilities.Interfaces; 
public interface INavigationPage {
    public static abstract Type Type { get; }

    public void OnNavigationReInvoke();
}
