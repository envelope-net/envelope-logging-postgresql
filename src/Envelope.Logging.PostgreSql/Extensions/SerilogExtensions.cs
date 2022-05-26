using Envelope.Logging.PostgreSql;
using Envelope.Logging.PostgreSql.SerilogEx.Sink;
using Envelope.Logging.SerilogEx;
using Serilog.Configuration;
using Serilog.Events;
using System.Diagnostics;

namespace Serilog;

[DebuggerStepThrough]
public static class SerilogExtensions
{
	[DebuggerHidden]
	[DebuggerStepThrough]
	public static LoggerConfiguration LogMessageSinkToPostgreSql<TIdentity>(
		this LoggerSinkConfiguration loggerConfiguration,
		DBLogMessageSinkOptions<TIdentity> options,
		LogEventLevel restrictedToMinimumLevel = LevelAlias.Minimum)
		where TIdentity : struct
	{
		if (loggerConfiguration == null)
			throw new ArgumentNullException(nameof(loggerConfiguration));

		var sink = new DBLogMessageSink<TIdentity>(options);

		[DebuggerHidden]
		[DebuggerStepThrough]
		bool Condition(LogEvent logEvent)
			=> LogEventHelper.IsLogMessage(logEvent);

		[DebuggerHidden]
		[DebuggerStepThrough]
		void Configure(LoggerSinkConfiguration configuration)
			=> configuration.Sink(sink, restrictedToMinimumLevel);

		return loggerConfiguration
				.Conditional(Condition, Configure);
	}

	[DebuggerHidden]
	[DebuggerStepThrough]
	public static LoggerConfiguration LogSinkToPostgreSql<TIdentity>(
		this LoggerSinkConfiguration loggerConfiguration,
		DBLogSinkOptions<TIdentity> options,
		bool logAllLogEvents = false,
		LogEventLevel restrictedToMinimumLevel = LevelAlias.Minimum)
		where TIdentity : struct
	{
		if (loggerConfiguration == null)
			throw new ArgumentNullException(nameof(loggerConfiguration));

		var sink = new DBLogSink<TIdentity>(options);

		[DebuggerHidden]
		[DebuggerStepThrough]
		bool Condition(LogEvent logEvent)
			=> logAllLogEvents
				|| (!LogEventHelper.IsLogMessage(logEvent)
					&& !LogEventHelper.IsEnvironmentInfo(logEvent)
					&& !LogEventHelper.IsHardwareInfo(logEvent));

		[DebuggerHidden]
		[DebuggerStepThrough]
		void Configure(LoggerSinkConfiguration configuration)
			=> configuration.Sink(sink, restrictedToMinimumLevel);

		return loggerConfiguration
				.Conditional(Condition, Configure);
	}

	//public static LoggerConfiguration HardwareInfoSinkToPostgreSql(
	//	this LoggerSinkConfiguration loggerConfiguration,
	//	DBHardwareInfoSinkOptions options,
	//	LogEventLevel restrictedToMinimumLevel = LevelAlias.Minimum)
	//{
	//	if (loggerConfiguration == null)
	//		throw new ArgumentNullException(nameof(loggerConfiguration));

	//	var sink = new DBHardwareInfoSink(options);
	//	return loggerConfiguration
	//			.Conditional(
	//				logEvent => LogEventHelper.IsHardwareInfo(logEvent),
	//				cfg => cfg.Sink(sink, restrictedToMinimumLevel));
	//}

	//public static LoggerConfiguration EnvironmentInfoSinkToPostgreSql(
	//	this LoggerSinkConfiguration loggerConfiguration,
	//	DBEnvironmentInfoSinkOptions options,
	//	LogEventLevel restrictedToMinimumLevel = LevelAlias.Minimum)
	//{
	//	if (loggerConfiguration == null)
	//		throw new ArgumentNullException(nameof(loggerConfiguration));

	//	var sink = new DBEnvironmentInfoSink(options);
	//	return loggerConfiguration
	//			.Conditional(
	//				logEvent => LogEventHelper.IsEnvironmentInfo(logEvent),
	//				cfg => cfg.Sink(sink, restrictedToMinimumLevel));
	//}
}
