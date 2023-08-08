using System;
namespace net_rpg.utils
{
    public class Encryption
    {
        public string Encrypt(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public Boolean Compare(string password, string hash)
        {
            if(!BCrypt.Net.BCrypt.Verify(password, hash))
            {
                 return false;
            }

            return true;
        }
    }
}