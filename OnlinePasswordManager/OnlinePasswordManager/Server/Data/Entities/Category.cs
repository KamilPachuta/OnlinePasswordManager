using System.Runtime.CompilerServices;

namespace OnlinePasswordManager.Server.Data.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }

        public int? UserId { get; set; }
        public User User { get; set; }

        public virtual IEnumerable<Password> Passwords { get; set; } // możliwe że d o usunięcia, wtedy pobieranie haseł użytkownika i filtrownaie na poziomie category
    }
}
