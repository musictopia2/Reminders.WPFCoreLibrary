namespace Reminders.WPFCoreLibrary.Windows;
public class HiddenReminderWindow : Window
{
    public static string ExtraText { get; set; } = "";
    public HiddenReminderWindow(string title, string message)
    {
        Title = title;
        Background = Brushes.Aqua;
        if (BasicPopUp.IsTesting == false)
        {
            Topmost = true;
            ResizeMode = ResizeMode.NoResize;
            WindowStyle = WindowStyle.None;
        }
        ShowInTaskbar = false;
        Left = 1500;
        Top = 960;
        Width = 400;
        Height = 100;
        TextBlock temps = new();
        temps.TextWrapping = TextWrapping.Wrap;
        temps.FontSize = 16;
        temps.Text = $"{message}  {ExtraText}  Time now is {DateTime.Now}";
        Content = temps;
        Show();
    }
}