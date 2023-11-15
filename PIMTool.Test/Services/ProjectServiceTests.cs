using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using PIMTool.Core.Domain.Entities;
using PIMTool.Core.Interfaces.Repositories;
using PIMTool.Core.Interfaces.Services;
using PIMTool.Core.Specifications;

namespace PIMTool.Test.Services
{
    public class ProjectServiceTests : BaseTest
    {
        private IProjectService _projectService = null!;
        private IRepository<Project> _repository;

        [SetUp]
        public void SetUp()
        {
            _projectService = ServiceProvider.GetRequiredService<IProjectService>();
            _repository = ServiceProvider.GetRequiredService<IRepository<Project>>();
        }

        [Test]
        public async Task Get()
        {
            // Arrange
            

            // Act
            var result = await _repository.GetAsync();

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task AddProjectAsync()
        {
            //Arrange
            var project = new Project()
            {
                Name = "Test",
                Group_Id = 1,
                Customer = "A San",
                Status = "NEW"
            };
            await Context.AddAsync(project);
            await Context.SaveChangesAsync();

            //Act
            await _projectService.AddProjectAsync(project);

            //Assert
            Assert.IsNotNull(project);
        }
    }
}