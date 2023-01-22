using Envelope.Audit;
using Envelope.Database.PostgreSql;

namespace Envelope.Logging.PostgreSql;

public class ApplicationEntryWriter : DbBatchWriter<ApplicationEntry>, IDisposable
{
	public ApplicationEntryWriter(DBApplicationEntrySinkOptions options, Action<string, object?, object?, object?>? errorLogger = null)
		: base(options ?? new DBApplicationEntrySinkOptions(), errorLogger ?? DefaultErrorLoggerDelegate.Log)
	{
	}

	public override IDictionary<string, object?>? ToDictionary(ApplicationEntry applicationEntry)
		=> applicationEntry.ToDictionary();
}
