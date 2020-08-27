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
    /// Логика взаимодействия для UserEmployerAuthorization.xaml
    /// </summary>
    public partial class UserEmployerAuthorization : UserControl
    {
        public UserEmployerAuthorization()
        {
            InitializeComponent();
        }

        private void AuthorizeClick(object sender, RoutedEventArgs e)
        {
            string tal = TextAuthorizationLogin.Text;
            string tap = TextAuthorizationPassword.Text;
            if (tal != "" && tap != "")
            {
                SqlConnection connection = new SqlConnection("Server=localhost\\SQLEXPRESS;Database=master;Trusted_Connection=True;");
                SqlCommand command = new SqlCommand("USE SchoolBase SELECT [Идентификатор], [Логин], [Пароль] FROM dbo.SchoolEmployees WHERE [Логин] = '" + tal + "' AND [Пароль] = '" + tap + "'", connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read()) 
                {
                    if (reader.GetString(1) == tal && reader.GetString(2) == tap && EmployerSession.SetEmployer(reader.GetInt32(0)))
                    {
                        ((Border)Parent).Child = new UserEmployerMainMenu();
                        break;
                    }
                }
                connection.Close();
            }
        }
    }
}
