using CovidCorpus.Interfaces;
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
	public partial class HomePage : ContentPage
	{
        double BoxViewShadowHeight = App.ScreenHeight * 0.8;
        static bool temp = true;
        static bool isSelfAssessmentCompleted;

        public HomePage (bool isSelfAssessmentDone = false)
		{
			InitializeComponent ();
            navigationBar.OnLeftButtonTapped += HambergurMenuClicked;
            frameShadow.HeightRequest = BoxViewShadowHeight;
            if(isSelfAssessmentDone || isSelfAssessmentCompleted )
            {
                lblStatus.Text = "High Risk   ";
                frameStatus.BackgroundColor = Color.FromHex("#FF6B82");
                imageStatus.Source = "ic_delete";
                isSelfAssessmentCompleted = true; ;

            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            //Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            //{
            //    Task.Delay(1000);
            //    activityIndicator.IsVisible = false;
            //    CheckIfThereIsAnyCovidPositiveCase();
            //    return false;
            //});

        }
        async void HambergurMenuClicked(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            if (btn.ClassId == "0")
            {
                double translationX = App.ScreenWidth - 0.3 * App.ScreenWidth;
                contentViewCovidUpdates.CornerRadius = 30;
                contentViewCovidUpdates.ScaleTo(0.95);
                await gridParent.TranslateTo(translationX, 0);
                btn.ClassId = "1";
            }
            else
            {

                contentViewCovidUpdates.ScaleTo(1);
                contentViewCovidUpdates.CornerRadius = 0;
                await gridParent.TranslateTo(0, 0);
                btn.ClassId = "0";
            }

        }

        private void CheckIfThereIsAnyCovidPositiveCase()
        {
            if(temp)
            {
                temp = false;
                DependencyService.Get<IAdvertiseAndDiscoverBluetoothDevice>().Advertise();
                DependencyService.Get<IAdvertiseAndDiscoverBluetoothDevice>().Discover();
            }
           
        }
        protected override bool OnBackButtonPressed()
        {
            return true;

        }
    }
}