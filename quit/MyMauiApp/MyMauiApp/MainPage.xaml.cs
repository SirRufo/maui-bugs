using Microsoft.Maui.Dispatching;

namespace MyMauiApp;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    private async void OnQuitClicked( object sender, EventArgs e )
    {
        await QuitApplicationAsync();
    }

    private async void OnQuitFromTaskClicked( object sender, EventArgs e )
    {
        await Task.Run( async () => await QuitApplicationAsync() );
    }

    private async void OnQuitFromTaskWithSavedContextClicked( object sender, EventArgs e )
    {
        var synccontext = SynchronizationContext.Current;

        await Task.Run( () =>
        {
            synccontext.Send( async _ => await QuitApplicationAsync(), null );
        } );
    }

    public static async Task QuitApplicationAsync()
    {
        var app = Application.Current;

        if ( app.Dispatcher.IsDispatchRequired )
            await app.Dispatcher.DispatchAsync( async () => await QuitApplicationAsync() );
        else
            app.Quit();
    }
}


