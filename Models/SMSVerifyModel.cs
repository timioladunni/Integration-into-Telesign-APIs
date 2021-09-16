using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TelesignCodes.Models
{


    // SMSVerifyInputModel myDeserializedClass = JsonConvert.DeserializeObject<SMSVerifyInputModel>(myJsonResponse); 
        public class Verify
        {
            public string code_state { get; set; }
            public string code_entered { get; set; }
        }

        

        public class SMSVerifyModel
        {
            public string reference_id { get; set; }
            public string sub_resource { get; set; }
            public List<object> errors { get; set; }
            public Verify verify { get; set; }
            public Status status { get; set; }
        }

        public class SMSVerifyInputModel
        {
            public string phone_number { get; set; }
            public string verify_code { get; set; }
            public string template { get; set; }
        }
    
}
