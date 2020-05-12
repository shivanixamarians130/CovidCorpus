using CovidCorpus.Constants;
using CovidCorpus.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CovidCorpus.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RegistrationPage : ContentPage
	{
        string responseOtp = "1234";
        string TimerText = "";

        public RegistrationPage ()
		{
			InitializeComponent ();
            navBar.OnLeftButtonTapped += Back_Button_Clicked;
            //lblOtpDigit1.Text = "1";
            //lblOtpDigit2.Text = "2";
            //lblOtpDigit3.Text = "3";
            //lblOtpDigit4.Text = "4";
        }

        private void Back_Button_Clicked(object sender, EventArgs e)
        {
            if(contentViewEnterNumber.IsVisible)
            {
                Navigation.PopAsync();
            }
            else
            {
                contentViewEnterNumber.IsVisible = true;
                contentViewEnterOtp.IsVisible = false;
            }
        }
        async private void Submit_Button_Clicked(object sender, EventArgs e)
        {
            contentViewEnterNumber.IsVisible = false;
            contentViewEnterOtp.IsVisible = true;
            UpperInvisiblEntry.Focus();

            int temp = 85;
            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                if (temp <= 0)
                    return false;

                TimeSpan maxSpan = TimeSpan.FromSeconds(temp--);
                Device.BeginInvokeOnMainThread(() =>
                {
                    spannableStringTimerText.Text = " Re-send in " + maxSpan.ToString();
                });
                return true;
            });
            //await RequestOTP();
        }

        private void VerifyNow_Button_Clicked(object sender, EventArgs e)
        {
                activityIndicator.IsVisible = true;


            if (lblOtpDigit1.Text + lblOtpDigit2.Text + lblOtpDigit3.Text + lblOtpDigit4.Text == responseOtp)
            {
                Task.Delay(1000);
                activityIndicator.IsVisible = false;

                Navigation.PushAsync(new HomePage(), true);
            }
            else
            {
                activityIndicator.IsVisible = false;

                lblOtpDigit1.Text = lblOtpDigit2.Text = lblOtpDigit3.Text = lblOtpDigit4.Text = UpperInvisiblEntry.Text = "";
                Application.Current.MainPage.DisplayAlert(AppConstants.AlertHeading, AppConstants.WongOtpMessage, AppConstants.AletOk);
                UpperInvisiblEntry.Focus();
            }
        }



        async Task RequestOTP()
        {
            var registerManager = new RegistrationManager();

            var response = await registerManager.GetOtp(entyEnterNumber.Text);

            if (!response.IsSuccess)
            {
                IsBusy = false;
                await Application.Current.MainPage.DisplayAlert(AppConstants.AlertHeading, AppConstants.SomethingWentWrong, AppConstants.AletOk);
                return;
            }

            responseOtp = (string)response.Data;

        }

        private void UpperInvisiblEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            Entry entry = (Entry)sender;
            string input = e.NewTextValue;
            if (!Regex.IsMatch(e.NewTextValue, "^[0-9]+$", RegexOptions.CultureInvariant))
            {
                entry.Text = Regex.Replace(e.NewTextValue, "[^0-9]", string.Empty);
            }

            lblOtpDigit1.BackgroundColor = Color.White;
            lblOtpDigit2.BackgroundColor = Color.White;
            lblOtpDigit3.BackgroundColor = Color.White;
            lblOtpDigit4.BackgroundColor = Color.White;

            switch (entry.Text.Length)
            {
                case 0:
                    lblOtpDigit1.Text = "";
                    lblOtpDigit2.Text = "";
                    lblOtpDigit3.Text = "";
                    lblOtpDigit4.Text = "";

                    lblOtpDigit1.BackgroundColor = Color.Transparent;

                    break;
                case 1:
                    lblOtpDigit1.Text = input.Substring(0, 1);
                    lblOtpDigit2.Text = "";
                    lblOtpDigit3.Text = "";
                    lblOtpDigit4.Text = "";

                    lblOtpDigit2.BackgroundColor = Color.Transparent;
                    break;
                case 2:
                    lblOtpDigit1.Text = input.Substring(0, 1);
                    lblOtpDigit2.Text = input.Substring(1, 1);
                    lblOtpDigit3.Text = "";
                    lblOtpDigit4.Text = "";
                    lblOtpDigit3.BackgroundColor = Color.Transparent;

                    break;
                case 3:
                    lblOtpDigit1.Text = input.Substring(0, 1);
                    lblOtpDigit2.Text = input.Substring(1, 1);
                    lblOtpDigit3.Text = input.Substring(2, 1);
                    lblOtpDigit4.Text = "";

                    lblOtpDigit4.BackgroundColor = Color.Transparent;

                    break;
                case 4:
                    lblOtpDigit1.Text = input.Substring(0, 1);
                    lblOtpDigit2.Text = input.Substring(1, 1);
                    lblOtpDigit3.Text = input.Substring(2, 1);
                    lblOtpDigit4.Text = input.Substring(3, 1);
                    break;
                default:
                    UpperInvisiblEntry.Text = e.OldTextValue;
                    break;
            }


        }

        private void EntyEnterNumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                var entry = (Entry)sender;
                if (!Regex.IsMatch(e.NewTextValue, "^[0-9]+$", RegexOptions.CultureInvariant))
                {
                    entry.Text = Regex.Replace(e.NewTextValue, "[^0-9]", string.Empty);
                }
                else if (entry.Text.Length > 10)
                {
                    entry.Text = entry.Text.Remove(10);
                }
            }
            catch(Exception exc)
            {

            }
           
        }
    }
}