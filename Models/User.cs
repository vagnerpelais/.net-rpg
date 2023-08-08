using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace net_rpg.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string HashedPassword { get; set; } = string.Empty;
    }
}