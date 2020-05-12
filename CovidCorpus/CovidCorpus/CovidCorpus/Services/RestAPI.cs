using CovidCorpus.Constants;
using CovidCorpus.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace CovidCorpus.Services
{
    internal class RestAPI
    {
        static Timer timer;
        static CancellationTokenSource cancellationTokenSource;

        private static HttpClient CreateHttpClient()
        {
            var client = new HttpClient();
            client.MaxResponseContentBufferSize = 25600000;
            client.Timeout = new TimeSpan(0, 0, 60);
            //if (!string.IsNullOrEmpty(AppSecurity.Token))
            //    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AppSecurity.Token);
            return client;
        }

        private static RestResult<T> OKResult<T>(string json)
        {
            try
            {
                var responseData = JsonConvert.DeserializeObject<T>(json);
                return new RestResult<T>
                {
                    Data = responseData,
                    StatusCode = 200
                };
            }
            catch (Exception ex)
            {
                return new RestResult<T>
                {
                    Message = "Parse Json Error: " + ex.Message,
                    StatusCode = 200
                };
            }
        }

        private static RestResult<T> ErrorResult<T>(string message, int statusCode)
        {
            if (statusCode == 403)
            {
                return new RestResult<T>
                {
                    Message = Constants.Messages.NotAuthorizedMessage,
                    StatusCode = statusCode
                };
            }
            return new RestResult<T>
            {
                Message = message,
                StatusCode = statusCode
            };
        }

        private static StringContent GetStringContent(object postData)
        {
            var requestJson = postData == null ? string.Empty : JsonConvert.SerializeObject(postData);
            return new StringContent(requestJson, null, AppConstants.ContentType);
        }

        private static async Task<IRestResult<T>> HandleResponse<T>(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                return OKResult<T>(responseString);
            }
            else
            {
                var responseString = await response.Content.ReadAsStringAsync();
                return ErrorResult<T>(responseString, (int)response.StatusCode);
            }
        }

        /// <summary>
        /// Send POST request with postdata to uri.
        /// </summary>
        /// <typeparam name="T">Response Data Type</typeparam>
        /// <param name="uri">Request End Point</param>
        /// <param name="postData">Data to be post on server.</param>
        public static async Task<IRestResult<T>> PostAsync<T>(string uri, object postData, CancellationTokenSource cancellationToken = default(CancellationTokenSource))
        {
            if (cancellationToken == null)
                cancellationTokenSource = new CancellationTokenSource();
            else
                cancellationTokenSource = cancellationToken;
            try
            {
                var content = GetStringContent(postData);
                using (var client = CreateHttpClient())
                {
                    var response = await client.PostAsync(APIConstants.BaseURL + uri, content, cancellationTokenSource.Token);
                    return await HandleResponse<T>(response);
                }
            }
            catch (Exception ex)
            {
                timer?.Change(Timeout.Infinite, Timeout.Infinite);
                return ErrorResult<T>(ex.Message, 500);
            }
        }

        /// <summary>
        /// Send GET Request with parameters as query string to uri.
        /// </summary>
        /// <typeparam name="T">Response Data Type</typeparam>
        /// <param name="uri">Request End Point</param>
        /// <param name="parameters">Parameters to send as query string</param>
        public static async Task<IRestResult<T>> GetAsync<T>(string uri, Dictionary<string, string> parameters, CancellationTokenSource cancellationToken = default(CancellationTokenSource))
        {
            try
            {
                //timer = new Timer(OnTimerCallback, null, 0, 4000);
                if (cancellationToken == null)
                    cancellationTokenSource = new CancellationTokenSource();
                else
                    cancellationTokenSource = cancellationToken;
                if (parameters != null)
                {
                    var qs = string.Join("&", parameters.Select(x => x.Key + "=" + x.Value));
                    uri = uri + (uri.IndexOf("?") == -1 ? "?" : "&") + qs;
                }
                using (var client = CreateHttpClient())
                {
                    var response = await client.GetAsync(APIConstants.BaseURL + uri, cancellationTokenSource.Token);
                   // var response = await client.GetAsync(APIConstants.BaseURL + uri, cancellationTokenSource.Token);
                    //timer?.Change(Timeout.Infinite, Timeout.Infinite);
                    return await HandleResponse<T>(response);
                }
            }
            catch (InvalidCastException ex)
            {
                cancellationTokenSource = new CancellationTokenSource();
                timer?.Change(Timeout.Infinite, Timeout.Infinite);
                return ErrorResult<T>(Constants.AppConstants.InvalidCastExceptionMessage, 0);
            }
            catch (Exception ex)
            {
                cancellationTokenSource = new CancellationTokenSource();
                timer?.Change(Timeout.Infinite, Timeout.Infinite);
                return ErrorResult<T>(ex.Message, 500);
            }
        }



        private static async void OnTimerCallback(object state)
        {
            if (!await Connectivity.IsInternetAvailable())
            {
                cancellationTokenSource.Cancel();
                if (cancellationTokenSource.IsCancellationRequested)
                    timer?.Change(Timeout.Infinite, Timeout.Infinite);
            }
        }
    }
}
