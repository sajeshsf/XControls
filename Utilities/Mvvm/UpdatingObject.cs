using System;
using System.Windows.Threading;
using System.Collections.ObjectModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;

namespace XControls.Utilities.Mvvm
{
    public abstract class UpdatingObject : ObservableObject
    {
        private readonly DispatcherTimer dispatcherTimer;
        //
        // Summary:
        //     Gets or sets a value that indicates whether the timer is running.
        //
        // Returns:
        //     true if the timer is enabled; otherwise, false. The default is false.
        public bool IsEnabled { get => dispatcherTimer.IsEnabled; set => dispatcherTimer.IsEnabled = value; }
        //
        // Summary:
        //     Gets or sets the period of time between timer ticks.
        //
        // Returns:
        //     The period of time between ticks. The default is 00:00:00.
        //
        // Exceptions:
        //   T:System.ArgumentOutOfRangeException:
        //     interval is less than 0 or greater than System.Int32.MaxValue milliseconds.
        public TimeSpan Interval { get => dispatcherTimer.Interval; set => dispatcherTimer.Interval = value; }
        public Collection<string> PropertyNames { get; private set; }
        public UpdatingObject()
        {
            PropertyNames = new Collection<string>();
            dispatcherTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            dispatcherTimer.Tick += Timer_Tick;
            dispatcherTimer.Start();
        }
        protected virtual void Timer_Tick(object? sender, EventArgs e) => PropertyNames.ToList().ForEach(x => OnPropertyChanged(x));
    }
}
