namespace MailingApp.Utilities
{
    public static class DateFormatUtil
    {
        public static DateTime GetCurrentDate()
        {
            DateTime utcNow = DateTime.UtcNow;
            TimeZoneInfo jakartaTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
            DateTime currDate = TimeZoneInfo.ConvertTimeFromUtc(utcNow, jakartaTimeZone);
            return DateTime.SpecifyKind(currDate, DateTimeKind.Utc);
        }
    }
}