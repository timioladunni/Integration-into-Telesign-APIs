using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace TelesignCodes.Models
{
    public partial class TelesignInsight
    {
        
        public long Id { get; set; }
        public long? Code { get; set; }
        public string Name { get; set; }
        public string Meaning { get; set; }
        public bool? RiskSignal { get; set; }
        public bool? TrustSignal { get; set; }
    }
}
