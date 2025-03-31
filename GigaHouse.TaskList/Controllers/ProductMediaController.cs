using AutoMapper;
using GigaHouse.Application.ProductMedias.Create;
using GigaHouse.Application.ProductMedias.Delete;
using GigaHouse.Application.ProductMedias.Get;
using GigaHouse.Application.ProductMedias.GetList;
using GigaHouse.TaskList.Common;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NLog;

namespace GigaHouse.TaskList.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductMediaController : ControllerBase
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public ProductMediaController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [Authorize]
        [HttpGet]
        [ProducesResponseType(typeof(List<GetListResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetList([FromQuery] Guid productId, CancellationToken cancellationToken = default)
        {
            try
            {
                var request = new GetListRequest { ProductId = productId };
                var validator = new GetListRequestValidator();
                var validationResult = await validator.ValidateAsync(request, cancellationToken);

                if (!validationResult.IsValid)
                    return BadRequest(validationResult.Errors);

                var command = _mapper.Map<GetListCommand>(request);
                var response = await _mediator.Send(command, cancellationToken);
                var items = _mapper.Map<List<GetListResponse>>(response);

                return Ok(items);
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

                return Created(string.Empty, new ApiResponseWithData<CreateResponse>
                {
                    Success = true,
                    Message = "Media created successfully",
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
                    Message = "Media retrieved successfully",
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
                    Message = "Media deleted successfully"
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
