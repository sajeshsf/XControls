using MahApps.Metro.ValueBoxes;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace UserControls.NotificationBanner
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

        private void ItemCloseButton_Click(object sender, RoutedEventArgs e) => NotificationCollection.Remove((sender as Button).DataContext as Notification);

        public bool BaseTextVisibility => NotificationCollection?.Count() == 0;

        public static readonly DependencyProperty IsFlyoutOpenProperty =
            DependencyProperty.Register(nameof(IsFlyoutOpen), typeof(bool), typeof(NotificationFlyout), new PropertyMetadata(default(bool), OnIsFyloutOpenPropertyChanged));

        private static void OnIsFyloutOpenPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) => (d as NotificationFlyout).OnIsFyloutOpenPropertyChanged();

        private void OnIsFyloutOpenPropertyChanged() => UpdateTime();

        private void UpdateTime()
        {
            if (IsFlyoutOpen)
            {
                foreach (var item in NotificationCollection)
                {
                    item.OnPropertyChanged(nameof(item.TimeStamp));
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
             DependencyPropertyChangedEventArgs e) => (dependencyObject as NotificationFlyout).OnNotificationCollectionChanged();

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
