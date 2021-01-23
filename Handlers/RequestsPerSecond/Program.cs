using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace RequestsPerSecond
{
    //https://github.com/dotnet/EntityFramework.Docs/tree/master/samples/core/DbContextPooling
    class Program
    {
        private const int Threads = 32;
        private const int Seconds = 10;

        private static long _requestsProcessed;

        private static async Task Main()
        {
            var stopwatch = new Stopwatch();

            var monitorTask = MonitorResults(TimeSpan.FromSeconds(Seconds), stopwatch);

            await Task.WhenAll(
                Enumerable
                    .Range(0, Threads)
                    .Select(_ => SimulateRequestsAsync(stopwatch)));

            await monitorTask;
        }

        private static async Task SimulateRequestsAsync(Stopwatch stopwatch)
        {
            while (stopwatch.IsRunning)
            {
                var client = new HttpClient();
                var response = await client.GetAsync("https://localhost:5001/orders/2");

                Interlocked.Increment(ref _requestsProcessed);
            }
        }

        private static async Task MonitorResults(TimeSpan duration, Stopwatch stopwatch)
        {
            var lastRequestCount = 0L;
            var lastElapsed = TimeSpan.Zero;

            stopwatch.Start();

            while (stopwatch.Elapsed < duration)
            {
                await Task.Delay(TimeSpan.FromSeconds(1));

                var requestCount = _requestsProcessed;
                var elapsed = stopwatch.Elapsed;
                var currentElapsed = elapsed - lastElapsed;
                var currentRequests = requestCount - lastRequestCount;

                Console.WriteLine(
                    $"[{DateTime.Now:HH:mm:ss.fff}] "
                    + $"Requests/second: {Math.Round(currentRequests / currentElapsed.TotalSeconds)}");

                lastRequestCount = requestCount;
                lastElapsed = elapsed;
            }

            Console.WriteLine();
            Console.WriteLine(
                $"Requests per second:     {Math.Round(_requestsProcessed / stopwatch.Elapsed.TotalSeconds)}");

            stopwatch.Stop();
        }
    }
}
