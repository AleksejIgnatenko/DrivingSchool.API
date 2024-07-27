namespace DrivingSchool.API.Contracts.TestContracts
{
    public record GetAllTestResponse(
        Guid Id,
        string? NameCategory,
        string? NameTest
        );
}
