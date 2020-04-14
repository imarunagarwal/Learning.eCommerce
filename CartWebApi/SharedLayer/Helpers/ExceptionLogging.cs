using System;
using System.IO;

namespace Helpers
{

    /// <summary>  
    /// Summary description for ExceptionLogging  
    /// </summary>  
    public static class ExceptionLogging
    {

        private static string ErrorlineNo, Errormsg, extype, ErrorLocation;

        public static void SendErrorToText(Exception ex)
        {
            try
            {
                var line = Environment.NewLine;

                ErrorlineNo = ex.StackTrace.Substring(ex.StackTrace.Length - 7, 7);
                Errormsg = ex.GetType().Name.ToString();
                extype = ex.GetType().ToString();
                ErrorLocation = ex.Message.ToString();
                var filePath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).FullName, "ErrorLogging");
                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }
                filePath = Path.Combine(filePath, "ErrorLog" + DateTime.Today.ToString("dd-MM-yy") + ".txt");   //Text File Name
                if (!File.Exists(filePath))
                {
                    File.Create(filePath).Dispose();

                }
                using StreamWriter sw = File.AppendText(filePath);
                string error = "Log Written Date:" + " " + DateTime.Now.ToString()
                + line + "Error Line No :" + " " + ErrorlineNo
                + line + "Error Message:" + " " + Errormsg
                + line + "Exception Type:" + " " + extype
                + line + "Error Location :" + " " + ErrorLocation
                + line + "Error Page Url:" + " " + line;

                sw.WriteLine("-----------Exception Details on " + " " + DateTime.Now.ToString() + "-----------------");
                sw.WriteLine("-------------------------------------------------------------------------------------");
                sw.WriteLine(line);
                sw.WriteLine(error);
                sw.WriteLine("--------------------------------*End*------------------------------------------");
                sw.WriteLine(line);
                sw.Flush();
                sw.Close();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

    }
}
