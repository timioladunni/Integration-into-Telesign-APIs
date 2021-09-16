using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TelesignCodes.Interface;
using TelesignCodes.Models;
using Microsoft.Extensions.Configuration;
using System.Web.Mvc;

namespace TelesignCodes.Service
{
    public class TelesignService : ITelesignInterface
    {
        private readonly IConfiguration _authorization;
        public  TelesignService(IConfiguration authorization)
        {
            _authorization = authorization;
        }
        public TelesignScoreOutputModel Result(string number)
        {
            if (number == null)
            {
                throw new Exception("Number is empty");
            }
            var key = _authorization["Key"];
            var contentType = _authorization["wwwFormContent"];
            var account_lifecycle_event = _authorization["account_lifecycle_event"];
            var request_risk_insights = _authorization["request_risk_insights"];
            var client = new RestClient($"https://rest-ww.telesign.com/v1/score/{number}");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", contentType);
            request.AddHeader("Authorization", key);
            request.AddParameter("account_lifecycle_event", account_lifecycle_event);
            request.AddParameter("request_risk_insights", request_risk_insights);
            IRestResponse response = client.Execute(request);
            var res = response.Content;
            if (res.Contains("\"Invalid Request\""))
            {
                throw new Exception("Invalid Request");
            }
            Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(res);
            var a2p = myDeserializedClass.risk_insights.a2p.ToList();
            var p2p = myDeserializedClass.risk_insights.p2p.ToList();
            var category = myDeserializedClass.risk_insights.category.ToList();
            var numbertype = myDeserializedClass.risk_insights.number_type.ToList();
            var ip = myDeserializedClass.risk_insights.ip.ToList();
            var email = myDeserializedClass.risk_insights.email.ToList();
            TelesignCodeInfoContext tell = new TelesignCodeInfoContext();
            TelesignScoreOutputModel result = new TelesignScoreOutputModel();
            result.A2p = tell.TelesignInsights.Where(t => a2p.Contains(t.Code))
            .Select(t=> new {t.Code,t.Name,t.Meaning,t.RiskSignal,t.TrustSignal});
            result.P2p = tell.TelesignInsights.Where(t => p2p.Contains(t.Code))
            .Select(t => new { t.Code, t.Name, t.Meaning, t.RiskSignal, t.TrustSignal });
            result.Category = tell.TelesignInsights.Where(t => category.Contains(t.Code))
            .Select(t => new { t.Code, t.Name, t.Meaning, t.RiskSignal, t.TrustSignal });
            result.NumberType = tell.TelesignInsights.Where(t => numbertype.Contains(t.Code));
            result.IP = tell.TelesignInsights.Where(t => ip.Contains(t.Code))
            .Select(t => new { t.Code, t.Name, t.Meaning, t.RiskSignal, t.TrustSignal });
            result.Email = tell.TelesignInsights.Where(t => email.Contains(t.Code))
            .Select(t => new { t.Code, t.Name, t.Meaning, t.RiskSignal, t.TrustSignal });
            result.county = myDeserializedClass.location.county ?? "";
            result.city = myDeserializedClass.location.city ?? "";
            result.state = myDeserializedClass.location.state ?? "";
            result.zip = myDeserializedClass.location.zip ?? "";
            Coordinates coordinates = new Coordinates();
            coordinates.latitude = myDeserializedClass.location.coordinates.latitude ?? "";
            coordinates.longitude = myDeserializedClass.location.coordinates.longitude ?? "";
            result.coordinates = coordinates;
            Country country = new Country();
            country.name = myDeserializedClass.location.country.name ?? "";
            country.iso2 = myDeserializedClass.location.country.iso2 ?? "";
            country.iso3 = myDeserializedClass.location.country.iso3 ?? "";
            result.country = country;
            Models.TimeZone timeZone = new Models.TimeZone();
            timeZone.name = myDeserializedClass.location.time_zone.name ?? "";
            timeZone.utc_offset_min = myDeserializedClass.location.time_zone.utc_offset_min ?? "";
            timeZone.utc_offset_max = myDeserializedClass.location.time_zone.utc_offset_max ?? "";
            result.time_zone = timeZone;
            result.reference_id = myDeserializedClass.reference_id ?? "";
            result.external_id = myDeserializedClass.external_id ?? "";
            Status status = new Status();
            status.updated_on = myDeserializedClass.status.updated_on;
            status.code = myDeserializedClass.status.code;
            status.description = myDeserializedClass.status.description ?? "";
            result.status = status;
            Numbering numbering = new Numbering();
            numbering.cleansing = myDeserializedClass.numbering.cleansing;
            numbering.cleansing.call.cleansed_code = myDeserializedClass.numbering.cleansing.call.cleansed_code;
            numbering.cleansing.call.country_code = myDeserializedClass.numbering.cleansing.call.country_code ?? "";
            numbering.cleansing.call.max_length = myDeserializedClass.numbering.cleansing.call.max_length;
            numbering.cleansing.call.min_length = myDeserializedClass.numbering.cleansing.call.min_length;
            numbering.cleansing.call.phone_number = myDeserializedClass.numbering.cleansing.call.phone_number ?? "";
            numbering.cleansing.sms.phone_number = myDeserializedClass.numbering.cleansing.sms.phone_number ?? "";
            numbering.cleansing.sms.cleansed_code = myDeserializedClass.numbering.cleansing.sms.cleansed_code;
            numbering.cleansing.sms.country_code = myDeserializedClass.numbering.cleansing.sms.country_code ?? "";
            numbering.cleansing.sms.max_length = myDeserializedClass.numbering.cleansing.sms.max_length;
            numbering.cleansing.sms.min_length = myDeserializedClass.numbering.cleansing.sms.min_length;
            numbering.original = myDeserializedClass.numbering.original;
            numbering.original.complete_phone_number = myDeserializedClass.numbering.original.complete_phone_number ?? "";
            numbering.original.country_code = myDeserializedClass.numbering.original.country_code ?? "";
            numbering.original.phone_number = myDeserializedClass.numbering.original.phone_number ?? "";
            result.numbering = numbering;
            PhoneType phoneType = new PhoneType();
            phoneType.code = myDeserializedClass.phone_type.code ?? "";
            phoneType.description = myDeserializedClass.phone_type.description ?? "";
            result.phone_type = phoneType;
            Blocklisting blocklisting = new Blocklisting();
            blocklisting.blocked = myDeserializedClass.blocklisting.blocked;
            blocklisting.block_code = myDeserializedClass.blocklisting.block_code;
            blocklisting.block_description = myDeserializedClass.blocklisting.block_description ?? "";
            result.blocklisting = blocklisting;
            Risk risk = new Risk();
            risk.level = myDeserializedClass.risk.level ?? "";
            risk.score = myDeserializedClass.risk.score;
            risk.recommendation = myDeserializedClass.risk.recommendation ?? "";
            result.risk = risk;
            Carrier carrier = new Carrier();
            carrier.name = myDeserializedClass.carrier.name ?? "";
            result.carrier = carrier;
            return result;
        }

        public SimInfoOutputModel SimInfo(string number)
        {
            if (number == null)
            {
                throw new Exception("Number is empty");
            }
            var key = _authorization["Key"];
            var contentType = _authorization["JsonContent"];
            var body = @"{""addons"": { ""porting_status"": {},""sim_swap"": {} }}";
            var client = new RestClient($"https://rest-ww.telesign.com/v1/phoneid/{number}");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", contentType);
            request.AddHeader("Authorization", key);
            request.AddParameter("application/json", body, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            var res = response.Content;
            if (res.Contains("\"Invalid Request\""))
            {
                throw new Exception("Invalid Request");
            }
            SimInfoModel myDeserializedClass = JsonConvert.DeserializeObject<SimInfoModel>(res);
            SimInfoOutputModel outputModel = new SimInfoOutputModel();
            Status status = new Status();
            status.updated_on = myDeserializedClass.status.updated_on;
            status.code = myDeserializedClass.status.code;
            status.description = myDeserializedClass.status.description ?? "";
            outputModel.status = status;
            PhoneType phoneType = new PhoneType();
            phoneType = myDeserializedClass.phone_type;
            phoneType.code = myDeserializedClass.phone_type.code ?? "";
            phoneType.description = myDeserializedClass.phone_type.description ?? "";
            outputModel.phone_type = phoneType;
            PortingStatus portingStatus = new PortingStatus();
            portingStatus.ported = myDeserializedClass.porting_status.ported;
            portingStatus.status = myDeserializedClass.porting_status.status;
            portingStatus.status.code = myDeserializedClass.porting_status.status.code;
            portingStatus.status.description = myDeserializedClass.porting_status.status.description ?? ""; ;
            portingStatus.mcc_current = myDeserializedClass.porting_status.mcc_current ?? "";
            portingStatus.mnc_current = myDeserializedClass.porting_status.mnc_current ?? "";
            outputModel.porting_status = portingStatus;
            Numbering numbering = new Numbering();
            numbering.original = myDeserializedClass.numbering.original;
            numbering.original.phone_number = myDeserializedClass.numbering.original.phone_number ?? "";
            numbering.original.complete_phone_number = myDeserializedClass.numbering.original.complete_phone_number ?? "";
            numbering.cleansing = myDeserializedClass.numbering.cleansing;
            numbering.cleansing.sms = myDeserializedClass.numbering.cleansing.sms;
            numbering.cleansing.sms.phone_number = myDeserializedClass.numbering.cleansing.sms.phone_number ?? "";
            numbering.cleansing.sms.min_length = myDeserializedClass.numbering.cleansing.sms.min_length;
            numbering.cleansing.sms.cleansed_code = myDeserializedClass.numbering.cleansing.sms.cleansed_code;
            numbering.cleansing.sms.max_length = myDeserializedClass.numbering.cleansing.sms.max_length;
            numbering.cleansing.sms.country_code = myDeserializedClass.numbering.cleansing.sms.country_code ?? "";
            numbering.cleansing.call = myDeserializedClass.numbering.cleansing.call;
            numbering.cleansing.call.phone_number = myDeserializedClass.numbering.cleansing.call.phone_number ?? "";
            numbering.cleansing.call.min_length = myDeserializedClass.numbering.cleansing.call.min_length;
            numbering.cleansing.call.cleansed_code = myDeserializedClass.numbering.cleansing.call.cleansed_code;
            numbering.cleansing.call.max_length = myDeserializedClass.numbering.cleansing.call.max_length;
            numbering.cleansing.call.country_code = myDeserializedClass.numbering.cleansing.call.country_code ?? "";
            outputModel.numbering = numbering;
            Blocklisting blocklisting = new Blocklisting();
            blocklisting.block_code = myDeserializedClass.blocklisting.block_code;
            blocklisting.block_description = myDeserializedClass.blocklisting.block_description ?? "";
            blocklisting.blocked = myDeserializedClass.blocklisting.blocked;
            outputModel.blocklisting = blocklisting;
            SimSwap simSwap = new SimSwap();
            simSwap.status = myDeserializedClass.sim_swap.status;
            simSwap.status.code = myDeserializedClass.sim_swap.status.code;
            simSwap.status.description = myDeserializedClass.sim_swap.status.description ?? "";
            outputModel.sim_swap = simSwap;
            Carrier carrier = new Carrier();
            carrier.name = myDeserializedClass.carrier.name ?? "";
            outputModel.carrier = carrier;
            outputModel.reference_id = myDeserializedClass.reference_id ?? "";
            outputModel.external_id = myDeserializedClass.external_id ?? "";
            Location location = new Location();
            location.city = myDeserializedClass.location.city ?? "";
            location.zip = myDeserializedClass.location.zip;
            location.country = myDeserializedClass.location.country;
            location.country.iso3 = myDeserializedClass.location.country.iso3 ?? "";
            location.country.iso2 = myDeserializedClass.location.country.iso2 ?? "";
            location.country.name = myDeserializedClass.location.country.name ?? "";
            location.time_zone = myDeserializedClass.location.time_zone;
            location.time_zone.utc_offset_min = myDeserializedClass.location.time_zone.utc_offset_min ?? "";
            location.time_zone.name = myDeserializedClass.location.time_zone.name;
            location.time_zone.utc_offset_max = myDeserializedClass.location.time_zone.utc_offset_max ?? "";
            location.coordinates = myDeserializedClass.location.coordinates;
            location.coordinates.latitude = myDeserializedClass.location.coordinates.latitude;
            location.coordinates.longitude = myDeserializedClass.location.coordinates.longitude;
            location.metro_code = myDeserializedClass.location.metro_code;
            location.county = myDeserializedClass.location.county;
            location.state = myDeserializedClass.location.state;
            outputModel.location = location;
            return outputModel;

        }

        public SMSOutputModel SMS(SMSInputModel model)
        {
            if (model == null)
            {
                throw new Exception("Model is empty");
            }
            var key = _authorization["Key"];
            var contentType = _authorization["wwwFormContent"];
            var client = new RestClient("https://rest-ww.telesign.com/v1/messaging");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", contentType);
            request.AddHeader("Authorization", key);
            request.AddParameter("message", model.message);
            request.AddParameter("message_type", model.message_type);
            request.AddParameter("phone_number", model.phone_number);
            IRestResponse response = client.Execute(request);
            var res = response.Content;
            if (res.Contains("\"Invalid Request\""))
            {
                throw new Exception("Invalid Request");
            }
            SMSModel myDeserializedClass = JsonConvert.DeserializeObject<SMSModel>(res);
            SMSOutputModel sMSOutputModel = new SMSOutputModel();
            sMSOutputModel.reference_id = myDeserializedClass.reference_id ?? "";
            sMSOutputModel.status = myDeserializedClass.status;
            sMSOutputModel.status.code = myDeserializedClass.status.code;
            sMSOutputModel.status.description = myDeserializedClass.status.description ?? "";
            sMSOutputModel.external_id = myDeserializedClass.external_id ?? "";
            return sMSOutputModel;
        }

        public SMSVerifyOutputModel SMSVerify(SMSVerifyInputModel model)
        {
            var key = _authorization["Key"];
            var contentType = _authorization["wwwFormContent"];
            var client = new RestClient("https://rest-ww.telesign.com/v1/verify/sms");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", contentType);
            request.AddHeader("Authorization", key);
            request.AddParameter("phone_number", model.phone_number);
            request.AddParameter("verify_code", model.verify_code);
            request.AddParameter("template", model.template);
            IRestResponse response = client.Execute(request);
            var res = response.Content;
            if (res.Contains("\"Invalid Request\""))
            {
                throw new Exception("Invalid Request");
            }
            SMSVerifyModel myDeserializedClass = JsonConvert.DeserializeObject<SMSVerifyModel>(res);
            SMSVerifyOutputModel outputModel = new SMSVerifyOutputModel();
            outputModel.reference_id = myDeserializedClass.reference_id ?? "";
            outputModel.sub_resource = myDeserializedClass.sub_resource ?? "";
            outputModel.errors = myDeserializedClass.errors.ToList();
            outputModel.verify = myDeserializedClass.verify;
            outputModel.verify.code_state = myDeserializedClass.verify.code_state ?? "";
            outputModel.verify.code_entered = myDeserializedClass.verify.code_entered ?? "";
            Status status = new Status();
            status.updated_on = myDeserializedClass.status.updated_on;
            status.code = myDeserializedClass.status.code;
            status.description = myDeserializedClass.status.description ?? "";
            outputModel.status = status;
            return outputModel;
        }
    }
}
