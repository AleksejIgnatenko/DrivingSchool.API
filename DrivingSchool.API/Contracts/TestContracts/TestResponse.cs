﻿using DrivingSchool.API.Contracts.QuestionContracts;

namespace DrivingSchool.API.Contracts.TestContracts
{
    public record TestResponse(
        Guid IdTest,
        string? NameCategory,
        string? NameTest,
        List<QuestionResponse> Questions
        );
}
