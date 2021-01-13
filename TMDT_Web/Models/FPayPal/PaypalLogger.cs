using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace TMDT_Web.Models.FPayPal
{
    public class PaypalLogger
    {
        public static string LogDirectoryPath = Environment.CurrentDirectory;
        public static void Log(String messages)
        {
            try
            {
                StreamWriter strw = new StreamWriter(LogDirectoryPath + "\\PaypalError.log", true);
                strw.WriteLine(DateTime.Now.ToString("MM/dd/yyy HH:mm:ss")+ " ---> " + messages);
                strw.Close();
            }
            catch
            {
               /* throw e;*/
            }
        }
    }
}