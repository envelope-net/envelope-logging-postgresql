using NpgsqlTypes;
using Envelope.Data;
using Envelope.Database.PostgreSql;
using Envelope.Infrastructure;

namespace Envelope.Logging.PostgreSql;

public class DBEnvironmentInfoSinkOptions : DbBatchWriterOptions, IBatchWriterOptions
{
	public DBEnvironmentInfoSinkOptions()
	{
		TableName = nameof(EnvironmentInfo);

		PropertyNames = new List<string>
		{
			nameof(EnvironmentInfo.RuntimeUniqueKey),
			nameof(EnvironmentInfo.CreatedUtc),
			nameof(EnvironmentInfo.RunningEnvironment),
			nameof(EnvironmentInfo.FrameworkDescription),
			nameof(EnvironmentInfo.TargetFramework),
			nameof(EnvironmentInfo.CLRVersion),
			nameof(EnvironmentInfo.EntryAssemblyName),
			nameof(EnvironmentInfo.EntryAssemblyVersion),
			nameof(EnvironmentInfo.BaseDirectory),
			nameof(EnvironmentInfo.MachineName),
			nameof(EnvironmentInfo.ProcessName),
			nameof(EnvironmentInfo.ProcessId),
			nameof(EnvironmentInfo.CurrentAppDomainName),
			nameof(EnvironmentInfo.Is64BitOperatingSystem),
			nameof(EnvironmentInfo.Is64BitProcess),
			nameof(EnvironmentInfo.OperatingSystemArchitecture),
			nameof(EnvironmentInfo.OperatingSystemPlatform),
			nameof(EnvironmentInfo.OperatingSystemVersion),
			nameof(EnvironmentInfo.ProcessArchitecture),
			nameof(EnvironmentInfo.CommandLine)
		};

		PropertyTypeMapping = new Dictionary<string, NpgsqlDbType>
		{
			{ nameof(EnvironmentInfo.RuntimeUniqueKey), NpgsqlDbType.Uuid },
			{ nameof(EnvironmentInfo.CreatedUtc), NpgsqlDbType.TimestampTz },
			{ nameof(EnvironmentInfo.RunningEnvironment), NpgsqlDbType.Varchar },
			{ nameof(EnvironmentInfo.FrameworkDescription), NpgsqlDbType.Varchar },
			{ nameof(EnvironmentInfo.TargetFramework), NpgsqlDbType.Varchar },
			{ nameof(EnvironmentInfo.CLRVersion), NpgsqlDbType.Varchar },
			{ nameof(EnvironmentInfo.EntryAssemblyName), NpgsqlDbType.Varchar },
			{ nameof(EnvironmentInfo.EntryAssemblyVersion), NpgsqlDbType.Varchar },
			{ nameof(EnvironmentInfo.BaseDirectory), NpgsqlDbType.Varchar },
			{ nameof(EnvironmentInfo.MachineName), NpgsqlDbType.Varchar },
			{ nameof(EnvironmentInfo.ProcessName), NpgsqlDbType.Varchar },
			{ nameof(EnvironmentInfo.ProcessId), NpgsqlDbType.Integer },
			{ nameof(EnvironmentInfo.CurrentAppDomainName), NpgsqlDbType.Varchar },
			{ nameof(EnvironmentInfo.Is64BitOperatingSystem), NpgsqlDbType.Boolean },
			{ nameof(EnvironmentInfo.Is64BitProcess), NpgsqlDbType.Boolean },
			{ nameof(EnvironmentInfo.OperatingSystemArchitecture), NpgsqlDbType.Varchar },
			{ nameof(EnvironmentInfo.OperatingSystemPlatform), NpgsqlDbType.Varchar },
			{ nameof(EnvironmentInfo.OperatingSystemVersion), NpgsqlDbType.Varchar },
			{ nameof(EnvironmentInfo.ProcessArchitecture), NpgsqlDbType.Varchar },
			{ nameof(EnvironmentInfo.CommandLine), NpgsqlDbType.Varchar }
		};
	}
}
