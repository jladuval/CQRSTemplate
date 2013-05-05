namespace CQRS.Infrastructure.Logging
{
    using System;

    using NLog;

    using CQRS.Infrastructure.Logging.Interfaces;

    public class NLogLogger : ILogger
    {
        private readonly Logger _logger;

        public NLogLogger(string name)
        {
            _logger = LogManager.GetLogger(name);
        }

        public void Trace(string message)
        {
            _logger.Trace(message);
        }

        public void Debug(string message)
        {
            _logger.Debug(message);
        }

        public void Info(string message)
        {
            _logger.Info(message);
        }

        public void Error(string message)
        {
            _logger.Error(message);
        }

        public void Error(string message, Exception exc)
        {
            _logger.Error(message, exc);
        }
    }
}