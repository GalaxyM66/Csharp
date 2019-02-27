using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace PriceManager
{
    class AppConfigUtils
    {
        public static void setConfig(string key, string value)
        {
            //string file = System.Windows.Forms.Application.ExecutablePath;
            Configuration config =
                ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                //ConfigurationManager.OpenExeConfiguration("B2BTools.config");
            config.AppSettings.Settings.Remove(key);
            config.AppSettings.Settings.Add(key, value);
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }


        public static string getConfig(string key)
        {
            
            foreach (string strKey in ConfigurationManager.AppSettings)
            {
                if (key == strKey)
                {
                    return ConfigurationManager.AppSettings[strKey];
                }
            }
            return null;
        }
    }
}
