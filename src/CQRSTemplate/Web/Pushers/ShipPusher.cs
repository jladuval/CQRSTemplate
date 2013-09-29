namespace Web.Pushers
{
    using System;
    using System.Threading;
    using Hubs;
    using Microsoft.WindowsAzure;
    using Microsoft.WindowsAzure.ServiceRuntime;
    using Microsoft.WindowsAzure.Storage;
    using Microsoft.WindowsAzure.Storage.Queue;

    public class ShipPusher
    {
        private Timer _timer;

        private CloudQueue _queue;

        public void Run()
        {
            if (!RoleEnvironment.IsAvailable) return;

            // Continuously process messages sent to the "TestQueue" 
            _timer = new Timer(PollQueue, null, 0, 5000);
        }

        private void PollQueue(object state)
        {
            var storageAccount = CloudStorageAccount.Parse(
            CloudConfigurationManager.GetSetting("StorageConnectionString"));

            var queueClient = storageAccount.CreateCloudQueueClient();
            // Retrieve storage account from connection string.
            var shipQueueName = CloudConfigurationManager.GetSetting("ShipQueue.Name");

            _queue = queueClient.GetQueueReference(shipQueueName);

            var retrievedMessage = _queue.GetMessage();

            while (retrievedMessage != null)
            {
                EventHub.Send(retrievedMessage.AsString);

                //Process the message in less than 30 seconds, and then delete the message
                _queue.DeleteMessage(retrievedMessage);
                retrievedMessage = _queue.GetMessage();
            }
        }
    }
}