using ConsentCollector.Entities.Consent;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConsentCollector.Persistence.UserRepository
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAll();
        Task<User> GetUserById(Guid id);

        Task<User> GetUserByUsername(string username);

        Task Create(User user);

        Task SaveChanges();

        void Delete(User user);
        void Update(User user);
    }
}
