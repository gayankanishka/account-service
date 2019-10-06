using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Queue;

namespace Account.Service.Core
{
    /// <summary>
    /// Handles all of the cloud storage related operations
    /// </summary>
    public class CloudStorage : ICloudStorage
    {
        #region Variables

        private readonly CloudQueueClient _cloudQueueClient;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructs with a cloud storage connection string
        /// </summary>
        /// <param name="connectionString">Cloud storage account connection string</param>
        public CloudStorage(string connectionString)
        {
            CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(connectionString);
            _cloudQueueClient = cloudStorageAccount.CreateCloudQueueClient();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Inserts a message into the queue
        /// </summary>
        /// <param name="queueName">Name of the queue</param>
        /// <param name="message">Actual message</param>
        /// <returns></returns>
        public async Task InsertQueueMessageAsync(string queueName, string message)
        {
            CloudQueue queue = _cloudQueueClient.GetQueueReference(queueName);
            CloudQueueMessage cloudQueueMessage = new CloudQueueMessage(message);

            await queue.CreateIfNotExistsAsync();

            await queue.AddMessageAsync(cloudQueueMessage);
        }

        /// <summary>
        /// Gets a single message from the queue
        /// </summary>
        /// <param name="queueName">Name of the queue</param>
        /// <returns>Retrieved message from the queue</returns>
        public async Task<CloudQueueMessage> GetQueueMessageAsync(string queueName)
        {
            CloudQueue queue = _cloudQueueClient.GetQueueReference(queueName);

            return await queue.GetMessageAsync();
        }

        /// <summary>
        /// Gets list of message from the queue
        /// </summary>
        /// <param name="queueName">Name of the queue</param>
        /// <param name="messageCount">Message batch size</param>
        /// <returns>List of messages that retrieved from the queue</returns>
        public async Task<IEnumerable<CloudQueueMessage>> GetQueueMessagesAsync(string queueName, int messageCount)
        {
            CloudQueue queue = _cloudQueueClient.GetQueueReference(queueName);

            return await queue.GetMessagesAsync(messageCount);
        }

        /// <summary>
        /// Deletes a message form the queue
        /// </summary>
        /// <param name="queueName">Name of the queue</param>
        /// <param name="cloudQueueMessage">Message that need to be deleted</param>
        /// <returns></returns>
        public async Task DeleteQueueMessageAsync(string queueName, CloudQueueMessage cloudQueueMessage)
        {
            CloudQueue queue = _cloudQueueClient.GetQueueReference(queueName);

            await queue.DeleteMessageAsync(cloudQueueMessage);
        }

        #endregion
    }
}
