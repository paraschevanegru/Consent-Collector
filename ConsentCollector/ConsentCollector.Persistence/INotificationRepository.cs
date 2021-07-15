using ConsentCollector.Entities.Consent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsentCollector.Persistence
{
    public interface INotificationRepository
    {
        Task<Notification> GetNotificationById(Guid id);

        Task Create(Notification notification);

        Task SaveChanges();

        void Delete(Notification notification);
        void Update(Notification notification);
    }
}
