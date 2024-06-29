﻿using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace OpenObsere.Sample;

public class CustomBackgroundService : IHostedService, IDisposable
{
    private int _executionCount;
    private readonly ILogger<CustomBackgroundService> _logger;
    private readonly Timer _timer;

    public CustomBackgroundService(ILogger<CustomBackgroundService> logger)
    {
        _logger = logger;
        _timer = new Timer(DoWork, null, TimeSpan.Zero,
            TimeSpan.FromSeconds(3));
    }
    
    public Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogDebug("Debug message");
        
        _logger.LogInformation("{ServiceName}: StartAsync", nameof(CustomBackgroundService));
        Console.WriteLine($"{nameof(CustomBackgroundService)}: StartAsync");
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("{ServiceName}: StopAsync", nameof(CustomBackgroundService));
        Console.WriteLine($"{nameof(CustomBackgroundService)}: StopAsync");
        return Task.CompletedTask;
    }
    
    private void DoWork(object? state)
    {
        var count = Interlocked.Increment(ref _executionCount);

        _logger.LogInformation(
            "Timed Hosted Service is working. Count: {Count}", count);
        Console.WriteLine($"Timed Hosted Service is working. Count: {count}");
        
    }
    
    public void Dispose()
    {
        _timer.Dispose();
    }
}