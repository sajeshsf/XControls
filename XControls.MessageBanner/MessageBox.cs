using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Security;
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

namespace XControls.MessageBanner
{
    //
    // Summary:
    //     Displays a message box.
    public sealed class MessageBox
    {
        private const string SucessBoxCaption = "SUCCESS";
        private const string MsgBoxCaption = "Message Box";
        private MessagePopup MessagePopup { get; set; }
        public string Content
        {
            get => MessagePopup.MessageBoxContent;
            set => MessagePopup.MessageBoxContent = value;
        }
        public string Caption
        {
            get => MessagePopup.MessageCaption;
            set => MessagePopup.MessageCaption = value;
        }
        private MessageBox()
        {
            //MessagePopup = new MessagePopup();
        }
        private DialogResult ShowPopUpWindow(Window owner, string caption, string text, MessageBoxButtons button, MessageBoxIcon img, DialogResult defaultResult)
        {
            Window trueOwner = owner;
            if ((owner?.IsLoaded) != true)
            {
                trueOwner = new Window()
                {
                    Visibility = Visibility.Hidden,
                    AllowsTransparency = true,                // Just hiding the window is not sufficient, as it still temporarily pops up the first time. Therefore, make it transparent.
                    Background = Brushes.Transparent,
                    WindowStyle = WindowStyle.None,
                    ShowInTaskbar = false
                };
                trueOwner.Show();
            }
            MessagePopup = new MessagePopup()
            {
                Owner = trueOwner,
                MessageCaption = caption,
                MessageBoxContent = text,
                Result = defaultResult,
                Button = button,
                Image = img
            };
            MessagePopup.ShowDialog();
            if ((owner?.IsLoaded) != true)
            {
                trueOwner.Hide();
            }
            return MessagePopup.Result;
        }
        public void Show() => MessagePopup.Show();
        public void Hide() => MessagePopup.Show();
        public static MessageBox ShowPopup(Window owner, string caption, string text, MessageBoxButtons button, MessageBoxIcon img, DialogResult defaultResult)
        {
            var tmp = new MessageBox();
            tmp.ShowPopUpWindow(owner, caption, text, button, img, defaultResult);
            return tmp;
        }
        //
        // Summary:
        //     Displays a message box that has a message and that returns a result.
        //
        // Parameters:
        //   messageBoxText:
        //     A System.String that specifies the text to display.
        //
        // Returns:
        //     A System.Windows.MessageBoxResult value that specifies which message box button
        //     is clicked by the user.
        [SecurityCritical]
        public static DialogResult ShowSuccessMessageBox(string messageBoxText) => ShowMessageBox(Application.Current.MainWindow, SucessBoxCaption,
            messageBoxText, MessageBoxButtons.Ok, MessageBoxIcon.Success, DialogResult.Ok);
        //
        // Summary:
        //     Displays a message box that has a message and that returns a result.
        //
        // Parameters:
        //   title:
        //     A System.String that specifies the title bar caption to display.
        //
        //   messageBoxText:
        //     A System.String that specifies the text to display.
        //     
        // Returns:
        //     A System.Windows.MessageBoxResult value that specifies which message box button
        //     is clicked by the user.
        [SecurityCritical]
        public static DialogResult ShowStatusMessageBox(string title, string messageBoxText) // Set parameter title as the title 
        {
            // implement cancel button
            return ShowMessageBox(Application.Current.MainWindow, title, messageBoxText, MessageBoxButtons.Cancel,
                MessageBoxIcon.Status, DialogResult.Cancel);
        }
        public static MessageBox ShowPopup(string title, string messageBoxText) => ShowPopup(Application.Current.MainWindow, title, 
            messageBoxText, MessageBoxButtons.Cancel, MessageBoxIcon.Status, DialogResult.Cancel);
        //
        // Summary:
        //     Displays a message box with no Owner and has a message and title bar caption; and that returns
        //     a result.
        //
        // Parameters:
        //   caption:
        //     A System.String that specifies the title bar caption to display.
        //
        //   messageBoxText:
        //     A System.String that specifies the text to display.
        //
        // Returns:
        //     A System.Windows.MessageBoxResult value that specifies which message box button
        //     is clicked by the user.
        [SecurityCritical]
        public static DialogResult ShowFloatingMessageBox(string caption, string messageBoxText)
        {
            Window window = new Window()
            {
                Visibility = Visibility.Hidden,
                AllowsTransparency = true, // Just hiding the window is not sufficient, as it still temporarily pops up the first time. Therefore, make it transparent.
                Background = System.Windows.Media.Brushes.Transparent,
                WindowStyle = WindowStyle.None,
                ShowInTaskbar = false
            };
            window.Show();
            DialogResult result = new MessageBox().ShowPopUpWindow(window, caption, messageBoxText, MessageBoxButtons.Ok, MessageBoxIcon.Error, DialogResult.None);
            window.Hide();
            return result;
        }
        //
        // Summary:
        //     Displays a message box in front of the specified window. The message box displays
        //     a message, title bar caption, button, and icon; and accepts a default message
        //     box result and returns a result.
        //
        // Parameters:
        //   owner:
        //     A System.Windows.Window that represents the owner window of the message box.
        //
        //   messageBoxText:
        //     A System.String that specifies the text to display.
        //
        //   caption:
        //     A System.String that specifies the title bar caption to display.
        //
        //   button:
        //     A System.Windows.MessageBoxButton value that specifies which button or buttons
        //     to display.
        //
        //   icon:
        //     A System.Windows.MessageBoxImage value that specifies the icon to display.
        //
        //   defaultResult:
        //     A System.Windows.MessageBoxResult value that specifies the default result of
        //     the message box.
        //
        // Returns:
        //     A System.Windows.MessageBoxResult value that specifies which message box button
        //     is clicked by the user.
        [SecurityCritical]
        public static DialogResult ShowMessageBox(Window owner, string caption, string messageBoxText, MessageBoxButtons button, MessageBoxIcon icon, DialogResult defaultResult) =>
            new MessageBox().ShowPopUpWindow(owner, caption, messageBoxText, button, icon, defaultResult);
        //
        // Summary:
        //     Displays a message box that has a message and that returns a result.
        //
        // Parameters:
        //   messageBoxText:
        //     A System.String that specifies the text to display.
        //
        // Returns:
        //     A System.Windows.MessageBoxResult value that specifies which message box button
        //     is clicked by the user.
        [SecurityCritical]
        public static DialogResult ShowMessageBox(string messageBoxText) => new MessageBox().ShowPopUpWindow(Application.Current.MainWindow, MsgBoxCaption, 
            messageBoxText, MessageBoxButtons.Ok, MessageBoxIcon.Status, DialogResult.None);
        //
        // Summary:
        //     Displays a message box that has a message and title bar caption; and that returns
        //     a result.
        //
        // Parameters:
        //   messageBoxText:
        //     A System.String that specifies the text to display.
        //
        //   caption:
        //     A System.String that specifies the title bar caption to display.
        //
        // Returns:
        //     A System.Windows.MessageBoxResult value that specifies which message box button
        //     is clicked by the user.
        //[SecurityCritical]
        //public static XMessageBoxResult ShowMessageBox(string messageBoxText, string caption) => new MessageBox().ShowPopUpWindow(Application.Current.MainWindow,
        //    caption, messageBoxText, XMessageBoxButton.Ok, XMessageBoxImage.None, XMessageBoxResult.None);
        ////
        //// Summary:
        ////     Displays a message box in front of the specified window. The message box displays
        ////     a message and returns a result.
        ////
        //// Parameters:
        ////   owner:
        ////     A System.Windows.Window that represents the owner window of the message box.
        ////
        ////   messageBoxText:
        ////     A System.String that specifies the text to display.
        ////
        //// Returns:
        ////     A System.Windows.MessageBoxResult value that specifies which message box button
        ////     is clicked by the user.
        //[SecurityCritical]
        //public static XMessageBoxResult ShowMessageBox(Window owner, string messageBoxText) => new MessageBox().ShowPopUpWindow(owner,
        //    new ResourceManager(typeof(Resources)).GetString("ID_XMESSAGEBOXCAPTION"), messageBoxText,
        //    XMessageBoxButton.Ok, XMessageBoxImage.None, XMessageBoxResult.None);
        ////
        //// Summary:
        ////     Displays a message box that has a message, title bar caption, and button; and
        ////     that returns a result.
        ////
        //// Parameters:
        ////   messageBoxText:
        ////     A System.String that specifies the text to display.
        ////
        ////   caption:
        ////     A System.String that specifies the title bar caption to display.
        ////
        ////   button:
        ////     A System.Windows.MessageBoxButton value that specifies which button or buttons
        ////     to display.
        ////
        //// Returns:
        ////     A System.Windows.MessageBoxResult value that specifies which message box button
        ////     is clicked by the user.
        //[SecurityCritical]
        //public static XMessageBoxResult ShowMessageBox(string messageBoxText, string caption, XMessageBoxButton button) =>
        //    ShowMessageBox(Application.Current.MainWindow, caption, messageBoxText, button, XMessageBoxImage.None, XMessageBoxResult.None);
        ////
        //// Summary:
        ////     Displays a message box in front of the specified window. The message box displays
        ////     a message and title bar caption; and it returns a result.
        ////
        //// Parameters:
        ////   owner:
        ////     A System.Windows.Window that represents the owner window of the message box.
        ////
        ////   messageBoxText:
        ////     A System.String that specifies the text to display.
        ////
        ////   caption:
        ////     A System.String that specifies the title bar caption to display.
        ////
        //// Returns:
        ////     A System.Windows.MessageBoxResult value that specifies which message box button
        ////     is clicked by the user.
        //[SecurityCritical]
        //public static XMessageBoxResult ShowMessageBox(Window owner, string messageBoxText, string caption) =>
        //    ShowMessageBox(owner, caption, messageBoxText, XMessageBoxButton.Ok, XMessageBoxImage.None, XMessageBoxResult.None);
        //
        // Summary:
        //     Displays a message box that has a message, title bar caption, button, and icon;
        //     and that returns a result.
        //
        // Parameters:
        //   messageBoxText:
        //     A System.String that specifies the text to display.
        //
        //   caption:
        //     A System.String that specifies the title bar caption to display.
        //
        //   button:
        //     A System.Windows.MessageBoxButton value that specifies which button or buttons
        //     to display.
        //
        //   icon:
        //     A System.Windows.MessageBoxImage value that specifies the icon to display.
        //
        // Returns:
        //     A System.Windows.MessageBoxResult value that specifies which message box button
        //     is clicked by the user.
        [SecurityCritical]
        public static DialogResult ShowMessageBox(string messageBoxText, string caption, MessageBoxButtons button, MessageBoxIcon icon) =>
            ShowMessageBox(Application.Current.MainWindow, caption, messageBoxText, button, icon, DialogResult.None);
        ////
        //// Summary:
        ////     Displays a message box in front of the specified window. The message box displays
        ////     a message, title bar caption, and button; and it also returns a result.
        ////
        //// Parameters:
        ////   owner:
        ////     A System.Windows.Window that represents the owner window of the message box.
        ////
        ////   messageBoxText:
        ////     A System.String that specifies the text to display.
        ////
        ////   caption:
        ////     A System.String that specifies the title bar caption to display.
        ////
        ////   button:
        ////     A System.Windows.MessageBoxButton value that specifies which button or buttons
        ////     to display.
        ////
        //// Returns:
        ////     A System.Windows.MessageBoxResult value that specifies which message box button
        ////     is clicked by the user.
        //[SecurityCritical]
        //public static XMessageBoxResult ShowMessageBox(Window owner, string messageBoxText, string caption, XMessageBoxButton button) =>
        //    ShowMessageBox(owner, caption, messageBoxText, button, XMessageBoxImage.None, XMessageBoxResult.None);
        //
        // Summary:
        //     Displays a message box that has a message, title bar caption, button, and icon;
        //     and that accepts a default message box result and returns a result.
        //
        // Parameters:
        //   messageBoxText:
        //     A System.String that specifies the text to display.
        //
        //   caption:
        //     A System.String that specifies the title bar caption to display.
        //
        //   button:
        //     A System.Windows.MessageBoxButton value that specifies which button or buttons
        //     to display.
        //
        //   icon:
        //     A System.Windows.MessageBoxImage value that specifies the icon to display.
        //
        //   defaultResult:
        //     A System.Windows.MessageBoxResult value that specifies the default result of
        //     the message box.
        //
        // Returns:
        //     A System.Windows.MessageBoxResult value that specifies which message box button
        //     is clicked by the user.
        [SecurityCritical]
        public static DialogResult ShowMessageBox(string messageBoxText, string caption, MessageBoxButtons button, MessageBoxIcon icon, DialogResult defaultResult) =>
            ShowMessageBox(Application.Current.MainWindow, caption, messageBoxText, button, icon, defaultResult);
        //
        // Summary:
        //     Displays a message box that has a message and that returns a result.
        //
        // Parameters:
        //   messageBoxText:
        //     A System.String that specifies the text to display.
        //
        // Returns:
        //     A System.Windows.MessageBoxResult value that specifies which message box button
        //     is clicked by the user.
        //[SecurityCritical]
        //public static XMessageBoxResult ShowStatusMessageBox(string messageBoxText) => new MessageBox().ShowPopUpWindow(Application.Current.MainWindow,
        //new ResourceManager(typeof(Resources)).GetString("ID_STATUSBOXCAPTION"),
        //messageBoxText,
        //XMessageBoxButton.Ok,
        //XMessageBoxImage.Status,
        //XMessageBoxResult.Ok);
        //
        // Summary:
        //     Displays a message box in front of the specified window. The message box displays
        //     a message, title bar caption, button, and icon; and accepts a default message
        //     box result, complies with the specified options, and returns a result.
        //
        // Parameters:
        //   owner:
        //     A System.Windows.Window that represents the owner window of the message box.
        //
        //   messageBoxText:
        //     A System.String that specifies the text to display.
        //
        //   caption:
        //     A System.String that specifies the title bar caption to display.
        //
        //   button:
        //     A System.Windows.MessageBoxButton value that specifies which button or buttons
        //     to display.
        //
        //   icon:
        //     A System.Windows.MessageBoxImage value that specifies the icon to display.
        //
        //   defaultResult:
        //     A System.Windows.MessageBoxResult value that specifies the default result of
        //     the message box.
        //
        //   options:
        //     A System.Windows.MessageBoxOptions value object that specifies the options.
        //
        // Returns:
        //     A System.Windows.MessageBoxResult value that specifies which message box button
        //     is clicked by the user.
        // [SecurityCritical]
        //public static MessageBoxResult Show(Window owner, string messageBoxText, string caption, MessageBoxButton button, MessageBoxImage icon, MessageBoxResult defaultResult, MessageBoxOptions options)
        //{
        //    MessageBoxResult result = MessageBoxResult.Cancel;
        //    return result;
        //}

        //
        // Summary:
        //     Displays a message box in front of the specified window. The message box displays
        //     a message, title bar caption, button, and icon; and it also returns a result.
        //
        // Parameters:
        //   owner:
        //     A System.Windows.Window that represents the owner window of the message box.
        //
        //   messageBoxText:
        //     A System.String that specifies the text to display.
        //
        //   caption:
        //     A System.String that specifies the title bar caption to display.
        //
        //   button:
        //     A System.Windows.MessageBoxButton value that specifies which button or buttons
        //     to display.
        //
        //   icon:
        //     A System.Windows.MessageBoxImage value that specifies the icon to display.
        //
        // Returns:
        //     A System.Windows.MessageBoxResult value that specifies which message box button
        //     is clicked by the user.
        //[SecurityCritical]
        //public static XMessageBoxResult ShowMessageBox(Window owner, string messageBoxText, string caption, XMessageBoxButton button, XMessageBoxImage icon) =>
        //    ShowMessageBox(owner, caption, messageBoxText, button, icon, XMessageBoxResult.None);
        //
        // Summary:
        //     Displays a message box that has a message, title bar caption, button, and icon;
        //     and that accepts a default message box result, complies with the specified options,
        //     and returns a result.
        //
        // Parameters:
        //   messageBoxText:
        //     A System.String that specifies the text to display.
        //
        //   caption:
        //     A System.String that specifies the title bar caption to display.
        //
        //   button:
        //     A System.Windows.MessageBoxButton value that specifies which button or buttons
        //     to display.
        //
        //   icon:
        //     A System.Windows.MessageBoxImage value that specifies the icon to display.
        //
        //   defaultResult:
        //     A System.Windows.MessageBoxResult value that specifies the default result of
        //     the message box.
        //
        //   options:
        //     A System.Windows.MessageBoxOptions value object that specifies the options.
        //
        // Returns:
        //     A System.Windows.MessageBoxResult value that specifies which message box button
        //     is clicked by the user.
        //[SecurityCritical]
        // public static MessageBoxResult Show(string messageBoxText, string caption, MessageBoxButton button, MessageBoxImage icon, MessageBoxResult defaultResult, MessageBoxOptions options)
        //{
        //    MessageBoxResult result = MessageBoxResult.Cancel;
        //    return result;
        //}
        //
    }
}
