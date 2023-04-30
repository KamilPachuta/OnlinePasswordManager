namespace OnlinePasswordManager.Server.Data.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public DateTime CreatedAt { get; set; }

        public virtual IEnumerable<Password> Passwords { get; set; }
        public virtual IEnumerable<Note> Notes { get; set; }
        public virtual IEnumerable<Login> Logins { get;set; }

        public virtual IEnumerable<Category> Categories { get; set; }
    }
}
