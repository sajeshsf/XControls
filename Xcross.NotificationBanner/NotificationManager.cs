using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Threading;
using XControls.Helpers.Extensions;
using XControls.MessageBanner;

namespace UserControls.NotificationBanner
{
    public class NotificationManager
    {
        private static NotificationManager _Instance;
        public static NotificationManager Instance => _Instance ??= new NotificationManager();
        private NLog.Logger LogInstance { get; set; }
        public System.Windows.Application CurrentApplication { get; set; }
        public bool ShowTraceMessage { get; set; }
        public ObservableCollection<Notification> NotificationList { get; private set; }
        public int NewMessageCount { get; set; }
        private NotificationManager()
        {
            CurrentApplication ??= System.Windows.Application.Current;
            NotificationList = new ObservableCollection<Notification>();
            //LogInstance ??= LogHelper.Logger.Instance;

            //CurrentApplication.MainWindow.ContentRendered += OnContentRendered;
            //CurrentApplication.MainWindow.Closed += OnMainWindowClosed;
        }
        private void OnMainWindowClosed(object sender, EventArgs e)
        {
            NotificationList.Clear();
        }
        private void OnContentRendered(object sender, EventArgs e) => ShowPendingErrors();
        public void ShowErrorPopup(string errorMessage)
        {
            CurrentApplication?.Dispatcher?.Invoke(delegate
            {
                AddNewNotification(errorMessage);
                if (CurrentApplication?.MainWindow?.IsLoaded == true)
                {
                    var result = MessageBox.ShowMessageBox(CurrentApplication.MainWindow, "ERROR", errorMessage,
                        MessageBoxButtons.Ok, MessageBoxIcon.Error, DialogResult.Ok);
                }
            });
        }
        public void ShowPendingErrors()
        {
            if (NotificationList.Count > 0)
            {
                //var resultantErrorString = string.Join(Environment.NewLine, NotificationList);
                //NotificationList.ForEach(x => ReadPositionAndStatus(x));
                var errorString = string.Empty;
                foreach (var notification in NotificationList)
                {
                    errorString += notification.Message + Environment.NewLine;
                }
                CurrentApplication?.Dispatcher?.Invoke(delegate
                {
                    var result = MessageBox.ShowMessageBox(CurrentApplication.MainWindow, "ERROR", errorString,
                        MessageBoxButtons.Ok, MessageBoxIcon.Error, DialogResult.Ok);
                });
            }
        }
        public void AddNewNotification(string notification)
        {
            NewMessageCount++;
            NotificationList.Add(new Notification(notification));
            while (NotificationList.Count > 25)
            {
                NotificationList.RemoveAt(0);
                if (NewMessageCount >= 25)
                {
                    NewMessageCount--;
                }
            }
        }
        public static void ShowError(Exception ex) => ShowError(null, ex);
        public static void ShowError(string errorMessage) => ShowError(errorMessage, null);
        public static void ShowError(string msg, Exception ex)
        {
            Instance.LogInstance.Error($"Exception from {Thread.CurrentThread.Name}, ID: " +
                $"{Thread.CurrentThread.ManagedThreadId.ToString(CultureInfo.InvariantCulture)}");
            string errorMessage = msg ?? "An Error has occured.! ";
            if (ex != null)
            {
                errorMessage = $"{errorMessage}. {ex.GetStringFromException()}";
                Instance.LogInstance.Error(ex);
            }
            Console.WriteLine(errorMessage);
            Instance.ShowErrorPopup(errorMessage);
        }
        public static void HandleUnhandledException(string source, Exception exception) => ShowError($"Unhandled exception ({source})", exception);
        public static void Info(string msg, bool writeToLog = true)
        {
            string message = $"{msg} from {Thread.CurrentThread.Name}, ID: " +
                $"{Thread.CurrentThread.ManagedThreadId.ToString(CultureInfo.InvariantCulture)}";
            if (writeToLog)
            {
                Instance.LogInstance.Info(message);
            }
            Console.WriteLine(message);
        }
        public static void Error(Exception ex)
        {
            Instance.LogInstance.Error($"Exception from {Thread.CurrentThread.Name}, ID: " +
                $"{Thread.CurrentThread.ManagedThreadId.ToString(CultureInfo.InvariantCulture)}");
            Instance.LogInstance.Error(ex);
        }
        public static DialogResult ShowMessage(string messageBoxText, string caption,
            MessageBoxButtons button, MessageBoxIcon icon) => Instance.CurrentApplication.Dispatcher.Invoke(delegate
        {
            return MessageBox.ShowMessageBox(messageBoxText, caption, button, icon);
        });
        public static DialogResult ShowSuccessMessage(string msg)
        {
            Instance.CurrentApplication?.Dispatcher?.Invoke(delegate
            {
                return MessageBox.ShowSuccessMessageBox(msg);
            });
            return DialogResult.Cancel;
        }
    }
}
