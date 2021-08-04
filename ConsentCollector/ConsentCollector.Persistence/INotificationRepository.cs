using ConsentCollector.Entities.Consent;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConsentCollector.Persistence
{
    public interface INotificationRepository
    {
        Task<Notification> GetNotificationById(Guid id);

        IEnumerable<Notification> GetNotificationByIdAndSeen(Guid id, bool seen);

        IEnumerable<Notification> GetNotificationByUserId(Guid userId);
        IEnumerable<Notification> GetAll();

        Task Create(Notification notification);

        Task SaveChanges();

        void Delete(Notification notification);
        void Update(Notification notification);
    }
}
