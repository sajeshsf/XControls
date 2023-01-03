using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using XControls.Controls.MessageBanner;

namespace XControls.MessageBanner
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    internal partial class MessagePopup : Window
    {
        private const string BlueColorCode = "#FF1096eb";
        private const string GreenColorCode = "#248f24";
        private const string RedColorCode = "#FFe57373";
        private const string YellowColorCode = "#FFd1b515";
        private MessageBoxButtons button;
        private MessageBoxIcon image;
        public string MessageCaption
        {
            get => MessageCaptionTextBlock.Text;
            set => MessageCaptionTextBlock.Text = value;
        }
        public string MessageBoxContent
        {
            get => MessageContentTextBlock.Text;
            set => MessageContentTextBlock.Text = value;
        }
        public DialogResult Result { get; set; }
        public MessageBoxButtons Button
        {
            get => button;
            set
            {
                button = value;
                SetVisibilityOfButtons(value);
            }
        }
        public MessageBoxIcon Image
        {
            get => image;
            set
            {
                image = value;
                SetMessageBoxImage(value);
            }
        }
        public string ImagePath { get; set; }
        public MessagePopup()
        {
            InitializeComponent();
            ImagePath = "Images/Error.png";
        }
        private Brush SetBackgroudColor(string colorCode) => (Brush)new BrushConverter().ConvertFromString(colorCode);
        private void SetMessageBoxImage(MessageBoxIcon messageBoxImage)
        {
            switch (messageBoxImage)
            {
                case MessageBoxIcon.None:
                    break;

                case MessageBoxIcon.Asterisk:
                case MessageBoxIcon.Information:
                    ImagePath = "Images/information.png";
                    MessageBoxBorder.Background = SetBackgroudColor(BlueColorCode);
                    OkButton.Background = SetBackgroudColor(BlueColorCode);
                    YesButton.Background = SetBackgroudColor(BlueColorCode);
                    NoButton.Background = SetBackgroudColor(BlueColorCode);
                    CancelButton.Background = SetBackgroudColor(BlueColorCode);
                    // ProgressRing.Visibility = Visibility.Collapsed;
                    break;

                case MessageBoxIcon.Error:
                case MessageBoxIcon.Hand:
                case MessageBoxIcon.Stop:
                    ImagePath = "Images/Error.png";
                    MessageBoxBorder.Background = SetBackgroudColor(RedColorCode);
                    OkButton.Background = SetBackgroudColor(RedColorCode);
                    YesButton.Background = SetBackgroudColor(RedColorCode);
                    NoButton.Background = SetBackgroudColor(RedColorCode);
                    CancelButton.Background = SetBackgroudColor(RedColorCode);
                    // ProgressRing.Visibility = Visibility.Collapsed;
                    break;

                case MessageBoxIcon.Exclamation:
                case MessageBoxIcon.Question:
                case MessageBoxIcon.Warning:
                    ImagePath = "Images/warning.png";
                    MessageBoxBorder.Background = SetBackgroudColor(YellowColorCode);
                    OkButton.Background = SetBackgroudColor(YellowColorCode);
                    YesButton.Background = SetBackgroudColor(YellowColorCode);
                    NoButton.Background = SetBackgroudColor(YellowColorCode);
                    CancelButton.Background = SetBackgroudColor(YellowColorCode);
                    // ProgressRing.Visibility = Visibility.Collapsed;
                    break;
                case MessageBoxIcon.Success:
                    ImagePath = "Images/success.png";
                    MessageBoxBorder.Background = SetBackgroudColor(GreenColorCode);
                    OkButton.Background = SetBackgroudColor(GreenColorCode);
                    YesButton.Background = SetBackgroudColor(GreenColorCode);
                    NoButton.Background = SetBackgroudColor(GreenColorCode);
                    CancelButton.Background = SetBackgroudColor(GreenColorCode);
                    // ProgressRing.Visibility = Visibility.Collapsed;
                    break;
                case MessageBoxIcon.Status:
                    // Icon.Visibility = Visibility.Hidden;
                    MessageBoxBorder.Background = SetBackgroudColor(BlueColorCode);
                    OkButton.Background = SetBackgroudColor(BlueColorCode);
                    YesButton.Background = SetBackgroudColor(BlueColorCode);
                    NoButton.Background = SetBackgroudColor(BlueColorCode);
                    CancelButton.Background = SetBackgroudColor(BlueColorCode);
                    // ProgressRing.Visibility = Visibility.Visible;
                    break;
                default:
                    break;
            }
        }
        private void SetVisibilityOfButtons(MessageBoxButtons msgBoxButton)
        {
            switch (msgBoxButton)
            {
                case MessageBoxButtons.Ok:
                    CancelButton.Visibility = Visibility.Collapsed;
                    NoButton.Visibility = Visibility.Collapsed;
                    YesButton.Visibility = Visibility.Collapsed;
                    OkButton.Focus();
                    break;
                case MessageBoxButtons.Cancel:
                    OkButton.Visibility = Visibility.Collapsed;
                    NoButton.Visibility = Visibility.Collapsed;
                    YesButton.Visibility = Visibility.Collapsed;
                    CancelButton.Focus();
                    break;
                case MessageBoxButtons.OkCancel:
                    NoButton.Visibility = Visibility.Collapsed;
                    YesButton.Visibility = Visibility.Collapsed;
                    CancelButton.Focus();
                    break;
                case MessageBoxButtons.YesNo:
                    OkButton.Visibility = Visibility.Collapsed;
                    CancelButton.Visibility = Visibility.Collapsed;
                    NoButton.Focus();
                    break;
                case MessageBoxButtons.YesNoCancel:
                    OkButton.Visibility = Visibility.Collapsed;
                    CancelButton.Focus();
                    break;
                default:
                    break;
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (sender == OkButton)
                Result = Controls.MessageBanner.DialogResult.Ok;
            else if (sender == YesButton)
                Result = Controls.MessageBanner.DialogResult.Yes;
            else if (sender == NoButton)
                Result = Controls.MessageBanner.DialogResult.No;
            else if (sender == CancelButton)
                Result = Controls.MessageBanner.DialogResult.Cancel;
            else
                Result = Controls.MessageBanner.DialogResult.None;
            Close();
            Application.Current.MainWindow.Activate();
        }
        private void EnterClicked(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                e.Handled = true;
                Close();
                Application.Current.MainWindow.Activate();
            }
        }
    }
}
