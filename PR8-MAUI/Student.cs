using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PR8_MAUI
{
    public class Student : INotifyPropertyChanged
    {
        private string _lastName;
        private string _firstName;
        private string _middleName;
        private DateTime _birthDate;
        private int _age;
        private string _gender;
        private bool _needsDormitory;
        private bool _isMonitor;
        private string _mathGrade;
        private string _programmingGrade;
        private string _englishGrade;
        private string _additionalInfo;
        private ImageSource _photo;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public string LastName
        {
            get => _lastName;
            set { _lastName = value; OnPropertyChanged(); OnPropertyChanged(nameof(FullName)); }
        }

        public string FirstName
        {
            get => _firstName;
            set { _firstName = value; OnPropertyChanged(); OnPropertyChanged(nameof(FullName)); }
        }

        public string MiddleName
        {
            get => _middleName;
            set { _middleName = value; OnPropertyChanged(); OnPropertyChanged(nameof(FullName)); }
        }

        public string FullName => $"{LastName} {FirstName} {MiddleName}";

        public DateTime BirthDate
        {
            get => _birthDate;
            set { _birthDate = value; OnPropertyChanged(); }
        }

        public int Age
        {
            get => _age;
            set { _age = value; OnPropertyChanged(); OnPropertyChanged(nameof(AgeInfo)); }
        }

        public string Gender
        {
            get => _gender;
            set { _gender = value; OnPropertyChanged(); }
        }

        public bool NeedsDormitory
        {
            get => _needsDormitory;
            set { _needsDormitory = value; OnPropertyChanged(); OnPropertyChanged(nameof(DormitoryInfo)); }
        }

        public bool IsMonitor
        {
            get => _isMonitor;
            set { _isMonitor = value; OnPropertyChanged(); OnPropertyChanged(nameof(MonitorInfo)); }
        }

        public string MathGrade
        {
            get => _mathGrade;
            set { _mathGrade = value; OnPropertyChanged(); OnPropertyChanged(nameof(GradesInfo)); }
        }

        public string ProgrammingGrade
        {
            get => _programmingGrade;
            set { _programmingGrade = value; OnPropertyChanged(); OnPropertyChanged(nameof(GradesInfo)); }
        }

        public string EnglishGrade
        {
            get => _englishGrade;
            set { _englishGrade = value; OnPropertyChanged(); OnPropertyChanged(nameof(GradesInfo)); }
        }

        public string AdditionalInfo
        {
            get => _additionalInfo;
            set { _additionalInfo = value; OnPropertyChanged(); }
        }

        public ImageSource Photo
        {
            get => _photo;
            set { _photo = value; OnPropertyChanged(); }
        }

        public string AgeInfo => $"Возраст: {Age} лет";
        public string GradesInfo => $"Оценки: М:{MathGrade}, П:{ProgrammingGrade}, А:{EnglishGrade}";
        public string DormitoryInfo => NeedsDormitory ? "Нужно общежитие" : "Не нужно общежитие";
        public string MonitorInfo => IsMonitor ? "Староста" : "Не староста";
    }
}
