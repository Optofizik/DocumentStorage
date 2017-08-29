using DocumentStorage.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace DocumentStorage.Helpers
{
    public static class LogHelper
    {
        public static void WriteToLog(string message)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "\\" + StaticInfo.LogFileName;
            string[] text = new string[] { "****************", message, "****************", string.Empty };

            File.AppendAllLines(path, text);
        }
    }
}