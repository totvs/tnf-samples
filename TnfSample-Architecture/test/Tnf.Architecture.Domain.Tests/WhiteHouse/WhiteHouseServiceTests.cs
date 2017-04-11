using NSubstitute;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tnf.Architecture.Domain.Interfaces.Repositories;
using Tnf.Architecture.Domain.WhiteHouse;
using Tnf.Architecture.Dto;
using Xunit;
using System.Linq;
using Tnf.Architecture.Domain.Events;
using Tnf.Tests;

namespace Tnf.Architecture.Domain.Tests.WhiteHouse
{
    public class WhiteHouseServiceTests : TestBaseWithLocalIocManager
    {
        [Fact]
        public async Task WhiteHouse_Service_Return_All_Values()
        {
            // Arrange
            var requestDto = new GellAllPresidentsRequestDto(0, 2);

            var repository = Substitute.For<IWhiteHouseRepository>();

            var result = new PagingDtoResponse<PresidentDto>(new List<PresidentDto>()
            {
                new PresidentDto("1", "George Washington", "12345678"),
                new PresidentDto("2", "Bill Clinton", "87654321")
            });

            repository.GetAllPresidents(requestDto).Returns(
                Task.FromResult(result));

            var service = new WhiteHouseService(repository, EventBus);

            // Act
            var allPresidents = await service.GetAllPresidents(requestDto);

            // Assert
            Assert.True(allPresidents.Success);
            Assert.False(allPresidents.Notifications.Any());
            Assert.True(allPresidents.Data.Count == 2);
        }

        [Fact]
        public async Task WhiteHouse_Service_Return_PresidentById()
        {
            // Arrange
            var requestDto = new GellAllPresidentsRequestDto(0, 2);

            var repository = Substitute.For<IWhiteHouseRepository>();

            var result = new PresidentDto("1", "George Washington", "12345678");

            repository.GetPresidentById("1").Returns(Task.FromResult(result));

            var service = new WhiteHouseService(repository, EventBus);

            // Act
            var president = await service.GetPresidentById("1");

            // Assert
            Assert.True(president.Id == "1");
            Assert.True(president.Name == "George Washington");
        }

        [Fact]
        public async Task WhiteHouse_Service_Insert_Valid_Presidents()
        {
            // Arrange
            var requestDto = new GellAllPresidentsRequestDto(0, 2);

            var repository = Substitute.For<IWhiteHouseRepository>();

            var parameter = new List<PresidentDto>()
            {
                new PresidentDto("1", "George Washington", "12345678"),
                new PresidentDto("2", "Bill Clinton", "87654321")
            };

            repository.InsertPresidentsAsync(parameter).Returns(Task.FromResult(parameter));

            EventBus.Register<PresidentCreatedEvent>(
                eventData =>
                {
                    var president = eventData.President;
                    Assert.True(president.Id == "1" || president.Id == "2");
                });

            var service = new WhiteHouseService(repository, EventBus);

            // Act
            var responseBase = await service.InsertPresidentAsync(parameter);

            // Assert
            Assert.True(responseBase.Success);
            Assert.False(responseBase.Notifications.Any());
            Assert.True(responseBase.Data.Count == 2);
        }

        [Fact]
        public async Task WhiteHouse_Service_Not_Accept_Invalid_Presidents()
        {
            // Arrange
            var requestDto = new GellAllPresidentsRequestDto(0, 2);

            var repository = Substitute.For<IWhiteHouseRepository>();

            var parameter = new List<PresidentDto>()
            {
                new PresidentDto("1", "George Washington", "178"),
                new PresidentDto("2", "Bill Clinton", "87654321")
            };

            repository.InsertPresidentsAsync(parameter).Returns(Task.FromResult(parameter));

            var service = new WhiteHouseService(repository, EventBus);

            // Act
            var responseBase = await service.InsertPresidentAsync(parameter);

            // Assert
            Assert.False(responseBase.Success);
            Assert.True(responseBase.Notifications.Any(a => a.Message == President.Error.PresidentZipCodeMustHaveValue.ToString()));
            Assert.Null(responseBase.Data);
        }
    }
}
