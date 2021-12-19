using System;
using System.ComponentModel;

namespace UserControls.NotificationBanner
{
    public class Notification : INotifyPropertyChanged
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public DateTime TimeStamp { get; set; }
        public NotificationType Type { get; set; }
        public Notification(string message)
        {
            Title = string.Empty;
            Message = message;
            TimeStamp = DateTime.Now;
        }

        public Notification(string title, string message, DateTime timeStamp, NotificationType type)
        {
            Title = title;
            Message = message;
            TimeStamp = timeStamp;
            Type = type;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
