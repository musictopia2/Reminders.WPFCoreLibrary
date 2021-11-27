namespace Reminders.WPFCoreLibrary.PopUps;
public class LoadSoundsHelperClass
{
    private SoundRepeater? _rs;
    public LoadSoundsHelperClass()
    {

    }
    public void PlaySound(int howOftenToRepeat)
    {
        _rs = new()
        {
            HowOftenToPlay = new TimeSpan(0, 0, howOftenToRepeat)
        };
        _rs.PlaySound();
    }
    public void StopPlay()
    {
        if (_rs is null)
        {
            return;
        }
        _rs.StopPlay();
        _rs = null;
    }
}