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
            string output = loggerImport.TimeStampPrefix;
            output += DateTime.Now.ToString(loggerImport.TimeFormat);
            output += loggerImport.PrePrefix;
            output += loggerImport.prefixArr[level];
            output += text;
            return output;
        }
        internal static string GetString(Logger loggerImport, Exception ex, int level)
        {
            return GetString(loggerImport, ex.Message, level);
        }
    }
}
