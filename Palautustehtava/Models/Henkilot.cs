using System;
using System.Collections.Generic;

namespace Palautustehtava.Models
{
    public partial class Henkilot
    {
        public int HenkiloId { get; set; }
        public string? Nimi { get; set; }
        public string? Sukunimi { get; set; }
        public string? Osoite { get; set; }
    }
}
