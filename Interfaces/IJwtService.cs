using TaskMini.Entities;

namespace TaskMini.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(User user);
    }
}
