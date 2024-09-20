using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using BetaLogistics.Models;
using BetaLogistics.Models.Dto;
using BetaLogistics.Repository.IRepository;
using System.Net;
using Asp.Versioning;
using System.Text.Json;

namespace BetaLogistics.Controllers.v1
{
    [Route("api/V{version:apiVersion}/Driver")]
    [ApiController]
    [ApiVersion("1.0")]
    public class DriverController : Controller
    {
        private readonly IDriverRepository _dbDriver;
        private IMapper _mapper;
        private readonly APIResponse _response;
        public DriverController(IDriverRepository dbDriver, IMapper mapper)
        {
            _dbDriver = dbDriver;
            _response = new APIResponse();
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<APIResponse>> GetDrivers([FromQuery]int? driver, [FromQuery]string? search, int pageSize = 3, int pageNumber = 1)
        {
            try
            {
                IEnumerable<Driver> driverList;
                if (driver > 0)
                {
                    driverList = await _dbDriver.GetAllAsync(u =>u.DriverId == driver, pageSize:pageSize, pageNumber:pageNumber);
                }
                else
                {
                    driverList = await _dbDriver.GetAllAsync(pageSize: pageSize, pageNumber: pageNumber);
                }
                if (!string.IsNullOrEmpty(search))
                {
                    driverList = driverList.Where(u =>u.Name.ToLower().Contains(search));
                }
                Pagination pagination = new Pagination() { PageNumber = pageNumber, PageSize = pageSize };

                Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagination));
                _response.Result = _mapper.Map<List<DriverDto>>(driverList);
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
        [HttpGet("{id:int}", Name = "GetDriver")]
        public async Task<ActionResult<APIResponse>> GetDriver(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                var vehicle = await _dbDriver.GetAsync(u => u.DriverId == id);
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
        public async Task<ActionResult<APIResponse>> CreateDriver([FromBody] CreateDriverDto createDto)
        {
            try
            {
                if (createDto == null)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                Driver model = _mapper.Map<Driver>(createDto);
                await _dbDriver.CreateAsync(model);
                await _dbDriver.SaveAsync();

                _response.Result = _mapper.Map<CreateDriverDto>(model);
                _response.IsSuccess = true;
                _response.StatusCode = HttpStatusCode.Created;
                return CreatedAtRoute("GetDriver", new { id = model.DriverId }, _response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.Message };
            }
            return _response;
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<APIResponse>> UpdateDriver(int id, [FromBody] UpdateDriverDto updateDto)
        {
            try
            {
                if (updateDto == null || updateDto.DriverId != id)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                var objFromDb = await _dbDriver.GetAsync(u => u.DriverId == id);
                if (objFromDb == null)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                _mapper.Map(updateDto, objFromDb);
                await _dbDriver.UpdateAsync(objFromDb);

                _response.Result = _mapper.Map<UpdateDriverDto>(objFromDb);
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
        public async Task<ActionResult<APIResponse>> DeleteDriver(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                Driver driver = await _dbDriver.GetAsync(u => id == u.DriverId);
                if (driver == null)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                await _dbDriver.RemoveAsync(driver);
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
