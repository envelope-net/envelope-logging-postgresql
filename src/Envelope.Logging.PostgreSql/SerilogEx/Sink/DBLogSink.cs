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
		.WriteTo.DBLogSink(new Envelope.Logging.DB.SerilogEx.Sink.DBLogSinkOptions
		{
			ConnectionString = "Host=localhost;Database=..."
		})
		.WriteTo.Console())
 */

public class DBLogSink<TIdentity> : DbBatchWriter<LogEvent>, ILogEventSink, IDisposable
	where TIdentity : struct
{
	public DBLogSink(DBLogSinkOptions<TIdentity> options, Action<string, object?, object?, object?>? errorLogger = null)
		: base(options ?? new DBLogSinkOptions<TIdentity>(), errorLogger ?? SelfLog.WriteLine)
	{
	}

	public override IDictionary<string, object?>? ToDictionary(LogEvent logEvent)
		=> LogEventHelper.ConvertLogToDictionary<TIdentity>(logEvent);

	public void Emit(LogEvent logEvent)
		=> Write(logEvent);
}
