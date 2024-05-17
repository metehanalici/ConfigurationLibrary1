using Microsoft.AspNetCore.Mvc;
using ConfigurationLibrary;
using System.Collections.Generic;

namespace WebApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ConfigurationsController : ControllerBase
    {
        private readonly ConfigurationReader _configurationReader;

        public ConfigurationsController(ConfigurationReader configurationReader)
        {
            _configurationReader = configurationReader;
        }

        [HttpGet("{key}")]
        public IActionResult GetValue(string key)
        {
            try
            {
                var value = _configurationReader.GetValue<string>(key);
                return Ok(value);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
