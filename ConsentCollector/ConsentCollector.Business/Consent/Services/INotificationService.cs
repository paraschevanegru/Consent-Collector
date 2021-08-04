using ConsentCollector.Business.Consent.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConsentCollector.Business.Consent.Services
{
    public interface INotificationService
    {
        Task<NotificationModel> GetById(Guid id);

        IEnumerable<NotificationModel> GetByUserId(Guid userId);

        IEnumerable<NotificationModel> GetByUserIdAndSeen(Guid userId,bool seen);

        Task<NotificationModel> Create(CreateNotificationModel model);

        Task Delete(Guid notificationId);

        Task Update(Guid notificationId, NotificationModel model);

        IEnumerable<NotificationModel> GetAll();
    }
}
