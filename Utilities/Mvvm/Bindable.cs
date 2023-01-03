using CommunityToolkit.Mvvm.ComponentModel;

namespace XControls.Utilities.Mvvm
{
    public class Bindable : ObservableObject
    {
        public void InvokePropertyChanged(string? propertyName) => OnPropertyChanged(propertyName);
    }
}
