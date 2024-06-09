using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Azure.Data.Tables;
using Azure;

namespace Company.Function
{
    public class HttpTriggerMGP
    {
        private readonly ILogger<HttpTriggerMGP> _logger;
        private readonly TableClient _tableClient;

        public HttpTriggerMGP(ILogger<HttpTriggerMGP> logger)
        {
            _logger = logger;
            string connectionString = Environment.GetEnvironmentVariable("AzureWebJobsStorage");
            _tableClient = new TableClient(connectionString, "Users");
        }

        [Function("HttpTriggerMGP")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequest req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            // Retrieve the entity
            string partitionKey = "UniqueUserId";
            string rowKey = "UniqueUserId"; // RowKey ile aynı değeri kullanıyoruz
            var entity = await _tableClient.GetEntityAsync<TableEntity>(partitionKey, rowKey);

            if (entity != null)
            {
                // Increment the value
                int currentValue = (int)entity.Value["GameLaunch"];
                entity.Value["GameLaunch"] = currentValue + 1;

                // Update the table
                await _tableClient.UpdateEntityAsync(entity.Value, ETag.All, TableUpdateMode.Replace);

                return new OkObjectResult(entity.Value["GameLaunch"]);
            }
            else
            {
                // Eğer entity bulunamazsa, yeni bir entity oluşturun ve GameLaunch değerini 1 olarak ayarlayın
                var newEntity = new TableEntity(partitionKey, rowKey)
                {
                    { "GameLaunch", 1 }
                };
                await _tableClient.AddEntityAsync(newEntity);

                return new OkObjectResult(1);
            }
        }

        [Function("UpdateGameLaunch")]
        public async Task<IActionResult> UpdateGameLaunch(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequest req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            // Get the partition key value from the query string parameter "partitionKey"
            string partitionKey = req.Query["partitionKey"];

            // Check if partition key is provided
            if (string.IsNullOrEmpty(partitionKey))
            {
                return new BadRequestObjectResult("PartitionKey parameter is required.");
            }

            // Row key olarak partition key değerini kullanıyoruz
            string rowKey = partitionKey;

            // Retrieve the entity
            var entity = await _tableClient.GetEntityAsync<TableEntity>(partitionKey, rowKey);

            if (entity != null)
            {
                // Increment the value
                int currentValue = (int)entity.Value["GameLaunch"];
                entity.Value["GameLaunch"] = currentValue + 1;

                // Update the table
                await _tableClient.UpdateEntityAsync(entity.Value, ETag.All, TableUpdateMode.Replace);

                return new OkObjectResult(entity.Value["GameLaunch"]);
            }
            else
            {
                // Eğer entity bulunamazsa, yeni bir entity oluşturun ve GameLaunch değerini 1 olarak ayarlayın
                var newEntity = new TableEntity(partitionKey, rowKey)
                {
                    { "GameLaunch", 1 }
                };
                await _tableClient.AddEntityAsync(newEntity);

                return new OkObjectResult(1);
            }
        }
    }
}
