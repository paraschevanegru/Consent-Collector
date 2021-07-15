using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsentCollector.Entities.Consent;

namespace ConsentCollector.Persistence.UserRepository
{
    public interface IUserDetailRepository
    {
        IEnumerable<UserDetail> GetAll();

        Task<UserDetail> GetUserDetailById(Guid id);

        Task Create(UserDetail detail);

        Task SaveChanges();

        void Delete(UserDetail detail);
        void Update(UserDetail detail);
    }
}
