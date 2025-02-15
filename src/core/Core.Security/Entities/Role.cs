using Core.DataAccess.Entities;

namespace Core.Security.Entities;

public class Role : Entity<int>
{
    public string Name { get; set; } = string.Empty;

    public ICollection<UserRole> UserRoles { get; set; } = new HashSet<UserRole>();
}
