using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigiAviator.Core.Models
{
    public class AirportListViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string IcaoIdentifier { get; set; }

        public int Elevation { get; set; }
    }
}
