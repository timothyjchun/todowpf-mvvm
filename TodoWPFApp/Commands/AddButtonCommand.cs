using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TodoWPFApp.Commands
{
    class AddButtonCommand : ICommand
    {


        Action<object> ExecuteMethod;
        Func<object, bool> CanExecuteMethod;


        public AddButtonCommand(Action<object> executeMethod, Func<object, bool> canExecuteMethod)
        {
            ExecuteMethod = executeMethod;
            CanExecuteMethod = canExecuteMethod;
        }


        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            // return true;
            return CanExecuteMethod(parameter);
        }

        public void Execute(object parameter)
        {
           ExecuteMethod(parameter);
        }
    }
}
