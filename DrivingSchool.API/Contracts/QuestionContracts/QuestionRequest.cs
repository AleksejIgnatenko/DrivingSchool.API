namespace DrivingSchool.API.Contracts.QuestionContracts
{
    public record QuestionRequest(
        Guid IdTest,
        string? QuestionText,
        string? LinkPhoto,    
        string? Answer1,
        string? Answer2,
        string? Answer3,
        string? Answer4,
        string? CorrectAnswer
        );
}
