using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CovidCorpus.CustomControls
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CustomNavigationBar : ContentView
	{
       
#region PublicProperties
public event EventHandler OnLeftButtonTapped;
public event EventHandler OnRightButtonTapped;
public DatePicker OnDatePickerTapped;
// public string selectedDateString = null;



public string LeftImage
{
    get
    {
        return (string)GetValue(LeftImageProperty);
    }
    set
    {
        SetValue(LeftImageProperty, value);
    }
}

public string RightImage
{
    get
    {
        return (string)GetValue(RightImageProperty);
    }
    set
    {
        SetValue(RightImageProperty, value);
    }
}
public string Title
{
    get
    {
        return (string)GetValue(TitleProperty);
    }
    set
    {
        SetValue(TitleProperty, value);
    }
}
public string RightText
{
    get
    {
        return (string)GetValue(RightTextProperty);
    }
    set
    {
        SetValue(RightTextProperty, value);
    }
}
        #endregion

        public CustomNavigationBar()
        {
            InitializeComponent();
            btnLeft.Clicked += Tgr_Left_Tapped;
            btnRight.Clicked += Tgr_Right_Tapped;
        }
        public static readonly BindableProperty TitleProperty = BindableProperty.Create(
                                         propertyName: "Title",
                                         returnType: typeof(string),
                                         declaringType: typeof(CustomNavigationBar),
                                         defaultValue: "",
                                         defaultBindingMode: BindingMode.TwoWay,
                                         propertyChanged: TitlePropertyChanged);


public static readonly BindableProperty RightTextProperty = BindableProperty.Create(
    propertyName: "RightTest",
                                                returnType: typeof(string),
                                                declaringType: typeof(CustomNavigationBar),
                                                defaultValue: "",
                                                defaultBindingMode: BindingMode.TwoWay,
                                                propertyChanged: RightTextPropertyChanged);


public static readonly BindableProperty LeftImageProperty = BindableProperty.Create(
                                                 propertyName: "LeftImage",
                                                 returnType: typeof(string),
                                                 declaringType: typeof(CustomNavigationBar),
                                                 defaultValue: "",
                                                 defaultBindingMode: BindingMode.TwoWay,
    propertyChanged: LeftImagePropertyChanged);


public static readonly BindableProperty RightImageProperty = BindableProperty.Create(
                                               propertyName: "RightImage",
                                               returnType: typeof(string),
                                               declaringType: typeof(CustomNavigationBar),
                                               defaultValue: "",
                                               defaultBindingMode: BindingMode.TwoWay,
                                               propertyChanged: RightImagePropertyChanged);

#region EventHandlers


private static void RightImagePropertyChanged(BindableObject bindable, object oldValue, object newValue)
{
    var control = (CustomNavigationBar)bindable;
    control.imgRight.Source = control.RightImage;
}



private static void LeftImagePropertyChanged(BindableObject bindable, object oldValue, object newValue)
{
    var control = (CustomNavigationBar)bindable;
    control.imageLeft.Source = control.LeftImage;
}
private static void TitlePropertyChanged(BindableObject bindable, object oldValue, object newValue)
{
    var control = (CustomNavigationBar)bindable;
    control.lblTitle.Text = newValue.ToString();
}
private static void RightTextPropertyChanged(BindableObject bindable, object oldValue, object newValue)
{
    var control = (CustomNavigationBar)bindable;

    // control.lblTitle.Text = newValue.ToString();
    control.lblRight.Text = newValue.ToString();
}
private void Tgr_Left_Tapped(object sender, EventArgs e)
{
    OnLeftButtonTapped?.Invoke(sender, e);
}
private void Tgr_Right_Tapped(object sender, EventArgs e)
{
    try
    {
        OnRightButtonTapped?.Invoke(sender, e);
        //if (GlobalVariables.FromRequiredCalenderPage)
        //{

        //    calenderRight.IsEnabled = true;
        //    calenderRight.IsVisible = true;
        //}
    }
    catch (Exception exe)
    {

    }

}

        #endregion
    }
}