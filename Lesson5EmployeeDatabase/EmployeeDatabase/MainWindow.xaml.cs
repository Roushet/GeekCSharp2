using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using EmployeeDatabase.Data;

namespace EmployeeDatabase
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var file_name = Properties.Settings.Default.DataFilePath;
            var data = Data.Enterprise.LoadFromFile(file_name);

            DataContext = data;

            PresentationTraceSources.SetTraceLevel(this, PresentationTraceLevel.High);
        }

        /// <inheritdoc />
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            Properties.Settings.Default.Save();
        }

        private void AddDepartamentButton_OnClick(object Sender, RoutedEventArgs E)
        {
            if(!(DataContext is Data.Enterprise enterprise)) return;
            var dep = new Departament("New dep");
            enterprise.Departaments.Add(dep);
            //throw new ApplicationException("Ошибка в программе!");
        }

        private void ListViewItem_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var item = sender as ListViewItem;
            if(item != null && item.IsSelected)
            {
                var employee = item.Content as Employee;
                employee.Name = "!1111!!!!1";
                Debug.Print(employee.Name);

            }
        }

        private void menuSave_Click(object sender, RoutedEventArgs e)
        {
            Enterprise instance = Enterprise.GetInstance();
            instance.SaveToFile();
        }
    }
}
