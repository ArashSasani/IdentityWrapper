using System;

namespace WebApplication.Infrastructure.Interfaces
{
    public interface IExceptionLogger
    {
        void LogRunTimeError(Exception exception, string msgFormat
            , params object[] formatVars);

        void LogLogicalError(LogicalException exception, string msgFormat
            , params object[] formatVars);

        void LogWarning(Exception exception, string msgFormat
            , params object[] formatVars);
    }
}
