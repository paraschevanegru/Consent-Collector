using ConsentCollector.Entities.Consent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ConsentCollector.Persistence.UserRepository
{
    public class UserDetailRepository : IUserDetailRepository
    {
        private readonly ConsentContext context;

        public UserDetailRepository(ConsentContext context)
        {
            this.context = context;
        }

        public async Task Create(UserDetail detail)
        {
            await this.context.UserDetail.AddAsync(detail);
        }

        public void Delete(UserDetail detail)
        {
            context.UserDetail.Remove(detail);
        }

        public IEnumerable<UserDetail> GetAll()
        {
            return context.UserDetail;
        }

        public async Task<UserDetail> GetUserDetailById(Guid id)
        {
            return await context.UserDetail.FirstAsync(d => d.Id == id);
        }

        public async Task SaveChanges()
        {
            await this.context.SaveChangesAsync();
        }

        public void Update(UserDetail detail)
        {
            this.context.UserDetail.Update(detail);
        }
    }
}
