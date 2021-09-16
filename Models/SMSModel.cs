using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TelesignCodes.Models
{
    
        // SMSModel myDeserializedClass = JsonConvert.DeserializeObject<SMSModel>(myJsonResponse); 
        public class SMSStatus
        {
            public int code { get; set; }
            public string description { get; set; }
        }

        public class SMSModel
        {
            public string reference_id { get; set; }
            public SMSStatus status { get; set; }
            public object external_id { get; set; }
        }

        public class SMSInputModel
        {
             public string message { get; set; }
             public string message_type { get; set; }
             public string phone_number { get; set; }
        }
    
}
