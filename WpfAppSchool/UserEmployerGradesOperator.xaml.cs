using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Логика взаимодействия для UserEmployerGradesOperator.xaml
    /// </summary>
    public partial class UserEmployerGradesOperator : UserControl
    {
        private short hours;
        private short minutes;
        private int studentID;
        
        public UserEmployerGradesOperator(int id)
        {
            InitializeComponent();
            studentID = id;
            ElementsGrade = new ObservableCollection<ListElementGrade>();
            for (int grade = 1; grade < 6; grade++) BoxGrades.Items.Add(grade);

            SqlConnection connection = new SqlConnection("Server=localhost\\SQLEXPRESS;Database=master;Trusted_Connection=True;");
            SqlCommand command = new SqlCommand("USE SchoolBase SELECT [Предмет] FROM dbo.SchoolTeacherLessons WHERE [Преподаватель ID] = " + EmployerSession.Id + "", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read()) BoxLessons.Items.Add(reader.GetString(0));
            connection.Close();

            command = new SqlCommand("USE SchoolBase SELECT [Дата], [Предмет], [Оценка] FROM dbo.SchoolGrades WHERE [Идентификатор учащегося] = " + studentID + "", connection);
            connection.Open();
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                ElementsGrade.Add(new ListElementGrade() { 
                    Date = reader.GetDateTime(0).ToString("yyyy-MM-dd"), 
                    Lesson = reader.GetString(1), 
                    Grade = reader.GetByte(2) });   //(reader.GetDateTime(3).ToString("yyyy-MM-dd") + " | " + reader.GetString(0) + ":    " + reader.GetByte(2));
            }
        }

        public ObservableCollection<ListElementGrade> ElementsGrade
        {
            get { return (ObservableCollection<ListElementGrade>)GetValue(ElementsGradeProperty); }
            set { SetValue(ElementsGradeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ElementGrade.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ElementsGradeProperty =
            DependencyProperty.Register("ElementsGrade", typeof(ObservableCollection<ListElementGrade>), typeof(UserEmployerGradesOperator), new PropertyMetadata());



        /*
         * Increase and Decrease. Future features. Might not needed
         */
        private void IncreaseTimeHour(object sender, RoutedEventArgs e)
        {
            if ((hours += 1) > 23) hours = 0;
            ClockHours.Text = hours + "";
        }
        private void IncreaseTimeMinutes(object sender, RoutedEventArgs e)
        {
            if ((minutes += 10) > 50) minutes = 0;
            ClockMinutes.Text = ":" + minutes;
        }

        private void DecreaseTimeHour(object sender, RoutedEventArgs e)
        {
            if ((hours -= 1) < 0) hours = 23;
            ClockHours.Text = hours + "";
        }
        private void DecreaseTimeMinutes(object sender, RoutedEventArgs e)
        {
            if ((minutes -= 10) < 0) minutes = 50;
            ClockMinutes.Text = ":" + minutes;
        }
        /**/




        private void AddGradeClick(object sender, RoutedEventArgs e)
        {
            if (BoxLessons.SelectedIndex != -1 && BoxGrades.SelectedIndex != -1 && CalendarOperator.SelectedDate != null)
            {
                SqlConnection connection = new SqlConnection("Server=localhost\\SQLEXPRESS;Database=master;Trusted_Connection=True;");
                SqlCommand command = new SqlCommand("USE SchoolBase INSERT INTO dbo.SchoolGrades VALUES('" + BoxLessons.SelectedItem.ToString() + "', " + studentID + ", " + Byte.Parse(BoxGrades.SelectedItem.ToString()) + ", '" + (CalendarOperator.SelectedDate.Value).ToString("yyyy-MM-dd") + "')", connection);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
                ((Border)Parent).Child = new UserEmployerGrades();
            }
        }

        private void CancelClick(object sender, RoutedEventArgs e)
        {
            ((Border)Parent).Child = new UserEmployerGrades();
        }
    }
}
