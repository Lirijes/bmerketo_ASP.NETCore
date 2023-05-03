using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApp.Models.Enteties
{
    public class ImagesEntity
    {
        [Key, ForeignKey("Products")]
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Image { get; set; } = null!;

        public string ProductId { get; set; } = null!;
        public ProductEntity Products { get; set; } = null!;
    }
}
