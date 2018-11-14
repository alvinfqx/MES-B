using MonkeyFly.Core;
using MonkeyFly.MES.BasicService;
using MonkeyFly.Request;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;

namespace MonkeyFly.MES.FilterAttributes
{
    /// <summary>
    /// 普通函数验证
    /// SAM 2017年7月30日12:33:11
    /// </summary>
    public class AuthenticateAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            String token = null;
            try
            {
                //取出所有参数判定是否有Token值
                Dictionary<string, object> actionargument = actionContext.ActionArguments;
                foreach (KeyValuePair<string, object> arg in actionargument)
                {
                    string key = arg.Key;
                    if (arg.Value.GetType().FullName.Equals("System.String") && arg.Key.Equals("Token"))
                    {
                        token = (String)arg.Value;
                        break;
                    }
                    if (arg.Value.GetType().FullName.EndsWith("JObject"))
                    {
                        JObject request = (JObject)arg.Value;
                        token = request.Value<string>("Token");
                        if (!String.IsNullOrEmpty(token))
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.OK, new { status = "404", msg = ex.ToString() });
            }
            if (String.IsNullOrEmpty(token))
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.OK, new { status = "404", msg = CodeService.C404 });
            }
            else
            {
                //检查token里面的时间和ip是否与当前一致，或已经过期
                //bool ipFilter = false;
                //bool timeFilter = false;
                //token = token.Replace(" ", "+");
                //string detoken = Utils.EncryptHelper.DESDecrypt(token);
                //string lastIP = detoken.Substring(detoken.IndexOf('-') + 1, (detoken.LastIndexOf('-') - detoken.IndexOf('-') - 1));
                //long timeTicks = long.Parse(detoken.Substring(detoken.LastIndexOf('-') + 1));
                //String thisIP = RequestHelper.ClientIP;
                ////if (lastIP.Equals(thisIP))
                //ipFilter = true;
                //TimeSpan ts = new TimeSpan(DateTime.Now.Ticks - timeTicks);

                ////定义Token的有效期12小时
                //if (ts.Hours < 12)
                //    timeFilter = true;

                //if (ipFilter != true || timeFilter != true)
                //    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.OK, new { status = "403", msg = CodeService.C403 });

                token = token.Replace(" ", "+");
                bool checkresult = Framework.checkCurrentUser(token);
                if (!checkresult)
                    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.OK, new { status = "403", msg = CodeService.C403 });
                //base.OnActionExecuting(actionContext);
            }

            base.OnActionExecuting(actionContext);
        }
    }

    /// <summary>
    /// 导入函数验证
    /// SAM 2017年7月30日12:33:21
    /// </summary>
    public class FileAuthenticateAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            String token = System.Web.HttpContext.Current.Request.Form["Token"];
            if (String.IsNullOrEmpty(token))
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.OK, new { status = "404", msg = CodeService.C404 });
            }
            else
            {
                //检查token里面的时间和ip是否与当前一致，或已经过期
                /*
                bool ipFilter = false;
                bool timeFilter = false;
                string detoken = MonkeyFly.Utils.EncryptHelper.DESDecrypt(token);
                string lastIP = detoken.Substring(detoken.IndexOf('-') + 1, (detoken.LastIndexOf('-') - detoken.IndexOf('-') - 1));
                long timeTicks = long.Parse(detoken.Substring(detoken.LastIndexOf('-') + 1));
                String thisIP = RequestHelper.ClientIP;
                //if (lastIP.Equals(thisIP))
                ipFilter = true;
                TimeSpan ts = new TimeSpan(DateTime.Now.Ticks - timeTicks);

                //定义Token的有效期12小时
                if (ts.Hours < 12)
                    timeFilter = true;

                if (ipFilter != true || timeFilter != true)
                    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.OK, new { status = "403", msg = CodeService.C403 });
                    */
                token = token.Replace(" ", "+");
                bool checkresult = Framework.checkCurrentUser(token);
                if (!checkresult)
                    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.OK, new { status = "403", msg = CodeService.C403 });
            }

            base.OnActionExecuting(actionContext);
        }
    }
}
