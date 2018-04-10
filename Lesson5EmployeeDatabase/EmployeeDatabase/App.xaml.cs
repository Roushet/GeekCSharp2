using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;
using System.Windows.Threading;

namespace EmployeeDatabase
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void App_OnDispatcherUnhandledException(object Sender, DispatcherUnhandledExceptionEventArgs E)
        {
            MessageBox.Show(E.Exception.Message, "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            //E.Handled = true;
        }
    }
}
