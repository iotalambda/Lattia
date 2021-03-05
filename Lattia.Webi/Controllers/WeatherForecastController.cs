using AutoMapper;
using Lattia.Services;
using Lattia.Webi;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Lattia.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;

        private readonly IMapper _mapper;
        private readonly IPropertyGatesService service;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IMapper mapper, IPropertyGatesService service)
        {
            _logger = logger;

            _mapper = mapper;

            this.service = service;
        }

        [HttpPost]
        [RequirePropertyGate]
        ////[RequireFeaturePermissions(FeatureFlags.WriteMyModel)]
        public ActionResult Post([FromBody] MyModel myModel)
        {
            var myEntity = _mapper.Map<MyEntity>(myModel);

            var myModel2 = _mapper.Map<MyModel>(myEntity);

            myModel2 = service.ExcludeErrorProperties(myModel2, WriteOnlyPropertyGate.Instance);

            return Ok(myModel2);
        }
    }
}
