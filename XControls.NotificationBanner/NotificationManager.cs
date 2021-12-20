using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Threading;
using System.Windows.Threading;
using XControls.Helpers.Extensions;
using XControls.Loader;
using XControls.MessageBanner;
using XControls.Utilities;

namespace XControls.NotificationBanner
{
    //public class NotificationManager : LocalisationBase
    //{
    //    private static NotificationManager? _Instance;
    //    public static NotificationManager Instance => _Instance ??= new NotificationManager();
    //    private NLog.Logger LogInstance { get; set; }
    //    public System.Windows.Application CurrentApplication { get; set; }
    //    public bool ShowTraceMessage { get; set; }
    //    public ObservableCollection<Notification> NotificationList { get; private set; }
    //    public int NewMessageCount { get; set; }
    //    private NotificationManager()
    //    {
    //        CurrentApplication ??= System.Windows.Application.Current;
    //        NotificationList = new ObservableCollection<Notification>();
    //        //CurrentApplication.MainWindow.ContentRendered += OnContentRendered;
    //        //CurrentApplication.MainWindow.Closed += OnMainWindowClosed;
    //    }
    //    private void OnMainWindowClosed(object sender, EventArgs e) => NotificationList.Clear();
    //    private void OnContentRendered(object sender, EventArgs e) => ShowPendingErrors();
    //    private void ConsoleWriteLine(string msg)
    //    {
    //        BeginDispatcherOperation(delegate
    //        {
    //            if (CurrentApplication?.MainWindow?.IsLoaded == true)
    //            {
    //                Console.WriteLine(msg);
    //            }
    //        });
    //    }
    //    public TResult DispatcherOperation<TResult>(Func<TResult> callback) => CurrentApplication.Dispatcher.Invoke(callback);
    //    public DispatcherOperation BeginDispatcherOperation(Action method, DispatcherPriority priority = DispatcherPriority.Render) =>
    //        CurrentApplication.Dispatcher.BeginInvoke(priority, method);
    //    public void ShowErrorPopup(string errorMessage) => DispatcherOperation(delegate
    //    {
    //        AddNewNotification(errorMessage);
    //        Console.WriteLine(errorMessage);
    //        if (CurrentApplication?.MainWindow?.IsLoaded == true)
    //        {
    //            string caption = $"{GetText("Error")}";
    //            var result = MessageBox.ShowMessageBox(CurrentApplication.MainWindow, caption,
    //                errorMessage, MessageBoxButtons.Ok, MessageBoxIcon.Error, DialogResult.Ok);
    //        }
    //        return true;
    //    });
    //    public void ShowPendingErrors()
    //    {
    //        if (NotificationList.Count > 0)
    //        {
    //            //var resultantErrorString = string.Join(Environment.NewLine, NotificationList);
    //            //NotificationList.ForEach(x => ReadPositionAndStatus(x));
    //            var errorString = string.Empty;
    //            foreach (var notification in NotificationList)
    //            {
    //                errorString += notification.Message + Environment.NewLine;
    //            }
    //            DispatcherOperation(delegate
    //            {
    //                return MessageBox.ShowMessageBox(CurrentApplication.MainWindow, $"{GetText("Error", CultureInfo.CurrentUICulture)}",
    //                    errorString, MessageBoxButtons.Ok, MessageBoxIcon.Error, DialogResult.Ok);
    //            });
    //        }
    //    }
    //    public void AddNewNotification(string notification)
    //    {
    //        NewMessageCount++;
    //        NotificationList.Add(new Notification(notification));
    //        while (NotificationList.Count > 25)
    //        {
    //            NotificationList.RemoveAt(0);
    //            if (NewMessageCount >= 25)
    //            {
    //                NewMessageCount--;
    //            }
    //        }
    //    }
    //    public static void Error(Exception ex, bool showPopup = false) => Error(null, ex, showPopup);
    //    public static void Error(string? msg = null, Exception? ex = null, bool showPopup = true)
    //    {
    //        Instance.LogInstance.Error($"Exception from {Thread.CurrentThread.Name}, " +
    //            $"ID: {Thread.CurrentThread.ManagedThreadId.ToString(CultureInfo.InvariantCulture)}");
    //        var errorMessage = msg ?? "An Error has occured.! ";
    //        if (ex != null)
    //        {
    //            errorMessage = $"{errorMessage}. {ex.GetStringFromException()}";
    //            Instance.LogInstance.Error(errorMessage);
    //            Instance.LogInstance.Error(ex);
    //        }
    //        if (showPopup)
    //        {
    //            Instance.ShowErrorPopup(errorMessage.ToString(CultureInfo.CurrentUICulture));
    //        }
    //    }
    //    public static void Trace(string msg, bool writeToLog = false)
    //    {
    //        if (Instance.ShowTraceMessage)
    //        {
    //            Instance.ConsoleWriteLine(msg);
    //        }
    //        if (writeToLog)
    //        {
    //            Instance.LogInstance.Trace($"{msg} from {Thread.CurrentThread.Name}, ID: {Thread.CurrentThread.ManagedThreadId.ToString(CultureInfo.InvariantCulture)}");

    //        }
    //    }
    //    public static void Info(string msg, bool writeToLog = true)
    //    {
    //        Instance.ConsoleWriteLine(msg);
    //        if (writeToLog)
    //        {
    //            Instance.LogInstance.Info($"{msg} from {Thread.CurrentThread.Name}, ID: {Thread.CurrentThread.ManagedThreadId.ToString(CultureInfo.InvariantCulture)}");
    //        }
    //    }
    //    public static DialogResult ShowMessage(string messageBoxText, string caption, MessageBoxButtons button, MessageBoxIcon icon) =>
    //        Instance.DispatcherOperation(delegate
    //        {
    //            return MessageBox.ShowMessageBox(messageBoxText, caption, button, icon);
    //        });
    //    public static DialogResult ShowSuccessMessage(string msg)
    //    {
    //        Instance.DispatcherOperation(delegate
    //        {
    //            return MessageBox.ShowSuccessMessageBox(msg);
    //        });
    //        return DialogResult.Cancel;
    //    }
    //    public static void CloseApplication() => Instance.DispatcherOperation(delegate
    //    {
    //        var loader = VerticalLoader.Show();
    //        try
    //        {
    //            Instance.CurrentApplication?.Shutdown();
    //        }
    //        finally
    //        {
    //            loader?.Hide();
    //        }
    //        return true;
    //    });
    //}
}
