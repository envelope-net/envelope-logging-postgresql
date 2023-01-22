using Envelope.Audit;
using Envelope.Hardware;
using Envelope.Infrastructure;

namespace Envelope.Logging.PostgreSql.Internal;

internal class SilentDbLogWriter : IDbLogWriter, IDisposable
{
	public static readonly IDbLogWriter Instance = new SilentDbLogWriter();

	public void WriteEnvironmentInfo(string applicationName) { }

	public void WriteEnvironmentInfo(EnvironmentInfo environmentInfo) { }

	public void WriteHardwareInfo(HardwareInfo hardwareInfo) { }

	public Task<ApplicationEntryToken?> GetApplicationEntryTokenAsync(string token, int version, string sourceFilePath, CancellationToken cancellationToken = default)
		=> Task.FromResult((ApplicationEntryToken?)null);

	public void WriteApplicationEntryToken(ApplicationEntryToken applicationEntryToken) { }

	public Task UpdateApplicationEntryTokenAsync(ApplicationEntryToken applicationEntryToken, CancellationToken cancellationToken = default)
		=> Task.CompletedTask;

	public void WriteApplicationEntry(ApplicationEntry applicationEntry) { }

	public void Dispose() { }
}
