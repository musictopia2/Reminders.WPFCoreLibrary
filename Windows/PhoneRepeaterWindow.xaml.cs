namespace Reminders.WPFCoreLibrary.Windows;
/// <summary>
/// Interaction logic for PhoneRepeaterWindow.xaml
/// </summary>
public partial class PhoneRepeaterWindow : Window
{
    public PhoneRepeaterWindow(string title)
    {
        Resources.Add("services", StartUp.GetProvider());
        InitializeComponent();
        if (BasicPopUp.IsTesting == false)
        {
            Topmost = true;
            ResizeMode = ResizeMode.NoResize;
            WindowStyle = WindowStyle.None;
        }
        Title = title;
        throw new CustomBasicException("Phone repeater not supported anymore");
    }
}