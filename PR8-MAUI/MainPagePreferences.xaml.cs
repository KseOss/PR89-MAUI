using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;
namespace PR8_MAUI;

public partial class MainPagePreferences : ContentPage
{
	public MainPagePreferences()
	{
		InitializeComponent();
        dateBirth.Date = DateTime.Now.AddYears(-18);
        UpdateAge();
    }

    private void dateBirth_DateSelected(object sender, DateChangedEventArgs e)
    {
        UpdateAge();
    }

    private void UpdateAge()
    {
        int ageValue = DateTime.Now.Year - dateBirth.Date.Year;
        if (DateTime.Now.Month < dateBirth.Date.Month ||
            (DateTime.Now.Month == dateBirth.Date.Month && DateTime.Now.Day < dateBirth.Date.Day))
        {
            ageValue--;
        }
        age.Text = $"Возраст - {ageValue}";
    }

    private void SaveToPreferences_Clicked(object sender, System.EventArgs e)
    {
        Preferences.Default.Set("familia", lastName.Text);
        Preferences.Default.Set("name", firstName.Text);
        Preferences.Default.Set("otchestvo", middleName.Text);
        Preferences.Default.Set("birthDate", dateBirth.Date);
        Preferences.Default.Set("gender", genderPicker.SelectedItem?.ToString() ?? "");
        Preferences.Default.Set("age", age.Text);

        DisplayAlert("Успех", "Данные сохранены в Preferences", "OK");
    }

    private void LoadFromPreferences_Clicked(object sender, System.EventArgs e)
    {
        lastName.Text = Preferences.Default.Get("familia", "");
        firstName.Text = Preferences.Default.Get("name", "");
        middleName.Text = Preferences.Default.Get("otchestvo", "");
        dateBirth.Date = Preferences.Default.Get("birthDate", DateTime.Now.AddYears(-18));

        var savedGender = Preferences.Default.Get("gender", "");
        if (!string.IsNullOrEmpty(savedGender))
        {
            genderPicker.SelectedItem = savedGender;
        }

        age.Text = Preferences.Default.Get("age", "Возраст - 18");

        DisplayAlert("Успех", "Данные загружены из Preferences", "OK");
    }

    private void ClearPreferences_Clicked(object sender, System.EventArgs e)
    {
        Preferences.Default.Remove("familia");
        Preferences.Default.Remove("name");
        Preferences.Default.Remove("otchestvo");
        Preferences.Default.Remove("birthDate");
        Preferences.Default.Remove("gender");
        Preferences.Default.Remove("age");

        lastName.Text = "";
        firstName.Text = "";
        middleName.Text = "";
        dateBirth.Date = DateTime.Now.AddYears(-18);
        genderPicker.SelectedIndex = -1;
        UpdateAge();

        DisplayAlert("Успех", "Preferences очищены", "OK");
    }
}