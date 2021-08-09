using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace SDC.Tools.Helpers
{
    public class BaseLogs
    {
        public static string SubFolder = "";

        public static void WriteLog(string AString)
        {
            try
            {
                string FileName = GetLogFileName();
                StreamWriter file = new StreamWriter(FileName, true);
                file.WriteLine(String.Format("{0}", DateTime.Now.ToString("yyyy MM dd HH:mm:ss.fff")));
                file.WriteLine(String.Format("{0}", AString));
                file.WriteLine("------------------------------------------------");
                file.Close();
                file.Dispose();
            }
            catch
            {
            }
        }


        public static void WriteLog(Exception e)
        {
            try
            {
                string FileName = GetLogFileName();
                string ErrNo = "";
                string Source = "";

                if (e is SqlException)
                {
                    ErrNo = "SQLERROR NO:" + ((SqlException)e).Number.ToString();
                    Source = "Source of exception: " + ((SqlException)e).Source;
                }

                StreamWriter file = new StreamWriter(FileName, true);
                file.WriteLine(String.Format("{0}", DateTime.Now.ToString("yyyy MM dd HH:mm:ss.fff")));
                if (ErrNo != "")
                {
                    file.WriteLine(String.Format("{0}", ErrNo));
                }
                if (Source != "")
                {
                    file.WriteLine(String.Format("{0}", Source));
                }
                file.WriteLine(String.Format("{0}", e.Message));
                file.WriteLine(String.Format("{0}", e.StackTrace));
                file.WriteLine("------------------------------------------------");
                file.Close();
                file.Dispose();
                if (e.InnerException != null)
                {
                    WriteLog(e.InnerException);
                }
            }
            catch { }
        }


        private static string GetLogFileName()
        {
            string ExeName = (Environment.GetCommandLineArgs()[0]);
            string Folder = Path.Combine(
                Path.Combine(Path.GetDirectoryName(ExeName), @"Log\" + string.Format("{0}_{1}_{2}",
                            DateTime.Today.Year.ToString().PadLeft(4, '0'),
                            DateTime.Today.Month.ToString().PadLeft(2, '0'),
                            DateTime.Today.Day.ToString().PadLeft(2, '0')

                )), SubFolder);
            Directory.CreateDirectory(Folder);

            string FileName = Path.Combine(Folder, Path.GetFileNameWithoutExtension(ExeName)
                + " " + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() + ".log");
            if (File.Exists(FileName))
            {
                long length = new FileInfo(FileName).Length;
                int counter = 0;
                while (length > 26214400)
                {
                    counter++;
                    FileName = Path.Combine(Folder, Path.GetFileNameWithoutExtension(ExeName)
                    + " " + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() + "-" + counter + ".log");
                    if (File.Exists(FileName))
                    {
                        length = new FileInfo(FileName).Length;
                    }
                    else
                    {
                        return FileName;
                    }
                }
            }


            return FileName;
        }

    }
}
