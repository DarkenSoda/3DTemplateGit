using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Core.DesignPatterns.EventSystem
{
    /// <summary>
    /// Thread-safe, type-keyed pub-sub event bus.
    /// Registered as a Singleton in GameLifetimeScope so it persists
    /// across all scenes. Inject <see cref="IEventSystem"/> into your classes.
    /// </summary>
    public class EventBus : IEventSystem
    {
        // Each TEvent type maps to a Delegate (multicast) stored as object.
        private readonly Dictionary<Type, object> handlers = new Dictionary<Type, object>();

        // ------------------------------------------------------------------ //
        // IEventPublisher
        // ------------------------------------------------------------------ //

        public void Publish<TEvent>(in TEvent evt) where TEvent : IEvent
        {
            Type key = typeof(TEvent);

            if (!handlers.TryGetValue(key, out object raw))
                return;

            if (raw is EventDelegate<TEvent> handler)
            {
                try
                {
                    handler(in evt);
                }
                catch (Exception e)
                {
                    Debug.LogError($"[EventBus] Exception in handler for {key.Name}: {e}");
                }
            }
        }

        // ------------------------------------------------------------------ //
        // IEventSubscriber
        // ------------------------------------------------------------------ //

        public void Subscribe<TEvent>(EventDelegate<TEvent> callback) where TEvent : IEvent
        {
            Type key = typeof(TEvent);

            if (handlers.TryGetValue(key, out object raw))
            {
                handlers[key] = (EventDelegate<TEvent>)raw + callback;
            }
            else
            {
                handlers[key] = callback;
            }
        }

        public void Unsubscribe<TEvent>(EventDelegate<TEvent> callback) where TEvent : IEvent
        {
            Type key = typeof(TEvent);

            if (!handlers.TryGetValue(key, out object raw))
                return;

            var updated = (EventDelegate<TEvent>)raw - callback;

            if (updated == null)
                handlers.Remove(key);
            else
                handlers[key] = updated;
        }

        public void UnsubscribeAll<TEvent>() where TEvent : IEvent
        {
            handlers.Remove(typeof(TEvent));
        }
    }
}
