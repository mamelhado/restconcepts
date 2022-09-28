using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Rest.Api.Attributes;
using Rest.Api.Models.Response;
using Rest.Domain.App.Interfaces.Service;
using Rest.Infra.CrossCutting.Utils.Authentication;
using Rest.Infra.CrossCutting.Utils.Enum;

namespace Rest.Api.Controllers.v1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Consumes("application/json")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class AccessController : ControllerBase
    {
        private readonly ILogger<AccessController> _logger;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        public AccessController(ILogger<AccessController> logger, IUserService userService, IMapper mapper)
        {
            _logger = logger;
            _userService = userService;
            _mapper = mapper;
        }

        /// <summary>
        /// Authenticate an user by credentials.
        /// </summary>
        /// <param name="credentials"></param> 
        /// <response code="200">Returns de token for access another resources</response>
        /// <response code="400">If the request has errors or an error ocurred in generating the token</response>
        /// <response code="401">If the request contain credentials not authorized</response>
        [HttpPost("authenticate")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<AuthenticateResponse>> AuthenticateAccess([FromBody] AuthenticateRequest credentials) 
        {
            try
            {
                var response = await _userService.Authenticate(credentials);
                return Ok(response);
            }
            catch (Exception ex) 
            {
                return BadRequest($"An error occurred in generating the token. Exception {ex.StackTrace}" );
            }

            return Unauthorized(new { message = "Unauthorized" });
        }

                
        /// <summary>
        /// Get user by id.
        /// </summary>
        /// <param name="id"></param> 
        /// <response code="200">Returns de users</response>
        /// <response code="400">If the request has errors</response>
        /// <response code="401">If the requester is unauthorized</response>
        [HttpGet("{id:int}")]
        [Authorize(Rest.Infra.CrossCutting.Utils.Enum.Role.Admin)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<UserModel>> GetByIdAsync(int id, CancellationToken token)
        {
            // only admins can access other user records
            /*var currentUser = (User)HttpContext.User.Identity;
            if (id != currentUser.Id && currentUser.Role != Infra.CrossCutting.Utils.Enum.Role.Admin)
                return Unauthorized(new { message = "Unauthorized" });*/

            var user = await _userService.GetByIdAsync(id, token);
            return Ok(_mapper.Map<UserModel>(user));
        }

        /// <summary>
        /// Get all users.
        /// </summary> 
        /// <response code="200">Returns de users</response>
        /// <response code="400">If the request has errors</response>
        /// <response code="401">If the requester is unauthorized</response>
        [HttpGet("")]
        [Authorize(Rest.Infra.CrossCutting.Utils.Enum.Role.Admin)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<IEnumerable<UserModel>> GetAll()
        {
            var users = _userService.GetAll();
            if (users == null)
            {
                _logger.LogTrace($"The collection not found!");
                return NotFound();
            }

            if (users != null && !users.Any())
            {
                _logger.LogTrace($"The collection is empty!");
                return NoContent();
            }
            return Ok(_mapper.Map<IEnumerable<UserModel>>(users));
        }
    }
}
