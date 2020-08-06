using System;
using System.Collections.Generic;
using System.Text;

namespace CNX_Domain.Models
{
    public class AuthenticatedUserVM
    {
        public UserVM User { get; set; }
        public string Token { get; set; }
    }
}
