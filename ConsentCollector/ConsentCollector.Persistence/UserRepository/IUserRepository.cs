using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsentCollector.Entities.Consent;

namespace ConsentCollector.Persistence.UserRepository
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAll();
        Task<User> GetUserById(Guid id);

        Task<User> GetUserByUsernameAndPassword(string username, string password);

        Task Create(User user);

        Task SaveChanges();

        void Delete(User user);
        void Update(User user);
    }
}
