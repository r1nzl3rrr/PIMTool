using Microsoft.Extensions.FileSystemGlobbing.Internal.PatternContexts;
using PIMTool.Core.Domain.Entities;
using System.Text.Json;

namespace PIMTool.Database
{
    public class PimContextSeed
    {
        public static async Task SeedAsync(PimContext context)
        {
            if (!context.Employees.Any())
            {
                var employeesData = File.ReadAllText("../PIMTool/SeedData/employees.json");
                var employees = JsonSerializer.Deserialize<List<Employee>>(employeesData);
                context.Employees.AddRange(employees);
            }

            if (!context.Groups.Any())
            {
                var groupsData = File.ReadAllText("../PIMTool/SeedData/groups.json");
                var groups = JsonSerializer.Deserialize<List<Group>>(groupsData);
                context.Groups.AddRange(groups);
            }

            if (!context.Projects.Any())
            {
                var projectsData = File.ReadAllText("../PIMTool/SeedData/projects.json");
                var projects = JsonSerializer.Deserialize<List<Project>>(projectsData);
                context.Projects.AddRange(projects);
            }

            if (context.ChangeTracker.HasChanges()) await context.SaveChangesAsync();

            if (!context.ProjectEmployees.Any())
            {
                var peData = File.ReadAllText("../PIMTool/SeedData/projectemployee.json");
                var pe = JsonSerializer.Deserialize<List<ProjectEmployee>>(peData);
                context.ProjectEmployees.AddRange(pe);
            }

            if (context.ChangeTracker.HasChanges()) await context.SaveChangesAsync();
        }
    }
}
