using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TelesignCodes.Models;

namespace TelesignCodes.Interface
{
    public interface ITelesignInterface
    {
        TelesignScoreOutputModel Result(string number);
        SimInfoOutputModel SimInfo(string number);
        SMSOutputModel SMS(SMSInputModel model);
        SMSVerifyOutputModel SMSVerify(SMSVerifyInputModel model);
    }
}
