using CovidCorpus.Constants;
using CovidCorpus.Managers;
using CovidCorpus.Models.ResponseModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CovidCorpus.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CovidUpdatesPage : ContentPage
    {
        double BoxViewShadowHeight = App.ScreenHeight * 0.8;
        List<CovidUpdatesModel> CovidUpdatesList;
        public CovidUpdatesPage()
        {
            InitializeComponent();
            navigationBar.OnLeftButtonTapped += HambergurMenuClicked;
            frameShadow.HeightRequest = BoxViewShadowHeight;

            CovidUpdatesList = new List<CovidUpdatesModel>
            {
                new CovidUpdatesModel{Location = "Northland",Infections="454",Death="41",Recovered="45"},
                new CovidUpdatesModel{Location = "Auckland",Infections="8984",Death="12",Recovered="74"},
                new CovidUpdatesModel{Location = "Waikato",Infections="784",Death="89",Recovered="47"},
                new CovidUpdatesModel{Location = "Bay of Plenty",Infections="2154",Death="63",Recovered="58"},
                new CovidUpdatesModel{Location = "Hawke's Bay",Infections="4571",Death="455",Recovered="58"},
                new CovidUpdatesModel{Location = "Nelson",Infections="97",Death="13",Recovered="25"},
                new CovidUpdatesModel{Location = "Fiordland",Infections="26",Death="21",Recovered="36"},
                new CovidUpdatesModel{Location = "Southland",Infections="58",Death="32",Recovered="21"},
                new CovidUpdatesModel{Location = "Waitaki",Infections="43",Death="21",Recovered="43"},
                new CovidUpdatesModel{Location = "Dunedin",Infections="21",Death="10",Recovered="23"},
                new CovidUpdatesModel{Location = "Clutha",Infections="45",Death="21",Recovered="45"},
            };
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            //GetCovidUpdatesAsync();
            // listViewUpdates.ItemsSource = CovidUpdatesList;
            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                Task.Delay(1000);
                activityIndicator.IsVisible = false;
                lblTime.Text = DateTime.Now.ToString("ddd, dd MMM yyy hh:mm tt");
                BindableLayout.SetItemsSource(listViewUpdates, CovidUpdatesList);
                return false;
            });
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

        async Task GetCovidUpdatesAsync()
        {
            CovidUpdatesManager manage = new CovidUpdatesManager();
            var response = await manage.GetCovidUpdates(false);
            if (!response.IsSuccess)
            {
                IsBusy = false;
                await Application.Current.MainPage.DisplayAlert(AppConstants.AlertHeading, AppConstants.SomethingWentWrong, AppConstants.AletOk);
                return;
            }

            List<CovidUpdatesModel> CovidUpdatesList = response.Data;
        }
        
        private void Button_Clicked(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            string cmd = (string)btn.CommandParameter;
            if(cmd == "NZ")
            {
                btnNewZealand.BackgroundColor = (Color)Application.Current.Resources["ColorYellow"];
                btnGlobal.BackgroundColor = Color.White;
            }
            else
            {
                btnNewZealand.BackgroundColor = Color.White; ;
                btnGlobal.BackgroundColor = (Color) Application.Current.Resources["ColorYellow"];
            }
        }
        protected override bool OnBackButtonPressed()
        {
            return true;

        }
    }
}