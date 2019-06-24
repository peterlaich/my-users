using System;
using System.Collections.Generic;

namespace userapi.Models
{
    public partial class Managers
    {
        public Managers()
        {
            Clients = new HashSet<Clients>();
        }

        public int UserId { get; set; }
        public int Position { get; set; }

        public virtual Users User { get; set; }
        public virtual ICollection<Clients> Clients { get; set; }
    }
}
