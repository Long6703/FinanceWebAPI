using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Account
{
    public class NewUserDTO
    {
        public string Username { get; set; }

        public string Email { get; set; }

        public string Tokens { get; set; }
    }
}