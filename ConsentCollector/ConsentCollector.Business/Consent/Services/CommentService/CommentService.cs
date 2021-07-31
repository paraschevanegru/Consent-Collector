using AutoMapper;
using ConsentCollector.Business.Consent.Models.CommentModel;
using ConsentCollector.Entities.Consent;
using ConsentCollector.Persistence.CommentRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsentCollector.Business.Consent.Services.CommentService
{
    public sealed class CommentService : ICommentService
    {
        private readonly ICommentRepository commentRepository;
        private readonly IMapper mapper;
        public CommentService(ICommentRepository commentRepository, IMapper mapper)
        {
            this.commentRepository = commentRepository;
            this.mapper = mapper;
        }
        public async Task<CommentModel> GetById(Guid id)
        {
            var comment = await commentRepository.GetCommentById(id);
            return mapper.Map<CommentModel>(comment);
        }

        public async Task<CommentModel> GetByUserAndSurveyId(Guid userId, Guid surveyId)
        {
            var comment = await commentRepository.GetAnswerByUserAndSurveyId(userId, surveyId);
            return mapper.Map<CommentModel>(comment);
        }

        public async Task<CommentModel> Create(CreateCommentModel model)
        {
            var comment = this.mapper.Map<Comment>(model);

            await this.commentRepository.Create(comment);

            await this.commentRepository.SaveChanges();

            return mapper.Map<CommentModel>(comment);
        }

        public async Task Delete(Guid commentId)
        {
            var comment = await commentRepository.GetCommentById(commentId);

            commentRepository.Delete(comment);

            await commentRepository.SaveChanges();
        }

        public async Task Update(Guid commentId, CreateCommentModel model)
        {
            var comment = await commentRepository.GetCommentById(commentId);

            mapper.Map(model, comment);

            commentRepository.Update(comment);

            await commentRepository.SaveChanges();
        }

        public IEnumerable<CommentModel> GetAll()
        {
            var comment = commentRepository.GetAll();

            return mapper.Map<IEnumerable<CommentModel>>(comment);
        }
    }
}
