using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAppSchool
{
    public class EmployerUnit : INotifyPropertyChanged
    {
        private int id;
        private string login;
        private string password;
        private string name;
        private string surname;
        private string position;
        private string classNumber;

        public int Id { get { return id; } private set { id = value; OnPropertyChanged("Id"); } }
        public string Login { get { return login; } set { login = value; OnPropertyChanged("Login"); } }
        public string Password { get { return password; } set { password = value; OnPropertyChanged("Password"); } }
        public string Name { get { return name; } set { name = value; OnPropertyChanged("Name"); } }
        public string Surname { get { return surname; } set { surname = value; OnPropertyChanged("Surname"); } }
        public string Position { get { return position; } set { position = value; OnPropertyChanged("Position"); } }
        public string ClassNumber { get { return classNumber; } set { classNumber = value; OnPropertyChanged("ClassNumber"); } }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string p)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(p));
            }
        }

        public EmployerUnit(int i, string log, string pas, string nam, string sur, string pos, string cls)
        {
            Id = i;
            Login = log;
            Password = pas;
            Name = nam;
            Surname = sur;
            Position = pos;
            ClassNumber = cls;
        }
    }
}
