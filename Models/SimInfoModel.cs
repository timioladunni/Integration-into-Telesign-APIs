using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TelesignCodes.Models
{
    public class SimInfo
    {
        
    }
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
   

    public class PortingStatus
    {
        public bool ported { get; set; }
        public SimStatus status { get; set; }
        public string mnc_current { get; set; }
        public string mcc_current { get; set; }
    }

    public class SimStatus
    {
        
        public int code { get; set; }
        public string description { get; set; }
    }
    public class SimSwap
    {
        public SimStatus status { get; set; }
    }


    public class SimInfoModel
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
