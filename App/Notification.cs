using Microsoft.Windows.PushNotifications;

namespace App;

public sealed class Notification {
    public Notification() {
        // PushNotificationManager.Default.Register();
    }

    public async void Initialize() {
        if (PushNotificationManager.IsSupported()) {
            var channel = await PushNotificationManager.Default.CreateChannelAsync(Model.Constants.MsaAppId);
            Console.WriteLine($"Notification channel created: {channel.Channel.Uri}");
        }
    }
}
