using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Moq;
using ConfigurationLibrary.Interfaces;
using ConfigurationLibrary.Models;

namespace ConfigurationLibrary.Tests
{
    public class ConfigurationReaderTests
    {
        [Fact]
        public async Task GetValue_ShouldReturnCorrectValue()
        {
            // Arrange
            var repositoryMock = new Mock<IConfigurationRepository>();
            repositoryMock.Setup(repo => repo.GetActiveConfigurationsAsync(It.IsAny<string>()))
                .ReturnsAsync(new List<ConfigurationRecord>
                {
                    new ConfigurationRecord { Name = "SiteName", Value = "soty.io", IsActive = true }
                });

            var reader = new ConfigurationReader("SERVICE-A", repositoryMock.Object);

            // Act
            var value = reader.GetValue<string>("SiteName");

            // Assert
            Assert.Equal("soty.io", value);
        }
    }
}
