using AutoMapper;
using ConsentCollector.Business.Consent.Models.HistoryModel;
using ConsentCollector.Business.Consent.Services.HistoryService;
using ConsentCollector.Entities.Consent;
using ConsentCollector.Persistence.HistoryRepository;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace TestConsentCollector.TestServices
{
    public class HistoryServiceTest:IDisposable
    {
        private readonly MockRepository _mockRepository;
        private readonly Mock<IHistoryRepository> _historyRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;

        private readonly HistoryService sut;
        public HistoryServiceTest()
        {
            _mockRepository = new MockRepository(MockBehavior.Strict);
            _historyRepositoryMock = _mockRepository.Create<IHistoryRepository>();
            _mapperMock = _mockRepository.Create<IMapper>();
            sut = new HistoryService(_historyRepositoryMock.Object, _mapperMock.Object);
        }
        public void Dispose()
        {
            _mockRepository.VerifyAll();
        }

        [Fact]
        public async void When_Create_IsCalled_Expect_CreateAndSaveChangesFromRepositoryToBeInvoked_And_MappedResponseToBeReturned()
        {
            //Arrange
            var history = new History("test");
            var initialModel = new CreateHistoryModel()
            {
                Description = history.Description
            };

            var expectedResult = new HistoryModel()
            {
                Id = history.Id,
                Description = history.Description
            };

            _mapperMock
                .Setup(a => a.Map<History>(initialModel))
                .Returns(history);

            _historyRepositoryMock
                .Setup(a => a.Create(history))
                .Returns(Task.CompletedTask);

            _historyRepositoryMock
                .Setup(a => a.SaveChanges())
                .Returns(Task.CompletedTask);

            _mapperMock
                .Setup(m => m.Map<HistoryModel>(history))
                .Returns(expectedResult);
            //Act
            var result = await sut.Create(initialModel);

            //Assert
            result.Should().BeEquivalentTo(expectedResult);

        }

        [Fact]
        public void When_GetAll_IsCalled_Expect_GetAllFromRepositoryToBeInvoked_And_MappedResponseToBeReturned()
        {
            //Arrange
            var historys = new List<History>();
            historys.Add(new History("test"));
            historys.Add(new History("rest"));

            var expectedResult = historys.Select(a => new HistoryModel()
            {
                Id = a.Id,
                Description=a.Description
            });

            _historyRepositoryMock
                .Setup(a => a.GetAll())
                .Returns(historys);

            _mapperMock
                .Setup(m => m.Map<IEnumerable<HistoryModel>>(historys))
                .Returns(expectedResult);
            //Act
            var result = sut.GetAll();

            //Assert
            result.Should().BeEquivalentTo(expectedResult);

        }
    }
}
