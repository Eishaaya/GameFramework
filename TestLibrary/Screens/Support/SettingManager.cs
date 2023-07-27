using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BaseGameLibrary.Screens.Support
{
    internal class SettingManager
    {
        //public Dictionary<Setting, HashSet<Screen>> ScreenSettings { get; } = new Dictionary<Setting, HashSet<Screen>>();

        //public bool BindsChanged { get; set; }

        //public void GiveSettings(HashSet<Setting> settings, Screen screen)
        //{
        //    foreach (var setting in settings)
        //    {
        //        if (ScreenSettings.ContainsKey(setting))
        //        {
        //            ScreenSettings[setting].Add(screen);
        //        }
        //        else
        //        {
        //            ScreenSettings.Add(setting, new HashSet<Screen>() { screen });
        //        }

        //        screen.AddSetting(setting);
        //    }
        //}

        //public void ChangeSetting(Setting changedSetting)
        //{
        //    var changingScreens = ScreenSettings[changedSetting];
        //    foreach (var screen in changingScreens)
        //    {
        //        screen.ChangeSetting(changedSetting);
        //    }
        //}
    }
}
