using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace PathEditor
{
    public class FolderButtonClick: ICommand
    {
        private BreadcrumbControl owner;

        public FolderButtonClick(BreadcrumbControl ow)
        {
            owner = ow;
        }

        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested+=value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            owner.EditValue = parameter;
        }
    }
}