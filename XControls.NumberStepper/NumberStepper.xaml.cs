using System;
using System.ComponentModel;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace XControls.NumberStepper
{
    /// <summary>
    /// Interaction logic for NumberStepper.xaml
    /// </summary>
    public partial class NumberStepper : UserControl
    {
        private double incrementValue = 1;
        private Timer mousePressedTimer;
        #region StepperBackgroundProperty
        public static readonly DependencyProperty StepperBackgroundProperty = DependencyProperty.Register(nameof(StepperBackground), 
            typeof(Brush), typeof(NumberStepper), new PropertyMetadata(default(Brush), OnBackgroundPropertyChanged));
        private static void OnBackgroundPropertyChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e) =>
            (dependencyObject as NumberStepper)?.OnBackgroundPropertyChanged(e);
        private void OnBackgroundPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue is Brush brush)
            {
                contentBox.Background = brush;
                UpButton.Background = brush;
                DownButton.Background = brush; 
            }
        }
        public Brush StepperBackground
        {
            get
            {
                return (Brush)GetValue(StepperBackgroundProperty);
            }
            set
            {
                if ((Brush)GetValue(StepperBackgroundProperty) != value)
                {
                    SetValue(StepperBackgroundProperty, value);
                }
            }
        }
        #endregion
        #region StepperForegroundProperty
        public static readonly DependencyProperty StepperForegroundProperty = DependencyProperty.Register(nameof(StepperForeground),
            typeof(Brush), typeof(NumberStepper), new PropertyMetadata(default(Brush), OnForegroundPropertyChanged));
        private static void OnForegroundPropertyChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e) =>
            (dependencyObject as NumberStepper)?.OnForegroundPropertyChanged(e);
        private void OnForegroundPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue is Brush brush)
            {
                contentBox.Foreground = brush;
                UpButton.Foreground = brush;
                DownButton.Foreground = brush;
            }
        }
        public Brush StepperForeground
        {
            get
            {
                return (Brush)GetValue(StepperForegroundProperty);
            }
            set
            {
                if ((Brush)GetValue(StepperForegroundProperty) != value)
                {
                    SetValue(StepperForegroundProperty, value);
                }
            }
        }
        #endregion
        #region MinMax
        public double Min
        {
            get { return (double)GetValue(MinProperty); }
            set { SetValue(MinProperty, value); }
        }
        public static readonly DependencyProperty MinProperty = DependencyProperty.Register(nameof(Min), typeof(double), typeof(NumberStepper), new PropertyMetadata(default(double)));
        public double Max
        {
            get { return (double)GetValue(MaxProperty); }
            set { SetValue(MaxProperty, value); }
        }
        public static readonly DependencyProperty MaxProperty = DependencyProperty.Register(nameof(Max), typeof(double), typeof(NumberStepper), new PropertyMetadata(default(double)));
        #endregion
        #region NumberProperty
        public static readonly DependencyProperty NumberProperty = DependencyProperty.Register(nameof(Number), typeof(double), 
            typeof(NumberStepper), new PropertyMetadata(default(double), OnNumberPropertyChanged));
        private static void OnNumberPropertyChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e) => 
            (dependencyObject as NumberStepper)?.OnNumberPropertyChanged();
        private void OnNumberPropertyChanged()
        {
            if (Number > Max)
            {
                Number = Max;
            }
            else if (Number < Min)
            {
                Number = Min;
            }
        }
        public double Number
        {
            get
            {
                return (double)GetValue(NumberProperty);
            }
            set
            {
                if ((double)GetValue(NumberProperty) != value)
                {
                    SetValue(NumberProperty, value);
                    OnPropertyChanged(nameof(Number));
                }
            }
        }
        #endregion
        public bool DownButtonPressed { get; set; }
        public bool UpButtonPressed { get; set; }
        public double IncrementValue
        {
            get
            {
                return incrementValue;
            }
            set
            {
                incrementValue = value;
                if (incrementValue != 1)
                {
                    contentBox.SetBinding(TextBox.TextProperty, new Binding("ContentProperty")
                    {
                        Source = this,
                        Path = new PropertyPath("Number"),
                        UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
                        //Delay = 1000,
                        Mode = BindingMode.TwoWay,
                        FallbackValue = 0,
                        //StringFormat = "{0:000.000}",
                        StringFormat = "N" + (incrementValue.ToString().Length - (((int)incrementValue).ToString().Length + 1)).ToString(),
                    });
                }
            }
        }
        public NumberStepper()
        {
            InitializeComponent();
            contentBox.DataContext = this;
            Min = -double.MaxValue;
            Max = double.MaxValue;
            MousePressedTimerInit();
        }
        #region INotifyPropertyChangedSupport 
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
        private void MousePressedTimerInit()
        {
            mousePressedTimer = new System.Timers.Timer
            {
                Interval = 250,
                Enabled = false,
                AutoReset = true
            };
            mousePressedTimer.Elapsed += new ElapsedEventHandler(MousePressedTimerInit_Elapsed);
        }
        private void MousePressedTimerInit_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (UpButtonPressed)
            {
                Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                {
                    Number += IncrementValue;
                }));
            }
            if (DownButtonPressed)
            {
                Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                {
                    Number -= IncrementValue;
                }));
            }
        }
        private void UpButton_Click(object sender, RoutedEventArgs e) => Number += IncrementValue;
        private void DownButton_Click(object sender, RoutedEventArgs e) => Number -= IncrementValue;
        private void UpButton_PreviewMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            UpButtonPressed = true;
            mousePressedTimer.Start();
        }
        private void UpButton_PreviewMouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            UpButtonPressed = false;
            mousePressedTimer.Stop();
        }
        private void DownButton_PreviewMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            DownButtonPressed = true;
            mousePressedTimer.Start();
        }
        private void DownButton_PreviewMouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            DownButtonPressed = false;
            mousePressedTimer.Stop();
        }
    }
}

