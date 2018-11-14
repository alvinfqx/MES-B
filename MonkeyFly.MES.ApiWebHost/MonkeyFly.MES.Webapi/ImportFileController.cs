using MonkeyFly.Core;
using MonkeyFly.MES.BasicService;
using MonkeyFly.MES.FilterAttributes;
using MonkeyFly.MES.Services;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Drawing;

namespace MonkeyFly.MES.Webapi
{
    /// <summary>
    /// 导入文件专用控制器
    /// SAM 2017年6月6日21:52:13
    /// </summary>
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ImportFileController : ApiController
    {
        /// <summary>
        /// ImportFileAPI
        /// SAM 2017年6月6日21:52:13
        /// </summary>
        [HttpGet]
        public void ImportFileAPI() { }

        /// <summary>
        /// 导入厂别
        /// SAM 2017年4月26日16:36:26
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [FileAuthenticate]
        public object PlantImport()
        {
            try
            {
                string Token = HttpContext.Current.Request.Form["Token"];
                HttpPostedFile file = HttpContext.Current.Request.Files["file"];
                if (file == null)
                    return new { status = "440", msg = "文件为空！" };
                string filePath = AppDomain.CurrentDomain.BaseDirectory + "Upload/";
                string filename = Path.GetFileName(file.FileName);
                file.SaveAs(Path.Combine(filePath, filename));
                UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "厂别主档", null, "导入", null);
                return ImportFileBussinessService.plantImport(Token, Path.Combine(filePath, filename));
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return new { status = "440", msg = "异常已捕获！请联系研发人员查询问题！" };
            }
        }

        /// <summary>
        /// 导入厂区
        /// SAM 2017年4月26日16:37:13
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [FileAuthenticate]
        public object PlantAreaImport()
        {
            try
            {
                string Token = HttpContext.Current.Request.Form["Token"];
                HttpPostedFile file = HttpContext.Current.Request.Files["file"];
                if (file == null)
                    return new { status = "440", msg = "文件为空！" };
                string filePath = AppDomain.CurrentDomain.BaseDirectory + "Upload/";
                string filename = Path.GetFileName(file.FileName);
                file.SaveAs(Path.Combine(filePath, filename));
                UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "厂区主档", null, "导入", null);
                return ImportFileBussinessService.PlantAreaImport(Token, Path.Combine(filePath, filename));
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return new { status = "440", msg = "异常已捕获！请联系研发人员查询问题！" };
            }
        }

        /// <summary>
        /// 客户导入
        /// SAM 2017年4月27日10:34:30
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [FileAuthenticate]
        public object CustomerImport()
        {
            try
            {
                string Token = HttpContext.Current.Request.Form["Token"];
                HttpPostedFile file = HttpContext.Current.Request.Files["file"];
                if (file == null)
                    return new { status = "440", msg = "文件为空！" };
                string filePath = AppDomain.CurrentDomain.BaseDirectory + "Upload/";
                string filename = Path.GetFileName(file.FileName);
                file.SaveAs(Path.Combine(filePath, filename));
                UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "客户主档", null, "导入", null);
                return ImportFileBussinessService.CustomerImport(Token, Path.Combine(filePath, filename));
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return new { status = "440", msg = "异常已捕获！请联系研发人员查询问题！" };
            }
        }

        /// <summary>
        /// 厂商导入
        /// SAM 2017年4月27日11:45:12
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [FileAuthenticate]
        public object ManufacturerImport()
        {
            try
            {
                string Token = HttpContext.Current.Request.Form["Token"];
                HttpPostedFile file = HttpContext.Current.Request.Files["file"];
                if (file == null)
                    return new { status = "440", msg = "文件为空！" };
                string filePath = AppDomain.CurrentDomain.BaseDirectory + "Upload/";
                string filename = Path.GetFileName(file.FileName);
                file.SaveAs(Path.Combine(filePath, filename));
                UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "厂商主档", null, "导入", null);
                return ImportFileBussinessService.ManufacturerImport(Token, Path.Combine(filePath, filename));
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return new { status = "440", msg = "异常已捕获！请联系研发人员查询问题！" };
            }
        }

        /// <summary>
        /// 语序导入
        /// SAM 2017年5月3日11:06:50
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [FileAuthenticate]
        public object Lan00000Import()
        {
            try
            {
                string Token = HttpContext.Current.Request.Form["Token"];
                HttpPostedFile file = HttpContext.Current.Request.Files["file"];
                if (file == null)
                    return new { status = "440", msg = "文件为空！" };
                string filePath = AppDomain.CurrentDomain.BaseDirectory + "Upload/";
                string filename = Path.GetFileName(file.FileName);
                file.SaveAs(Path.Combine(filePath, filename));
                return ImportFileBussinessService.Lan00000Import(Token, Path.Combine(filePath, filename));
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return new { status = "440", msg = "异常已捕获！请联系研发人员查询问题！" };
            }
        }

        /// <summary>
        /// 分类群码导入
        /// Tom 2017年5月4日10:19:03
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [FileAuthenticate]
        public object Inf00009GroupCodeImport()
        {
            try
            {
                string Token = HttpContext.Current.Request.Form["Token"];
                HttpPostedFile file = HttpContext.Current.Request.Files["file"];
                if (file == null)
                    return new { status = "440", msg = "文件为空！" };

                string UserID = UtilBussinessService.detoken(Token);
                UtilBussinessService.WriteLog(UserID, "分类群码", null, "导入", null);
                return ImportFileBussinessService.Inf00009GroupCodeImport(UserID, file.FileName, file.InputStream);
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return new { status = "440", msg = "异常已捕获！请联系研发人员查询问题！" };
            }
        }

        /// <summary>
        /// 分类导入
        /// Tom 2017年5月4日10:24:54
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [FileAuthenticate]
        public object Inf00009ClassImport()
        {
            try
            {
                string Token = HttpContext.Current.Request.Form["Token"];
                HttpPostedFile file = HttpContext.Current.Request.Files["file"];
                if (file == null)
                    return new { status = "440", msg = "文件为空！" };
                string UserID = UtilBussinessService.detoken(Token);
                UtilBussinessService.WriteLog(UserID, "分类", null, "导入", null);
                return ImportFileBussinessService.Inf00009ClassImport(UserID, file.FileName, file.InputStream);
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return new { status = "440", msg = "异常已捕获！请联系研发人员查询问题！" };
            }
        }

        /// <summary>
        /// 单位导入
        /// Tom 2017年5月4日11:50:39
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [FileAuthenticate]
        public object Inf00011Import()
        {
            try
            {
                string Token = HttpContext.Current.Request.Form["Token"];
                HttpPostedFile file = HttpContext.Current.Request.Files["file"];
                if (file == null)
                    return new { status = "440", msg = "文件为空！" };

                string UserID = UtilBussinessService.detoken(Token);
                UtilBussinessService.WriteLog(UserID, "单位", null, "导入", null);
                return ImportFileBussinessService.Inf00011Import(UserID, file.FileName, file.InputStream);

            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return new { status = "440", msg = "异常已捕获！请联系研发人员查询问题！" };
            }
        }


        /// <summary>
        /// 仓库导入
        /// SAM 2017年5月4日17:04:39
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [FileAuthenticate]
        public object WarehouseImport()
        {
            try
            {
                string Token = HttpContext.Current.Request.Form["Token"];
                HttpPostedFile file = HttpContext.Current.Request.Files["file"];
                if (file == null)
                    return new { status = "440", msg = "文件为空！" };
                string filePath = AppDomain.CurrentDomain.BaseDirectory + "Upload/";
                string filename = Path.GetFileName(file.FileName);
                file.SaveAs(Path.Combine(filePath, filename));
                return ImportFileBussinessService.WarehouseImport(Token, Path.Combine(filePath, filename));
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return new { status = "440", msg = "异常已捕获！请联系研发人员查询问题！" };
            }
        }

        /// <summary>
        /// 班别导入
        /// Tom 2017年5月5日16:11:47
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [FileAuthenticate]
        public object Inf00013Import()
        {
            try
            {
                string Token = HttpContext.Current.Request.Form["Token"];
                HttpPostedFile file = HttpContext.Current.Request.Files["file"];
                if (file == null)
                    return new { status = "440", msg = "文件为空！" };

                string UserID = UtilBussinessService.detoken(Token);
                UtilBussinessService.WriteLog(UserID, "班别", null, "导入", null);
                return ImportFileBussinessService.Inf00013Import(UserID, file.FileName, file.InputStream);

            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return new { status = "440", msg = "异常已捕获！请联系研发人员查询问题！" };
            }
        }

        /// <summary>
        /// 项目导入
        /// Tom 2017年5月9日12:14:00
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [FileAuthenticate]
        public object Inf00019Import()
        {
            try
            {
                string Token = HttpContext.Current.Request.Form["Token"];
                HttpPostedFile file = HttpContext.Current.Request.Files["file"];
                if (file == null)
                    return new { status = "440", msg = "文件为空！" };

                string UserID = UtilBussinessService.detoken(Token);
                UtilBussinessService.WriteLog(UserID, "项目", null, "导入", null);
                return ImportFileBussinessService.Inf00019Import(UserID, file.FileName, file.InputStream);
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return new { status = "440", msg = "异常已捕获！请联系研发人员查询问题！" };
            }
        }


        /// <summary>
        /// 料品属性导入
        /// SAM 2017年5月4日17:04:39
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [FileAuthenticate]
        public object Inf00024Import()
        {
            try
            {
                string Token = HttpContext.Current.Request.Form["Token"];
                HttpPostedFile file = HttpContext.Current.Request.Files["file"];
                if (file == null)
                    return new { status = "440", msg = "文件为空！" };
                string filePath = AppDomain.CurrentDomain.BaseDirectory + "Upload/";
                string filename = Path.GetFileName(file.FileName);
                file.SaveAs(Path.Combine(filePath, filename));
                return ImportFileBussinessService.Inf00024Import(Token, Path.Combine(filePath, filename));
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return new { status = "440", msg = "异常已捕获！请联系研发人员查询问题！" };
            }
        }


        /// <summary>
        /// 账户导入
        /// SAM 2017年5月10日14:04:11
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [FileAuthenticate]
        public object UserImport()
        {
            try
            {
                string Token = HttpContext.Current.Request.Form["Token"];
                HttpPostedFile file = HttpContext.Current.Request.Files["file"];
                if (file == null)
                    return new { status = "440", msg = "文件为空！" };
                string filePath = AppDomain.CurrentDomain.BaseDirectory + "Upload/";
                string filename = Path.GetFileName(file.FileName);
                file.SaveAs(Path.Combine(filePath, filename));
                return ImportFileBussinessService.UserImport(Token, Path.Combine(filePath, filename));
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return new { status = "440", msg = "异常已捕获！请联系研发人员查询问题！" };
            }
        }

        /// <summary>
        /// 原因群码导入
        /// SAM 2017年5月11日11:06:40
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [FileAuthenticate]
        public object Inf00017GroupImport()
        {
            try
            {
                string Token = HttpContext.Current.Request.Form["Token"];
                HttpPostedFile file = HttpContext.Current.Request.Files["file"];
                if (file == null)
                    return new { status = "440", msg = "文件为空！" };
                string filePath = AppDomain.CurrentDomain.BaseDirectory + "Upload/";
                string filename = Path.GetFileName(file.FileName);
                file.SaveAs(Path.Combine(filePath, filename));
                return ImportFileBussinessService.ParImport(Token, Path.Combine(filePath, filename), Framework.SystemID + "0191213000011");
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return new { status = "440", msg = "异常已捕获！请联系研发人员查询问题！" };
            }
        }


        /// <summary>
        /// 原因码导入
        /// SAM 2017年5月11日11:33:11
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [FileAuthenticate]
        public object Inf00017Import()
        {
            try
            {
                string Token = HttpContext.Current.Request.Form["Token"];
                HttpPostedFile file = HttpContext.Current.Request.Files["file"];
                if (file == null)
                    return new { status = "440", msg = "文件为空！" };
                string filePath = AppDomain.CurrentDomain.BaseDirectory + "Upload/";
                string filename = Path.GetFileName(file.FileName);
                file.SaveAs(Path.Combine(filePath, filename));
                return ImportFileBussinessService.Inf00017Import(Token, Path.Combine(filePath, filename));
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return new { status = "440", msg = "异常已捕获！请联系研发人员查询问题！" };
            }
        }

        /// <summary>
        /// 用途别导入
        /// SAM 2017年5月12日10:17:05
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [FileAuthenticate]
        public object USE00001Import()
        {
            try
            {
                string Token = HttpContext.Current.Request.Form["Token"];
                HttpPostedFile file = HttpContext.Current.Request.Files["file"];
                if (file == null)
                    return new { status = "440", msg = "文件为空！" };
                string filePath = AppDomain.CurrentDomain.BaseDirectory + "Upload/";
                string filename = Path.GetFileName(file.FileName);
                file.SaveAs(Path.Combine(filePath, filename));
                return ImportFileBussinessService.ParImport(Token, Path.Combine(filePath, filename), Framework.SystemID + "019121300000D");
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return new { status = "440", msg = "异常已捕获！请联系研发人员查询问题！" };
            }
        }


        /// <summary>
        /// 资源类别导入
        /// SAM 2017年5月12日10:44:08
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [FileAuthenticate]
        public object Inf00015ClassImport()
        {
            try
            {
                string Token = HttpContext.Current.Request.Form["Token"];
                HttpPostedFile file = HttpContext.Current.Request.Files["file"];
                if (file == null)
                    return new { status = "440", msg = "文件为空！" };
                string filePath = AppDomain.CurrentDomain.BaseDirectory + "Upload/";
                string filename = Path.GetFileName(file.FileName);
                file.SaveAs(Path.Combine(filePath, filename));
                return ImportFileBussinessService.ParImport(Token, Path.Combine(filePath, filename), Framework.SystemID + "0191213000013");
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return new { status = "440", msg = "异常已捕获！请联系研发人员查询问题！" };
            }
        }



        /// <summary>
        /// 资源群组导入
        /// SAM 2017年5月12日11:01:06
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [FileAuthenticate]
        public object Inf00015GroupImport()
        {
            try
            {
                string Token = HttpContext.Current.Request.Form["Token"];
                HttpPostedFile file = HttpContext.Current.Request.Files["file"];
                if (file == null)
                    return new { status = "440", msg = "文件为空！" };
                string filePath = AppDomain.CurrentDomain.BaseDirectory + "Upload/";
                string filename = Path.GetFileName(file.FileName);
                file.SaveAs(Path.Combine(filePath, filename));
                return ImportFileBussinessService.ParImport(Token, Path.Combine(filePath, filename), Framework.SystemID + "0191213000014");
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return new { status = "440", msg = "异常已捕获！请联系研发人员查询问题！" };
            }
        }

        /// <summary>
        /// 资源导入
        /// SAM 2017年5月12日14:35:15
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [FileAuthenticate]
        public object Inf00015Import()
        {
            try
            {
                string Token = HttpContext.Current.Request.Form["Token"];
                HttpPostedFile file = HttpContext.Current.Request.Files["file"];
                if (file == null)
                    return new { status = "440", msg = "文件为空！" };
                string filePath = AppDomain.CurrentDomain.BaseDirectory + "Upload/";
                string filename = Path.GetFileName(file.FileName);
                file.SaveAs(Path.Combine(filePath, filename));
                return ImportFileBussinessService.Inf00015Import(Token, Path.Combine(filePath, filename));
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return new { status = "440", msg = "异常已捕获！请联系研发人员查询问题！" };
            }
        }

        /// <summary>
        /// 料品的导入
        /// SAM 2017年5月16日16:27:35
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [FileAuthenticate]
        public object Inf00010Import()
        {
            try
            {
                string Token = HttpContext.Current.Request.Form["Token"];
                HttpPostedFile file = HttpContext.Current.Request.Files["file"];
                if (file == null)
                    return new { status = "440", msg = "文件为空！" };
                string filePath = AppDomain.CurrentDomain.BaseDirectory + "Upload/";
                string filename = Path.GetFileName(file.FileName);
                file.SaveAs(Path.Combine(filePath, filename));
                return ImportFileBussinessService.Inf00010Import(Token, Path.Combine(filePath, filename));
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return new { status = "440", msg = "异常已捕获！请联系研发人员查询问题！" };
            }
        }



        /// <summary>
        /// 批号自动编号设定的导入
        /// SAM 22017年5月17日16:12:04
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [FileAuthenticate]
        public object Inf00023Import()
        {
            try
            {
                string Token = HttpContext.Current.Request.Form["Token"];
                HttpPostedFile file = HttpContext.Current.Request.Files["file"];
                if (file == null)
                    return new { status = "440", msg = "文件为空！" };
                string filePath = AppDomain.CurrentDomain.BaseDirectory + "Upload/";
                string filename = Path.GetFileName(file.FileName);
                file.SaveAs(Path.Combine(filePath, filename));
                return ImportFileBussinessService.Inf00023Import(Token, Path.Combine(filePath, filename));
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return new { status = "440", msg = "异常已捕获！请联系研发人员查询问题！" };
            }
        }


        /// <summary>
        /// 行事历的导入
        /// SAM 2017年5月18日11:21:37
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [FileAuthenticate]
        public object Inf00014Import()
        {
            try
            {
                string Token = HttpContext.Current.Request.Form["Token"];
                HttpPostedFile file = HttpContext.Current.Request.Files["file"];
                if (file == null)
                    return new { status = "440", msg = "文件为空！" };
                string filePath = AppDomain.CurrentDomain.BaseDirectory + "Upload/";
                string filename = Path.GetFileName(file.FileName);
                file.SaveAs(Path.Combine(filePath, filename));
                return ImportFileBussinessService.Inf00014Import(Token, Path.Combine(filePath, filename));
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return new { status = "440", msg = "异常已捕获！请联系研发人员查询问题！" };
            }
        }

        /// <summary>
        /// 部门的导入
        /// SAM 2017年5月18日23:53:59
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [FileAuthenticate]
        public object Inf00005Import()
        {
            try
            {
                string Token = HttpContext.Current.Request.Form["Token"];
                HttpPostedFile file = HttpContext.Current.Request.Files["file"];
                if (file == null)
                    return new { status = "440", msg = "文件为空！" };
                string filePath = AppDomain.CurrentDomain.BaseDirectory + "Upload/";
                string filename = Path.GetFileName(file.FileName);
                file.SaveAs(Path.Combine(filePath, filename));
                UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "部门主档", null, "导入", null);
                return ImportFileBussinessService.Inf00005Import(Token, Path.Combine(filePath, filename));
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return new { status = "440", msg = "异常已捕获！请联系研发人员查询问题！" };
            }
        }



        /// <summary>
        /// 设备的导入
        /// SAM 2017年5月22日17:44:13
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [FileAuthenticate]
        public object Ems00001Import()
        {
            try
            {
                string Token = HttpContext.Current.Request.Form["Token"];
                HttpPostedFile file = HttpContext.Current.Request.Files["file"];
                if (file == null)
                    return new { status = "440", msg = "文件为空！" };
                string filePath = AppDomain.CurrentDomain.BaseDirectory + "Upload/";
                string filename = Path.GetFileName(file.FileName);
                file.SaveAs(Path.Combine(filePath, filename));
                UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "设备主档", null, "导入", null);
                return ImportFileBussinessService.Ems00001Import(Token, Path.Combine(filePath, filename));
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return new { status = "440", msg = "异常已捕获！请联系研发人员查询问题！" };
            }
        }


        /// <summary>
        /// 设备项目的导入
        /// SAM 2017年5月22日17:44:13
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [FileAuthenticate]
        public object Ems00001ProjectImport()
        {
            try
            {
                string Token = HttpContext.Current.Request.Form["Token"];
                HttpPostedFile file = HttpContext.Current.Request.Files["file"];
                if (file == null)
                    return new { status = "440", msg = "文件为空！" };
                string filePath = AppDomain.CurrentDomain.BaseDirectory + "Upload/";
                string filename = Path.GetFileName(file.FileName);
                file.SaveAs(Path.Combine(filePath, filename));
                UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "设备项目主档", null, "导入", null);
                return ImportFileBussinessService.Ems00001ProjectImport(Token, Path.Combine(filePath, filename));
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return new { status = "440", msg = "异常已捕获！请联系研发人员查询问题！" };
            }
        }


        /// <summary>
        /// 制程替代群组的导入
        /// SAM 2017年5月23日09:39:08
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [FileAuthenticate]
        public object Inf00021Import()
        {
            try
            {
                string Token = HttpContext.Current.Request.Form["Token"];
                HttpPostedFile file = HttpContext.Current.Request.Files["file"];
                if (file == null)
                    return new { status = "440", msg = "文件为空！" };
                string filePath = AppDomain.CurrentDomain.BaseDirectory + "Upload/";
                string filename = Path.GetFileName(file.FileName);
                file.SaveAs(Path.Combine(filePath, filename));
                return ImportFileBussinessService.ParImport(Token, Path.Combine(filePath, filename), Framework.SystemID + "0191213000015");
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return new { status = "440", msg = "异常已捕获！请联系研发人员查询问题！" };
            }
        }


        /// <summary>
        /// 设备的图样新增
        /// SAM 2017年5月23日10:02:09
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public object Ems00001PatternAdd(JObject data)
        {
            String img = data.Value<String>("img");
            //当传入的值有值且为base64格式是执行上传
            if (!String.IsNullOrWhiteSpace(img) && img.IndexOf("base64,") >= 0)
            {
                Image file = UtilBussinessService.GetImageFromBase64(img.Substring(img.IndexOf("base64,") + 7));

                return ImportFileBussinessService.Ems00001PatternAdd(file, data);
            }
            else
                return new { status = "410", msg = "base64格式错误" };
        }


        /// <summary>
        /// 设备的图样编辑
        /// SAM 2017年5月27日14:31:06
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public object Ems00001PatternUpdate(JObject data)
        {
            String img = data.Value<String>("img");
            //当传入的值有值且为base64格式是执行上传
            if (!String.IsNullOrWhiteSpace(img) && img.IndexOf("base64,") >= 0)
            {
                Image file = UtilBussinessService.GetImageFromBase64(img.Substring(img.IndexOf("base64,") + 7));

                return ImportFileBussinessService.Ems00001PatternUpdate(file, data);
            }
            else
                return new { status = "410", msg = "base64格式错误" };
        }


        /// <summary>
        /// 感知器的导入
        /// SAM 2017年6月6日21:58:50
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [FileAuthenticate]
        public object Iot00001Import()
        {
            try
            {
                string Token = HttpContext.Current.Request.Form["Token"];
                HttpPostedFile file = HttpContext.Current.Request.Files["file"];
                if (file == null)
                    return new { status = "440", msg = "文件为空！" };
                string filePath = AppDomain.CurrentDomain.BaseDirectory + "Upload/";
                string filename = Path.GetFileName(file.FileName);
                file.SaveAs(Path.Combine(filePath, filename));
                return ImportFileBussinessService.Iot00001Import(Token, Path.Combine(filePath, filename));
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return new { status = "440", msg = "异常已捕获！请联系研发人员查询问题！" };
            }
        }

        /// <summary>
        /// 工序的导入
        /// SAM 2017年5月25日11:02:05
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [FileAuthenticate]
        public object Inf00018OperationImport()
        {
            try
            {
                string Token = HttpContext.Current.Request.Form["Token"];
                HttpPostedFile file = HttpContext.Current.Request.Files["file"];
                if (file == null)
                    return new { status = "440", msg = "文件为空！" };
                string filePath = AppDomain.CurrentDomain.BaseDirectory + "Upload/";
                string filename = Path.GetFileName(file.FileName);
                file.SaveAs(Path.Combine(filePath, filename));
                return ImportFileBussinessService.ParImport(Token, Path.Combine(filePath, filename), Framework.SystemID + "0191213000016");
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return new { status = "440", msg = "异常已捕获！请联系研发人员查询问题！" };
            }
        }

        /// <summary>
        /// 制程的导入
        /// SAM 2017年5月25日11:02:05
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [FileAuthenticate]
        public object Inf00018ProcessImport()
        {
            try
            {
                string Token = HttpContext.Current.Request.Form["Token"];
                HttpPostedFile file = HttpContext.Current.Request.Files["file"];
                if (file == null)
                    return new { status = "440", msg = "文件为空！" };
                string filePath = AppDomain.CurrentDomain.BaseDirectory + "Upload/";
                string filename = Path.GetFileName(file.FileName);
                file.SaveAs(Path.Combine(filePath, filename));
                return ImportFileBussinessService.Inf00018ProcessImport(Token, Path.Combine(filePath, filename));
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return new { status = "440", msg = "异常已捕获！请联系研发人员查询问题！" };
            }
        }


        /// <summary>
        /// 工作中心的导入
        /// SAM 2017年5月25日11:02:05
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [FileAuthenticate]
        public object Inf00018WorkCenterImport()
        {
            try
            {
                string Token = HttpContext.Current.Request.Form["Token"];
                HttpPostedFile file = HttpContext.Current.Request.Files["file"];
                if (file == null)
                    return new { status = "440", msg = "文件为空！" };
                string filePath = AppDomain.CurrentDomain.BaseDirectory + "Upload/";
                string filename = Path.GetFileName(file.FileName);
                file.SaveAs(Path.Combine(filePath, filename));
                return ImportFileBussinessService.Inf00018WorkCenterImport(Token, Path.Combine(filePath, filename));
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return new { status = "440", msg = "异常已捕获！请联系研发人员查询问题！" };
            }
        }


        /// <summary>
        /// 检验群组码的导入
        /// SAM 2017年5月26日15:25:10
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [FileAuthenticate]
        public object Qcs00003Import()
        {
            try
            {
                string Token = HttpContext.Current.Request.Form["Token"];
                HttpPostedFile file = HttpContext.Current.Request.Files["file"];
                if (file == null)
                    return new { status = "440", msg = "文件为空！" };
                string filePath = AppDomain.CurrentDomain.BaseDirectory + "Upload/";
                string filename = Path.GetFileName(file.FileName);
                file.SaveAs(Path.Combine(filePath, filename));
                return ImportFileBussinessService.ParImport(Token, Path.Combine(filePath, filename), Framework.SystemID + "0191213000018");
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return new { status = "440", msg = "异常已捕获！请联系研发人员查询问题！" };
            }
        }

        /// <summary>
        /// 单据子轨设定的导入
        /// SAM 2017年6月2日15:00:03
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [FileAuthenticate]
        public object Inf00016Import()
        {
            try
            {
                string Token = HttpContext.Current.Request.Form["Token"];
                HttpPostedFile file = HttpContext.Current.Request.Files["file"];
                if (file == null)
                    return new { status = "440", msg = "文件为空！" };
                string filePath = AppDomain.CurrentDomain.BaseDirectory + "Upload/";
                string filename = Path.GetFileName(file.FileName);
                file.SaveAs(Path.Combine(filePath, filename));
                return ImportFileBussinessService.Inf00016Import(Token, Path.Combine(filePath, filename));
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return new { status = "440", msg = "异常已捕获！请联系研发人员查询问题！" };
            }
        }

        /// <summary>
        /// 抽样检验设定导入
        /// SAM 2017年6月6日14:31:10
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [FileAuthenticate]
        public object Qcs00001Import()
        {
            try
            {
                string Token = HttpContext.Current.Request.Form["Token"];
                HttpPostedFile file = HttpContext.Current.Request.Files["file"];
                if (file == null)
                    return new { status = "440", msg = "文件为空！" };
                string filePath = AppDomain.CurrentDomain.BaseDirectory + "Upload/";
                string filename = Path.GetFileName(file.FileName);
                file.SaveAs(Path.Combine(filePath, filename));
                return ImportFileBussinessService.Qcs00001Import(Token, Path.Combine(filePath, filename));
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return new { status = "440", msg = "异常已捕获！请联系研发人员查询问题！" };
            }
        }

        /// <summary>
        /// 检验类别的导入
        /// SAM 2017年6月9日11:37:37
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [FileAuthenticate]
        public object Qcs00002TypeImport()
        {
            try
            {
                string Token = HttpContext.Current.Request.Form["Token"];
                HttpPostedFile file = HttpContext.Current.Request.Files["file"];
                if (file == null)
                    return new { status = "440", msg = "文件为空！" };
                string filePath = AppDomain.CurrentDomain.BaseDirectory + "Upload/";
                string filename = Path.GetFileName(file.FileName);
                file.SaveAs(Path.Combine(filePath, filename));
                return ImportFileBussinessService.Qcs00002TypeImport(Token, Path.Combine(filePath, filename));
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return new { status = "440", msg = "异常已捕获！请联系研发人员查询问题！" };
            }
        }

        /// <summary>
        /// 检验项目的导入
        /// SAM 2017年6月11日12:44:55
        /// TODO
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [FileAuthenticate]
        public object Qcs00002ProjectImport()
        {
            try
            {
                string Token = HttpContext.Current.Request.Form["Token"];
                HttpPostedFile file = HttpContext.Current.Request.Files["file"];
                if (file == null)
                    return new { status = "440", msg = "文件为空！" };
                string filePath = AppDomain.CurrentDomain.BaseDirectory + "Upload/";
                string filename = Path.GetFileName(file.FileName);
                file.SaveAs(Path.Combine(filePath, filename));
                return ImportFileBussinessService.Qcs00002ProjectImport(Token, Path.Combine(filePath, filename));
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return new { status = "440", msg = "异常已捕获！请联系研发人员查询问题！" };
            }
        }

        /// <summary>
        /// 客诉单明细的导入
        /// SAM 2017年6月15日09:55:24
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [FileAuthenticate]
        public object Qcs00009Import()
        {
            try
            {
                string Token = HttpContext.Current.Request.Form["Token"];
                string ComplaintID = HttpContext.Current.Request.Form["ComplaintID"];
                if (ComplaintID == null)
                    return new { status = "440", msg = "表头流水号为空！" };
                HttpPostedFile file = HttpContext.Current.Request.Files["file"];
                if (file == null)
                    return new { status = "440", msg = "文件为空！" };
                string filePath = AppDomain.CurrentDomain.BaseDirectory + "Upload/";
                string filename = Path.GetFileName(file.FileName);
                file.SaveAs(Path.Combine(filePath, filename));
                return ImportFileBussinessService.Qcs00009Import(Token, Path.Combine(filePath, filename), ComplaintID);
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return new { status = "440", msg = "异常已捕获！请联系研发人员查询问题！" };
            }
        }

        /// <summary>
        /// 客诉单附件—客诉明细上傳保存
        /// SAM 2017年6月21日17:22:09
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [FileAuthenticate]
        public object Qcs00009FileAdd()
        {
            try
            {
                string Token = HttpContext.Current.Request.Form["Token"];
                string ComplaintDetailID = HttpContext.Current.Request.Form["ComplaintDetailID"];
                HttpPostedFile file = HttpContext.Current.Request.Files["file"];
                if (string.IsNullOrWhiteSpace(ComplaintDetailID))
                    return new { status = "440", msg = "所需参数不全！" };

                return ImportFileBussinessService.Qcs00009FileAdd(Token, ComplaintDetailID, file);
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return new { status = "440", msg = "异常已捕获！请联系研发人员查询问题！" };
            }
        }

        /// <summary>
        /// 客诉单-处理对策上传保存
        /// SAM 2017年6月22日14:11:10
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [FileAuthenticate]
        public object Qcs00010FileAdd()
        {
            try
            {
                string Token = HttpContext.Current.Request.Form["Token"];
                string ComplaintDetailID = HttpContext.Current.Request.Form["ComplaintDetailID"];
                HttpPostedFile file = HttpContext.Current.Request.Files["file"];
                if (string.IsNullOrWhiteSpace(ComplaintDetailID))
                    return new { status = "440", msg = "所需参数不全！" };

                return ImportFileBussinessService.Qcs00010FileAdd(Token, ComplaintDetailID, file);
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return new { status = "440", msg = "异常已捕获！请联系研发人员查询问题！" };
            }
        }

        /// <summary>
        /// 客诉原因的导入
        /// SAM 2017年6月15日14:31:30
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [FileAuthenticate]
        public object Qcs00010ReasonImport()
        {
            try
            {
                string Token = HttpContext.Current.Request.Form["Token"];
                string ComplaintDetailID = HttpContext.Current.Request.Form["ComplaintDetailID"];
                if (ComplaintDetailID == null)
                    return new { status = "440", msg = "明细流水号为空！" };
                HttpPostedFile file = HttpContext.Current.Request.Files["file"];
                if (file == null)
                    return new { status = "440", msg = "文件为空！" };
                string filePath = AppDomain.CurrentDomain.BaseDirectory + "Upload/";
                string filename = Path.GetFileName(file.FileName);
                file.SaveAs(Path.Combine(filePath, filename));
                return ImportFileBussinessService.Qcs00010ReasonImport(Token, Path.Combine(filePath, filename), ComplaintDetailID);
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return new { status = "440", msg = "异常已捕获！请联系研发人员查询问题！" };
            }
        }


        /// <summary>
        /// 保养项目的导入
        /// SAM 2017年7月5日15:56:28
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [FileAuthenticate]
        public object Ems00008ProjectImport()
        {
            try
            {
                string Token = HttpContext.Current.Request.Form["Token"];
                HttpPostedFile file = HttpContext.Current.Request.Files["file"];
                if (file == null)
                    return new { status = "440", msg = "文件为空！" };
                string filePath = AppDomain.CurrentDomain.BaseDirectory + "Upload/";
                string filename = Path.GetFileName(file.FileName);
                file.SaveAs(Path.Combine(filePath, filename));
                return ImportFileBussinessService.Ems00008ProjectImport(Token, Path.Combine(filePath, filename));
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return new { status = "440", msg = "异常已捕获！请联系研发人员查询问题！" };
            }
        }


        /// <summary>
        /// 保养类型的导入
        /// SAM 2017年7月5日15:56:28
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [FileAuthenticate]
        public object Ems00008TypeImport()
        {
            try
            {
                string Token = HttpContext.Current.Request.Form["Token"];
                HttpPostedFile file = HttpContext.Current.Request.Files["file"];
                if (file == null)
                    return new { status = "440", msg = "文件为空！" };
                string filePath = AppDomain.CurrentDomain.BaseDirectory + "Upload/";
                string filename = Path.GetFileName(file.FileName);
                file.SaveAs(Path.Combine(filePath, filename));
                return ImportFileBussinessService.ParImport(Token, Path.Combine(filePath, filename), Framework.SystemID + "0191213000023");
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return new { status = "440", msg = "异常已捕获！请联系研发人员查询问题！" };
            }
        }

        /// <summary>
        /// 设备保养清单设定导入
        /// SAM 2017年7月5日16:07:01
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [FileAuthenticate]
        public object Ems00008Import()
        {
            try
            {
                string Token = HttpContext.Current.Request.Form["Token"];
                HttpPostedFile file = HttpContext.Current.Request.Files["file"];
                if (file == null)
                    return new { status = "440", msg = "文件为空！" };
                string filePath = AppDomain.CurrentDomain.BaseDirectory + "Upload/";
                string filename = Path.GetFileName(file.FileName);
                file.SaveAs(Path.Combine(filePath, filename));
                return ImportFileBussinessService.Ems00008Import(Token, Path.Combine(filePath, filename));
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return new { status = "440", msg = "异常已捕获！请联系研发人员查询问题！" };
            }
        }

        /// <summary>
        /// 设备巡检维护
        /// SAM 2017年7月6日17:44:35
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [FileAuthenticate]
        public object Ems00003Import()
        {
            try
            {
                string Token = HttpContext.Current.Request.Form["Token"];
                string EquipmentInspectionRecordID = HttpContext.Current.Request.Form["EquipmentInspectionRecordID"];
                HttpPostedFile file = HttpContext.Current.Request.Files["file"];
                if (file == null)
                    return new { status = "440", msg = "文件为空！" };
                if (string.IsNullOrWhiteSpace(EquipmentInspectionRecordID))
                    return new { status = "440", msg = "表头流水号为空" };
                string filePath = AppDomain.CurrentDomain.BaseDirectory + "Upload/";
                string filename = Path.GetFileName(file.FileName);
                file.SaveAs(Path.Combine(filePath, filename));
                return ImportFileBussinessService.Ems00003Import(Token, Path.Combine(filePath, filename), EquipmentInspectionRecordID);
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return new { status = "440", msg = "异常已捕获！请联系研发人员查询问题！" };
            }
        }
        /// <summary>
        /// 制程检验单-表头导入
        /// Joint
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [FileAuthenticate]
        public object Qcs00005Import()
        {
            try
            {
                string Token = HttpContext.Current.Request.Form["Token"];
                HttpPostedFile file = HttpContext.Current.Request.Files["file"];
                if (file == null)
                    return new { status = "440", msg = "文件为空！" };
                string filePath = AppDomain.CurrentDomain.BaseDirectory + "Upload/";
                string filename = Path.GetFileName(file.FileName);
                file.SaveAs(Path.Combine(filePath, filename));
                return ImportFileBussinessService.Qcs00005Import(Token, Path.Combine(filePath, filename));
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return new { status = "440", msg = "异常已捕获！请联系研发人员查询问题！" };
            }
        }

        /// <summary>
        /// 制程首件检验导入
        /// Joint 2017年8月2日14:22:05
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [FileAuthenticate]
        public object Qcs00007Import()
        {
            try
            {
                string Token = HttpContext.Current.Request.Form["Token"];
                HttpPostedFile file = HttpContext.Current.Request.Files["file"];
                if (file == null)
                    return new { status = "440", msg = "文件为空！" };
                string filePath = AppDomain.CurrentDomain.BaseDirectory + "Upload/";
                string filename = Path.GetFileName(file.FileName);
                file.SaveAs(Path.Combine(filePath, filename));
                return ImportFileBussinessService.Qcs00007Import(Token, Path.Combine(filePath, filename));
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return new { status = "440", msg = "异常已捕获！请联系研发人员查询问题！" };
            }
        }

        /// <summary>
        /// 制程巡检检验导入
        /// Joint 2017年8月4日12:07:07
        /// </summary>
        /// <returns></returns>
        public object Qcs00008Import()
        {
            try
            {
                string Token = HttpContext.Current.Request.Form["Token"];
                HttpPostedFile file = HttpContext.Current.Request.Files["file"];
                if (file == null)
                    return new { status = "440", msg = "文件为空！" };
                string filePath = AppDomain.CurrentDomain.BaseDirectory + "Upload/";
                string filename = Path.GetFileName(file.FileName);
                file.SaveAs(Path.Combine(filePath, filename));
                return ImportFileBussinessService.Qcs00008Import(Token, Path.Combine(filePath, filename));
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return new { status = "440", msg = "异常已捕获！请联系研发人员查询问题！" };
            }
        }
        /// <summary>
        /// 设备保养工单的导入
        /// SAM 2017年7月31日23:58:07
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [FileAuthenticate]
        public object Ems00009Import()
        {
            try
            {
                string Token = HttpContext.Current.Request.Form["Token"];
                HttpPostedFile file = HttpContext.Current.Request.Files["file"];
                if (file == null)
                    return new { status = "440", msg = "文件为空！" };
                string filePath = AppDomain.CurrentDomain.BaseDirectory + "Upload/";
                string filename = Path.GetFileName(file.FileName);
                file.SaveAs(Path.Combine(filePath, filename));
                return ImportFileBussinessService.Ems00009Import(Token, Path.Combine(filePath, filename));
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return new { status = "440", msg = "异常已捕获！请联系研发人员查询问题！" };
            }
        }

        /// <summary>
        /// 設備保養資料維護导入
        /// Joint 2017年8月1日16:17:10
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [FileAuthenticate]
        public object Ems00010Import()
        {
            try
            {
                string Token = HttpContext.Current.Request.Form["Token"];
                HttpPostedFile file = HttpContext.Current.Request.Files["file"];
                if (file == null)
                    return new { status = "440", msg = "文件为空！" };
                string filePath = AppDomain.CurrentDomain.BaseDirectory + "Upload/";
                string filename = Path.GetFileName(file.FileName);
                file.SaveAs(Path.Combine(filePath, filename));
                return ImportFileBussinessService.Ems00010Import(Token, Path.Combine(filePath, filename));
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return new { status = "440", msg = "异常已捕获！请联系研发人员查询问题！" };
            }
        }

        /// <summary>
        /// 完工单的导入
        /// SAM 2017年8月1日15:13:57
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [FileAuthenticate]
        public object Sfc00007Import()
        {
            try
            {
                string Token = HttpContext.Current.Request.Form["Token"];
                HttpPostedFile file = HttpContext.Current.Request.Files["file"];
                if (file == null)
                    return new { status = "440", msg = "文件为空！" };
                string filePath = AppDomain.CurrentDomain.BaseDirectory + "Upload/";
                string filename = Path.GetFileName(file.FileName);
                file.SaveAs(Path.Combine(filePath, filename));
                return ImportFileBussinessService.Sfc00007Import(Token, Path.Combine(filePath, filename));
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return new { status = "440", msg = "异常已捕获！请联系研发人员查询问题！" };
            }
        }


        /// <summary>
        /// 调整单的导入
        /// SAM 2017年8月1日16:19:22
        /// </summary>
        /// <returns></returns>
        public object Sfc00008Import()
        {
            try
            {
                string Token = HttpContext.Current.Request.Form["Token"];
                HttpPostedFile file = HttpContext.Current.Request.Files["file"];
                if (file == null)
                    return new { status = "440", msg = "文件为空！" };
                string filePath = AppDomain.CurrentDomain.BaseDirectory + "Upload/";
                string filename = Path.GetFileName(file.FileName);
                file.SaveAs(Path.Combine(filePath, filename));
                return ImportFileBussinessService.Sfc00008Import(Token, Path.Combine(filePath, filename));
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return new { status = "440", msg = "异常已捕获！请联系研发人员查询问题！" };
            }
        }

        /// <summary>
        /// 制品制程的导入
        /// SAM 2017年8月15日14:19:32
        /// TODO 未完成
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [FileAuthenticate]
        public object Sfc00001Import()
        {
            try
            {
                string Token = HttpContext.Current.Request.Form["Token"];
                HttpPostedFile file = HttpContext.Current.Request.Files["file"];
                if (file == null)
                    return new { status = "440", msg = "文件为空！" };
                string filePath = AppDomain.CurrentDomain.BaseDirectory + "Upload/";
                string filename = Path.GetFileName(file.FileName);
                file.SaveAs(Path.Combine(filePath, filename));
                return ImportFileBussinessService.Sfc00001Import(Token, Path.Combine(filePath, filename));
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return new { status = "440", msg = "异常已捕获！请联系研发人员查询问题！" };
            }
        }

        /// <summary>
        /// Alvin 2017年9月5日16:06:33
        /// QCS00004 标准检验规范设定（制程）导入
        /// </summary>
        /// <returns></returns>
        public object Qcs00004Import()
        {
            try
            {
                string Token = HttpContext.Current.Request.Form["Token"];
                HttpPostedFile file = HttpContext.Current.Request.Files["file"];
                if (file == null)
                {
                    return new { status = "440", msg = "文件为空！" };
                }
                string PartID = HttpContext.Current.Request.Form["PartID"];
                if (string.IsNullOrWhiteSpace(PartID))
                {
                    return new { status = "440", msg = "料品流水号为空！" };
                }
                string InspectionType = HttpContext.Current.Request.Form["InspectionType"];
                if (string.IsNullOrWhiteSpace(InspectionType))
                {
                    return new { status = "440", msg = "檢驗種類参数为空！" };
                }
                string SettingType = HttpContext.Current.Request.Form["SettingType"];
                if (string.IsNullOrWhiteSpace(SettingType))
                {
                    return new { status = "440", msg = "檢驗設定型態参数为空！" };
                }
                string ProcessID = HttpContext.Current.Request.Form["ProcessID"];
                if (string.IsNullOrWhiteSpace(ProcessID))
                {
                    return new { status = "440", msg = "制程流水号为空！" };
                }
                string OperationID = HttpContext.Current.Request.Form["OperationID"];
                if (string.IsNullOrWhiteSpace(OperationID))
                {
                    return new { status = "440", msg = "工序流水号为空！" };
                }

                string filePath = AppDomain.CurrentDomain.BaseDirectory + "Upload/";
                string filename = Path.GetFileName(file.FileName);
                file.SaveAs(Path.Combine(filePath, filename));
                return ImportFileBussinessService.Qcs00004Import(Token, Path.Combine(filePath, filename), PartID, InspectionType, SettingType, ProcessID, OperationID);

            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return new { status = "440", msg = "异常已捕获！请联系研发人员查询问题！" };
            }

        }


        /// <summary>
        /// 制令单的导入
        /// SAM 2017年9月29日15:00:23
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [FileAuthenticate]
        public object Sfc00002Import()
        {
            try
            {
                string Token = HttpContext.Current.Request.Form["Token"];
                HttpPostedFile file = HttpContext.Current.Request.Files["file"];
                if (file == null)
                    return new { status = "440", msg = "文件为空！" };
                string filePath = AppDomain.CurrentDomain.BaseDirectory + "Upload/";
                string filename = Path.GetFileName(file.FileName);
                file.SaveAs(Path.Combine(filePath, filename));
                return ImportFileBussinessService.Sfc00002Import(Token, Path.Combine(filePath, filename));
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return new { status = "440", msg = "异常已捕获！请联系研发人员查询问题！" };
            }
        }


        /// <summary>
        /// 账户的导入，包括对应角色的导入
        /// Sam 2017年10月17日09:47:27
        /// 2017年10月16日晚，台湾顾问方面提出的要求
        /// 1.导入模板最后面添加5列，角色列。
        /// 2.如果存在导入失败的话，失败原因将以txt文档的方式直接下载
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [FileAuthenticate]
        public object Inf00003Import()
        {
            try
            {
                string Token = HttpContext.Current.Request.Form["Token"];
                HttpPostedFile file = HttpContext.Current.Request.Files["file"];
                if (file == null)
                    return new { status = "440", msg = "文件为空！" };
                string filePath = AppDomain.CurrentDomain.BaseDirectory + "ImportFile";

                DirectoryInfo di = new DirectoryInfo(filePath);
                if (!di.Exists)//创建目录
                    di.Create();

                string filename = Path.GetFileName(file.FileName);
                file.SaveAs(Path.Combine(filePath, filename));
                object result = ImportFileBussinessService.Inf00003Import(Token, Path.Combine(filePath, filename));
                return result;
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return new { status = "440", msg = "异常已捕获！请联系研发人员查询问题！" };
            }
        }
        /// <summary>
        /// Qcs00004新的导入(群组)
        /// Sam 2017年10月19日09:32:23
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [FileAuthenticate]
        public object Qcs00004ImportV1()
        {
            try
            {
                string Token = HttpContext.Current.Request.Form["Token"];
                HttpPostedFile file = HttpContext.Current.Request.Files["file"];
                if (file == null)
                    return new { status = "440", msg = "文件为空！" };
                string filePath = AppDomain.CurrentDomain.BaseDirectory + "ImportFile";

                DirectoryInfo di = new DirectoryInfo(filePath);
                if (!di.Exists)//创建目录
                    di.Create();

                string filename = Path.GetFileName(file.FileName);
                file.SaveAs(Path.Combine(filePath, filename));
                object result = ImportFileBussinessService.Qcs00004ImportV1(Token, Path.Combine(filePath, filename));
                return result;
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return new { status = "440", msg = "异常已捕获！请联系研发人员查询问题！" };
            }
        }

        /// <summary>
        /// QCS00004的导入（料品）
        /// Sam 2017年10月19日20:36:09
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [FileAuthenticate]
        public object Qcs00004ImportV2()
        {
            try
            {
                string Token = HttpContext.Current.Request.Form["Token"];
                HttpPostedFile file = HttpContext.Current.Request.Files["file"];
                if (file == null)
                    return new { status = "440", msg = "文件为空！" };
                string filePath = AppDomain.CurrentDomain.BaseDirectory + "ImportFile";

                DirectoryInfo di = new DirectoryInfo(filePath);
                if (!di.Exists)//创建目录
                    di.Create();

                string filename = Path.GetFileName(file.FileName);
                file.SaveAs(Path.Combine(filePath, filename));
                object result = ImportFileBussinessService.Qcs00004ImportV2(Token, Path.Combine(filePath, filename));
                return result;
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return new { status = "440", msg = "异常已捕获！请联系研发人员查询问题！" };
            }
        }

        /// <summary>
        /// 料品的导入
        /// Sam 2017年10月19日16:14:20
        /// 具体逻辑调整
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [FileAuthenticate]
        public object Inf00010ImportV1()
        {
            try
            {
                string Token = HttpContext.Current.Request.Form["Token"];
                HttpPostedFile file = HttpContext.Current.Request.Files["file"];
                if (file == null)
                    return new { status = "440", msg = "文件为空！" };
                string filePath = AppDomain.CurrentDomain.BaseDirectory + "ImportFile";

                DirectoryInfo di = new DirectoryInfo(filePath);
                if (!di.Exists)//创建目录
                    di.Create();

                string filename = Path.GetFileName(file.FileName);
                file.SaveAs(Path.Combine(filePath, filename));
                object result = ImportFileBussinessService.Inf00010ImportV1(Token, Path.Combine(filePath, filename));
                return result;
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return new { status = "440", msg = "异常已捕获！请联系研发人员查询问题！" };
            }
        }

        /// <summary>
        /// 设备项目的导入
        /// SAM 2017年10月23日20:29:35
        /// 根据要求，导入模板调整
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [FileAuthenticate]
        public object Ems00001ProjectImportV1()
        {
            try
            {
                string Token = HttpContext.Current.Request.Form["Token"];
                HttpPostedFile file = HttpContext.Current.Request.Files["file"];
                if (file == null)
                    return new { status = "440", msg = "文件为空！" };

                string filePath = AppDomain.CurrentDomain.BaseDirectory + "ImportFile";

                DirectoryInfo di = new DirectoryInfo(filePath);
                if (!di.Exists)//创建目录
                    di.Create();

                string filename = Path.GetFileName(file.FileName);
                file.SaveAs(Path.Combine(filePath, filename));
                UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "设备项目主档", null, "导入", null);
                return ImportFileBussinessService.Ems00001ProjectImportV1(Token, Path.Combine(filePath, filename));
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return new { status = "440", msg = "异常已捕获！请联系研发人员查询问题！" };
            }
        }


        /// <summary>
        /// 制令单的导入
        /// SAM 2017年10月24日14:17:59
        /// 上次写的代码不知为何被覆盖了。重新编写
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [FileAuthenticate]
        public object Sfc00002ImportV1()
        {
            try
            {
                string Token = HttpContext.Current.Request.Form["Token"];
                HttpPostedFile file = HttpContext.Current.Request.Files["file"];
                if (file == null)
                    return new { status = "440", msg = "文件为空！" };

                string filePath = AppDomain.CurrentDomain.BaseDirectory + "ImportFile";

                DirectoryInfo di = new DirectoryInfo(filePath);
                if (!di.Exists)//创建目录
                    di.Create();

                string filename = Path.GetFileName(file.FileName);
                file.SaveAs(Path.Combine(filePath, filename));
                return ImportFileBussinessService.Sfc00002ImportV1(Token, Path.Combine(filePath, filename));
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return new { status = "440", msg = "异常已捕获！请联系研发人员查询问题！" };
            }
        }

        /// <summary>
        /// 制品制程的导入
        /// SAM 2017年10月26日14:35:16 
        /// V1版本,添加错误日志的产生
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [FileAuthenticate]
        public object Sfc00001ImportV1()
        {
            try
            {
                string Token = HttpContext.Current.Request.Form["Token"];
                HttpPostedFile file = HttpContext.Current.Request.Files["file"];
                if (file == null)
                    return new { status = "440", msg = "文件为空！" };

                string filePath = AppDomain.CurrentDomain.BaseDirectory + "ImportFile";

                DirectoryInfo di = new DirectoryInfo(filePath);
                if (!di.Exists)//创建目录
                    di.Create();
                string filename = Path.GetFileName(file.FileName);
                file.SaveAs(Path.Combine(filePath, filename));
                return ImportFileBussinessService.Sfc00001ImportV1(Token, Path.Combine(filePath, filename));
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return new { status = "440", msg = "异常已捕获！请联系研发人员查询问题！" };
            }
        }

        /// <summary>
        /// 制令单的导入
        /// Sam 2017年10月27日11:11:58
        /// 在V1的版本上 添加了条件的数据过滤,添加了错误日志的生成
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [FileAuthenticate]
        public object Sfc00002ImportV2()
        {
            try
            {
                string Token = HttpContext.Current.Request.Form["Token"];
                HttpPostedFile file = HttpContext.Current.Request.Files["file"];
                if (file == null)
                    return new { status = "440", msg = "请先选择文件" };

                string Date = HttpContext.Current.Request.Form["Date"];
                string StartMoNo = HttpContext.Current.Request.Form["StartMoNo"];
                string EndMoNo = HttpContext.Current.Request.Form["EndMoNo"];
                string Cust = HttpContext.Current.Request.Form["Cust"];

                if (string.IsNullOrWhiteSpace(Date))
                    return new { status = "440", msg = "时间为空！" };

                if (string.IsNullOrWhiteSpace(StartMoNo))
                    return new { status = "440", msg = "开始区间为空！" };

                if (string.IsNullOrWhiteSpace(EndMoNo))
                    return new { status = "440", msg = "结束区间为空！" };

                string filePath = AppDomain.CurrentDomain.BaseDirectory + "ImportFile";

                DirectoryInfo di = new DirectoryInfo(filePath);
                if (!di.Exists)//创建目录
                    di.Create();

                string filename = Path.GetFileName(file.FileName);
                file.SaveAs(Path.Combine(filePath, filename));
                return ImportFileBussinessService.Sfc00002ImportV2(Token, Path.Combine(filePath, filename), Date, StartMoNo, EndMoNo, Cust);
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return new { status = "440", msg = "异常已捕获！请联系研发人员查询问题！" };
            }
        }
    }
}
