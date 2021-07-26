using ConsentCollector.Business.Consent.Models.UserDetails;
using ConsentCollector.Persistence.UserRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ConsentCollector.Entities.Consent;
using ConsentCollector.Persistence;

namespace ConsentCollector.Business.Consent.Services.UserDetails
{
    public sealed class UserDetailService : IUserDetailService
    {
        private readonly IMapper mapper;
        private readonly IUserDetailRepository userDetailRepository;

        public UserDetailService(IUserDetailRepository userDetailRepository, IMapper mapper)
        {
            this.userDetailRepository = userDetailRepository;
            this.mapper = mapper;
        }

        public async Task<UserDetailModel> GetByUserId(Guid userId)
        {
            var detail = await userDetailRepository.GetUserDetailByUserId(userId);

            return mapper.Map<UserDetailModel>(detail);
        }

        public async Task<UserDetailModel> GetByEmailAndNumber(string email, string number)
        {
            var detail = await userDetailRepository.GetUserDetailByEmailAndNumber(email,number);

            return mapper.Map<UserDetailModel>(detail);
        }

        public async Task<UserDetailModel> Create(CreateUserDetailModel model)
        {
            var detail = this.mapper.Map<UserDetail>(model);

            await this.userDetailRepository.Create(detail);

            await this.userDetailRepository.SaveChanges();

            return mapper.Map<UserDetailModel>(detail);
        }

        public async Task Delete(Guid detailId)
        {
            var user = await userDetailRepository.GetUserDetailById(detailId);

            userDetailRepository.Delete(user);

            await userDetailRepository.SaveChanges();
        }

        public IEnumerable<UserDetailModel> GetAll()
        {
            var details = userDetailRepository.GetAll();

            return mapper.Map<IEnumerable<UserDetailModel>>(details);
        }

        public async Task<UserDetailModel> GetById(Guid id)
        {
            var detail = await userDetailRepository.GetUserDetailById(id);

            return mapper.Map<UserDetailModel>(detail);
        }

        public async Task Update(Guid detailId, CreateUserDetailModel model)
        {
            var user = await userDetailRepository.GetUserDetailById(detailId);

            mapper.Map(model, user);

            userDetailRepository.Update(user);

            await userDetailRepository.SaveChanges();
        }
        //public async Task<IEnumerable<UserDetailModel>> Get(Guid userId)
        //{
        //    var user = await userRepository.GetUserById(userId);
        //    Console.WriteLine(user.Detail);
        //    Console.WriteLine(mapper.Map<IEnumerable<UserDetailModel>>(user.Detail));
        //    Console.WriteLine(user.Id);


        //    return mapper.Map<IEnumerable<UserDetailModel>>(user.Detail);
        //}
    }
}
