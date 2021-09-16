using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TelesignCodes.Models
{
    public class TelesignScoreOutputModel
    {

        public object reference_id { get; set; }
        public object external_id { get; set; }
        public Status status { get; set; }
        public Carrier carrier { get; set; }
        public Numbering numbering { get; set; }
        public PhoneType phone_type { get; set; }
        public string city { get; set; }
        public object state { get; set; }
        public object zip { get; set; }
        public object metro_code { get; set; }
        public object county { get; set; }
        public Country country { get; set; }
        public Coordinates coordinates { get; set; }
        public Models.TimeZone time_zone { get; set; }

        public object A2p { get; set; }
        public object P2p { get; set; }
        public object Category { get; set; }
        public object NumberType { get; set; }
        public object IP { get; set; }
        public object Email { get; set; }
        public Blocklisting blocklisting { get; set; }
        public Risk risk { get; set; }
    }
    
}
