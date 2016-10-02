using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace XPlatform.Core
{
    public class DelegateCommand : ICommand
    {
        Action action;
        Action<object> actionWithParam;

        public DelegateCommand(Action action)
        {
            this.action = action;
        }
        public DelegateCommand(Action<object> action)
        {
            this.actionWithParam = action;
        }

        #region ICommand implementation
        public event EventHandler CanExecuteChanged;
        public bool CanExecute(object parameter)
        {
            return true;
        }
        public void Execute(object parameter)
        {
            if (action != null)
                action();
            if (actionWithParam != null)
                actionWithParam(parameter);
        }
        #endregion
    }
}
