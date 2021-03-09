using System;
using System.Collections.Generic;

namespace EventBrokerFolder {
    public class EventBroker : IEventBroker {
        private static EventBroker _thisInstance;

        public static EventBroker Instance() {
            return _thisInstance ?? (_thisInstance = new EventBroker());
        }

        readonly Dictionary<Type, object> subscribers = new Dictionary<Type, object>();

        public void SubscribeMessage<TMessage>(Action<TMessage> callback) {
            if (this.subscribers.TryGetValue(typeof(TMessage), out var oldSubscribers)) {
                callback = (oldSubscribers as Action<TMessage>) + callback;
            }

            this.subscribers[typeof(TMessage)] = callback;
        }

        public void UnsubscribeMessage<TMessage>(Action<TMessage> callback) {
            if (this.subscribers.TryGetValue(typeof(TMessage), out var oldSubscribers)) {
                callback = (oldSubscribers as Action<TMessage>) - callback;

                if (callback != null)
                    this.subscribers[typeof(TMessage)] = callback;
                else
                    this.subscribers.Remove(typeof(TMessage));
            }
        }

        public void SendMessage<TMessage>(TMessage message) {
            if (this.subscribers.TryGetValue(typeof(TMessage), out var currentSubscribers)) {
                (currentSubscribers as Action<TMessage>)?.Invoke(message);
            }
        }
    }
}