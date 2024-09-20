using Asp.Versioning;
using AutoMapper;
using BetaLogistics.Hubs;
using BetaLogistics.Models;
using BetaLogistics.Repository;
using BetaLogistics.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace BetaLogistics.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]

    public class LocationController : ControllerBase
    {
        private readonly IHubContext<TrackingHub> _hubContext;
        private readonly ILocationUpdateRepository _dbLocation;
        private IMapper _mapper;
        private readonly APIResponse _response;
        public LocationController(IHubContext<TrackingHub> hubContext, ILocationUpdateRepository dbLocation, IMapper mapper)
        {
            _dbLocation = dbLocation;
            _hubContext = hubContext;
            _mapper = mapper;
            _response = new APIResponse();
        }

        [HttpGet]
        public async Task<ActionResult<APIResponse>> GetLocations()
        {
            return Ok(_response);
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<APIResponse>> GetLocation()
        {
            return Ok(_response);
        }
        [HttpPost]
        public async Task<ActionResult<APIResponse>> createLocation()
        {
            return Ok(_response);
        }
    }
}
