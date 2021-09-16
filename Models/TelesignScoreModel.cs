using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TelesignCodes.Models
{
    public class TelesignScoreModel
    {
    }
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class Status
    {
        public DateTime updated_on { get; set; }
        public int code { get; set; }
        public string description { get; set; }
    }

    public class Original
    {
        public string complete_phone_number { get; set; }
        public string country_code { get; set; }
        public string phone_number { get; set; }
    }

    public class Call
    {
        public string country_code { get; set; }
        public string phone_number { get; set; }
        public int cleansed_code { get; set; }
        public int min_length { get; set; }
        public int max_length { get; set; }
    }

    public class Sms
    {
        public string country_code { get; set; }
        public string phone_number { get; set; }
        public int cleansed_code { get; set; }
        public int min_length { get; set; }
        public int max_length { get; set; }
    }

    public class Cleansing
    {
        public Call call { get; set; }
        public Sms sms { get; set; }
    }

    public class Numbering
    {
        public Original original { get; set; }
        public Cleansing cleansing { get; set; }
    }

    public class RiskInsights
    {
        public int status { get; set; }
        public List<long?> category { get; set; }
        public List<long?> a2p { get; set; }
        public List<long?> p2p { get; set; }
        public List<long?> number_type { get; set; }
        public List<long?> ip { get; set; }
        public List<long?> email { get; set; }
    }

    public class PhoneType
    {
        public string code { get; set; }
        public string description { get; set; }
    }

    public class Country
    {
        public string name { get; set; }
        public string iso2 { get; set; }
        public string iso3 { get; set; }
    }

    public class Coordinates
    {
        public object latitude { get; set; }
        public object longitude { get; set; }
    }

    public class TimeZone
    {
        public object name { get; set; }
        public string utc_offset_min { get; set; }
        public string utc_offset_max { get; set; }
    }

    public class Location
    {
        public string city { get; set; }
        public object state { get; set; }
        public object zip { get; set; }
        public object metro_code { get; set; }
        public object county { get; set; }
        public Country country { get; set; }
        public Coordinates coordinates { get; set; }
        public TimeZone time_zone { get; set; }
    }

    public class Carrier
    {
        public string name { get; set; }
    }

    public class Blocklisting
    {
        public bool blocked { get; set; }
        public int block_code { get; set; }
        public string block_description { get; set; }
    }

    public class Risk
    {
        public string level { get; set; }
        public string recommendation { get; set; }
        public int score { get; set; }
    }

    public class Root
    {
        public string reference_id { get; set; }
        public object external_id { get; set; }
        public Status status { get; set; }
        public Numbering numbering { get; set; }
        public RiskInsights risk_insights { get; set; }
        public PhoneType phone_type { get; set; }
        public Location location { get; set; }
        public Carrier carrier { get; set; }
        public Blocklisting blocklisting { get; set; }
        public Risk risk { get; set; }
       
    }


}
