using MonkeyFly.Core;
using MonkeyFly.MES.BasicService;
using MonkeyFly.MES.FilterAttributes;
using MonkeyFly.MES.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace MonkeyFly.MES.Webapi
{
    /// <summary>
    /// 
    /// </summary>
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class UtilController : ApiController
    {
        /// <summary>
        /// UtilAPI
        /// </summary>
        [HttpGet]
        public void UtilAPI() { }

        /// <summary>
        /// 获取多个参数列表的json(多用于下拉框)
        /// SAM 2017年4月28日11:22:24
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="typeIDs"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public JObject GetParameters(string Token, string typeIDs)
        {
            if (string.IsNullOrWhiteSpace(typeIDs)) return null;
            else
            {
                string[] TypeIDArray = typeIDs.Split(',');
                for (int i = 0; i < TypeIDArray.Length; i++)
                {
                    TypeIDArray[i] = Framework.SystemID + TypeIDArray[i];
                }
                typeIDs = string.Join("','", TypeIDArray);
            }
            JObject result = new JObject();
            IList<Hashtable> data = UtilBussinessService.GetParameters(Token, typeIDs);
            string item = null;
            foreach (string p in typeIDs.Split(','))
            {
                item = p.Replace("'", "");
                if (data.Where(w => w.ContainsValue(item)).Count() > 0)
                    result.Add("PT" + item.Substring(5), JsonConvert.DeserializeObject<JArray>(JsonConvert.SerializeObject(data.Where(w => w.ContainsValue(item)).ToList())));
                else
                    result.Add("PT" + item.Substring(5), JsonConvert.DeserializeObject<JArray>("[]"));
            }
            return result;
        }

        /// <summary>
        /// 根据单据类别流水号获取自动编号
        /// SAM 2017年7月30日21:31:16
        /// {AutoNumber=自动给号，DocumentAutoNumberID=给号记录的流水号 }
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="DocumentID"></param>
        /// <param name="Date"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object GetDocumentAutoNumber(string Token, string DocumentID, string Date = null)
        {
            DataLogerService.writeURL(Token, "DocumentID:" + DocumentID);
            return UtilBussinessService.GetDocumentAutoNumber(UtilBussinessService.detoken(Token), DocumentID, Date);
        }

        /// <summary>
        /// 根据批号类别ID获取自动批号编号
        /// Tom 2017年6月27日20点43分
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="LotClassID"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public string GetAutoLotNumber(string Token, string LotClassID)
        {
            DataLogerService.writeURL(Token, "LotClassID:" + LotClassID);
            return UtilBussinessService.GetAutoLotNumber(UtilBussinessService.detoken(Token), LotClassID);
        }

        /// <summary>
        /// 获取当前登录用户的个人信息
        /// SAM 2017年6月14日00:40:12
        /// </summary>
        /// <param name="Token"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public Hashtable GetUser(string Token)
        {
            DataLogerService.writeURL(Token, null);
            return UtilBussinessService.GetUser(Token);
        }

        /// <summary>
        /// 根据制品制程流水号获取相关信息
        /// SAM 2017年6月23日10:29:32
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="ItemProcessID"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public Hashtable GetItemProcess(string Token, string ItemProcessID)
        {
            DataLogerService.writeURL(Token, ItemProcessID);
            return UtilBussinessService.GetItemProcess(Token, ItemProcessID);
        }

        /// <summary>
        /// 根据附件流水号获取指定附件并其转到base64
        /// SAM 2017年8月2日16:01:01
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="AttachmentID"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public string ImgToBase64String(string Token, string AttachmentID)
        {
            DataLogerService.writeURL(Token, AttachmentID);
            return UtilBussinessService.ImgToBase64String(AttachmentID);
        }
        /// <summary>
        /// 获取登入者的角色
        /// </summary>
        /// Mouse 2017年9月5日10:30:07
        /// <param name="Token"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public object GetRoles(string Token, string UserID)
        {
            DataLogerService.writeURL(Token, UserID);
            return UtilBussinessService.GetRoles(UserID);
        }

        /// <summary>
        /// 判断当前登录用户是否为品管
        /// SAM 2017年6月14日00:40:12
        /// </summary>
        /// <param name="Token"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public bool CheckUserRole(string Token)
        {
            DataLogerService.writeURL(Token, null);
            return UtilBussinessService.CheckUserRole(Token);
        }

        /// <summary>
        /// 根据代号，精准查询单个料品信息
        /// SAM 2017年9月23日15:18:18
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="Code"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public Hashtable GetItem(string Token, string Code)
        {
            DataLogerService.writeURL(Token, null);
            return UtilBussinessService.GetItem(Code);
        }

        /// <summary>
        /// 下载txt
        /// SAM 2017年10月17日17:55:41
        /// 专门用于下载导入错误报告
        /// </summary>
        /// <param name="Name">文件名</param>
        /// <param name="Token"></param>
        [HttpGet]
        public void ImportDownload(string Token, String Name)
        {
            try
            {
                string strLogFolderPath = AppDomain.CurrentDomain.BaseDirectory + "ImportWriteErrLog";
                string strLogPath = Path.Combine(strLogFolderPath, Name + ".txt");
                FileStream fs = new FileStream(strLogPath, FileMode.Open);
                byte[] bytes = new byte[(int)fs.Length];
                fs.Read(bytes, 0, bytes.Length);
                fs.Close();
                HttpContext.Current.Response.Charset = "UTF-8";
                HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + HttpContext.Current.Server.UrlEncode(Name) + ".txt");
                byte[] publicCustomerListExcelBytes = bytes;
                HttpContext.Current.Response.BinaryWrite(publicCustomerListExcelBytes);
                HttpContext.Current.Response.Flush();
                HttpContext.Current.Response.End();
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
            }
        }


        /// <summary>
        /// 系统设置--上传底图获取列表
        /// Alvin 2017年10月19日10:30:07
        /// 附件表  ObjectID = "10039007121300000A"
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet]
        [Authenticate]
        public Object GetBaseMapList(string Token, int page = 1, int rows = 10)
        {
            DataLogerService.writeURL(Token, null);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "上传底图", null, "查看", null);
            return UtilBussinessService.GetBaseMapList(Token, page, rows);

        }

        /// <summary>
        /// 系统设置--BaseMap 单条删除数据
        /// Alvin 2017年10月19日11:09:24
        /// ObjectID = "10039007121300000A"
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public Object MapDelete(JObject request)
        {
            string token = request.Value<string>("Token");
            DataLogerService.writeURL(token, request.ToString());
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(token), "上传底图", null, "删除", null);
            return UtilBussinessService.MapDelete(request);
        }

        /// <summary>
        /// 系统设置--BaseMap 图片上传
        /// Alvin 2017年10月19日14:29:51
        /// 附件表 隶属对象ObjectID
        /// ObjectID = "10039007121300000A"
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [FileAuthenticate]
        public object UploadMap()
        {
            try
            {
                string Token = HttpContext.Current.Request.Form["Token"];
                string comment = HttpContext.Current.Request.Form["Comments"];
                HttpPostedFile file = System.Web.HttpContext.Current.Request.Files["file"];
                if (file == null)
                {
                    return new { status = "410", msg = "文件为空！" };
                }
               
               
                UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "上传底图", null, "导入", null);

                return UtilBussinessService.UploadMap(Token, file, comment);

            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return new { status = "440", msg = "异常已捕获！请联系研发人员查询问题！" };
            }
        }



        /// <summary>
        /// 登录界面获取图片
        /// Alvin 2017年10月19日18:57:30
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IList<Hashtable> GetMap()
        {
            return UtilBussinessService.GetMap();
        }


    }
}
