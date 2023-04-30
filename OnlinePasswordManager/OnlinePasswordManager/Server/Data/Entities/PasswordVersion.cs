namespace OnlinePasswordManager.Server.Data.Entities
{
    public class PasswordVersion
    {
        public int Id { get; set; }
        public string PreviousEncryptedPassword { get; set; }
        public DateTime ChangeTime { get; set; }

        public int PasswordId { get; set; }
        public Password Password { get; set; }
    }
}
