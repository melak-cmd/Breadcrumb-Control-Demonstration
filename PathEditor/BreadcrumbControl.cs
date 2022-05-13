using DevExpress.Xpf.Editors;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PathEditor
{
    /// <summary>
    /// Follow steps 1a or 1b and then 2 to use this custom control in a XAML file.
    ///
    /// Step 1a) Using this custom control in a XAML file that exists in the current project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:PathEditor"
    ///
    ///
    /// Step 1b) Using this custom control in a XAML file that exists in a different project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:PathEditor;assembly=PathEditor"
    ///
    /// You will also need to add a project reference from the project where the XAML file lives
    /// to this project and Rebuild to avoid compilation errors:
    ///
    ///     Right click on the target project in the Solution Explorer and
    ///     "Add Reference"->"Projects"->[Select this project]
    ///
    ///
    /// Step 2)
    /// Go ahead and use your control in the XAML file.
    ///
    ///     <MyNamespace:CustomControl1/>
    ///
    /// </summary>
    public class BreadcrumbControl : TextEdit
    {
        private ObservableCollection<PathItem> pathItems;

        static BreadcrumbControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(BreadcrumbControl), new FrameworkPropertyMetadata(typeof(BreadcrumbControl)));
        }

        public BreadcrumbControl()
        {
            DependencyPropertyDescriptor descriptor = DependencyPropertyDescriptor.FromProperty(EditValueProperty, typeof(TextEdit));
            descriptor.AddValueChanged(this, EditValChanged);
        }

        public ObservableCollection<PathItem> PathItems
        {
            get => pathItems;
            set
            {
                pathItems = value;
            }
        }

        private void EditValChanged(object? sender, EventArgs e)
        {
            BreadcrumbControl? editor = sender as BreadcrumbControl;
            PathItems.Clear();
            string? valueString = editor.EditValue as string;

            if (valueString == null)
            {
                return;
            }

            string[] dirsInCurrentPath = valueString.Split('\\');
            string pathString = "";
            for (int i = 0; i < dirsInCurrentPath.Count(); i++)
            {
                if (dirsInCurrentPath[i] == "")
                {
                    break;
                }

                pathString += dirsInCurrentPath[i] + "\\";

                if (!Directory.Exists(pathString))
                {
                    Text = pathString.Substring(0, pathString.Length - dirsInCurrentPath[i].Length - 1);
                    break;
                }
                PathItems.Add(new PathItem(GetDirs(pathString, i), dirsInCurrentPath[i], pathString, this));
            }
        }

        public ObservableCollection<string> GetDirs(string path, int index)
        {
            ObservableCollection<string> col = new ObservableCollection<string>();
            string[] drs;
            try
            {
                drs = Directory.GetDirectories(path);
            }
            catch (UnauthorizedAccessException)
            {
                col = null;
                return col;
            }

            foreach (string s in drs)
            {
                try
                {
                    Directory.GetDirectories(s);
                    string[] dirsInCurrentPath = s.Split('\\');
                    col.Add(dirsInCurrentPath[dirsInCurrentPath.Count() - 1]);
                }
                catch (UnauthorizedAccessException)
                {
                }
            }
            return col;
        }
    }
}
