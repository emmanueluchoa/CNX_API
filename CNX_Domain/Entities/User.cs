using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace CNX_Domain.Entities
{
    public class User : IdentityUser
    {
        public string Locale { get; set; }
        public virtual IList<PersonalNote> PersonalNoteList { get; set; }
    }
}
