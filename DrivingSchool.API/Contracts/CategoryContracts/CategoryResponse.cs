
namespace DrivingSchool.API.Contracts.CategoryContracts
{
    public record CategoryResponse(
        Guid Id,
        string? NameCategory
        //Dictionary<Guid, string?> Tests
    );
}
