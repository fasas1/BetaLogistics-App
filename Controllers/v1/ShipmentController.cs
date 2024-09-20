using AutoMapper;
using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BetaLogistics.Models;
using BetaLogistics.Models.Dto;
using BetaLogistics.Repository;
using BetaLogistics.Repository.IRepository;
using System.Net;
using Asp.Versioning;

namespace BetaLogistics.Controllers.v1
{
    [Route("api/V{version:apiVersion}/Shipment")]
    [ApiController]
    [ApiVersion("1.0")]
    public class ShipmentController : ControllerBase
    {
        private readonly IShipmentRepository _dbShipment;
        private readonly ICustomerRepository _dbCustomer;
        private IMapper _mapper;
        private readonly APIResponse _response;
        public ShipmentController(IShipmentRepository dbShipment, ICustomerRepository dbCustomer, IMapper mapper)
        {
            _dbShipment = dbShipment;
            _dbCustomer = dbCustomer;
            _response = new APIResponse();
            _mapper = mapper;
        }
        [HttpGet]
        [ResponseCache(CacheProfileName ="Default30")]
        public async Task<ActionResult<APIResponse>> GetShipments()
        {
            try
            {
                var shipmentList = await _dbShipment.GetAllAsync(
                   includeProperties: "Vehicle,Customer");

                _response.Result = _mapper.Map<List<ShipmentDto>>(shipmentList);
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

        [HttpGet("{id:int}", Name = "GetShipment")]

        public async Task<ActionResult<APIResponse>> GetShipment(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                Shipment shipment = await _dbShipment.GetAsync(u => u.ShipmentId == id, includeProperties: "Vehicle,Customer");
                if (shipment == null)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }

                _response.Result = _mapper.Map<ShipmentDto>(shipment);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.Message }; ;
            }
            return _response;
        }
        [HttpPost]
        public async Task<ActionResult<APIResponse>> CreateShipment([FromBody] CreateShipmentDto createShipmentDto)
        {
            try
            {
                if (createShipmentDto == null)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.ErrorMessages = new List<string> { "Invalid input data." };
                    return BadRequest(_response);
                }

                // Check if the CustomerId exists using CustomerRepository
                var customerExists = await _dbCustomer.GetAsync(c => c.CustomerId == createShipmentDto.CustomerId) != null;
                if (!customerExists)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.ErrorMessages = new List<string> { "The provided CustomerId does not exist." };
                    return BadRequest(_response);
                }

                // Map DTO to entity
                Shipment model = _mapper.Map<Shipment>(createShipmentDto);

                // Create shipment and save to database
                await _dbShipment.CreateAsync(model);
                await _dbShipment.SaveAsync();

                // Map back to DTO for response
                _response.Result = _mapper.Map<CreateShipmentDto>(model);
                _response.IsSuccess = true;
                _response.StatusCode = HttpStatusCode.Created;
                return CreatedAtRoute("GetShipment", new { id = model.ShipmentId }, _response);
            }
            catch (DbUpdateException ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { "A database error occurred while saving the shipment.", ex.Message };

                if (ex.InnerException != null)
                {
                    _response.ErrorMessages.Add(ex.InnerException.Message);
                }

                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { "An unexpected error occurred.", ex.Message };

                if (ex.InnerException != null)
                {
                    _response.ErrorMessages.Add(ex.InnerException.Message);
                }

                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<APIResponse>> UpdateShipment(int id, [FromBody] UpdateShipmentDto updateDto)
        {
            try
            {
                if (updateDto == null || updateDto.ShipmentId != id)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                var objFromDb = await _dbShipment.GetAsync(u => u.VehicleId == id);
                if (objFromDb == null)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                _mapper.Map(updateDto, objFromDb);
                await _dbShipment.UpdateAsync(objFromDb);

                _response.Result = _mapper.Map<UpdateShipmentDto>(objFromDb);
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
        public async Task<ActionResult<APIResponse>> DeleteShipment(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                Shipment shipment = await _dbShipment.GetAsync(u => id == u.ShipmentId);
                if (shipment == null)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                await _dbShipment.RemoveAsync(shipment);
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
