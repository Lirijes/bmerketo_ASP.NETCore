namespace WebApp.Models.Enteties
{
    public class ContactsEntity
    {
        public int Id { get; set; } 
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string? Company { get; set; }
        public string Comment { get; set; } = null!;
    }
}
