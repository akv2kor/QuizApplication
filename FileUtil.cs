using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizApplication
{

    internal static class FileUtil
    {
        private static object lockObject = new object();
        private static bool CreateDirectory(string fileName)
        {
            bool result = false;
            try
            {
                string directoryName = Path.GetDirectoryName(fileName);
                if (!Directory.Exists(directoryName))
                {
                    Console.WriteLine($"Directory Doesnt exist Creating {directoryName}");
                    Directory.CreateDirectory(directoryName);
                    Console.WriteLine($"Directory{directoryName}Creation Success");
                }
                result = true;
            }
            catch (DirectoryNotFoundException dirNotFoundEx)
            {
                Console.WriteLine($"DirectoryNotFoundException while creating directory {dirNotFoundEx}");

            }
            catch (IOException ioex)
            {
                Console.WriteLine($"IOException while creating directory {ioex}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception while creating directory {ex}");
            }
            return result;
        }
        public static bool CreateOutputFile(string fileName)
        {
            bool result = false;
            lock(lockObject)
            {
                if (CreateDirectory(Path.GetDirectoryName(fileName)))
                {
                    try
                    {
                        using (FileStream fs = File.Create(fileName))
                        {
                            // just creating file
                            //Console.WriteLine("File Creation success");
                        }
                        return true;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Unable to create/access/write file {ex.Message}");
                    }

                }

            }
            return result;
        }
    }
}
