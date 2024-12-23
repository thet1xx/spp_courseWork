using System.Collections.ObjectModel;
using Plugin.LocalNotification;

namespace ReminderApp;

public partial class MainPage : ContentPage
{
    public ObservableCollection<Reminder> Reminders { get; set; } = new();

    public MainPage()
    {
        InitializeComponent();
        BindingContext = this;
    }

    private void AddReminder_Clicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(ReminderTitle.Text))
        {
            DisplayAlert("Ошибка", "Название напоминания не может быть пустым.", "ОК");
            return;
        }

        var reminder = new Reminder
        {
            Title = ReminderTitle.Text,
            ReminderTime = ReminderDate.Date + ReminderTime.Time
        };

        Reminders.Add(reminder);

        // Планирование уведомления
        ScheduleNotification(reminder);

        ReminderTitle.Text = string.Empty;
    }

    private void ScheduleNotification(Reminder reminder)
    {
        var notification = new NotificationRequest
        {
            Title = "Напоминание",
            Description = reminder.Title,
            Schedule = new NotificationRequestSchedule
            {
                NotifyTime = reminder.ReminderTime
            }
        };
        LocalNotificationCenter.Current.Show(notification);
    }
}

public class Reminder
{
    public string Title { get; set; }
    public DateTime ReminderTime { get; set; }
}
