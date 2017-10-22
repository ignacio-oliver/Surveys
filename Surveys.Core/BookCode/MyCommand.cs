using System;
using System.Windows.Input;

namespace Surveys.Core.BookCode
{
    public class MyCommand : ICommand
    {
        private Action action = null;
        private Func<bool> canExecute = null;

        public MyCommand(Action action)
        {
            this.action = action;
        }
   
        public MyCommand(Action action, Func<bool> canExecute) : this(action)
        {
            this.canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return canExecute();
        }

        public void Execute(object parameter)
        {
            action();
        }

        public event EventHandler CanExecuteChanged;

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
