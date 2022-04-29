using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigiAviator.Core.Models
{
    public class LogbookViewModel
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }

        public IList<FlightListViewModel> Flights { get; set; } = new List<FlightListViewModel>();

        public string HolderId { get; set; }
    }
}
