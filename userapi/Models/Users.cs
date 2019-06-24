using System;
using System.Collections.Generic;

namespace userapi.Models
{
    public partial class Users
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Alias { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public virtual Clients Clients { get; set; }
        public virtual Managers Managers { get; set; }
    }
}
