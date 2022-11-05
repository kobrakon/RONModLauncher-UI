using System.Runtime.CompilerServices;

namespace RONML_UI
{
    internal static class Program
    {
        internal static Form1 Instance = new();
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(Instance);
        }
    }
}