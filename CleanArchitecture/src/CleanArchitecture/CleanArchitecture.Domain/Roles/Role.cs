using CleanArchitecture.Domain.Permissions;
using CleanArchitecture.Domain.Shared;

namespace CleanArchitecture.Domain.Roles
{
    public sealed class Role : Enumeration<Role>
    {
        public static Role Cliente = new Role(1, "Cliente");

        public static Role Admin = new Role(2, "Admin");

        public Role(int id, string name) : base(id, name)
        {
        }

        public ICollection<Permission>? Permissions { get; set; }
    }
}
