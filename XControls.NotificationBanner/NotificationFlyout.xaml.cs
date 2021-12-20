using MahApps.Metro.ValueBoxes;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace XControls.NotificationBanner
{
    /// <summary>
    /// Interaction logic for NotificationFlyout.xaml
    /// </summary>
    public partial class NotificationFlyout : UserControl, INotifyPropertyChanged
    {
        public NotificationFlyout()
        {
            InitializeComponent();
        }

        private void OnNotificationCollectionCollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged(nameof(BaseTextVisibility));
            UpdateTime();
        }

        private void ItemCloseButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn)
            {
                if (btn.DataContext is Notification item)
                {
                    NotificationCollection.Remove(item);
                }
            }
        }

        public bool BaseTextVisibility => NotificationCollection?.Count == 0;

        public static readonly DependencyProperty IsFlyoutOpenProperty =
            DependencyProperty.Register(nameof(IsFlyoutOpen), typeof(bool), typeof(NotificationFlyout), new PropertyMetadata(default(bool), OnIsFyloutOpenPropertyChanged));

        private static void OnIsFyloutOpenPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is NotificationFlyout nf)
            {
                nf.OnIsFyloutOpenPropertyChanged();
            }
        }

        private void OnIsFyloutOpenPropertyChanged() => UpdateTime();

        private void UpdateTime()
        {
            if (IsFlyoutOpen)
            {
                foreach (var item in NotificationCollection)
                {
                    item.InvokePropertyChanged(nameof(item.TimeStamp));
                }
            }
        }

        public bool IsFlyoutOpen
        {
            get
            {
                return (bool)this.GetValue(IsFlyoutOpenProperty);
            }
            set
            {
                this.SetValue(IsFlyoutOpenProperty, BooleanBoxes.Box(value));
            }
        }

        public static readonly DependencyProperty NotificationCollectionProperty =
           DependencyProperty.Register(nameof(NotificationCollection), typeof(ObservableCollection<Notification>), typeof(NotificationFlyout),
               new PropertyMetadata(default(ObservableCollection<Notification>), OnNotificationCollectionChanged));

        private static void OnNotificationCollectionChanged(DependencyObject dependencyObject,
             DependencyPropertyChangedEventArgs e)
        {
            if (dependencyObject is NotificationFlyout nf)
            {
                nf.OnNotificationCollectionChanged();
            }
        }

        private void OnNotificationCollectionChanged() => NotificationCollection.CollectionChanged += OnNotificationCollectionCollectionChanged;

        public ObservableCollection<Notification> NotificationCollection
        {
            get
            {
                return (ObservableCollection<Notification>)GetValue(NotificationCollectionProperty);
            }
            set
            {
                SetValue(NotificationCollectionProperty, value);
            }
        }
        private void ClearButton_Click(object sender, RoutedEventArgs e) => NotificationCollection.Clear();

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    }
}
