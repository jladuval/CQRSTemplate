namespace Base.StorageQueue
{
    using Microsoft.WindowsAzure;
    using Microsoft.WindowsAzure.ServiceRuntime;
    using Microsoft.WindowsAzure.Storage;
    using Microsoft.WindowsAzure.Storage.Queue;

    public class ShipQueue
    {
        public static void Push(string message)
        {
            if (!RoleEnvironment.IsAvailable) return;
            var storageAccount = CloudStorageAccount.Parse(
                CloudConfigurationManager.GetSetting("StorageConnectionString"));

            var mailQueueName = CloudConfigurationManager.GetSetting("ShipQueue.Name");

            // Create the queue client.
            var queueClient = storageAccount.CreateCloudQueueClient();

            // Retrieve a reference to a queue.
            var queue = queueClient.GetQueueReference(mailQueueName);

            queue.CreateIfNotExists();
            queue.AddMessage(new CloudQueueMessage(message));
        }
    }
}
