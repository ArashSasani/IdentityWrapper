using NLog;
using System;
using System.Text;
using WebApplication.Infrastructure.Interfaces;

namespace WebApplication.Infrastructure.Logging
{
    public class CustomLogger : ICustomLogger
    {
        private static Logger _logger;

        public CustomLogger()
        {
            _logger = LogManager.GetCurrentClassLogger();
        }

        public void Information(string message)
        {
            _logger.Info(message);
        }

        public void Information(string fmt, params object[] vars)
        {
            _logger.Info(fmt, vars);
        }

        public void Information(Exception exception, string fmt, params object[] vars)
        {
            _logger.Info(FormatExceptionMessage(exception, fmt, vars));
        }

        public void Warning(string message)
        {
            _logger.Warn(message);
        }

        public void Warning(string fmt, params object[] vars)
        {
            _logger.Warn(fmt, vars);
        }

        public void Warning(Exception exception, string fmt, params object[] vars)
        {
            _logger.Warn(FormatExceptionMessage(exception, fmt, vars));
        }

        public void Error(string message)
        {
            _logger.Error(message);
        }

        public void Error(string fmt, params object[] vars)
        {
            _logger.Error(fmt, vars);
        }

        public void Error(Exception exception, string fmt, params object[] vars)
        {
            _logger.Error(FormatExceptionMessage(exception, fmt, vars));
        }

        public void Fatal(string message)
        {
            _logger.Fatal(message);
        }

        public void Fatal(string fmt, params object[] vars)
        {
            _logger.Fatal(fmt, vars);
        }

        public void Fatal(Exception exception, string fmt, params object[] vars)
        {
            _logger.Fatal(FormatExceptionMessage(exception, fmt, vars));
        }

        public void TraceApi(string componentName, string method, TimeSpan timespan)
        {
            TraceApi(componentName, method, timespan, "");
        }

        public void TraceApi(string componentName, string method, TimeSpan timespan, string fmt, params object[] vars)
        {
            TraceApi(componentName, method, timespan, string.Format(fmt, vars));
        }
        public void TraceApi(string componentName, string method, TimeSpan timespan, string properties)
        {
            string message = String.Concat("Component:", componentName, ";Method:", method
                , ";Timespan:", timespan.ToString(), ";Properties:", properties);
            _logger.Info(message);
#if DEBUG
            System.Diagnostics.Debug.Write(message);
#endif
        }

        private static string FormatExceptionMessage(Exception exception, string fmt, object[] vars)
        {
            var sb = new StringBuilder();
            sb.Append(" > Exception: ");
            sb.Append(exception.ToString());
            sb.Append(" > Error Message: ");
            sb.Append(string.Format(fmt, vars));
            return sb.ToString();
        }
    }
}
