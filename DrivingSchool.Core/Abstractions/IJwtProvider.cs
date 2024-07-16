using DrivingSchool.Core.Models;

namespace DrivingSchool.Infrastructure
{
    public interface IJwtProvider
    {
        string GenerateToken(UserModel user);
        Guid GetIdFromToken(string jwtToken);
    }
}