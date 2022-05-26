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
		.WriteTo.DBHardwareInfoSink(new Envelope.Logging.DB.SerilogEx.Sink.DBHardwareInfoSinkOptions
		{
			ConnectionString = "Host=localhost;Database=..."
		})
		.WriteTo.Console())
 */

public class DBHardwareInfoSink : DbBatchWriter<LogEvent>, ILogEventSink, IDisposable
{
	public DBHardwareInfoSink(DBHardwareInfoSinkOptions options, Action<string, object?, object?, object?>? errorLogger = null)
		: base(options ?? new DBHardwareInfoSinkOptions(), errorLogger ?? SelfLog.WriteLine)
	{
	}

	public override IDictionary<string, object?>? ToDictionary(LogEvent logEvent)
		=> LogEventHelper.ConvertHardwareInfoToDictionary(logEvent);

	public void Emit(LogEvent logEvent)
		=> Write(logEvent);
}
