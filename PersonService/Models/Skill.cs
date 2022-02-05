using System.Text.Json.Serialization;

namespace PersonService.Models;

public class Skill
{
    [JsonIgnore]
    public int Id { get; set; }
    public string Name { get; set; }
    public byte Level { get; set; }
    
    [JsonIgnore]
    public int PersonId { get; set; }
    [JsonIgnore]
    public Person? Person { get; set; }
}