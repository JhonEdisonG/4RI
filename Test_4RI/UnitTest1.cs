namespace _4RI.Test
{
    public class BusinessIdeasControllerTests
    {
        [Fact]
        public void Add_Get_ReturnsViewResult()
        {
            // Arrange
            var controller = new BusinessIdeasController();

            // Act
            var result = controller.Add();

            // Assert
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Add_Post_ValidModel_ReturnsRedirectToActionResult()
        {
            // Arrange
            var controller = new BusinessIdeasController();
            var newIdea = new BusinessIdea
            {
                // Inicializa los datos necesarios para un modelo válido
                Name = "Idea1",
                Impact = "Impacto",
                InvestmentValue = 1000,
                TotalIncomeIn3Years = 5000,
            };

            // Act
            var result = controller.Add(newIdea);

            // Assert
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectResult.ActionName);
        }

        [Fact]
        public void Add_Post_InvalidModel_ReturnsViewResult()
        {
            // Arrange
            var controller = new BusinessIdeasController();
            controller.ModelState.AddModelError("Name", "Required"); // Simula un modelo no válido.

            // Act
            var result = controller.Add(new BusinessIdea());

            // Assert
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Index_ReturnsViewResultWithBusinessIdeasList()
        {
            // Arrange
            var controller = new BusinessIdeasController();
            var businessIdeas = new List<BusinessIdea>
            {
                new BusinessIdea { Id = Guid.NewGuid(), Name = "Idea1", Impact = "Impacto" },
                new BusinessIdea { Id = Guid.NewGuid(), Name = "Idea2", Impact = "Impacto2" }
            };
            // Simula la asignación de BusinessIdeas a la propiedad estática BusinessIdeas.
            BusinessIdeasController.BusinessIdeas = businessIdeas;

            // Act
            var result = controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<List<BusinessIdea>>(viewResult.ViewData.Model);
            Assert.Equal(businessIdeas, model);
        }

        // Agrega más pruebas para otros métodos aquí...

        [Fact]
        public void BusinessStatistics_ReturnsViewResultWithStatistics()
        {
            // Arrange
            var controller = new BusinessIdeasController();
            var businessIdeas = new List<BusinessIdea>
            {
                new BusinessIdea { Name = "Idea1", Impact = "Impacto", TotalIncomeIn3Years = 5000, InvestmentValue = 1000, Tools4IR = new List<string> { "AI" } },
                new BusinessIdea { Name = "Idea2", Impact = "Impacto2", TotalIncomeIn3Years = 7000, InvestmentValue = 2000, Tools4IR = new List<string> { "AI", "IoT" } },
            };
            // Simula la asignación de BusinessIdeas a la propiedad estática BusinessIdeas.
            BusinessIdeasController.BusinessIdeas = businessIdeas;

            // Act
            var result = controller.BusinessStatistics();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<BusinessStatisticsViewModel>(viewResult.ViewData.Model);
            Assert.NotNull(model.IdeaWithMostDepartments);
            Assert.NotNull(model.IdeaWithHighestIncome);
            Assert.NotEmpty(model.Top3ProfitableIdeas);
            Assert.NotEmpty(model.IdeasImpactingMoreThan3Depts);
            Assert.Equal(12000, model.TotalIncome);
            Assert.Equal(3000, model.TotalInvestment);
            Assert.NotNull(model.IdeaWithMost4RITools);
            Assert.Equal(2, model.CountOfAIUse);
            Assert.NotEmpty(model.IdeasWithSustainableDevelopment);
        }
    }
}