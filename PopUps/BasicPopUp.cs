namespace Reminders.WPFCoreLibrary.PopUps;
public class BasicPopUp : IPopUp
{
    public static bool SupportsSnooze { get; set; } = true;
    public BasicPopUp(ReminderContainer container)
    {
        _container = container;
        _container.ClosePopup = CloseSoundPopup;
        _container.SupportsSnooze = SupportsSnooze; //this way i can have something that does not support snoozing like testing refilling water on desktop to fix bugs.
    }
    public bool SupportsSound { get; set; }
    public Func<Task>? ClosedAsync { get; set; }
    public Func<TimeSpan, Task>? SnoozedAsync { get; set; }
    private LoadSoundsHelperClass? _sounds;
    public static Func<Task>? CloseCustomToastAsync { get; set; }
    public static bool IsTesting { get; set; } = false;
    public static bool ForceNoSound { get; set; } = false;
    private HiddenReminderWindow? _hidden;
    private SoundReminderWindow? _soundWindow;
    private readonly ReminderContainer _container;
    public Task LoadAsync(string title, string message)
    {
        if (SupportsSound)
        {
            _soundWindow = new(title);
            _container.Message = message;
            _soundWindow.Show();
        }
        else
        {
            CloseCustomToastAsync = CloseToastAsync;
            _hidden = new(title, message);
            _hidden.Show();
        }
        return Task.CompletedTask;
    }
    public void PlaySound(int howOftenToRepeat)
    {
        if (ForceNoSound)
        {
            return; //because forced no sound means do not even try.
        }
        if (SupportsSound == false)
        {
            throw new CustomBasicException("Cannot play sounds because not even supported.");
        }
        _sounds = new();
        _sounds.PlaySound(howOftenToRepeat);
    }
    private void CloseSoundPopup()
    {
        if (ForceNoSound)
        {
            return; //because forced no sound means do not even try.
        }
        if (_sounds is not null)
        {
            _sounds.StopPlay();
            _sounds = null;
        }
        if (_soundWindow is not null)
        {
            _soundWindow.Close();
            _soundWindow = null;
        }
    }
    private async Task CloseToastAsync()
    {
        if (SupportsSound)
        {
            throw new CustomBasicException("Unable to close toast because it supports sounds.  Should not even have closed the toast");
        }
        if (_hidden is null)
        {
            throw new CustomBasicException("Never opened the toast.  Rethink");
        }
        _hidden.Close();
        _hidden = null;
        CloseCustomToastAsync = null;
        await FinishCloseAsync();
    }
    private async Task FinishCloseAsync()
    {
        if (ClosedAsync == null)
        {
            throw new CustomBasicException("Nobody is listening to the close event.  Rethink");
        }
        try
        {
            await ClosedAsync.Invoke();
        }
        catch (Exception ex)
        {

            throw new CustomBasicException(ex.Message); //this is the best i can do this time.  does mean i get no stack trace.
        }
    }
}