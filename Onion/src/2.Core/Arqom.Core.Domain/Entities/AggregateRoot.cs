using Arqom.Core.Domain.Events;
using Arqom.Core.Domain.Exceptions;
using System.Collections.Immutable;

namespace Arqom.Core.Domain.Entities;

public abstract class AggregateRoot<TId> : Entity<TId>, IAggregateRoot
{
    private readonly List<IDomainEvent> _events = new();

    protected AggregateRoot() { }

    /// <summary>
    /// Constructor مخصوص Event Replay
    /// </summary>
    protected AggregateRoot(IEnumerable<IDomainEvent> events)
    {
        if (events == null) return;

        foreach (var @event in events)
        {
            When(@event);
        }
    }

    // گزینه ۱: فقط ثبت ایونت (برای پروژه‌های معمولی)
    protected void AddEvent(IDomainEvent @event)
    {
        _events.Add(@event);
    }

    /// <summary>
    /// Apply + Register new domain event
    /// </summary>
    protected void RaiseEvent(IDomainEvent @event)
    {
        When(@event);
        _events.Add(@event);
    }

    /// <summary>
    /// Mutates state without registering event (Replay)
    /// </summary>
    private void When(IDomainEvent @event)
    {
        var handler = GetType()
            .GetMethod(
                "On",
                System.Reflection.BindingFlags.Instance |
                System.Reflection.BindingFlags.NonPublic,
                new[] { @event.GetType() }
            );

        if (handler is null)
            throw new InvalidEntityStateException(@event.GetType().ToString()
                //GetType(), @event.GetType()
            );

        handler.Invoke(this, new object[] { @event });
    }

    // ۲. پیاده‌سازی صریح برای پاس کردن الزامات اینترفیس
    IEnumerable<IDomainEvent> IAggregateRoot.GetEvents() => _events.ToImmutableList();

    public void ClearEvents() => _events.Clear();
}
