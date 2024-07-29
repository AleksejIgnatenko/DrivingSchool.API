namespace DrivingSchool.API.Contracts.TestContracts
{
    public record GetTestResponse(
        Guid Id,
        string? NameCategory,
        string? NameTest
        );
}
