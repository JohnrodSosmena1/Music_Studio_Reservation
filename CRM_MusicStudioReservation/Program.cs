using CRM_MusicStudioReservation;
using CRM_MusicStudioReservation.Forms;
using System;
using System.Windows.Forms;

namespace CRM.winforms
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            // Catch all UI-thread exceptions
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);

            Application.ThreadException += (s, e) =>
            {
                var msg = e.Exception?.Message ?? "";

                // 👇 Silently ignore harmless race-condition errors
                if (msg.Contains("DataGridView") && msg.Contains("does not have columns"))
                {
                    System.Diagnostics.Debug.WriteLine("[GLOBAL] Ignored: " + msg);
                    return;
                }

                // Everything else: show a message
                System.Diagnostics.Debug.WriteLine(
                    $"[GLOBAL] {e.Exception?.GetType().Name}: {msg}");

                MessageBox.Show(
                    $"Something went wrong, but the app will continue running.\n\n{msg}",
                    "Unexpected Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            };

            AppDomain.CurrentDomain.UnhandledException += (s, e) =>
            {
                if (e.ExceptionObject is Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(
                        $"[GLOBAL-DOMAIN] {ex.GetType().Name}: {ex.Message}");
                }
            };

            Application.Run(new LoginForm());
        }
    }
}