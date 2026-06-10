namespace ODataSample.Database.Models;

public partial class Company
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? City { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
