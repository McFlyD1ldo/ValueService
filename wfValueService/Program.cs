using libValueService;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace wfValueService
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var builder = Host.CreateDefaultBuilder();

            builder.ConfigureServices((hostContext, services) =>
            {
                services.AddSingleton<IValueService, ValueService>();
            });
            var host = builder.Build();
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new Form1(host.Services.GetRequiredService<IValueService>()));
        }
    }
}