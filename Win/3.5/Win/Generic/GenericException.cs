using System;
using System.Runtime.Serialization;

namespace MControl.Generic
{
    /// <summary>
    /// Represents errors that occur during application execution
    /// </summary>
    [Serializable]
    public class GenericException<T> : Exception
    {
        T _State;

        public T State
        {
            get { return _State; }
        }

        /// <summary>
        /// Initializes a new instance of the Exception class.
        /// </summary>
        public GenericException()
        {
            _State=default(T);
        }

        /// <summary>
        /// Initializes a new instance of the Exception class with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public GenericException(string message)
            : base(message)
        {
            _State = default(T);
        }

       /// <summary>
        /// Initializes a new instance of the Exception class with a specified error message and state.
       /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="state">The value that describes the error state.</param>
        public GenericException(string message, T state)
            : base(message)
        {
            _State = state;
        }

        /// <summary>
        /// Initializes a new instance of the Exception class with serialized data.
        /// </summary>
        /// <param name="info">The SerializationInfo that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The StreamingContext that contains contextual information about the source or destination.</param>
        protected GenericException(SerializationInfo
            info, StreamingContext context)
            : base(info, context)
        {
        }

        /// <summary>
        /// Initializes a new instance of the Exception class with a specified error message and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="state">The value that describes the error state.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference if no inner exception is specified.</param>
        public GenericException(string message, T state, Exception innerException)
            : base(message, innerException)
        {
            _State = state;
        }
    }
}
