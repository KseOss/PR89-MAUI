namespace PR8_MAUI.Pages;

public partial class ProfilePage : ContentPage
{
    public ProfilePage()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        FullNameLabel.Text = Data.CurrentUser.FullName;
        EmailLabel.Text = Data.CurrentUser.Email;
        PhoneLabel.Text = Data.CurrentUser.Phone;
        GroupLabel.Text = Data.CurrentUser.Group;
    }

    private async void OnEditClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new EditProfilePage());
    }

    private async void OnChangePinClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ChangePinPage());
    }

    private async void OnLogoutClicked(object sender, EventArgs e)
    {
        // Возвращаем на экран входа
        var auth = new PinAuthPage();
        Navigation.InsertPageBefore(auth, this);
        await Navigation.PopAsync();
    }
}
