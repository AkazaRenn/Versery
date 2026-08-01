using Model.Server.Entities;

namespace Model.Server;

partial class Client {

    /// <summary>
    /// Retrieving Home timeline
    /// </summary>
    /// <param name="options">Define the first and last items to get</param>
    /// <returns>Returns an array of Statuses, most recent ones first</returns>
    public Task<MastodonList<Status>> GetHomeTimeline(ArrayOptions? options = null) {
        string url = "/api/v1/timelines/home";
        if (options != null) {
            url += "?" + options.ToQueryString();
        }
        return GetMastodonList<Status>(url);
    }

    /// <summary>
    /// Conversations (direct messages) for an account
    /// </summary>
    /// <param name="options">Define the first and last items to get</param>
    /// <returns>Returns array of Conversation</returns>
    public Task<MastodonList<Conversation>> GetConversations(ArrayOptions? options = null) {
        string url = "/api/v1/conversations";
        if (options != null) {
            url += "?" + options.ToQueryString();
        }
        return GetMastodonList<Conversation>(url);
    }

    /// <summary>
    /// Remove conversation
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public Task DeleteConversation(string id) {
        return Delete("/api/v1/conversations/" + id);
    }

    /// <summary>
    /// Mark as read
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public Task<Conversation> MarkAsRead(string id) {
        return Post<Conversation>($"/api/v1/conversations/{id}/read");
    }

    /// <summary>
    /// Retrieving Public timeline
    /// </summary>
    /// <param name="options">Define the first and last items to get</param>
    /// <param name="local">Only return statuses originating from this instance</param>
    /// <param name="onlyMedia">Only statuses with media attachments</param>
    /// <returns>Returns an array of Statuses, most recent ones first</returns>
    public Task<MastodonList<Status>> GetPublicTimeline(ArrayOptions? options = null, bool local = false, bool onlyMedia = false) {
        string url = "/api/v1/timelines/public";

        var queryParams = "";
        if (local) {
            queryParams += "?local=true";
        }
        if (onlyMedia) {
            queryParams += (queryParams != "" ? "&" : "?") + "only_media=true";
        }
        if (options != null) {
            queryParams += (queryParams != "" ? "&" : "?") + options.ToQueryString();
        }

        return GetMastodonList<Status>(url + queryParams);
    }

    /// <summary>
    /// Retrieving Tag timeline
    /// </summary>
    /// <param name="hashtag">The tag to retieve</param>
    /// <param name="options">Define the first and last items to get</param>
    /// <param name="local">Only return statuses originating from this instance</param>
    /// <param name="onlyMedia">Only statuses with media attachments</param>
    /// <returns>Returns an array of Statuses, most recent ones first</returns>
    public Task<MastodonList<Status>> GetTagTimeline(string hashtag, ArrayOptions? options = null, bool local = false, bool onlyMedia = false) {
        string url = "/api/v1/timelines/tag/" + hashtag;

        var queryParams = "";
        if (local) {
            queryParams += "?local=true";
        }
        if (onlyMedia) {
            queryParams += (queryParams != "" ? "&" : "?") + "only_media=true";
        }
        if (options != null) {
            queryParams += (queryParams != "" ? "&" : "?") + options.ToQueryString();
        }

        return GetMastodonList<Status>(url + queryParams);
    }

    /// <summary>
    /// Retrieving List timeline
    /// </summary>
    /// <param name="listId"></param>
    /// <param name="options">Define the first and last items to get</param>
    /// <returns>Returns an array of Statuses, most recent ones first</returns>
    public Task<MastodonList<Status>> GetListTimeline(long listId, ArrayOptions? options = null) {
        string url = "/api/v1/timelines/list/" + listId;

        if (options != null) {
            url += "?" + options.ToQueryString();
        }

        return GetMastodonList<Status>(url);
    }
}
