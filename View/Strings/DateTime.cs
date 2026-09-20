// Ported from Tusky's TimestampUtils.kt, licensed under GPLv3:
// https://codeberg.org/tusky/Tusky/src/commit/7fef19efc66f9462e01435396fe10ffbb228ccda/app/src/main/java/com/keylesspalace/tusky/util/TimestampUtils.kt
// https://codeberg.org/tusky/Tusky/src/commit/7fef19efc66f9462e01435396fe10ffbb228ccda/LICENSE.txt
using Microsoft.Windows.ApplicationModel.Resources;

namespace View.Strings;

internal static class DateTime {
    static readonly Type type = typeof(DateTime);
    static readonly ResourceLoader resourceLoader = new("resources.pri", $"{type.Namespace!.Split('.')[0]}/{type.Name}");

    const long SecondInMillis = 1000;
    const long MinuteInMillis = SecondInMillis * 60;
    const long HourInMillis = MinuteInMillis * 60;
    const long DayInMillis = HourInMillis * 24;
    const long YearInMillis = DayInMillis * 365;

    extension(System.DateTime dateTime) {
        internal string ToRelativeStringShort() {
            long span = (long)(System.DateTime.UtcNow - dateTime.ToUniversalTime()).TotalMilliseconds;
            bool future = false;
            if (Math.Abs(span) < SecondInMillis) {
                return resourceLoader.GetString("Now");
            } else if (span < 0) {
                future = true;
                span = -span;
            }

            string key;
            if (span < MinuteInMillis) {
                span /= SecondInMillis;
                key = future ? "InSeconds" : "SecondsAgo";
            } else if (span < HourInMillis) {
                span /= MinuteInMillis;
                key = future ? "InMinutes" : "MinutesAgo";
            } else if (span < DayInMillis) {
                span /= HourInMillis;
                key = future ? "InHours" : "HoursAgo";
            } else if (span < YearInMillis) {
                span /= DayInMillis;
                key = future ? "InDays" : "DaysAgo";
            } else {
                span /= YearInMillis;
                key = future ? "InYears" : "YearsAgo";
            }
            return string.Format(resourceLoader.GetString(key), span);
        }
    }

    extension(TimeSpan timeSpan) {
        internal string ToStringShort() {
            long span = Math.Max((long)timeSpan.TotalMilliseconds, 0);

            string oneKey, otherKey;
            if (span < MinuteInMillis) {
                span /= SecondInMillis;
                oneKey = "DurationSecondOne";
                otherKey = "DurationSecondOther";
            } else if (span < HourInMillis) {
                span /= MinuteInMillis;
                oneKey = "DurationMinuteOne";
                otherKey = "DurationMinuteOther";
            } else if (span < DayInMillis) {
                span /= HourInMillis;
                oneKey = "DurationHourOne";
                otherKey = "DurationHourOther";
            } else {
                span /= DayInMillis;
                oneKey = "DurationDayOne";
                otherKey = "DurationDayOther";
            }
            return string.Format(resourceLoader.GetString(span == 1 ? oneKey : otherKey), span);
        }
    }
}
