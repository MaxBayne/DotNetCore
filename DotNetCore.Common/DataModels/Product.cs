using System.Text.Json;



public class Product
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string ImageUrl { get; set; }
    public string Url { get; set; }

    //Convert Object To Json String using Json Serialization
    public override string ToString()=> JsonSerializer.Serialize<Product>(this);
}