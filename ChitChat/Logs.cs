using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Configuration;

namespace ChitChat
{
    public class Logs
    {
        public string fileLocation { get; private set; }
        

        public Logs()
        {
            fileLocation = ConfigurationSettings.AppSettings["logPath"].Trim();
            if(!File.Exists(fileLocation))
                using(FileStream fs = File.Create(fileLocation))
                {

                }
            
                
        }
        public void writeException(Exception ex)
        {
            using (StreamWriter sw = File.AppendText(fileLocation))
            {
                sw.WriteLine(DateTime.UtcNow.Month + "-" + DateTime.UtcNow.Day + "-" + DateTime.UtcNow.Year + " " + DateTime.UtcNow.Hour + ":" + DateTime.UtcNow.Minute + ":" + DateTime.UtcNow.Second);
                sw.WriteLine(ex.Message);
                sw.WriteLine(ex.HResult);
                sw.WriteLine(ex.InnerException);
                sw.WriteLine(ex.StackTrace);
                sw.WriteLine("++++++++++++++++++++++++++++++++++++++++++++");
            }
        }
    }
}
