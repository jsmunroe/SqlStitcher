using System;

namespace SqlStitcher.Forms.Contracts
{
    public interface IMessenger
    {
        /// <summary>
        /// Subscribe to the given type of message (<paramref name="action"/>).
        /// </summary>
        /// <typeparam name="TMessage">Type of message.</typeparam>
        /// <param name="sender">Sender reference used to unsubscribe.</param>
        /// <param name="action">Action delegate to be called when message is published.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="action"/> is null.</exception>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="sender"/> is null.</exception>
        void Subscribe<TMessage>(object sender, Action<TMessage> action);

        /// <summary>
        /// Publish the given message (<paramref name="message"/>).
        /// </summary>
        /// <typeparam name="TMessage">Type of message.</typeparam>
        /// <param name="message">Message to publish.</param>
        void Publish<TMessage>(TMessage message);

        /// <summary>
        /// Unsubscribe all of the subscriptions for the given sender (<paramref name="sender"/>).
        /// </summary>
        /// <param name="sender">Sender for which to unsubscribe.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="sender"/> is null.</exception>
        void Unsubscribe(object sender);
    }
}