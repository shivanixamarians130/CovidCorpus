using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CovidCorpus.Constants;
using CovidCorpus.Services;

namespace CovidCorpus.Managers
{
    class RegistrationManager
    {
        public async Task<IRestResult<string>> GetOtp(string phoneNumber, CancellationTokenSource cancellationTokenSource = default(CancellationTokenSource))
        {
                Dictionary<string, string> param = new Dictionary<string, string>();
                   param.Add("PhoneNumber", phoneNumber);
            return await RestAPI.PostAsync<string>(APIConstants.GetOTPUrl, param, cancellationTokenSource);
        }
}
}
