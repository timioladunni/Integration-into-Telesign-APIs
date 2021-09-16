using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TelesignCodes.Models
{
    public class AppSettings
    {
        public string Authentication { get; set; }
        public int ValidityPeriod { get; set; }
        public string AccessName { get; set; }
        public string SaveToThirdParty { get; set; }
        public string ClientLogsUrl { get; set; }
        public string GetTransaction { get; set; }
        public string CustomerIP { get; set; }
        public bool IsDemo { get; set; }
    }
}
