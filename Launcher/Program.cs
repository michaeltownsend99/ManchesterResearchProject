using System;
using System.Diagnostics;
class Program
{
    static void Main(string[] args)
    {
        //gets a file path from the file at the hard code location and executes it with default launcher
        try
        {

            if (args.Length != 0)
            {
                string executable = args[2];
                /*uncomment the below three lines if the exe file is in the Assets  
                    folder of the project and not installed with the system*/
                //string path = Assembly.GetExecutingAssembly().CodeBase;
                //string directory = Path.GetDirectoryName(path);
                //process.Start(directory + "\\" + executable);
                //string parameters = ApplicationData.Current.LocalSettings.Values["parameters"] as string;#
                //Console.WriteLine(Assembly.GetExecutingAssembly().Location);
                //Console.ReadLine();
                //System.IO.StreamReader file = new System.IO.StreamReader(Assembly.GetExecutingAssembly().Location + "/pythonPaths.txt");
                //var path = RuntimeVariables.Variables.path;
                System.IO.StreamReader file = new System.IO.StreamReader(@"C:\Users\mgsto\AppData\Local\Packages\de1f3e85-0808-48fd-84a2-1704212da7be_77706dn16q6d8\LocalState\paths.txt");
                string path = file.ReadLine();
                Process.Start(path);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            Console.ReadLine();
        } 

    }
}

