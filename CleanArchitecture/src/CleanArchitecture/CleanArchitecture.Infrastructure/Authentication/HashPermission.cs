using CleanArchitecture.Domain.Permissions;
using Microsoft.AspNetCore.Authorization;

namespace CleanArchitecture.Infrastructure.Authentication
{
    public class HashPermission : AuthorizeAttribute
    {
        public HashPermission(PermissionEnum permission) : base(policy: permission.ToString())
        {

        }
    }
}
