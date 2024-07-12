using DrivingSchool.Core.Enum;

namespace DrivingSchool.API.Contracts.UserContracts
{
    public record UpdateUsersRequest(
        string UserName,
        string Email,
        string Password,
        RoleEnum Role
        );
}
