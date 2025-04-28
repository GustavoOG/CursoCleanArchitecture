using CleanArchitecture.Application.Users.GetusersDapperPagination;
using CleanArchitecture.Application.Users.GetUsersPagination;
using CleanArchitecture.Application.Users.LoginUser;
using CleanArchitecture.Application.Users.RegisterUser;
using CleanArchitecture.Domain.Abstractions;
using CleanArchitecture.Domain.Users;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CleanArchitecture.Api.Controllers.Users
{
    [Route("api/Users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ISender _sender;

        public UsersController(ISender sender)
        {
            _sender = sender;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserRequest request, CancellationToken cancellationToken)
        {

            var command = new LoginCommand(request.Email, request.Password);
            var result = await _sender.Send(command, cancellationToken);
            return result.IsSuccess
                ? Ok(result.Value)
                : Unauthorized(result.Error);
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserRequest request,
            CancellationToken cancellationToken)
        {
            var command = new RegisterUserCommand(request.Email, request.Nombre, request.Apellidos, request.Password);
            var result = await _sender.Send(command, cancellationToken);
            return result.IsSuccess
                ? Ok(result.Value)
                : Unauthorized(result.Error);
        }


        [AllowAnonymous]
        [HttpGet("getPagination", Name = "PaginationUsers")]
        [ProducesResponseType(typeof(PaginationResult<User, UserId>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<PaginationResult<User, UserId>>> GetPaginationUsers
            ([FromQuery] GetUsersPaginationQuery request)
        {
            var resultados = await _sender.Send(request);
            return Ok(resultados);
        }

        [AllowAnonymous]
        [HttpGet("getPaginationDapper", Name = "PaginationDapper")]
        [ProducesResponseType(typeof(PagedDapperResults<UserPaginationData>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<PagedDapperResults<UserPaginationData>>> GetPaginationPagger
       ([FromQuery] GetusersDapperPaginationQuery request)
        {
            var resultados = await _sender.Send(request);
            return Ok(resultados);
        }
    }
}
