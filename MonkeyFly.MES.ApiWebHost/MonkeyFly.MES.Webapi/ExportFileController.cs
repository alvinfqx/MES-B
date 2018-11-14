using MonkeyFly.MES.BasicService;
using MonkeyFly.MES.FilterAttributes;
using MonkeyFly.MES.Services;
using System;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace MonkeyFly.MES.Webapi
{
    /// <summary>
    /// 导出文件专用控制器
    /// SAM 2017年6月6日21:50:39
    /// </summary>
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ExportFileController : ApiController
    {
        /// <summary>
        /// ExportFileAPI
        /// </summary>
        [HttpGet]
        public void ExportFileAPI() { }

        /// <summary>
        /// 导出厂别
        /// SAM 2017年4月26日16:43:19
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="Code">代号</param>
        [HttpGet]
        [Authenticate]
        public void PlantExport(string Token, string Code = null)
        {

            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            string fileName = "厂别信息" + DateTime.Now.ToString("yyyyMMdd");
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName + ".xls");
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "厂别主档", null, "导出", null);
            byte[] publicCustomerListExcelBytes = ExportFileBussinessService.PlantExport(Code);
            HttpContext.Current.Response.BinaryWrite(publicCustomerListExcelBytes);
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }

        /// <summary>
        /// 导出厂区
        /// SAM 2017年4月26日16:43:19
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="Code">代号</param>
        [HttpGet]
        [Authenticate]
        public void PlantAreaExport(string Token, string Code = null)
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            string fileName = "厂区信息" + DateTime.Now.ToString("yyyyMMdd");
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName + ".xls");
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "厂区主档", null, "导出", null);
            byte[] publicCustomerListExcelBytes = ExportFileBussinessService.PlantAreaExport(Code);
            HttpContext.Current.Response.BinaryWrite(publicCustomerListExcelBytes);
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }

        /// <summary>
        /// 导出客户
        /// SAM 2017年4月27日11:25:49
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="Code">代号</param>
        ///  <param name="Name">名称</param>
        [HttpGet]
        [Authenticate]
        public void CustomerExport(string Token, string Code = null, string Name = null)
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            string fileName = "客户信息" + DateTime.Now.ToString("yyyyMMdd");
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "客户主档", null, "导出", null);
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName + ".xls");
            byte[] publicCustomerListExcelBytes = ExportFileBussinessService.CustomerExport(Code, Name);
            HttpContext.Current.Response.BinaryWrite(publicCustomerListExcelBytes);
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }

        /// <summary>
        /// 厂商导出
        /// SAM 2017年4月27日11:45:40
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="Code">代号</param>
        /// <param name="Type"></param>
        [HttpGet]
        [Authenticate]
        public void ManufacturerExport(string Token, string Code = null, string Type = null)
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            string fileName = "厂商信息" + DateTime.Now.ToString("yyyyMMdd");
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName + ".xls");
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "厂商主档", null, "导出", null);
            byte[] publicCustomerListExcelBytes = ExportFileBussinessService.ManufacturerExport(Code, Type);
            HttpContext.Current.Response.BinaryWrite(publicCustomerListExcelBytes);
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }

        /// <summary>
        /// 语系导出
        /// SAM 2017年5月3日11:19:50
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="Code">代号</param>

        [HttpGet]
        [Authenticate]
        public void Lan00000Export(string Token, string Code = null)
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            string fileName = "语系文件" + DateTime.Now.ToString("yyyyMMdd");
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName + ".xls");
            byte[] publicCustomerListExcelBytes = ExportFileBussinessService.Lan00000Export(Code);
            HttpContext.Current.Response.BinaryWrite(publicCustomerListExcelBytes);
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }




        /// <summary>
        /// 分类群码导出
        /// Tom 2017年5月3日22:32:41
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="Code">代号</param>
        /// <param name="Status">状态</param>
        [HttpGet]
        [Authenticate]
        public void Inf00009GroupCodeExport(string Token, string Code = null, int? Status = null)
        {
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "分类群组码主档", null, "导出", "Code:" + Code + "<-->Status:" + Status);
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            string fileName = "分类群码信息" + DateTime.Now.ToString("yyyyMMdd");
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName + ".xls");
            byte[] publicCustomerListExcelBytes = ExportFileBussinessService.Inf00009GroupCodeExport(Code, Status);
            HttpContext.Current.Response.BinaryWrite(publicCustomerListExcelBytes);
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }

        /// <summary>
        /// 分类导出
        /// Tom 2017年5月3日22:50:11
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="Code">代号</param>
        /// <param name="UseCode"></param>
        [HttpGet]
        [Authenticate]
        public void Inf00009ClassExport(string Token, string Code = null, string UseCode = null)
        {
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "分类主档", null, "导出", "Code:" + Code + "<-->UseCode:" + UseCode);
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            string fileName = "分类信息" + DateTime.Now.ToString("yyyyMMdd");
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName + ".xls");
            byte[] publicCustomerListExcelBytes = ExportFileBussinessService.Inf00009ClassExport(Code, UseCode);
            HttpContext.Current.Response.BinaryWrite(publicCustomerListExcelBytes);
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }

        /// <summary>
        /// 单位导出
        /// Tom 2017年5月4日11:48:51
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="Code">代号</param>
        [HttpGet]
        [Authenticate]
        public void Inf00011Export(string Token, string Code = null)
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            string fileName = "单位信息" + DateTime.Now.ToString("yyyyMMdd");
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName + ".xls");
            byte[] publicCustomerListExcelBytes = ExportFileBussinessService.Inf00011ExportList(Code);
            HttpContext.Current.Response.BinaryWrite(publicCustomerListExcelBytes);
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }



        /// <summary>
        /// 仓库导出
        /// SAM 2017年6月6日21:47:59
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="Code"></param>
        /// <param name="Status"></param>
        [HttpGet]
        [Authenticate]
        public void WarehouseExport(string Token, string Code = null, string Status = null)
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            string fileName = "仓库信息" + DateTime.Now.ToString("yyyyMMdd");
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName + ".xls");
            byte[] publicCustomerListExcelBytes = ExportFileBussinessService.WarehouseExport(Code, Status);
            HttpContext.Current.Response.BinaryWrite(publicCustomerListExcelBytes);
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }


        /// <summary>
        /// 班别导出
        /// Tom 2017年5月5日16:11:24
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="Code">代号</param>
        /// <param name="Status">状态</param>
        [HttpGet]
        [Authenticate]
        public void Inf00013Export(string Token, string Code = null, string Status = null)
        {
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "班别主档", null, "导出", "Code:" + Code + "<-->Status:" + Status);
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            string fileName = "班别信息" + DateTime.Now.ToString("yyyyMMdd");
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName + ".xls");
            byte[] publicCustomerListExcelBytes = ExportFileBussinessService.Inf00013ExportList(Code, Status);
            HttpContext.Current.Response.BinaryWrite(publicCustomerListExcelBytes);
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }


        /// <summary>
        /// 项目导出
        /// Tom 2017年5月9日12:12:45
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="Code">代号</param>
        [HttpGet]
        [Authenticate]
        public void Inf00019Export(string Token, string Code = null)
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            string fileName = "项目信息" + DateTime.Now.ToString("yyyyMMdd");
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName + ".xls");
            byte[] publicCustomerListExcelBytes = ExportFileBussinessService.Inf00019ExportList(Code);
            HttpContext.Current.Response.BinaryWrite(publicCustomerListExcelBytes);
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }


        /// <summary>
        /// 料品属性导出
        /// SAM 2017年5月10日11:05:01
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="Code">代号</param>
        [HttpGet]
        [Authenticate]
        public void Inf00024Export(string Token, string Code = null)
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            string fileName = "料品属性主档"+ DateTime.Now.ToString("yyyyMMdd");
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName + ".xls");
            byte[] publicCustomerListExcelBytes = ExportFileBussinessService.Inf00024Export(Code);
            HttpContext.Current.Response.BinaryWrite(publicCustomerListExcelBytes);
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }

        /// <summary>
        /// 账户导出
        /// SAM 2017年5月10日14:04:40
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="Code"></param>
        /// <param name="Status"></param>
        [HttpGet]
        [Authenticate]
        public void UserExport(string Token, string Code = null, string Status = null)
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            string fileName = "账户主档" +  DateTime.Now.ToString("yyyyMMdd");
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName + ".xls");
            byte[] publicCustomerListExcelBytes = ExportFileBussinessService.UserExport(Code, Status);
            HttpContext.Current.Response.BinaryWrite(publicCustomerListExcelBytes);
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }


        /// <summary>
        /// 原因群码导出
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="Code">代号</param>
        /// <param name="Status">状态</param>
        [HttpGet]
        [Authenticate]
        public void Inf00017GroupExport(string Token, string Code = null, string Status = null)
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            string fileName = "原因群码主档" + DateTime.Now.ToString("yyyyMMdd");
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName + ".xls");
            byte[] publicCustomerListExcelBytes = ExportFileBussinessService.Inf00017GroupExport(Code, Status);
            HttpContext.Current.Response.BinaryWrite(publicCustomerListExcelBytes);
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }

        /// <summary>
        /// 原因码导出
        /// SAM 2017年5月11日11:32:55
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="Code">代号</param>
        /// <param name="Status">状态</param>
        [HttpGet]
        [Authenticate]
        public void Inf00017Export(string Token, string Code = null, string Status = null)
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            string fileName = "原因码主档" + DateTime.Now.ToString("yyyyMMdd");
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName + ".xls");
            byte[] publicCustomerListExcelBytes = ExportFileBussinessService.Inf00017Export(Code, Status);
            HttpContext.Current.Response.BinaryWrite(publicCustomerListExcelBytes);
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }


        /// <summary>
        /// 用途别导出
        /// SAM  2017年5月12日10:17:03
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="Code">代号</param>
        /// <param name="Status">状态</param>
        [HttpGet]
        [Authenticate]
        public void USE00001Export(string Token, string Code = null, string Status = null)
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            string fileName = "用途别主档" + DateTime.Now.ToString("yyyyMMdd");
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName + ".xls");
            byte[] publicCustomerListExcelBytes = ExportFileBussinessService.USE00001Export(Code, Status);
            HttpContext.Current.Response.BinaryWrite(publicCustomerListExcelBytes);
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }


        /// <summary>
        /// 资源类别导出
        /// SAM 2017年5月12日10:44:05
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="Code">代号</param>
        /// <param name="Status">状态</param>
        [HttpGet]
        [Authenticate]
        public void Inf00015ClassExport(string Token, string Code = null, string Status = null)
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            string fileName = "资源类别主档" + DateTime.Now.ToString("yyyyMMdd");
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName + ".xls");
            byte[] publicCustomerListExcelBytes = ExportFileBussinessService.Inf00015ClassExport(Code, Status);
            HttpContext.Current.Response.BinaryWrite(publicCustomerListExcelBytes);
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }

        /// <summary>
        /// 资源群组导出
        /// SAM 2017年5月12日10:44:05
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="Code">代号</param>
        /// <param name="Status">状态</param>
        [HttpGet]
        [Authenticate]
        public void Inf00015GroupExport(string Token, string Code = null, string Status = null)
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            string fileName =  "资源群组主档"+ DateTime.Now.ToString("yyyyMMdd");
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName + ".xls");
            byte[] publicCustomerListExcelBytes = ExportFileBussinessService.Inf00015GroupExport(Code, Status);
            HttpContext.Current.Response.BinaryWrite(publicCustomerListExcelBytes);
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }


        /// <summary>
        /// 资源导出
        /// SAM 2017年5月12日14:34:51
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="Code">代号</param>
        /// <param name="Status">状态</param>
        [HttpGet]
        [Authenticate]
        public void Inf00015Export(string Token, string Code = null, string Status = null)
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            string fileName =  "资源主档" + DateTime.Now.ToString("yyyyMMdd");
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName + ".xls");
            byte[] publicCustomerListExcelBytes = ExportFileBussinessService.Inf00015Export(Code, Status);
            HttpContext.Current.Response.BinaryWrite(publicCustomerListExcelBytes);
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }


        /// <summary>
        /// 料品的导出
        /// SAM 2017年5月16日16:26:42
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="Code">代号</param>
        /// <param name="Status">状态</param>
        [HttpGet]
        [Authenticate]
        public void Inf00010Export(string Token, string Code = null, string Status = null)
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            string fileName = "料品主档"+ DateTime.Now.ToString("yyyyMMdd") ;
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName + ".xls");
            byte[] publicCustomerListExcelBytes = ExportFileBussinessService.Inf00010Export(Code, Status);
            HttpContext.Current.Response.BinaryWrite(publicCustomerListExcelBytes);
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }


        /// <summary>
        /// 批号自动编号设定的导出
        /// SAM 2017年5月17日16:11:47
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="Code">代号</param>
        [HttpGet]
        [Authenticate]
        public void Inf00023Export(string Token, string Code = null)
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            string fileName = "批号自动编号设定主档" + DateTime.Now.ToString("yyyyMMdd");
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName + ".xls");
            byte[] publicCustomerListExcelBytes = ExportFileBussinessService.Inf00023Export(Code);
            HttpContext.Current.Response.BinaryWrite(publicCustomerListExcelBytes);
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }



        /// <summary>
        /// 行事历的导出
        /// SAM 2017年5月18日11:21:35
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="Code">代号</param>
        [HttpGet]
        [Authenticate]
        public void Inf00014Export(string Token, string Code = null)
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            string fileName = "行事历主档" + DateTime.Now.ToString("yyyyMMdd");
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName + ".xls");
            byte[] publicCustomerListExcelBytes = ExportFileBussinessService.Inf00014Export(Code);
            HttpContext.Current.Response.BinaryWrite(publicCustomerListExcelBytes);
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }


        /// <summary>
        /// 部门的导出
        /// SAM 2017年5月18日23:51:27
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="Code">代号</param>
        /// <param name="Status">状态</param>
        [HttpGet]
        [Authenticate]
        public void Inf00005Export(string Token, string Code = null, string Status = null)
        {
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "部门主档", null, "导出", null);
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            string fileName = "部门主档"+  DateTime.Now.ToString("yyyyMMdd") ;
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName + ".xls");
            byte[] publicCustomerListExcelBytes = ExportFileBussinessService.Inf00005Export(Code, Status);
            HttpContext.Current.Response.BinaryWrite(publicCustomerListExcelBytes);
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }

        /// <summary>
        /// 设备的导出
        /// SAM 2017年5月22日17:32:49
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="Code">代号</param>
        /// <param name="Status">状态</param>
        [HttpGet]
        [Authenticate]
        public void Ems00001Export(string Token, string Code = null, string Status = null)
        {
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "设备主档", null, "导出", null);
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            string fileName = "设备主档" + DateTime.Now.ToString("yyyyMMdd");
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName + ".xls");
            byte[] publicCustomerListExcelBytes = ExportFileBussinessService.Ems00001Export(Code, Status);
            HttpContext.Current.Response.BinaryWrite(publicCustomerListExcelBytes);
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }


        /// <summary>
        /// 设备项目的导出
        /// SAM 2017年5月22日17:32:49
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="EquipmentID"></param>
        [HttpGet]
        [Authenticate]
        public void Ems00001ProjectExport(string Token, string EquipmentID)
        {
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "设备主档", null, "导出", null);
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            string fileName = "设备项目主档" + DateTime.Now.ToString("yyyyMMdd");
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName + ".xls");
            byte[] publicCustomerListExcelBytes = ExportFileBussinessService.Ems00001ProjectExport(EquipmentID);
            HttpContext.Current.Response.BinaryWrite(publicCustomerListExcelBytes);
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }



        /// <summary>
        /// 制程替代群组的导出
        /// SAM 2017年5月23日09:37:48
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="Code">代号</param>
        /// <param name="Status">状态</param>
        [HttpGet]
        [Authenticate]
        public void Inf00021Export(string Token, string Code = null, string Status = null)
        {
            DataLogerService.writeURL(Token, "Code:" + Code + "--->Status:" + Status);
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            string fileName = "制程替代群组主档" + DateTime.Now.ToString("yyyyMMdd")  ;
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName + ".xls");
            byte[] publicCustomerListExcelBytes = ExportFileBussinessService.Inf00021Export(Code, Status);
            HttpContext.Current.Response.BinaryWrite(publicCustomerListExcelBytes);
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }


        /// <summary>
        /// 感知器的导出
        /// SAM 2017年5月23日14:17:46
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="Code">代号</param>
        /// <param name="Status">状态</param>
        [HttpGet]
        [Authenticate]
        public void Iot00001Export(string Token, string Code = null, string Status = null)
        {
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "感知器主档", null, "导出", "Code:" + Code + "<==>Status:" + Status);
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            string fileName = "感知器主档"+ DateTime.Now.ToString("yyyyMMdd");
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName + ".xls");
            byte[] publicCustomerListExcelBytes = ExportFileBussinessService.Iot00001Export(Code, Status);
            HttpContext.Current.Response.BinaryWrite(publicCustomerListExcelBytes);
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }


        /// <summary>
        /// 工序的导出
        /// SAM 2017年5月25日11:02:05
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="Code">代号</param>
        /// <param name="Status">状态</param>
        [HttpGet]
        [Authenticate]
        public void Inf00018OperationExport(string Token, string Code = null, string Status = null)
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            string fileName = "工序主档" + DateTime.Now.ToString("yyyyMMdd");
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName + ".xls");
            byte[] publicCustomerListExcelBytes = ExportFileBussinessService.Inf00018OperationExport(Code, Status);
            HttpContext.Current.Response.BinaryWrite(publicCustomerListExcelBytes);
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }


        /// <summary>
        /// 制程的导出
        /// SAM 2017年5月25日11:02:05
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="Code">代号</param>
        /// <param name="Status">状态</param>
        [HttpGet]
        [Authenticate]
        public void Inf00018ProcessExport(string Token, string Code = null, string Status = null)
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            string fileName = "制程主档" +DateTime.Now.ToString("yyyyMMdd");
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName + ".xls");
            byte[] publicCustomerListExcelBytes = ExportFileBussinessService.Inf00018ProcessExport(Code, Status);
            HttpContext.Current.Response.BinaryWrite(publicCustomerListExcelBytes);
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }


        /// <summary>
        /// 工作中心的导出
        /// SAM 2017年5月25日11:02:05
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="Code">代号</param>
        /// <param name="Status">状态</param>
        [HttpGet]
        [Authenticate]
        public void Inf00018WorkCenterExport(string Token, string Code = null, string Status = null)
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            string fileName = "工作中心主档" + DateTime.Now.ToString("yyyyMMdd");
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName + ".xls");
            byte[] publicCustomerListExcelBytes = ExportFileBussinessService.Inf00018WorkCenterExport(Code, Status);
            HttpContext.Current.Response.BinaryWrite(publicCustomerListExcelBytes);
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }

        /// <summary>
        /// 单据子轨设定的导出
        /// SAM 2017年6月2日14:56:50
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="Code">代号</param>
        /// <param name="TypeCode"></param>
        /// <param name="Status">状态</param>
        [HttpGet]
        [Authenticate]
        public void Inf00016Export(string Token, string TypeCode = null, string Code = null, string Status = null)
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            string fileName = "单据子轨设定主档" + DateTime.Now.ToString("yyyyMMdd");
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName + ".xls");
            byte[] publicCustomerListExcelBytes = ExportFileBussinessService.Inf00016Export(TypeCode, Code, Status);
            HttpContext.Current.Response.BinaryWrite(publicCustomerListExcelBytes);
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }

        /// <summary>
        /// 检验群组码的导出
        /// SAM 2017年5月26日15:24:323
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="Code">代号</param>
        /// <param name="Status">状态</param>
        [HttpGet]
        [Authenticate]
        public void Qcs00003Export(string Token, string Code = null, string Status = null)
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            string fileName = "检验群组码主档" + DateTime.Now.ToString("yyyyMMdd");
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName + ".xls");
            byte[] publicCustomerListExcelBytes = ExportFileBussinessService.Qcs00003Export(Code, Status);
            HttpContext.Current.Response.BinaryWrite(publicCustomerListExcelBytes);
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }

        /// <summary>
        /// 抽样检验设定导出
        /// SAM 2017年6月6日14:28:37
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="Type"></param>
        [HttpGet]
        [Authenticate]
        public void Qcs00001Export(string Token, string Type = null)
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            string fileName = "抽样检验设定维护" + DateTime.Now.ToString("yyyyMMdd");
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName + ".xls");
            byte[] publicCustomerListExcelBytes = ExportFileBussinessService.Qcs00001Export(Type);
            HttpContext.Current.Response.BinaryWrite(publicCustomerListExcelBytes);
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }

        /// <summary>
        /// 设备巡检维护导出
        /// SAM 2017年6月8日16:31:54
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="Code"></param>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        [HttpGet]
        [Authenticate]
        public void Ems00003Export(string Token, string Code = null, string StartDate = null, string EndDate = null)
        {
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "设备巡检维护", null, "导出", "Code:" + Code + "--->StartDate:" + StartDate + "--->EndDate:" + EndDate);
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            string fileName = "设备巡检维护" + DateTime.Now.ToString("yyyyMMdd");
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName + ".xls");
            byte[] publicCustomerListExcelBytes = ExportFileBussinessService.Ems00003Export(Code, StartDate, EndDate);
            HttpContext.Current.Response.BinaryWrite(publicCustomerListExcelBytes);
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }


        /// <summary>
        /// 检验类别的导出
        /// SAM 2017年6月9日11:28:12
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="Code"></param>
        [HttpGet]
        [Authenticate]
        public void Qcs00002TypeExport(string Token, string Code = null)
        {
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "检验类别主档", null, "导出", "Code:" + Code);
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            string fileName = "检验类别主档" + DateTime.Now.ToString("yyyyMMdd");
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName + ".xls");
            byte[] publicCustomerListExcelBytes = ExportFileBussinessService.Qcs00002TypeExport(Code);
            HttpContext.Current.Response.BinaryWrite(publicCustomerListExcelBytes);
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }



        /// <summary>
        /// 检验项目的导出
        /// SAM 2017年6月11日12:38:21
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="Code"></param>
        /// <param name="Status"></param>
        [HttpGet]
        [Authenticate]
        public void Qcs00002ProjectExport(string Token, string Code = null, string Status = null)
        {
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "检验项目主档", null, "导出", "Code:" + Code);
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            string fileName = "检验项目主档" + DateTime.Now.ToString("yyyyMMdd");
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName + ".xls");
            byte[] publicCustomerListExcelBytes = ExportFileBussinessService.Qcs00002ProjectExport(Code, Status);
            HttpContext.Current.Response.BinaryWrite(publicCustomerListExcelBytes);
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }


        /// <summary>
        /// 客诉单的导出
        /// SAM 2017年6月15日10:21:04
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="StartCode"></param>
        /// <param name="EndCode"></param>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="StartCustCode"></param>
        /// <param name="EndCustCode"></param>
        /// <param name="StartOrderCode"></param>
        /// <param name="EndOrderCode"></param>
        /// <param name="Status"></param>
        [HttpGet]
        [Authenticate]
        public void Qcs00009Export(string Token, string StartCode = null, string EndCode = null, string StartDate = null,
          string EndDate = null, string StartCustCode = null, string EndCustCode = null, string Status = null, string StartOrderCode = null, string EndOrderCode = null)
        {
            //DataLogerService.writeURL(Token, "StartCode:" + StartCode + "EndCode:" + EndCode + "StartDate:" + StartDate + "EndDate:" + EndDate + "StartCustCode:" + StartCustCode + "EndCustCode:" + EndCustCode + "Status:" + Status+ "StartOrderCode:"+ StartOrderCode+ "EndOrderCode:"+ EndOrderCode);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "客诉单主档", null, "导出", "StartCode:" + StartCode + "EndCode:" + EndCode + "StartDate:" + StartDate + "EndDate:" + EndDate + "StartCustCode:" + StartCustCode + "EndCustCode:" + EndCustCode + "Status:" + Status + "StartOrderCode:" + StartOrderCode + "EndOrderCode:" + EndOrderCode);
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            string fileName = "客诉单主档" + DateTime.Now.ToString("yyyyMMdd");
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName + ".xls");
            byte[] publicCustomerListExcelBytes = ExportFileBussinessService.Qcs00009Export(StartCode, EndCode, StartDate, EndDate, StartCustCode, EndCustCode, Status, StartOrderCode, EndOrderCode);
            HttpContext.Current.Response.BinaryWrite(publicCustomerListExcelBytes);
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }

        /// <summary>
        /// 客诉单原因的导出
        /// SAM 2017年6月15日14:24:11
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="ComplaintDetailID"></param>
        /// <param name="GroupCode"></param>
        [HttpGet]
        [Authenticate]
        public void Qcs00010ReasonExport(string Token, string ComplaintDetailID, string GroupCode = null)
        {
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "客诉原因", null, "导出", "ComplaintDetailID:" + ComplaintDetailID + "GroupCode:" + GroupCode);
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            string fileName = "客诉原因" + DateTime.Now.ToString("yyyyMMdd");
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName + ".xls");
            byte[] publicCustomerListExcelBytes = ExportFileBussinessService.Qcs00010ReasonExport(ComplaintDetailID, GroupCode);
            HttpContext.Current.Response.BinaryWrite(publicCustomerListExcelBytes);
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }

        /// <summary>
        /// 保养项目的导出
        /// SAM 2017年7月5日15:48:41
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="Code"></param>
        /// <param name="Name"></param>
        [HttpGet]
        [Authenticate]
        public void Ems00008ProjectExport(string Token, string Code = null, string Name = null)
        {
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "保养项目", null, "导出", "Code:" + Code + "Name:" + Name);
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            string fileName = "保养项目主档" + DateTime.Now.ToString("yyyyMMdd");
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName + ".xls");
            byte[] publicCustomerListExcelBytes = ExportFileBussinessService.Ems00008ProjectExport(Code, Name);
            HttpContext.Current.Response.BinaryWrite(publicCustomerListExcelBytes);
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }

        /// <summary>
        /// 保养类型导出
        /// SAM 2017年7月5日15:55:30
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="Code"></param>
        /// <param name="Name"></param>
        [HttpGet]
        [Authenticate]
        public void Ems00008TypeExport(string Token, string Code = null, string Name = null)
        {
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "保养类型", null, "导出", "Code:" + Code + "Name:" + Name);
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            string fileName = "保养类型主档" + DateTime.Now.ToString("yyyyMMdd");
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName + ".xls");
            byte[] publicCustomerListExcelBytes = ExportFileBussinessService.Ems00008TypeExport(Code, Name);
            HttpContext.Current.Response.BinaryWrite(publicCustomerListExcelBytes);
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }

        /// <summary>
        /// 设备保养清单设定导出
        /// SAM 2017年7月5日16:31:20
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="Code"></param>
        /// <param name="Name"></param>
        [HttpGet]
        [Authenticate]
        public void Ems00008Export(string Token, string Code = null, string Name = null)
        {
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "设备保养清单设定", null, "导出", "Code:" + Code + "Name:" + Name);
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            string fileName = "保设备保养清单设定" + DateTime.Now.ToString("yyyyMMdd");
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName + ".xls");
            byte[] publicCustomerListExcelBytes = ExportFileBussinessService.Ems00008Export(Code, Name);
            HttpContext.Current.Response.BinaryWrite(publicCustomerListExcelBytes);
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }

        /// <summary>
        /// 制程检验维护导出
        /// Joint
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="InspectionNo"></param>
        /// <param name="Status"></param>
        [HttpGet]
        [Authenticate]
        public void Qcs00005Export(string Token, string InspectionNo = null, string Status = null)
        {
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "制程检验维护", null, "导出", null);
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            string fileName = "制程检验维护" + DateTime.Now.ToString("yyyyMMdd");
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName + ".xls");
            byte[] publicCustomerListExcelBytes = ExportFileBussinessService.Qcs00005Export(InspectionNo, Status);
            HttpContext.Current.Response.BinaryWrite(publicCustomerListExcelBytes);
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }

        /// <summary>
        /// 制程首件检验维护导出
        /// Joint
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="InspectionNo"></param>
        [HttpGet]
        [Authenticate]
        public void Qcs00007Export(string Token, string InspectionNo = null)
        {
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "制程首件检验维护", null, "导出", "InspectionNo:" + InspectionNo);
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            string fileName = "制程首件检验维护" + DateTime.Now.ToString("yyyyMMdd");
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName + ".xls");
            byte[] publicCustomerListExcelBytes = ExportFileBussinessService.Qcs00007Export(InspectionNo);
            HttpContext.Current.Response.BinaryWrite(publicCustomerListExcelBytes);
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }

        /// <summary>
        /// 制程巡检检验维护导出
        /// Joint
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="InspectionNo"></param>
        [HttpGet]
        [Authenticate]
        public void Qcs00008Export(string Token, string InspectionNo = null)
        {
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "制程巡检检验维护", null, "导出", "InspectionNo:" + InspectionNo);
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            string fileName = "制程巡检检验维护" + DateTime.Now.ToString("yyyyMMdd");
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName + ".xls");
            byte[] publicCustomerListExcelBytes = ExportFileBussinessService.Qcs00008Export(InspectionNo);
            HttpContext.Current.Response.BinaryWrite(publicCustomerListExcelBytes);
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }


        /// <summary>
        /// 完工单的导出
        /// SAM 2017年7月20日11:33:07
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="FinishNo">完工单，格式：单号</param>
        /// <param name="Date">完工日期</param>
        /// <param name="WorkCenterID">工作中心，格式：流水号</param>
        /// <param name="ProcessID">制程，格式：流水号</param>
        /// <param name="FabricatedMotherID">制令单号，格式：流水号</param>
        /// <param name="Status">状态，格式：流水号，流水号</param>
        [HttpGet]
        [Authenticate]
        public void Sfc00007Export(string Token, string FinishNo = null, string Date = null, 
            string WorkCenterID = null, string ProcessID = null, string FabricatedMotherID = null, string Status = null)
        {
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "完工单", null, "导出", "FinishNo:" + FinishNo + "--->Date:" + Date + "--->WorkCenterID:" + WorkCenterID + "--->ProcessID:" + ProcessID + "--->FabricatedMotherID:" + FabricatedMotherID + "--->Status:" + Status);
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            string fileName = "完工单" + DateTime.Now.ToString("yyyyMMdd");
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName + ".xls");
            byte[] publicCustomerListExcelBytes = ExportFileBussinessService.Sfc00007Export(FinishNo, Date, WorkCenterID, ProcessID, FabricatedMotherID, Status);
            HttpContext.Current.Response.BinaryWrite(publicCustomerListExcelBytes);
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }

        /// <summary>
        /// 调整单的导出
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="AdjustCode"></param>
        [HttpGet]
        [Authenticate]
        public void Sfc00008Export(string Token, string AdjustCode = null)
        {
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "完工调整作业", null, "导出", "AdjustCode:" + AdjustCode);
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            string fileName = "完工调整作业"+ DateTime.Now.ToString("yyyyMMdd");
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName + ".xls");
            byte[] publicCustomerListExcelBytes = ExportFileBussinessService.Sfc00008Export(AdjustCode);
            HttpContext.Current.Response.BinaryWrite(publicCustomerListExcelBytes);
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }

        /// <summary>
        /// 人工工時統計分析导出
        /// SAM 2017年7月22日18:45:21
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="StartWorkCenterCode"></param>
        /// <param name="EndWorkCenterCode"></param>
        /// <param name="StartUserCode"></param>
        /// <param name="EndUserCode"></param>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        [HttpGet]
        [Authenticate]
        public void Sfc00014Export(string Token, string StartWorkCenterCode = null, string EndWorkCenterCode = null,
           string StartUserCode = null, string EndUserCode = null,
           string StartDate = null, string EndDate = null)
        {
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "人工工時統計分析导出", null, "导出", null);
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            string fileName = "制程巡检检验维护" + DateTime.Now.ToString("yyyyMMdd");
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName + ".xls");
            byte[] publicCustomerListExcelBytes = ExportFileBussinessService.Sfc00014Export(StartWorkCenterCode, EndWorkCenterCode, StartUserCode, EndUserCode, StartDate, EndDate);
            HttpContext.Current.Response.BinaryWrite(publicCustomerListExcelBytes);
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }

        /// <summary>
        /// 机器工时统计分析导出
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="StartWorkCenterCode"></param>
        /// <param name="EndWorkCenterCode"></param>
        /// <param name="StartEquipmentCode"></param>
        /// <param name="EndEquipmentCode"></param>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        [HttpGet]
        [Authenticate]
        public void Sfc00015Export(string Token, string StartWorkCenterCode = null, string EndWorkCenterCode = null,
          string StartEquipmentCode = null, string EndEquipmentCode = null,
          string StartDate = null, string EndDate = null)
        {
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "机器工时统计分析导出", null, "导出", null);
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            string fileName = "制程巡检检验维护" + DateTime.Now.ToString("yyyyMMdd");
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName + ".xls");
            byte[] publicCustomerListExcelBytes = ExportFileBussinessService.Sfc00015Export(StartWorkCenterCode, EndWorkCenterCode, StartEquipmentCode, EndEquipmentCode, StartDate, EndDate);
            HttpContext.Current.Response.BinaryWrite(publicCustomerListExcelBytes);
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }

        /// <summary>
        /// 工作站导出
        /// Tom 2017年7月25日14:27:09
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="TaskNo"></param>
        /// <param name="MoNo"></param>
        /// <param name="WorkcenterID"></param>
        /// <param name="ProcessID"></param>
        /// <param name="OperationID"></param>
        /// <param name="ClassID"></param>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="Status"></param>
        [HttpGet]
        public void Sfc00006Export(string Token, string TaskNo = null, string MoNo = null, string WorkcenterID = null, string ProcessID = null,
            string OperationID = null, string ClassID = null, string StartDate = null, string EndDate = null, string Status = null)
        {
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "工作站", null, "导出", null);
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            string fileName ="工作站信息" + DateTime.Now.ToString("yyyyMMdd");
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName + ".xls");
            byte[] publicCustomerListExcelBytes = ExportFileBussinessService.Sfc00006Export(TaskNo, MoNo, WorkcenterID, ProcessID, OperationID, ClassID, StartDate, EndDate, Status);
            HttpContext.Current.Response.BinaryWrite(publicCustomerListExcelBytes);
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }

        /// <summary>
        /// 設備保養資料維護导出
        /// Joint 2017年8月1日16:52:03
        /// 
        /// SAM 2017年8月28日23:32:17
        /// 完善了导出
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="TypeCode"></param>
        /// <param name="StartCode"></param>
        /// <param name="EndCode"></param>
        /// <param name="Status"></param>
        /// <param name="UserCode"></param>
        /// <param name="EquipmentCode"></param>、
        [HttpGet]
        [Authenticate]
        public void Ems00010Export(string Token, string StartDate = null, string EndDate = null, 
            string TypeCode = null, string StartCode = null, string EndCode = null, string Status = null, 
            string UserCode = null, string EquipmentCode = null)
        {
            //DataLogerService.writeURL(Token,null);
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "設備保養資料維護", null, "导出", null);
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            string fileName = "設備保養資料維護"+ DateTime.Now.ToString("yyyyMMdd");
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName + ".xls");
            byte[] publicCustomerListExcelBytes = ExportFileBussinessService.Ems00010Export(StartDate, EndDate, TypeCode, StartCode, EndCode, Status, UserCode, EquipmentCode);
            HttpContext.Current.Response.BinaryWrite(publicCustomerListExcelBytes);
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }


        /// <summary>
        ///设备保养工单的导出
        ///SAM2017年8月2日15:34:17
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="Type"></param>
        /// <param name="Status"></param>
        /// <param name="EquipmentID"></param>
        /// <param name="UserID"></param>
        /// <param name="StartCode"></param>
        /// <param name="EndCode"></param>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        [HttpGet]
        public void Ems00009Export(string Token, string Type = null, string Status = null, string EquipmentID = null, 
            string UserID = null, string StartCode = null, string EndCode = null, string StartDate = null, string EndDate = null)
        {
            UtilBussinessService.WriteLog(UtilBussinessService.detoken(Token), "设备保养工单维护", null, "导出", null);
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            string fileName =  "设备保养工单维护"+ DateTime.Now.ToString("yyyyMMdd");
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName + ".xls");
            byte[] publicCustomerListExcelBytes = ExportFileBussinessService.Ems00009Export(Type, Status, EquipmentID, UserID, StartCode, EndCode, StartDate, EndDate);
            HttpContext.Current.Response.BinaryWrite(publicCustomerListExcelBytes);
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }
    }
}
