using System.ComponentModel.DataAnnotations;
using System.Text.Json;


public interface IProduct
{
    string Id { get; set; }
    string Name { get; set; }
    string Description { get; set; }
    string ImageUrl { get; set; }
    string Url { get; set; }
    string Phone { get; set; }
    string Email { get; set; }
    decimal Percent { get; set; }
    string ToString();
}

public class Product : IProduct
{
    public string Id { get; set; }

    
    [Required]
    [MinLength(5)]
    [MaxLength(50)]
    public string Name { get; set; }
    [MaxLength(255)]
    public string Description { get; set; }
    public string ImageUrl { get; set; }
    public string Url { get; set; }
    
    [Phone]
    public string Phone { get; set; }

    [EmailAddress]
    public string Email { get; set; }

    [Range(0,100)]
    public  decimal Percent { get; set; }

    //Convert Object To Json String using Json Serialization
    public override string ToString()=> JsonSerializer.Serialize<Product>(this);
}