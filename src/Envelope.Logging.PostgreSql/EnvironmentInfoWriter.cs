using Envelope.Database.PostgreSql;
using Envelope.Infrastructure;

namespace Envelope.Logging.PostgreSql;

public class EnvironmentInfoWriter : DbBatchWriter<EnvironmentInfo>, IDisposable
{
	public EnvironmentInfoWriter(DBEnvironmentInfoSinkOptions options, Action<string, object?, object?, object?>? errorLogger = null)
		: base(options ?? new DBEnvironmentInfoSinkOptions(), errorLogger ?? DefaultErrorLoggerDelegate.Log)
	{
	}

	public override IDictionary<string, object?>? ToDictionary(EnvironmentInfo environmentInfo)
		=> environmentInfo.ToDictionary();
}
