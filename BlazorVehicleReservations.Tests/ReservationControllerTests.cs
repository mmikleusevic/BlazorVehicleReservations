using BlazorVehicleReservations.API.Controllers;
using BlazorVehicleReservations.API.Service.Interface;
using BlazorVehicleReservations.Shared.Models.Dto;
using BlazorVehicleReservations.Shared.Models.Search;
using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BlazorVehicleReservations.Tests
{
    public class ReservationControllerTests
    {
        [Fact]
        public async Task CreateReservation_Returns_One_Inserted_Row()
        {
            //Arrange
            var fakeLogger = A.Fake<ILogger<ReservationController>>();
            var dummyReservation = A.Dummy<ReservationDto>();
            var fakeReservationService = A.Fake<IReservationService>();
            A.CallTo(() => fakeReservationService.CreateReservation(dummyReservation)).Returns(Task.FromResult(1));
            var controller = new ReservationController(fakeReservationService, fakeLogger);

            //Act
            var actionResult = await controller.CreateReservation(dummyReservation);

            //Assert
            var result = Assert.IsType<CreatedResult>(actionResult);
            Assert.Equal(1, result.Value);
        }

        [Fact]
        public async Task CreateReservation_Returns_Zero_Inserted_Rows()
        {
            //Arrange
            var fakeLogger = A.Fake<ILogger<ReservationController>>();
            var dummyReservation = A.Dummy<ReservationDto>();
            var fakeReservationService = A.Fake<IReservationService>();
            A.CallTo(() => fakeReservationService.CreateReservation(dummyReservation)).Returns(Task.FromResult(0));
            var controller = new ReservationController(fakeReservationService, fakeLogger);

            //Act
            var actionResult = await controller.CreateReservation(dummyReservation);

            //Assert
            var result = Assert.IsType<BadRequestResult>(actionResult);
            Assert.Equal(400, result.StatusCode);
        }

        [Fact]
        public async Task CreateReservation_Throws_Server_Error()
        {
            //Arrange
            var fakeLogger = A.Fake<ILogger<ReservationController>>();
            var dummyReservation = A.Dummy<ReservationDto>();
            var fakeReservationService = A.Fake<IReservationService>();
            A.CallTo(() => fakeReservationService.CreateReservation(dummyReservation)).Throws(new Exception());
            var controller = new ReservationController(fakeReservationService, fakeLogger);

            //Act
            var actionResult = await controller.CreateReservation(dummyReservation);

            //Assert
            var result = Assert.IsType<StatusCodeResult>(actionResult);
            Assert.Equal(500, result.StatusCode);
        }

        [Fact]
        public async Task CreateReservation_Can_Vehicle_Be_Reserved_Returns_True()
        {
            //Arrange
            var fakeLogger = A.Fake<ILogger<ReservationController>>();
            var dummyReservation = A.Dummy<ReservationDto>();
            var fakeReservationService = A.Fake<IReservationService>();
            A.CallTo(() => fakeReservationService.CanVehicleBeReserved(dummyReservation)).Returns(Task.FromResult(true));
            A.CallTo(() => fakeReservationService.CreateReservation(dummyReservation)).Returns(Task.FromResult(1));
            var controller = new ReservationController(fakeReservationService, fakeLogger);

            //Act
            var actionResult = await controller.CreateReservation(dummyReservation);

            //Assert
            var result = Assert.IsType<CreatedResult>(actionResult);
            Assert.Equal(201, result.StatusCode);
        }

        [Fact]
        public async Task CreateReservation_Can_Vehicle_Be_Reserved_Returns_False()
        {
            //Arrange
            var fakeLogger = A.Fake<ILogger<ReservationController>>();
            var dummyReservation = A.Dummy<ReservationDto>();
            var fakeReservationService = A.Fake<IReservationService>();
            A.CallTo(() => fakeReservationService.CanVehicleBeReserved(dummyReservation)).Returns(Task.FromResult(false));
            A.CallTo(() => fakeReservationService.CreateReservation(dummyReservation)).Returns(Task.FromResult(0));
            var controller = new ReservationController(fakeReservationService, fakeLogger);

            //Act
            var actionResult = await controller.CreateReservation(dummyReservation);

            //Assert
            var result = Assert.IsType<BadRequestResult>(actionResult);
            Assert.Equal(400, result.StatusCode);
        }

        [Fact]
        public async Task GetAllReservations_Returns_Populated_List()
        {
            //Arrange
            var fakeLogger = A.Fake<ILogger<ReservationController>>();
            var fakeDummyList = A.CollectionOfFake<ReservationDto>(3).ToList();
            var fakeReservationService = A.Fake<IReservationService>();
            A.CallTo(() => fakeReservationService.GetAllReservations()).Returns(Task.FromResult(fakeDummyList));
            var controller = new ReservationController(fakeReservationService, fakeLogger);

            //Act
            var actionResult = await controller.GetAllReservations();

            //Assert
            var result = Assert.IsType<OkObjectResult>(actionResult);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async Task GetAllReservations_Returns_Zero_Rows()
        {
            //Arrange
            var fakeLogger = A.Fake<ILogger<ReservationController>>();
            var fakeDummyList = A.CollectionOfFake<ReservationDto>(0).ToList();
            var fakeReservationService = A.Fake<IReservationService>();
            A.CallTo(() => fakeReservationService.GetAllReservations()).Returns(Task.FromResult(fakeDummyList));
            var controller = new ReservationController(fakeReservationService, fakeLogger);

            //Act
            var actionResult = await controller.GetAllReservations();

            //Assert
            var result = Assert.IsType<NotFoundResult>(actionResult);
            Assert.Equal(404, result.StatusCode);
        }

        [Fact]
        public async Task GetAllReservations_Throws_Exception()
        {
            //Arrange
            var fakeLogger = A.Fake<ILogger<ReservationController>>();
            var fakeReservationService = A.Fake<IReservationService>();
            A.CallTo(() => fakeReservationService.GetAllReservations()).Throws(new Exception());
            var controller = new ReservationController(fakeReservationService, fakeLogger);

            //Act
            var actionResult = await controller.GetAllReservations();

            //Assert
            var result = Assert.IsType<StatusCodeResult>(actionResult);
            Assert.Equal(500, result.StatusCode);
        }

        [Fact]
        public async Task GetAllCurrentReservations_Returns_Populated_List()
        {
            //Arrange
            var fakeLogger = A.Fake<ILogger<ReservationController>>();
            var fakeDummyList = A.CollectionOfFake<ReservationDto>(3).ToList();
            var fakeReservationService = A.Fake<IReservationService>();
            A.CallTo(() => fakeReservationService.GetAllCurrentReservations()).Returns(Task.FromResult(fakeDummyList));
            var controller = new ReservationController(fakeReservationService, fakeLogger);

            //Act
            var actionResult = await controller.GetAllCurrentReservations();

            //Assert
            var result = Assert.IsType<OkObjectResult>(actionResult);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async Task GetAllCurrentReservations_Returns_Zero_Rows()
        {
            //Arrange
            var fakeLogger = A.Fake<ILogger<ReservationController>>();
            var fakeDummyList = A.CollectionOfFake<ReservationDto>(0).ToList();
            var fakeReservationService = A.Fake<IReservationService>();
            A.CallTo(() => fakeReservationService.GetAllCurrentReservations()).Returns(Task.FromResult(fakeDummyList));
            var controller = new ReservationController(fakeReservationService, fakeLogger);

            //Act
            var actionResult = await controller.GetAllCurrentReservations();

            //Assert
            var result = Assert.IsType<NotFoundResult>(actionResult);
            Assert.Equal(404, result.StatusCode);
        }

        [Fact]
        public async Task GetAllCurrentReservations_Throws_Exception()
        {
            //Arrange
            var fakeLogger = A.Fake<ILogger<ReservationController>>();
            var fakeReservationService = A.Fake<IReservationService>();
            A.CallTo(() => fakeReservationService.GetAllCurrentReservations()).Throws(new Exception());
            var controller = new ReservationController(fakeReservationService, fakeLogger);

            //Act
            var actionResult = await controller.GetAllCurrentReservations();

            //Assert
            var result = Assert.IsType<StatusCodeResult>(actionResult);
            Assert.Equal(500, result.StatusCode);
        }

        [Fact]
        public async Task GetReservation_Returns_One()
        {
            //Arrange
            var fakeLogger = A.Fake<ILogger<ReservationController>>();
            var fakeReservationService = A.Fake<IReservationService>();
            var fakeDummy = A.Fake<ReservationDto>();
            A.CallTo(() => fakeReservationService.GetReservation(1)).Returns(Task.FromResult(fakeDummy));
            var controller = new ReservationController(fakeReservationService, fakeLogger);

            //Act
            var actionResult = await controller.GetReservation(1);

            //Assert
            var result = Assert.IsType<OkObjectResult>(actionResult);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async Task GetReservation_Returns_Zero()
        {
            //Arrange
            var fakeLogger = A.Fake<ILogger<ReservationController>>();
            var fakeReservationService = A.Fake<IReservationService>();
            var fakeDummy = A.Fake<ReservationDto>();
            A.CallTo(() => fakeReservationService.GetReservation(0)).Returns(Task.FromResult(fakeDummy));
            var controller = new ReservationController(fakeReservationService, fakeLogger);

            //Act
            var actionResult = await controller.GetReservation(0);

            //Assert
            var result = Assert.IsType<NotFoundResult>(actionResult);
            Assert.Equal(404, result.StatusCode);
        }

        [Fact]
        public async Task GetReservation_Throws_Exception()
        {
            //Arrange
            var fakeLogger = A.Fake<ILogger<ReservationController>>();
            var fakeReservationService = A.Fake<IReservationService>();
            A.CallTo(() => fakeReservationService.GetReservation('a')).Throws(new Exception());
            var controller = new ReservationController(fakeReservationService, fakeLogger);

            //Act
            var actionResult = await controller.GetReservation('a');

            //Assert
            var result = Assert.IsType<StatusCodeResult>(actionResult);
            Assert.Equal(500, result.StatusCode);
        }

        [Fact]
        public async Task DeleteReservation_Returns_One_Row_Deleted()
        {
            //Arrange
            var fakeLogger = A.Fake<ILogger<ReservationController>>();
            var fakeReservationService = A.Fake<IReservationService>();
            A.CallTo(() => fakeReservationService.DeleteReservation(1)).Returns(Task.FromResult(1));
            var controller = new ReservationController(fakeReservationService, fakeLogger);

            //Act
            var actionResult = await controller.DeleteReservation(1);

            //Assert
            var result = Assert.IsType<NoContentResult>(actionResult);
            Assert.Equal(204, result.StatusCode);
        }

        [Fact]
        public async Task DeleteReservation_Returns_No_Rows_Deleted()
        {
            //Arrange
            var fakeLogger = A.Fake<ILogger<ReservationController>>();
            var fakeReservationService = A.Fake<IReservationService>();
            A.CallTo(() => fakeReservationService.DeleteReservation(1)).Returns(Task.FromResult(0));
            var controller = new ReservationController(fakeReservationService, fakeLogger);

            //Act
            var actionResult = await controller.DeleteReservation(0);

            //Assert
            var result = Assert.IsType<BadRequestResult>(actionResult);
            Assert.Equal(400, result.StatusCode);
        }

        [Fact]
        public async Task DeleteReservation_Throws_Exception()
        {
            //Arrange
            var fakeLogger = A.Fake<ILogger<ReservationController>>();
            var fakeReservationService = A.Fake<IReservationService>();
            A.CallTo(() => fakeReservationService.DeleteReservation('a')).Throws(new Exception());
            var controller = new ReservationController(fakeReservationService, fakeLogger);

            //Act
            var actionResult = await controller.DeleteReservation('a');

            //Assert
            var result = Assert.IsType<StatusCodeResult>(actionResult);
            Assert.Equal(500, result.StatusCode);
        }

        [Fact]
        public async Task UpdateReservation_Returns_Row_Updated()
        {
            //Arrange
            var fakeLogger = A.Fake<ILogger<ReservationController>>();
            var fakeReservationService = A.Fake<IReservationService>();
            var fakeReservationDto = A.Fake<ReservationDto>();
            fakeReservationDto.Id = 1;
            A.CallTo(() => fakeReservationService.UpdateReservation(fakeReservationDto)).Returns(Task.FromResult(1));
            var controller = new ReservationController(fakeReservationService, fakeLogger);

            //Act
            var actionResult = await controller.UpdateReservation(fakeReservationDto, 1);

            //Assert
            var result = Assert.IsType<OkResult>(actionResult);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async Task UpdateReservation_Returns_Zero_Rows_Updated()
        {
            //Arrange
            var fakeLogger = A.Fake<ILogger<ReservationController>>();
            var fakeReservationService = A.Fake<IReservationService>();
            var fakeReservationDto = A.Fake<ReservationDto>();
            fakeReservationDto.Id = 0;
            A.CallTo(() => fakeReservationService.UpdateReservation(fakeReservationDto)).Returns(Task.FromResult(0));
            var controller = new ReservationController(fakeReservationService, fakeLogger);

            //Act
            var actionResult = await controller.UpdateReservation(fakeReservationDto, 0);

            //Assert
            var result = Assert.IsType<NoContentResult>(actionResult);
            Assert.Equal(204, result.StatusCode);
        }

        [Fact]
        public async Task UpdateReservation_Returns_Id_Not_Found()
        {
            //Arrange
            var fakeLogger = A.Fake<ILogger<ReservationController>>();
            var fakeReservationService = A.Fake<IReservationService>();
            var fakeReservationDto = A.Fake<ReservationDto>();
            fakeReservationDto.Id = 1;
            A.CallTo(() => fakeReservationService.UpdateReservation(fakeReservationDto)).Returns(Task.FromResult(0));
            var controller = new ReservationController(fakeReservationService, fakeLogger);

            //Act
            var actionResult = await controller.UpdateReservation(fakeReservationDto, 0);

            //Assert
            var result = Assert.IsType<NotFoundResult>(actionResult);
            Assert.Equal(404, result.StatusCode);
        }

        [Fact]
        public async Task UpdateReservation_Throws_Exception()
        {
            //Arrange
            var fakeLogger = A.Fake<ILogger<ReservationController>>();
            var fakeReservationService = A.Fake<IReservationService>();
            var fakeReservationDto = A.Fake<ReservationDto>();
            fakeReservationDto.Id = 'a';
            A.CallTo(() => fakeReservationService.UpdateReservation(fakeReservationDto)).Throws(new Exception());
            var controller = new ReservationController(fakeReservationService, fakeLogger);

            //Act
            var actionResult = await controller.UpdateReservation(fakeReservationDto, 'a');

            //Assert
            var result = Assert.IsType<StatusCodeResult>(actionResult);
            Assert.Equal(500, result.StatusCode);
        }

        [Fact]
        public async Task SearchReservation_Returns_Populated_List()
        {
            //Arrange
            var fakeLogger = A.Fake<ILogger<ReservationController>>();
            var fakeReservationService = A.Fake<IReservationService>();
            var fakeDummySearch = A.Fake<ReservationSearch>();
            var fakeDummyList = A.CollectionOfFake<ReservationDto>(3).AsEnumerable();
            A.CallTo(() => fakeReservationService.SearchReservation(fakeDummySearch)).Returns(Task.FromResult(fakeDummyList));
            var controller = new ReservationController(fakeReservationService, fakeLogger);

            //Act
            var actionResult = await controller.SearchReservation(fakeDummySearch);

            //Assert
            var result = Assert.IsType<OkObjectResult>(actionResult);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async Task SearchReservation_Returns_Empty_List()
        {
            //Arrange
            var fakeLogger = A.Fake<ILogger<ReservationController>>();
            var fakeReservationService = A.Fake<IReservationService>();
            var fakeDummySearch = A.Fake<ReservationSearch>();
            var fakeDummyList = A.CollectionOfFake<ReservationDto>(0).AsEnumerable();
            A.CallTo(() => fakeReservationService.SearchReservation(fakeDummySearch)).Returns(Task.FromResult(fakeDummyList));
            var controller = new ReservationController(fakeReservationService, fakeLogger);

            //Act
            var actionResult = await controller.SearchReservation(fakeDummySearch);

            //Assert
            var result = Assert.IsType<NotFoundResult>(actionResult);
            Assert.Equal(404, result.StatusCode);
        }

        [Fact]
        public async Task SearchReservation_Throws_Exception()
        {
            //Arrange
            var fakeLogger = A.Fake<ILogger<ReservationController>>();
            var fakeReservationService = A.Fake<IReservationService>();
            var fakeDummySearch = A.Fake<ReservationSearch>();
            A.CallTo(() => fakeReservationService.SearchReservation(fakeDummySearch)).Throws(new Exception());
            var controller = new ReservationController(fakeReservationService, fakeLogger);

            //Act
            var actionResult = await controller.SearchReservation(fakeDummySearch);

            //Assert
            var result = Assert.IsType<StatusCodeResult>(actionResult);
            Assert.Equal(500, result.StatusCode);
        }

    }
}
