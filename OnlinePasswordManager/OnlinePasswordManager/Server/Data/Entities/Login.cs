namespace OnlinePasswordManager.Server.Data.Entities
{
    public class Login
    {
        public int Id { get; set; }
        public string IpAddress { get; set; }
        public DateTime LoginTime { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}
