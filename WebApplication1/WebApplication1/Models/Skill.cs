using System.Text.Json.Serialization;

namespace WebApplication1.Models;

public class Skill
{
    public int Id { get; set; }
    public string Name { get; set; }
    public byte Level { get; set; }
    
    public int PersonId { get; set; }
    [JsonIgnore]
    public Person? Person { get; set; }
}