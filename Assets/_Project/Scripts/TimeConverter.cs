using System;

public class TimeConverter  
{
    public static ClockTime ConvertToClockTime(DateTime dateTime )
    {
        ClockTime clockTime = new ClockTime();
        clockTime.hours = (float)dateTime.Hour * (float)60 * (float)60 + (float)dateTime.Second + (float)dateTime.Minute * (float)60;
        clockTime.minutes = (float)dateTime.Minute;
        clockTime.seconds = (float)dateTime.Second;

        return clockTime;
    } 
    public static DateTime ConvertUnixToDateTime(long timestamp)
    {
        DateTime dateTime = (new DateTime(1970, 1, 1, 0, 0, 0, 0)).AddSeconds(timestamp / 1000).ToLocalTime();
        return dateTime;
    }
    public static string ConvertTimeToString(DateTime dateTime)
    {
        return $"{dateTime.Hour:00}:{dateTime.Minute:00}:{dateTime.Second:00}";
    }
}
