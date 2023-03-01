using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotNetCore.Common.DataModels
{
    [Table("tbl_Employees", Schema = "dbo")]
    public class Employee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Id")]
        [Column("Id")]
        public Guid Id { get; set; }

        [Display(Name = "Name")]
        [Column("Name",TypeName = "nvarchar(50)")]
        [Required(ErrorMessage = "Please Input Name")]
        [StringLength(50,MinimumLength = 4,ErrorMessage = "The Length of Name is Invalid")]
        public string Name { get; set; }

        [Display(Name = "IsActive")]
        [Column("IsActive", TypeName = "bit")]
        public bool IsActive { get; set; }

        [Display(Name = "DOB")]
        [Column("DateOfBirth", TypeName = "datetime")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Display(Name = "Phone")]
        [Column("Phone", TypeName = "nvarchar(20)")]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        [Display(Name = "Email")]
        [Column("Email", TypeName = "nvarchar(50)")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [ForeignKey(nameof(Department))]
        [Display(Name = "DepartmentId")]
        [Column("DepartmentId")]
        [Required(ErrorMessage = "Please Input Department")]
        public Guid DepartmentId { get; set; }
        public virtual Department Department { get; set; }
    }
}
