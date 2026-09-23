using System;
using System.Windows.Forms;
using CRM.winforms.Forms;
using CRM.winforms.Services;

namespace CRM.winforms
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            // Register services
            ServiceLocator.AuthService = new AuthService();
            ServiceLocator.Session = new SessionManager();

            // ApiClient with base address pointed to local API (adjust as needed)
            var http = new System.Net.Http.HttpClient { BaseAddress = new Uri("http://localhost:5000/") };
            ServiceLocator.ApiClient = new ApiClient(http);

            Application.Run(new LoginForm());
        }
    }
}
