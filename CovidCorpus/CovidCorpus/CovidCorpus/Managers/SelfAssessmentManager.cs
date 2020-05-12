using CovidCorpus.Constants;
using CovidCorpus.Services;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CovidCorpus.Managers
{
    class SelfAssessmentManager
    {
        public async Task<IRestResult<string>> GetOtp(string gender,string age, string symptom,List<string> disease,string interaction, CancellationTokenSource cancellationTokenSource = default(CancellationTokenSource))
        {
            Dictionary<string, string> param = new Dictionary<string, string>();
            return await RestAPI.PostAsync<string>(APIConstants.SubmitAssessmentURL, param, cancellationTokenSource);
        }
    }
}
