using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Account.Service.Business;
using Account.Service.Core;
using Account.Service.Core.Models;
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
        private readonly IAccountBusiness _accountBusiness;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor of the ingest processor
        /// </summary>
        /// <param name="cloudStorage">Injected cloud storage account</param>
        public Processor(ICloudStorage cloudStorage, IAccountBusiness accountBusiness)
        {
            _cloudStorage = cloudStorage;
            _accountBusiness = accountBusiness;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Retrieves messages from the Storage queue and sends for processing
        /// </summary>
        /// <returns></returns>
        public async Task ProcessMessages()
        {
            try
            {
                IEnumerable<CloudQueueMessage> queueMessages = await _cloudStorage.GetQueueMessagesAsync(QueueName, MessageBatchCount);

                IList<Task> taskList = queueMessages.Select(ProcessMessage).ToList();

                await Task.WhenAll(taskList);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        // Process the queue message and sends to the DB handler
        private async Task ProcessMessage(CloudQueueMessage cloudQueueMessage)
        {
            if (cloudQueueMessage.DequeueCount > MessageRetryCount)
            {
                //await _cloudStorage.DeleteQueueMessageAsync(QueueName, cloudQueueMessage);
                return;
            }

            AccountDto account = await Task.Run(() => JsonConvert.DeserializeObject<AccountDto>(cloudQueueMessage.AsString));

            await _accountBusiness.RoutOperation(account);

            // await _cloudStorage.DeleteQueueMessageAsync(QueueName, cloudQueueMessage);
        }

        #endregion
    }
}
