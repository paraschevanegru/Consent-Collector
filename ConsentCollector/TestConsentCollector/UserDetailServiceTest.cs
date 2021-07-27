using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ConsentCollector.Business.Consent.Models.UserDetails;
using ConsentCollector.Business.Consent.Models.Users;
using ConsentCollector.Business.Consent.Services.UserDetails;
using ConsentCollector.Business.Consent.Services.Users;
using ConsentCollector.Entities.Consent;
using ConsentCollector.Persistence.UserRepository;
using FluentAssertions;
using Xunit;

namespace TestConsentCollector
{
    public class UserDetailServiceTest : IDisposable
    {
        private readonly MockRepository mockRespository;
        private Mock<IUserDetailRepository> usersDetailsRepositoryMock;
        private Mock<IMapper> mapperMock;

        private UserDetailService sut;
        public UserDetailServiceTest()
        {
            mockRespository = new MockRepository(MockBehavior.Strict);

            usersDetailsRepositoryMock = mockRespository.Create<IUserDetailRepository>();
            mapperMock = mockRespository.Create<IMapper>();

            sut = new UserDetailService(usersDetailsRepositoryMock.Object, mapperMock.Object);
        }

        public void Dispose()
        {
            mockRespository.VerifyAll();
        }

        [Fact]
        public async void When_GetById_IsCalled_Expect_GetUserDetailByIdToBeInvoked_And_UsersDetailsToBeReturned()
        {
            //Arrange
            var userDetails = new UserDetail(Guid.NewGuid(), "Firstname", "Lastname", "Number", "Email");

            var expectedResult = new UserDetailModel()
            {
                Id = userDetails.Id,
                Firstname = userDetails.Firstname,
                Lastname = userDetails.Lastname,
                Number = userDetails.Number,
                Email = userDetails.Email
            };

            usersDetailsRepositoryMock
                .Setup(u => u.GetUserDetailById(userDetails.Id))
                .ReturnsAsync(userDetails);

            mapperMock
                .Setup(m => m.Map<UserDetailModel>(userDetails))
                .Returns(expectedResult);

            //Act
            var result = await sut.GetById(userDetails.Id);

            //Assert
            result.Should().BeEquivalentTo(expectedResult);
        }



        [Fact]
        public async void When_Create_IsCalled_Expect_CreateAndSaveChangesToBeInvoked_And_UsersDetailsToBeCreated()
        {
            //Arrange
            var userDetails = new UserDetail(Guid.NewGuid(), "Firstname", "Lastname", "Number", "Email");
            var model = new CreateUserDetailModel();

            var expectedResult = new UserDetailModel()
            {
                Id = userDetails.Id,
                Firstname = userDetails.Firstname,
                Lastname = userDetails.Lastname,
                Number = userDetails.Number,
                Email = userDetails.Email
            };

            mapperMock
                .Setup(u => u.Map<UserDetail>(model))
                .Returns(userDetails);

            usersDetailsRepositoryMock
                .Setup(u => u.Create(userDetails))
                .Returns(Task.CompletedTask);

            usersDetailsRepositoryMock
                .Setup(u => u.SaveChanges())
                .Returns(Task.CompletedTask);

            mapperMock
                .Setup(m => m.Map<UserDetailModel>(userDetails))
                .Returns(expectedResult);

            //Act
            var result = await sut.Create(model);

            //Assert
            result.Should().BeEquivalentTo(expectedResult);
        }

        [Fact]
        public void When_GetAll_IsCalled_Expect_GetAllToBeInvoked_And_AllUsersDetailsToBeReturned()
        {
            //Arrange
            var users = new List<UserDetail>();
            users.Add(new UserDetail(Guid.NewGuid(), "Firstname", "Lastname", "Number", "Email"));
            users.Add(new UserDetail(Guid.NewGuid(), "Firstname1", "Lastname1", "Number1", "Email1"));

            var expectedResult = users.Select(u => new UserDetailModel()
            {
                Id = u.Id,
                Firstname = u.Firstname,
                Lastname = u.Lastname,
                Number = u.Number,
                Email = u.Email
            });

            usersDetailsRepositoryMock
                .Setup(u => u.GetAll())
                .Returns(users);

            mapperMock
                .Setup(m => m.Map<IEnumerable<UserDetailModel>>(users))
                .Returns(expectedResult);

            //Act
            var result = sut.GetAll();

            //Assert
            result.Should().BeEquivalentTo(expectedResult);
        }

        [Fact]
        public async void When_Delete_IsCalled_Expect_GetUserDetailByIdAndDeleteAndSaveChangesToBeInvoked()
        {
            //Arrange
            var userDetails = new UserDetail(Guid.NewGuid(), "Firstname", "Lastname", "Number", "Email");

            usersDetailsRepositoryMock
                .Setup(u => u.GetUserDetailById(userDetails.Id))
                .ReturnsAsync(userDetails);

            usersDetailsRepositoryMock
                .Setup(u => u.Delete(userDetails));

            usersDetailsRepositoryMock
                .Setup(u => u.SaveChanges())
                .Returns(Task.CompletedTask);

            //Act
            await sut.Delete(userDetails.Id);

            //Assert
        }

        [Fact]
        public async void When_Update_IsCalled_Expect_GetUserDetailByIdAndUpdateAndSaveChangesToBeInvoked()
        {
            //Arrange
            var userDetails = new UserDetail(Guid.NewGuid(), "Firstname", "Lastname", "Number", "Email");
            var expectedResult = new CreateUserDetailModel()
            {
                Firstname = userDetails.Firstname,
                Lastname = userDetails.Lastname,
                Number = userDetails.Number,
                Email = userDetails.Email
            };

            usersDetailsRepositoryMock
                .Setup(u => u.GetUserDetailById(userDetails.Id))
                .ReturnsAsync(userDetails);

            mapperMock
                .Setup(mapper => mapper.Map(expectedResult, userDetails))
                .Returns(userDetails);

            usersDetailsRepositoryMock
                .Setup(u => u.Update(userDetails));

            usersDetailsRepositoryMock
                .Setup(u => u.SaveChanges())
                .Returns(Task.CompletedTask);

            //Act
            await sut.Update(userDetails.Id, expectedResult);

            //Assert
        }
    }
}
