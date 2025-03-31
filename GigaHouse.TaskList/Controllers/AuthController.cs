using AutoMapper;
using GigaHouse.Application.Auth.AuthenticateUser;
using GigaHouse.TaskList.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NLog;

namespace GigaHouse.TaskList.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : BaseController
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public AuthController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost]
        [ProducesResponseType(typeof(ApiResponseWithData<AuthenticateUserResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AuthenticateUser([FromBody] AuthenticateUserRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var validator = new AuthenticateUserRequestValidator();
                var validationResult = await validator.ValidateAsync(request, cancellationToken);

                if (!validationResult.IsValid)
                    return BadRequest(validationResult.Errors);

                var command = _mapper.Map<AuthenticateUserCommand>(request);
                var response = await _mediator.Send(command, cancellationToken);

                return Ok(new ApiResponseWithData<AuthenticateUserResponse>
                {
                    Success = true,
                    Message = "User authenticated successfully",
                    Data = _mapper.Map<AuthenticateUserResponse>(response)
                });
            }
            catch (UnauthorizedAccessException unauthorized) 
            {
                _logger.Error(unauthorized.Message);
                return Unauthorized(new { message = unauthorized.Message });
            }
            catch (Exception error)
            {
                _logger.Error(error.Message);
                return StatusCode(500, new { message = error.Message });
            }
        }
    }

}
