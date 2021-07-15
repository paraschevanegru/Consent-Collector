using AutoMapper;
using ConsentCollector.Business.Consent.Models;
using ConsentCollector.Entities.Consent;
using System;
using ConsentCollector.Business.Consent.Models.UserDetails;
using ConsentCollector.Business.Consent.Models.Users;

namespace ConsentCollector.Business
{
    public sealed class ConsentMappingProfile:Profile
    {
        public ConsentMappingProfile()
        {
            CreateMap<Survey, SurveyModel>();

            CreateMap<CreateSurveyModel, Survey>();

            CreateMap<User, UserModel>();

            CreateMap<CreateUserModel, User>();


            CreateMap<UserDetail, UserDetailModel>();

            CreateMap<CreateUserDetailModel, UserDetail>();
        }
    }
}
