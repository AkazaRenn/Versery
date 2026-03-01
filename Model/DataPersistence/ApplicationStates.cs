using System.Runtime.CompilerServices;
using Windows.Storage;

namespace Model.DataPersistence; 
public class ApplicationStates {
    private ApplicationDataContainer Container => ApplicationData.Current.LocalSettings.CreateContainer(this.GetType().FullName, ApplicationDataCreateDisposition.Always);

    private void Set<T>(T value, [CallerMemberName] string name = "")
        where T : notnull {
        Container.Values.Add(name, value);
    }

    private void InitializeValue<T>(string key, ref T value) {
        if (Container.Values.TryGetValue(key, out object? valueObject) && valueObject is T typedValue) {
            value = typedValue;
        }
    }

    private string activeUser = string.Empty;
    public string ActiveUser {
        get => activeUser;
        set { 
            activeUser = value;
            Set(value);
        }
    }

    public ApplicationStates() {
        InitializeValue(nameof(ActiveUser), ref activeUser);
    }
}
