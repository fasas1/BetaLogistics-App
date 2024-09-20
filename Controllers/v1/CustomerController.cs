using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using BetaLogistics.Models;
using BetaLogistics.Models.Dto;
using BetaLogistics.Repository.IRepository;
using System.Net;
using Asp.Versioning;

namespace BetaLogistics.Controllers.v1
{
    [Route("api/V{version:apiVersion}/Customer")]
    [ApiController]
    [ApiVersion("1.0")]
    public class CustomerController : Controller
    {
        private readonly ICustomerRepository _dbCustomer;
        private IMapper _mapper;
        private readonly APIResponse _response;
        public CustomerController(ICustomerRepository dbCustomer, IMapper mapper)
        {
            _dbCustomer = dbCustomer;
            _response = new APIResponse();
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<APIResponse>> GetCustomers()
        {
            try
            {
                var customerList = await _dbCustomer.GetAllAsync();
                _response.Result = _mapper.Map<List<CustomerDto>>(customerList);
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

        [HttpGet("{id:int}", Name = "GetCustomer")]
        public async Task<ActionResult<APIResponse>> GetCustomer(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                var customer = await _dbCustomer.GetAsync(u => u.CustomerId == id);
                if (customer == null)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                _response.Result = _mapper.Map<CustomerDto>(customer);
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
        public async Task<ActionResult<APIResponse>> CreateCustomer([FromBody] CreateCustomerDto createDto)
        {
            try
            {
                if (createDto == null)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                Customer model = _mapper.Map<Customer>(createDto);
                await _dbCustomer.CreateAsync(model);
                await _dbCustomer.SaveAsync();

                _response.Result = _mapper.Map<CreateCustomerDto>(model);
                _response.IsSuccess = true;
                _response.StatusCode = HttpStatusCode.Created;
                return CreatedAtRoute("GetCustomer", new { id = model.CustomerId }, _response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.Message };
            }
            return _response;
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<APIResponse>> UpdateCustomer(int id, [FromBody] UpdateCustomerDto updateDto)
        {
            try
            {
                if (updateDto == null || updateDto.CustomerId != id)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                var objFromDb = await _dbCustomer.GetAsync(u => u.CustomerId == id);
                if (objFromDb == null)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                _mapper.Map(updateDto, objFromDb);
                await _dbCustomer.UpdateAsync(objFromDb);

                _response.Result = _mapper.Map<UpdateCustomerDto>(objFromDb);
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
        public async Task<ActionResult<APIResponse>> DeleteCustomer(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                Customer customer = await _dbCustomer.GetAsync(u => id == u.CustomerId);
                if (customer == null)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                await _dbCustomer.RemoveAsync(customer);
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
