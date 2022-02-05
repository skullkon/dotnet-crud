using System.ComponentModel.DataAnnotations;
using PersonService.Models;

namespace PersonService.Dto;

public class PersonCreateDto
{
    [Required]
    public string Name { get; set; }
    [Required]
    public string DisplayName { get; set; }
    [Required]
    public List<Skill> Skills { get; set; } 
}