using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для MainUserControl.xaml
    /// </summary>
    public partial class MainUserControl : UserControl
    {
        public MainUserControl()
        {
            InitializeComponent();
        }

        private void TreeViewItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void TreeStudentAuthorizationClick(object sender, MouseButtonEventArgs e)
        {
            ContentCell.Child = new UserStudentAuthorization();
        }

        private void TreeEmployerAuthorizationClick(object sender, MouseButtonEventArgs e)
        {
            ContentCell.Child = new UserEmployerAuthorization();
        }

        private void TreeStudentMenuClick(object sender, MouseButtonEventArgs e)
        {
            if (StudentSession.Id != 0) ContentCell.Child = new UserStudentMainMenu();
            else ContentCell.Child = new UserStudentAuthorization();
        }

        private void TreeStudentDescriptionClick(object sender, MouseButtonEventArgs e)
        {
            if (StudentSession.Id != 0) ContentCell.Child = new UserStudentDescription();
        }
        
        private void TreeEmployerMenuClick(object sender, MouseButtonEventArgs e)
        {
            if (EmployerSession.Id != 0) ContentCell.Child = new UserEmployerMainMenu();
            else ContentCell.Child = new UserEmployerAuthorization();
        }
    }
}
