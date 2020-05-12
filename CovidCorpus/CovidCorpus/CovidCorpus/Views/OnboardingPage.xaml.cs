using CovidCorpus.Constants;
using CovidCorpus.Helpers;
using CovidCorpus.ViewModel;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CovidCorpus.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OnboardingPage : ContentPage
    {
        private OnboardingViewModel vm;

        public OnboardingPage()
        {
            InitializeComponent();
            BindingContext = vm = new OnboardingViewModel();
        }

        protected override bool OnBackButtonPressed()
        {
            if (vm.CallBackPress())
            {
                return base.OnBackButtonPressed();
            }
            return true;
        }

        private void Skip_Button_Clicked(object sender, EventArgs e)
        {
            if (AppPreferences.GetValue(AppConstants.IsAppLoggedInKey))
            {
                Application.Current.MainPage.Navigation.PushAsync(new HomePage(), true);
            }
            else
            {
                Application.Current.MainPage.Navigation.PushAsync(new RegistrationPage(), true);
            }
        }
    }
}