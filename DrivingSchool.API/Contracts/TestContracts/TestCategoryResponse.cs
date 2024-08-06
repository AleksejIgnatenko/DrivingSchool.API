using DrivingSchool.API.Contracts.QuestionContracts;

namespace DrivingSchool.API.Contracts.TestContracts
{
    public record TestCategoryResponse(
        Guid IdTest,
        string? NameTest,
        List<QuestionResponse> Questions
    );
}
