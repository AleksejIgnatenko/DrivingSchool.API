namespace DrivingSchool.API.Contracts.AnswerUserTestContracts
{
    public record AnswerUserTestRequest(
        Guid TestId,
        int ResultTest
        );
}
