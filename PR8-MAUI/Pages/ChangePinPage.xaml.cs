namespace PR8_MAUI.Pages;

public partial class ChangePinPage : ContentPage
{
    public ChangePinPage()
    {
        InitializeComponent();
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        InfoLabel.Text = "";

        var oldPin = OldPinEntry.Text ?? "";
        var newPin = NewPinEntry.Text ?? "";
        var repPin = RepeatPinEntry.Text ?? "";

        if (oldPin != Data.PinCode)
        {
            InfoLabel.Text = "Старый ПИН неверный";
            return;
        }

        if (newPin.Length != 3 || !newPin.All(char.IsDigit))
        {
            InfoLabel.Text = "Новый ПИН должен быть из 3 цифр";
            return;
        }

        if (newPin != repPin)
        {
            InfoLabel.Text = "Новый ПИН и повтор не совпадают";
            return;
        }

        Data.PinCode = newPin;

        await DisplayAlert("Готово", "ПИН изменён", "OK");
        await Navigation.PopAsync();
    }
}
