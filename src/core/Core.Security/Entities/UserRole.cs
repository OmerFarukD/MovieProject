using Core.DataAccess.Entities;

namespace Core.Security.Entities;

public class UserRole : Entity<int>
{

    public int RoleId { get; set; }
    public Role Role { get; set; } = new Role();

    public int UserId { get; set; }
    public User User { get; set; } = new User();
}
