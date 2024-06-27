namespace DrivingSchool.API.Contracts.AnswerUserTestContracts
{
    public record AnswerUserTestRequest(
        Guid UserId,
        Guid TestId,
        int ResultTest
        );
}
