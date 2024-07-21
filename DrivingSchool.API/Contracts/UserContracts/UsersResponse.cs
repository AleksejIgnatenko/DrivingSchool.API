namespace DrivingSchool.API.Contracts.UserContracts
{
    public record UsersResponse(
        Guid IdUser,
        string? UserName,
        string? Email,
        string? Role,
        Dictionary<string, int[]>? ResultsTests
        );
}
