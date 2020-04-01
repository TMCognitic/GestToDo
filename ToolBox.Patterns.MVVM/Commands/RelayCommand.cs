﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ToolBox.Patterns.MVVM.Commands
{
    public class RelayCommand : ICommand
    {
        private Action _execute;
        private Func<bool> _canExecute;

        public event EventHandler CanExecuteChanged;

        public RelayCommand(Action execute, Func<bool> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return (_canExecute is null) ? true : _canExecute();
        }

        public void Execute(object parameter)
        {
            _execute();
        }

        void ICommand.RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
