using System.ComponentModel.DataAnnotations;
using WebApplication1.Models;

namespace WebApplication1.Dto;

public class PersonCreateDto
{
    [Required]
    public string Name { get; set; }
    [Required]
    public string DisplayName { get; set; }
    [Required]
    public List<Skill> Skills { get; set; } 
}