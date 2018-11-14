using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace MonkeyFly.MES.Webapi
{
    /// <summary>
    /// 智能产能模块
    /// </summary>
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class IntelligentProCapacityController : ApiController
    {
    }
}
