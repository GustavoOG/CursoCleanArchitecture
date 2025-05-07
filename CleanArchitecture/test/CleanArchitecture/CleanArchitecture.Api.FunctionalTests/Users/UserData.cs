using CleanArchitecture.Application.Users.RegisterUser;

namespace CleanArchitecture.Api.FunctionalTests.Users
{
    internal static class UserData
    {
        public static RegisterUserRequest RegisterUserRequestTest =
            new RegisterUserRequest("jdxtavo@gmail.com", "Gustavo", "Ortiz", "12345678a");
    }
}
