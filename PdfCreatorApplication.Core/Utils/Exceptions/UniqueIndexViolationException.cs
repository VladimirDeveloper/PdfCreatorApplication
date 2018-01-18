using System;
using System.Runtime.Serialization;

namespace SiteCore.Utils.Exceptions
{
    [Serializable]
    public class UniqueIndexViolationException : ApplicationException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UniqueIndexViolationException"/> class.
        /// </summary>
        public UniqueIndexViolationException()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UniqueIndexViolationException"/> class.
        /// </summary>
        /// <param name="message">A message that describes the error.</param>
        public UniqueIndexViolationException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UniqueIndexViolationException"/> class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not a null reference, the current exception is raised in a catch block that handles the inner exception.</param>
        public UniqueIndexViolationException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UniqueIndexViolationException"/> class.
        /// </summary>
        /// <param name="innerException">The inner exception.</param>
        /// <param name="format">The format.</param>
        /// <param name="args">The arguments.</param>
        public UniqueIndexViolationException(Exception innerException, string format, params object[] args)
            : base(string.Format(format, args), innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UniqueIndexViolationException"/> class.
        /// </summary>
        /// <param name="info">The object that holds the serialized object data.</param>
        /// <param name="context">The contextual information about the source or destination.</param>
        protected UniqueIndexViolationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            
        }

    }
}
