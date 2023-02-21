using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DotNetCore.Common.DataModels
{
    [Table("tbl_Departments", Schema = "dbo")]
    public class Department
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Id")]
        [Column("Id")]
        public Guid Id { get; set; }


        [Display(Name = "Department")]
        [Column("Name", TypeName = "nvarchar(50)")]
        [Required(ErrorMessage = "Please Input Name")]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "The Length of Name is Invalid")]
        public string Name { get; set; }

    }
}
