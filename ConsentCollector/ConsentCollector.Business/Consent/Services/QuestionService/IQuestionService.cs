using ConsentCollector.Business.Consent.Models.QuestionModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsentCollector.Business.Consent.Services.QuestionService
{
    public interface IQuestionService
    {
        IEnumerable<QuestionModel> GetAll();
        Task<QuestionModel> GetById(Guid id);

        Task<QuestionModel> Create(CreateQuestionModel model);

        Task Delete(Guid questionId);

        Task Update(Guid questionId, CreateQuestionModel model);
    }
}
