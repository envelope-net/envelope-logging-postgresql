using NpgsqlTypes;
using Envelope.Data;
using Envelope.Database.PostgreSql;
using Envelope.Audit;

namespace Envelope.Logging.PostgreSql;

public class DBApplicationEntryTokenSinkOptions : DbBatchWriterOptions, IBatchWriterOptions
{
	public DBApplicationEntryTokenSinkOptions()
	{
		TableName = nameof(ApplicationEntryToken);

		PropertyNames = new List<string>
		{
			nameof(ApplicationEntryToken.IdApplicationEntryToken),
			nameof(ApplicationEntryToken.Token),
			nameof(ApplicationEntryToken.Version),
			nameof(ApplicationEntryToken.SourceFilePath),
			nameof(ApplicationEntryToken.MethodInfo),
			nameof(ApplicationEntryToken.MainEntityName),
			nameof(ApplicationEntryToken.Description),
			nameof(ApplicationEntryToken.TokenHistory)
		};

		PropertyTypeMapping = new Dictionary<string, NpgsqlDbType>
		{
			{ nameof(ApplicationEntryToken.IdApplicationEntryToken), NpgsqlDbType.Uuid },
			{ nameof(ApplicationEntryToken.Token), NpgsqlDbType.Varchar },
			{ nameof(ApplicationEntryToken.Version), NpgsqlDbType.Integer },
			{ nameof(ApplicationEntryToken.SourceFilePath), NpgsqlDbType.Varchar },
			{ nameof(ApplicationEntryToken.MethodInfo), NpgsqlDbType.Varchar },
			{ nameof(ApplicationEntryToken.MainEntityName), NpgsqlDbType.Varchar },
			{ nameof(ApplicationEntryToken.Description), NpgsqlDbType.Varchar },
			{ nameof(ApplicationEntryToken.TokenHistory), NpgsqlDbType.Varchar }
		};
	}
}
