namespace PdfCreatorApplication.WebSite.ViewModels
{
    /// <summary>
    /// Type of message
    /// </summary>
    public enum MessageType
    {
        /// <summary>
        /// The information
        /// </summary>
        Information,

        /// <summary>
        /// The warning
        /// </summary>
        Warning,
        
        /// <summary>
        /// The error
        /// </summary>
        Error,

        /// <summary>
        /// The quick message
        /// </summary>
        QuickMessage,

        /// <summary>
        /// The system tip message
        /// </summary>
        SystemTipMessage
    }
}