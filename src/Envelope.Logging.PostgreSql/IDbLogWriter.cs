using Envelope.Audit;
using Envelope.Hardware;
using Envelope.Infrastructure;

namespace Envelope.Logging.PostgreSql;

public interface IDbLogWriter : IDisposable
{
	void WriteEnvironmentInfo(string applicationName);
	void WriteEnvironmentInfo(EnvironmentInfo environmentInfo);
	void WriteHardwareInfo(HardwareInfo hardwareInfo);
	Task<ApplicationEntryToken?> GetApplicationEntryTokenAsync(string token, int version, string sourceFilePath, CancellationToken cancellationToken = default);
	void WriteApplicationEntryToken(ApplicationEntryToken applicationEntryToken);
	Task UpdateApplicationEntryTokenAsync(ApplicationEntryToken applicationEntryToken, CancellationToken cancellationToken = default);
	void WriteApplicationEntry(ApplicationEntry applicationEntry);
}
