namespace DrivingSchool.API.Contracts.TestContracts
{
    public record TestResponse(
        Guid IdTest,
        string? NameCategory,
        string? NameTest,
        Dictionary<Guid, string?> Questions
        );
}
