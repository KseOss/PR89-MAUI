namespace PR8_MAUI.Pages;

public partial class EditProfilePage : ContentPage
{
    public EditProfilePage()
    {
        InitializeComponent();

        FullNameEntry.Text = Data.CurrentUser.FullName;
        EmailEntry.Text = Data.CurrentUser.Email;
        PhoneEntry.Text = Data.CurrentUser.Phone;
        GroupEntry.Text = Data.CurrentUser.Group;
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        Data.CurrentUser.FullName = FullNameEntry.Text ?? "";
        Data.CurrentUser.Email = EmailEntry.Text ?? "";
        Data.CurrentUser.Phone = PhoneEntry.Text ?? "";
        Data.CurrentUser.Group = GroupEntry.Text ?? "";

        await Navigation.PopAsync();
    }

    private async void OnCancelClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}
