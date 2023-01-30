using System.ComponentModel.DataAnnotations;

namespace DotNetCore.Common.DataModels
{
    public class Employee
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Please Input Name")]
        [StringLength(20,MinimumLength = 5,ErrorMessage = "The Length of Name is Invalid")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please Input Department")]
        public string Department { get; set; }

        public bool IsActive { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

    }
}
