using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApp.Models.Enteties;

public class ProductEntity
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }

    [Column(TypeName = "money")]
    public decimal Price { get; set; }
    public string? ImgUrl { get; set; }
    public int CategoryId { get; set; }
    public IEnumerable<ProductCategoryEntity> Category { get; set; } = null!;
    //public GridCollectionViewModel Category { get; set; } = null!;

    public ICollection<ImagesEntity> Images = new HashSet<ImagesEntity>();
}
