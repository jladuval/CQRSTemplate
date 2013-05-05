namespace CQRS.Infrastructure.Logging.Interfaces
{
    using System;

    /*The following are the common log levels consistent with NLog/Log4j/Log4net (in descending order):

        Off
        Fatal
        Error
        Warn
        Info
        Debug
        Trace*/

    /// <summary>
    /// Common application events logging interface.
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// Writes trace information to the log.
        /// </summary>
        /// <param name="message">Trace message.</param>
        void Trace(string message);

        /// <summary>
        /// Writes debugging information to the log.
        /// </summary>
        /// <param name="message">Debug message.</param>
        void Debug(string message);

        /// <summary>
        /// Writes info information to the log.
        /// </summary>
        /// <param name="message">Info message.</param>
        void Info(string message);

        /// <summary>
        /// Writes error inforamtion to the log.
        /// </summary>
        /// <param name="message">Error message.</param>
        void Error(string message);

        /// <summary>
        /// Writes an error to the log.
        /// </summary>
        /// <param name="message">Error message.</param>
        /// <param name="exc">Exception object.</param>
        void Error(string message, Exception exc);
    }
}
