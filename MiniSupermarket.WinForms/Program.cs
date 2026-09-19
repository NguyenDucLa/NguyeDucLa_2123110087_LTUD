namespace MiniSupermarket.WinForms
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            // Đổi FormLogin làm form khởi chạy mặc định khi bật chương trình WinForms
            Application.Run(new FormLogin());
        }
    }
}