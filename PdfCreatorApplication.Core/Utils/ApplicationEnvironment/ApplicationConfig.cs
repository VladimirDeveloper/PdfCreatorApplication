using System;
using System.Configuration;
using System.Diagnostics;

namespace PdfCreatorApplication.Core.Utils.ApplicationEnvironment
{
    /// <summary>
    /// Configuration of current application
    /// </summary>
    public class ApplicationConfig
    {
        /// <summary>
        /// The web site default root
        /// </summary>
        private const string WebSiteDefaultRoot = "";

        /// <summary>
        /// The web site root
        /// </summary>
        private const string WebSiteRoot = "WebSiteRoot";

        /// <summary>
        /// Gets the application settings.
        /// </summary>
        /// <param name="settingsName">Name of the settings.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns></returns>
        private static string GetAppSettings(string settingsName, string defaultValue = "")
        {
            string settings;
            try
            {
                settings = ConfigurationManager.AppSettings[settingsName] ?? defaultValue;
            }
            catch (ConfigurationErrorsException configEx)
            {
                Debug.WriteLine(configEx.ToString());
                settings = defaultValue;
            }

            return settings;
        }

        /// <summary>
        /// Gets the web site root.
        /// </summary>
        /// <returns></returns>
        public static string GetWebSiteRoot()
        {
            return GetAppSettings(WebSiteRoot, AppDomain.CurrentDomain.BaseDirectory);
        }
    }
}
