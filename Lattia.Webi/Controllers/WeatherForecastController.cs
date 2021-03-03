using AutoMapper;
using Lattia.Attributes;
using Lattia.DependencyInjection;
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

        private readonly CheckPropertyPermissionsService service;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IMapper mapper, CheckPropertyPermissionsService service)
        {
            _logger = logger;

            _mapper = mapper;

            this.service = service;
        }

        [HttpPost]
        [RequirePropertyPermissions]
        ////[RequireFeaturePermissions(FeatureFlags.WriteMyModel)]
        public ActionResult Post([FromBody, RequirePropertyWritePermissions] MyModel myModel)
        {
            var myEntity = _mapper.Map<MyEntity>(myModel);

            var myModel2 = _mapper.Map<MyModel>(myEntity);

            return Ok(myModel2);
        }
    }
}
