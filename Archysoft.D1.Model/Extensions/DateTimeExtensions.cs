using System;

namespace Archysoft.D1.Model.Extensions
{
    public static class DateTimeExtensions
    {
        private static readonly DateTime Epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public static long ConvertToTimestamp(this DateTime date)
        {
            var timespan = date - Epoch;
            return (long)timespan.TotalSeconds;
        }

        public static DateTime ConvertToDateTime(this long timestamp)
        {
            return Epoch.AddSeconds(timestamp);
        }
    }
}
