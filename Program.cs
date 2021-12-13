using System;
using System.IO;

namespace Timestomp
{
    class Program
    {

        static bool CopyTimestampFromFile(string toPath, string fromPath)
        {
            try
            {
                if (!File.Exists(toPath))
                {
                    Console.WriteLine("Cannot find {0}", toPath);
                    return false;
                }

                if (!File.Exists(fromPath))
                {
                    Console.WriteLine("Cannot find {0}", fromPath);
                    return false;
                }
                Console.WriteLine("Old timestamps {0} Create:{1} Modify:{2}", new FileInfo(toPath).FullName, File.GetCreationTime(toPath), File.GetLastWriteTime(toPath));


                DateTime creationTime = File.GetCreationTime(fromPath);
                DateTime modifyTime = File.GetLastWriteTime(fromPath);

                File.SetCreationTimeUtc(toPath, creationTime);
                File.SetLastWriteTimeUtc(toPath, modifyTime);

            } catch (Exception e)
            {
                Console.WriteLine("Error copying timestamp from {0} to {1}: {2}", new FileInfo(toPath).FullName, toPath, e.Message);
                return false;
            }
            return true;
        }

        static bool SetTimestampOnFile(string targetfile, string createTimestamp, string modifyTimestamp)
        {
            if (!File.Exists(targetfile))
            {
                Console.WriteLine("Cannot find {0}", targetfile);
                return false;
            }
            Console.WriteLine("Old timestamps {0} Create:{1} Modify:{2}", new FileInfo(targetfile).FullName, File.GetCreationTime(targetfile), File.GetLastWriteTime(targetfile));

            //parse into DateTime object
            DateTime parsedCreate = new DateTime();
            DateTime parsedModify = new DateTime();
            try
            {
                parsedCreate = DateTime.Parse(createTimestamp);
                parsedModify = DateTime.Parse(modifyTimestamp);
            } catch (Exception e)
            {
                Console.WriteLine("Error parsing timestamps into objects: {0}", e.Message);
                PrintUsage();
                return false;
            }

            try
            {
 
                File.SetCreationTimeUtc(targetfile, parsedCreate);
                File.SetLastWriteTimeUtc(targetfile, parsedModify);

            } catch (Exception e)
            {
                Console.WriteLine("Error setting timestamp on {0}: {1}", new FileInfo(targetfile).FullName, e.Message);
                return false;
            }
            return true;
        }
        static void Main(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("Invalid args");
                PrintUsage();
                return;
            }

            string filename = args[0];
            string action = args[1];
            if (!(action == "-copy" || action == "-set" || action == "-get"))
            {
                PrintUsage();
                return;
            }

            if (action == "-get")
            {
                GetTimestamp(filename);
            }

            if (action == "-copy") {
                var copyfromfile = args[2];
                CopyTimestampFromFile(filename, copyfromfile);
                Console.WriteLine("New timestamps {0} Create={1} Modify={2}", new FileInfo(filename).FullName, File.GetCreationTime(filename), File.GetLastWriteTime(filename));

            }

            if (action == "-set")
            {
                if (args.Length == 3) //create and modify timestamp same
                {
                    string newCreate = args[2];
                    string newModify = args[2];
                    SetTimestampOnFile(filename, newCreate, newModify);
                }
                else //different create and modify timestamps
                {
                    string newCreate = args[2];
                    string newModify = args[3];
                    SetTimestampOnFile(filename, newCreate, newModify);
                }
                Console.WriteLine("New timestamps {0} Create={1} Modify={2}", new FileInfo(filename).FullName, File.GetCreationTime(filename), File.GetLastWriteTime(filename));

            }

        }

        private static bool GetTimestamp(string filename)
        {
            if (!File.Exists(filename))
            {
                Console.WriteLine("Cannot find {0}", filename);
                return false;
            }
            Console.WriteLine("Existing timestamps {0} Create={1} Modify={2}", new FileInfo(filename).FullName, File.GetCreationTime(filename), File.GetLastWriteTime(filename));
            return true;
        }

        public static void PrintUsage()
        {
            Console.WriteLine(@"Get timestamp of file: Timestomp.exe C:\windows\explorer.exe -get");
            Console.WriteLine(@"Copy from another file: Timestomp.exe C:\targetfile.exe -copy C:\windows\system32\calc.exe");
            Console.WriteLine(@"Set create: Timestomp.exe C:\targetfile.exe -set CreateDate (like YYYY-MM-DDTHH:mm:ss)");
            Console.WriteLine(@"Set create and modify: Timestomp.exe C:\targetfile.exe -set CreateDate ModifyDate");
            Console.WriteLine(@"Example: Timestomp.exe C:\targetfile.exe -set 2018-08-18T07:22:16");
            Console.WriteLine(@"Example: Timestomp.exe C:\targetfile.exe -set 2017-08-18T07:22:16 2018-03-14T03:21:11");
        }
    }
}
