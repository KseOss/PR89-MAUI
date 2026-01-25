using System.Text;
using System.Threading;
using Plugin.Maui.Biometric;

namespace PR8_MAUI.Pages;

public partial class PinAuthPage : ContentPage
{
    private readonly StringBuilder _pin = new();

    public PinAuthPage()
    {
        InitializeComponent();
        UpdateDots();
    }

    private void OnDigitClicked(object sender, EventArgs e)
    {
        if (sender is not Button btn) return;

        StatusLabel.Text = "";

        if (_pin.Length >= Data.PinCode.Length)
            return;

        _pin.Append(btn.Text);
        UpdateDots();

        if (_pin.Length == Data.PinCode.Length)
            _ = ValidatePinAsync();
    }

    private void OnBackspaceClicked(object sender, EventArgs e)
    {
        StatusLabel.Text = "";

        if (_pin.Length == 0) return;

        _pin.Remove(_pin.Length - 1, 1);
        UpdateDots();
    }

    private async Task ValidatePinAsync()
    {
        if (_pin.ToString() == Data.PinCode)
        {
            await GoToProfileAsync();
            return;
        }

        StatusLabel.Text = "Неверный ПИН";
        _pin.Clear();
        UpdateDots();
    }

    private async void OnFingerprintClicked(object sender, EventArgs e)
    {
        StatusLabel.Text = "";

        try
        {
            // Вызов биометрии (отпечаток/FaceID/TouchID — что доступно на устройстве)
            var result = await BiometricAuthenticationService.Default.AuthenticateAsync(
                new AuthenticationRequest
                {
                    Title = "Вход в приложение",
                    NegativeText = "Отмена"
                },
                CancellationToken.None
            );

            if (result.Status == BiometricResponseStatus.Success)
            {
                await GoToProfileAsync();
            }
            else
            {
                StatusLabel.Text = "Не удалось подтвердить биометрию";
            }
        }
        catch (Exception ex)
        {
            StatusLabel.Text = "Ошибка биометрии: " + ex.Message;
        }
    }

    private async Task GoToProfileAsync()
    {
        var profile = new ProfilePage();

        // Чтобы назад на вход не возвращаться:
        Navigation.InsertPageBefore(profile, this);
        await Navigation.PopAsync();
    }

    private void UpdateDots()
    {
        int need = Data.PinCode.Length;
        int filled = _pin.Length;

        var parts = new List<string>();
        for (int i = 0; i < need; i++)
            parts.Add(i < filled ? "●" : "○");

        DotsLabel.Text = string.Join(" ", parts);
    }
}
