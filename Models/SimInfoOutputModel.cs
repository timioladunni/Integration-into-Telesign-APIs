using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TelesignCodes.Models
{
    public class SimInfoOutputModel
    {
        public Status status { get; set; }
        public PhoneType phone_type { get; set; }
        public PortingStatus porting_status { get; set; }
        public Numbering numbering { get; set; }
        public Blocklisting blocklisting { get; set; }
        public SimSwap sim_swap { get; set; }
        public Carrier carrier { get; set; }
        public string reference_id { get; set; }
        public object external_id { get; set; }
        public Location location { get; set; }
    }
}
