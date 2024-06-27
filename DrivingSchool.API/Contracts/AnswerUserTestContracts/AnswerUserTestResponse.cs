namespace DrivingSchool.API.Contracts.AnswerUserTestContracts
{
    public record AnswerUserTestResponse(
        Guid Id,
        string? UserName,
        string? NameTest,
        int ResultTest
        );
}
