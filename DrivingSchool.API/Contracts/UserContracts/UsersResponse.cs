namespace DrivingSchool.API.Contracts.UserContracts
{
    public record UsersResponse(
        Guid Id,
        string? UserName,
        string? Email,
        string? Role,
        Dictionary<string, int[]>? ResultsTests
        );
}
