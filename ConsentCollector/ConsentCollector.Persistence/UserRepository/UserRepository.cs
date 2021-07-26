using ConsentCollector.Entities.Consent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ConsentCollector.Persistence.UserRepository
{
    public class UserRepository : IUserRepository
    {
        private readonly ConsentContext context;

        public UserRepository(ConsentContext context)
        {
            this.context = context;
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

        public async Task<User> GetUserByUsernameAndPassword(string username, string password)
        {
            return await context.User.FirstAsync(u => u.Username == username && u.Password == password);
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
