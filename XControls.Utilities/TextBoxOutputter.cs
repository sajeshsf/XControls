﻿using System;
using System.Text;
using System.IO;
using System.Windows.Documents;
using System.Linq;
using System.Windows.Controls;
using System.Collections.Concurrent;
using System.Windows.Threading;

namespace XControls.Utilities
{
    public class TextBoxOutputter : TextWriter
    {
        private readonly RichTextBox RichTextBox;
        private DispatcherTimer Timer { get; set; }
        private ConcurrentQueue<string> Messages { get; set; }
        private bool ClearRequested { get; set; }
        private int CurrentLineCount { get; set; }
        public int MaximumNumberOfLines { get; set; }
        public double? UpdatePeriod
        {
            get => Timer?.Interval.TotalMilliseconds;
            set
            {
                if (Timer != null)
                {
                    Timer.Interval = TimeSpan.FromMilliseconds(value ?? 1000);
                }
            }
        }
        public bool? UseTimer
        {
            get => Timer?.IsEnabled;
            set
            {
                if (Timer != null)
                {
                    Timer.IsEnabled = value ?? false;
                }
            }
        }
        public override Encoding Encoding => Encoding.UTF8;
        public TextBoxOutputter(RichTextBox richTextBox)
        {
            RichTextBox = richTextBox;
            if (RichTextBox.Document.Blocks.First() is Paragraph paragraph)
            {
                paragraph.LineHeight = 20;
            }
            MaximumNumberOfLines = 2000;
            Messages = new ConcurrentQueue<string>();
            Timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(500),
                IsEnabled = true
            };
            Timer.Tick += Timer_Tick;
            Console.SetOut(this);
        }
        private void Timer_Tick(object? sender, EventArgs e) => UpdateUi();
        public override void Write(char value)
        {
            base.Write(value);
            AddMessage($"{value}");
        }
        public void SetOut() => Console.SetOut(this);
        public void UpdateUi()
        {
            Paragraph paragraph = (Paragraph)RichTextBox.Document.Blocks.First();
            if (ClearRequested || CurrentLineCount >= MaximumNumberOfLines)
            {
                ClearRequested = false;
                CurrentLineCount = 0;
                paragraph.Inlines.Clear();
            }
            int count = 0;
            var message = new StringBuilder();
            while (count < 1000 && Messages.TryDequeue(out string? text))
            {
                message.Append(text);
                CurrentLineCount++;
                count++;
            }
            if (message.Length > 0)
            {
                paragraph.Inlines.Add(message.ToString());
                RichTextBox.ScrollToVerticalOffset(RichTextBox.ExtentHeight);
            }
        }
        public void AddMessage(string value) => Messages.Enqueue(value);
        public void ClearMessages() => ClearRequested = true;
    }
}
