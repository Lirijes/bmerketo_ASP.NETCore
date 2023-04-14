using System.Security.Cryptography;
using System.Text;

namespace WebApp.Models.Enteties;

public class UserEntity
{
    public string Id { get; set; } = null!;
    public string Email { get; set; } = null!;
    public byte[] Password { get; private set; } = null!;
    public byte[] SecurityKey { get; private set; } = null!;

    public void GenerateSecurePassword(string password)
    {
        using var hmac = new HMACSHA512(); //using gör en instans av denna algoritm generatorn, efter {} så förstörs den så den kommer ej finnas längre än just här
        SecurityKey = hmac.Key;
        Password = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
    }

    public bool VerifySecurePassword(string password) //verifiera lösenordet
    {
        using var hmac = new HMACSHA512(SecurityKey);
        var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

        for (int i = 0; i < hash.Length; i++)
        {
            if (hash[i] != Password[i]) //om de skiljer sig ifrån varandra return false
                return false;
        }

        return true;
    }
}
