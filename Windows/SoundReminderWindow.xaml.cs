namespace Reminders.WPFCoreLibrary.Windows;
/// <summary>
/// Interaction logic for SoundReminderWindow.xaml
/// </summary>
public partial class SoundReminderWindow : Window
{
    public SoundReminderWindow(string title)
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
    }
}