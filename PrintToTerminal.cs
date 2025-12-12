using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigLog
{
    internal class PrintToTerminal
    {
        // level: 0 = inf, 1 = success, 2 = warning, 3 = error, 4 = custom
        internal static void ToTerm(Logger loggerImport, string text, Logger.LogLevel level)
        {
            if (level >= loggerImport.minLogLevelFile)
            {
                string output = loggerImport.TimeStampPrefix;
                output += DateTime.Now.ToString(loggerImport.TimeFormat);
                output += loggerImport.PrePrefix;
                if (loggerImport.ColorAll)
                {
                    output += loggerImport.prefixArr[(int)level];
                    output += text;
                    Console.ForegroundColor = loggerImport.ColorArr[(int)level];
                    Console.WriteLine(output);
                }
                else
                {
                    Console.ForegroundColor = loggerImport.Color_fallback;
                    Console.Write(output);

                    /*if (!loggerImport.ColorAll && loggerImport.ColorLevelPrefix)
                    {
                        Console.ForegroundColor = loggerImport.ColorArr[level];
                        Console.Write(loggerImport.prefixArr[level]);
                    }
                    else if (!loggerImport.ColorAll && !loggerImport.ColorLevelPrefix)
                    {
                        Console.ForegroundColor = loggerImport.Color_fallback;
                        Console.Write(loggerImport.prefixArr[level]);
                    }

                    if (!loggerImport.ColorAll && loggerImport.ColorMessage)
                    {
                        Console.ForegroundColor = loggerImport.ColorArr[level];
                        Console.WriteLine(text);
                    }
                    else if (!loggerImport.ColorAll && !loggerImport.ColorMessage)
                    {
                        Console.ForegroundColor = loggerImport.Color_fallback;
                        Console.WriteLine(text);
                    }*/

                    Console.ForegroundColor = loggerImport.ColorLevelPrefix ? loggerImport.ColorArr[(int)level] : loggerImport.Color_fallback;
                    Console.Write(loggerImport.prefixArr[(int)level]);
                    Console.ForegroundColor = loggerImport.ColorMessage ? loggerImport.ColorArr[(int)level] : loggerImport.Color_fallback;
                    Console.WriteLine(text);
                }
            }
        }
        internal static void ToTerm(Logger loggerImport, List<string> cache)
        {
            Console.ForegroundColor = loggerImport.Color_fallback;
            foreach (string entry in cache)
            {
                Console.WriteLine(entry);
            }
        }
    }
}
