using Model.Server.Entities;

namespace ViewModel.Enumerations;

public enum NotificationType {
    Unknown,
    Mention,
    Status,
    Reblog,
    Follow,
    Follow_request,
    Favourite,
    Poll,
    Update,
    Admin_sign_up,
    Admin_report,
    Severed_relationships,
    Moderation_warning,
    Quote,
    Quoted_update,
    Added_to_collection,
    Collection_update,
}

internal static class NotificationTypeMethods {
    extension(NotificationTypeEnum serverNotificationType) {
        public NotificationType Client {
            get => serverNotificationType switch {
                NotificationTypeEnum.Mention => NotificationType.Mention,
                NotificationTypeEnum.Status => NotificationType.Status,
                NotificationTypeEnum.Reblog => NotificationType.Reblog,
                NotificationTypeEnum.Follow => NotificationType.Follow,
                NotificationTypeEnum.Follow_request => NotificationType.Follow_request,
                NotificationTypeEnum.Favourite => NotificationType.Favourite,
                NotificationTypeEnum.Poll => NotificationType.Poll,
                NotificationTypeEnum.Update => NotificationType.Update,
                NotificationTypeEnum.Admin_sign_up => NotificationType.Admin_sign_up,
                NotificationTypeEnum.Admin_report => NotificationType.Admin_report,
                NotificationTypeEnum.Severed_relationships => NotificationType.Severed_relationships,
                NotificationTypeEnum.Moderation_warning => NotificationType.Moderation_warning,
                NotificationTypeEnum.Quote => NotificationType.Quote,
                NotificationTypeEnum.Quoted_update => NotificationType.Quoted_update,
                NotificationTypeEnum.Added_to_collection => NotificationType.Added_to_collection,
                NotificationTypeEnum.Collection_update => NotificationType.Collection_update,
                _ => NotificationType.Unknown,
            };
        }
    }
}
