using NpgsqlTypes;
using Envelope.Data;
using Envelope.Database.PostgreSql;
using Envelope.Audit;

namespace Envelope.Logging.PostgreSql;

public class DBApplicationEntrySinkOptions : DbBatchWriterOptions, IBatchWriterOptions
{
	public DBApplicationEntrySinkOptions()
	{
		TableName = nameof(ApplicationEntry);

		PropertyNames = new List<string>
		{
			nameof(ApplicationEntry.IdApplicationEntryToken),
			nameof(ApplicationEntry.AuditOperation),
			nameof(ApplicationEntry.RuntimeUniqueKey),
			nameof(ApplicationEntry.CreatedUtc),
			nameof(ApplicationEntry.CorrelationId),
			nameof(ApplicationEntry.ExternalCorrelationId),
			nameof(ApplicationEntry.MainEntityIdentifier),
			nameof(ApplicationEntry.Uri),
			nameof(ApplicationEntry.IdUser)
		};

		PropertyTypeMapping = new Dictionary<string, NpgsqlDbType>
		{
			{ nameof(ApplicationEntry.IdApplicationEntryToken), NpgsqlDbType.Uuid },
			{ nameof(ApplicationEntry.AuditOperation), NpgsqlDbType.Integer },
			{ nameof(ApplicationEntry.RuntimeUniqueKey), NpgsqlDbType.Uuid },
			{ nameof(ApplicationEntry.CreatedUtc), NpgsqlDbType.TimestampTz },
			{ nameof(ApplicationEntry.CorrelationId), NpgsqlDbType.Uuid },
			{ nameof(ApplicationEntry.ExternalCorrelationId), NpgsqlDbType.Varchar },
			{ nameof(ApplicationEntry.MainEntityIdentifier), NpgsqlDbType.Varchar },
			{ nameof(ApplicationEntry.Uri), NpgsqlDbType.Varchar },
			{ nameof(ApplicationEntry.IdUser), NpgsqlDbType.Uuid }
		};
	}
}
