using Microsoft.AspNetCore.Mvc;
using Moq;
using Predictr.Controllers;
using Predictr.Interfaces;
using Predictr.Models;
using Predictr.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Predictr.Tests.Controllers
{
    public class FixturesControllerTest
    {
        private Mock<IFixtureRepository> fixturesRepoMock;
        private Mock<IPredictionRepository> predictionsRepoMock;
        private FixturesController controller;

        public FixturesControllerTest()
        {
            fixturesRepoMock = new Mock<IFixtureRepository>();
            predictionsRepoMock = new Mock<IPredictionRepository>();
            controller = new FixturesController(fixturesRepoMock.Object, predictionsRepoMock.Object);
        }

        [Fact]
        public async Task IndexTest_ReturnsViewWithFixturesList()
        {
            // Arrange
            var mockFixturesList = new List<Fixture>
            {
            new Fixture { Group = "A", FixtureDateTime = DateTime.Now, Home = "Brazil", Away = "Germany", HomeScore = 1, AwayScore = 1 },
            new Fixture { Group = "B", FixtureDateTime = DateTime.Now, Home = "England", Away = "Belgium", HomeScore = 3, AwayScore = 2 }
            };

            fixturesRepoMock
                .Setup(repo => repo.GetAll())
                .Returns(Task.FromResult(mockFixturesList));

            // Act
            var result = await controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<VM_Fixture>>(viewResult.ViewData.Model);
            Assert.Equal(2, model.Count());
        }

        [Fact]
        public async Task DetailsTest_ReturnViewWithSingleFixture_WhenFixtureExists() {

            // Arrange
            var fixture = new Fixture { Id = 12, Group = "A", FixtureDateTime = DateTime.Now, Home = "Brazil", Away = "Germany", HomeScore = 1, AwayScore = 1 };

            fixturesRepoMock
                .Setup(repo => repo.GetSingleFixture(fixture.Id))
                .Returns(Task.FromResult(fixture));

            // Act
            var result = await controller.Details(fixture.Id);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = viewResult.ViewData.Model;
            Assert.Equal(fixture, viewResult.ViewData.Model);
        }

        [Fact]
        public async Task DetailsTest_ReturnsNotFound_WhenNoIdProvided()
        {
            // Arrange
            // Controller created already

            // Act
            var result = await controller.Details(null);

            // Assert
            var viewResult = Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task DetailsTest_ReturnsNotFound_WhenNoFixtureWithGivenId()
        {
            // Arrange
            var mockId = 42;
            fixturesRepoMock
                .Setup(repo => repo.GetSingleFixture(mockId))
                .Returns(Task.FromResult<Fixture>(null));

            // Act
            var result = await controller.Details(42);

            // Assert
            var viewResult = Assert.IsType<NotFoundResult>(result);
        }


        // get create
        [Fact]
        public void CreateTest_ReturnsViewWhenCreatingFixture() {
            // Arrange
            // Controller created already in constructor

            // Act
            var result = controller.Create();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal("Create", viewResult.ViewName);
        }
        


        // post create
        [Fact]
        public async Task CreateTest_RedirectToAdminIndex_WhenModelStateIsInvalid()
        {
            // Arrange
            var mockFixture = new Fixture { Id = 12, Group = "A", FixtureDateTime = DateTime.Now, Away = "Germany", HomeScore = 1, AwayScore = 1 };
            controller.ModelState.AddModelError("Home", "This field is required");

            // Act
            var RedirectResult = (RedirectToActionResult) await controller.Create(mockFixture);

            // Assert
            Assert.Equal("Admin", RedirectResult.ControllerName);
            Assert.Equal("Index", RedirectResult.ActionName);
        }

        [Fact]
        public async Task CreateTest_AddsToRepository_RedirectToAdminIndex_WhenModelStateIsValid()
        {
            // Arrange
            var mockFixture = new Fixture { Id = 12, Group = "A", Home = "England", FixtureDateTime = DateTime.Now, Away = "Germany", HomeScore = 1, AwayScore = 1 };
            fixturesRepoMock
                .Setup(repo => repo.SaveChanges())
                .Returns(Task.CompletedTask);

            // Act
            var result = await controller.Create(mockFixture);

            // Assert
            fixturesRepoMock.Verify(repo => repo.Add(mockFixture));
            var viewResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", viewResult.ActionName);
        }

        // get edit
        [Fact]
        public async Task EditTest_ReturnViewWithSingleFixture_WhenFixtureExists()
        {

            // Arrange
            var fixture = new Fixture { Id = 12, Group = "A", FixtureDateTime = DateTime.Now, Home = "Brazil", Away = "Germany", HomeScore = 1, AwayScore = 1 };

            fixturesRepoMock
                .Setup(repo => repo.GetSingleFixture(fixture.Id))
                .Returns(Task.FromResult(fixture));

            // Act
            var result = await controller.Edit(fixture.Id);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<VM_EditFixture>(viewResult.ViewData.Model);
            Assert.Equal("Edit", viewResult.ViewName);
        }

        // get edit where no fixture exists
        [Fact]
        public async Task EditTest_ReturnsNotFound_WhenNoFixtureExists()
        {
            // Arrange
            var mockId = 42;
            fixturesRepoMock
                .Setup(repo => repo.GetSingleFixture(mockId))
                .Returns(Task.FromResult<Fixture>(null));

            // Act
            var result = await controller.Edit(42);

            // Assert
            var viewResult = Assert.IsType<NotFoundResult>(result);
        }

        // post edit when invalid

        // post edit when valid

        // get delete

        // post delete
    }
}
