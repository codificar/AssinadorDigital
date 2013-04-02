using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace RegistraOID
{
    class Program
    {
        static void Main(string[] args)
        {
            Process myProcess1 = new Process();
            myProcess1.StartInfo.FileName = "cmd.exe";

            myProcess1.StartInfo.UseShellExecute = false;
            myProcess1.StartInfo.RedirectStandardInput = true;
            myProcess1.StartInfo.RedirectStandardOutput = true;
            myProcess1.StartInfo.RedirectStandardError = true;
            myProcess1.Start();

            StreamWriter sIn = myProcess1.StandardInput;
            StreamReader sOut = myProcess1.StandardOutput;
            StreamReader sErr = myProcess1.StandardError;

            string batPath = getApplicationPath();
            sIn.AutoFlush = true;
            sIn.Write("cd " + batPath + System.Environment.NewLine);
            sIn.Write("icpbrz-config-20.bat" + System.Environment.NewLine);
            sIn.Write("exit" + System.Environment.NewLine);

            String sOutput = sOut.ReadToEnd();

            if (!myProcess1.HasExited)
            {
                try
                {
                    myProcess1.Kill();
                }
                catch (Exception)
                { }
            }

            string msg = "The OID Register command window was closed at: " + myProcess1.ExitTime +
                "\n" + System.Environment.NewLine + "Exit Code: " + myProcess1.ExitCode +
                "\n" + sOutput + "\n";

            sIn.Close();
            sOut.Close();
            sErr.Close();
            myProcess1.Close();

            FileStream fstream = new FileStream(getApplicationPath() + "\\RegistraOID_Output.txt", FileMode.Create, FileAccess.ReadWrite);
            foreach (char ch in (msg))
            {
                fstream.WriteByte(Convert.ToByte(ch));
            }
            fstream.Close();
        }

        private static string getApplicationPath()
        {
            string appPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            return appPath;
        }
    }
}