using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WpfAppSchool
{
    public class EmployerViewModel : INotifyPropertyChanged
    {
        private EmployerUnit employer;
        public EmployerUnit Employer { get { return employer; } set { employer = value; NotifyPropertyChanged("Employer"); } }

        private ICommand empCommand;
        public ICommand EmpCommand
        {
            get
            {
                if (empCommand == null) empCommand = new MaskCommand(Execute, CanExecute);
                return empCommand;
            }
        }

        private void Execute(object parameter)
        {
            
        }

        private bool CanExecute(object parameter)
        {
            return true;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged(string p)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(p));
            }
        }
    }
}
