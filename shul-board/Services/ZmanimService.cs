using shul_board.Models;
using shul_board.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zmanim;
using Zmanim.JewishCalendar;
using Zmanim.TimeZone;
using Zmanim.TzDatebase;
using Zmanim.Utilities;

namespace shul_board.Services
{
    public class ZmanimService
    {
        private LocationSettings locationSettings;

        public ZmanimService(LocationSettings locationSettings)
        {
            this.locationSettings = locationSettings;
        }

        public ZmanimResults GetZmanim(DateTime date)
        {
            string locationName = locationSettings.LocationName;
            double latitude = locationSettings.Latitude;
            double longitude = locationSettings.Longitude;
            double elevation = 0; //optional elevation
            ITimeZone timeZone = new OlsonTimeZone(locationSettings.TimeZone);
            GeoLocation location = new GeoLocation(locationName, latitude, longitude, elevation, timeZone);
            ComplexZmanimCalendar zc = new ComplexZmanimCalendar(date, location);
            ZmanimResults results = new ZmanimResults()
            {
                Alos = zc.GetAlos16Point1Degrees(),
                Neitz = zc.GetSunrise(),
                SofZmanShmaMGA = zc.GetSofZmanShmaMGA16Point1Degrees(),
                SofZmanShmaGRA = zc.GetSofZmanShmaGRA(),
                SofZmanTefilaMGA = zc.GetSofZmanTfilaMGA(),
                SofZmanTefilaGRA = zc.GetSofZmanTfilaGRA(),
                Chatzos = zc.GetChatzos(),
                Shkia = zc.GetSunset(),
                Tzeis60 = zc.GetTzais60(),
                Tzeis72 = zc.GetTzais72(),
                CandleLighting = zc.GetCandleLighting()
            };

            return results;
        }

        public HebrewCalendarResults GetCalendar(DateTime dateTime)
        {
            var results = new HebrewCalendarResults();

            JewishCalendar cal = new JewishCalendar();
            HebrewDateFormatter formatter = new HebrewDateFormatter() { HebrewFormat = true };

            results.EnglishDate = dateTime;
            results.HebrewDate = $"{formatter.FormatHebrewNumber(cal.GetDayOfMonth(dateTime))} {formatter.FormatMonth(dateTime)} {formatter.FormatHebrewNumber(cal.GetYear(dateTime))}";
            results.Parsha = $"{(cal.GetJewishDayOfWeek(dateTime)<7?formatter.FormatHebrewNumber(cal.GetJewishDayOfWeek(dateTime)): "שבת")} {formatter.FormatParsha(dateTime.AddDays(7 - cal.GetJewishDayOfWeek(dateTime)))}";
            results.Daf = formatter.FormatDafYomiBavli(cal.GetDafYomiBavli(dateTime));
            results.YomTov = formatter.FormatYomTov(dateTime, false);

            return results;
        }
    }
}
