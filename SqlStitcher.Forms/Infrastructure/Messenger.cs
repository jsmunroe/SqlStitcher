using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlStitcher.Forms.Contracts;

namespace SqlStitcher.Forms.Infrastructure
{
    public class Messenger : IMessenger
    {
        private readonly List<MessengerSubscription> _subscriptions = new List<MessengerSubscription>();

        /// <summary>
        /// Subscribe to the given type of message (<paramref name="action"/>).
        /// </summary>
        /// <typeparam name="TMessage">Type of message.</typeparam>
        /// <param name="sender">Sender reference used to unsubscribe.</param>
        /// <param name="action">Action delegate to be called when message is published.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="action"/> is null.</exception>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="sender"/> is null.</exception>
        public void Subscribe<TMessage>(object sender, Action<TMessage> action)
        {
            #region Argument Validation

            if (action == null)
            {
                throw new ArgumentNullException("action");
            }

            if (sender == null)
            {
                throw new ArgumentNullException("sender");
            }

            #endregion

            _subscriptions.Add(new MessengerSubscription<TMessage>
            {
                Sender = sender,
                MessageType = typeof(TMessage),
                Action = action
            });
        }

        /// <summary>
        /// Publish the given message (<paramref name="message"/>).
        /// </summary>
        /// <typeparam name="TMessage">Type of message.</typeparam>
        /// <param name="message">Message to publish.</param>
        public void Publish<TMessage>(TMessage message)
        {
            var subscriptions = _subscriptions.Where(p => p.MessageType.IsAssignableFrom(typeof(TMessage)));

            foreach (var subscription in subscriptions.ToArray())
            {
                subscription.Publish(message);
            }
        }

        /// <summary>
        /// Unsubscribe all of the subscriptions for the given sender (<paramref name="sender"/>).
        /// </summary>
        /// <param name="sender">Sender for which to unsubscribe.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="sender"/> is null.</exception>
        public void Unsubscribe(object sender)
        {
            #region Argument Validation

            if (sender == null)
            {
                throw new ArgumentNullException("sender");
            }

            #endregion

            _subscriptions.RemoveAll(p => ReferenceEquals(p.Sender, sender));
        }

        abstract class MessengerSubscription
        {
            public object Sender { get; set; }

            public Type MessageType { get; set; }

            public abstract void Publish(Object message);
        }

        class MessengerSubscription<TMessage> : MessengerSubscription
        {
            public Action<TMessage> Action { get; set; }

            public override void Publish(object message)
            {
                if (message is TMessage == false && (message != null || default(TMessage) != null))
                {
                    throw new InvalidOperationException("Cannot publish this message type!");
                }

                Action((TMessage)message);
            }
        }
    }
}
