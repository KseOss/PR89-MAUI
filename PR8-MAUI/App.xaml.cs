using PR8_MAUI.Pages;

namespace PR8_MAUI;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        Data.InitDefaults();

        // Корень приложения - NavigationPage
        MainPage = new NavigationPage(new PinAuthPage());
    }
}
