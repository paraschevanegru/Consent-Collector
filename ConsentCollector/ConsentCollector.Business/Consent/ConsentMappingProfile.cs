using AutoMapper;
using ConsentCollector.Business.Consent.Models;
using ConsentCollector.Business.Consent.Models.CommentModel;
using ConsentCollector.Business.Consent.Models.HistoryModel;
using ConsentCollector.Business.Consent.Models.QuestionModel;
using ConsentCollector.Business.Consent.Models.SurveyQuestionModel;
using ConsentCollector.Business.Consent.Models.UserDetails;
using ConsentCollector.Business.Consent.Models.Users;
using ConsentCollector.Entities.Consent;

namespace ConsentCollector.Business
{
    public sealed class ConsentMappingProfile:Profile
    {
        public ConsentMappingProfile()
        {
            CreateMap<Survey, SurveyModel>();

            CreateMap<CreateSurveyModel, Survey>();
            CreateMap<History, HistoryModel>();
            CreateMap<CreateHistoryModel, History>();

            CreateMap<User, UserModel>();

            CreateMap<CreateUserModel, User>();


            CreateMap<UserDetail, UserDetailModel>();

            CreateMap<CreateUserDetailModel, UserDetail>();
            
            CreateMap<Notification, NotificationModel>();
            CreateMap<CreateNotificationModel, Notification>();

            CreateMap<NotificationModel, Notification>();

            CreateMap<Question, QuestionModel>();

            CreateMap<CreateQuestionModel, Question>();

            CreateMap<Comment, CommentModel>();

            CreateMap<CreateCommentModel, Comment>();

            CreateMap<Answer, AnswerModel>();
            CreateMap<CreateAnswerModel, Answer>();

            CreateMap<SurveyQuestion, SurveyQuestionModel>();

            CreateMap<CreateSurveyQuestionModel, SurveyQuestion>();

        }
    }
}
