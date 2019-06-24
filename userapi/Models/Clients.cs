using System;
using System.Collections.Generic;

namespace userapi.Models
{
    public partial class Clients
    {
        public int UserId { get; set; }
        public int Level { get; set; }
        public int MgrId { get; set; }

        public virtual Managers Mgr { get; set; }
        public virtual Users User { get; set; }
    }
}
