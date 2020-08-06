using System;
using System.Collections.Generic;
using System.Text;

namespace CNX_Domain.Models
{
    public class UserVM
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string UserHomeTown { get; set; }
    }

    public class UserFullVM : UserVM
    {
        public string UserPassword { get; set; }
    }
}
