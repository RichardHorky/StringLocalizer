namespace StringLocalizer
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // set exceptions management
            // Catch exceptions on the UI thread (WinForms)
            Application.ThreadException += Application_ThreadException;

            // Catch exceptions on non-UI threads
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            // Catch unobserved task exceptions
            TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException;

            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new MainForm());
        }

        #region exceptions

        private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            MessageBox.Show(e.Exception.Message, "StringLocalizer", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            // Non-UI thread exceptions
            Exception ex = e.ExceptionObject as Exception;
            MessageBox.Show(ex.Message, "StringLocalizer", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private static void TaskScheduler_UnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            // Async Task exceptions
            e.SetObserved(); // Prevent the app from crashing
            MessageBox.Show(e.Exception.Message, "StringLocalizer", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        #endregion
    }
}