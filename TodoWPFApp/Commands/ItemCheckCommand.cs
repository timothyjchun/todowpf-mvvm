using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TodoWPFApp.Commands
{
    class ItemCheckCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;


        Action<object> ExecuteMethod;
        Func<object, bool> CanExecuteMethod;


        public ItemCheckCommand(Action<object> executeMethod, Func<object,bool> canExecuteMethod)
        {
            ExecuteMethod = executeMethod;
            CanExecuteMethod = canExecuteMethod;
        }
        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            ExecuteMethod(parameter);
        }
    }
}
