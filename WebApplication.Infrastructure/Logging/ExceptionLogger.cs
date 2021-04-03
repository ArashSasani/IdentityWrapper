using System;
using WebApplication.Infrastructure.Interfaces;

namespace WebApplication.Infrastructure.Logging
{
    public class ExceptionLogger : IExceptionLogger
    {

        private readonly ICustomLogger _logger;
        public ExceptionLogger(ICustomLogger logger)
        {
            _logger = logger;
        }

        public void LogRunTimeError(Exception exception, string msgFormat
            , params object[] formatVars)
        {
            _logger.Fatal(exception, msgFormat, formatVars);
        }

        public void LogLogicalError(LogicalException exception, string msgFormat
            , params object[] formatVars)
        {
            _logger.Error(exception, msgFormat, formatVars);
        }

        public void LogWarning(Exception exception, string msgFormat
            , params object[] formatVars)
        {
            _logger.Warning(exception, msgFormat, formatVars);
        }
    }
}
