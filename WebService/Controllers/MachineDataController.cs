using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebService.Hubs;

namespace WebService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MachineDataController : ControllerBase
    {
        private readonly ILogger<MachineDataController> _logger;
        private readonly TimerGetData _timer;

        public MachineDataController(ILogger<MachineDataController> logger, TimerGetData timer)
        {
            _logger = logger;
            _timer = timer;
        }
        [HttpGet]
        public MachineData Get()
        {
            return new MachineData()
            {
                MachineName = "",
                DotNetVersion = "",
                OsVersion = "",
                TimeZone = ""
            };
        }
    }
}
