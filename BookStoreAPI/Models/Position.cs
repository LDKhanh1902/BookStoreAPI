using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ReactAppTest.Server.Models
{
    public class Position
    {
        [Key]
        public int PositionId { get; set; }
        public string PositionName { get; set; } = string.Empty;
        public int Salary { get; set; }

        // Một vị trí có nhiều nhân viên
        [JsonIgnore]
        public ICollection<Employee>? Employees { get; set; }
    }
}
