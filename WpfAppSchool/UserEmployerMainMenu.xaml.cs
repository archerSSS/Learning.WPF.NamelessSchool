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
    /// Логика взаимодействия для UserEmployerMainMenu.xaml
    /// </summary>
    public partial class UserEmployerMainMenu : UserControl
    {
        public UserEmployerMainMenu()
        {
            InitializeComponent();
            IsEnabledRegulator = EmployerSession.IsPrincipal();
        }


        public bool IsEnabledRegulator
        {
            get { return (bool)GetValue(IsEnabledRegulatorProperty); }
            set { SetValue(IsEnabledRegulatorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsEnabledRegulator.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsEnabledRegulatorProperty =
            DependencyProperty.Register("IsEnabledRegulator", typeof(bool), typeof(UserEmployerMainMenu), new PropertyMetadata());



        private void ScheduleClick(object sender, RoutedEventArgs e)
        {
            ContentCell.Child = new UserEmployerSchedule();
        }

        private void GradesClick(object sender, RoutedEventArgs e)
        {
            ContentCell.Child = new UserEmployerGrades();
        }

        private void RegulateClick(object sender, RoutedEventArgs e)
        {
            ContentCell.Child = new UserEmployerOperator();
        }

        private void DescriptionClick(object sender, RoutedEventArgs e)
        {

        }

        public static void SetDefaultUserControl()
        {

        }

        public static void SetGradesUserControl()
        {
            
        }
    }
}
