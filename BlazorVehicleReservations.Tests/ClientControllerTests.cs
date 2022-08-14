using BlazorVehicleReservations.API.Controllers;
using BlazorVehicleReservations.API.Service.Interface;
using BlazorVehicleReservations.Shared.Models.Dto;
using BlazorVehicleReservations.Shared.Models.Search;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BlazorVehicleReservations.Tests
{
    public class ClientControllerTests
    {
        [Fact]
        public async Task CreateClient_Returns_One_Inserted_Row()
        {
            //Arrange
            var fakeLogger = A.Fake<ILogger<ClientController>>();
            var dummyClient = A.Dummy<ClientDto>();
            var fakeClientService = A.Fake<IClientService>();
            A.CallTo(() => fakeClientService.CreateClient(dummyClient)).Returns(Task.FromResult(1));
            var controller = new ClientController(fakeClientService, fakeLogger);

            //Act
            var actionResult = await controller.CreateClient(dummyClient);

            //Assert
            var result = Assert.IsType<CreatedResult>(actionResult);
            Assert.Equal(1, result.Value);
        }

        [Fact]
        public async Task CreateController_Returns_Zero_Inserted_Rows()
        {
            //Arrange
            var fakeLogger = A.Fake<ILogger<ClientController>>();
            var dummyClient = A.Dummy<ClientDto>();
            var fakeClientService = A.Fake<IClientService>();
            A.CallTo(() => fakeClientService.CreateClient(dummyClient)).Returns(Task.FromResult(0));
            var controller = new ClientController(fakeClientService, fakeLogger);

            //Act
            var actionResult = await controller.CreateClient(dummyClient);

            //Assert
            var result = Assert.IsType<BadRequestResult>(actionResult);
            Assert.Equal(400, result.StatusCode);
        }

        [Fact]
        public async Task CreateClient_Throws_Server_Error()
        {
            //Arrange
            var fakeLogger = A.Fake<ILogger<ClientController>>();
            var dummyClient = A.Dummy<ClientDto>();
            var fakeClientService = A.Fake<IClientService>();
            A.CallTo(() => fakeClientService.CreateClient(dummyClient)).Throws(new Exception());
            var controller = new ClientController(fakeClientService, fakeLogger);

            //Act
            var actionResult = await controller.CreateClient(dummyClient);

            //Assert
            var result = Assert.IsType<StatusCodeResult>(actionResult);
            Assert.Equal(500, result.StatusCode);
        }

        [Fact]
        public async Task GetAllClients_Returns_Populated_List()
        {
            //Arrange
            var fakeLogger = A.Fake<ILogger<ClientController>>();
            var fakeDummyList = A.CollectionOfFake<ClientDto>(3).ToList();
            var fakeClientService = A.Fake<IClientService>();
            A.CallTo(() => fakeClientService.GetAllClients()).Returns(Task.FromResult(fakeDummyList));
            var controller = new ClientController(fakeClientService, fakeLogger);

            //Act
            var actionResult = await controller.GetAllClients();

            //Assert
            var result = Assert.IsType<OkObjectResult>(actionResult);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async Task GetAllClients_Returns_Zero_Rows()
        {
            //Arrange
            var fakeLogger = A.Fake<ILogger<ClientController>>();
            var fakeDummyList = A.CollectionOfFake<ClientDto>(0).ToList();
            var fakeClientService = A.Fake<IClientService>();
            A.CallTo(() => fakeClientService.GetAllClients()).Returns(Task.FromResult(fakeDummyList));
            var controller = new ClientController(fakeClientService, fakeLogger);

            //Act
            var actionResult = await controller.GetAllClients();

            //Assert
            var result = Assert.IsType<NotFoundResult>(actionResult);
            Assert.Equal(404, result.StatusCode);
        }

        [Fact]
        public async Task GetAllClients_Throws_Exception()
        {
            //Arrange
            var fakeLogger = A.Fake<ILogger<ClientController>>();
            var fakeClientService = A.Fake<IClientService>();
            A.CallTo(() => fakeClientService.GetAllClients()).Throws(new Exception());
            var controller = new ClientController(fakeClientService, fakeLogger);

            //Act
            var actionResult = await controller.GetAllClients();

            //Assert
            var result = Assert.IsType<StatusCodeResult>(actionResult);
            Assert.Equal(500, result.StatusCode);
        }

        [Fact]
        public async Task GetClient_Returns_One()
        {
            //Arrange
            var fakeLogger = A.Fake<ILogger<ClientController>>();
            var fakeClientService = A.Fake<IClientService>();
            var fakeDummy = A.Fake<ClientDto>();
            A.CallTo(() => fakeClientService.GetClient(1)).Returns(Task.FromResult(fakeDummy));
            var controller = new ClientController(fakeClientService, fakeLogger);

            //Act
            var actionResult = await controller.GetClient(1);

            //Assert
            var result = Assert.IsType<OkObjectResult>(actionResult);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async Task GetClient_Returns_Zero()
        {
            //Arrange
            var fakeLogger = A.Fake<ILogger<ClientController>>();
            var fakeClientService = A.Fake<IClientService>();
            var fakeDummy = A.Fake<ClientDto>();
            A.CallTo(() => fakeClientService.GetClient(0)).Returns(Task.FromResult(fakeDummy));
            var controller = new ClientController(fakeClientService, fakeLogger);

            //Act
            var actionResult = await controller.GetClient(0);

            //Assert
            var result = Assert.IsType<NotFoundResult>(actionResult);
            Assert.Equal(404, result.StatusCode);
        }

        [Fact]
        public async Task GetClient_Throws_Exception()
        {
            //Arrange
            var fakeLogger = A.Fake<ILogger<ClientController>>();
            var fakeClientService = A.Fake<IClientService>();
            A.CallTo(() => fakeClientService.GetClient('a')).Throws(new Exception());
            var controller = new ClientController(fakeClientService, fakeLogger);

            //Act
            var actionResult = await controller.GetClient('a');

            //Assert
            var result = Assert.IsType<StatusCodeResult>(actionResult);
            Assert.Equal(500, result.StatusCode);
        }

        [Fact]
        public async Task DeleteClient_Returns_One_Row_Deleted()
        {
            //Arrange
            var fakeLogger = A.Fake<ILogger<ClientController>>();
            var fakeClientService = A.Fake<IClientService>();
            A.CallTo(() => fakeClientService.DeleteClient(1)).Returns(Task.FromResult(1));
            var controller = new ClientController(fakeClientService, fakeLogger);

            //Act
            var actionResult = await controller.DeleteClient(1);

            //Assert
            var result = Assert.IsType<NoContentResult>(actionResult);
            Assert.Equal(204, result.StatusCode);
        }

        [Fact]
        public async Task DeleteClient_Returns_No_Rows_Deleted()
        {
            //Arrange
            var fakeLogger = A.Fake<ILogger<ClientController>>();
            var fakeClientService = A.Fake<IClientService>();
            A.CallTo(() => fakeClientService.DeleteClient(0)).Returns(Task.FromResult(0));
            var controller = new ClientController(fakeClientService, fakeLogger);

            //Act
            var actionResult = await controller.DeleteClient(0);

            //Assert
            var result = Assert.IsType<BadRequestResult>(actionResult);
            Assert.Equal(400, result.StatusCode);
        }

        [Fact]
        public async Task DeleteClient_Throws_Exception()
        {
            //Arrange
            var fakeLogger = A.Fake<ILogger<ClientController>>();
            var fakeClientService = A.Fake<IClientService>();
            A.CallTo(() => fakeClientService.DeleteClient('a')).Throws(new Exception());
            var controller = new ClientController(fakeClientService, fakeLogger);

            //Act
            var actionResult = await controller.DeleteClient('a');

            //Assert
            var result = Assert.IsType<StatusCodeResult>(actionResult);
            Assert.Equal(500, result.StatusCode);
        }

        [Fact]
        public async Task UpdateClient_Returns_Row_Updated()
        {
            //Arrange
            var fakeLogger = A.Fake<ILogger<ClientController>>();
            var fakeClientService = A.Fake<IClientService>();
            var fakeClientDto = A.Fake<ClientDto>();
            fakeClientDto.ClientId = 1;
            A.CallTo(() => fakeClientService.UpdateClient(fakeClientDto)).Returns(Task.FromResult(1));
            var controller = new ClientController(fakeClientService, fakeLogger);

            //Act
            var actionResult = await controller.UpdateClient(fakeClientDto, 1);

            //Assert
            var result = Assert.IsType<OkResult>(actionResult);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async Task UpdateClient_Returns_Zero_Rows_Updated()
        {
            //Arrange
            var fakeLogger = A.Fake<ILogger<ClientController>>();
            var fakeClientService = A.Fake<IClientService>();
            var fakeClientDto = A.Fake<ClientDto>();
            fakeClientDto.ClientId = 0;
            A.CallTo(() => fakeClientService.UpdateClient(fakeClientDto)).Returns(Task.FromResult(0));
            var controller = new ClientController(fakeClientService, fakeLogger);

            //Act
            var actionResult = await controller.UpdateClient(fakeClientDto, 0);

            //Assert
            var result = Assert.IsType<NoContentResult>(actionResult);
            Assert.Equal(204, result.StatusCode);
        }

        [Fact]
        public async Task UpdateClient_Returns_Id_Not_Found()
        {
            //Arrange
            var fakeLogger = A.Fake<ILogger<ClientController>>();
            var fakeClientService = A.Fake<IClientService>();
            var fakeClientDto = A.Fake<ClientDto>();
            fakeClientDto.ClientId = 1;
            A.CallTo(() => fakeClientService.UpdateClient(fakeClientDto)).Returns(Task.FromResult(0));
            var controller = new ClientController(fakeClientService, fakeLogger);

            //Act
            var actionResult = await controller.UpdateClient(fakeClientDto, 0);

            //Assert
            var result = Assert.IsType<NotFoundResult>(actionResult);
            Assert.Equal(404, result.StatusCode);
        }

        [Fact]
        public async Task UpdateClient_Throws_Exception()
        {
            //Arrange
            var fakeLogger = A.Fake<ILogger<ClientController>>();
            var fakeClientService = A.Fake<IClientService>();
            var fakeClientDto = A.Fake<ClientDto>();
            fakeClientDto.ClientId = 'a';
            A.CallTo(() => fakeClientService.UpdateClient(fakeClientDto)).Throws(new Exception());
            var controller = new ClientController(fakeClientService, fakeLogger);

            //Act
            var actionResult = await controller.UpdateClient(fakeClientDto, 'a');

            //Assert
            var result = Assert.IsType<StatusCodeResult>(actionResult);
            Assert.Equal(500, result.StatusCode);
        }

        [Fact]
        public async Task SearchClient_Returns_Populated_List()
        {
            //Arrange
            var fakeLogger = A.Fake<ILogger<ClientController>>();
            var fakeClientService = A.Fake<IClientService>();
            var fakeDummySearch = A.Fake<ClientSearch>();
            var fakeDummyList = A.CollectionOfFake<ClientDto>(3).AsEnumerable();
            A.CallTo(() => fakeClientService.SearchClient(fakeDummySearch)).Returns(Task.FromResult(fakeDummyList));
            var controller = new ClientController(fakeClientService, fakeLogger);

            //Act
            var actionResult = await controller.SearchClient(fakeDummySearch);

            //Assert
            var result = Assert.IsType<OkObjectResult>(actionResult);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async Task SearchClient_Returns_Empty_List()
        {
            //Arrange
            var fakeLogger = A.Fake<ILogger<ClientController>>();
            var fakeClientService = A.Fake<IClientService>();
            var fakeDummySearch = A.Fake<ClientSearch>();
            var fakeDummyList = A.CollectionOfFake<ClientDto>(0).AsEnumerable();
            A.CallTo(() => fakeClientService.SearchClient(fakeDummySearch)).Returns(Task.FromResult(fakeDummyList));
            var controller = new ClientController(fakeClientService, fakeLogger);

            //Act
            var actionResult = await controller.SearchClient(fakeDummySearch);

            //Assert
            var result = Assert.IsType<NotFoundResult>(actionResult);
            Assert.Equal(404, result.StatusCode);
        }

        [Fact]
        public async Task SearchClient_Throws_Exception()
        {
            //Arrange
            var fakeLogger = A.Fake<ILogger<ClientController>>();
            var fakeClientService = A.Fake<IClientService>();
            var fakeDummySearch = A.Fake<ClientSearch>();
            A.CallTo(() => fakeClientService.SearchClient(fakeDummySearch)).Throws(new Exception());
            var controller = new ClientController(fakeClientService, fakeLogger);

            //Act
            var actionResult = await controller.SearchClient(fakeDummySearch);

            //Assert
            var result = Assert.IsType<StatusCodeResult>(actionResult);
            Assert.Equal(500, result.StatusCode);
        }
    }
}
