using System;
using System.Collections.Generic;
using System.Text;

namespace CovidCorpus.Models.ResponseModels
{
    public class CovidUpdatesModel
    {
        public string Location { get; set; }
        public string Infections { get; set; }
        public string Death { get; set; }
        public string Recovered { get; set; }
    }
}
