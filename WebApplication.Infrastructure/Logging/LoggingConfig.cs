using NLog;
using NLog.Config;
using NLog.Targets;
using System.Data.SqlClient;
using System.Text;

namespace WebApplication.Infrastructure.Logging
{
    public class LoggingConfig
    {
        public static string ConnectionString
        {
            get
            {
                return AppSettings.CONNECTION_STRING;
            }
        }

        public static void LogToFile()
        {
            var config = new LoggingConfiguration();
            var fileTarget = new FileTarget { FileName = AppSettings.APP_LOGS_FILE_PATH };
            config.AddTarget("logfile", fileTarget);
            LoggingRule rule = new LoggingRule("*", LogLevel.Warn, fileTarget);
            config.LoggingRules.Add(rule);
            LogManager.Configuration = config;
        }

#warning Should be called on the app start up
        public static void LogToDb()
        {
            DatabaseTarget dbTarget = new DatabaseTarget
            {
                Name = "db",
                ConnectionString = ConnectionString
            };
            dbTarget.Parameters.Add(new DatabaseParameterInfo("@message"
                , new NLog.Layouts.SimpleLayout("${message}")));
            dbTarget.CommandText = string.Format("INSERT INTO ApplicationLogs(Message, RegisteredDate) " +
                "VALUES (@message, GETDATE());");
            // Keep original configuration
            LoggingConfiguration config = LogManager.Configuration;
            if (config == null)
                config = new LoggingConfiguration();
            config.AddTarget(dbTarget.Name, dbTarget);
            LoggingRule rule = new LoggingRule("*", LogLevel.Warn, dbTarget);
            config.LoggingRules.Add(rule);
            LogManager.Configuration = config;

            CreateLogTableIfNotExists(dbTarget, ConnectionString);
        }

        public static void CreateLogTableIfNotExists(DatabaseTarget dbTarget
            , string strConnectionString)
        {
            StringBuilder sb = new StringBuilder();
            InstallationContext installationContext = new InstallationContext();
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder
            {
                ConnectionString = strConnectionString
            };
            string strDatabase = builder.InitialCatalog;
            sb.AppendLine("IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' " +
                "AND  TABLE_NAME = 'ApplicationLogs')");
            sb.AppendLine("RETURN");
            sb.AppendLine("");
            sb.AppendLine("CREATE TABLE [dbo].[ApplicationLogs](");
            sb.AppendLine("[Id] [int] IDENTITY NOT NULL,");
            sb.AppendLine("[RegisteredDate] [DATETIME] NOT NULL,");
            sb.AppendLine("[Message] [nvarchar](max) NULL,");
            sb.AppendLine(" CONSTRAINT [PK_ApplicationLogs] PRIMARY KEY CLUSTERED ");
            sb.AppendLine("(");
            sb.AppendLine("[Id] ASC");
            sb.AppendLine(")WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON" +
                ", ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]");
            sb.AppendLine(") ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]");

            DatabaseCommandInfo createTableCommand = new DatabaseCommandInfo
            {
                Text = sb.ToString(),
                CommandType = System.Data.CommandType.Text
            };
            dbTarget.InstallDdlCommands.Add(createTableCommand);
            // we can now connect to the target DB
            dbTarget.InstallConnectionString = strConnectionString;
            // create the table if it does not exist
            dbTarget.Install(installationContext);
        }
    }
}
