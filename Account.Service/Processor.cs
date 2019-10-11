using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Account.Service.Core;
using Microsoft.Azure.Storage.Queue;
using Newtonsoft.Json;

namespace Account.Service
{
    public class Processor
    {
        #region Variables

        private const int MessageBatchCount = 20;
        private const string QueueName = "accountqueue";
        private const int MessageRetryCount = 2;

        private readonly ICloudStorage _cloudStorage;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor of the ingest processor
        /// </summary>
        /// <param name="cloudStorage">Injected cloud storage account</param>
        public Processor(ICloudStorage cloudStorage)
        {
            this._cloudStorage = cloudStorage;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Retrieves messages from the Storage queue and sends for processing
        /// </summary>
        /// <returns></returns>
        public async Task ProcessMessages()
        {
            IEnumerable<CloudQueueMessage> queueMessages = await _cloudStorage.GetQueueMessagesAsync(QueueName, MessageBatchCount);

            IList<Task> taskList = queueMessages.Select(ProcessMessage).ToList();

            await Task.WhenAll(taskList);
        }

        // Process the queue message and sends to the Twilio
        private async Task ProcessMessage(CloudQueueMessage cloudQueueMessage)
        {
            if (cloudQueueMessage.DequeueCount > MessageRetryCount)
            {
                await _cloudStorage.DeleteQueueMessageAsync(QueueName, cloudQueueMessage);
                return;
            }

            Models.Account account = await Task.Run(() => JsonConvert.DeserializeObject<Models.Account>(cloudQueueMessage.AsString));

            // await _cloudStorage.DeleteQueueMessageAsync(QueueName, cloudQueueMessage);
        }

        #endregion
    }
}
