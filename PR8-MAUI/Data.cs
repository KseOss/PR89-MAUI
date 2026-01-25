using PR8_MAUI.Models;

namespace PR8_MAUI;

public static class Data
{
    // По заданию пароль 123 — делаем ПИН "123"
    public static string PinCode { get; set; } = "123";

    // Данные пользователя
    public static UserProfile CurrentUser { get; set; } = new UserProfile();

    public static void InitDefaults()
    {
        // Если у вас в ПР9 уже есть Student и т.п. — можно подстроить, но это рабочий минимум.
        if (string.IsNullOrWhiteSpace(CurrentUser.FullName))
        {
            CurrentUser.FullName = "Иванов Иван";
            CurrentUser.Email = "ivanov@mail.ru";
            CurrentUser.Phone = "+7 (900) 000-00-00";
            CurrentUser.Group = "ИС-21";
        }
    }
}
