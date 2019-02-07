using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Windows.UI.Popups;

namespace curr.Models
{
    public class Date
    {
        public int Day { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public override string ToString()
        {
            return Year + "-" + Day + "-" + Month;
        }
    }
    public class Helper
    {
        public static void TraceMessage(string message,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            Debug.WriteLine("message: " + message);
            Debug.WriteLine("member name: " + memberName);
            Debug.WriteLine("source file path: " + sourceFilePath);
            Debug.WriteLine("source line number: " + sourceLineNumber);
        }
        public static async void Message(string info)
        {
            var dialog = new MessageDialog(info);
            await dialog.ShowAsync();
        }
    }
}