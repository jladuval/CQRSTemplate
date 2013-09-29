namespace Web.Pushers
{
    using System;
    using System.Dynamic;
    using System.Threading;
    using System.Web.Helpers;
    using Hubs;
    using Microsoft.WindowsAzure;
    using Microsoft.WindowsAzure.ServiceRuntime;
    using Microsoft.WindowsAzure.Storage;
    using Microsoft.WindowsAzure.Storage.Queue;

    public class ShipPusher
    {
        private Timer _timer1;
        private Timer _timer2;

        private CloudQueueClient _queueClient;

        public void Run()
        {
            if (!RoleEnvironment.IsAvailable) return;

            var storageAccount = CloudStorageAccount.Parse(
            CloudConfigurationManager.GetSetting("StorageConnectionString"));

            _queueClient = storageAccount.CreateCloudQueueClient();
            // Retrieve storage account from connection string.
            

            // Continuously process messages sent to the "TestQueue" 
            _timer1 = new Timer(PollQueue, null, 0, 5000);
            _timer2 = new Timer(PollObjectQueue, null, 0, 5000);
        }

        private void PollQueue(object state)
        {
            var shipQueueName = CloudConfigurationManager.GetSetting("ShipQueue.Name");

            var queue = _queueClient.GetQueueReference(shipQueueName);

            var retrievedMessage = queue.GetMessage();

            while (retrievedMessage != null)
            {
                EventHub.Send(retrievedMessage.AsString);

                //Process the message in less than 30 seconds, and then delete the message
                queue.DeleteMessage(retrievedMessage);
                retrievedMessage = queue.GetMessage();
            }
        }

        private void PollObjectQueue(object state)
        {
            var shipQueueName = CloudConfigurationManager.GetSetting("ShipObjectQueue.Name");

            var queue = _queueClient.GetQueueReference(shipQueueName);

            var retrievedMessage = queue.GetMessage();

            while (retrievedMessage != null)
            {
                var shipObject = Json.Decode(retrievedMessage.AsString);
                EventHub.SendShipObject(shipObject.message, shipObject.id);

                //Process the message in less than 30 seconds, and then delete the message
                queue.DeleteMessage(retrievedMessage);
                retrievedMessage = queue.GetMessage();
            }
        }
    }
}