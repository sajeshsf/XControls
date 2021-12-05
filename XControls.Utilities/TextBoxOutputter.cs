using System;
using System.Text;
using System.IO;
using System.Windows.Documents;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Collections.Concurrent;
using System.Windows.Threading;

namespace XControls.Utilities
{
    public class TextBoxOutputter : TextWriter
    {
        private readonly RichTextBox RichTextBox = null;
        public DispatcherTimer Timer { get; set; }
        private ConcurrentQueue<string> Messages { get; set; }
        private bool ClearRequested { get; set; }
        private int CurrentLineCount { get; set; }
        public int MaximumNumberOfLines { get; set; }
        public double UpdatePeriod
        {
            get => Timer?.Interval.TotalMilliseconds ?? 0;
            set
            {
                if (Timer != null)
                {
                    Timer.Interval = TimeSpan.FromMilliseconds(UpdatePeriod);
                }
            }
        }
        public override Encoding Encoding => Encoding.UTF8;
        public TextBoxOutputter()
        {
            UpdatePeriod = 500;
            MaximumNumberOfLines = 2000;
            Messages = new ConcurrentQueue<string>();
            Timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(UpdatePeriod),
                IsEnabled = true
            };
            Timer.Tick += Timer_Tick;
        }
        public TextBoxOutputter(RichTextBox richTextBox) : this() => RichTextBox = richTextBox;
        public void SetOut() => Console.SetOut(this);
        private void Timer_Tick(object? sender, EventArgs e)
        {
            int count = 0;
            while (Messages.TryDequeue(out string? text) && count < 10)
            {
                ((Paragraph)RichTextBox.Document.Blocks.First()).Inlines.Add(text);
                CurrentLineCount++;
                count++;
                if (count == 10)
                {
                    RichTextBox.ScrollToVerticalOffset(RichTextBox.ExtentHeight);
                }
            }
            if (ClearRequested || CurrentLineCount >= MaximumNumberOfLines)
            {
                ClearRequested = false;
                CurrentLineCount = 0;
                ((Paragraph)RichTextBox.Document.Blocks.First()).Inlines.Clear();
            }
        }
        public override void Write(char value)
        {
            base.Write(value);
            AddMessage($"{value}");
        }
        public void AddMessage(string value) => Messages.Enqueue(value);
        public void ClearMessages() => ClearRequested = true;
    }
}
