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
        public Logger()
        {
            ColorArr = [
                ColorInf,
                ColorSuc,
                ColorWar,
                ColorErr,
                ColorCustom
                ];
            prefixArr = [
                PrefixInf,
                PrefixSuc,
                PrefixWar,
                PrefixErr,
                PrefixCustom
                ];
        }
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

        ////////////////////
        /// GENERAL-ZONE ///
        ////////////////////

        public bool useTerminalAsFAllback = true; // when set to false, logging to file will be used as fallback
        
        private bool useTerminal = false;
        public bool UseTerminal
        {
            get { return useTerminal; }
            set
            {
                if (value) 
                { 
                    useDefinitive = false;
                    useFile = false;
                }
                else
                {
                    useDefinitive = false;
                    useTerminal = true;
                }
                useTerminal = value;
            }
        }
        private bool useFile = false;
        public bool UseFile
        {
            get { return useFile; }
            set
            {
                if (value)
                {
                    useDefinitive = false;
                    useTerminal = false;
                }
                else
                {
                    useDefinitive = false;
                    useTerminal = true;
                }
                useFile = value;
            }
        }
        private bool useDefinitive = true;
        public bool UseDefinitive
        {
            get { return useDefinitive; }
            set
            {
                if (value)
                {
                    useTerminal = false;
                    useFile = false;
                }
                else
                {
                    if (useTerminalAsFAllback)
                    {
                        useTerminal = true;
                        useFile = false;
                    }
                    else
                    {
                        useTerminal = false;
                        useFile = true;
                    }
                }
                useDefinitive = value;
            }
        }

        //////////////////////////////
        /// PRINTING-ZONE-TERMINAL ///
        //////////////////////////////


        //////////////////
        /// COLOR-ZONE ///
        //////////////////

        private bool colorAll = false;
        public bool ColorAll
        {
            get { return colorAll; }
            set
            {
                colorMessage = !value;
                colorLevelPrefix = !value;
                /*if (value)
                {
                    colorMessage = false;
                    colorLevelPrefix = false;
                }
                else
                {
                    colorMessage = true;
                    colorLevelPrefix = true;
                }*/
                colorAll = value;
            }
        }
        private bool colorMessage = true;
        public bool ColorMessage
        {
            get { return colorMessage; }
            set 
            {
                colorAll = false; 
                colorMessage = value; 
            }
        }
        private bool colorLevelPrefix = false;
        public bool ColorLevelPrefix
        {
            get { return colorLevelPrefix; }
            set 
            {
                colorAll = false; 
                colorLevelPrefix = value; 
            }
        }
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
        public static readonly ConsoleColor ColorInf_default = ConsoleColor.White;
        public static readonly ConsoleColor ColorSuc_default = ConsoleColor.Green;
        public static readonly ConsoleColor ColorWar_default = ConsoleColor.Yellow;
        public static readonly ConsoleColor ColorErr_default = ConsoleColor.Red;
        public static readonly ConsoleColor ColorCustom_default = ConsoleColor.Magenta;

        private ConsoleColor colorInf = ColorInf_default;
        private ConsoleColor colorSuc = ColorSuc_default;
        private ConsoleColor colorWar = ColorWar_default;
        private ConsoleColor colorErr = ColorErr_default;
        private ConsoleColor colorCustom = ColorCustom_default;
        public ConsoleColor ColorInf { get => colorInf; set { colorInf = value; updateColorArr(); } }
        public ConsoleColor ColorSuc { get => colorSuc; set { colorSuc = value; updateColorArr(); } }
        public ConsoleColor ColorWar { get => colorWar; set { colorWar = value; updateColorArr(); } }
        public ConsoleColor ColorErr { get => colorErr; set { colorErr = value; updateColorArr(); } }
        public ConsoleColor ColorCustom { get => colorCustom; set { colorCustom = value; updateColorArr(); } }


        internal ConsoleColor[] ColorArr = new ConsoleColor[5];

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

        public static readonly string PrePrefix_default = " :: "; // string between timestamp and level prefix + remember the spaces :3
        public string PrePrefix = PrePrefix_default;

        // level prefixes (short and long) + default values + remember the spaces :3
        public static readonly string PrefixInf_default_short = "inf: ";
        public static readonly string PrefixSuc_default_short = "suc: ";
        public static readonly string PrefixWar_default_short = "war: ";
        public static readonly string PrefixErr_default_short = "err: ";
        public static readonly string PrefixCustom_default_short = "ctm: ";

        public static readonly string PrefixInf_default_long = "info: ";
        public static readonly string PrefixSuc_default_long = "success: ";
        public static readonly string PrefixWar_default_long = "warning: ";
        public static readonly string PrefixErr_default_long = "error: ";
        public static readonly string PrefixCustom_default_long = "custom: ";

        private string prefixInf = PrefixInf_default_short;
        private string prefixSuc = PrefixSuc_default_short;
        private string prefixWar = PrefixWar_default_short;
        private string prefixErr = PrefixErr_default_short;
        private string prefixCustom = PrefixCustom_default_short;
        public string PrefixInf { get => prefixInf; set { prefixInf = value; updatePrefixArr(); } }
        public string PrefixSuc { get => prefixSuc; set { prefixSuc = value; updatePrefixArr(); } }
        public string PrefixWar { get => prefixWar; set { prefixWar = value; updatePrefixArr(); } }
        public string PrefixErr { get => prefixErr; set { prefixErr = value; updatePrefixArr(); } }
        public string PrefixCustom { get => prefixCustom; set { prefixCustom = value; updatePrefixArr(); } }


        internal string[] prefixArr = new string[5];

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
        public string TimeFormat = "yyyy-MM-dd HH:mm:ss.ffff K"; // refer to https://www.c-sharpcorner.com/blogs/date-and-time-format-in-c-sharp-programming1
        public string TimeStampPrefix = "log: "; // string before timestamp + remember the space ^^
        private void setPrefix_short()
        {
            PrefixInf = PrefixInf_default_short;
            PrefixSuc = PrefixSuc_default_short;
            PrefixWar = PrefixWar_default_short;
            PrefixErr = PrefixErr_default_short;
            PrefixCustom = PrefixCustom_default_short;
        }
        private void setPrefix_long()
        {
            PrefixInf = PrefixInf_default_long;
            PrefixSuc = PrefixSuc_default_long;
            PrefixWar = PrefixWar_default_long;
            PrefixErr = PrefixErr_default_long;
            PrefixCustom = PrefixCustom_default_long;
        }
        private void updateColorArr()
        {
            ColorArr = [
                ColorInf,
                ColorSuc,
                ColorWar,
                ColorErr,
                ColorCustom
                ];
        }
        private void updatePrefixArr()
        {
            prefixArr = [
                PrefixInf,
                PrefixSuc,
                PrefixWar,
                PrefixErr,
                PrefixCustom
                ];
        }
        // printing text
        public void toTerm(string text)
        {
            PrintToTerminal.ToTerm(this, text);
        }

        // printing exceptions
        public void toTerm(Exception ex)
        {
            PrintToTerminal.ToTerm(this, ex.Message);
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