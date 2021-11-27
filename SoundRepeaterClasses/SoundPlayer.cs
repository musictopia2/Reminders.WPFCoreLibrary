using static Reminders.WPFCoreLibrary.SoundRepeaterClasses.SoundRepeater;
namespace Reminders.WPFCoreLibrary.SoundRepeaterClasses;
internal class SoundPlayer
{
    public bool Active { get; set; } = true;
    private readonly TimeSpan _ts_RepeatTime = new();
    private readonly EnumSoundType _obj_Sound;
    public SoundPlayer(TimeSpan howOften, EnumSoundType sound)
    {
        _ts_RepeatTime = howOften;
        _obj_Sound = sound;
    }
    public void Play()
    {
        var thisResource = _obj_Sound switch
        {
            EnumSoundType.Buzzer => Properties.Resources.buzzer,
            EnumSoundType.DoorBell => Properties.Resources.doorbell,
            EnumSoundType.CensoredBeep => Properties.Resources.censoredbeep,
            EnumSoundType.MissileBeep => Properties.Resources.misslebeep,
            EnumSoundType.ElevatorBellDing => Properties.Resources.elevatording,
            EnumSoundType.TypewriterDing => Properties.Resources.typewriterding,
            _ => throw new Exception("No Resource Found"),
        };
        System.Media.SoundPlayer thisS = new(thisResource);
        while (Active)
        {
            thisS.PlaySync(); //so it will just play until it shows its finished
            Thread.Sleep((int)_ts_RepeatTime.TotalMilliseconds);
            if (WasDisposed)
            {
                thisS.Dispose();
                return;
            }
        }
        thisS.Dispose();
    }
}