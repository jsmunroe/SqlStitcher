using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SqlStitcher.Forms.Infrastructure;

namespace SqlStitcher.Test.Forms.Infrastructure
{
    [TestClass]
    public class MessengerTests
    {
        [TestMethod]
        public void Construct()
        {
            // Execute
            new Messenger();
        }


        [TestMethod]
        public void SubscribeAndSend()
        {
            // Setup
            var messenger = new Messenger();

            // Execute
            var calledValue = 0;
            messenger.Subscribe<Int32>(this, n => calledValue = n);
            messenger.Publish(42);

            // Assert
            Assert.AreEqual(42, calledValue);
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SubscribeWithNullAction()
        {
            // Setup
            var messenger = new Messenger();

            // Execute
            messenger.Subscribe<Int32>(this, action: null);
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SubscribeWithNullSender()
        {
            // Setup
            var messenger = new Messenger();

            // Execute
            messenger.Subscribe<Int32>(sender: null, action: n => { });
        }


        [TestMethod]
        public void PublishWithNullMessage()
        {
            // Setup
            var messenger = new Messenger();

            // Execute
            Int32? calledValue = 0;
            messenger.Subscribe<Int32?>(this, n => calledValue = n);
            messenger.Publish<Int32?>(null);

            // Assert
            Assert.IsNull(calledValue);
        }


        [TestMethod]
        public void Unsubscribe()
        {
            // Setup
            var messenger = new Messenger();
            var calledValue = 0;
            messenger.Subscribe<Int32>(this, n => calledValue = n);

            // Execute
            messenger.Unsubscribe(this);

            // Assert
            messenger.Publish(42);
            Assert.AreEqual(0, calledValue);
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void UnsubscribeWithNull()
        {
            // Setup
            var messenger = new Messenger();
            messenger.Subscribe<Int32>(this, n => { });

            // Execute
            messenger.Unsubscribe(sender: null);
        }
    }
}
