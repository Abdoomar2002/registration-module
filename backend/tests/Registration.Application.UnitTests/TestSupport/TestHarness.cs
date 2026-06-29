using Microsoft.EntityFrameworkCore;
using Registration.Application.Common.Interfaces;
using Registration.Domain.Lookups;

namespace Registration.Application.UnitTests.TestSupport;

/// <summary>Fixed clock for deterministic time-based rules in tests.</summary>
public sealed class FixedDateTimeProvider : IDateTimeProvider
{
    public FixedDateTimeProvider(DateOnly today) => Today = today;

    public DateTime UtcNow => Today.ToDateTime(TimeOnly.MinValue, DateTimeKind.Utc);

    public DateOnly Today { get; }
}

/// <summary>No-op integration event publisher that records what was published.</summary>
public sealed class RecordingIntegrationEventPublisher : IIntegrationEventPublisher
{
    public List<object> Published { get; } = new();

    public Task PublishAsync<TEvent>(TEvent integrationEvent, CancellationToken cancellationToken = default)
        where TEvent : class
    {
        Published.Add(integrationEvent);
        return Task.CompletedTask;
    }
}

/// <summary>Helpers to spin up a seeded InMemory context for Application tests.</summary>
public static class TestHarness
{
    public static readonly DateOnly Today = new(2026, 6, 29);

    public static TestApplicationDbContext CreateContext(bool seedLookups = true)
    {
        var options = new DbContextOptionsBuilder<TestApplicationDbContext>()
            .UseInMemoryDatabase($"app-tests-{Guid.NewGuid()}")
            .EnableSensitiveDataLogging()
            .Options;

        var context = new TestApplicationDbContext(options);

        if (seedLookups)
        {
            context.Governorates.Add(new Governorate(1, "Cairo", "القاهرة"));
            context.Governorates.Add(new Governorate(2, "Giza", "الجيزة"));
            context.Cities.Add(new City(1, 1, "Nasr City", "مدينة نصر"));
            context.Cities.Add(new City(2, 1, "Maadi", "المعادي"));
            context.Cities.Add(new City(3, 2, "Dokki", "الدقي")); // belongs to Giza
            context.SaveChanges();
        }

        return context;
    }
}
