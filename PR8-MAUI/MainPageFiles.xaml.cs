using Microsoft.Maui.Controls;
namespace PR8_MAUI;

public partial class MainPageFiles : ContentPage
{
    private string folderPath;
    public MainPageFiles()
	{
		InitializeComponent();
        dateBirth.Date = DateTime.Now.AddYears(-18);
        UpdateAge();

        folderPath = FileSystem.Current.AppDataDirectory;
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

    private void SaveToFile_Clicked(object sender, System.EventArgs e)
    {
        try
        {
            StreamWriter outFile = new StreamWriter(Path.Combine(folderPath, "student.txt"));
            outFile.WriteLine(lastName.Text);
            outFile.WriteLine(firstName.Text);
            outFile.WriteLine(middleName.Text);
            outFile.WriteLine(dateBirth.Date.ToString());
            outFile.WriteLine(age.Text);
            outFile.Close();

            DisplayAlert("Успех", $"Данные сохранены в файл: {Path.Combine(folderPath, "student.txt")}", "OK");
        }
        catch (Exception ex)
        {
            DisplayAlert("Ошибка", $"Не удалось сохранить файл: {ex.Message}", "OK");
        }
    }

    private void LoadFromFile_Clicked(object sender, System.EventArgs e)
    {
        try
        {
            string filePath = Path.Combine(folderPath, "student.txt");

            if (File.Exists(filePath))
            {
                StreamReader inFile = new StreamReader(filePath);
                lastName.Text = inFile.ReadLine();
                firstName.Text = inFile.ReadLine();
                middleName.Text = inFile.ReadLine();

                var dateStr = inFile.ReadLine();
                if (DateTime.TryParse(dateStr, out DateTime birthDate))
                {
                    dateBirth.Date = birthDate;
                }

                age.Text = inFile.ReadLine();
                inFile.Close();

                DisplayAlert("Успех", "Данные загружены из файла", "OK");
            }
            else
            {
                DisplayAlert("Ошибка", "Файл не найден", "OK");
            }
        }
        catch (Exception ex)
        {
            DisplayAlert("Ошибка", $"Не удалось загрузить файл: {ex.Message}", "OK");
        }
    }
}