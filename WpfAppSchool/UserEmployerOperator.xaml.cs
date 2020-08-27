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
    /// Логика взаимодействия для UserEmployerOperator.xaml
    /// </summary>
    public partial class UserEmployerOperator : UserControl
    {
        public UserEmployerOperator()
        {
            InitializeComponent();

            ListEmployer = new ObservableCollection<EmployerUnit>();
            SqlConnection connection = new SqlConnection("Server=localhost\\SQLEXPRESS;Database=master;Trusted_Connection=True;");
            SqlCommand command = new SqlCommand("USE SchoolBase SELECT * FROM dbo.SchoolEmployees", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                ListEmployer.Add(new EmployerUnit(reader.GetInt32(0), reader.GetString(1), 
                                                    reader.GetString(2), reader.GetString(3), 
                                                    reader.GetString(4), reader.GetString(5), 
                                                    reader.IsDBNull(6) ? null : reader.GetString(6)));
            }
            connection.Close();
        }



        public ObservableCollection<EmployerUnit> ListEmployer
        {
            get { return (ObservableCollection<EmployerUnit>)GetValue(ListEmployerProperty); }
            set { SetValue(ListEmployerProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ListEmployer.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ListEmployerProperty =
            DependencyProperty.Register("ListEmployer", typeof(ObservableCollection<EmployerUnit>), typeof(UserEmployerOperator), new PropertyMetadata());



        private void AddEmployer(object sender, RoutedEventArgs e)
        {
            ((Border)Parent).Child = new UserEmployerEditor(null);
        }

        private void EditEmployer(object sender, RoutedEventArgs e)
        {
            if (DataGridEmployers.SelectedIndex != -1) ((Border)Parent).Child = new UserEmployerEditor((EmployerUnit)DataGridEmployers.SelectedItem);
        }

        private void DeleteEmployer(object sender, RoutedEventArgs e)
        {
            if (DataGridEmployers.SelectedItem != null)
            {
                /*ListEmployer.Remove((EmployerUnit)DataGridEmployers.SelectedItem);*/
                EmployerUnit eu = (EmployerUnit)DataGridEmployers.SelectedItem;
                SqlConnection connection = new SqlConnection("Server=localhost\\SQLEXPRESS;Database=master;Trusted_Connection=True;");
                SqlCommand command = new SqlCommand("USE SchoolBase DELETE FROM dbo.SchoolEmployees WHERE [Идентификатор] = " + eu.Id + "", connection);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();

                ListEmployer.Remove(eu);
            }
        }

        private void OnEmployerSelected(object sender, SelectionChangedEventArgs e)
        {
            if (DataGridEmployers.SelectedItem != null)
            {
                EmployerUnit eu = (EmployerUnit)DataGridEmployers.SelectedItem;
                TextInfoLogin.Text = eu.Login;
                TextInfoPassword.Text = eu.Password;
                TextInfoClassNumber.Text = eu.ClassNumber;
            }
        }

        private void DataGridEmployers_SourceUpdated(object sender, DataTransferEventArgs e)
        {

        }
    }
}
