namespace Reminders.WPFCoreLibrary.SoundRepeaterClasses;
public class SoundRepeater
{
    public enum EnumSoundType
    {
        Buzzer = 1, DoorBell, CensoredBeep, MissileBeep, ElevatorBellDing, TypewriterDing
    }
    public EnumSoundType SoundType { get; set; } = EnumSoundType.ElevatorBellDing;
    public TimeSpan HowOftenToPlay { get; set; }
    private SoundPlayer? _obj_Player = null;
    private Thread? _thread_Player;
    public TimeSpan? MaxTimeToPlay { get; set; }
    private void RunTask()
    {
        int totalSeconds = (int)MaxTimeToPlay!.Value.TotalSeconds;
        Task.Run(() =>
        {
            for (int x = 0; x < totalSeconds; x++)
            {
                Thread.Sleep(1000);
            }
            _obj_Player!.Active = false;
        });
    }
    public void PlaySound()
    {
        _obj_Player = new SoundPlayer(HowOftenToPlay, SoundType);
        _thread_Player = new Thread(_obj_Player.Play);
        _thread_Player.Start();
        if (MaxTimeToPlay.HasValue == true)
        {
            RunTask();
        }
    }
    internal static bool WasDisposed { get; set; }
    public static void Dispose()
    {
        WasDisposed = true;
    }
    public async void StopPlay()
    {
        if (_obj_Player == null)
        {
            throw new Exception("No system sound is currently playing.");
        }
        _obj_Player.Active = false;
        await Task.Run(() =>
        {
            do
            {
                if (_thread_Player!.IsAlive == false || WasDisposed)
                {
                    break;
                }
            }
            while (true);
        });
        _obj_Player = null;
    }
}