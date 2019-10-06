using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Azure.Storage.Queue;

namespace Account.Service.Core
{
    /// <summary>
    /// Handles all of the cloud storage related operations
    /// </summary>
    public interface ICloudStorage
    {
        /// <summary>
        /// Inserts a message into the queue
        /// </summary>
        /// <param name="queueName">Name of the queue</param>
        /// <param name="message">Actual message</param>
        /// <returns></returns>
        Task InsertQueueMessageAsync(string queueName, string message);

        /// <summary>
        /// Gets a single message from the queue
        /// </summary>
        /// <param name="queueName">Name of the queue</param>
        /// <returns>Retrieved message from the queue</returns>
        Task<CloudQueueMessage> GetQueueMessageAsync(string queueName);

        /// <summary>
        /// Gets list of message from the queue
        /// </summary>
        /// <param name="queueName">Name of the queue</param>
        /// <param name="messageCount">Message batch size</param>
        /// <returns>List of messages that retrieved from the queue</returns>
        Task<IEnumerable<CloudQueueMessage>> GetQueueMessagesAsync(string queueName, int messageCount);

        /// <summary>
        /// Deletes a message form the queue
        /// </summary>
        /// <param name="queueName">Name of the queue</param>
        /// <param name="cloudQueueMessage">Message that need to be deleted</param>
        /// <returns></returns>
        Task DeleteQueueMessageAsync(string queueName, CloudQueueMessage cloudQueueMessage);
    }
}
