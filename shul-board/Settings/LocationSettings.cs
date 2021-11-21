using shul_board.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace shul_board.Settings
{
    public class LocationSettings
    {
        private SettingsService settings;

        public LocationSettings(SettingsService settings)
        {
            this.settings = settings;
        }


        public double Latitude { get => Double.Parse(settings.GetSetting("location.latitude")); }
        public double Longitude { get => Double.Parse(settings.GetSetting("location.longitude")); }
        public string Zip { get => settings.GetSetting("location.zip"); }
        public string LocationName { get => settings.GetSetting("location.name"); }
        public string TimeZone { get => settings.GetSetting("location.timezone"); }
    }
}
