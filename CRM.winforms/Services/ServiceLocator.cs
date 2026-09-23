namespace CRM.winforms.Services
{
    /// <summary>
    /// Very small service locator for WinForms app to swap implementations quickly.
    /// </summary>
    public static class ServiceLocator
    {
        public static IAuthService? AuthService { get; set; }
        public static SessionManager? Session { get; set; }
        public static ApiClient? ApiClient { get; set; }
    }
}
