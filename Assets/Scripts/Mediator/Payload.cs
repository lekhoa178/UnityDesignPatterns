
using UnityEngine;

public abstract class Payload<TData> : Mediator.IVisitor
{
    public abstract TData Content { get; set; }
    public abstract void Visit<T>(T visitable) where T : Component, Mediator.IVisitable;
}

public class MessagePayload : Payload<string>
{
    public GoapAgent Source { get; set; }
    public override string Content { get; set; }

    private MessagePayload() { }

    public override void Visit<T>(T visitable)
    {
        Debug.Log($"{visitable.name} received message from {Source.name} : {Content}");
        // Execute logic on visitable here
    }

    public class Builder
    {
        MessagePayload payload = new MessagePayload();

        public Builder(GoapAgent source)
        {
            payload.Source = source;
        }

        public Builder WithContent(string content)
        {
            payload.Content = content;
            return this;
        }

        public MessagePayload Build() => payload;
    }
}