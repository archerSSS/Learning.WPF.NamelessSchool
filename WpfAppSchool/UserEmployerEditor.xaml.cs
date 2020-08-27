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
    /// Логика взаимодействия для UserEmployerEditor.xaml
    /// </summary>
    public partial class UserEmployerEditor : UserControl
    {
        private EmployerUnit employer;
        public UserEmployerEditor(EmployerUnit e)
        {
            InitializeComponent();
            employer = e;
            if (employer != null)
            {
                TextID.Text = employer.Id + "";
                TextLogin.Text = employer.Login;
                TextPassword.Text = employer.Password;
                TextName.Text = employer.Name;
                TextSurname.Text = employer.Surname;
                TextPosition.Text = employer.Position;
                TextClassNumber.Text = employer.ClassNumber;
            }
        }

        private void AcceptClick(object sender, RoutedEventArgs e)
        {
            string login = TextLogin.Text;
            string password = TextPassword.Text;
            string name = TextName.Text;
            string surname = TextSurname.Text;
            string position = TextPosition.Text;
            string classNumber;

            if (TextClassNumber.Text == "") classNumber = "NULL";
            else classNumber = "'" + TextClassNumber.Text + "'";

            string commandText;
            if (employer == null) commandText = "USE SchoolBase INSERT INTO dbo.SchoolEmployees VALUES('" + login + "', '" + password + "', '" + name + "', '" + surname + "', '" + position + "', " + classNumber + ")";
            else commandText = "USE SchoolBase UPDATE dbo.SchoolEmployees SET [Логин] = '" + login + "', [Пароль] = '" + password + "', [Имя сотрудника] = '" + name + "', [Фамилия сотрудника] = '" + surname + "', [Должность] = '" + position + "', [Класс] = " + classNumber + " WHERE [Идентификатор] = " + employer.Id + "";
            
            try
            {
                using (SqlConnection connection = new SqlConnection("Server=localhost\\SQLEXPRESS;Database=master;Trusted_Connection=True;"))
                {
                    SqlCommand command = new SqlCommand(commandText, connection);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex){}
            
            ((Border)Parent).Child = new UserEmployerOperator();
        }

        private void CancelClick(object sender, RoutedEventArgs e)
        {
            ((Border)Parent).Child = new UserEmployerOperator();
        }
    }
}
