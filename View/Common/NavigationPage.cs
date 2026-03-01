namespace View.Common; 
internal interface INavigationPage {
    public static abstract Type Type { get; }

    public void OnNavigationReInvoke() { }
}
