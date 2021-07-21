using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
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
    public class UserServiceTest
    {
        [Fact]
        public async void When_Get_IsCalled_Expect_UsersToBeReturned()
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

            var mockRespository = new MockRepository(MockBehavior.Strict);
            var usersRepositoryMock = mockRespository.Create<IUserRepository>();
            var mapperMock = mockRespository.Create<IMapper>();

            usersRepositoryMock
                .Setup(u => u.GetUserById(user.Id))
                .ReturnsAsync(user);


            mapperMock
                .Setup(m => m.Map<UserModel>(user))
                .Returns(expectedResult);

            var sut = new UserService(usersRepositoryMock.Object, mapperMock.Object);

            //Act

            var result = await sut.GetById(user.Id);

            //Assert
            result.Should().BeEquivalentTo(expectedResult);

            mockRespository.VerifyAll();
        }
    }
}
