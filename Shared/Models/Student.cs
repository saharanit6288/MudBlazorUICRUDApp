using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MudBlazorUICRUDApp.Shared.Models
{
    public class Student
    {
        [Key]
        public int StudentID { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Name { get; set; }

        [Required]
        public int Age { get; set; }
        public string? Address { get; set; }


        [StringLength(100)]
        public string? ContactNo { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime DateOfBrith { get; set; }
        public Gender Gender { get; set; }
        public string? PhotoPath { get; set; }
    }
}
