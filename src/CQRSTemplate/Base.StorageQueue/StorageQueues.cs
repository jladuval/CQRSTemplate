using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.ServiceRuntime;
using Microsoft.WindowsAzure.Storage;
using Base.DDD.Domain.Annotations;

namespace Base.StorageQueue
{
    using Microsoft.WindowsAzure.Storage.Queue;

    [DomainService]
    public class StorageQueues : IStorageQueues
    {
        public void InitializeAllQueues()
        {
            if (!RoleEnvironment.IsAvailable) return;

            var storageAccount = CloudStorageAccount.Parse(
                CloudConfigurationManager.GetSetting("StorageConnectionString"));

            var queueClient = storageAccount.CreateCloudQueueClient();

            CreateShipQueue(queueClient);

            CreateMailQueue(queueClient);
        }

        private void CreateShipQueue(CloudQueueClient queueClient)
        {
            var shipQueueName = CloudConfigurationManager.GetSetting("ShipQueue.Name");

            var queue = queueClient.GetQueueReference(shipQueueName);

            queue.CreateIfNotExists();
        }

        private void CreateMailQueue(CloudQueueClient queueClient)
        {
            var mailQueueName = CloudConfigurationManager.GetSetting("MailQueue.Name");

            var queue = queueClient.GetQueueReference(mailQueueName);

            queue.CreateIfNotExists();
        }
    }
}
