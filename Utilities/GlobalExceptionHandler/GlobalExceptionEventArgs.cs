using System;

namespace XControls.Utilities
{
    public class GlobalExceptionEventArgs : EventArgs
    {
        public string Source { get; set; }
        public object Sender { get; set; }
        public Exception Exception { get; set; }
        public GlobalExceptionEventArgs(string source, object sender, Exception exception)
        {
            Source = source;
            Sender = sender;
            Exception = exception;
        }
    }
}
