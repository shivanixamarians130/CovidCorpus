using CovidCorpus.Constants;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CovidCorpus.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AssessmentPage : ContentPage
    {
        int temp = 1;
        string gender;
        string age;
        List<string> symptoms = new List<string>();
        List<string> disease = new List<string>();
        string interaction;
        public AssessmentPage()
        {
            InitializeComponent();
            mainContentView.Content = contentview1.Content;
            navigationBar.OnRightButtonTapped += NavigationBar_OnRightButtonTapped;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
          
        }
        private void NavigationBar_OnRightButtonTapped(object sender, EventArgs e)
        {
            Application.Current.MainPage = new HomePage();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {

           if(temp == 1)
            {
                Application.Current.MainPage = new HomePage();
            }
            else
            {
                temp -= 1;
                frameDescription.IsVisible = true;
                nextButton.Text = "Next";

                double data = (temp * 2) / 10.0;
                progressTemp.Progress = data;
                switch (temp)
                {
                    case 1:
                        mainContentView.Content = contentview1.Content;
                        break;
                    case 2:
                        mainContentView.Content = contentView2.Content;

                        break;
                    case 3:
                        mainContentView.Content = contentview3.Content;

                        break;
                    case 4:
                        mainContentView.Content = contentview4.Content;

                        break;
                }
            }
          
        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {
            temp += 1;
            if(temp!=6)
            {
                frameDescription.IsVisible = true;
                nextButton.Text = "Next";
            }

            switch (temp)
            {
                case 2:
                    if(string.IsNullOrEmpty(gender))
                    {
                        --temp;
                        Application.Current.MainPage.DisplayAlert(Constants.Messages.AlertHeading, Constants.Messages.PleaseSelectTheGender, Constants.Messages.AlertOk);
                        break;
                    }
                    mainContentView.Content = contentView2.Content;
                    break;
                case 3:
                    if (string.IsNullOrEmpty(age))
                    {
                        --temp;
                        Application.Current.MainPage.DisplayAlert(Constants.Messages.AlertHeading, Constants.Messages.PleaseEnterYourAge, Constants.Messages.AlertOk);
                        break;
                    }
                    mainContentView.Content = contentview3.Content;
                    break;
                case 4:
                    if (symptoms.Count == 0)
                    {
                        --temp;
                        Application.Current.MainPage.DisplayAlert(Constants.Messages.AlertHeading, Constants.Messages.PleaseSelectTheSymptom, Constants.Messages.AlertOk);
                        break;
                    }
                    mainContentView.Content = contentview4.Content;
                    break;
                case 5:
                    if (disease.Count == 0)
                    {
                        --temp;
                        Application.Current.MainPage.DisplayAlert(Constants.Messages.AlertHeading, Constants.Messages.PleaseSelectTheDisease, Constants.Messages.AlertOk);
                        break;
                    }
                    frameDescription.IsVisible = false;
                    mainContentView.Content = contentView5.Content;
                    nextButton.Text = "Submit";
                    break;
                case 6:
                    if (string.IsNullOrEmpty(interaction))
                    {
                        --temp;
                        Application.Current.MainPage.DisplayAlert(Constants.Messages.AlertHeading, Constants.Messages.PleaseSelectAnyOfTheAboveOption, Constants.Messages.AlertOk);
                        break;
                    }
                    activityIndicator.IsVisible = true;
                    Task.Delay(1000);
                    activityIndicator.IsVisible = false;

                    Application.Current.MainPage = new HomePage(true);
                    break;
            }
            var data = (temp * 2) / 10.0;
            progressTemp.Progress = data;
        }



        void SubmitAssessmentTest()
        {

        }


        private void Gendertn_Clicked(object sender , EventArgs e)
        {
            var btn = (Button)sender;

            ResetTheUnselectedButtonColors(btnMaleContentView1);
            ResetTheUnselectedButtonColors(btnFemaleContentView1);
            ResetTheUnselectedButtonColors(btnTrnsGndrContentView1);
            ResetTheUnselectedButtonColors(btnPrfrNtToSayContentView1);

            ChangeSelectedOptionColor(btn);
            gender = btn.Text;
        }
        private void SymptomsBtn_Clicked(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            
            if ((string)btn.CommandParameter == "NoneOfTheAbove")
            {
                ResetTheUnselectedButtonColors(btnFeverContentView3);
                ResetTheUnselectedButtonColors(btnCoghContentView3);
                ResetTheUnselectedButtonColors(btnDifficultyInBreathingContentView3);
                ChangeSelectedOptionColor(btnNoneOfTheAboveContentView3);
                symptoms.Add(btn.Text);
                return;
            }
            ResetTheUnselectedButtonColors(btnNoneOfTheAboveContentView3);
            if (btn.BackgroundColor == Color.Black)
            {
                ResetTheUnselectedButtonColors(btn);
                symptoms.Remove(btn.Text);
            }
            else
            {
                ChangeSelectedOptionColor(btn);
                symptoms.Add(btn.Text);
            }
        }

        private void DiseaseBtn_Clicked(object sender,EventArgs e)
        {
            var btn = (Button)sender;
            if ((string)btn.CommandParameter == "NoneOfTheAbove")
            {
                ResetTheUnselectedButtonColors(btnDiabetesContentView4);
                ResetTheUnselectedButtonColors(btnHyperTntnContentView4);
                ResetTheUnselectedButtonColors(btnLungDiseaseContentView4);
                ResetTheUnselectedButtonColors(btnHrtDiseaseContentView4);
                ChangeSelectedOptionColor(btnNoneOfTheAboveContentView4);
                disease.Add(btn.Text);
                return;
            }
            ResetTheUnselectedButtonColors(btnNoneOfTheAboveContentView4);
            if (btn.BackgroundColor == Color.Black)
            {
                ResetTheUnselectedButtonColors(btn);
                disease.Remove(btn.Text);
            }
            else
            {
                ChangeSelectedOptionColor(btn);
                disease.Add(btn.Text);
            }
        }

        private void CheckInteractionBtn_Clicked(Object sender,EventArgs e)
        {
            var btn = (Button)sender;
            ResetTheUnselectedButtonColors(btnTravelledWithIn14DaysContentView5);
            ResetTheUnselectedButtonColors(btnRecentlyIntractedWithPositiveCasesContentView5);
            ResetTheUnselectedButtonColors(btnHealthCareWorkerContentView5);
            ResetTheUnselectedButtonColors(btnNoneOfTheAboveContentView5);


            ChangeSelectedOptionColor(btn);
            interaction = btn.Text;
        }

        private void ChangeSelectedOptionColor(Button btn)
        {
                btn.BackgroundColor = Color.Black;
                btn.TextColor = Color.White;
        }
        private void ResetTheUnselectedButtonColors(Button btn)
        {
            btn.BackgroundColor = Color.Transparent;
            btn.TextColor = Color.Black;
        }

        private void EntryAgeContentView2_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                var entry = (Entry)sender;
                if (!Regex.IsMatch(e.NewTextValue, "^[0-9]+$", RegexOptions.CultureInvariant))
                {
                    entry.Text = Regex.Replace(e.NewTextValue, "[^0-9]", string.Empty);
                }
                else if (entry.Text.Length > 3)
                {
                    entry.Text = entry.Text.Remove(10);
                }
                age = entry.Text;
            }
            catch (Exception exc)
            {

            }
        }
        protected override bool OnBackButtonPressed()
        {
            return true;

        }
    }
}