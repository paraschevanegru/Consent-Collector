using ConsentCollector.Business.Consent.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsentCollector.Business.Consent.Services
{
    public interface INotificationService
    {
        Task<NotificationModel> GetById(Guid id);

        Task<NotificationModel> Create(NotificationModel model);

        Task Delete(Guid notificationId);

        Task Update(Guid notificationId, NotificationModel model);
    }
}
