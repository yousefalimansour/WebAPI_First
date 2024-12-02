using System.Text.Json.Serialization;

namespace WebAPI_First.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? ManagerName { get; set; }
        [JsonIgnore]
        public List<Empolyee>? Empolyes { get; set; }
    }
}
