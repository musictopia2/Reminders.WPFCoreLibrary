using Microsoft.Extensions.DependencyInjection;
namespace Reminders.WPFCoreLibrary.Windows;
public static class StartUp
{
    private readonly static ServiceCollection _services = new();
    private static ServiceProvider? _provides;
    public static Action<ServiceCollection>? ExtraServiceProcesses { get; set; }
    private static bool _loaded = false;
    public static ServiceProvider GetProvider()
    {
        if (_loaded)
        {
            return _provides!;
        }
        _services.AddBlazorWebView();
        _services.AddSingleton<ReminderContainer, ReminderContainer>();
        ExtraServiceProcesses?.Invoke(_services);
        _loaded = true;
        _provides = _services.BuildServiceProvider();
        return _provides;
    }

    public static IServiceCollection TransferPopups(this IServiceCollection services)
    {
        var provider = GetProvider();
        IPopUp pop = provider.GetRequiredService<IPopUp>();
        services.AddSingleton(pop);
        ReminderContainer reminder = provider.GetRequiredService<ReminderContainer>();
        services.AddSingleton(reminder);
        return services;
    }
}