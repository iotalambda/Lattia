using AutoMapper;
using Lattia.Attributes;
using Lattia.DependencyInjection;
using Lattia.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lattia.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;

        private readonly IMapper _mapper;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IMapper mapper)
        {
            _logger = logger;

            _mapper = mapper;
        }

        [HttpPost]
        [RequirePropertyGate]
        ////[RequireFeaturePermissions(FeatureFlags.WriteMyModel)]
        public ActionResult Post([FromBody, RequirePropertyWriteGates] MyModel myModel)
        {
            var myEntity = _mapper.Map<MyEntity>(myModel);

            var myModel2 = _mapper.Map<MyModel>(myEntity);

            return Ok(myModel2);
        }
    }
}
