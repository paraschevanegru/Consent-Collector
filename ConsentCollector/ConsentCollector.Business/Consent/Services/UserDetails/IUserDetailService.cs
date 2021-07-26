using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsentCollector.Business.Consent.Models.UserDetails;

namespace ConsentCollector.Business.Consent.Services.UserDetails
{
    public interface IUserDetailService
    {
        //Task<IEnumerable<UserDetailModel>> Get(Guid userId);
        IEnumerable<UserDetailModel> GetAll();

        Task<UserDetailModel> GetById(Guid id);

        Task<UserDetailModel> GetByUserId(Guid userId);

        Task<UserDetailModel> GetByEmailAndNumber(string email, string number);
        Task<UserDetailModel> Create(CreateUserDetailModel model);

        Task Delete(Guid detailId);

        Task Update(Guid detailId, CreateUserDetailModel model);
    }
}
