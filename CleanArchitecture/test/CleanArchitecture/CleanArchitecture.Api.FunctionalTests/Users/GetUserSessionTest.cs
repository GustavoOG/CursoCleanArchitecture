using CleanArchitecture.Api.FunctionalTests.Infrastructure;
using CleanArchitecture.Application.Users.GetUserSession;
using CleanArchitecture.Application.Users.LoginUser;
using CleanArchitecture.Application.Users.RegisterUser;
using FluentAssertions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Xunit;

namespace CleanArchitecture.Api.FunctionalTests.Users
{
    public class GetUserSessionTest : BaseFunctionalTest
    {
        public GetUserSessionTest(FunctionalTestWebAppFactory factory) : base(factory)
        {
        }

        [Fact]
        public async Task Get_ShouldReturnUnauthorized_WhenTokenIsMissing()
        {
            //act
            var response = await HttpClient.GetAsync("api/v1/users/me");
            //assert
            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task Get_ShouldReturnUser_WhenTokenExists()
        {
            //arrange
            var token = await GetToken();
            HttpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue(
                JwtBearerDefaults.AuthenticationScheme,
                token);

            //act
            var user = await HttpClient.GetFromJsonAsync<UserResponse>("api/v1/users/me");

            //assert
            user.Should().NotBeNull();
        }


        [Fact]
        public async Task Login_ShouldReturnOk_WhenUserExists()
        {
            //arrange
            var request = new LoginUserRequest(
                UserData.RegisterUserRequestTest.Email,
                UserData.RegisterUserRequestTest.Password
                );


            //act
            var response = await HttpClient.PostAsJsonAsync("api/v1/users/login", request);

            //assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Register_ShouldReturnOk_WhenRequestIsValid()
        {
            //arrange
            var request = new RegisterUserRequest(
                "pruebas@pruebas.com", "nombre", "apellido", "12345678a"
                );
            //act
            var response = await HttpClient.PostAsJsonAsync("api/v1/users/register", request);
            //assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }

}
