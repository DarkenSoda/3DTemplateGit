namespace Game.Core.DesignPatterns.EventSystem
{
    /// <summary>
    /// Marker interface. Every event must be a struct implementing this interface.
    /// Keep event structs small and data-only — no logic.
    /// </summary>
    public interface IEvent { }
}
