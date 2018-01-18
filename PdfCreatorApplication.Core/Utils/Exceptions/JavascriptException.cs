using System;
using System.Runtime.Serialization;

namespace PdfCreatorApplication.Core.Utils.Exceptions
{
    /// <summary>
    /// The JScript exception
    /// </summary>
    [Serializable]
    public class JavascriptException : Exception
    {
        /// <summary>
        /// Gets a message that describes the current exception.
        /// </summary>
        /// <returns>The error message that explains the reason for the exception, or an empty string("").</returns>
        public new string Message { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the line number.
        /// </summary>
        /// <value>
        /// The line number.
        /// </value>
        public string LineNumber { get; set; }

        /// <summary>
        /// Gets or sets the URL.
        /// </summary>
        /// <value>
        /// The URL.
        /// </value>
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets the user agent.
        /// </summary>
        /// <value>
        /// The user agent.
        /// </value>
        public string UserAgent { get; set; }

        /// <summary>
        /// Gets or sets the browser.
        /// </summary>
        /// <value>
        /// The browser.
        /// </value>
        public string Browser { get; set; }

        /// <summary>
        /// Gets or sets the browser version.
        /// </summary>
        /// <value>
        /// The browser version.
        /// </value>
        public string BrowserVersion { get; set; }

        /// <summary>
        /// Gets or sets the platform.
        /// </summary>
        /// <value>
        /// The platform.
        /// </value>
        public string Platform { get; set; }


        /// <summary>
        /// Gets or sets the username under which error occured.
        /// </summary>
        /// <value>
        /// The username.
        /// </value>
        public string Username { get; set; }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return "JavascriptException " +
                   "{\n\tName: '" + this.Name + "'" +
                   ";\n\tMessage: '" + this.Message + "'" +
                   ";\n\tLineNumber: " + this.LineNumber +
                   ";\n\tUsername: " + this.Username +
                   ";\n\tUrl: '" + this.Url + "'" +
                   "\n\t}";
        }
    }
}
