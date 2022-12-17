using Envelope.Audit;
using Envelope.Database.PostgreSql;
using Envelope.Extensions;
using Npgsql;
using NpgsqlTypes;

namespace Envelope.Logging.PostgreSql;

public class ApplicationEntryTokenWriter : DbBatchWriter<ApplicationEntryToken>, IDisposable
{
	private readonly string? _connectionString;
	private readonly string? _tableName;

	public ApplicationEntryTokenWriter(DBApplicationEntryTokenSinkOptions options, Action<string, object?, object?, object?>? errorLogger = null)
		: base(options ?? new DBApplicationEntryTokenSinkOptions(), errorLogger ?? DefaultErrorLoggerDelegate.Log)
	{
		_connectionString = options!.ConnectionString;
		_tableName = options.UseQuotationMarksForTableName
			? $"{options.SchemaName}.\"{options.TableName}\""
			: $"{options.SchemaName}.{options.TableName}";
	}

	public override IDictionary<string, object?>? ToDictionary(ApplicationEntryToken applicationEntryToken)
		=> applicationEntryToken.ToDictionary();

	public async Task<ApplicationEntryToken?> GetApplicationEntryTokenAsync(string token, int version, string sourceFilePath, CancellationToken cancellationToken = default)
	{
		await using var connection = new NpgsqlConnection(_connectionString);
		await connection.OpenAsync(cancellationToken).ConfigureAwait(false);

		var columns = BulkInsert.UseQuotationMarksForColumnNames
			? $"\"{nameof(ApplicationEntryToken.IdApplicationEntryToken)}\", \"{nameof(ApplicationEntryToken.Token)}\", \"{nameof(ApplicationEntryToken.Version)}\", \"{nameof(ApplicationEntryToken.SourceFilePath)}\", \"{nameof(ApplicationEntryToken.MethodInfo)}\", \"{nameof(ApplicationEntryToken.MainEntityName)}\", \"{nameof(ApplicationEntryToken.Description)}\", \"{nameof(ApplicationEntryToken.TokenHistory)}\""
			: $"{nameof(ApplicationEntryToken.IdApplicationEntryToken)}, {nameof(ApplicationEntryToken.Token)}, {nameof(ApplicationEntryToken.Version)}, {nameof(ApplicationEntryToken.SourceFilePath)}, {nameof(ApplicationEntryToken.MethodInfo)}, {nameof(ApplicationEntryToken.MainEntityName)}, {nameof(ApplicationEntryToken.Description)}, {nameof(ApplicationEntryToken.TokenHistory)}";

		var cmd = BulkInsert.UseQuotationMarksForColumnNames
			? $"SELECT {columns} FROM {_tableName} WHERE \"{nameof(ApplicationEntryToken.Token)}\" = @token AND \"{nameof(ApplicationEntryToken.Version)}\" = @vers AND \"{nameof(ApplicationEntryToken.SourceFilePath)}\" = @source"
			: $"SELECT {columns} FROM {_tableName} WHERE {nameof(ApplicationEntryToken.Token)} = @token AND {nameof(ApplicationEntryToken.Version)} = @vers AND {nameof(ApplicationEntryToken.SourceFilePath)} = @source";

#if NET6_0_OR_GREATER
		await
#endif
		using var npgsqlCommand = new NpgsqlCommand(cmd, connection);
		npgsqlCommand.Parameters.AddWithValue("@token", NpgsqlDbType.Varchar, token);
		npgsqlCommand.Parameters.AddWithValue("@vers", NpgsqlDbType.Integer, version);
		npgsqlCommand.Parameters.AddWithValue("@source", NpgsqlDbType.Varchar, sourceFilePath);

		var reader = await npgsqlCommand.ExecuteReaderAsync(cancellationToken).ConfigureAwait(false);

		ApplicationEntryToken? applicationEntryToken = null;
		if (await reader.ReadAsync(cancellationToken))
		{
			var id = reader.GetGuid(0);
			token = reader.GetString(1);
			version = reader.GetInt32(2);
			sourceFilePath = reader.GetString(3);

			applicationEntryToken = new ApplicationEntryToken(id, token, version, sourceFilePath)
			{
				MethodInfo = reader.GetNullableString(3),
				MainEntityName = reader.GetNullableString(4),
				Description = reader.GetNullableString(5),
				TokenHistory = reader.GetNullableString(6)
			};
		}

		return applicationEntryToken;
	}

	public async Task UpdateApplicationEntryTokenAsync(ApplicationEntryToken applicationEntryToken, CancellationToken cancellationToken = default)
	{
		if (applicationEntryToken == null)
			throw new ArgumentNullException(nameof(applicationEntryToken));

		await using var connection = new NpgsqlConnection(_connectionString);
		await connection.OpenAsync(cancellationToken).ConfigureAwait(false);

		var columns = BulkInsert.UseQuotationMarksForColumnNames
			? $"\"{nameof(ApplicationEntryToken.MethodInfo)}\" = @method, \"{nameof(ApplicationEntryToken.MainEntityName)}\" = @entity, \"{nameof(ApplicationEntryToken.Description)}\" = @desc, \"{nameof(ApplicationEntryToken.TokenHistory)}\" = @hist"
			: $"{nameof(ApplicationEntryToken.MethodInfo)} = @method, {nameof(ApplicationEntryToken.MainEntityName)} = @entity, {nameof(ApplicationEntryToken.Description)} = @desc, {nameof(ApplicationEntryToken.TokenHistory)} = @hist";

		var cmd = BulkInsert.UseQuotationMarksForColumnNames
			? $"UPDATE {_tableName} SET {columns} WHERE \"{nameof(ApplicationEntryToken.IdApplicationEntryToken)}\" = @id"
			: $"UPDATE {_tableName} SET {columns} WHERE {nameof(ApplicationEntryToken.IdApplicationEntryToken)} = @id";

#if NET6_0_OR_GREATER
		await
#endif
		using var npgsqlCommand = new NpgsqlCommand(cmd, connection);
		npgsqlCommand.Parameters.AddWithValue("@id", NpgsqlDbType.Uuid, (object?)applicationEntryToken.IdApplicationEntryToken ?? DBNull.Value);
		npgsqlCommand.Parameters.AddWithValue("@method", NpgsqlDbType.Varchar, (object?)applicationEntryToken.MethodInfo ?? DBNull.Value);
		npgsqlCommand.Parameters.AddWithValue("@entity", NpgsqlDbType.Varchar, (object?)applicationEntryToken.MainEntityName ?? DBNull.Value);
		npgsqlCommand.Parameters.AddWithValue("@desc", NpgsqlDbType.Varchar, (object?)applicationEntryToken.Description ?? DBNull.Value);
		npgsqlCommand.Parameters.AddWithValue("@hist", NpgsqlDbType.Varchar, (object?)applicationEntryToken.TokenHistory ?? DBNull.Value);

		await npgsqlCommand.ExecuteNonQueryAsync(cancellationToken).ConfigureAwait(false);
	}
}
