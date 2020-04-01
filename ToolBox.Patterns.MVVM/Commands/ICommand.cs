using System;
using System.Collections.Generic;
using System.Text;

namespace ToolBox.Patterns.MVVM.Commands
{
    public interface ICommand : System.Windows.Input.ICommand
    {
        void RaiseCanExecuteChanged();
    }
}
