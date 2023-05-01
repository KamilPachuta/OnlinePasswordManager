using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlinePasswordManager.Shared.Models.DTO
{
    public class PasswordCreateDTO
    {
        public string ServiceName { get; set; }
        public string Login { get; set; }
        public string EncryptedPassword { get; set; }
        public string? URL { get; set; }
        public string? QuickNote { get; set; }

        public int? CategoryId { get; set; }
    }
}
