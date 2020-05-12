using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace CovidCorpus.ViewModel
{
    class InformationSetupViewModel : INotifyPropertyChanged
    {
        public InformationSetupViewModel()
        {
            Steps = new List<KeyValuePair<string, bool>>
            {
                new KeyValuePair<string, bool>("step1",true)
            };
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public List<KeyValuePair<string, bool>> Steps { get; set; }


    }
}
