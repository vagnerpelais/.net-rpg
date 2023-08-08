using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace net_rpg.Dtos.User
{
    public class RequestUserDto
    {
        public required string Username { get; set; }
        public required string Password { get; set; }
    }
}