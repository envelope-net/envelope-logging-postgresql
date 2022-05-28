using Envelope.Hardware;
using Envelope.Infrastructure;

namespace Envelope.Logging.PostgreSql.Internal;

internal class SilentDbLogWriter : IDbLogWriter, IDisposable
{
	public static readonly IDbLogWriter Instance = new SilentDbLogWriter();

	public void WriteEnvironmentInfo() { }

	public void WriteEnvironmentInfo(EnvironmentInfo environmentInfo) { }

	public void WriteHardwareInfo(HardwareInfo hardwareInfo) { }

	public void Dispose() { }
}
