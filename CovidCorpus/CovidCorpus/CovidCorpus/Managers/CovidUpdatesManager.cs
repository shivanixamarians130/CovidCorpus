using CovidCorpus.Constants;
using CovidCorpus.Models.ResponseModels;
using CovidCorpus.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CovidCorpus.Managers
{
    public class CovidUpdatesManager
    {
        public async Task<IRestResult<List<CovidUpdatesModel>>> GetCovidUpdates(bool globalData, CancellationTokenSource cancellationTokenSource = default(CancellationTokenSource))
        {
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("globalData", globalData.ToString());
            return await RestAPI.GetAsync<List<CovidUpdatesModel>>(APIConstants.CovidUpdates, param, cancellationTokenSource);
        }
    }
}
