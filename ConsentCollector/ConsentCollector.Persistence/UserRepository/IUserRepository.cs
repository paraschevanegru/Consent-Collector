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

        Task Create(User user);

        Task SaveChanges();

        void Delete(User user);
        void Update(User user);
    }
}
