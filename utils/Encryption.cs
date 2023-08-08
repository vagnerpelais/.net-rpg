using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace net_rpg.utils
{
    public class Encryption
    {
        public string Encrypt(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }
    }
}