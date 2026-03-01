using System.Runtime.CompilerServices;
using Windows.Storage;

namespace Model.DataPersistence; 
public class ApplicationStates {
    private ApplicationDataContainer Container => ApplicationData.Current.LocalSettings.CreateContainer(this.GetType().FullName, ApplicationDataCreateDisposition.Always);

    private void Set<T>(ref T field, T value, [CallerMemberName] string name = "")
        where T : notnull {
        field = value;
        Container.Values.Add(name, value);
    }

    private void Initialize<T>(ref T value, string key) {
        if (Container.Values.TryGetValue(key, out object? valueObject) && valueObject is T typedValue) {
            value = typedValue;
        }
    }

    private string activeUser = string.Empty;
    public string ActiveUser {
        get => activeUser;
        set => Set(ref activeUser, value);
    }

    public ApplicationStates() {
        Initialize(ref activeUser, nameof(ActiveUser));
    }
}
