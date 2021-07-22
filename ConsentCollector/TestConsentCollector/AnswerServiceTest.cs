using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ConsentCollector.Business.Consent.Models;
using ConsentCollector.Business.Consent.Services;
using ConsentCollector.Entities.Consent;
using ConsentCollector.Persistence;
using FluentAssertions;
using Moq;
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
        public async void When_GetById_IsCalled_Expect_AnswerToBeReturned()
        {
            //Arrange
            var answer = new Answer(false, DateTime.Now);

            var expectedResult = new AnswerModel()
            {
                Id = answer.Id,
                Agree = answer.Agree,
                AnswerDate = answer.AnswerDate
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
        public void When_GetAll_IsCalled_Expect_AllAnswersToBeReturned()
        {
            //Arrange
            var answers = new List<Answer>();
            answers.Add(new Answer(true,DateTime.Today));
            answers.Add(new Answer(false,DateTime.Now));

            var expectedResult = answers.Select(a =>new AnswerModel()
            {
                Id = a.Id,
                Agree = a.Agree,
                AnswerDate = a.AnswerDate
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
        public async void When_Create_IsCalled_Expect_AnswerToBeCreated()
        {
            //Arrange
            var answer = new Answer(false, DateTime.Now);
            var model = new CreateAnswerModel();

            var expectedResult = new AnswerModel()
            {
                Id = answer.Id,
                Agree = answer.Agree,
                AnswerDate = answer.AnswerDate
            };

            _mapperMock
                .Setup(a => a.Map<Answer>(model))
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
            var result = await sut.Create(model);

            //Assert
            result.Should().BeEquivalentTo(expectedResult);

        }
    }

}

