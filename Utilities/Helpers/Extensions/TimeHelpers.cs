using System;

namespace XControls.Utilities.Helpers.Extensions
{
    public static class TimeHelpers
    {
        public static bool CompareTime(this double previousWakeTime, double periodicity) =>
            previousWakeTime + periodicity <= DateTime.UtcNow.TimeOfDay.TotalMilliseconds;
        public static bool CompareTime(this long previousWakeTime, long periodicity) => previousWakeTime + periodicity <= DateTime.Now.ToFileTime();
        public static bool CompareTime(this DateTime previousWakeTime, double periodicity) =>
            previousWakeTime.Ticks / TimeSpan.TicksPerMillisecond + periodicity <= DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
        public static double GetDiffernce(this DateTime old) => Math.Max(DateTime.Now.TimeOfDay.TotalMilliseconds - old.TimeOfDay.TotalMilliseconds, 0);
        public static TimeSpan GetRemainingTime(this double totalTime, double elapsedTime) =>
            new((long)((totalTime - elapsedTime) * TimeSpan.TicksPerMillisecond));
    }
}
