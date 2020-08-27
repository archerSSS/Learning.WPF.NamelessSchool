using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.IO;
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
    /// Логика взаимодействия для UserStudentGrades.xaml
    /// </summary>
    public partial class UserStudentGrades : UserControl
    {
        public UserStudentGrades()
        {
            InitializeComponent();
            StudentGrades = new ObservableCollection<StudentGrade>();

            SqlConnection connection = new SqlConnection("Server=localhost\\SQLEXPRESS;Database=master;Trusted_Connection=True;");
            SqlCommand com = new SqlCommand("USE SchoolBase SELECT [Предмет] FROM dbo.SchoolLessons", connection);
            connection.Open();
            SqlDataReader reader = com.ExecuteReader();
            while (reader.Read()) BoxLessons.Items.Add(reader.GetString(0));
            connection.Close();
        }


        public ObservableCollection<StudentGrade> StudentGrades
        {
            get { return (ObservableCollection<StudentGrade>)GetValue(StudentGradesProperty); }
            set { SetValue(StudentGradesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for StudentGrades.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StudentGradesProperty =
            DependencyProperty.Register("StudentGrades", typeof(ObservableCollection<StudentGrade>), typeof(UserStudentGrades), new PropertyMetadata());



        private void GradeCalendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            StudentGrades.Clear();
            SqlConnection connection = new SqlConnection("Server=localhost\\SQLEXPRESS;Database=master;Trusted_Connection=True;");

            string dateText = CalendarGrade.SelectedDate.Value.ToString("yyyy-MM-dd");
            //string commandText = "USE SchoolBase SELECT [Предмет], [Дата], [Оценка] FROM dbo.SchoolGrades WHERE [Дата] BETWEEN '2042-26-10 0:00' AND '2042-27-10 23:59' AND [Идентификатор учащегося] = " + 4;
            string commandText = "USE SchoolBase SELECT [Предмет], [Дата], [Оценка] FROM dbo.SchoolGrades WHERE [Дата] BETWEEN '" + dateText + " 0:00' AND '" + dateText + " 23:59' AND [Идентификатор учащегося] = " + StudentSession.Id + "";
            //if (BoxLessons.SelectedIndex != -1) commandText += " AND [Предмет] = '" + BoxLessons.SelectedItem.ToString() + "'";
            //commandText += " ORDER BY [Дата]";

            SqlCommand com = new SqlCommand(commandText, connection);
            connection.Open();
            SqlDataReader reader = com.ExecuteReader();
            while (reader.Read())
                StudentGrades.Add(new StudentGrade() { 
                    Lesson = reader.GetString(0), 
                    Date = reader.GetDateTime(1).ToString("yyyy-MM-dd"), 
                    Grade = reader.GetByte(2) });
            connection.Close();
        }
    }
}
