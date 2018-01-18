using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PdfCreatorApplication.WebSite.ViewModels
{
    /// <summary>
    /// Model of Message
    /// </summary>
    [Serializable]
    public class MessageModel
    {
        public MessageType Type { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>
        /// The text.
        /// </value>
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets the details.
        /// </summary>
        /// <value>
        /// The details.
        /// </value>
        public string Details { get; set; }

        
        [NonSerialized]
        private Exception _error;

        /// <summary>
        /// Gets or sets the error.
        /// </summary>
        /// <value>
        /// The error.
        /// </value>
        public Exception Error
        {
            get { return _error; }
            set { _error = value; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MessageModel"/> class.
        /// </summary>
        public MessageModel()
        {
            Type = MessageType.Information;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MessageModel"/> class.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="title">The title.</param>
        /// <param name="text">The text.</param>
        public MessageModel(MessageType type, string title, string text)
        {
            Type = type;
            Title = title;
            Text = text;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MessageModel"/> class.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="title">The title.</param>
        /// <param name="text">The text.</param>
        /// <param name="details">The details.</param>
        /// <param name="error">The error.</param>
        public MessageModel(MessageType type, string title, string text, string details = null, Exception error = null)
        {
            Type = type;
            Title = title;
            Text = text;
            Details = details;
            Error = error;
        }

    }
}