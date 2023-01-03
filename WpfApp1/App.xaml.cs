using System.Windows;
using XControls.Utilities;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            GlobalExceptionHandler.Instance.HandleUnhandledException += (s, e) =>
            {
            };
        }
    }
}
