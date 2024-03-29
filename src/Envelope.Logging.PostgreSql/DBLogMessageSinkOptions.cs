﻿using NpgsqlTypes;
using Envelope.Data;
using Envelope.Database.PostgreSql;

namespace Envelope.Logging.PostgreSql;

public class DBLogMessageSinkOptions : DbBatchWriterOptions, IBatchWriterOptions
{
	public DBLogMessageSinkOptions()
	{
		PropertyNames = new List<string>
		{
			nameof(ILogMessage.IdLogMessage),
			nameof(ILogMessage.IdLogLevel),
			nameof(ILogMessage.CreatedUtc),
			nameof(ILogMessage.TraceInfo.SourceSystemName),
			nameof(ILogMessage.TraceInfo.RuntimeUniqueKey),
			nameof(ILogMessage.LogCode),
			nameof(ILogMessage.ClientMessage),
			nameof(ILogMessage.InternalMessage),
			nameof(ILogMessage.TraceInfo.TraceFrame.MethodCallId),
			Serilog.Core.Constants.SourceContextPropertyName,
			nameof(ILogMessage.TraceInfo.TraceFrame),
			nameof(ILogMessage.StackTrace),
			nameof(ILogMessage.Detail),
			nameof(ILogMessage.TraceInfo.IdUser),
			nameof(ILogMessage.CommandQueryName),
			nameof(ILogMessage.IdCommandQuery),
			nameof(ILogMessage.MethodCallElapsedMilliseconds),
			nameof(ILogMessage.PropertyName),
			nameof(ILogMessage.DisplayPropertyName),
			nameof(ILogMessage.ValidationFailure),
			nameof(ILogMessage.IsValidationError),
			nameof(ILogMessage.TraceInfo.ContextProperties),
			nameof(ILogMessage.Tags),
			nameof(ILogMessage.TraceInfo.CorrelationId)
		};

		PropertyTypeMapping = new Dictionary<string, NpgsqlDbType>
		{
			{ nameof(ILogMessage.IdLogMessage), NpgsqlDbType.Uuid },
			{ nameof(ILogMessage.IdLogLevel), NpgsqlDbType.Integer },
			{ nameof(ILogMessage.CreatedUtc), NpgsqlDbType.TimestampTz },
			{ nameof(ILogMessage.TraceInfo.SourceSystemName), NpgsqlDbType.Varchar },
			{ nameof(ILogMessage.TraceInfo.RuntimeUniqueKey), NpgsqlDbType.Uuid },
			{ nameof(ILogMessage.LogCode), NpgsqlDbType.Varchar },
			{ nameof(ILogMessage.ClientMessage), NpgsqlDbType.Varchar },
			{ nameof(ILogMessage.InternalMessage), NpgsqlDbType.Varchar },
			{ nameof(ILogMessage.TraceInfo.TraceFrame.MethodCallId), NpgsqlDbType.Uuid },
			{ Serilog.Core.Constants.SourceContextPropertyName, NpgsqlDbType.Varchar },
			{ nameof(ILogMessage.TraceInfo.TraceFrame), NpgsqlDbType.Varchar },
			{ nameof(ILogMessage.StackTrace), NpgsqlDbType.Varchar },
			{ nameof(ILogMessage.Detail), NpgsqlDbType.Varchar },
			{ nameof(ILogMessage.TraceInfo.IdUser), NpgsqlDbTypeHelper.GetNpgsqlDbType<Guid>() },
			{ nameof(ILogMessage.CommandQueryName), NpgsqlDbType.Varchar },
			{ nameof(ILogMessage.IdCommandQuery), NpgsqlDbType.Uuid },
			{ nameof(ILogMessage.MethodCallElapsedMilliseconds), NpgsqlDbType.Numeric },
			{ nameof(ILogMessage.PropertyName), NpgsqlDbType.Varchar },
			{ nameof(ILogMessage.DisplayPropertyName), NpgsqlDbType.Varchar },
			{ nameof(ILogMessage.ValidationFailure), NpgsqlDbType.Varchar },
			{ nameof(ILogMessage.IsValidationError), NpgsqlDbType.Boolean },
			{ nameof(ILogMessage.TraceInfo.ContextProperties), NpgsqlDbType.Varchar }, //json
			{ nameof(ILogMessage.Tags), NpgsqlDbType.Varchar }, //json
			{ nameof(ILogMessage.TraceInfo.CorrelationId), NpgsqlDbType.Uuid }
		};
	}
}
