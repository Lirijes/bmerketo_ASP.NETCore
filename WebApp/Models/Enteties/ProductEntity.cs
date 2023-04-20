using System.ComponentModel.DataAnnotations.Schema;

namespace WebApp.Models.Enteties;

public class ProductEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }

    [Column(TypeName = "money")]
    public decimal Price { get; set; }

    //public string? Picture { get; set; }
    //public string? ImageLocation { get; set; }

    public ICollection<ImagesEntity> Images = new HashSet<ImagesEntity>();
}
