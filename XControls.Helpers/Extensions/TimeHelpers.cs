using System;

namespace XControls.Helpers.Extensions
{
    public static class TimeHelpers
    {
        public static bool CompareTime(double previousWakeTime, double periodicity) => previousWakeTime + periodicity <= DateTime.UtcNow.TimeOfDay.TotalMilliseconds;
        public static bool CompareTime(long previousWakeTime, long periodicity) => previousWakeTime + periodicity <= DateTime.Now.ToFileTime();
        public static bool CompareTime(DateTime previousWakeTime, double periodicity) => previousWakeTime.Ticks / TimeSpan.TicksPerMillisecond + periodicity <= DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
        public static double GetDiffernce(DateTime old) => Math.Max(DateTime.Now.TimeOfDay.TotalMilliseconds - old.TimeOfDay.TotalMilliseconds, 0);
        public static TimeSpan GetRemainingTime(double totalTime, double elapsedTime) => new TimeSpan((long)((totalTime - elapsedTime) * TimeSpan.TicksPerMillisecond));
    }
}
