using AutoMapper;
using ConsentCollector.Business.Consent.Models;
using ConsentCollector.Entities.Consent;
using System;

namespace ConsentCollector.Business
{
    public sealed class ConsentMappingProfile:Profile
    {
        public ConsentMappingProfile()
        {
            CreateMap<Survey, SurveyModel>();

            CreateMap<CreateSurveyModel, Survey>();

            CreateMap<Notification, NotificationModel>();

            CreateMap<NotificationModel, Notification>();
        }
    }
}
