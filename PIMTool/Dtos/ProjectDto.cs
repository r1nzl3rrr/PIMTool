using PIMTool.Core.Domain.Entities;
using PIMTool.Core.Validation;
using System.ComponentModel.DataAnnotations;

namespace PIMTool.Dtos;

public class ProjectDto
{
    public int Id { get; set; }

    public int Group { get; set; }

    public int Project_Number { get; set; }

    public string Name { get; set; } = null!;

    public string Customer { get; set; } = null!;

    [ProjectStatus]
    public string Status { get; set; } = null!;
    public DateTime Start_Date { get; set; }
    public DateTime End_Date { get; set; }
}