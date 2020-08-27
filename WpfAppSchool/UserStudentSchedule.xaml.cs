using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfAppSchool
{
    /// <summary>
    /// Логика взаимодействия для UserStudentSchedule.xaml
    /// </summary>
    public partial class UserStudentSchedule : UserControl
    {
        public UserStudentSchedule()
        {
            InitializeComponent();

            ScheduleListMonday = new List<string>();
            ScheduleListTuesday = new List<string>();
            ScheduleListWednesday = new List<string>();
            ScheduleListThursday = new List<string>();
            ScheduleListFriday = new List<string>();
            ScheduleListSaturday = new List<string>();
        }


        public List<string> ScheduleListMonday
        {
            get { return (List<string>)GetValue(ScheduleListMondayProperty); }
            set { SetValue(ScheduleListMondayProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ScheduleListMondayProperty =
            DependencyProperty.Register("ScheduleListMonday", typeof(List<string>), typeof(UserStudentSchedule), new PropertyMetadata());

        public List<string> ScheduleListTuesday
        {
            get { return (List<string>)GetValue(ScheduleListTuesdayProperty); }
            set { SetValue(ScheduleListTuesdayProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ScheduleListTuesdayProperty =
            DependencyProperty.Register("ScheduleListTuesday", typeof(List<string>), typeof(UserStudentSchedule), new PropertyMetadata());

        public List<string> ScheduleListWednesday
        {
            get { return (List<string>)GetValue(ScheduleListWednesdayProperty); }
            set { SetValue(ScheduleListWednesdayProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ScheduleListWednesdayProperty =
            DependencyProperty.Register("ScheduleListWednesday", typeof(List<string>), typeof(UserStudentSchedule), new PropertyMetadata());

        public List<string> ScheduleListThursday
        {
            get { return (List<string>)GetValue(ScheduleListThursdayProperty); }
            set { SetValue(ScheduleListThursdayProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ScheduleListThursdayProperty =
            DependencyProperty.Register("ScheduleListThursday", typeof(List<string>), typeof(UserStudentSchedule), new PropertyMetadata());

        public List<string> ScheduleListFriday
        {
            get { return (List<string>)GetValue(ScheduleListFridayProperty); }
            set { SetValue(ScheduleListFridayProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ScheduleListFridayProperty =
            DependencyProperty.Register("ScheduleListFriday", typeof(List<string>), typeof(UserStudentSchedule), new PropertyMetadata());


        public List<string> ScheduleListSaturday
        {
            get { return (List<string>)GetValue(ScheduleListSaturdayProperty); }
            set { SetValue(ScheduleListSaturdayProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ScheduleListSaturdayProperty =
            DependencyProperty.Register("ScheduleListSaturday", typeof(List<string>), typeof(UserStudentSchedule), new PropertyMetadata());



        private void Calendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            DateTime date = CalendarSchedule.SelectedDate.Value;

            int dayOffset = (int)date.DayOfWeek - 1;
            date = date.AddDays(-dayOffset);

            ScheduleListMonday.Clear();
            ScheduleListTuesday.Clear();
            ScheduleListWednesday.Clear();
            ScheduleListThursday.Clear();
            ScheduleListFriday.Clear();
            ScheduleListSaturday.Clear();
            ScheduleListMonday = SetDailySchedule(date.ToString("yyyy-dd-MM"));
            ScheduleListTuesday = SetDailySchedule((date = date.AddDays(1)).ToString("yyyy-dd-MM"));
            ScheduleListWednesday = SetDailySchedule((date = date.AddDays(1)).ToString("yyyy-dd-MM"));
            ScheduleListThursday = SetDailySchedule((date = date.AddDays(1)).ToString("yyyy-dd-MM"));
            ScheduleListFriday = SetDailySchedule((date = date.AddDays(1)).ToString("yyyy-dd-MM"));
            ScheduleListSaturday = SetDailySchedule(date.AddDays(1).ToString("yyyy-dd-MM"));

            Mouse.Capture(null);
        }

        private List<string> SetDailySchedule(string day)
        {
            List<string> dailySchedule = new List<string>();
            SqlConnection connection = new SqlConnection("Server=localhost\\SQLEXPRESS;Database=master;Trusted_Connection=True;");
            SqlCommand command = new SqlCommand("USE SchoolBase SELECT [Дата], [Предмет] FROM SchoolSchedule WHERE [Дата] BETWEEN '" + day + " 0:00' AND '" + day + " 23:59' AND [Класс] = '" + StudentSession.ClassNumber + "' ORDER BY [Дата]", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read()) dailySchedule.Add(reader.GetDateTime(0).ToString("HH:mm") + " " + reader.GetString(1));
            connection.Close();
            return dailySchedule;
        }
    }
}
