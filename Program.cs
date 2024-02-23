using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Threading;
namespace QuizApplication
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string filename = "/log/out.txt";
            var status =FileUtil.CreateOutputFile(filename);
            if(!status)
            {
                Console.WriteLine("Unable to Create/access file Terminating application");
                return;
            }

            try
            {

                List<Thread> threads = new List<Thread>();
                using (StreamWriter sw = new StreamWriter(filename))
                {
                    PrintToFile pf = new PrintToFile(sw);

                    for (int i = 0; i < 10; i++)
                    {
                        try
                        {
                            Thread t = new Thread(pf.PrintThread);
                            threads.Add(t);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine($"Exception while creating Thread {e.Message}");
                        }
                    }
                    foreach (Thread t in threads)
                    {
                        try
                        {
                            t.Start();
                        }
                        catch(Exception e)
                        {
                            Console.WriteLine($"Unable to start Thread {e.Message}");
                        }
                        
                    }
                    foreach (Thread t in threads)
                    {
                        try
                        {
                            t.Join();
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine($"Unable to Join Thread {e.Message}");
                        }
                        
                    }
                }

            }
            catch(Exception ex )
            {
                Console.WriteLine($"Exception while playing with threads{ex.Message}");
            }
            Console.WriteLine("Press anykey to terminate application");
            try
            {
                Console.ReadKey(); 
            }
            catch(InvalidOperationException consoleException)
            {
                Console.WriteLine("Console input has been redirected please use -t option when running this docker");
                Console.WriteLine(@"Usage: docker run -it -v c:\junk:/log <ImageName>");
                Console.WriteLine("Press Enter to exit");
                Console.Read();
            }
            
            return;
        }
    }
}