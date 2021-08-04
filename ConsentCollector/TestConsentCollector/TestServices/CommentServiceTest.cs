using AutoMapper;
using ConsentCollector.Business.Consent.Models.CommentModel;
using ConsentCollector.Business.Consent.Services.CommentService;
using ConsentCollector.Entities.Consent;
using ConsentCollector.Persistence.CommentRepository;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace TestConsentCollector
{
    public class CommentServiceTest : IDisposable
    {
        private readonly MockRepository _mockRepository;
        private readonly Mock<ICommentRepository> _commentRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;

        private readonly CommentService sut;
        public CommentServiceTest()
        {
            _mockRepository = new MockRepository(MockBehavior.Strict);
            _commentRepositoryMock = _mockRepository.Create<ICommentRepository>();
            _mapperMock = _mockRepository.Create<IMapper>();
            sut = new CommentService(_commentRepositoryMock.Object, _mapperMock.Object);
        }

        public void Dispose()
        {
            _mockRepository.VerifyAll();
        }

        [Fact]
        public async void When_GetById_IsCalled_Expect_GetCommentByIdFromRepositoryToBeInvoked_And_MappedResponseToBeReturned()
        {
            //Arrange
            var comment = new Comment(Guid.NewGuid(), Guid.NewGuid(), "Text");

            var expectedResult = new CommentModel()
            {
                Id = comment.Id,
                IdUser = comment.IdUser,
                IdSurvey = comment.IdSurvey,
                Text = comment.Text,

            };

            _commentRepositoryMock
                .Setup(c => c.GetCommentById(comment.Id))
                .ReturnsAsync(comment);

            _mapperMock
                .Setup(m => m.Map<CommentModel>(comment))
                .Returns(expectedResult);

            //Act
            var result = await sut.GetById(comment.Id);

            //Assert
            result.Should().BeEquivalentTo(expectedResult);
        }

        [Fact]
        public async void When_GetByUserAndSurveyId_IsCalled_Expect_GetAnswerByUserAndSurveyIdFromRepositoryToBeInvoked_And_MappedResponseToBeReturned()
        {
            //Arrange
            var comment = new Comment(Guid.NewGuid(), Guid.NewGuid(), "Text");

            var expectedResult = new CommentModel()
            {
                Id = comment.Id,
                IdUser = comment.IdUser,
                IdSurvey = comment.IdSurvey,
                Text = comment.Text,

            };

            _commentRepositoryMock
                .Setup(c => c.GetAnswerByUserAndSurveyId(comment.IdUser,comment.IdSurvey))
                .ReturnsAsync(comment);

            _mapperMock
                .Setup(m => m.Map<CommentModel>(comment))
                .Returns(expectedResult);

            //Act
            var result = await sut.GetByUserAndSurveyId(comment.IdUser, comment.IdSurvey);

            //Assert
            result.Should().BeEquivalentTo(expectedResult);
        }

        [Fact]
        public void When_GetAll_IsCalled_Expect_GetAllFromRepositoryToBeInvoked_And_MappedResponseToBeReturned()
        {
            //Arrange
            var comments = new List<Comment>();
            comments.Add(new Comment(Guid.NewGuid(), Guid.NewGuid(), "Text"));
            comments.Add(new Comment(Guid.NewGuid(), Guid.NewGuid(), "Text1"));

            var expectedResult = comments.Select(c => new CommentModel()
            {
                Id = c.Id,
                IdUser = c.IdUser,
                IdSurvey = c.IdSurvey,
                Text = c.Text
            });

            _commentRepositoryMock
                .Setup(c => c.GetAll())
                .Returns(comments);

            _mapperMock
                .Setup(m => m.Map<IEnumerable<CommentModel>>(comments))
                .Returns(expectedResult);
            //Act
            var result = sut.GetAll();

            //Assert
            result.Should().BeEquivalentTo(expectedResult);

        }


        [Fact]
        public async void When_Create_IsCalled_Expect_CreateAndSaveChangesFromRepositoryToBeInvoked_And_MappedResponseToBeReturned()
        {
            //Arrange
            var comment = new Comment(Guid.NewGuid(), Guid.NewGuid(), "Text");
            var initialModel = new CreateCommentModel()
            {
                IdUser = comment.IdUser,
                IdSurvey = comment.IdSurvey,
                Text = comment.Text
            };

            var expectedResult = new CommentModel()
            {
                Id = comment.Id,
                IdUser = comment.IdUser,
                IdSurvey = comment.IdSurvey,
                Text = comment.Text
            };

            _mapperMock
                .Setup(c => c.Map<Comment>(initialModel))
                .Returns(comment);

            _commentRepositoryMock
                .Setup(c => c.Create(comment))
                .Returns(Task.CompletedTask);

            _commentRepositoryMock
                .Setup(c => c.SaveChanges())
                .Returns(Task.CompletedTask);

            _mapperMock
                .Setup(m => m.Map<CommentModel>(comment))
                .Returns(expectedResult);
            //Act
            var result = await sut.Create(initialModel);

            //Assert
            result.Should().BeEquivalentTo(expectedResult);

        }

        [Fact]
        public async void When_Delete_IsCalled_Expect_GetCommentByIdAndDeleteAndSaveChangesFromRepositoryToBeInvoked()
        {
            //Arrange
            var comment = new Comment(Guid.NewGuid(), Guid.NewGuid(), "Text");

            _commentRepositoryMock
                .Setup(a => a.GetCommentById(comment.Id))
                .ReturnsAsync(comment);

            _commentRepositoryMock
                .Setup(a => a.Delete(comment));

            _commentRepositoryMock
                .Setup(a => a.SaveChanges())
                .Returns(Task.CompletedTask);
            //Act
            await sut.Delete(comment.Id);

            //Assert
            //result.Should().BeEquivalentTo(expectedResult);

        }

        [Fact]
        public async void When_Update_IsCalled_Expect_GetCommentByIdAndUpdateAndSaveChangesFromRepositoryToBeInvoked()
        {
            //Arrange
            var comment = new Comment(Guid.NewGuid(), Guid.NewGuid(), "Text");
            var initialModel = new CreateCommentModel()
            {
                IdUser = comment.IdUser,
                IdSurvey = comment.IdSurvey,
                Text = comment.Text
            };

            //var expectedResult = new CommentModel()
            //{
            //    Id = comment.Id,
            //    IdUser = comment.IdUser,
            //    IdSurvey = comment.IdSurvey,
            //    Text = comment.Text
            //};

            _commentRepositoryMock
                .Setup(a => a.GetCommentById(comment.Id))
                .ReturnsAsync(comment);

            _mapperMock
                .Setup(m => m.Map(initialModel, comment))
                .Returns(comment);

            _commentRepositoryMock
                .Setup(a => a.Update(comment));

            _commentRepositoryMock
                .Setup(a => a.SaveChanges())
                .Returns(Task.CompletedTask);


            //Act
            await sut.Update(comment.Id, initialModel);

            //Assert
            //result.Should().BeEquivalentTo(expectedResult);

        }
    }
}
