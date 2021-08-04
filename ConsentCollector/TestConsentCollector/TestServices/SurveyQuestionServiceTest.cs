using AutoMapper;
using ConsentCollector.Business.Consent.Models.SurveyQuestionModel;
using ConsentCollector.Business.Consent.Services.SurveyQuestionService;
using ConsentCollector.Entities.Consent;
using ConsentCollector.Persistence.SurveyQuestionRepository;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace TestConsentCollector.TestServices
{
    public class SurveyQuestionServiceTest
    {
        private readonly MockRepository mockRepository;
        private readonly Mock<ISurveyQuestionRepository> surveyQuestionRepositoryMock;
        private readonly Mock<IMapper> mapperMock;

        private readonly SurveyQuestionService sut;

        public SurveyQuestionServiceTest()
        {
            mockRepository = new MockRepository(MockBehavior.Strict);
            surveyQuestionRepositoryMock = mockRepository.Create<ISurveyQuestionRepository>();
            mapperMock = mockRepository.Create<IMapper>();

            sut = new SurveyQuestionService(surveyQuestionRepositoryMock.Object, mapperMock.Object);
        }
        public void Dispose()
        {
            mockRepository.VerifyAll();
        }

        [Fact]
        public void When_GetAll_IsCalled_Expect_GetAllToBeInvoked_And_MappedResponseToBeReturned()
        {
            //Arrange
            var surveysQuestions = new List<SurveyQuestion>();
            surveysQuestions.Add(new SurveyQuestion(Guid.NewGuid(), Guid.NewGuid()));
            surveysQuestions.Add(new SurveyQuestion(Guid.NewGuid(), Guid.NewGuid()));

            var expectedResult = surveysQuestions.Select(s => new SurveyQuestionModel()
            {
                Id = s.Id,
                IdQuestion = s.IdQuestion,
                IdSurvey = s.IdSurvey
            });

            surveyQuestionRepositoryMock
                .Setup(repository => repository.GetAll())
                .Returns(surveysQuestions);

            mapperMock
                .Setup(mapper => mapper.Map<IEnumerable<SurveyQuestionModel>>(surveysQuestions))
                .Returns(expectedResult);
            //Act
            var result = sut.GetAll();

            //Assert
            result.Should().BeEquivalentTo(expectedResult);
        }

        [Fact]
        public async void When_GetById_IsCalled_Expect_GetSurveyQuestionByIdFromRepositoryToBeInvoked_And_MappedResponseToBeReturned()
        {
            //Arrange
            var surveyQuestion = new SurveyQuestion(Guid.NewGuid(), Guid.NewGuid());
            var expectedResult = new SurveyQuestionModel()
            {
                Id = surveyQuestion.Id,
                IdQuestion = surveyQuestion.IdQuestion,
                IdSurvey = surveyQuestion.IdSurvey
            };

            surveyQuestionRepositoryMock
                .Setup(s => s.GetSurveyQuestionById(surveyQuestion.Id))
                .ReturnsAsync(surveyQuestion);

            mapperMock
                .Setup(m => m.Map<SurveyQuestionModel>(surveyQuestion))
                .Returns(expectedResult);

            //Act
            var result = await sut.GetById(surveyQuestion.Id);

            //Assert
            result.Should().BeEquivalentTo(expectedResult);
        }

        [Fact]
        public async void When_Create_IsCalled_Expect_CreateAndSaveChangesFromRepositoryToBeInvoked_And_MappedResponseToBeReturned()
        {
            //Arrange
            var surveyQuestion = new SurveyQuestion(Guid.NewGuid(), Guid.NewGuid());
            var expectedResult = new SurveyQuestionModel()
            {
                Id = surveyQuestion.Id,
                IdQuestion = surveyQuestion.IdQuestion,
                IdSurvey = surveyQuestion.IdSurvey
                
            };

            var initialModel = new CreateSurveyQuestionModel()
            {
                IdQuestion = surveyQuestion.IdQuestion,
                IdSurvey = surveyQuestion.IdSurvey
            };

            mapperMock
                .Setup(m => m.Map<SurveyQuestion>(initialModel))
                .Returns(surveyQuestion);

            surveyQuestionRepositoryMock
                .Setup(n => n.Create(surveyQuestion))
                .Returns(Task.CompletedTask);


            surveyQuestionRepositoryMock
                .Setup(n => n.SaveChanges())
                .Returns(Task.CompletedTask);

            mapperMock
                .Setup(m => m.Map<SurveyQuestionModel>(surveyQuestion))
                .Returns(expectedResult);

            //Act
            var result = await sut.Create(initialModel);

            //Assert
            result.Should().BeEquivalentTo(expectedResult);
        }

        [Fact]
        public async void When_Delete_IsCalled_Expect_GetSurveyQuestionByIdAndDeleteAndSaveChangesFromRepositoryToBeInvoked()
        {
            //Arrange
            var surveyQuestion = new SurveyQuestion(Guid.NewGuid(), Guid.NewGuid());

            surveyQuestionRepositoryMock
                .Setup(s => s.GetSurveyQuestionById(surveyQuestion.Id))
                .ReturnsAsync(surveyQuestion);

            surveyQuestionRepositoryMock
                .Setup(n => n.Delete(surveyQuestion));


            surveyQuestionRepositoryMock
                .Setup(n => n.SaveChanges())
                .Returns(Task.CompletedTask);

            //Act
            await sut.Delete(surveyQuestion.Id);

            //Assert
            //result.Should().BeEquivalentTo(expectedResult);
        }

        [Fact]
        public async void When_DeleteBySurvey_IsCalled_Expect_DeleteBySurveyIdAndSaveChangesFromRepositoryToBeInvoked()
        {
            //Arrange
            var surveyQuestion = new SurveyQuestion(Guid.NewGuid(), Guid.NewGuid());

            surveyQuestionRepositoryMock
                .Setup(s => s.DeleteBySurveyId(surveyQuestion.IdSurvey));

            surveyQuestionRepositoryMock
                .Setup(n => n.SaveChanges())
                .Returns(Task.CompletedTask);

            //Act
            await sut.DeleteBySurvey(surveyQuestion.IdSurvey);

            //Assert
            //result.Should().BeEquivalentTo(expectedResult);
        }

        [Fact]
        public async void When_Update_IsCalled_Expect_GetSurveyQuestionByIdAndUpdateAndSaveChangesFromRepositoryToBeInvoked()
        {
            //Arrange
            var surveyQuestion = new SurveyQuestion(Guid.NewGuid(), Guid.NewGuid());
            var expectedResult = new SurveyQuestionModel()
            {
                Id = surveyQuestion.Id,
                IdQuestion = surveyQuestion.IdQuestion,
                IdSurvey = surveyQuestion.IdSurvey
            };

            var initialModel = new CreateSurveyQuestionModel()
            {
                IdQuestion = surveyQuestion.IdQuestion,
                IdSurvey = surveyQuestion.IdSurvey
            };

            surveyQuestionRepositoryMock
                .Setup(s => s.GetSurveyQuestionById(surveyQuestion.Id))
                .ReturnsAsync(surveyQuestion);

            mapperMock
                .Setup(mapper => mapper.Map(initialModel, surveyQuestion))
                .Returns(surveyQuestion);


            surveyQuestionRepositoryMock
                .Setup(n => n.Update(surveyQuestion));


            surveyQuestionRepositoryMock
                .Setup(n => n.SaveChanges())
                .Returns(Task.CompletedTask);

            //Act
            await sut.Update(surveyQuestion.Id, initialModel);

            //Assert
            //result.Should().BeEquivalentTo(expectedResult);
        }

        [Fact]
        public async void When_GetBySurveyId_IsCalled_Expect_GetSurveyQuestionBySurveyIdFromRepositoryToBeInvoked_And_MappedResponseToBeReturned()
        {
            //Arrange
            var SurveyId = Guid.NewGuid();
            var surveyQuestion = new List<SurveyQuestion>();
            surveyQuestion.Add(new SurveyQuestion(Guid.NewGuid(), Guid.NewGuid()));
            surveyQuestion.Add(new SurveyQuestion(Guid.NewGuid(), Guid.NewGuid()));

            surveyQuestion.ForEach(surveyQuestion =>
            {
                surveyQuestion.IdSurvey = SurveyId;
            });

            var expectedResult = surveyQuestion.Select(a => new SurveyQuestionModel()
            {
                Id = a.Id,
                IdSurvey = a.IdSurvey,
                IdQuestion = a.IdQuestion,
            });

            surveyQuestionRepositoryMock
                .Setup(a => a.GetSurveyQuestionBySurveyId(SurveyId))
                .Returns(surveyQuestion);

            mapperMock
                .Setup(m => m.Map<IEnumerable<SurveyQuestionModel>>(surveyQuestion))
                .Returns(expectedResult);
            //Act
            var result = sut.GetBySurveyId(SurveyId);

            //Assert
            result.Should().BeEquivalentTo(expectedResult);
        }
    }
}
