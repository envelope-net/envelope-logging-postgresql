using Envelope.Hardware;
using Envelope.Infrastructure;

namespace Envelope.Logging.PostgreSql;

public interface IDbLogWriter : IDisposable
{
	void WriteEnvironmentInfo();
	void WriteEnvironmentInfo(EnvironmentInfo environmentInfo);
	void WriteHardwareInfo(HardwareInfo hardwareInfo);
}
