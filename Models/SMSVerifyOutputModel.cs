using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TelesignCodes.Models
{
    public class SMSVerifyOutputModel
    {
     
            public string reference_id { get; set; }
            public string sub_resource { get; set; }
            public List<object> errors { get; set; }
            public Verify verify { get; set; }
            public Status status { get; set; }
        
    }
}
