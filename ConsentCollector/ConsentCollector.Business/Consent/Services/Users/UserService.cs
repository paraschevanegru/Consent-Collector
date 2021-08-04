using AutoMapper;
using ConsentCollector.Business.Consent.Models.Users;
using ConsentCollector.Entities.Consent;
using ConsentCollector.Persistence.UserRepository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConsentCollector.Business.Consent.Services.Users
{
    public sealed class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            this.userRepository = userRepository;
            this.mapper = mapper;
        }

        public async Task<UserModel> GetByUsername(string username)
        {
            var user = await userRepository.GetUserByUsername(username);

            return mapper.Map<UserModel>(user);
        }

        public async Task<UserModel> Create(CreateUserModel model)
        {
            var user = this.mapper.Map<User>(model);

            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

            await this.userRepository.Create(user);

            await this.userRepository.SaveChanges();

            return mapper.Map<UserModel>(user);
        }

        public async Task Delete(Guid userId)
        {
            var user = await userRepository.GetUserById(userId);


            userRepository.Delete(user);

            await userRepository.SaveChanges();
        }

        public IEnumerable<UserModel> GetAll()
        {
            var users = userRepository.GetAll();

            return mapper.Map<IEnumerable<UserModel>>(users);
        }

        public async Task<UserModel> GetById(Guid id)
        {
            var user = await userRepository.GetUserById(id);
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

            return mapper.Map<UserModel>(user);
        }

        public async Task Update(Guid userId, CreateUserModel model)
        {
            var user = await userRepository.GetUserById(userId);

            mapper.Map(model, user);

            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

            userRepository.Update(user);

            await userRepository.SaveChanges();
        }
    }
}
