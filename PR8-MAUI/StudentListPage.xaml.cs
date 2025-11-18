using Microsoft.Maui.Controls;
namespace PR8_MAUI;

public partial class StudentListPage : ContentPage
{
    private Student selectedStudent;
    public StudentListPage()
	{
		InitializeComponent();
	}
    protected override void OnAppearing()
    {
        base.OnAppearing();
        RefreshStudentList();
    }

    private void RefreshStudentList()
    {
        studentsListView.ItemsSource = null;
        studentsListView.ItemsSource = StudentService.Students;
    }

    private void StudentsListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        selectedStudent = e.SelectedItem as Student;
        editButton.IsEnabled = selectedStudent != null;
        deleteButton.IsEnabled = selectedStudent != null;
    }

    private async void EditButton_Clicked(object sender, EventArgs e)
    {
        if (selectedStudent == null) return;
        await Navigation.PushAsync(new EditStudentPage(selectedStudent));
    }

    private async void DeleteButton_Clicked(object sender, EventArgs e)
    {
        if (selectedStudent == null) return;

        bool result = await DisplayAlert("Подтверждение удаления",
            $"Вы действительно хотите удалить студента {selectedStudent.FullName}?",
            "Да", "Нет");

        if (result)
        {
            StudentService.Students.Remove(selectedStudent);
            selectedStudent = null;
            editButton.IsEnabled = false;
            deleteButton.IsEnabled = false;
            RefreshStudentList();
            await DisplayAlert("Успех", "Студент удален из списка", "OK");
        }
    }
}