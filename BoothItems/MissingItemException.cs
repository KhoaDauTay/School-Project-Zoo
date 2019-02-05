using System;

namespace BoothItems
{
    /// <summary>
    /// The class that represents an exception for missing booth items.
    /// </summary>
    public class MissingItemException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the MissingItemException class.
        /// </summary>
        /// <param name="message">The exception's message.</param>
        public MissingItemException(string message)
            : base(message)
        {
        }
    }
}