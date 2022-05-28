using NpgsqlTypes;
using Envelope.Data;
using Envelope.Database.PostgreSql;

namespace Envelope.Logging.PostgreSql;

public class DBLogMessageSinkOptions<TIdentity> : DbBatchWriterOptions, IBatchWriterOptions
	where TIdentity : struct
{
	public DBLogMessageSinkOptions()
	{
		PropertyNames = new List<string>
		{
			nameof(ILogMessage<TIdentity>.IdLogMessage),
			nameof(ILogMessage<TIdentity>.IdLogLevel),
			nameof(ILogMessage<TIdentity>.CreatedUtc),
			nameof(ILogMessage<TIdentity>.TraceInfo.SourceSystemName),
			nameof(ILogMessage<TIdentity>.TraceInfo.RuntimeUniqueKey),
			nameof(ILogMessage<TIdentity>.LogCode),
			nameof(ILogMessage<TIdentity>.ClientMessage),
			nameof(ILogMessage<TIdentity>.InternalMessage),
			nameof(ILogMessage<TIdentity>.TraceInfo.TraceFrame.MethodCallId),
			Serilog.Core.Constants.SourceContextPropertyName,
			nameof(ILogMessage<TIdentity>.TraceInfo.TraceFrame),
			nameof(ILogMessage<TIdentity>.StackTrace),
			nameof(ILogMessage<TIdentity>.Detail),
			nameof(ILogMessage<TIdentity>.TraceInfo.IdUser),
			nameof(ILogMessage<TIdentity>.CommandQueryName),
			nameof(ILogMessage<TIdentity>.IdCommandQuery),
			nameof(ILogMessage<TIdentity>.MethodCallElapsedMilliseconds),
			nameof(ILogMessage<TIdentity>.PropertyName),
			nameof(ILogMessage<TIdentity>.DisplayPropertyName),
			nameof(ILogMessage<TIdentity>.ValidationFailure),
			nameof(ILogMessage<TIdentity>.IsValidationError),
			nameof(ILogMessage<TIdentity>.CustomData),
			nameof(ILogMessage<TIdentity>.Tags),
			nameof(ILogMessage<TIdentity>.TraceInfo.CorrelationId)
		};

		PropertyTypeMapping = new Dictionary<string, NpgsqlDbType>
		{
			{ nameof(ILogMessage<TIdentity>.IdLogMessage), NpgsqlDbType.Uuid },
			{ nameof(ILogMessage<TIdentity>.IdLogLevel), NpgsqlDbType.Integer },
			{ nameof(ILogMessage<TIdentity>.CreatedUtc), NpgsqlDbType.TimestampTz },
			{ nameof(ILogMessage<TIdentity>.TraceInfo.SourceSystemName), NpgsqlDbType.Varchar },
			{ nameof(ILogMessage<TIdentity>.TraceInfo.RuntimeUniqueKey), NpgsqlDbType.Uuid },
			{ nameof(ILogMessage<TIdentity>.LogCode), NpgsqlDbType.Varchar },
			{ nameof(ILogMessage<TIdentity>.ClientMessage), NpgsqlDbType.Varchar },
			{ nameof(ILogMessage<TIdentity>.InternalMessage), NpgsqlDbType.Varchar },
			{ nameof(ILogMessage<TIdentity>.TraceInfo.TraceFrame.MethodCallId), NpgsqlDbType.Uuid },
			{ Serilog.Core.Constants.SourceContextPropertyName, NpgsqlDbType.Varchar },
			{ nameof(ILogMessage<TIdentity>.TraceInfo.TraceFrame), NpgsqlDbType.Varchar },
			{ nameof(ILogMessage<TIdentity>.StackTrace), NpgsqlDbType.Varchar },
			{ nameof(ILogMessage<TIdentity>.Detail), NpgsqlDbType.Varchar },
			{ nameof(ILogMessage<TIdentity>.TraceInfo.IdUser), NpgsqlDbTypeHelper.GetNpgsqlDbType<TIdentity>() },
			{ nameof(ILogMessage<TIdentity>.CommandQueryName), NpgsqlDbType.Varchar },
			{ nameof(ILogMessage<TIdentity>.IdCommandQuery), NpgsqlDbType.Uuid },
			{ nameof(ILogMessage<TIdentity>.MethodCallElapsedMilliseconds), NpgsqlDbType.Numeric },
			{ nameof(ILogMessage<TIdentity>.PropertyName), NpgsqlDbType.Varchar },
			{ nameof(ILogMessage<TIdentity>.DisplayPropertyName), NpgsqlDbType.Varchar },
			{ nameof(ILogMessage<TIdentity>.ValidationFailure), NpgsqlDbType.Varchar },
			{ nameof(ILogMessage<TIdentity>.IsValidationError), NpgsqlDbType.Boolean },
			{ nameof(ILogMessage<TIdentity>.CustomData), NpgsqlDbType.Varchar }, //json
			{ nameof(ILogMessage<TIdentity>.Tags), NpgsqlDbType.Varchar }, //json
			{ nameof(ILogMessage<TIdentity>.TraceInfo.CorrelationId), NpgsqlDbType.Uuid }
		};
	}
}
