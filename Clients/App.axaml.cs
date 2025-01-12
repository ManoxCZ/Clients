using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using Clients.Services;
using Clients.ViewModels;
using Clients.Views;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Clients;

public partial class App : Application
{
    public IServiceProvider Services { get; }


    public App()
    {
        Services = ConfigureServices();
    }

    /// <summary>
    /// Gets the current <see cref="App"/> instance in use
    /// </summary>
    public new static App Current => (App)Application.Current;

    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            // Line below is needed to remove Avalonia data validation.
            // Without this line you will get duplicate validations from both Avalonia and CT
            BindingPlugins.DataValidators.RemoveAt(0);

            desktop.MainWindow = new MainWindow()
            {
                DataContext = new MainWindowViewModel(Services.GetRequiredService<ClientDataService>()),
            };
        }

        base.OnFrameworkInitializationCompleted();
    }

    /// <summary>
    /// Configures the services for the application.
    /// </summary>
    private static IServiceProvider ConfigureServices()
    {
        ServiceCollection services = new();

        services.AddSingleton<ClientDataService>();

        return services.BuildServiceProvider();
    }
}