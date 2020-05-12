using CovidCorpus.Models;
using CovidCorpus.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace CovidCorpus
{
    public partial class App : Application
    {
        public static int ScreenHeight, ScreenWidth;
        public static List<UserInfoModel> UserInfoList;

        public App()
        {
            InitializeComponent();
            UserInfoList = new List<UserInfoModel>
            {
                new UserInfoModel{UserId="222",Status="Positive"},
                new UserInfoModel{UserId="333",Status="Negative"},
            };
            MainPage = new NavigationPage(new OnboardingPage());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
