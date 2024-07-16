using DrivingSchool.Core.Enum;

namespace DrivingSchool.API.Contracts.UserContracts
{
    public record UsersResponse(
/*        Guid IdUser,*/
        string? UserName,
        string? Email
/*        string? Password,
        string? Role,
        Dictionary<Guid, int[]>? ResultTest*/
        );
}
