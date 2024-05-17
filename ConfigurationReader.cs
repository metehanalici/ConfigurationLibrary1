using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using ConfigurationLibrary.Interfaces;
using ConfigurationLibrary.Models;

namespace ConfigurationLibrary
{
    public class ConfigurationReader
    {
        private readonly string _applicationName;
        private readonly IConfigurationRepository _repository;
        private readonly Timer _timer;
        private List<ConfigurationRecord> _configurations;

        public ConfigurationReader(string applicationName, string connectionString, int refreshTimerIntervalInMs)
        {
            _applicationName = applicationName;
            _repository = new MongoConfigurationRepository(connectionString, "configDb", "configurations");
            _timer = new Timer(async _ => await RefreshConfigurations(), null, 0, refreshTimerIntervalInMs);
        }

        private async Task RefreshConfigurations()
        {
            _configurations = await _repository.GetActiveConfigurationsAsync(_applicationName);
        }

        public T GetValue<T>(string key)
        {
            var config = _configurations?.FirstOrDefault(x => x.Name == key);
            if (config == null)
            {
                throw new KeyNotFoundException($"Configuration key '{key}' not found.");
            }

            return (T)Convert.ChangeType(config.Value, typeof(T));
        }
    }
}
