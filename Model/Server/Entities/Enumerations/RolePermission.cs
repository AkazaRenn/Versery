namespace Model.Server.Entities.Enumerations;
/// <summary>
/// To determine the permissions available to a certain role, convert the permissions attribute to binary and compare from the least significant bit upwards. For convenience (and to prevent the terms from growing too long), permissions will be presented below using hexadecimal values.
/// Version: 4.0.0
/// </summary>
/// <see href="https://docs.joinmastodon.org/entities/Role/#permission-flags">Mastodon API Documentation</see>
[Flags]
public enum RolePermission {
    /// <summary>
    /// Administrator. Users with this permission bypass all permissions.
    /// </summary>
    Administrator = 0x1,

    /// <summary>
    /// Devops. Allows users to access Sidekiq and PgHero dashboards.
    /// </summary>
    Devops = 0x2,

    /// <summary>
    /// View Audit Log. Allows users to see history of admin actions.
    /// </summary>
    ViewAuditLog = 0x4,

    /// <summary>
    /// View Dashboard. Allows users to access the dashboard and various metrics.
    /// </summary>
    ViewDashboard = 0x8,

    /// <summary>
    /// Manage Reports. Allows users to review reports and perform moderation actions against them.
    /// </summary>
    ManageReports = 0x10,

    /// <summary>
    /// Manage Federation. Allows users to block or allow federation with other domains, and control deliverability.
    /// </summary>
    ManageFederation = 0x20,

    /// <summary>
    /// Manage Settings. Allows users to change site settings.
    /// </summary>
    ManageSettings = 0x40,

    /// <summary>
    /// Manage Blocks. Allows users to block e-mail providers and IP addresses.
    /// </summary>
    ManageBlocks = 0x80,

    /// <summary>
    /// Manage Taxonomies. Allows users to review trending content and update hashtag settings.
    /// </summary>
    ManageTaxonomies = 0x100,

    /// <summary>
    /// Manage Appeals. Allows users to review appeals against moderation actions.
    /// </summary>
    ManageAppeals = 0x200,

    /// <summary>
    /// Manage Users. Allows users to view other users’ details and perform moderation actions against them.
    /// </summary>
    ManageUsers = 0x400,

    /// <summary>
    /// Manage Invites. Allows users to browse and deactivate invite links.
    /// </summary>
    ManageInvites = 0x800,

    /// <summary>
    /// Manage Rules. Allows users to change server rules.
    /// </summary>
    ManageRules = 0x1000,

    /// <summary>
    /// Manage Announcements. Allows users to manage announcements on the server.
    /// </summary>
    ManageAnnouncements = 0x2000,

    /// <summary>
    /// Manage Custom Emojis. Allows users to manage custom emojis on the server.
    /// </summary>
    ManageCustomEmojis = 0x4000,

    /// <summary>
    /// Manage Webhooks. Allows users to set up webhooks for administrative events.
    /// </summary>
    ManageWebhooks = 0x8000,

    /// <summary>
    /// Invite Users. Allows users to invite new people to the server.
    /// </summary>
    InviteUsers = 0x10000,

    /// <summary>
    /// Manage Roles. Allows users to manage and assign roles below theirs.
    /// </summary>
    ManageRoles = 0x20000,

    /// <summary>
    /// Manage User Access. Allows users to disable other users’ two-factor authentication, change their e-mail address, and reset their password.
    /// </summary>
    ManageUserAccess = 0x40000,

    /// <summary>
    /// Delete User Data. Allows users to delete other users’ data without delay.
    /// </summary>
    DeleteUserData = 0x80000,

    /// <summary>
    /// View live and topic feeds. Allows users to view public “firehose”, hashtag and link feeds even when those are disabled.
    /// </summary>
    ViewLiveAndTopicFeeds = 0x100000
}