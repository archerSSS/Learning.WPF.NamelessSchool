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
    /// Логика взаимодействия для UserEmployerGrades.xaml
    /// </summary>
    public partial class UserEmployerGrades : UserControl
    {
        private List<int> studentIdList;

        public UserEmployerGrades()
        {
            InitializeComponent();

            studentIdList = new List<int>();
            DeleteGradeEnabled = EmployerSession.IsPrincipal();
            AddStudentEnabled = EmployerSession.IsPrincipal();

            string commandText = "USE SchoolBase SELECT [Идентификатор], [Имя учащегося], [Фамилия учащегося] FROM dbo.SchoolStudents";
            if (EmployerSession.GetClassNumber() != "") commandText += " WHERE [Класс] = '" + EmployerSession.GetClassNumber() + "'"; 
            SqlConnection connection = new SqlConnection("Server=localhost\\SQLEXPRESS;Database=master;Trusted_Connection=True;");
            SqlCommand command = new SqlCommand(commandText, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                studentIdList.Add(reader.GetInt32(0));
                ListStudents.Items.Add(reader.GetInt32(0) + " | " +
                                        reader.GetString(1) + " | " +
                                        reader.GetString(2));
            }
            connection.Close();
        }


        public bool DeleteGradeEnabled
        {
            get { return (bool)GetValue(DeleteGradeEnabledProperty); }
            set { SetValue(DeleteGradeEnabledProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DeleteGradeEnabledProperty =
            DependencyProperty.Register("DeleteGradeEnabledProperty", typeof(bool), typeof(UserEmployerGrades), new PropertyMetadata(false));



        public bool AddStudentEnabled
        {
            get { return (bool)GetValue(AddStudentEnabledProperty); }
            set { SetValue(AddStudentEnabledProperty, value); }
        }

        // Using a DependencyProperty as the backing store for AddStudentEnabled.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AddStudentEnabledProperty =
            DependencyProperty.Register("AddStudentEnabled", typeof(bool), typeof(UserEmployerGrades), new PropertyMetadata(false));



        private void AddGradeClick(object sender, RoutedEventArgs e)
        {
            int index = ListStudents.SelectedIndex;
            if(index != -1) ((Border)Parent).Child = new UserEmployerGradesOperator(studentIdList[index]);
        }

        private void DeleteGradeClick(object sender, RoutedEventArgs e)
        {
            if (EmployerSession.IsPrincipal())
            {
                int index = ListStudents.SelectedIndex;
                if (index != -1) ((Border)Parent).Child = new UserEmployerGradesOperator(studentIdList[index]);
            }
        }

        private void AddStudentClick(object sender, RoutedEventArgs e)
        {
            if (EmployerSession.IsPrincipal())
            {

            }
        }

        public static void SetDefaultUserControl()
        {
            
        }
    }
}
