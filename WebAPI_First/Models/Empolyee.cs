using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI_First.Models
{
    public class Empolyee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Address { get; set; }
        [ForeignKey("Department")]
        public int DepartmentId { get; set; }
        public Department? Department { get; set; }
    }
}
