using Microsoft.Extensions.Configuration;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Service;
using OpenQA.Selenium.Appium.Windows;
using System.Diagnostics;

namespace DesktopAutomation
{

    public class WindowsBase
    {
        private static WindowsDriver? _driver;
        private static AppiumLocalService? _service;
        public static Uri? _serverUri;
        private readonly string AppId = string.Empty;

        public static Uri InitiateAppiumService()
        {
            _service = new AppiumServiceBuilder().UsingAnyFreePort().Build();
            _service.Start();

            _serverUri = _service.ServiceUrl;
            Console.WriteLine("Appium is Started Running....");
            return _serverUri;
        }
        public WindowsDriver StartAppium()
        {
            var appCapabilities = new AppiumOptions();
            appCapabilities.App = GetConfigurationValue("AppSettings", "App");
            appCapabilities.DeviceName = GetConfigurationValue("AppSettings", "DeviceName");
            appCapabilities.PlatformName = GetConfigurationValue("AppSettings", "PlatformName");
            appCapabilities.AutomationName = GetConfigurationValue("AppSettings", "AutomationName");
            try
            {
                var _serverUri = InitiateAppiumService();
                _driver = new WindowsDriver(_serverUri, appCapabilities);
                _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error on Starting the Appium: {ex.Message}");
            }
            return _driver;

        }
        private void StartWinAppDriver()
        {
            // Check if WinAppDriver is already running, if not, start it
            if (Process.GetProcessesByName("WinAppDriver").Length == 0)
            {
                Process.Start("C:\\Program Files (x86)\\Windows Application Driver\\WinAppDriver.exe");
                System.Threading.Thread.Sleep(2000); // Give time for WinAppDriver to start
            }
        }


        public static string GetConfigurationValue(string Section, string Key)
        {
            try
            {
                if (string.IsNullOrEmpty(Section))
                {
                    Console.WriteLine("Section cannot be null or empty.");
                    return string.Empty;
                }

                var basePath = Directory.GetCurrentDirectory();

                var config = new ConfigurationBuilder()
                    .SetBasePath(basePath)
                    .AddJsonFile($"appsettings.json", optional: false, reloadOnChange: true)
                    .Build();

                return config.GetSection(Section).GetSection(Key).Value ?? string.Empty;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching configuration: {ex.Message}");
                return string.Empty;
            }
        }
        public void StopServices()
        {
            _service?.Dispose();
        }
    }
}
