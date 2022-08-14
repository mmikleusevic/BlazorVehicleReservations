using BlazorVehicleReservations.API.Controllers;
using BlazorVehicleReservations.API.Service.Interface;
using BlazorVehicleReservations.Shared.Models.Dto;
using BlazorVehicleReservations.Shared.Models.Search;
using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BlazorVehicleReservations.Tests
{
    public class VehicleControllerTests
    {
        [Fact]
        public async Task CreateVehicle_Returns_One_Inserted_Row()
        {
            //Arrange
            var fakeLogger = A.Fake<ILogger<VehicleController>>();
            var dummyVehicle = A.Dummy<VehicleDto>();
            var fakeVehicleService = A.Fake<IVehicleService>();
            A.CallTo(() => fakeVehicleService.CreateVehicle(dummyVehicle)).Returns(Task.FromResult(1));
            var controller = new VehicleController(fakeVehicleService, fakeLogger);

            //Act
            var actionResult = await controller.CreateVehicle(dummyVehicle);

            //Assert
            var result = Assert.IsType<CreatedResult>(actionResult);
            Assert.Equal(1, result.Value);
        }

        [Fact]
        public async Task CreateVehicle_Returns_Zero_Inserted_Rows()
        {
            //Arrange
            var fakeLogger = A.Fake<ILogger<VehicleController>>();
            var dummyVehicle = A.Dummy<VehicleDto>();
            var fakeVehicleService = A.Fake<IVehicleService>();
            A.CallTo(() => fakeVehicleService.CreateVehicle(dummyVehicle)).Returns(Task.FromResult(0));
            var controller = new VehicleController(fakeVehicleService, fakeLogger);

            //Act
            var actionResult = await controller.CreateVehicle(dummyVehicle);

            //Assert
            var result = Assert.IsType<BadRequestResult>(actionResult);
            Assert.Equal(400, result.StatusCode);
        }

        [Fact]
        public async Task CreateVehicle_Throws_Server_Error()
        {
            //Arrange
            var fakeLogger = A.Fake<ILogger<VehicleController>>();
            var dummyVehicle = A.Dummy<VehicleDto>();
            var fakeVehicleService = A.Fake<IVehicleService>();
            A.CallTo(() => fakeVehicleService.CreateVehicle(dummyVehicle)).Throws(new Exception());
            var controller = new VehicleController(fakeVehicleService, fakeLogger);

            //Act
            var actionResult = await controller.CreateVehicle(dummyVehicle);

            //Assert
            var result = Assert.IsType<StatusCodeResult> (actionResult);
            Assert.Equal(500, result.StatusCode);
        }

        [Fact]
        public async Task GetAllVehicles_Returns_Populated_List()
        {
            //Arrange
            var fakeLogger = A.Fake<ILogger<VehicleController>>();
            var fakeVehicleService = A.Fake<IVehicleService>();
            var fakeDummyList = A.CollectionOfFake<VehicleDto>(3).ToList();
            A.CallTo(() => fakeVehicleService.GetAllVehicles()).Returns(Task.FromResult(fakeDummyList));
            var controller = new VehicleController(fakeVehicleService, fakeLogger);

            //Act
            var actionResult = await controller.GetAllVehicles();

            //Assert
            var result = Assert.IsType<OkObjectResult>(actionResult);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async Task GetAllVehicles_Returns_Zero_Rows()
        {
            //Arrange
            var fakeLogger = A.Fake<ILogger<VehicleController>>();
            var fakeVehicleService = A.Fake<IVehicleService>();
            var fakeDummyList = A.CollectionOfFake<VehicleDto>(0).ToList();
            A.CallTo(() => fakeVehicleService.GetAllVehicles()).Returns(Task.FromResult(fakeDummyList));
            var controller = new VehicleController(fakeVehicleService, fakeLogger);

            //Act
            var actionResult = await controller.GetAllVehicles();

            //Assert
            var result = Assert.IsType<NotFoundResult>(actionResult);
            Assert.Equal(404, result.StatusCode);
        }

        [Fact]
        public async Task GetAllVehicles_Throws_Exception()
        {
            //Arrange
            var fakeLogger = A.Fake<ILogger<VehicleController>>();
            var fakeVehicleService = A.Fake<IVehicleService>();
            A.CallTo(() => fakeVehicleService.GetAllVehicles()).Throws(new Exception());
            var controller = new VehicleController(fakeVehicleService, fakeLogger);

            //Act
            var actionResult = await controller.GetAllVehicles();

            //Assert
            var result = Assert.IsType<StatusCodeResult>(actionResult);
            Assert.Equal(500, result.StatusCode);
        }

        [Fact]
        public async Task GetAllAvailableVehicles_Returns_Populated_List()
        {
            //Arrange
            var fakeLogger = A.Fake<ILogger<VehicleController>>();
            var fakeVehicleService = A.Fake<IVehicleService>();
            var fakeDummyList = A.CollectionOfFake<VehicleDto>(3).ToList();
            A.CallTo(() => fakeVehicleService.GetAllAvailableVehicles()).Returns(Task.FromResult(fakeDummyList));
            var controller = new VehicleController(fakeVehicleService, fakeLogger);

            //Act
            var actionResult = await controller.GetAllAvailableVehicles();

            //Assert
            var result = Assert.IsType<OkObjectResult>(actionResult);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async Task GetAllAvailableVehicles_Returns_Zero_Rows()
        {
            //Arrange
            var fakeLogger = A.Fake<ILogger<VehicleController>>();
            var fakeVehicleService = A.Fake<IVehicleService>();
            var fakeDummyList = A.CollectionOfFake<VehicleDto>(0).ToList();
            A.CallTo(() => fakeVehicleService.GetAllAvailableVehicles()).Returns(Task.FromResult(fakeDummyList));
            var controller = new VehicleController(fakeVehicleService, fakeLogger);

            //Act
            var actionResult = await controller.GetAllAvailableVehicles();

            //Assert
            var result = Assert.IsType<NotFoundResult>(actionResult);
            Assert.Equal(404, result.StatusCode);
        }

        [Fact]
        public async Task GetAllAvailableVehicles_Throws_Exception()
        {
            //Arrange
            var fakeLogger = A.Fake<ILogger<VehicleController>>();
            var fakeVehicleService = A.Fake<IVehicleService>();
            A.CallTo(() => fakeVehicleService.GetAllAvailableVehicles()).Throws(new Exception());
            var controller = new VehicleController(fakeVehicleService, fakeLogger);

            //Act
            var actionResult = await controller.GetAllAvailableVehicles();

            //Assert
            var result = Assert.IsType<StatusCodeResult>(actionResult);
            Assert.Equal(500, result.StatusCode);
        }

        [Fact]
        public async Task GetVehicle_Returns_One()
        {
            //Arrange
            var fakeLogger = A.Fake<ILogger<VehicleController>>();
            var fakeVehicleService = A.Fake<IVehicleService>();
            var fakeDummy = A.Fake<VehicleDto>();
            A.CallTo(() => fakeVehicleService.GetVehicle(1)).Returns(Task.FromResult(fakeDummy));
            var controller = new VehicleController(fakeVehicleService, fakeLogger);

            //Act
            var actionResult = await controller.GetVehicle(1);

            //Assert
            var result = Assert.IsType<OkObjectResult>(actionResult);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async Task GetVehicle_Returns_Zero()
        {
            //Arrange
            var fakeLogger = A.Fake<ILogger<VehicleController>>();
            var fakeVehicleService = A.Fake<IVehicleService>();
            var fakeDummy = A.Fake<VehicleDto>();
            A.CallTo(() => fakeVehicleService.GetVehicle(0)).Returns(Task.FromResult(fakeDummy));
            var controller = new VehicleController(fakeVehicleService, fakeLogger);

            //Act
            var actionResult = await controller.GetVehicle(0);

            //Assert
            var result = Assert.IsType<NotFoundResult>(actionResult);
            Assert.Equal(404, result.StatusCode);
        }

        [Fact]
        public async Task GetVehicle_Throws_Exception()
        {
            //Arrange
            var fakeLogger = A.Fake<ILogger<VehicleController>>();
            var fakeVehicleService = A.Fake<IVehicleService>();
            A.CallTo(() => fakeVehicleService.GetVehicle('a')).Throws(new Exception());
            var controller = new VehicleController(fakeVehicleService, fakeLogger);

            //Act
            var actionResult = await controller.GetVehicle('a');

            //Assert
            var result = Assert.IsType<StatusCodeResult>(actionResult);
            Assert.Equal(500, result.StatusCode);
        }

        [Fact]
        public async Task DeleteVehicle_Returns_One_Row_Deleted()
        {
            //Arrange
            var fakeLogger = A.Fake<ILogger<VehicleController>>();
            var fakeVehicleService = A.Fake<IVehicleService>();
            A.CallTo(() => fakeVehicleService.DeleteVehicle(1)).Returns(Task.FromResult(1));
            var controller = new VehicleController(fakeVehicleService, fakeLogger);

            //Act
            var actionResult = await controller.DeleteVehicle(1);

            //Assert
            var result = Assert.IsType<NoContentResult>(actionResult);
            Assert.Equal(204, result.StatusCode);
        }

        [Fact]
        public async Task DeleteVehicle_Returns_No_Rows_Deleted()
        {
            //Arrange
            var fakeLogger = A.Fake<ILogger<VehicleController>>();
            var fakeVehicleService = A.Fake<IVehicleService>();
            A.CallTo(() => fakeVehicleService.DeleteVehicle(0)).Returns(Task.FromResult(0));
            var controller = new VehicleController(fakeVehicleService, fakeLogger);

            //Act
            var actionResult = await controller.DeleteVehicle(0);

            //Assert
            var result = Assert.IsType<BadRequestResult>(actionResult);
            Assert.Equal(400, result.StatusCode);
        }

        [Fact]
        public async Task DeleteVehicle_Throws_Exception()
        {
            //Arrange
            var fakeLogger = A.Fake<ILogger<VehicleController>>();
            var fakeVehicleService = A.Fake<IVehicleService>();
            A.CallTo(() => fakeVehicleService.DeleteVehicle('a')).Throws(new Exception());
            var controller = new VehicleController(fakeVehicleService, fakeLogger);

            //Act
            var actionResult = await controller.DeleteVehicle('a');

            //Assert
            var result = Assert.IsType<StatusCodeResult>(actionResult);
            Assert.Equal(500, result.StatusCode);
        }

        [Fact]
        public async Task UpdateVehicle_Returns_Row_Updated()
        {
            //Arrange
            var fakeLogger = A.Fake<ILogger<VehicleController>>();
            var fakeVehicleService = A.Fake<IVehicleService>();
            var fakeVehicleDto = A.Fake<VehicleDto>();
            fakeVehicleDto.VehicleId = 1;
            A.CallTo(() => fakeVehicleService.UpdateVehicle(fakeVehicleDto)).Returns(Task.FromResult(1));
            var controller = new VehicleController(fakeVehicleService, fakeLogger);

            //Act
            var actionResult = await controller.UpdateVehicle(fakeVehicleDto,1);

            //Assert
            var result = Assert.IsType<OkResult>(actionResult);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async Task UpdateVehicle_Returns_Zero_Rows_Updated()
        {
            //Arrange
            var fakeLogger = A.Fake<ILogger<VehicleController>>();
            var fakeVehicleService = A.Fake<IVehicleService>();
            var fakeVehicleDto = A.Fake<VehicleDto>();
            fakeVehicleDto.VehicleId = 0;
            A.CallTo(() => fakeVehicleService.UpdateVehicle(fakeVehicleDto)).Returns(Task.FromResult(0));
            var controller = new VehicleController(fakeVehicleService, fakeLogger);

            //Act
            var actionResult = await controller.UpdateVehicle(fakeVehicleDto, 0);

            //Assert
            var result = Assert.IsType<NoContentResult>(actionResult);
            Assert.Equal(204, result.StatusCode);
        }

        [Fact]
        public async Task UpdateVehicle_Returns_Id_Not_Found()
        {
            //Arrange
            var fakeLogger = A.Fake<ILogger<VehicleController>>();
            var fakeVehicleService = A.Fake<IVehicleService>();
            var fakeVehicleDto = A.Fake<VehicleDto>();
            fakeVehicleDto.VehicleId = 1;
            A.CallTo(() => fakeVehicleService.UpdateVehicle(fakeVehicleDto)).Returns(Task.FromResult(0));
            var controller = new VehicleController(fakeVehicleService, fakeLogger);

            //Act
            var actionResult = await controller.UpdateVehicle(fakeVehicleDto, 0);

            //Assert
            var result = Assert.IsType<NotFoundResult>(actionResult);
            Assert.Equal(404, result.StatusCode);
        }

        [Fact]
        public async Task UpdateVehicle_Throws_Exception()
        {
            //Arrange
            var fakeLogger = A.Fake<ILogger<VehicleController>>();
            var fakeVehicleService = A.Fake<IVehicleService>();
            var fakeVehicleDto = A.Fake<VehicleDto>();
            fakeVehicleDto.VehicleId = 'a';
            A.CallTo(() => fakeVehicleService.UpdateVehicle(fakeVehicleDto)).Throws(new Exception());
            var controller = new VehicleController(fakeVehicleService, fakeLogger);

            //Act
            var actionResult = await controller.UpdateVehicle(fakeVehicleDto, 'a');

            //Assert
            var result = Assert.IsType<StatusCodeResult>(actionResult);
            Assert.Equal(500, result.StatusCode);
        }

        [Fact]
        public async Task SearchVehicle_Returns_Populated_List()
        {
            //Arrange
            var fakeLogger = A.Fake<ILogger<VehicleController>>();
            var fakeVehicleService = A.Fake<IVehicleService>();
            var fakeDummySearch = A.Fake<VehicleSearch>();
            var fakeDummyList = A.CollectionOfFake<VehicleDto>(3).AsEnumerable();
            A.CallTo(() => fakeVehicleService.SearchVehicle(fakeDummySearch)).Returns(Task.FromResult(fakeDummyList));
            var controller = new VehicleController(fakeVehicleService, fakeLogger);

            //Act
            var actionResult = await controller.SearchVehicle(fakeDummySearch);

            //Assert
            var result = Assert.IsType<OkObjectResult>(actionResult);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async Task SearchVehicle_Returns_Empty_List()
        {
            //Arrange
            var fakeLogger = A.Fake<ILogger<VehicleController>>();
            var fakeVehicleService = A.Fake<IVehicleService>();
            var fakeDummySearch = A.Fake<VehicleSearch>();
            var fakeDummyList = A.CollectionOfFake<VehicleDto>(0).AsEnumerable();
            A.CallTo(() => fakeVehicleService.SearchVehicle(fakeDummySearch)).Returns(Task.FromResult(fakeDummyList));
            var controller = new VehicleController(fakeVehicleService, fakeLogger);

            //Act
            var actionResult = await controller.SearchVehicle(fakeDummySearch);

            //Assert
            var result = Assert.IsType<NotFoundResult>(actionResult);
            Assert.Equal(404, result.StatusCode);
        }

        [Fact]
        public async Task SearchVehicle_Throws_Exception()
        {
            //Arrange
            var fakeLogger = A.Fake<ILogger<VehicleController>>();
            var fakeVehicleService = A.Fake<IVehicleService>();
            var fakeDummySearch = A.Fake<VehicleSearch>();
            A.CallTo(() => fakeVehicleService.SearchVehicle(fakeDummySearch)).Throws(new Exception());
            var controller = new VehicleController(fakeVehicleService, fakeLogger);

            //Act
            var actionResult = await controller.SearchVehicle(fakeDummySearch);

            //Assert
            var result = Assert.IsType<StatusCodeResult>(actionResult);
            Assert.Equal(500, result.StatusCode);
        }
    }
}