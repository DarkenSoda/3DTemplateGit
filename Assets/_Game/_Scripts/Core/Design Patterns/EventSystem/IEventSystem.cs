namespace Game.Core.DesignPatterns.EventSystem
{
    /// <summary>
    /// Contract for publishing events into the event bus.
    /// </summary>
    public interface IEventPublisher
    {
        void Publish<TEvent>(in TEvent evt) where TEvent : IEvent;
    }

    /// <summary>
    /// Contract for subscribing and unsubscribing to events from the event bus.
    /// </summary>
    public interface IEventSubscriber
    {
        void Subscribe<TEvent>(EventDelegate<TEvent> callback) where TEvent : IEvent;
        void Unsubscribe<TEvent>(EventDelegate<TEvent> callback) where TEvent : IEvent;

        /// <summary>
        /// Removes every callback registered for <typeparamref name="TEvent"/>.
        /// Useful for bulk-cleanup on scene unload.
        /// </summary>
        void UnsubscribeAll<TEvent>() where TEvent : IEvent;
    }

    /// <summary>
    /// Combined interface — inject this into services and MonoBehaviours
    /// that need both publish and subscribe access.
    /// </summary>
    public interface IEventSystem : IEventPublisher, IEventSubscriber { }
}
