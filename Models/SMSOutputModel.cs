using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static TelesignCodes.Models.SMSModel;

namespace TelesignCodes.Models
{
    public class SMSOutputModel
    {
        public string reference_id { get; set; }
        public SMSStatus status { get; set; }
        public object external_id { get; set; }
    }
}
