using DrivingSchool.Core.Models;

namespace DrivingSchool.API.Contracts.CategoryContracts
{
    public record CategoryResponse(
        Guid IdUser,
        string? NameCategory,
        Dictionary<Guid, string>? Tests
    );
}
