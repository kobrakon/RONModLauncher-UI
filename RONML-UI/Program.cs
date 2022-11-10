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
        public static void Main()
        {
            ApplicationConfiguration.Initialize();
            Application.Run(Instance);
        }
    }
}
