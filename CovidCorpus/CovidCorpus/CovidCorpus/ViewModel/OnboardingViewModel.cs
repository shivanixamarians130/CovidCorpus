using CovidCorpus.Constants;
using CovidCorpus.Helpers;
using CovidCorpus.Models;
using CovidCorpus.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace CovidCorpus.ViewModel
{
    class OnboardingViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private ObservableCollection<View> _myItemsSource;
        private List<OnBoardModel> dataList;
        private OnBoardModel _currentData;
        private string _proceedText = "Next";
        private int _carouselPosition;

        public Command MyCommand
        {
            get
            {
                return new Command(SkipAction);
            }
        }
        public Command SkipCommand
        {
            get
            {
                return new Command(SkipAction);
            }
        }

        public Command ProceedCommand
        {
            get
            {
                return new Command(ProceedAction);
            }
        }

        public Command PositionChangeCommand
        {
            get
            {
                return new Command(PositionChangeAction);
            }
        }

        private void PositionChangeAction(object obj)
        {
        }

        public ObservableCollection<View> MyItemsSource
        {
            set
            {
                _myItemsSource = value;
                OnPropertyChanged("MyItemsSource");
            }
            get
            {
                return _myItemsSource;
            }
        }

        public string ProceedText
        {
            set
            {
                _proceedText = value;
                OnPropertyChanged(nameof(ProceedText));
            }
            get
            {
                return _proceedText;
            }
        }

        public OnBoardModel CurrentData
        {
            set
            {
                _currentData = value;
                OnPropertyChanged(nameof(CurrentData));
            }
            get
            {
                return _currentData;
            }
        }
        public int CarouselPosition
        {
            set
            {
                _carouselPosition = value;
                if (value == dataList.Count-1)
                {
                    ProceedText = "Register Now";
                }
                else
                {
                    ProceedText = "Next";
                }
                OnPropertyChanged(nameof(CarouselPosition));
            }
            get
            {
                return _carouselPosition;
            }
        }
        public OnboardingViewModel()
        {
            SetPageContent();
        }
        public bool CallBackPress()
        {
            if (CarouselPosition != 0)
            {
                CarouselPosition = --CarouselPosition;
                CurrentData = dataList[CarouselPosition];
                return false;
            }

            return true;
        }

        private void SetPageContent()
        {
            dataList = new List<OnBoardModel>
            {
                new OnBoardModel
                {
                    Title = "The power to help prevent",
                    Description = "Would you like to be informed if you have crossed paths with someone who has tested COVID-19 positive or has the symtoms",
                    Image= new Image() { Source = "OnboardingImage3.png",  Aspect = Aspect.AspectFit },
                },
                new OnBoardModel
                {
                    Title = "Steps to follow",
                    Description = "1. Switch on the bluetooth & location"+System.Environment.NewLine+"2. Set Location sharing to 'Always'",
                    Image= new Image() { Source = "OnboardingImage1.png",  Aspect = Aspect.AspectFit },
                },
                new OnBoardModel
                {
                    Title = "With Covid Corpus",
                    Description = "With COVID CORPUS, you can protect yourself, your family and friends, and help country in the efforts to fight COVID-19.",
                    Image= new Image() { Source = "OnboardingImage2.png",  Aspect = Aspect.AspectFit },
                },
                new OnBoardModel
                {
                    Title = "Alert Features",
                    Description = "The App alerts are accomplanied by instructions on how to self-isolate and what to do in case you develop Symptoms that may need help and support",
                    Image= new Image() { Source = "OnboardingImage4.png",  Aspect = Aspect.AspectFit },
                },
            };
            CurrentData = dataList.FirstOrDefault();
            MyItemsSource = new ObservableCollection<View>(dataList.Select(s => s.Image).ToList());

        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void SkipAction(object obj)
        {
        }
         private void ProceedAction(object obj)
        {
            if (ProceedText != "Register Now")
            {
                CarouselPosition = ++CarouselPosition;
                CurrentData = dataList[CarouselPosition];
            }
            else
            {
                if(AppPreferences.GetValue(AppConstants.IsAppLoggedInKey))
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
}
