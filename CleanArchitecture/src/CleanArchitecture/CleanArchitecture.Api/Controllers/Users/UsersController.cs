using Asp.Versioning;
using CleanArchitecture.Api.Utils;
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
    [ApiController]
    [ApiVersion(ApiVersions.V1)]
    //[ApiVersion(ApiVersions.V2)]
    [Route("api/v{version:apiVersion}/Users")]
    public class UsersController : ControllerBase
    {
        private readonly ISender _sender;

        public UsersController(ISender sender)
        {
            _sender = sender;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        [MapToApiVersion(ApiVersions.V1)]
        public async Task<IActionResult> LoginV1([FromBody] LoginUserRequest request, CancellationToken cancellationToken)
        {
            var command = new LoginCommand(request.Email, request.Password);
            var result = await _sender.Send(command, cancellationToken);
            return result.IsSuccess
                ? Ok(result.Value)
                : Unauthorized(result.Error);
        }

        //[AllowAnonymous]
        //[HttpPost("login")]
        //[MapToApiVersion(ApiVersions.V2)]
        //public async Task<IActionResult> LoginV2([FromBody] LoginUserRequest request, CancellationToken cancellationToken)
        //{
        //    var command = new LoginCommand(request.Email, request.Password);
        //    var result = await _sender.Send(command, cancellationToken);
        //    return result.IsSuccess
        //        ? Ok(result.Value)
        //        : Unauthorized(result.Error);
        //}

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
        [ProducesResponseType(typeof(PagedResults<User, UserId>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<PagedResults<User, UserId>>> GetPaginationUsers
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
