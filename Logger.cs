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
        // usage: a brief test function to quickly create a logger with custom settings
        // here is an example of how to use it:
        /*internal void test()
        {
            Logger logger = new Logger()
            {
                LogToFile = true,
                UseShortPrefix = true
            };
        }*/
        // user manual: refer to the README.md file and the wiki in the GitHub repository found here: https://github.com/FaolanBig/BigLog


        // constructor
        public Logger()
        {
            // level-dev
            ColorArr = [
                ColorTrc,
                ColorDbg,
                ColorInf,
                ColorSuc,
                ColorWar,
                ColorErr,
                ColorCtc,
                ColorFat,
                ColorCustom
                ];
            // level-dev
            prefixArr = [
                PrefixTrc,
                PrefixDbg,
                PrefixInf,
                PrefixSuc,
                PrefixWar,
                PrefixErr,
                PrefixCtc,
                PrefixFat,
                PrefixCustom
                ];
        }
        public Logger(Logger loggerImport)
        {
            // level-dev
            // color import
            ColorTrc = loggerImport.ColorTrc;
            ColorDbg = loggerImport.ColorDbg;
            ColorInf = loggerImport.ColorInf;
            ColorSuc = loggerImport.ColorSuc;
            ColorWar = loggerImport.ColorWar;
            ColorErr = loggerImport.ColorErr;
            ColorCtc = loggerImport.ColorCtc;
            ColorFat = loggerImport.ColorFat;
            ColorCustom = loggerImport.ColorCustom;
            Color_fallback = loggerImport.Color_fallback;
            EnableDefaultColors = loggerImport.EnableDefaultColors;

            // level-dev
            // format import
            PrefixTrc = loggerImport.PrefixTrc;
            PrefixDbg = loggerImport.PrefixDbg;
            PrefixInf = loggerImport.PrefixInf;
            PrefixSuc = loggerImport.PrefixSuc;
            PrefixWar = loggerImport.PrefixWar;
            PrefixErr = loggerImport.PrefixErr;
            PrefixCtc = loggerImport.PrefixCtc;
            PrefixFat = loggerImport.PrefixFat;
            PrefixCustom = loggerImport.PrefixCustom;
        }

        ////////////////////
        /// GENERAL-ZONE ///
        ////////////////////

        private bool autoFlush = true; // flush every time something gets passed to BigLog. If disabled, BigLog will only flush if loggerObj.flush() is called. In the meantime logs will be stored in cache
        public bool AutoFlush
        {
            get { return autoFlush; }
            set { autoFlush = value; }
        }

        private bool logToTerminal = true;
        public bool LogToTerminal
        {
            get { return logToTerminal; }
            set { logToTerminal = value; }
        }
        private bool logToFile = false;
        public bool LogToFile
        {
            get { return logToFile; }
            set { logToFile = value; }
        }

        /*public bool useTerminalAsFAllback = true; // when set to false, logging to file will be used as fallback
        
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
        }*/

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
        // level-dev
        public static readonly ConsoleColor ColorTrc_default = ConsoleColor.Gray;
        public static readonly ConsoleColor ColorDbg_default = ConsoleColor.Gray;
        public static readonly ConsoleColor ColorInf_default = ConsoleColor.White;
        public static readonly ConsoleColor ColorSuc_default = ConsoleColor.Green;
        public static readonly ConsoleColor ColorWar_default = ConsoleColor.Yellow;
        public static readonly ConsoleColor ColorErr_default = ConsoleColor.Red;
        public static readonly ConsoleColor ColorCtc_default = ConsoleColor.Magenta;
        public static readonly ConsoleColor ColorFat_default = ConsoleColor.DarkRed;
        public static readonly ConsoleColor ColorCustom_default = ConsoleColor.Cyan;

        // level-dev
        private ConsoleColor colorTrc = ColorTrc_default;
        private ConsoleColor colorDbg = ColorDbg_default;
        private ConsoleColor colorInf = ColorInf_default;
        private ConsoleColor colorSuc = ColorSuc_default;
        private ConsoleColor colorWar = ColorWar_default;
        private ConsoleColor colorErr = ColorErr_default;
        private ConsoleColor colorCtc = ColorCtc_default;
        private ConsoleColor colorFat = ColorFat_default;
        private ConsoleColor colorCustom = ColorCustom_default;
        // level-dev
        public ConsoleColor ColorTrc { get => colorTrc; set { colorTrc = value; updateColorArr(); } }
        public ConsoleColor ColorDbg { get => colorDbg; set { colorDbg = value; updateColorArr(); } }
        public ConsoleColor ColorInf { get => colorInf; set { colorInf = value; updateColorArr(); } }
        public ConsoleColor ColorSuc { get => colorSuc; set { colorSuc = value; updateColorArr(); } }
        public ConsoleColor ColorWar { get => colorWar; set { colorWar = value; updateColorArr(); } }
        public ConsoleColor ColorErr { get => colorErr; set { colorErr = value; updateColorArr(); } }
        public ConsoleColor ColorCtc { get => colorCtc; set { colorCtc = value; updateColorArr(); } }
        public ConsoleColor ColorFat { get => colorFat; set { colorFat = value; updateColorArr(); } }
        public ConsoleColor ColorCustom { get => colorCustom; set { colorCustom = value; updateColorArr(); } }


        internal ConsoleColor[] ColorArr = new ConsoleColor[5];

        private bool enableDefaultColors = true;
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
            // level-dev
            ColorTrc = ColorTrc_default;
            ColorDbg = ColorDbg_default;
            ColorInf = ColorInf_default;
            ColorSuc = ColorSuc_default;
            ColorWar = ColorWar_default;
            ColorErr = ColorErr_default;
            ColorCtc = ColorCtc_default;
            ColorFat = ColorFat_default;
            ColorCustom = ColorCustom_default;
        }
        private void setDefaultColors_fallback()
        {
            // level-dev
            ColorTrc = color_fallback;
            ColorDbg = color_fallback;
            ColorInf = color_fallback;
            ColorSuc = color_fallback;
            ColorWar = color_fallback;
            ColorErr = color_fallback;
            ColorCtc = color_fallback;
            ColorFat = color_fallback;
            ColorCustom = color_fallback;
        }

        ///////////////////
        /// FORMAT-ZONE ///
        ///////////////////

        public static readonly string PrePrefix_default = " :: "; // string between timestamp and level prefix + remember the spaces :3
        public string PrePrefix = PrePrefix_default;

        // level prefixes (short and long) + default values + remember the spaces :3
        // level-dev
        public static readonly string PrefixTrc_default_short = "trc: ";
        public static readonly string PrefixDbg_default_short = "dbg: ";
        public static readonly string PrefixInf_default_short = "inf: ";
        public static readonly string PrefixSuc_default_short = "suc: ";
        public static readonly string PrefixWar_default_short = "war: ";
        public static readonly string PrefixErr_default_short = "err: ";
        public static readonly string PrefixCtc_default_short = "ctc: ";
        public static readonly string PrefixFat_default_short = "fat: ";
        public static readonly string PrefixCustom_default_short = "ctm: ";

        // level-dev
        public static readonly string PrefixTrc_default_long = "trace: ";
        public static readonly string PrefixDbg_default_long = "debug: ";
        public static readonly string PrefixInf_default_long = "info: ";
        public static readonly string PrefixSuc_default_long = "success: ";
        public static readonly string PrefixWar_default_long = "warning: ";
        public static readonly string PrefixErr_default_long = "error: ";
        public static readonly string PrefixCtc_default_long = "critical: ";
        public static readonly string PrefixFat_default_long = "fatal: ";
        public static readonly string PrefixCustom_default_long = "custom: ";

        // level-dev
        private string prefixTrc = PrefixTrc_default_short;
        private string prefixDbg = PrefixDbg_default_short;
        private string prefixInf = PrefixInf_default_short;
        private string prefixSuc = PrefixSuc_default_short;
        private string prefixWar = PrefixWar_default_short;
        private string prefixErr = PrefixErr_default_short;
        private string prefixCtc = PrefixCtc_default_short;
        private string prefixFat = PrefixFat_default_short;
        private string prefixCustom = PrefixCustom_default_short;
        // level-dev
        public string PrefixTrc { get => prefixTrc; set { prefixTrc = value; updatePrefixArr(); } }
        public string PrefixDbg { get => prefixDbg; set { prefixDbg = value; updatePrefixArr(); } }
        public string PrefixInf { get => prefixInf; set { prefixInf = value; updatePrefixArr(); } }
        public string PrefixSuc { get => prefixSuc; set { prefixSuc = value; updatePrefixArr(); } }
        public string PrefixWar { get => prefixWar; set { prefixWar = value; updatePrefixArr(); } }
        public string PrefixErr { get => prefixErr; set { prefixErr = value; updatePrefixArr(); } }
        public string PrefixCtc { get => prefixCtc; set { prefixCtc = value; updatePrefixArr(); } }
        public string PrefixFat { get => prefixFat; set { prefixFat = value; updatePrefixArr(); } }
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
            // level-dev
            PrefixTrc = PrefixTrc_default_short;
            PrefixDbg = PrefixDbg_default_short;
            PrefixInf = PrefixInf_default_short;
            PrefixSuc = PrefixSuc_default_short;
            PrefixWar = PrefixWar_default_short;
            PrefixErr = PrefixErr_default_short;
            PrefixCtc = PrefixCtc_default_short;
            PrefixFat = PrefixFat_default_short;
            PrefixCustom = PrefixCustom_default_short;
        }
        private void setPrefix_long()
        {
            // level-dev
            PrefixTrc = PrefixTrc_default_long;
            PrefixDbg = PrefixDbg_default_long;
            PrefixInf = PrefixInf_default_long;
            PrefixSuc = PrefixSuc_default_long;
            PrefixWar = PrefixWar_default_long;
            PrefixErr = PrefixErr_default_long;
            PrefixCtc = PrefixCtc_default_long;
            PrefixFat = PrefixFat_default_long;
            PrefixCustom = PrefixCustom_default_long;
        }
        private void updateColorArr()
        {
            // level-dev
            ColorArr = [
                ColorTrc,
                ColorDbg,
                ColorInf,
                ColorSuc,
                ColorWar,
                ColorErr,
                ColorCtc,
                ColorFat,
                ColorCustom
                ];
        }
        private void updatePrefixArr()
        {
            // level-dev
            prefixArr = [
                PrefixTrc,
                PrefixDbg,
                PrefixInf,
                PrefixSuc,
                PrefixWar,
                PrefixErr,
                PrefixCtc,
                PrefixFat,
                PrefixCustom
                ];
        }

        /////////////////
        /// FILE-ZONE ///
        /////////////////

        /*private string baseFilePath = AppDomain.CurrentDomain.BaseDirectory; // not used anymore, to set file, use the FileName property (this also works with absolute paths)
        public string BaseFilePath
        {  
            get { return baseFilePath; } 
            set { baseFilePath = value; }
        }*/
        private string fileName = "log.txt";
        public string FileName
        {
            get { return fileName; }
            set
            {
                fileName = value;
                //FullFilePath = Path.Combine(baseFilePath, value);
                //FullFilePath = Path.GetFullPath(value);
            }
        }
        /*private string fullFilePath = "log.txt";
        internal string FullFilePath
        { 
            get { return fullFilePath; } 
            set { fullFilePath = Path.GetFullPath(value); }
        }*/
        private bool useDefaultEncoding = true; // if true, the encoding variable below will be skipped
        public bool UseDefaultEncoding
        {
            get { return useDefaultEncoding; }
            set
            {
                useDefaultEncoding = value;
            }
        }
        private Encoding encoding = Encoding.Default; // if set, useDefaultEncoding will automatically get disabled
        public Encoding Encoding
        {
            get { return encoding; }
            set
            {
                encoding = value;
                useDefaultEncoding = false;
            }
        }

        //////////////////
        /// CACHE-ZONE ///
        //////////////////

        private List<string> cache = []; // if cache is used, colored output will be disabled (this shouldn't be a problem since cache and flush will mainly be used when logging to a file)

        public void ClearCache() // be careful, this function will empty the cache and its action can not be undone
        {
            cache.Clear();
        }
        public void AddToCache(string text, int level) { cache.Add(OutputFormatter.GetString(this, text, level)); } // adds text to cache and formatts them int he set manner and adds timestamps
        public void flushCache() // use this function to flush the cached logs to the logging file and/or the terminal output
        {
            if (LogToTerminal) { PrintToTerminal.ToTerm(this, cache); }
            if (LogToFile) { PrintToFile.ToFile(this, cache); }
            ClearCache();
        }

        /////////////////////
        /// PRINTING-ZONE ///
        /////////////////////

        // text logging functions
        // level-dev
        public void Trc(string text)
        {
            if (!autoFlush) { AddToCache(text, 0); }
            else
            {
                if (LogToTerminal) { PrintToTerminal.ToTerm(this, text, 0); }
                if (LogToFile) { PrintToFile.ToFile(this, text, 0); }
            }
        }
        public void Dbg(string text)
        {
            if (!autoFlush) { AddToCache(text, 1); }
            else
            {
                if (LogToTerminal) { PrintToTerminal.ToTerm(this, text, 1); }
                if (LogToFile) { PrintToFile.ToFile(this, text, 1); }
            }
        }
        public void Inf(string text)
        {
            if (!autoFlush) { AddToCache(text, 2); }
            else
            {
                if (LogToTerminal) { PrintToTerminal.ToTerm(this, text, 2); }
                if (LogToFile) { PrintToFile.ToFile(this, text, 2); }
            }
        }
        public void Suc(string text)
        {
            if (!autoFlush) { AddToCache(text, 3); }
            else
            {
                if (LogToTerminal) { PrintToTerminal.ToTerm(this, text, 3); }
                if (LogToFile) { PrintToFile.ToFile(this, text, 3); }
            }
        }
        public void War(string text)
        {
            if (!autoFlush) { AddToCache(text, 4); }
            else
            {
                if (LogToTerminal) { PrintToTerminal.ToTerm(this, text, 4); }
                if (LogToFile) { PrintToFile.ToFile(this, text, 4); }
            }
        }
        public void Err(string text)
        {
            if (!autoFlush) { AddToCache(text, 5); }
            else
            {
                if (LogToTerminal) { PrintToTerminal.ToTerm(this, text, 5); }
                if (LogToFile) { PrintToFile.ToFile(this, text, 5); }
            }
        }
        public void Ctc(string text)
        {
            if (!autoFlush) { AddToCache(text, 6); }
            else
            {
                if (LogToTerminal) { PrintToTerminal.ToTerm(this, text, 6); }
                if (LogToFile) { PrintToFile.ToFile(this, text, 6); }
            }
        }
        public void Fat(string text)
        {
            if (!autoFlush) { AddToCache(text, 7); }
            else
            {
                if (LogToTerminal) { PrintToTerminal.ToTerm(this, text, 7); }
                if (LogToFile) { PrintToFile.ToFile(this, text, 7); }
            }
        }
        public void Ctm(string text)
        {
            if (!autoFlush) { AddToCache(text, 8); }
            else
            {
                if (LogToTerminal) { PrintToTerminal.ToTerm(this, text, 8); }
                if (LogToFile) { PrintToFile.ToFile(this, text, 8); }
            }
        }

        // alias functions for text logging
        public void Trace(string text) // alias for Trc
        {
            Trc(text);
        }
        public void Debug(string text) // alias for Dbg
        {
            Dbg(text);
        }
        public void Info(string text) // alias for Inf
        {
            Inf(text);
        }
        public void Success(string text) // alias for Suc
        {
            Suc(text);
        }
        public void Warning(string text) // alias for War
        {
            War(text);
        }
        public void Error(string text) // alias for Err
        {
            Err(text);
        }
        public void Critical(string text) // alias for Ctc
        {
            Ctc(text);
        }
        public void Fatal(string text) // alias for Fat
        {
            Fat(text);
        }
        public void Custom(string text) // alias for Ctm
        {
            Ctm(text);
        }

        // exception logging functions
        // level-dev
        public void Trc(Exception ex)
        {
            if (!autoFlush) { AddToCache(ex.Message, 0); }
            else
            {
                if (LogToTerminal) { PrintToTerminal.ToTerm(this, ex.Message, 0); }
                if (LogToFile) { PrintToFile.ToFile(this, ex.Message, 0); }
            }
        }
        public void Dbg(Exception ex)
        {
            if (!autoFlush) { AddToCache(ex.Message, 1); }
            else
            {
                if (LogToTerminal) { PrintToTerminal.ToTerm(this, ex.Message, 1); }
                if (LogToFile) { PrintToFile.ToFile(this, ex.Message, 1); }
            }
        }
        public void Inf(Exception ex)
        {
            if (!autoFlush) { AddToCache(ex.Message, 2); }
            else
            {
                if (LogToTerminal) { PrintToTerminal.ToTerm(this, ex.Message, 2); }
                if (LogToFile) { PrintToFile.ToFile(this, ex.Message, 2); }
            }
        }
        public void Suc(Exception ex)
        {
            if (!autoFlush) { AddToCache(ex.Message, 3); }
            else
            {
                if (LogToTerminal) { PrintToTerminal.ToTerm(this, ex.Message, 3); }
                if (LogToFile) { PrintToFile.ToFile(this, ex.Message, 3); }
            }
        }
        public void War(Exception ex)
        {
            if (!autoFlush) { AddToCache(ex.Message, 4); }
            else
            {
                if (LogToTerminal) { PrintToTerminal.ToTerm(this, ex.Message, 4); }
                if (LogToFile) { PrintToFile.ToFile(this, ex.Message, 4); }
            }
        }
        public void Err(Exception ex)
        {
            if (!autoFlush) { AddToCache(ex.Message, 5); }
            else
            {
                if (LogToTerminal) { PrintToTerminal.ToTerm(this, ex.Message, 5); }
                if (LogToFile) { PrintToFile.ToFile(this, ex.Message, 5); }
            }
        }
        public void Ctc(Exception ex)
        {
            if (!autoFlush) { AddToCache(ex.Message, 6); }
            else
            {
                if (LogToTerminal) { PrintToTerminal.ToTerm(this, ex.Message, 6); }
                if (LogToFile) { PrintToFile.ToFile(this, ex.Message, 6); }
            }
        }
        public void Fat(Exception ex)
        {
            if (!autoFlush) { AddToCache(ex.Message, 7); }
            else
            {
                if (LogToTerminal) { PrintToTerminal.ToTerm(this, ex.Message, 7); }
                if (LogToFile) { PrintToFile.ToFile(this, ex.Message, 7); }
            }
        }
        public void Ctm(Exception ex)
        {
            if (!autoFlush) { AddToCache(ex.Message, 8); }
            else
            {
                if (LogToTerminal) { PrintToTerminal.ToTerm(this, ex.Message, 8); }
                if (LogToFile) { PrintToFile.ToFile(this, ex.Message, 8); }
            }
        }

        // alias functions for exception logging
        public void Trace(Exception ex) // alias for Trc
        {
            Trc(ex);
        }
        public void Debug(Exception ex) // alias for Dbg
        {
            Dbg(ex);
        }
        public void Info(Exception ex) // alias for Inf
        {
            Inf(ex);
        }
        public void Success(Exception ex) // alias for Suc
        {
            Suc(ex);
        }
        public void Warning(Exception ex) // alias for War
        {
            War(ex);
        }
        public void Error(Exception ex) // alias for Err
        {
            Err(ex);
        }
        public void Critical(Exception ex) // alias for Ctc
        {
            Ctc(ex);
        }
        public void Fatal(Exception ex) // alias for Fat
        {
            Fat(ex);
        }
        public void Custom(Exception ex) // alias for Ctm
        {
            Ctm(ex);
        }
    }
}