using Model.Server.Entities;

namespace ViewModel.Enumerations;

public enum Visibility {
    Public,
    Unlisted,
    Private,
    Direct,
}

internal static class VisibilityMethods {
    extension(Visibility visibility) {
        public StatusVisibilityEnum Server {
            get => visibility switch {
                Visibility.Unlisted => StatusVisibilityEnum.Unlisted,
                Visibility.Private => StatusVisibilityEnum.Private,
                Visibility.Direct => StatusVisibilityEnum.Direct,
                _ => StatusVisibilityEnum.Public,
            };
        }
    }
    extension(StatusVisibilityEnum serverVisibility) {
        public Visibility Client {
            get => serverVisibility switch {
                StatusVisibilityEnum.Unlisted => Visibility.Unlisted,
                StatusVisibilityEnum.Private => Visibility.Private,
                StatusVisibilityEnum.Direct => Visibility.Direct,
                _ => Visibility.Public,
            };
        }
    }
}
