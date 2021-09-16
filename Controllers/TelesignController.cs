using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TelesignCodes.Models;
using RestSharp;
using Newtonsoft.Json;
using TelesignCodes.Interface;
using System.Net.Mail;
using System.Net;
using Microsoft.Extensions.Options;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TelesignCodes.Controllers
{
    [Route("api/test")]
    [ApiController]
    public class TelesignController : ControllerBase
    {
        private ITelesignInterface _interface;
        private readonly AppSettings _appSettings;
        public TelesignController(ITelesignInterface telesignInterface, IOptions<AppSettings> options)
        {
            _interface = telesignInterface;
            _appSettings = options.Value;
        }
       
       
        
        [HttpGet("{number}")]
        public ActionResult <TelesignScoreOutputModel> Result(string number)
        {
            try
            {
                var accessName = _appSettings.AccessName;
                if (!(number == null))
                {
                    var result = _interface.Result(number);
                    return Ok(result);
                }
            }
            catch (Exception et)
            {
                var a = Convert.ToString(new Exception(et.Message));
                return UnprocessableEntity(a);
            }
            return NoContent();
        }
       
        [HttpPost("sms/verify")]
        public ActionResult SMSVerify([FromBody]SMSVerifyInputModel model)
        {
            try
            {
                if (!(ModelState.IsValid))
                {
                    return UnprocessableEntity("API body not valid");
                }
                else
                {
                    var result = _interface.SMSVerify(model);
                    return Ok(result);
                }
            }
            catch (Exception et)
            {
                var a = Convert.ToString(new Exception(et.Message));
                return UnprocessableEntity(a);
            }
            
        }

        [HttpGet("simInfo/{number}")]
        public ActionResult<SimInfoOutputModel> SimInformation(string number)
        {
            try
            {
                if (!(number == null))
                {
                    var result = _interface.SimInfo(number);
                    return Ok(result);
                }
            }
            catch (Exception et)
            {
                var a = Convert.ToString(new Exception(et.Message));
                return UnprocessableEntity(a);
            }
            return NoContent();
        }

        [HttpPost("sms")]
        public ActionResult<SimInfoOutputModel> SMS([FromBody] SMSInputModel  model)
        {
            try
            {
                if (!(ModelState.IsValid))
                {
                    return UnprocessableEntity("API body not valid");
                }
                else
                {
                    var result = _interface.SMS(model);
                    return Ok(result);
                }
            }
            catch (Exception et)
            {
                var a = Convert.ToString(new Exception(et.Message));
                return UnprocessableEntity(a);
            }
            
        }
    }
}
