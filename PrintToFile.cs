using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace QuizApplication
{
    public class PrintToFile
    {
        private static object lockObject = new object();
        StreamWriter _fs;
        static int currentLine=0;
        public PrintToFile(StreamWriter fs) {
            _fs = fs;
            _fs.WriteLine($"{currentLine},{0},{DateTime.Now.ToString("HH:mm:ss.fff")}");
        }
        public void PrintThread()
        {
            try
            {
                int threadId = Thread.CurrentThread.ManagedThreadId;
                for (int i = 0; i < 10; i++)
                {
                    lock (lockObject)
                    {
                        Interlocked.Increment(ref currentLine);
                        _fs.WriteLine($"{currentLine},{threadId},{DateTime.Now.ToString("HH:mm:ss.fff")}");
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception occured while printing to Thread {ex.Message}");
            }

        }
    }
}
