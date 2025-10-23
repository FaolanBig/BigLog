using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigLog
{
    internal class OutputFormatter
    {
        internal static string GetString(Logger loggerImport, string text, int level)
        {
            string output;
            string TimeStamp = DateTime.Now.ToString(loggerImport.TimeFormat);
            
        }
    }
}
