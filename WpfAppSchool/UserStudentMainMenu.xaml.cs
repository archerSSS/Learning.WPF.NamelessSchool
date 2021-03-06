﻿using System;
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
    /// Логика взаимодействия для UserStudentMainMenu.xaml
    /// </summary>
    public partial class UserStudentMainMenu : UserControl
    {
        public UserStudentMainMenu()
        {
            InitializeComponent();
        }

        private void GradesClick(object sender, RoutedEventArgs e)
        {
            if (StudentSession.Id != -1) ContentCell.Child = new UserStudentGrades();
        }

        private void ScheduleClick(object sender, RoutedEventArgs e)
        {
            if (StudentSession.Id != -1) ContentCell.Child = new UserStudentSchedule();
        }
    }
}
