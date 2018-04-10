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
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EmployeeDatabase
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class BindingTestWindow : Window
    {
        public BindingTestWindow()
        {
            InitializeComponent();

            var binding = new Binding();
            binding.Path = new PropertyPath("Text");
            binding.ElementName = "DataSourceTextBlock";
            binding.UpdateSourceTrigger = UpdateSourceTrigger.Explicit;
            DataDestBlock.SetBinding(TextBox.TextProperty, binding);
        }

        private void ButtonBase_OnClick(object Sender, RoutedEventArgs E)
        {
            var expr = DataDestBlock.GetBindingExpression(TextBox.TextProperty);
            expr?.UpdateSource();
        }
    }
}
