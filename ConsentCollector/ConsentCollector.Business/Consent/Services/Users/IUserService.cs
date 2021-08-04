using ConsentCollector.Business.Consent.Models.Users;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConsentCollector.Business.Consent.Services.Users
{
    public interface IUserService
    {
        IEnumerable<UserModel> GetAll();
        Task<UserModel> GetById(Guid id);

        Task<UserModel> GetByUsername(string username);
        Task<UserModel> Create(CreateUserModel model);

        Task Delete(Guid userId);

        Task Update(Guid userId, CreateUserModel model);
    }
}
 