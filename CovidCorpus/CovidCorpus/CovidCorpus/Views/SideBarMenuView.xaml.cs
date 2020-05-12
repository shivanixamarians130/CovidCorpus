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
	public partial class SideBarMenuView : ContentView
	{
        public List<MenuItemModel> MenuItemsList;

        public SideBarMenuView ()
		{
			InitializeComponent ();
            MenuItemsList = new List<MenuItemModel>
            {
                new MenuItemModel { MenuImage = "ic_home",MenuTitle="Home"},
                new MenuItemModel { MenuImage = "ic_self_assessment",MenuTitle="Self Assessment"},
                new MenuItemModel { MenuImage = "ic_covid_updates",MenuTitle="Covid Updates"},
                new MenuItemModel { MenuImage = "ic_settings",MenuTitle="Settings"},
                new MenuItemModel { MenuImage = "ic_logout",MenuTitle="Logout"},
            };
            BindableLayout.SetItemsSource(listViewMenuItems, MenuItemsList);
        }

       async private void Button_Clicked(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            string cmd = (string)btn.CommandParameter;
            switch(cmd)
            {
                case "Home":
                    Application.Current.MainPage = new HomePage();
                  
                    break;
                case "Covid Updates":
                    Application.Current.MainPage = new CovidUpdatesPage();
                  
                    break;
                case "Self Assessment":
                    Application.Current.MainPage = new AssessmentPage();
                    break;
                case "Logout":
                    Application.Current.MainPage = new RegistrationPage();
                    break;
                default:
                    break;
            }
        }
    }
    public class MenuItemModel
    {
        public string MenuImage { get; set; }
        public string MenuTitle { get; set; }
    }
}