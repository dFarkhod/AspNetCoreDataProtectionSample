using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Virtualdars.DataProtectionSample.Models
{
    public class EmployeeModel
    {
        [Required]
        public string Id { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        public string Email { get; set; }
    }
}
