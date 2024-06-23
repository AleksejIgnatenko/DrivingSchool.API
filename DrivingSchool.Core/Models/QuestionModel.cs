using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrivingSchool.Core.Models
{
    public class QuestionModel
    {
        public Guid Id { get; }
        public TestModel? Test { get; }
        public string? QuestionText { get; }
        public string? LinkPhoto { get; }
        public string? Answer1 { get; }
        public string? Answer2 { get; }
        public string? Answer3 { get; }
        public string? Answer4 { get; }
        public string? CorrectAnswer { get; }

        public QuestionModel(Guid id, string? questionText, string? linkPhoto, string? answer1, string? answer2, string? answer3, string? answer4, string? correctAnswer)
        {
            Id = id;
            QuestionText = questionText;
            LinkPhoto = linkPhoto;
            Answer1 = answer1;
            Answer2 = answer2;
            Answer3 = answer3;
            Answer4 = answer4;
            CorrectAnswer = correctAnswer;
        }

        public QuestionModel(Guid id, TestModel? test, string? questionText, string? linkPhoto, string? answer1, string? answer2, string? answer3, string? answer4, string? correctAnswer)
        {
            Id = id;
            Test = test;
            QuestionText = questionText;
            LinkPhoto = linkPhoto;
            Answer1 = answer1;
            Answer2 = answer2;
            Answer3 = answer3;
            Answer4 = answer4;
            CorrectAnswer = correctAnswer;
        }

        public static (QuestionModel question, string error) Create(Guid id, string? questionText, string? linkPhoto, string? answer1, string? answer2, string? answer3, string? answer4, string? correctAnswer)
        {
            string error = string.Empty;
            QuestionModel question = new QuestionModel(id, questionText, linkPhoto, answer1, answer2, answer3, answer4, correctAnswer);
            return (question, error);
        }

        public static (QuestionModel question, string error) Create(Guid id, TestModel? test, string? questionText, string? linkPhoto, string? answer1, string? answer2, string? answer3, string? answer4, string? correctAnswer)
        {
            string error = string.Empty;
            QuestionModel question = new QuestionModel(id, test, questionText, linkPhoto, answer1, answer2, answer3, answer4, correctAnswer);
            return (question, error);
        }
    }
}
