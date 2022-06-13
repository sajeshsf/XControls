using System;
using System.Threading.Tasks;

namespace XControls.Utilities
{
    public class GlobalExceptionEventArgs : EventArgs
    {
        public string Source { get; set; }
        public object Sender { get; set; }
        public Exception Exception { get; set; }
        public GlobalExceptionEventArgs(string source, object sender, Exception exception)
        {
            Source = source;
            Sender = sender;
            Exception = exception;
        }
    }
    public class GlobalExceptionHandler
    {
        static GlobalExceptionHandler? _Instance;
        public EventHandler<GlobalExceptionEventArgs> HandleUnhandledException;
        public static GlobalExceptionHandler Instance => _Instance ??= new GlobalExceptionHandler();
        private GlobalExceptionHandler()
        {
            var appDomain = AppDomain.CurrentDomain;
            if (appDomain != null)
            {
                appDomain.UnhandledException += (s, e) =>
                {
                    HandleUnhandledException?.Invoke(s, new GlobalExceptionEventArgs(
                        "AppDomain.CurrentDomain.UnhandledException", s, (Exception)e.ExceptionObject));
                };
            }
            var application = System.Windows.Application.Current;
            if (application != null)
            {
                application.DispatcherUnhandledException += (s, e) =>
                {
                    HandleUnhandledException?.Invoke(s, new GlobalExceptionEventArgs(
                        "Application.Current.DispatcherUnhandledException", s, e.Exception));
                    e.Handled = true;
                };
            }
            TaskScheduler.UnobservedTaskException += (s, e) =>
            {
                HandleUnhandledException?.Invoke(s, new GlobalExceptionEventArgs(
                    "TaskScheduler.UnobservedTaskException", s, e.Exception));
                e.SetObserved();
            };
        }
    }
}
