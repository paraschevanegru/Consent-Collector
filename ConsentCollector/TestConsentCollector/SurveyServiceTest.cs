﻿using Moq;
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
using Xunit;

namespace TestConsentCollector
{
    public class SurveyServiceTest:IDisposable
    {
        private readonly MockRepository mockRepository;
        private readonly Mock<IConsentRepository> surveyRepositoryMock;
        private readonly Mock<IMapper> mapperMock;

        private readonly SurveyService sut;
        public SurveyServiceTest()
        {
            mockRepository = new MockRepository(MockBehavior.Strict);
            surveyRepositoryMock = mockRepository.Create<IConsentRepository>();
            mapperMock = mockRepository.Create<IMapper>();

            sut = new SurveyService(surveyRepositoryMock.Object, mapperMock.Object);
        }

        public void Dispose()
        {
            mockRepository.VerifyAll();
        }

        [Fact]
        public async void When_GetById_IsCalled_Expect_GetNotificationByIdFromRepositoryToBeInvoked_And_MappedResponseToBeReturned()
        {
            //Arrange
            var survey = new Survey("Party", "New Year Ave","Contract", new DateTime(2021, 4, 12), new DateTime(2021, 4, 15));
            var expectedResult = new SurveyModel()
            {
                Id = survey.Id,
                Subject = survey.Subject,
                LegalBasis = survey.LegalBasis,
                LaunchDate = survey.LaunchDate,
                ExpirationDate = survey.ExpirationDate
            };

            surveyRepositoryMock
                .Setup(s => s.GetSurveyById(survey.Id))
                .ReturnsAsync(survey);

            mapperMock
                .Setup(m => m.Map<SurveyModel>(survey))
                .Returns(expectedResult);

            //Act
            var result = await sut.GetById(survey.Id);

            //Assert
            result.Should().BeEquivalentTo(expectedResult);
        }

        [Fact]
        public async void When_Create_IsCalled_Expect_CreateAndSaveChangesFromRepositoryToBeInvoked_And_MappedResponseToBeReturned()
        {
            //Arrange
            var survey = new Survey("Party", "New Year Ave", "Contract", new DateTime(2021, 4, 12), new DateTime(2021, 4, 15));
            var expectedResult = new SurveyModel()
            {
                Id = survey.Id,
                Subject = survey.Subject,
                LegalBasis = survey.LegalBasis,
                LaunchDate = survey.LaunchDate,
                ExpirationDate = survey.ExpirationDate
            };

            var initialModel = new CreateSurveyModel()
            {
                Subject = survey.Subject,
                LegalBasis = survey.LegalBasis,
                LaunchDate = survey.LaunchDate,
                ExpirationDate = survey.ExpirationDate
            };

            mapperMock
                .Setup(m => m.Map<Survey>(initialModel))
                .Returns(survey);

            surveyRepositoryMock
                .Setup(n => n.Create(survey))
                .Returns(Task.CompletedTask);


            surveyRepositoryMock
                .Setup(n => n.SaveChanges())
                .Returns(Task.CompletedTask);

            mapperMock
                .Setup(m => m.Map<SurveyModel>(survey))
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
            var survey = new Survey("Party", "New Year Ave", "Contract", new DateTime(2021, 4, 12), new DateTime(2021, 4, 15));

            surveyRepositoryMock
                .Setup(s => s.GetSurveyById(survey.Id))
                .ReturnsAsync(survey);

            surveyRepositoryMock
                .Setup(n => n.Delete(survey));


            surveyRepositoryMock
                .Setup(n => n.SaveChanges())
                .Returns(Task.CompletedTask);

            //Act
            await sut.Delete(survey.Id);

            //Assert
            //result.Should().BeEquivalentTo(expectedResult);
        }

        [Fact]
        public async void When_Update_IsCalled_Expect_GetAnswerByIdAndUpdateAndSaveChangesFromRepositoryToBeInvoked()
        {
            //Arrange
            var survey = new Survey("Party", "New Year Ave", "Contract", new DateTime(2021, 4, 12), new DateTime(2021, 4, 15));
            var expectedResult = new SurveyModel()
            {
                Id = survey.Id,
                Subject = survey.Subject,
                LegalBasis = survey.LegalBasis,
                LaunchDate = survey.LaunchDate,
                ExpirationDate = survey.ExpirationDate
            };

            var initialModel = new CreateSurveyModel()
            {
                Subject = survey.Subject,
                LegalBasis = survey.LegalBasis,
                LaunchDate = survey.LaunchDate,
                ExpirationDate = survey.ExpirationDate
            };

            surveyRepositoryMock
                .Setup(s => s.GetSurveyById(survey.Id))
                .ReturnsAsync(survey);

            mapperMock
                .Setup(mapper => mapper.Map(initialModel, survey))
                .Returns(survey);


            surveyRepositoryMock
                .Setup(n => n.Update(survey));


            surveyRepositoryMock
                .Setup(n => n.SaveChanges())
                .Returns(Task.CompletedTask);

            //Act
            await sut.Update(survey.Id, initialModel);

            //Assert
            //result.Should().BeEquivalentTo(expectedResult);
        }
    }
}

