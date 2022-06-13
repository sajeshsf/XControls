using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace XControls.Mvvm
{
    public class Bindable : ObservableObject
    {
        public void InvokePropertyChanged(string? propertyName) => OnPropertyChanged(propertyName);
    }
}
