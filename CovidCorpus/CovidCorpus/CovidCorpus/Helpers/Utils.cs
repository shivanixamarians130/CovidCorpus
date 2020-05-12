using System;
using System.Threading.Tasks;

namespace CovidCorpus.Helpers
{
    public static class Utils
    {
        static bool clicked;

        public static bool IsConcurrentClick(int ms = 1500)
        {
            if (clicked)
                return true;
            clicked = true;
            Task.Delay(ms).ContinueWith(x => clicked = false);
            return false;
        }
    }
}
