using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ConsentCollector.Business.Consent.Models.CommentModel;
using ConsentCollector.Business.Consent.Models.Users;
using ConsentCollector.Business.Consent.Services.CommentService;
using ConsentCollector.Business.Consent.Services.Users;
using ConsentCollector.Entities.Consent;
using ConsentCollector.Persistence.CommentRepository;
using ConsentCollector.Persistence.UserRepository;
using FluentAssertions;
using Microsoft.VisualStudio.TestPlatform.CoreUtilities.Extensions;
using Moq;
using Xunit;

namespace TestConsentCollector
{
    public class UserServiceTest:IDisposable
    {
        private readonly MockRepository mockRespository;
        private Mock<IUserRepository> usersRepositoryMock;
        private Mock<IMapper> mapperMock;

        private UserService sut;
        public UserServiceTest()
        {
             mockRespository = new MockRepository(MockBehavior.Strict);

             usersRepositoryMock = mockRespository.Create<IUserRepository>();
             mapperMock = mockRespository.Create<IMapper>();

             sut = new UserService(usersRepositoryMock.Object, mapperMock.Object);
        }

        public void Dispose()
        {
            mockRespository.VerifyAll();
        }

        [Fact]
        public async void When_GetById_IsCalled_Expect_GetUserByIdToBeInvoked_And_UsersToBeReturned()
        {
            //Arrange
            var user = new User("Username", "Password", "Role");

            var expectedResult = new UserModel()
            {
               Id = user.Id,
               Password = user.Password,
               Username = user.Username,
               Role=user.Role
            };

            usersRepositoryMock
                .Setup(u => u.GetUserById(user.Id))
                .ReturnsAsync(user);

            mapperMock
                .Setup(m => m.Map<UserModel>(user))
                .Returns(expectedResult);

            //Act
            var result = await sut.GetById(user.Id);

            //Assert
            result.Should().BeEquivalentTo(expectedResult);
        }



        [Fact]
        public async void When_Create_IsCalled_Expect_CreateAndSaveChangesToBeInvoked_And_UsersToBeCreated()
        {
            //Arrange
            var user = new User("Username", "Password", "Role");
            var model= new CreateUserModel();

            var expectedResult = new UserModel()
            {
                Id = user.Id,
                Password = user.Password,
                Username = user.Username,
                Role = user.Role
            };

            mapperMock
                .Setup(u => u.Map<User>(model))
                .Returns(user);

            usersRepositoryMock
                .Setup(u => u.Create(user))
                .Returns(Task.CompletedTask);

            usersRepositoryMock
                .Setup(u => u.SaveChanges())
                .Returns(Task.CompletedTask);

            mapperMock
                .Setup(m => m.Map<UserModel>(user))
                .Returns(expectedResult);

            //Act
            var result = await sut.Create(model);

            //Assert
            result.Should().BeEquivalentTo(expectedResult);
        }

        [Fact]
        public void When_GetAll_IsCalled_Expect_GetAllToBeInvoked_And_AllUsersToBeReturned()
        {
            //Arrange
            var users = new List<User>();
            users.Add(new User("Username", "Password", "Role"));
            users.Add(new User("Username1", "Password1", "Role1"));

            var expectedResult = users.Select(u =>new UserModel()
            {
                Id = u.Id,
                Password = u.Password,
                Username = u.Username,
                Role = u.Role
            });

            usersRepositoryMock
                .Setup(u => u.GetAll())
                .Returns(users);

            mapperMock
                .Setup(m => m.Map<IEnumerable<UserModel>>(users))
                .Returns(expectedResult);

            //Act
            var result = sut.GetAll();

            //Assert
            result.Should().BeEquivalentTo(expectedResult);
        }


        [Fact]
        public async void When_Delete_IsCalled_Expect_GetUserByIdAndDeleteAndSaveChangesToBeInvoked()
        {
            //Arrange
            var user = new User("Username", "Password", "Role");

            usersRepositoryMock
                .Setup(u => u.GetUserById(user.Id))
                .ReturnsAsync(user);

            usersRepositoryMock
                .Setup(u => u.Delete(user));

            usersRepositoryMock
                .Setup(u => u.SaveChanges())
                .Returns(Task.CompletedTask);

            //Act
            await sut.Delete(user.Id);

            //Assert
        }

        [Fact]
        public async void When_Update_IsCalled_Expect_GetUserByIdAndUpdateAndSaveChangesToBeInvoked()
        {
            //Arrange
            var user = new User("Username", "Password", "Role");
            var expectedResult = new CreateUserModel()
            {
                Password = user.Password,
                Username = user.Username,
                Role = user.Role
            };

            usersRepositoryMock
                .Setup(u => u.GetUserById(user.Id))
                .ReturnsAsync(user);

            mapperMock
                .Setup(mapper => mapper.Map(expectedResult, user))
                .Returns(user);

            usersRepositoryMock
                .Setup(u => u.Update(user));

            usersRepositoryMock
                .Setup(u => u.SaveChanges())
                .Returns(Task.CompletedTask);

            //Act
            await sut.Update(user.Id, expectedResult);

            //Assert
        }
    }
}
