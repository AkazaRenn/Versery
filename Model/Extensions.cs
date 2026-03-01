using Mastonet;

namespace Model; 
internal static class Extensions {
    extension(MastodonClient mastodonClient) {
        public async Task<string> GetFullUserId() {
            var account = await mastodonClient.GetCurrentUser();
            var instance = await mastodonClient.GetInstanceV2();
            return $"{account.UserName}@{instance.Domain}";
        }
    }
}
