﻿using Envelope.Database.PostgreSql;
using Envelope.Logging.SerilogEx;
using Serilog.Core;
using Serilog.Debugging;
using Serilog.Events;

namespace Envelope.Logging.PostgreSql.SerilogEx.Sink;

/*
 USAGE:
	Serilog.LoggerConfiguration
		.MinimumLevel.Verbose()
		.WriteTo.DBLogMessageSink(new Envelope.Logging.DB.SerilogEx.Sink.DBLogMessageSinkOptions
		{
			ConnectionString = "Host=localhost;Database=..."
		})
		.WriteTo.Console())
 */

public class DBLogMessageSink : DbBatchWriter<LogEvent>, ILogEventSink, IDisposable
{
	public DBLogMessageSink(DBLogMessageSinkOptions options, Action<string, object?, object?, object?>? errorLogger = null)
		: base(options ?? new DBLogMessageSinkOptions(), errorLogger ?? SelfLog.WriteLine)
	{
	}

	public override IDictionary<string, object?>? ToDictionary(LogEvent logEvent)
		=> LogEventHelper.ConvertLogMessageToDictionary(logEvent);

	public void Emit(LogEvent logEvent)
		=> Write(logEvent);
}
