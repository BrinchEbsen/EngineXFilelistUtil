using System;

namespace FilelistUtilities.Filelist
{
    public partial class Filelist
    {
        //Really minimal logging functionality for external programs like GUI tools to hook into.

        private static double _progress = 0d;

        private static Action<string, double> _logCallBack = null;

        private static Action<string> _errorCallBack = null;

        public static Action<string, double> LogCallBack
        {
            set { _logCallBack = value; }
        }

        public static Action<string> ErrorCallBack
        {
            set { _errorCallBack = value; }
        }

        public static void ClearCallBacks()
        {
            _logCallBack = null;
            _errorCallBack = null;
        }

        private static void LogInfo(string message)
        {
            if (_logCallBack is not null)
            {
                _logCallBack(message, _progress);
            } else
            {
                Console.WriteLine(message);
            }
        }

        private static void LogError(string message)
        {
            if (_errorCallBack is not null)
            {
                _errorCallBack(message);
            } else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(message);
                Console.ResetColor();
            }
        }
    }
}
