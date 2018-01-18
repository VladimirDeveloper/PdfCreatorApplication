using System;
using System.Runtime.Serialization;

namespace PdfCreatorApplication.Core.Utils.Exceptions
{

    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class UniqueConstraintViolationException : ApplicationException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UniqueConstraintViolationException"/> class.
        /// </summary>
        public UniqueConstraintViolationException()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UniqueConstraintViolationException"/> class.
        /// </summary>
        /// <param name="message">A message that describes the error.</param>
        public UniqueConstraintViolationException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UniqueConstraintViolationException"/> class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not a null reference, the current exception is raised in a catch block that handles the inner exception.</param>
        public UniqueConstraintViolationException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UniqueConstraintViolationException"/> class.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="args">The arguments.</param>
        public UniqueConstraintViolationException(string format, params object[] args)
            : base(string.Format(format, args))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UniqueConstraintViolationException"/> class.
        /// </summary>
        /// <param name="innerException">The inner exception.</param>
        /// <param name="format">The format.</param>
        /// <param name="args">The arguments.</param>
        public UniqueConstraintViolationException(Exception innerException, string format, params object[] args)
            : base(string.Format(format, args), innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UniqueConstraintViolationException"/> class.
        /// </summary>
        /// <param name="info">The object that holds the serialized object data.</param>
        /// <param name="context">The contextual information about the source or destination.</param>
        protected UniqueConstraintViolationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            
        }
    }
}
