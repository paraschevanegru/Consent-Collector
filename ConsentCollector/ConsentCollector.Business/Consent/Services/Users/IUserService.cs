using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsentCollector.Business.Consent.Models;
using ConsentCollector.Business.Consent.Models.Users;

namespace ConsentCollector.Business.Consent.Services.Users
{
    public interface IUserService
    {
        IEnumerable<UserModel> GetAll();
        Task<UserModel> GetById(Guid id);

        Task<UserModel> Create(CreateUserModel model);

        Task Delete(Guid userId);

        Task Update(Guid userId, CreateUserModel model);
    }
}
 