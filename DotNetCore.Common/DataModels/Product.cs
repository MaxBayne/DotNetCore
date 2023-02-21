using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;


public interface IProduct
{
    Guid Id { get; set; }
    string Name { get; set; }
    string Description { get; set; }
    string ImageUrl { get; set; }
    string Url { get; set; }
    string Phone { get; set; }
    string Email { get; set; }
    decimal Percent { get; set; }
    string ToString();
}

[Table("tbl_Products", Schema = "dbo")]
public class Product : IProduct
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("Id")]
    public Guid Id { get; set; }

    [Display(Name = "Name")]
    [Column("Name",TypeName = "nvarchar(50)")]
    [Required]
    [MinLength(5)]
    [MaxLength(50)]
    public string Name { get; set; }

    [Display(Name = "Description")]
    [Column("Description", TypeName = "nvarchar(50)")]
    [MaxLength(255)]
    public string Description { get; set; }

    [Display(Name = "ImageUrl")]
    [Column("ImageUrl", TypeName = "nvarchar(100)")]
    public string ImageUrl { get; set; }

    [Display(Name = "Url")]
    [Column("Url", TypeName = "nvarchar(100)")]
    public string Url { get; set; }

    [Display(Name = "Phone")]
    [Column("Phone", TypeName = "nvarchar(50)")]
    [Phone]
    public string Phone { get; set; }

    [Display(Name = "Email")]
    [Column("Email", TypeName = "nvarchar(50)")]
    [EmailAddress]
    public string Email { get; set; }

    [Display(Name = "Percent")]
    [Column("Percent", TypeName = "decimal")]
    [Range(0,100)]
    public  decimal Percent { get; set; }

    //Convert Object To Json String using Json Serialization
    public override string ToString()=> JsonSerializer.Serialize<Product>(this);

}