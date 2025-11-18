using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;
namespace PR8_MAUI
{
    public partial class MainPage : ContentPage
    {
        private bool _needsDormitory = false;
        private bool _isMonitor = false;
        private string _selectedPhotoPath;

        public MainPage()
        {
            InitializeComponent(); dateBirth.Date = DateTime.Now.AddYears(-18);
            UpdateAge();
        }

        private void OnDormitoryToggled(object sender, ToggledEventArgs e)
        {
            _needsDormitory = e.Value;
        }

        private void OnMonitorToggled(object sender, ToggledEventArgs e)
        {
            _isMonitor = e.Value;
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

        private void MathStepper_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            mathGrade.Text = ((int)e.NewValue).ToString();
        }

        private void ProgrammingStepper_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            programmingGrade.Text = ((int)e.NewValue).ToString();
        }

        private void EnglishSlider_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            englishGrade.Text = e.NewValue.ToString("0.0");
        }

        private async void AddPhotoBtn_Clicked(object sender, EventArgs e)
        {
            try
            {
                var options = new PickOptions
                {
                    PickerTitle = "Выберите картинку",
                    FileTypes = FilePickerFileType.Images,
                };

                var result = await FilePicker.PickAsync(options);
                if (result != null)
                {
                    _selectedPhotoPath = result.FullPath;
                    studentPhoto.Source = ImageSource.FromFile(_selectedPhotoPath);
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ошибка", $"Не удалось загрузить фото: {ex.Message}", "OK");
            }
        }

        private async void SaveStudent_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(lastName.Text) || string.IsNullOrWhiteSpace(firstName.Text))
            {
                await DisplayAlert("Ошибка", "Заполните обязательные поля (Фамилия и Имя)", "OK");
                return;
            }

            var student = new Student
            {
                LastName = lastName.Text,
                FirstName = firstName.Text,
                MiddleName = middleName.Text,
                BirthDate = dateBirth.Date,
                Age = CalculateAge(dateBirth.Date),
                Gender = genderPicker.SelectedItem?.ToString() ?? "Не указан",
                NeedsDormitory = _needsDormitory,
                IsMonitor = _isMonitor,
                MathGrade = mathGrade.Text,
                ProgrammingGrade = programmingGrade.Text,
                EnglishGrade = englishGrade.Text,
                AdditionalInfo = additionalInfo.Text,
                Photo = studentPhoto.Source
            };

            StudentService.Students.Add(student);
            ClearForm();
            await DisplayAlert("Успех", $"Студент {student.FullName} добавлен в список", "OK");
            await Shell.Current.GoToAsync("//StudentList");
        }

        private void ClearForm()
        {
            lastName.Text = string.Empty;
            firstName.Text = string.Empty;
            middleName.Text = string.Empty;
            dateBirth.Date = DateTime.Now.AddYears(-18);
            UpdateAge();
            genderPicker.SelectedIndex = -1;
            additionalInfo.Text = string.Empty;
            studentPhoto.Source = "dotnet_bot.png";
            _selectedPhotoPath = null;
        }

        private int CalculateAge(DateTime birthDate)
        {
            int age = DateTime.Now.Year - birthDate.Year;
            if (DateTime.Now.Month < birthDate.Month ||
                (DateTime.Now.Month == birthDate.Month && DateTime.Now.Day < birthDate.Day))
            {
                age--;
            }
            return age;
        }
    }
}