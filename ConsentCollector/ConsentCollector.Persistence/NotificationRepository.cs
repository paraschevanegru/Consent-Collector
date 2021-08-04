using ConsentCollector.Entities.Consent;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsentCollector.Persistence
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly ConsentContext context;

        public NotificationRepository(ConsentContext context)
        {
            this.context = context;
        }
        public async Task Create(Notification notification)
        {
            await this.context.Notification.AddAsync(notification);
        }

        public void Delete(Notification notification)
        {
            context.Notification.Remove(notification);
        }

        public async Task<Notification> GetNotificationById(Guid id)
        {
            return await context.Notification.FirstAsync(n => n.Id == id);
        }

        public  IEnumerable<Notification> GetNotificationByUserId(Guid userId)
        {
            return  context.Notification.Where(n => n.IdUser == userId).ToList();
        }


        public IEnumerable<Notification> GetNotificationByIdAndSeen(Guid id, bool seen)
        {
            return context.Notification.Where(n => n.IdUser == id && n.Seen==seen).ToList();
        }

        public async Task SaveChanges()
        {
            await this.context.SaveChangesAsync();
        }

        public void Update(Notification notification)
        {
            this.context.Notification.Update(notification);
        }

        public IEnumerable<Notification> GetAll()
        {
            return context.Notification;
        }


    }
}
