using AutoMapper;
using ConsentCollector.Business.Consent.Models;
using ConsentCollector.Business.Consent.Services;
using ConsentCollector.Entities.Consent;
using ConsentCollector.Persistence;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace TestConsentCollector
{
    public class NotificationServiceTest : IDisposable
    {
        private readonly MockRepository mockRepository;
        private readonly Mock<INotificationRepository> notificationRepositoryMock;
        private readonly Mock<IMapper> mapperMock;

        private readonly NotificationService sut;
        public NotificationServiceTest()
        {
            mockRepository = new MockRepository(MockBehavior.Strict);
            notificationRepositoryMock = mockRepository.Create<INotificationRepository>();
            mapperMock = mockRepository.Create<IMapper>();

            sut = new NotificationService(notificationRepositoryMock.Object, mapperMock.Object);
        }

        public void Dispose()
        {
            mockRepository.VerifyAll();
        }

        [Fact]

        public void When_GetByUserId_IsCalled_Expect_GetNotificationByUserIdToBeInvoked_And_MappedResponseToBeReturned()
        {
            //Arrange
            var IdUser = Guid.NewGuid();
            var notifications = new List<Notification>();
            notifications.Add(new Notification("Title", "Notificare noua", false));
            notifications.Add(new Notification("Title1", "Notificare noua1", true));

            var expectedResult = notifications.Select(n => new NotificationModel()
            {
                Id = n.Id,
                IdSurvey = n.IdSurvey,
                IdUser = n.IdUser,
                Description = n.Description,
                Seen = n.Seen,
                Title = n.Title
            });

            notifications.ForEach(notification =>
            {
                notification.IdUser = IdUser;
            });

            notificationRepositoryMock
                .Setup(repository => repository.GetNotificationByUserId(IdUser))
                .Returns(notifications);

            mapperMock
                .Setup(mapper => mapper.Map<IEnumerable<NotificationModel>>(notifications))
                .Returns(expectedResult);
            //Act
            var result = sut.GetByUserId(IdUser);
            
            //Assert
            result.Should().BeEquivalentTo(expectedResult);
        }

        [Fact]

        public void When_GetByUserIdAndSeen_IsCalled_Expect_GetNotificationByIdAndSeenToBeInvoked_And_MappedResponseToBeReturned()
        {
            //Arrange
            var IdUser = Guid.NewGuid();
            var notifications = new List<Notification>();
            notifications.Add(new Notification("Title", "Notificare noua", false));
            notifications.Add(new Notification("Title1", "Notificare noua1", true));

            var expectedResult = notifications.Select(n => new NotificationModel()
            {
                Id = n.Id,
                IdSurvey = n.IdSurvey,
                IdUser = n.IdUser,
                Description = n.Description,
                Seen = n.Seen,
                Title = n.Title
            });

            notifications.ForEach(notification =>
            {
                notification.IdUser = IdUser;
            });

            notificationRepositoryMock
                .Setup(repository => repository.GetNotificationByIdAndSeen(IdUser,true))
                .Returns(notifications);

            mapperMock
                .Setup(mapper => mapper.Map<IEnumerable<NotificationModel>>(notifications))
                .Returns(expectedResult);
            //Act
            var result = sut.GetByUserIdAndSeen(IdUser,true);

            //Assert
            result.Should().BeEquivalentTo(expectedResult);
        }

        [Fact]
        public void When_GetAll_IsCalled_Expect_GetAllToBeInvoked_And_MappedResponseToBeReturned()
        {
            //Arrange
            var notifications = new List<Notification>();
            notifications.Add(new Notification("Title", "Notificare noua", false));
            notifications.Add(new Notification("Title1", "Notificare noua1", true));

            var expectedResult = notifications.Select(n => new NotificationModel()
            {
                Id = n.Id,
                IdSurvey = n.IdSurvey,
                IdUser = n.IdUser,
                Description = n.Description,
                Seen = n.Seen,
                Title = n.Title
            });

            notificationRepositoryMock
                .Setup(repository => repository.GetAll())
                .Returns(notifications);

            mapperMock
                .Setup(mapper => mapper.Map<IEnumerable<NotificationModel>>(notifications))
                .Returns(expectedResult);
            //Act
            var result = sut.GetAll();

            //Assert
            result.Should().BeEquivalentTo(expectedResult);
        }

        [Fact]
        public async void When_GetById_IsCalled_Expect_GetNotificationByIdFromRepositoryToBeInvoked_And_MappedResponseToBeReturned()
        {
            //Arrange
            var notification = new Notification("Title", "Notificare noua", false);
            var expectedResult = new NotificationModel()
            {
                Id = notification.Id,
                Title = notification.Title,
                Description = notification.Description,
                Seen = notification.Seen,
                IdSurvey = notification.IdSurvey,
                IdUser = notification.IdUser
            };

            notificationRepositoryMock
                .Setup(s => s.GetNotificationById(notification.Id))
                .ReturnsAsync(notification);

            mapperMock
                .Setup(m => m.Map<NotificationModel>(notification))
                .Returns(expectedResult);

            //Act
            var result = await sut.GetById(notification.Id);

            //Assert
            result.Should().BeEquivalentTo(expectedResult);
        }

        [Fact]
        public async void When_Create_IsCalled_Expect_CreateAndSaveChangesFromRepositoryToBeInvoked_And_MappedResponseToBeReturned()
        {
            //Arrange
            var notification = new Notification("Title", "Notificare noua", false);
            var expectedResult = new CreateNotificationModel()
            {
                Title = notification.Title,
                Description = notification.Description,
                Seen = notification.Seen,
                IdSurvey = notification.IdSurvey,
                IdUser = notification.IdUser
            };

            var initialModel = new NotificationModel()
            {
                Id = notification.Id,
                Title = notification.Title,
                Description = notification.Description,
                Seen = notification.Seen,
                IdSurvey = notification.IdSurvey,
                IdUser = notification.IdUser
            };

            mapperMock
                .Setup(m => m.Map<Notification>(expectedResult))
                .Returns(notification);

            notificationRepositoryMock
                .Setup(n => n.Create(notification))
                .Returns(Task.CompletedTask);


            notificationRepositoryMock
                .Setup(n => n.SaveChanges())
                .Returns(Task.CompletedTask);

            mapperMock
                .Setup(m => m.Map<NotificationModel>(notification))
                .Returns(initialModel);

            //Act
            var result = await sut.Create(expectedResult);

            //Assert
            result.Should().BeEquivalentTo(expectedResult);
        }

        [Fact]
        public async void When_Delete_IsCalled_Expect_GetAnswerByIdAndDeleteAndSaveChangesFromRepositoryToBeInvoked()
        {
            //Arrange
            var notification = new Notification("Title", "Notificare noua", false);

            notificationRepositoryMock
                .Setup(s => s.GetNotificationById(notification.Id))
                .ReturnsAsync(notification);

            notificationRepositoryMock
                .Setup(n => n.Delete(notification));


            notificationRepositoryMock
                .Setup(n => n.SaveChanges())
                .Returns(Task.CompletedTask);

            //Act
            await sut.Delete(notification.Id);

            //Assert
            //result.Should().BeEquivalentTo(expectedResult);
        }

        [Fact]
        public async void When_Update_IsCalled_Expect_GetAnswerByIdAndUpdateAndSaveChangesFromRepositoryToBeInvoked()
        {
            //Arrange
            var notification = new Notification("Title", "Notificare noua", false);
            var expectedResult = new NotificationModel()
            {
                Id = notification.Id,
                Title = notification.Title,
                Description = notification.Description,
                Seen = notification.Seen,
                IdSurvey = notification.IdSurvey,
                IdUser = notification.IdUser
            };

            notificationRepositoryMock
                .Setup(s => s.GetNotificationById(notification.Id))
                .ReturnsAsync(notification);

            mapperMock
                .Setup(mapper => mapper.Map(expectedResult, notification))
                .Returns(notification);

            
            notificationRepositoryMock
                .Setup(n => n.Update(notification));


            notificationRepositoryMock
                .Setup(n => n.SaveChanges())
                .Returns(Task.CompletedTask);

            //Act
            await sut.Update(notification.Id,expectedResult);

            //Assert
            //result.Should().BeEquivalentTo(expectedResult);
        }
    }
}
