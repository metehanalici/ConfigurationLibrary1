using System.Collections.Generic;
using System.Threading.Tasks;
using ConfigurationLibrary.Models;

namespace ConfigurationLibrary.Interfaces
{
    public interface IConfigurationRepository
    {
        Task<List<ConfigurationRecord>> GetActiveConfigurationsAsync(string applicationName);
        Task UpdateConfigurationAsync(ConfigurationRecord record);
        Task AddConfigurationAsync(ConfigurationRecord record);
    }
}
