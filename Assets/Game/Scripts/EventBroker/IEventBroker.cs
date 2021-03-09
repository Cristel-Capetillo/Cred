using System;

namespace EventBrokerFolder{
    public interface IEventBroker{
        void SubscribeMessage<TMessage>(Action<TMessage> callback);
        void UnsubscribeMessage<TMessage>(Action<TMessage> callback);
        void SendMessage<TMessage>(TMessage message);
    }
}