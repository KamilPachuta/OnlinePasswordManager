namespace OnlinePasswordManager.Server.Data.Entities
{
    public class Password
    {
        public int Id { get; set; }
        public string ServiceName { get; set; }
        public string Login { get; set; }
        public string EncryptedPassword { get; set; }
        public string? URL { get; set; }
        public string? QuickNote { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }
        public int? CategoryId { get; set; }
        public virtual Category Category { get; set; }

        public virtual IEnumerable<PasswordVersion> Versions { get; set; }
    }
}
