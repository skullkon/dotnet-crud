using WebApplication1.Models;

namespace WebApplication1.Dto;

public class PersonReadDto
{
        public int Id { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public List<Skill> Skills { get; set; } 
}