using ConsentCollector.Entities.Consent;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConsentCollector.Persistence.UserRepository
{
    public class UserRepository : IUserRepository
    {
        private readonly ConsentContext context;

        public UserRepository(ConsentContext context)
        {
            this.context = context;
        }

        public async Task<User> GetUserByUsername(string username)
        {
            return await context.User.FirstAsync(u => u.Username == username);
        }

        public async Task Create(User user)
        {
            await this.context.User.AddAsync(user);
        }

        public void Delete(User user)
        {
            context.User.Remove(user);
        }

        public IEnumerable<User> GetAll()
        {
            return context.User;
        }

        public async Task<User> GetUserById(Guid id)
        {
            return await context.User
                .FirstAsync(u => u.Id == id);
        }


        public async Task SaveChanges()
        {
            await this.context.SaveChangesAsync();
        }

        public void Update(User user)
        {
            this.context.User.Update(user);
        }
    }
}
