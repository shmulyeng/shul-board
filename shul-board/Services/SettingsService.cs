using shul_board.Data;
using shul_board.Data.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace shul_board.Services
{
    public class SettingsService: BaseService<Setting>
    {
        private Dictionary<string, Setting> cache = new Dictionary<string, Setting>();

        public SettingsService(ShulBoardContext context): base(context)
        {
        }

        public string GetSetting (string key)
        {
            if (!cache.ContainsKey(key))
            {
                var setting = this._context.Settings.Where(s => s.Key == key).FirstOrDefault();
                if (setting != null)
                {
                    cache.Add(key, setting);
                }
                else
                {
                    return null;
                }
            }
            return cache[key].Value;
        }
    }
}
