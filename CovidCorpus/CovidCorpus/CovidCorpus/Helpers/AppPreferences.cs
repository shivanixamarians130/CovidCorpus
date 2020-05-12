using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace CovidCorpus.Helpers
{
    class AppPreferences
    {
        public static bool GetValue(string key)
        {
           return Preferences.Get(key, false);
        }
        public static void SetValue(string key, string value)
        {
            Preferences.Set(key, value);
        }
    }
}
