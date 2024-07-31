using DrivingSchool.Core.Models;
using DrivingSchool.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrivingSchool.BusinessLogic.QuestionServices
{
    public class QuestionServices : IQuestionServices
    {
        private readonly IQuestionRepository _questionRepository;

        public QuestionServices(IQuestionRepository questionRepository)
        {
            this._questionRepository = questionRepository;
        }

        public async Task<List<QuestionModel>> GetAllQuestionsAsync()
        {
            return await _questionRepository.GetAsync();
        }


        public async Task<Guid> CreateQuestionAsync(QuestionModel question)
        {
            return await _questionRepository.CreateAsync(question);
        }


        public async Task<QuestionModel> UpdateQuestionAsync(Guid id, Guid testId, string? questionText, string? linkPhoto, string? answer1, string? answer2, string? answer3, string? answer4, string? correctAnswer)
        {
            return await _questionRepository.UpdateAsync(id, testId, questionText, linkPhoto, answer1, answer2, answer3, answer4, correctAnswer);
        }

        public async Task<Guid> DeleteQuestionAsync(Guid id)
        {
            return await _questionRepository.DeleteAsync(id);
        }
    }
}
