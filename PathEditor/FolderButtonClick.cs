using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace PathEditor
{
    public class FolderButtonClick : ICommand
    {
        private BreadcrumbControl owner;

        public event EventHandler? CanExecuteChanged;

        public FolderButtonClick(BreadcrumbControl ow)
        {
            throw new System.NotImplementedException();
        }

        public bool CanExecute(object? parameter)
        {
            throw new NotImplementedException();
        }

        public void Execute(object? parameter)
        {
            throw new NotImplementedException();
        }
    }
}