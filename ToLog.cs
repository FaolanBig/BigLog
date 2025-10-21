using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigLog
{
    public class Logger
    {
        // constructor
        public Logger() { }
        public Logger(Logger loggerImport) 
        {
            ColorInf = loggerImport.ColorInf;
            ColorSuc = loggerImport.ColorSuc;
            ColorWar = loggerImport.ColorWar;
            ColorErr = loggerImport.ColorErr;
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
        public readonly ConsoleColor ColorSuc_default = ConsoleColor.Green; 
        public readonly ConsoleColor ColorWar_default = ConsoleColor.Yellow;
        public readonly ConsoleColor ColorErr_default = ConsoleColor.Red;
        public readonly ConsoleColor ColorInf_default = ConsoleColor.White;

        public ConsoleColor ColorInf = ConsoleColor.White;
        public ConsoleColor ColorSuc = ConsoleColor.White; //success
        public ConsoleColor ColorWar = ConsoleColor.White; //warning
        public ConsoleColor ColorErr = ConsoleColor.White;
        private bool enableDefaultColors = false;
        public bool EnableDefaultColors
        {
            get
            {
                return enableDefaultColors;
            }
            set
            {
                if (value)
                {
                    setDefaultColors();
                    enableDefaultColors = value;
                }
                else
                {
                    setDefaultColors_fallback();
                    enableDefaultColors = value;
                }
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

    }
}
