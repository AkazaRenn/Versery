using System.Runtime.CompilerServices;
using Windows.Storage;

namespace Model; 
internal class Settings: SettingsContainer {
    int Delay {
        get => Get(1000);
        set => Set(value);
    }

    bool AutoStart {
        get => Get(false);
        set => Set(value);
    }

    ApplicationSettings Application {
        get => Get(() => new ApplicationSettings(this));
        set => Set(value);
    }

    public class ApplicationSettings(SettingsContainer parent): NestedSettingsContainer(parent) {
        int Delay {
            get => Get(1000);
            set => Set(value);
        }

        bool AutoStart {
            get => Get(false);
            set => Set(value);
        }
    }

    public override ApplicationDataContainer Container { get => ApplicationData.Current.LocalSettings; }
}

internal abstract class AnonymousSettingsContainer(SettingsContainer parent): NestedSettingsContainer(parent) {
    protected string Get(string key) {
        if (Value.TryGetValue(key, out var value) && value is string typedValue) {
            return typedValue;
        } else {
            return string.Empty;
        }
    }

    protected void Set(string value, string key) {
        Value[key] = value;
        Container.Values[key] = value;
    }
}

internal abstract class NestedSettingsContainer(SettingsContainer parent): SettingsContainer {
    public override ApplicationDataContainer Container => parent.Container.CreateContainer(GetType().ToString(), ApplicationDataCreateDisposition.Always);
}

internal abstract class SettingsContainer {
    private readonly Dictionary<string, object> value;

    public abstract ApplicationDataContainer Container { get; }
    protected Dictionary<string, object> Value => value;

    protected SettingsContainer() {
        value = Container.Values.ToDictionary(kv => kv.Key, kv => kv.Value);
    }

    protected T Get<T>(T defaultValue, [CallerMemberName] string name = "")
        where T : notnull {
        if (Value.TryGetValue(name, out var value) && value is T typedValue) {
            return typedValue;
        } else {
            Set(defaultValue);
            return defaultValue;
        }
    }

    protected T Get<T>(Func<T> defaultFactory, [CallerMemberName] string name = "")
        where T : notnull {
        if (Value.TryGetValue(name, out var value) && value is T typedValue) {
            return typedValue;
        } else {
            T defaultValue = defaultFactory();
            Set(defaultValue);
            return defaultValue;
        }
    }

    protected void Set<T>(T value, [CallerMemberName] string key = "")
        where T : notnull {
        Value[key] = value;
        Container.Values[key] = value;
    }
}
