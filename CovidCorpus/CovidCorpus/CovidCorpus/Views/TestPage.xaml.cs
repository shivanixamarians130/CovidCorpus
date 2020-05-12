using CovidCorpus.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CovidCorpus.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TestPage : ContentPage
	{
		public TestPage ()
		{
			InitializeComponent ();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
        }
        public class CovidUpdatesModel
        {
            public string Location { get; set; }
            public string Infections { get; set; }
            public string Death { get; set; }
            public string Recovered { get; set; }
        }

    }
}