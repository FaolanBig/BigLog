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
        internal static void ToTerm(Logger logger, string text, int level = 0)
        {

        }
        internal static void ToTerm(Logger logger, Exception ex, int level = 0)
        {

        }
    }
}
