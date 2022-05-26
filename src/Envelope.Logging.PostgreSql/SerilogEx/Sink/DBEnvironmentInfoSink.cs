using Envelope.Database.PostgreSql;
using Envelope.Logging.SerilogEx;
using Serilog.Core;
using Serilog.Debugging;
using Serilog.Events;

namespace Envelope.Logging.PostgreSql.SerilogEx.Sink;

/*
 USAGE:
	Serilog.LoggerConfiguration
		.MinimumLevel.Verbose()
		.WriteTo.DBEnvironmentInfoSink(new Envelope.Logging.DB.SerilogEx.Sink.DBEnvironmentInfoSinkOptions
		{
			ConnectionString = "Host=localhost;Database=..."
		})
		.WriteTo.Console())
 */

public class DBEnvironmentInfoSink : DbBatchWriter<LogEvent>, ILogEventSink, IDisposable
{
	public DBEnvironmentInfoSink(DBEnvironmentInfoSinkOptions options, Action<string, object?, object?, object?>? errorLogger = null)
		: base(options ?? new DBEnvironmentInfoSinkOptions(), errorLogger ?? SelfLog.WriteLine)
	{
	}

	public override IDictionary<string, object?>? ToDictionary(LogEvent logEvent)
		=> LogEventHelper.ConvertEnvironmentInfoToDictionary(logEvent);

	public void Emit(LogEvent logEvent)
		=> Write(logEvent);
}
