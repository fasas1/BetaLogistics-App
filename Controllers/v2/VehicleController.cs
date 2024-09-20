using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BetaLogistics.Data;
using BetaLogistics.Models;
using BetaLogistics.Models.Dto;
using BetaLogistics.Repository.IRepository;
using System.Net;
using Asp.Versioning;

namespace BetaLogistics.Controllers.v2
{
    [Route("api/V{version:apiVersion}/Vehicle")]
    [ApiController]
    [ApiVersion("2.0")]
    public class VehicleController : ControllerBase
    {
        private readonly IVehicleRepository _dbVehicle;
        private IMapper _mapper;
        private APIResponse _response;
        public VehicleController(IVehicleRepository dbVehicle, IMapper mapper)
        {
            _dbVehicle = dbVehicle;
            _mapper = mapper;
            _response = new APIResponse();
        }

        [HttpGet]
        public async Task<ActionResult<APIResponse>> GetVehicles()
        {
            try
            {
                var vehicleList = await _dbVehicle.GetAllAsync(includeProperties: "Drivers");

                // Debugging log
                foreach (var vehicle in vehicleList)
                {
                    if (vehicle.Drivers != null)
                    {
                        Console.WriteLine($"Vehicle ID: {vehicle.VehicleId} has {vehicle.Drivers.Count} drivers.");
                    }
                    else
                    {
                        Console.WriteLine($"Vehicle ID: {vehicle.VehicleId} has no drivers loaded.");
                    }
                }
                _response.Result = _mapper.Map<List<VehicleDto>>(vehicleList);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.Message };
            }

            return _response;
        }

        [HttpGet("{id:int}", Name = "GetVehicle")]
        public async Task<ActionResult<APIResponse>> GetVehicle(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                var vehicle = await _dbVehicle.GetAsync(u => u.VehicleId == id);
                if (vehicle == null)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                _response.Result = _mapper.Map<VehicleDto>(vehicle);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.Message };
            }
            return _response;
        }

        [HttpPost]
        public async Task<ActionResult<APIResponse>> CreateVehicle([FromBody] CreateVehicleDto createDto)
        {
            try
            {
                if (createDto == null)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                Vehicle model = _mapper.Map<Vehicle>(createDto);
                await _dbVehicle.CreateAsync(model);
                await _dbVehicle.SaveAsync();

                _response.Result = _mapper.Map<CreateVehicleDto>(model);
                _response.IsSuccess = true;
                _response.StatusCode = HttpStatusCode.Created;
                return CreatedAtRoute("GetVehicle", new { id = model.VehicleId }, _response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.Message };
            }
            return _response;
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<APIResponse>> UpdateVehicle(int id, [FromBody] UpdateVehicleDto updateDto)
        {
            try
            {
                if (updateDto == null || updateDto.VehicleId != id)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                var objFromDb = await _dbVehicle.GetAsync(u => u.VehicleId == id);
                if (objFromDb == null)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                _mapper.Map(updateDto, objFromDb);
                await _dbVehicle.UpdateAsync(objFromDb);

                _response.Result = _mapper.Map<UpdateVehicleDto>(objFromDb);
                _response.IsSuccess = true;
                _response.StatusCode = HttpStatusCode.NoContent;
                return Ok();
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.Message };
            }
            return _response;
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<APIResponse>> DeleteVehicle(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                Vehicle vehicle = await _dbVehicle.GetAsync(u => id == u.VehicleId);
                if (vehicle == null)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                await _dbVehicle.RemoveAsync(vehicle);
                _response.IsSuccess = true;
                return Ok();
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.Message };
            }
            return _response;
        }
    }
}
