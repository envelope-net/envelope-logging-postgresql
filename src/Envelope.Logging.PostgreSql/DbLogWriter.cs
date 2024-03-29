﻿using Envelope.Audit;
using Envelope.Extensions;
using Envelope.Hardware;
using Envelope.Infrastructure;
using Envelope.Logging.PostgreSql.Internal;

namespace Envelope.Logging.PostgreSql;

public class DbLogWriter : IDbLogWriter, IDisposable
{
	private static IDbLogWriter _instance = SilentDbLogWriter.Instance;

	public static IDbLogWriter Instance
	{
		get => _instance;
		set => _instance = value ?? throw new ArgumentNullException(nameof(value));
	}

	private readonly EnvironmentInfoWriter? _environmentInfoWriter;
	private readonly HardwareInfoWriter? _hardwareInfoWriter;
	private readonly ApplicationEntryTokenWriter? _applicationEntryTokenWriter;
	private readonly ApplicationEntryWriter? _applicationEntryWriter;

	internal DbLogWriter(
		EnvironmentInfoWriter? environmentInfoWriter,
		HardwareInfoWriter? hardwareInfoWriter,
		ApplicationEntryTokenWriter? applicationEntryTokenWriter,
		ApplicationEntryWriter? applicationEntryWriter)
	{
		_environmentInfoWriter = environmentInfoWriter;
		_hardwareInfoWriter = hardwareInfoWriter;
		_applicationEntryTokenWriter = applicationEntryTokenWriter;
		_applicationEntryWriter = applicationEntryWriter;
	}

	public void WriteEnvironmentInfo(string applicationName)
		=> WriteEnvironmentInfo(EnvironmentInfoProvider.GetEnvironmentInfo(applicationName));

	public void WriteEnvironmentInfo(EnvironmentInfo environmentInfo)
	{
		if (_environmentInfoWriter == null)
			throw new InvalidOperationException($"{nameof(EnvironmentInfoWriter)} was not configured");

		_environmentInfoWriter.Write(environmentInfo);
	}

	public void WriteHardwareInfo(HardwareInfo hardwareInfo)
	{
		if (hardwareInfo == null)
			return;

		if (_hardwareInfoWriter == null)
			throw new InvalidOperationException($"{nameof(HardwareInfoWriter)} was not configured");

		_hardwareInfoWriter.Write(hardwareInfo);
	}

	public Task<ApplicationEntryToken?> GetApplicationEntryTokenAsync(string token, int version, string sourceFilePath, CancellationToken cancellationToken = default)
	{
		if (_applicationEntryTokenWriter == null)
			throw new InvalidOperationException($"{nameof(ApplicationEntryTokenWriter)} was not configured");

		return _applicationEntryTokenWriter.GetApplicationEntryTokenAsync(token, version, sourceFilePath, cancellationToken);
	}

	public void WriteApplicationEntryToken(ApplicationEntryToken applicationEntryToken)
	{
		if (_applicationEntryTokenWriter == null)
			throw new InvalidOperationException($"{nameof(ApplicationEntryTokenWriter)} was not configured");

		_applicationEntryTokenWriter.Write(applicationEntryToken);
	}

	public Task UpdateApplicationEntryTokenAsync(ApplicationEntryToken applicationEntryToken, CancellationToken cancellationToken = default)
	{
		if (_applicationEntryTokenWriter == null)
			throw new InvalidOperationException($"{nameof(ApplicationEntryTokenWriter)} was not configured");

		return _applicationEntryTokenWriter.UpdateApplicationEntryTokenAsync(applicationEntryToken, cancellationToken);
	}

	public void WriteApplicationEntry(ApplicationEntry applicationEntry)
	{
		if (_applicationEntryWriter == null)
			throw new InvalidOperationException($"{nameof(ApplicationEntryWriter)} was not configured");

		_applicationEntryWriter.Write(applicationEntry);
	}

	public static void CloseAndFlush()
	{
		var dbLogWriter = Interlocked.Exchange(ref _instance, SilentDbLogWriter.Instance);
		dbLogWriter?.Dispose();
	}

	private bool _disposed;
	protected virtual void Dispose(bool disposing)
	{
		if (_disposed)
			return;

		_disposed = true;

		if (disposing)
		{
			try
			{
				_environmentInfoWriter?.Dispose();
			}
			catch (Exception ex)
			{
				var msg = string.Format($"{nameof(LogWriter)}: Disposing {nameof(_environmentInfoWriter)} '{_environmentInfoWriter?.GetType().FullName ?? "null"}': {ex.ToStringTrace()}");
				Serilog.Log.Logger.Error(ex, msg);
			}

			try
			{
				_hardwareInfoWriter?.Dispose();
			}
			catch (Exception ex)
			{
				var msg = string.Format($"{nameof(LogWriter)}: Disposing {nameof(_hardwareInfoWriter)} '{_hardwareInfoWriter?.GetType().FullName ?? "null"}': {ex.ToStringTrace()}");
				Serilog.Log.Logger.Error(ex, msg);
			}

			try
			{
				_applicationEntryWriter?.Dispose();
			}
			catch (Exception ex)
			{
				var msg = string.Format($"{nameof(LogWriter)}: Disposing {nameof(_applicationEntryWriter)} '{_applicationEntryWriter?.GetType().FullName ?? "null"}': {ex.ToStringTrace()}");
				Serilog.Log.Logger.Error(ex, msg);
			}
		}
	}

	public void Dispose()
	{
		Dispose(true);
		GC.SuppressFinalize(this);
	}
}
