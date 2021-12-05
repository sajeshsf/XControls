using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Globalization;
using System.Windows;

namespace XControls.Utilities
{
    public class Bindable : INotifyPropertyChanged
    {
        public bool ThrowOnInvalidPropertyName { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        public Bindable()
        {
        }
        private void ReportError(string propertyName)
        {
            string msg = $"{GetType().Name} does not contain property: {propertyName}";
            if (ThrowOnInvalidPropertyName)
            {
                throw new ArgumentNullException(msg);
            }
            else
            {
                Debug.Fail(msg);
            }
        }
        [Conditional("DEBUG")]
        private void VerifyProperty(string propertyName)
        {
            if (TypeDescriptor.GetProperties(this)[propertyName] == null)
            {
                ReportError(propertyName);
            }
        }
        [Conditional("DEBUG")]
        private void VerifyProperty(int propertyValue)
        {
            if (TypeDescriptor.GetProperties(this)[propertyValue] == null)
            {
                ReportError(propertyValue.ToString());
            }
        }
        protected void OnPropertyChanged(string propertyName, object sender = null) => PropertyChanged?.Invoke(sender ?? this, new PropertyChangedEventArgs(propertyName));
        protected bool SetProperty<T>(ref T field, T value, [CallerMemberName] string propertyName = null) where T : struct
        {
            //VerifyProperty(propertyName);
            if (!System.Collections.Generic.EqualityComparer<T>.Default.Equals(field, value))
            {
                field = value;
                OnPropertyChanged(propertyName);
                return true;
            }
            return false;
        }
        public void InvokePropertyChanged([CallerMemberName] string propertyName = null, object sender = null) => OnPropertyChanged(propertyName, sender);
    }
}
