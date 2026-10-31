namespace Game.Core.DesignPatterns.EventSystem
{
    /// <summary>
    /// Delegate type used for all event callbacks.
    /// The event is passed by reference (in) to avoid copying value-type structs.
    /// </summary>
    public delegate void EventDelegate<TEvent>(in TEvent evt) where TEvent : IEvent;
}
