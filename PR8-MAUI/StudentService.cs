using System.Collections.ObjectModel;

namespace PR8_MAUI
{
    public static class StudentService
    {
        public static ObservableCollection<Student> Students { get; set; } = new ObservableCollection<Student>();
    }   
}
