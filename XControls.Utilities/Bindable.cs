using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace XControls.Utilities
{
    public class Bindable : ObservableObject
    {
        public void InvokePropertyChanged(string? propertyName) => OnPropertyChanged(propertyName);
    }
}
