using System;
using System.Collections.Generic;

namespace Palautustehtava.Models
{
    public partial class Tunnit
    {
        public int TunnitId { get; set; }
        public int HenkiloId { get; set; }
        public int? TunnitMaara { get; set; }
    }
}
