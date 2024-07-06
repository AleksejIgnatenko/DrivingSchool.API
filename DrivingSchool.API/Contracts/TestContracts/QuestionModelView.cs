namespace DrivingSchool.API.Contracts.TestContracts
{
    public record QuestionModelView(
        Guid Id,
        string? QuestionText,
        string? LinkPhoto,
        string? Answer1,
        string? Answer2,
        string? Answer3,
        string? Answer4,
        string? CorrectAnswer
        );
}
