using System;

internal interface IEventBinding<T>
{
    public Action<T> OnEvent { get; set; }
    public Action OnEventNoArgs { get; set; }
}

public class EventBinding<T> : IEventBinding<T> where T : IEvent
{
    Action<T> onEvent = _ => { };
    Action onEventNoArgs = () => { };

    public Action<T> OnEvent
    {
        get => onEvent;
        set => onEvent = value;
    }
    public Action OnEventNoArgs
    {
        get => onEventNoArgs;
        set => onEventNoArgs = value;
    }

    public EventBinding(Action<T> onEvent) => this.OnEvent = onEvent;
    public EventBinding(Action onEventNoArgs) => this.OnEventNoArgs = onEventNoArgs;

    public void Add(Action onEvent) => onEventNoArgs += onEvent;
    public void Remove(Action onEvent) => onEventNoArgs -= onEvent;

    public void Add(Action<T> onEvent) => onEvent += onEvent;
    public void Remove(Action<T> onEvent) => onEvent -= onEvent;
}