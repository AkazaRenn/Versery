namespace Model.Server.Entities.Enumerations;
/// <see href="https://docs.joinmastodon.org/entities/Role/#permission-flags">Mastodon API Documentation</see>
[Flags]
public enum RolePermission {
    Administrator = 0x1,

    Devops = 0x2,

    ViewAuditLog = 0x4,

    ViewDashboard = 0x8,

    ManageReports = 0x10,

    ManageFederation = 0x20,

    ManageSettings = 0x40,

    ManageBlocks = 0x80,

    ManageTaxonomies = 0x100,

    ManageAppeals = 0x200,

    ManageUsers = 0x400,

    ManageInvites = 0x800,

    ManageRules = 0x1000,

    ManageAnnouncements = 0x2000,

    ManageCustomEmojis = 0x4000,

    ManageWebhooks = 0x8000,

    InviteUsers = 0x10000,

    ManageRoles = 0x20000,

    ManageUserAccess = 0x40000,

    DeleteUserData = 0x80000,

    ViewLiveAndTopicFeeds = 0x100000
}