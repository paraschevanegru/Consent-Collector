using AutoMapper;
using ConsentCollector.Business.Consent.Models.QuestionModel;
using ConsentCollector.Business.Consent.Services.QuestionService;
using ConsentCollector.Entities.Consent;
using ConsentCollector.Persistence.QuestionRepository;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace TestConsentCollector
{
    public class QuestionServiceTest : IDisposable
    {
        private readonly MockRepository _mockRepository;
        private readonly Mock<IQuestionRepository> _questionRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;

        private readonly QuestionService sut;
        public QuestionServiceTest()
        {
            _mockRepository = new MockRepository(MockBehavior.Strict);
            _questionRepositoryMock = _mockRepository.Create<IQuestionRepository>();
            _mapperMock = _mockRepository.Create<IMapper>();
            sut = new QuestionService(_questionRepositoryMock.Object, _mapperMock.Object);
        }

        public void Dispose()
        {
            _mockRepository.VerifyAll();
        }

        [Fact]
        public async void When_GetById_IsCalled_Expect_GetQuestionByIdFromRepositoryToBeInvoked_And_MappedResponseToBeReturned()
        {
            //Arrange
            var question = new Question(false,"Question");

            var expectedResult = new QuestionModel()
            {
                Id = question.Id,
                Optional = question.Optional,
                Questions = question.Questions
            };

            _questionRepositoryMock
                .Setup(q => q.GetQuestionById(question.Id))
                .ReturnsAsync(question);

            _mapperMock
                .Setup(m => m.Map<QuestionModel>(question))
                .Returns(expectedResult);

            //Act
            var result = await sut.GetById(question.Id);

            //Assert
            result.Should().BeEquivalentTo(expectedResult);
        }

        [Fact]
        public void When_GetAll_IsCalled_Expect_GetAllFromRepositoryToBeInvoked_And_MappedResponseToBeReturned()
        {
            //Arrange
            var questions = new List<Question>();
            questions.Add(new Question(true,"Question"));
            questions.Add(new Question(false, "Question1"));

            var expectedResult = questions.Select(q => new QuestionModel()
            {
                Id = q.Id,
                Optional = q.Optional,
                Questions = q.Questions
            });

            _questionRepositoryMock
                .Setup(q => q.GetAll())
                .Returns(questions);

            _mapperMock
                .Setup(m => m.Map<IEnumerable<QuestionModel>>(questions))
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
            var question = new Question(false,"Question");
            var initialModel = new CreateQuestionModel()
            {
                Optional = question.Optional,
                Questions = question.Questions
            };

            var expectedResult = new QuestionModel()
            {
                Id = question.Id,
                Optional = question.Optional,
                Questions = question.Questions
            };

            _mapperMock
                .Setup(q => q.Map<Question>(initialModel))
                .Returns(question);

            _questionRepositoryMock
                .Setup(q => q.Create(question))
                .Returns(Task.CompletedTask);

            _questionRepositoryMock
                .Setup(q => q.SaveChanges())
                .Returns(Task.CompletedTask);

            _mapperMock
                .Setup(m => m.Map<QuestionModel>(question))
                .Returns(expectedResult);
            //Act
            var result = await sut.Create(initialModel);

            //Assert
            result.Should().BeEquivalentTo(expectedResult);

        }


        [Fact]
        public async void When_Delete_IsCalled_Expect_GetQuestionByIdAndDeleteAndSaveChangesFromRepositoryToBeInvoked()
        {
            //Arrange
            var question = new Question(false, "Question");

            _questionRepositoryMock
                .Setup(a => a.GetQuestionById(question.Id))
                .ReturnsAsync(question);

            _questionRepositoryMock
                .Setup(a => a.Delete(question));

            _questionRepositoryMock
                .Setup(a => a.SaveChanges())
                .Returns(Task.CompletedTask);
            //Act
            await sut.Delete(question.Id);

            //Assert
            //result.Should().BeEquivalentTo(expectedResult);

        }

        [Fact]
        public async void When_Update_IsCalled_Expect_GetQuestionByIdAndUpdateAndSaveChangesFromRepositoryToBeInvoked()
        {
            //Arrange
            var question = new Question(false, "Question");
            var initialModel = new CreateQuestionModel()
            {
                Optional = question.Optional,
                Questions = question.Questions
            };

            //var expectedResult = new QuestionModel()
            //{
            //    Id = question.Id,
            //    Optional = question.Optional,
            //    Questions = question.Questions
            //};

            _questionRepositoryMock
                .Setup(a => a.GetQuestionById(question.Id))
                .ReturnsAsync(question);

            _mapperMock
                .Setup(m => m.Map(initialModel, question))
                .Returns(question);

            _questionRepositoryMock
                .Setup(a => a.Update(question));

            _questionRepositoryMock
                .Setup(a => a.SaveChanges())
                .Returns(Task.CompletedTask);


            //Act
            await sut.Update(question.Id, initialModel);

            //Assert
            //result.Should().BeEquivalentTo(expectedResult);

        }
    }
}
