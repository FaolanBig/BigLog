using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace BigLog
{
    public class Logger
    {
        // constructor
        public Logger() { }
        public Logger(Logger loggerImport) 
        {
            // color import
            ColorInf = loggerImport.ColorInf;
            ColorSuc = loggerImport.ColorSuc;
            ColorWar = loggerImport.ColorWar;
            ColorErr = loggerImport.ColorErr;
            Color_fallback = loggerImport.Color_fallback;
            EnableDefaultColors = loggerImport.EnableDefaultColors;

            // format import
            PrefixInf = loggerImport.PrefixInf;
            PrefixSuc = loggerImport.PrefixSuc;
            PrefixWar = loggerImport.PrefixWar;
            PrefixErr = loggerImport.PrefixErr;
        }

        //////////////////////////////
        /// PRINTING-ZONE-TERMINAL ///
        //////////////////////////////


        //////////////////
        /// COLOR-ZONE ///
        //////////////////

        private ConsoleColor color_fallback = ConsoleColor.White;
        public ConsoleColor Color_fallback
        {
            get { return color_fallback; }
            set
            {
                color_fallback = value;
                setDefaultColors_fallback();
            }
        }
        public static readonly ConsoleColor ColorSuc_default = ConsoleColor.Green;
        public static readonly ConsoleColor ColorWar_default = ConsoleColor.Yellow;
        public static readonly ConsoleColor ColorErr_default = ConsoleColor.Red;
        public static readonly ConsoleColor ColorInf_default = ConsoleColor.White;

        public ConsoleColor ColorInf = ColorSuc_default;
        public ConsoleColor ColorSuc = ColorWar_default; //success
        public ConsoleColor ColorWar = ColorErr_default; //warning
        public ConsoleColor ColorErr = ColorInf_default;
        private bool enableDefaultColors = false;
        public bool EnableDefaultColors
        {
            get { return enableDefaultColors; }
            set
            {
                if (value) { setDefaultColors(); }
                else { setDefaultColors_fallback(); }
                enableDefaultColors = value;
            }
        }
        private void setDefaultColors()
        {
            ColorInf = ColorInf_default;
            ColorSuc = ColorSuc_default;
            ColorWar = ColorWar_default;
            ColorErr = ColorErr_default;
        }
        private void setDefaultColors_fallback()
        {
            ColorInf = color_fallback;
            ColorSuc = color_fallback;
            ColorWar = color_fallback;
            ColorErr = color_fallback;
        }

        ///////////////////
        /// FORMAT-ZONE ///
        ///////////////////

        public static readonly string PrePrefix_default = "logger: ";
        public string PrePrefix = PrePrefix_default;

        public static readonly string PrefixInf_default_short = "inf";
        public static readonly string PrefixSuc_default_short = "suc";
        public static readonly string PrefixWar_default_short = "war";
        public static readonly string PrefixErr_default_short = "err";

        public static readonly string PrefixInf_default_long = "info";
        public static readonly string PrefixSuc_default_long = "success";
        public static readonly string PrefixWar_default_long = "warning";
        public static readonly string PrefixErr_default_long = "error";

        public string PrefixInf = PrefixInf_default_short;
        public string PrefixSuc = PrefixSuc_default_short;
        public string PrefixWar = PrefixWar_default_short;
        public string PrefixErr = PrefixErr_default_short;

        private bool useShortPrefix = true;
        public bool UseShortPrefix
        {
            get { return useShortPrefix; }
            set
            {
                if (value) { setPrefix_short(); }
                else { setPrefix_long(); }
                    useShortPrefix = value;
            }
        }
        public bool PrintTimeStamp = true;
        public bool PrintTimeStampBeforeLevel = true;
        private void setPrefix_short()
        {
            PrefixInf = PrefixInf_default_short;
            PrefixSuc = PrefixSuc_default_short;
            PrefixWar = PrefixWar_default_short;
            PrefixErr = PrefixErr_default_short;
        }
        private void setPrefix_long()
        {
            PrefixInf = PrefixInf_default_long;
            PrefixSuc = PrefixSuc_default_long;
            PrefixWar = PrefixWar_default_long;
            PrefixErr = PrefixErr_default_long;
        }

        // printing text
        public void toTerm(string text)
        {
            PrintToTerminal.ToTerm(this, text);
        }

        // printing exceptions
        public void toTerm(Exception ex)
        {
            PrintToTerminal.ToTerm(this, ex);
        }

        //////////////////////////
        /// PRINTING-ZONE-FILE ///
        //////////////////////////

        // printing text
        public void toFile(string text)
        {
            PrintToFile.ToFile(this, text);
        }
        // printing exceptions
        public void toFile(Exception ex)
        {
            PrintToFile.ToFile(this, ex);
        }
    }
}