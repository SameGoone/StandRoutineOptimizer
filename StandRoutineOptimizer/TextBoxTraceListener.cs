using System.Diagnostics;
using System.Windows.Controls;

namespace StandRoutineOptimizer
{
    class TextBoxTraceListener : TraceListener
    {
        private TextBox tBox;

        public TextBoxTraceListener(TextBox box)
        {
            tBox = box;
        }

        public override void Write(string msg)
        {
            tBox.Text += msg;
        }

        public override void WriteLine(string msg)
        {
            Write(msg + "\r\n");
        }
    }
}
