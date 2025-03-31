using AutoMapper;
using GigaHouse.Application.Products.Create;
using GigaHouse.Application.Products.Delete;
using GigaHouse.Application.Products.Get;
using GigaHouse.Application.Products.GetList;
using GigaHouse.Application.Products.GetPriceList;
using GigaHouse.Application.Products.Update;
using GigaHouse.Core.Enums;
using GigaHouse.TaskList.Common;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NLog;
using System.Security.Claims;

namespace GigaHouse.TaskList.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : BaseController
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public ProductController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [Authorize]
        [HttpGet]
        [ProducesResponseType(typeof(PagedResponse<GetListResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetList([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10, [FromQuery] string? gtin = null, [FromQuery] string? name = null, [FromQuery] ProductStatus? status = null, CancellationToken cancellationToken = default)
        {
            try
            {
                var request = new GetListRequest { PageNumber = pageNumber, PageSize = pageSize, Gtin = gtin, Name = name, Status = status };
                var validator = new GetListRequestValidator();
                var validationResult = await validator.ValidateAsync(request, cancellationToken);

                if (!validationResult.IsValid)
                    return BadRequest(validationResult.Errors);

                var command = _mapper.Map<GetListCommand>(request);
                var response = await _mediator.Send(command, cancellationToken);

                var pagedResponse = new PagedResponse<GetListResponse>
                {
                    Items = _mapper.Map<List<GetListResponse>>(response),
                    TotalCount = response.TotalCount,
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    TotalPages = response.TotalPages
                };

                return Ok(pagedResponse);
            }
            catch (Exception error)
            {
                _logger.Error(error.Message);
                return StatusCode(500, new { message = error.Message });
            }
        }

        [Authorize]
        [HttpGet("GetPriceList/{id}")]
        [ProducesResponseType(typeof(ApiResponseWithData<GetPriceListResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetPriceList([FromRoute] Guid id = default, CancellationToken cancellationToken = default)
        {
            try
            {
                var request = new GetPriceListRequest { UserId = GetCurrentUserId(), ProductId = id };
                var validator = new GetPriceListRequestValidator();
                var validationResult = await validator.ValidateAsync(request, cancellationToken);

                if (!validationResult.IsValid)
                    return BadRequest(validationResult.Errors);

                var command = _mapper.Map<GetPriceListCommand>(request);
                var response = await _mediator.Send(command, cancellationToken);

                return Ok(new ApiResponseWithData<GetPriceListResponse>
                {
                    Success = true,
                    Message = "Media retrieved successfully",
                    Data = _mapper.Map<GetPriceListResponse>(response)
                });
            }
            catch (KeyNotFoundException notFoundException)
            {
                _logger.Error(notFoundException.Message);
                return NotFound(new { message = notFoundException.Message });
            }
            catch (Exception error)
            {
                _logger.Error(error.Message);
                return StatusCode(500, new { message = error.Message });
            }
        }

        [Authorize]
        [HttpPost]
        [ProducesResponseType(typeof(ApiResponseWithData<CreateResponse>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Save([FromBody] CreateRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var validator = new CreateRequestValidator();
                var validationResult = await validator.ValidateAsync(request, cancellationToken);

                if (!validationResult.IsValid)
                    return BadRequest(validationResult.Errors);

                var command = _mapper.Map<CreateCommand>(request);
                var responseCommand = await _mediator.Send(command, cancellationToken);
                var response = _mapper.Map<CreateResponse>(request);

                if (responseCommand != null)
                    response.Id = responseCommand.Id;

                response.Status = Core.Enums.ProductStatus.Pending.ToString();

                return Created(string.Empty, new ApiResponseWithData<CreateResponse>
                {
                    Success = true,
                    Message = "Product created successfully",
                    Data = response
                });
            }
            catch (Exception error)
            {
                _logger.Error(error.Message);
                return StatusCode(500, new { message = error.Message });
            }
        }

        [Authorize]
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApiResponseWithData<GetResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            try
            {
                var request = new GetRequest { Id = id };
                var validator = new GetRequestValidator();
                var validationResult = await validator.ValidateAsync(request, cancellationToken);

                if (!validationResult.IsValid)
                    return BadRequest(validationResult.Errors);

                var command = _mapper.Map<GetCommand>(request.Id);
                var response = await _mediator.Send(command, cancellationToken);

                return Ok(new ApiResponseWithData<GetResponse>
                {
                    Success = true,
                    Message = "Product retrieved successfully",
                    Data = _mapper.Map<GetResponse>(response)
                });
            }
            catch (KeyNotFoundException notFoundException)
            {
                _logger.Error(notFoundException.Message);
                return NotFound(new { message = notFoundException.Message });
            }
            catch (Exception error)
            {
                _logger.Error(error.Message);
                return StatusCode(500, new { message = error.Message });
            }
        }

        [Authorize]
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            try
            {
                var request = new DeleteRequest { Id = id };
                var validator = new DeleteRequestValidator();
                var validationResult = await validator.ValidateAsync(request, cancellationToken);

                if (!validationResult.IsValid)
                    return BadRequest(validationResult.Errors);

                var command = _mapper.Map<DeleteCommand>(request.Id);
                await _mediator.Send(command, cancellationToken);

                return Ok(new ApiResponse
                {
                    Success = true,
                    Message = "Product deleted successfully"
                });
            }
            catch (KeyNotFoundException notFoundException)
            {
                _logger.Error(notFoundException.Message);
                return NotFound(new { message = notFoundException.Message });
            }
            catch (Exception error)
            {
                _logger.Error(error.Message);
                return StatusCode(500, new { message = error.Message });
            }
        }

        [Authorize]
        [HttpPut]
        [ProducesResponseType(typeof(ApiResponseWithData<UpdateResponse>), StatusCodes.Status202Accepted)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Edit([FromBody] UpdateRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var getRequest = new GetRequest { Id = request.Id };
                var getValidator = new GetRequestValidator();
                var getValidationResult = await getValidator.ValidateAsync(getRequest, cancellationToken);

                if (!getValidationResult.IsValid)
                    return BadRequest(getValidationResult.Errors);

                var getCommand = _mapper.Map<GetCommand>(request.Id);
                var getResponse = await _mediator.Send(getCommand, cancellationToken);

                if (getResponse == null)
                    return NotFound($"Product with ID {request.Id} not found");

                var validator = new UpdateRequestValidator();
                var validationResult = await validator.ValidateAsync(request, cancellationToken);

                if (!validationResult.IsValid)
                    return BadRequest(validationResult.Errors);

                var command = _mapper.Map<UpdateCommand>(request);
                var responseCommand = await _mediator.Send(command, cancellationToken);
                var response = _mapper.Map<UpdateResponse>(request);

                return Accepted(string.Empty, new ApiResponseWithData<UpdateResponse>
                {
                    Success = true,
                    Message = "Product Updated successfully",
                    Data = response
                });
            }
            catch (KeyNotFoundException notFoundException)
            {
                _logger.Error(notFoundException.Message);
                return NotFound(new { message = notFoundException.Message });
            }
            catch (Exception error)
            {
                _logger.Error(error.Message);
                return StatusCode(500, new { message = error.Message });
            }
        }
    }
}
