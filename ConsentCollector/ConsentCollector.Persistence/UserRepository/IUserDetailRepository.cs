using ConsentCollector.Entities.Consent;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConsentCollector.Persistence.UserRepository
{
    public interface IUserDetailRepository
    {
        IEnumerable<UserDetail> GetAll();

        Task<UserDetail> GetUserDetailById(Guid id);

        Task<UserDetail> GetUserDetailByUserId(Guid userId);

        Task<UserDetail> GetUserDetailByEmailAndNumber(string email, string number);

        Task Create(UserDetail detail);

        Task SaveChanges();

        void Delete(UserDetail detail);
        void Update(UserDetail detail);
    }
}
