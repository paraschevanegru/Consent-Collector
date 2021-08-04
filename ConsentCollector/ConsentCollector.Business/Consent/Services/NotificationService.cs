using AutoMapper;
using ConsentCollector.Business.Consent.Models;
using ConsentCollector.Entities.Consent;
using ConsentCollector.Persistence;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConsentCollector.Business.Consent.Services
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository notificationRepository;
        private readonly IMapper mapper;
        public NotificationService(INotificationRepository notificationRepository, IMapper mapper)
        {
            this.notificationRepository = notificationRepository;
            this.mapper = mapper;
        }

        public  IEnumerable<NotificationModel> GetByUserId(Guid userId)
        {
            var notification = notificationRepository.GetNotificationByUserId(userId);
            return mapper.Map<IEnumerable<NotificationModel>>(notification);
        }

        public IEnumerable<NotificationModel> GetByUserIdAndSeen(Guid userId,bool seen)
        {
            var notification = notificationRepository.GetNotificationByIdAndSeen(userId,seen);
            return mapper.Map<IEnumerable<NotificationModel>>(notification);
        }

        public async Task<NotificationModel> Create(CreateNotificationModel model)
        {
            var notification = this.mapper.Map<Notification>(model);

            await this.notificationRepository.Create(notification);

            await this.notificationRepository.SaveChanges();

            return mapper.Map<NotificationModel>(notification);
        }

        public async Task Delete(Guid notificationId)
        {
            var notification = await notificationRepository.GetNotificationById(notificationId);

            notificationRepository.Delete(notification);

            await notificationRepository.SaveChanges();
        }

        public async Task<NotificationModel> GetById(Guid id)
        {
            var notification = await notificationRepository.GetNotificationById(id);
            return mapper.Map<NotificationModel>(notification);
        }

        public async Task Update(Guid notificationId, NotificationModel model)
        {
            var notification = await notificationRepository.GetNotificationById(notificationId);

            mapper.Map(model, notification);

            notificationRepository.Update(notification);

            await notificationRepository.SaveChanges();
        
        }

        public IEnumerable<NotificationModel> GetAll()
        {
            var notification = notificationRepository.GetAll();

            return mapper.Map<IEnumerable<NotificationModel>>(notification);
        }
    }
}
