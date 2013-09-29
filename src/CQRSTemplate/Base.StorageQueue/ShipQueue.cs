namespace Base.StorageQueue
{
    using System.Web.Helpers;
    using Microsoft.WindowsAzure;
    using Microsoft.WindowsAzure.ServiceRuntime;
    using Microsoft.WindowsAzure.Storage;
    using Microsoft.WindowsAzure.Storage.Queue;

    public class ShipQueue
    {
        public static void PushEvent(string message)
        {
            if (!RoleEnvironment.IsAvailable) return;
            var storageAccount = CloudStorageAccount.Parse(
                CloudConfigurationManager.GetSetting("StorageConnectionString"));

            var shipQueueName = CloudConfigurationManager.GetSetting("ShipQueue.Name");

            // Create the queue client.
            var queueClient = storageAccount.CreateCloudQueueClient();

            // Retrieve a reference to a queue.
            var queue = queueClient.GetQueueReference(shipQueueName);

            queue.CreateIfNotExists();
            queue.AddMessage(new CloudQueueMessage(message));
        }

        public static void PushShip(string message, string id)
        {
            if (!RoleEnvironment.IsAvailable) return;
            var storageAccount = CloudStorageAccount.Parse(
                CloudConfigurationManager.GetSetting("StorageConnectionString"));

            var shipQueueName = CloudConfigurationManager.GetSetting("ShipObjectQueue.Name");

            // Create the queue client.
            var queueClient = storageAccount.CreateCloudQueueClient();

            // Retrieve a reference to a queue.
            var queue = queueClient.GetQueueReference(shipQueueName);

            queue.CreateIfNotExists();
            queue.AddMessage(new CloudQueueMessage(Json.Encode(new { message, id})));
        }
    }
}
