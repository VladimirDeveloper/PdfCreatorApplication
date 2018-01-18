using System;

namespace PdfCreatorApplication.WebSite.ViewModels
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public abstract class BaseViewModel
    {
        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        public MessageModel Message { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [has error].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [has error]; otherwise, <c>false</c>.
        /// </value>
        public bool HasError { get; set; }

        /// <summary>
        /// Gets or sets the name of the controller.
        /// </summary>
        /// <value>
        /// The name of the controller.
        /// </value>
        public string ControllerName { get; set; }

        /// <summary>
        /// Gets or sets the name of the action method.
        /// </summary>
        /// <value>
        /// The name of the action method.
        /// </value>
        public string ActionMethodName { get; set; }

        public abstract void Initialize();

    }
}