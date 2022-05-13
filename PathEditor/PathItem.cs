using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace PathEditor
{
    public class PathItem
    {
        private BreadcrumbControl owner;
        private string _selectedItem;
        private FolderButtonClick clickCommand;

        public PathItem(ObservableCollection<string> dr, string fl, string fp, BreadcrumbControl ow)
        {
            Dirs = dr;
            Folder = fl;
            FullPathToFolder = fp;
            owner = ow;
            clickCommand = new FolderButtonClick(ow);
        }

        public string selectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value;
                if(value != null)
                {
                    owner.EditValue = FullPathToFolder + value;
                }
            }
        }

        public ObservableCollection<string> Dirs
        {
            get => default;
            set
            {
            }
        }

        public string Folder
        {
            get => default;
            set
            {
            }
        }

        public string FullPathToFolder
        {
            get => default;
            set
            {
            }
        }

        public ICommand ClickCommand
        {
            get => clickCommand;
        }
    }
}