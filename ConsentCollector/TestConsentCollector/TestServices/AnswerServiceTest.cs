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
    public class AnswerServiceTest : IDisposable
    {
        private readonly MockRepository _mockRepository;
        private readonly Mock<IAnswerRepository> _answerRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;

        private readonly AnswerService sut;
        public AnswerServiceTest()
        {
             _mockRepository = new MockRepository(MockBehavior.Strict);
             _answerRepositoryMock = _mockRepository.Create<IAnswerRepository>();
             _mapperMock = _mockRepository.Create<IMapper>();
             sut = new AnswerService(_answerRepositoryMock.Object, _mapperMock.Object);
        }

        public void Dispose()
        {
            _mockRepository.VerifyAll();
        }

        [Fact]
        public async void When_GetById_IsCalled_Expect_GetAnswerByIdFromRepositoryToBeInvoked_And_MappedResponseToBeReturned()
        {
            //Arrange
            var answer = new Answer(false, DateTime.Now);

            var expectedResult = new AnswerModel()
            {
                Id = answer.Id,
                Agree = answer.Agree,
                AnswerDate = answer.AnswerDate,
                IdSurvey = answer.IdSurvey,
                IdQuestion = answer.IdQuestion,
                IdUser = answer.IdUser
            };

            _answerRepositoryMock
                .Setup(a => a.GetAnswerById(answer.Id))
                .ReturnsAsync(answer);

            _mapperMock
                .Setup(m => m.Map<AnswerModel>(answer))
                .Returns(expectedResult);

            //Act
            var result = await sut.GetById(answer.Id);

            //Assert
            result.Should().BeEquivalentTo(expectedResult);
        }

        [Fact]

        public void When_GetByUserAndSurveyId_IsCalled_Expect_GetAnswerByUserAndSurveyIdToBeInvoked_And_MappedResponseToBeReturned()
        {
            //Arrange
            var IdUser = Guid.NewGuid();
            var IdSurvey = Guid.NewGuid();
            var answers = new List<Answer>();
            answers.Add(new Answer(true, DateTime.Today));
            answers.Add(new Answer(false, DateTime.Now));

            answers.ForEach(answer =>
            {
                answer.IdUser = IdUser;
                answer.IdSurvey = IdSurvey;
            });

            var expectedResult = answers.Select(a => new AnswerModel()
            {
                Id = a.Id,
                Agree = a.Agree,
                AnswerDate = a.AnswerDate,
                IdSurvey = a.IdSurvey,
                IdQuestion = a.IdQuestion,
                IdUser = a.IdUser
            });
            
            _answerRepositoryMock
                .Setup(a => a.GetAnswerByUserAndSurveyId(IdUser,IdSurvey))
                .Returns(answers);

            _mapperMock
                .Setup(m => m.Map<IEnumerable<AnswerModel>>(answers))
                .Returns(expectedResult);
            //Act
            var result = sut.GetByUserAndSurveyId(IdUser, IdSurvey);

            //Assert
            result.Should().BeEquivalentTo(expectedResult);
        }

        [Fact]
        public void When_GetAll_IsCalled_Expect_GetAllFromRepositoryToBeInvoked_And_MappedResponseToBeReturned()
        {
            //Arrange
            var answers = new List<Answer>();
            answers.Add(new Answer(true,DateTime.Today));
            answers.Add(new Answer(false,DateTime.Now));

            var expectedResult = answers.Select(a =>new AnswerModel()
            {
                Id = a.Id,
                Agree = a.Agree,
                AnswerDate = a.AnswerDate,
                IdSurvey = a.IdSurvey,
                IdQuestion = a.IdQuestion,
                IdUser = a.IdUser
            });

            _answerRepositoryMock
                .Setup(a => a.GetAll())
                .Returns(answers);

            _mapperMock
                .Setup(m => m.Map<IEnumerable<AnswerModel>>(answers))
                .Returns(expectedResult);
            //Act
            var result =  sut.GetAll();

            //Assert
            result.Should().BeEquivalentTo(expectedResult);

        }


        [Fact]
        public async void When_Create_IsCalled_Expect_CreateAndSaveChangesFromRepositoryToBeInvoked_And_MappedResponseToBeReturned()
        {
            //Arrange
            var answer = new Answer(false, DateTime.Now);
            var initialModel = new CreateAnswerModel()
            {
                Agree = answer.Agree,
                AnswerDate = answer.AnswerDate,
                IdSurvey = answer.IdSurvey,
                IdQuestion = answer.IdQuestion,
                IdUser = answer.IdUser
            };

            var expectedResult = new AnswerModel()
            {
                Id = answer.Id,
                Agree = answer.Agree,
                AnswerDate = answer.AnswerDate,
                IdSurvey = answer.IdSurvey,
                IdQuestion = answer.IdQuestion,
                IdUser = answer.IdUser
            };

            _mapperMock
                .Setup(a => a.Map<Answer>(initialModel))
                .Returns(answer);

            _answerRepositoryMock
                .Setup(a => a.Create(answer))
                .Returns(Task.CompletedTask);

            _answerRepositoryMock
                .Setup(a => a.SaveChanges())
                .Returns(Task.CompletedTask);

            _mapperMock
                .Setup(m => m.Map<AnswerModel>(answer))
                .Returns(expectedResult);
            //Act
            var result = await sut.Create(initialModel);

            //Assert
            result.Should().BeEquivalentTo(expectedResult);

        }
        [Fact]
        public async void When_Delete_IsCalled_Expect_GetAnswerByIdAndDeleteAndSaveChangesFromRepositoryToBeInvoked()
        {
            //Arrange
            var answer = new Answer(false, DateTime.Now);

            _answerRepositoryMock
                .Setup(a => a.GetAnswerById(answer.Id))
                .ReturnsAsync(answer);

            _answerRepositoryMock
                .Setup(a => a.Delete(answer));

            _answerRepositoryMock
                .Setup(a => a.SaveChanges())
                .Returns(Task.CompletedTask);
            //Act
            await sut.Delete(answer.Id);

            //Assert
            //result.Should().BeEquivalentTo(expectedResult);

        }

        [Fact]
        public async void When_Update_IsCalled_Expect_GetAnswerByIdAndUpdateAndSaveChangesFromRepositoryToBeInvoked()
        {
            //Arrange
            var answer = new Answer(false, DateTime.Now);
            var initialModel = new CreateAnswerModel()
            {
                Agree = answer.Agree,
                AnswerDate = answer.AnswerDate,
                IdSurvey = answer.IdSurvey,
                IdQuestion = answer.IdQuestion,
                IdUser = answer.IdUser
            };

            //var expectedResult = new AnswerModel()
            //{
            //    Id = answer.Id,
            //    Agree = answer.Agree,
            //    AnswerDate = answer.AnswerDate,
            //    IdSurvey = answer.IdSurvey,
            //    IdQuestion = answer.IdQuestion,
            //    IdUser = answer.IdUser
            //};

            _answerRepositoryMock
                .Setup(a => a.GetAnswerById(answer.Id))
                .ReturnsAsync(answer);

            _mapperMock
                .Setup(m => m.Map(initialModel, answer))
                .Returns(answer);

            _answerRepositoryMock
                .Setup(a => a.Update(answer));

            _answerRepositoryMock
                .Setup(a => a.SaveChanges())
                .Returns(Task.CompletedTask);

            
            //Act
             await sut.Update(answer.Id, initialModel);

            //Assert
            //result.Should().BeEquivalentTo(expectedResult);

        }
    }

}

