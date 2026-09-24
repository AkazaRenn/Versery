// Ported from Tusky's TimestampUtils.kt, licensed under GPLv3:
// https://codeberg.org/tusky/Tusky/src/commit/7fef19efc66f9462e01435396fe10ffbb228ccda/app/src/main/java/com/keylesspalace/tusky/util/TimestampUtils.kt
// https://codeberg.org/tusky/Tusky/src/commit/7fef19efc66f9462e01435396fe10ffbb228ccda/LICENSE.txt
using Microsoft.Windows.ApplicationModel.Resources;

namespace View.Strings;

internal static class DateTime {
    static readonly Type type = typeof(DateTime);
    static readonly ResourceLoader resourceLoader = new("resources.pri", $"{type.Namespace!.Split('.')[0]}/{type.Name}");

    const uint DaysPerYear = 365;

    extension(System.DateTime dateTime) {
        internal string ToRelativeStringShort() {
            TimeSpan span = System.DateTime.UtcNow - dateTime.ToUniversalTime();
            if (span.Duration() < TimeSpan.FromSeconds(1)) {
                return resourceLoader.GetString("Now");
            }

            bool future = span < TimeSpan.Zero;
            span = span.Duration();

            string key;
            uint value;
            if (span.TotalMinutes < 1) {
                value = (uint)span.TotalSeconds;
                key = future ? "InSeconds" : "SecondsAgo";
            } else if (span.TotalHours < 1) {
                value = (uint)span.TotalMinutes;
                key = future ? "InMinutes" : "MinutesAgo";
            } else if (span.TotalDays < 1) {
                value = (uint)span.TotalHours;
                key = future ? "InHours" : "HoursAgo";
            } else if (span.TotalDays < DaysPerYear) {
                value = (uint)span.TotalDays;
                key = future ? "InDays" : "DaysAgo";
            } else {
                value = (uint)(span.TotalDays / DaysPerYear);
                key = future ? "InYears" : "YearsAgo";
            }
            return string.Format(resourceLoader.GetString(key), value);
        }
    }

    extension(TimeSpan timeSpan) {
        internal string ToStringShort() {
            TimeSpan span = timeSpan < TimeSpan.Zero ? TimeSpan.Zero : timeSpan;

            string key;
            uint value;
            if (span.TotalMinutes < 1) {
                value = (uint)span.TotalSeconds;
                key = value == 1 ? "DurationSecondOne" : "DurationSecondOther";
            } else if (span.TotalHours < 1) {
                value = (uint)span.TotalMinutes;
                key = value == 1 ? "DurationMinuteOne" : "DurationMinuteOther";
            } else if (span.TotalDays < 1) {
                value = (uint)span.TotalHours;
                key = value == 1 ? "DurationHourOne" : "DurationHourOther";
            } else {
                value = (uint)span.TotalDays;
                key = value == 1 ? "DurationDayOne" : "DurationDayOther";
            }
            return string.Format(resourceLoader.GetString(key), value);
        }
    }
}
