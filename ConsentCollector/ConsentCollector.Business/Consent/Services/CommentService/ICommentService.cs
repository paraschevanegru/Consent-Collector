using ConsentCollector.Business.Consent.Models.CommentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsentCollector.Business.Consent.Services.CommentService
{
    public interface ICommentService
    {
        IEnumerable<CommentModel> GetAll();
        Task<CommentModel> GetById(Guid id);

        Task<CommentModel> Create(CreateCommentModel model);

        Task Delete(Guid commentId);
        Task<CommentModel> GetByUserAndSurveyId(Guid userId, Guid surveyId);
        Task Update(Guid commentId, CreateCommentModel model);
    }
}
