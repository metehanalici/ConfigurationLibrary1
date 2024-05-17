using System.Collections.Generic;
using System.Threading.Tasks;
using ConfigurationLibrary.Interfaces;
using ConfigurationLibrary.Models;
using MongoDB.Driver;

namespace ConfigurationLibrary.Repositories
{
    public class MongoConfigurationRepository : IConfigurationRepository
    {
        private readonly IMongoCollection<ConfigurationRecord> _collection;

        public MongoConfigurationRepository(string connectionString, string databaseName, string collectionName)
        {
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase(databaseName);
            _collection = database.GetCollection<ConfigurationRecord>(collectionName);
        }

        public async Task<List<ConfigurationRecord>> GetActiveConfigurationsAsync(string applicationName)
        {
            return await _collection.Find(x => x.ApplicationName == applicationName && x.IsActive).ToListAsync();
        }

        public async Task UpdateConfigurationAsync(ConfigurationRecord record)
        {
            var filter = Builders<ConfigurationRecord>.Filter.Eq(r => r.Id, record.Id);
            await _collection.ReplaceOneAsync(filter, record);
        }

        public async Task AddConfigurationAsync(ConfigurationRecord record)
        {
            await _collection.InsertOneAsync(record);
        }
    }
}
