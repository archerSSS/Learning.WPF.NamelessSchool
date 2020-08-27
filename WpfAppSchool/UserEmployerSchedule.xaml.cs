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
using System.Xml;

namespace WpfAppSchool
{
    /// <summary>
    /// Логика взаимодействия для UserEmployerSchedule.xaml
    /// </summary>
    public partial class UserEmployerSchedule : UserControl
    {
        private int minutes;
        private int hours;

        public UserEmployerSchedule()
        {
            InitializeComponent();

            ScheduleListMonday = new List<string>();
            ScheduleListTuesday = new List<string>();
            ScheduleListWednesday = new List<string>();
            ScheduleListThursday = new List<string>();
            ScheduleListFriday = new List<string>();
            ScheduleListSaturday = new List<string>();
            IsEnabledScheduleOperator = EmployerSession.IsPrincipal();
            GridScheduleOperatorVisibility = Visibility.Hidden;
            BorderClasses.Visibility = EmployerSession.IsPrincipal() ? Visibility.Visible : Visibility.Hidden;

            BoxClasses.Items.Add("Выберите класс");
            BoxLessons.Items.Add("Выберите предмет");
            BoxClasses.SelectedIndex = 0;
            BoxLessons.SelectedIndex = 0;

            XmlDocument doc = new XmlDocument();
            doc.Load("E://Univers//Other//Work//C#//SomeFiles//1//WpfAppSchool//Lessons.xml");
            XmlElement xe = doc.DocumentElement;
            foreach (XmlNode node in xe.ChildNodes)
            {
                string ns = node.InnerText.Trim(new char[] { '\r', '\n', ' ' });
                BoxLessons.Items.Add(ns);
            }

            doc.Load("E://Univers//Other//Work//C#//SomeFiles//1//WpfAppSchool//ClassNumbers.xml");
            xe = doc.DocumentElement;
            foreach (XmlNode node in xe.ChildNodes)
            {
                string ns = node.InnerText.Trim(new char[] { '\r', '\n', ' ' });
                BoxClasses.Items.Add(ns);
            }
        }


        public Visibility GridScheduleOperatorVisibility
        {
            get { return (Visibility)GetValue(GridScheduleOperatorVisibilityProperty); }
            set { SetValue(GridScheduleOperatorVisibilityProperty, value); }
        }

        // Using a DependencyProperty as the backing store for GridScheduleOperatorVisibility.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty GridScheduleOperatorVisibilityProperty =
            DependencyProperty.Register("GridScheduleOperatorVisibility", typeof(Visibility), typeof(UserEmployerSchedule), new PropertyMetadata());



        public bool IsEnabledScheduleOperator
        {
            get { return (bool)GetValue(IsEnabledScheduleOperatorProperty); }
            set { SetValue(IsEnabledScheduleOperatorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsSchedulerActive.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsEnabledScheduleOperatorProperty =
            DependencyProperty.Register("IsEnabledScheduleOperator", typeof(bool), typeof(UserEmployerSchedule), new PropertyMetadata(false));




        public List<string> ScheduleListMonday
        {
            get { return (List<string>)GetValue(ScheduleListMondayProperty); }
            set { SetValue(ScheduleListMondayProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ScheduleListMondayProperty =
            DependencyProperty.Register("ScheduleListMonday", typeof(List<string>), typeof(UserEmployerSchedule), new PropertyMetadata());

        public List<string> ScheduleListTuesday
        {
            get { return (List<string>)GetValue(ScheduleListTuesdayProperty); }
            set { SetValue(ScheduleListTuesdayProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ScheduleListTuesdayProperty =
            DependencyProperty.Register("ScheduleListTuesday", typeof(List<string>), typeof(UserEmployerSchedule), new PropertyMetadata());
        
        public List<string> ScheduleListWednesday
        {
            get { return (List<string>)GetValue(ScheduleListWednesdayProperty); }
            set { SetValue(ScheduleListWednesdayProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ScheduleListWednesdayProperty =
            DependencyProperty.Register("ScheduleListWednesday", typeof(List<string>), typeof(UserEmployerSchedule), new PropertyMetadata());

        public List<string> ScheduleListThursday
        {
            get { return (List<string>)GetValue(ScheduleListThursdayProperty); }
            set { SetValue(ScheduleListThursdayProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ScheduleListThursdayProperty =
            DependencyProperty.Register("ScheduleListThursday", typeof(List<string>), typeof(UserEmployerSchedule), new PropertyMetadata());

        public List<string> ScheduleListFriday
        {
            get { return (List<string>)GetValue(ScheduleListFridayProperty); }
            set { SetValue(ScheduleListFridayProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ScheduleListFridayProperty =
            DependencyProperty.Register("ScheduleListFriday", typeof(List<string>), typeof(UserEmployerSchedule), new PropertyMetadata());
        
        public List<string> ScheduleListSaturday
        {
            get { return (List<string>)GetValue(ScheduleListSaturdayProperty); }
            set { SetValue(ScheduleListSaturdayProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ScheduleListSaturdayProperty =
            DependencyProperty.Register("ScheduleListSaturday", typeof(List<string>), typeof(UserEmployerSchedule), new PropertyMetadata());



        public bool IsAddButtonFocused
        {
            get { return (bool)GetValue(IsAddButtonFocusedProperty); }
            set { SetValue(IsAddButtonFocusedProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsAddButtonFocused.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsAddButtonFocusedProperty =
            DependencyProperty.Register("IsAddButtonFocused", typeof(bool), typeof(UserEmployerSchedule), new PropertyMetadata());



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
            if (EmployerSession.GetClassNumber() != null || BoxClasses.SelectedIndex != - 1)
            {
                List<string> dailySchedule = new List<string>();
                string cn = EmployerSession.GetClassNumber();
                if (cn == "") cn = (string)BoxClasses.SelectedItem;
                else cn = EmployerSession.GetClassNumber();
                SqlConnection connection = new SqlConnection("Server=localhost\\SQLEXPRESS;Database=master;Trusted_Connection=True;");
                SqlCommand command = new SqlCommand("USE SchoolBase SELECT [Дата], [Предмет] FROM SchoolSchedule WHERE [Дата] BETWEEN '" + day + " 0:00' AND '" + day + " 23:59' AND [Класс] = '" + cn + "' ORDER BY [Дата]", connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read()) dailySchedule.Add(reader.GetDateTime(0).ToString("HH:mm") + " " + reader.GetString(1));
                connection.Close();
                return dailySchedule;
            }
            return null;
        }

        private void IncreaseTimeHour(object sender, RoutedEventArgs e)
        {
            if ((hours += 1) > 23) hours = 0;
            ClockHours.Text = hours + "";
        }
        private void IncreaseTimeMinutes(object sender, RoutedEventArgs e)
        {
            if ((minutes += 10) > 50) minutes = 0;
            if (minutes == 0) ClockMinutes.Text = ":00";
            else ClockMinutes.Text = ":" + minutes;
        }

        private void DecreaseTimeHour(object sender, RoutedEventArgs e)
        {
            if ((hours -= 1) < 0) hours = 23;
            ClockHours.Text = hours + "";
        }
        private void DecreaseTimeMinutes(object sender, RoutedEventArgs e)
        {
            if ((minutes -= 10) < 0) minutes = 50;
            if (minutes == 0) ClockMinutes.Text = ":00";
            else ClockMinutes.Text = ":" + minutes;
        }

        private void ActivateScheduler(object sender, RoutedEventArgs e)
        {
            GridScheduleOperatorVisibility = EmployerSession.IsPrincipal() ? Visibility.Visible : Visibility.Hidden;
        }

        private void AddLesson(object sender, RoutedEventArgs e)
        {
            string choosedClassNumber = BoxClasses.Text;
            string choosedLesson = BoxLessons.Text;

            bool freeToGo;
            if (!(freeToGo = BoxClasses.SelectedIndex != 0)) TextOperatorError.Text = "Выберите класс";
            else if (!(freeToGo = BoxLessons.SelectedIndex != 0)) TextOperatorError.Text = "Выберите урок";
            else if (!(freeToGo = CalendarSchedule.SelectedDate != null)) TextOperatorError.Text = "Выберите дату"; 

            if (freeToGo)
            {
                SqlConnection connection = new SqlConnection("Server=localhost\\SQLEXPRESS;Database=master;Trusted_Connection=True;");
                SqlCommand command = new SqlCommand("USE SchoolBase SELECT [Дата], [Класс] FROM dbo.SchoolSchedule WHERE [Дата] = '" + CalendarSchedule.SelectedDate.Value.ToString("yyyy-dd-MM") + " " + ClockHours.Text + ClockMinutes.Text + "'", connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                bool doUpdate = reader.Read();
                connection.Close();

                if (doUpdate)
                {
                    SqlCommand updateCommand = new SqlCommand("USE SchoolBase UPDATE dbo.SchoolSchedule SET [Предмет] = '" + choosedLesson + "' WHERE [Дата] = '" + CalendarSchedule.SelectedDate.Value.ToString("yyyy-dd-MM") + " " + ClockHours.Text + ClockMinutes.Text + "' AND [Класс] = '" + choosedClassNumber + "'", connection);
                    connection.Open();
                    updateCommand.ExecuteNonQuery();
                    connection.Close();
                }
                else
                {
                    SqlCommand updateCommand = new SqlCommand("USE SchoolBase INSERT INTO dbo.SchoolSchedule ([Дата], [Класс], [Предмет]) VALUES('" + CalendarSchedule.SelectedDate.Value.ToString("yyyy-dd-MM") + " " + ClockHours.Text + ClockMinutes.Text + "', '"+ choosedClassNumber + "', '" + choosedLesson + "')", connection);
                    connection.Open();
                    updateCommand.ExecuteNonQuery();
                    connection.Close();
                }
                //TextOperatorError.Visibility = Visibility.Hidden;
                TextOperatorError.Text = "Расписание изменено";
            }
            else TextOperatorError.Visibility = Visibility.Visible;
        }

        private void BoxClasses_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Mouse.Capture(null);
        }

        private void BoxLessons_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Mouse.Capture(null);
        }
    }


}
