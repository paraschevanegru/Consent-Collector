using ConsentCollector.Entities.Consent;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConsentCollector.Persistence.UserRepository
{
    public class UserDetailRepository : IUserDetailRepository
    {
        private readonly ConsentContext context;

        public UserDetailRepository(ConsentContext context)
        {
            this.context = context;
        }

        public async Task<UserDetail> GetUserDetailByUserId(Guid userId)
        {
            return await context.UserDetail.FirstAsync(d => d.IdUser == userId);
        }

        public async Task<UserDetail> GetUserDetailByEmailAndNumber(string email, string number)
        {
            return await context.UserDetail.FirstAsync(d => d.Email == email && d.Number == number);
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
