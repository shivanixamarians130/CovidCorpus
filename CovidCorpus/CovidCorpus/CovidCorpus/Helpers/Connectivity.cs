using System;
using System.Net;
using System.Threading.Tasks;

namespace CovidCorpus.Helpers
{
    public static class Connectivity
    {
        public static Task<bool> IsInternetAvailable()
        {
            try
            {
                using (var client = new MyWebClient())
                using (client.OpenRead("http://clients3.google.com/generate_204"))
                {

                    return Task.FromResult(true);
                }
            }
            catch
            {
                return Task.FromResult(false);
            }

        }
    }

    public class MyWebClient : WebClient
    {
        protected override WebRequest GetWebRequest(Uri uri)
        {
            WebRequest w = base.GetWebRequest(uri);
            w.Timeout = 4000;
            return w;
        }
    }
}
