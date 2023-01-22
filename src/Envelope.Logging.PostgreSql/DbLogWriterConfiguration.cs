namespace Envelope.Logging.PostgreSql;

public class DbLogWriterConfiguration
{
	protected EnvironmentInfoWriter? _environmentInfoWriter;
	protected HardwareInfoWriter? _hardwareInfoWriter;
	protected ApplicationEntryTokenWriter? _applicationEntryTokenWriter;
	protected ApplicationEntryWriter? _applicationEntryWriter;

	public DbLogWriterConfiguration SetEnvironmentInfoWriter(DBEnvironmentInfoSinkOptions options)
	{
		_environmentInfoWriter = new EnvironmentInfoWriter(options);
		return this;
	}

	public DbLogWriterConfiguration SetHardwareInfoWriter(DBHardwareInfoSinkOptions options)
	{
		_hardwareInfoWriter = new HardwareInfoWriter(options);
		return this;
	}

	public DbLogWriterConfiguration SetApplicationEntryWriter(DBApplicationEntrySinkOptions options)
	{
		_applicationEntryWriter = new ApplicationEntryWriter(options);
		return this;
	}

	public DbLogWriterConfiguration SetApplicationEntryTokenWriter(DBApplicationEntryTokenSinkOptions options)
	{
		_applicationEntryTokenWriter = new ApplicationEntryTokenWriter(options);
		return this;
	}

	public DbLogWriter? CreateDbLogWriter()
	{
		if (_environmentInfoWriter == null && _hardwareInfoWriter == null && _applicationEntryTokenWriter == null && _applicationEntryWriter == null)
			return null;

		return new DbLogWriter(_environmentInfoWriter, _hardwareInfoWriter, _applicationEntryTokenWriter, _applicationEntryWriter);
	}
}
