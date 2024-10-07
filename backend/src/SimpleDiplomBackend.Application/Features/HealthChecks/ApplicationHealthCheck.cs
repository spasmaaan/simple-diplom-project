﻿using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Reflection;

namespace SimpleDiplomBackend.Application.Features.HealthChecks
{
    public class ApplicationHealthCheck : IHealthCheck
    {
        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            var assembly = Assembly.Load("SimpleDiplomBackend.Api");
            var versionNumber = assembly.GetName().Version;

            return Task.FromResult(HealthCheckResult.Healthy(description: $"Build {versionNumber}"));
        }
    }
}