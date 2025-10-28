using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;

namespace BigLog
{
    internal class PrintToFile
    {
        // level: 0 = inf, 1 = success, 2 = warning, 3 = error, 4 = custom
        internal static void ToFile(Logger loggerImport, List<string> cache) 
        {
            FileStream fileStream = null;
            try
            {
                fileStream = new FileStream(Path.GetFullPath(loggerImport.FileName), FileMode.OpenOrCreate);
                using (StreamWriter writer = new StreamWriter(fileStream, loggerImport.UseDefaultEncoding ? Encoding.Default : loggerImport.Encoding))
                {
                    foreach (string entry in cache)
                    {
                        writer.WriteLine(entry);
                    }
                }
            }
            finally
            {
                if (fileStream != null) 
                { 
                    fileStream.Close(); 
                }
            }
        }
        internal static void ToFile(Logger loggerImport, string text, int level) 
        {
            FileStream fileStream = null;
            try
            {
                fileStream = new FileStream(Path.GetFullPath(loggerImport.FileName), FileMode.OpenOrCreate);
                using (StreamWriter writer = new StreamWriter(fileStream, loggerImport.UseDefaultEncoding ? Encoding.Default : loggerImport.Encoding))
                {
                    writer.WriteLine(OutputFormatter.GetString(loggerImport, text, level));
                }
            }
            finally
            {
                if (fileStream != null) 
                { 
                    fileStream.Close(); 
                }
            }
        }
    }
}
