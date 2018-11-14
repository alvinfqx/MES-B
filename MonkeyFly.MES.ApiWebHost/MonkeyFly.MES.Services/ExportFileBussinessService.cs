using MonkeyFly.MES.Models;
using MonkeyFly.MES.ModelServices;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonkeyFly.MES.Services
{
    public class ExportFileBussinessService
    {
        /// <summary>
        /// 通用导出函数
        /// SAM 2017年4月26日16:44:52
        /// </summary>
        /// <param name="title"></param>
        /// <param name="dt"></param>
        /// <param name="columnTitles"></param>
        /// <returns></returns>
        public static byte[] CreateExcelByte(string title, DataTable dt, string[] columnTitles)
        {
            HSSFWorkbook workbook = new HSSFWorkbook();
            ICellStyle HeadercellStyle = workbook.CreateCellStyle();
            HeadercellStyle.BorderBottom = BorderStyle.Thin;
            HeadercellStyle.BorderLeft = BorderStyle.Thin;
            HeadercellStyle.BorderRight = BorderStyle.Thin;
            HeadercellStyle.BorderTop = BorderStyle.Thin;
            HeadercellStyle.Alignment = HorizontalAlignment.Center;
            // 字体
            IFont headerfont = workbook.CreateFont();
            headerfont.Boldweight = (short)FontBoldWeight.Bold;
            HeadercellStyle.SetFont(headerfont);

            // 标题
            ISheet sheet = workbook.CreateSheet(title);
            sheet.AddMergedRegion(new CellRangeAddress(0, 0, 0, columnTitles.Length - 1));
            IRow titleRow = sheet.CreateRow(0);
            ICell Topcell = titleRow.CreateCell(0);
            Topcell.SetCellValue(title);
            Topcell.CellStyle = HeadercellStyle;
            Topcell.CellStyle.Alignment = HorizontalAlignment.Center;

            // 表头
            IRow headerRow = sheet.CreateRow(1);
            int columnCount = columnTitles.Length;
            for (int i = 0; i < columnCount; i++)
            {
                ICell cell = headerRow.CreateCell(i);
                cell.CellStyle = HeadercellStyle;
                cell.SetCellValue(columnTitles[i]);
            }

            // 行内容
            ICellStyle cellStyle = workbook.CreateCellStyle();

            // 为避免日期格式被Excel自动替换，所以设定 format 为 『@』 表示一率当成text來看
            cellStyle.DataFormat = HSSFDataFormat.GetBuiltinFormat("@");
            cellStyle.BorderBottom = BorderStyle.Thin;
            cellStyle.BorderLeft = BorderStyle.Thin;
            cellStyle.BorderRight = BorderStyle.Thin;
            cellStyle.BorderTop = BorderStyle.Thin;
            cellStyle.Alignment = HorizontalAlignment.Center;

            IFont cellfont = workbook.CreateFont();
            cellfont.Boldweight = (short)FontBoldWeight.Normal;
            cellStyle.SetFont(cellfont);

            int iRowIndex = 2;
            int iCellIndex = 0;
            foreach (DataRow Rowitem in dt.Rows)
            {
                IRow DataRow = sheet.CreateRow(iRowIndex);
                foreach (DataColumn Colitem in dt.Columns)
                {
                    ICell cell = DataRow.CreateCell(iCellIndex);
                    cell.SetCellValue(Rowitem[Colitem].ToString());
                    cell.CellStyle = cellStyle;
                    iCellIndex++;
                }
                iCellIndex = 0;
                iRowIndex++;
            }

            for (int i = 0; i <= dt.Rows.Count; i++)
            {
                sheet.AutoSizeColumn(i);
            }
            // 获取当前列的宽度，然后对比本列的长度，取最大值
            for (int columnNum = 0; columnNum <= dt.Rows.Count; columnNum++)
            {
                int columnWidth = sheet.GetColumnWidth(columnNum) / 256;
                for (int rowNum = 1; rowNum <= sheet.LastRowNum; rowNum++)
                {
                    IRow currentRow;
                    // 当前行未被使用过
                    if (sheet.GetRow(rowNum) == null)
                        currentRow = sheet.CreateRow(rowNum);
                    else
                        currentRow = sheet.GetRow(rowNum);
                    if (currentRow.GetCell(columnNum) != null)
                    {
                        ICell currentCell = currentRow.GetCell(columnNum);
                        int length = Encoding.Default.GetBytes(currentCell.ToString()).Length;
                        if (columnWidth < length)
                            columnWidth = length;
                    }
                }
                sheet.SetColumnWidth(columnNum, columnWidth * 256);
            }

            MemoryStream ms = new MemoryStream();
            workbook.Write(ms);

            byte[] result = ms.ToArray();
            ms.Close();

            return result;
        }

        /// <summary>
        /// 导出厂别
        /// SAM 2017年4月26日16:51:51
        /// </summary>
        /// <param name="Code">代号</param>
        /// <returns></returns>
        public static byte[] PlantExport(string Code)
        {
            DataTable dt = SYS_OrganizationService.GetExportList(Code);
            return CreateExcelByte("厂别信息" + DateTime.Now.ToString("yyyyMMdd"), dt, new string[] {
                    "代号", "说明", "备注","状态",  "建立人员",  "建立日期",  "最后修改人员",  "最后修改日期"});
        }

        /// <summary>
        /// 导出厂区
        /// SAM 2017年4月26日16:52:49
        /// </summary>
        /// <param name="Code">代号</param>
        /// <returns></returns>
        public static byte[] PlantAreaExport(string Code)
        {
            DataTable dt = SYS_PlantAreaService.GetExportList(Code);
            return CreateExcelByte("厂别信息" + DateTime.Now.ToString("yyyyMMdd"), dt, new string[] {
                    "厂区代号", "厂区说明","归属厂别","厂别说明",   "备注","状态",  "建立人员",  "建立日期",  "最后修改人员",  "最后修改日期"});
        }

        /// <summary>
        /// 客户导出
        /// SAM 2017年4月27日11:14:23
        /// </summary>
        /// <param name="Code">代号</param>
        /// <returns></returns>
        public static byte[] CustomerExport(string Code, string Name)
        {
            DataTable dt = SYS_CustomersService.GetExportList(Code, Name);
            return CreateExcelByte("客户信息" + DateTime.Now.ToString("yyyyMMdd"), dt, new string[] {
                    "序号", "客户代号","客户名称","联络人","Email","业务员", "业务员姓名", "分类一","分类二","备注","状态", "建立人员",  "建立日期",  "最后修改人员",  "最后修改日期"});
        }

        /// <summary>
        /// 厂商导出
        /// SAM 2017年4月27日11:53:40
        /// </summary>
        /// <param name="Code">代号</param>
        /// <param name="Name"></param>
        /// <returns></returns>
        public static byte[] ManufacturerExport(string Code, string Type)
        {
            DataTable dt = SYS_ManufacturersService.GetExportList(Code, Type);
            return CreateExcelByte("厂商信息" + DateTime.Now.ToString("yyyyMMdd"), dt, new string[] {
                    "序号", "类型", "厂商代号","厂商名称","联络人",   "Email","业务员", "业务员姓名", "分类一","分类二","备注","状态", "建立人员",  "建立日期",  "最后修改人员",  "最后修改日期"});
        }

        /// <summary>
        /// 语序导出
        /// SAM 2017年5月3日11:20:38
        /// </summary>
        /// <param name="Code">代号</param>
        /// <returns></returns>
        public static byte[] Lan00000Export(string Code)
        {
            DataTable dt = SYS_ParameterService.GetLan00000ExportList(Code);
            return CreateExcelByte("语系文件" + DateTime.Now.ToString("yyyyMMdd"), dt, new string[] {
                    "序号", "语系代码", "语系说明","状态","备注", "建立人员",  "建立日期",  "最后修改人员",  "最后修改日期"});
        }

        /// <summary>
        /// 分类群码导出
        /// Tom 2017年5月3日22:33:35
        /// </summary>
        /// <param name="Code">代号</param>
        /// <param name="Status">状态</param>
        /// <returns></returns>
        public static byte[] Inf00009GroupCodeExport(string code, int? status)
        {
            DataTable dt = SYS_ParameterService.Inf00009GroupCodeExportList(code, status);
            return CreateExcelByte("分类群码信息" + DateTime.Now.ToString("yyyyMMdd"), dt, new string[] {
                   "序号", "群组代号", "群组说明", "备注","状态", "建立人员",  "建立日期",  "最后修改人员",  "最后修改日期"});
        }

        /// <summary>
        /// 分类导出
        /// Tom 2017年5月3日22:50:58
        /// </summary>
        /// <param name="Code">代号</param>
        /// <param name="useCode"></param>
        /// <returns></returns>
        public static byte[] Inf00009ClassExport(string code, string useCode)
        {
            DataTable dt = SYS_ParameterService.Inf00009ClassExport(code, useCode);
            return CreateExcelByte("分类信息" + DateTime.Now.ToString("yyyyMMdd"), dt, new string[] {
                  "序号",  "用途别	", "分类代号", "分类说明", "备注", "状态", "群组代号", "群组说明", "建立人员",  "建立日期",  "最后修改人员",  "最后修改日期"});
        }

        /// <summary>
        /// 单位导出
        /// Tom 2017年5月3日23:11:31
        /// </summary>
        /// <param name="Code">代号</param>
        /// <returns></returns>
        public static byte[] Inf00011ExportList(string code)
        {
            DataTable dt = SYS_ParameterService.Inf00011ExportList(code);
            return CreateExcelByte("单位信息" + DateTime.Now.ToString("yyyyMMdd"), dt, new string[] {
                   "序号", "单位代号", "单位说明", "备注", "状态", "建立人员",  "建立日期",  "最后修改人员",  "最后修改日期"});
        }

        /// <summary>
        /// 导出仓库
        /// SAM 2017年5月4日11:48:26
        /// </summary>
        /// <param name="Code">代号</param>
        /// <returns></returns>
        public static byte[] WarehouseExport(string Code, string Status)
        {
            DataTable dt = SYS_OrganizationService.Inf00012GetExportList(Code, Status);
            return CreateExcelByte("仓库信息" + DateTime.Now.ToString("yyyyMMdd"), dt, new string[] {
                  "序号", "仓库代号", "仓库说明", "隶属厂别","备注","状态",  "建立人员",  "建立日期",  "最后修改人员",  "最后修改日期"});
        }

        /// <summary>
        /// 导出账号
        /// SAM 2017年5月4日15:42:51
        /// </summary>
        /// <param name="Code">代号</param>
        /// <param name="Status">状态</param>
        /// <returns></returns>
        public static byte[] UserExport(string Code, string Status)
        {
            DataTable dt = SYS_MESUserService.Inf00003GetExportList(Code, Status);
            return CreateExcelByte("账号信息" + DateTime.Now.ToString("yyyyMMdd"), dt, new string[] {
                  "序号", "账号", "姓名", "英文名","性别","出生日期","工号","部门","身份证","手机号码","E-mail","职工类型","入职时间","卡号","备注","状态"});
        }

        /// <summary>
        /// 班别导出
        /// Tom 2017年5月5日16:12:23
        /// </summary>
        /// <param name="Code">代号</param>
        /// <returns></returns>
        public static byte[] Inf00013ExportList(string code, string status)
        {
            DataTable dt = SYS_ClassService.GetExportList(code, status);
            return CreateExcelByte("班别信息" + DateTime.Now.ToString("yyyyMMdd"), dt, new string[] {
                  "序号", "班别代号", "班别说明", "备注","跨日","上班时间","下班时间","休息时数","工作时数","状态","建立人员",  "建立日期",  "最后修改人员",  "最后修改日期" });
        }

        /// <summary>
        /// 项目导出
        /// Tom 2017年5月9日12:11:45
        /// </summary>
        /// <param name="Code">代号</param>
        /// <returns></returns>
        public static byte[] Inf00019ExportList(string code)
        {
            DataTable dt = SYS_ProjectsService.GetExportList(code);
            return CreateExcelByte("项目信息" + DateTime.Now.ToString("yyyyMMdd"), dt, new string[] {
                 "序号", "项目代号", "项目说明", "属性", "备注", "状态","建立人员",  "建立日期",  "最后修改人员",  "最后修改日期"
            });
        }

        /// <summary>
        /// 导出料品属性导出
        /// SAM 2017年5月10日11:05:47
        /// </summary>
        /// <param name="Code">代号</param>
        /// <returns></returns>
        public static byte[] Inf00024Export(string Code)
        {
            DataTable dt = SYS_ParameterService.Inf00024Export(Code);
            return CreateExcelByte("料品属性主档" + DateTime.Now.ToString("yyyyMMdd"), dt, new string[] {
                  "序号", "属性代号", "说明", "手动输入","备注", "建立人员",  "建立日期",  "最后修改人员",  "最后修改日期"});
        }

        /// <summary>
        /// 原因群码导出
        /// SAM 2017年5月11日10:52:10
        /// </summary>
        /// <param name="Code">代号</param>
        /// <param name="Status">状态</param>
        /// <returns></returns>
        public static byte[] Inf00017GroupExport(string Code, string Status)
        {
            DataTable dt = SYS_ParameterService.Inf00017GroupExport(Code, Status);
            return CreateExcelByte("原因群码主档" + DateTime.Now.ToString("yyyyMMdd"), dt, new string[] {
                  "序号", "原因群码代号", "说明", "备注","状态", "建立人员",  "建立日期",  "最后修改人员",  "最后修改日期"});
        }

        /// <summary>
        /// 原因码导出
        /// SAM 2017年5月11日11:25:47
        /// </summary>
        /// <param name="Code">代号</param>
        /// <param name="Status">状态</param>
        /// <returns></returns>
        public static byte[] Inf00017Export(string Code, string Status)
        {
            DataTable dt = SYS_ParameterService.Inf00017Export(Code, Status);
            return CreateExcelByte("原因码主档" + DateTime.Now.ToString("yyyyMMdd"), dt, new string[] {
                  "序号",  "用途","原因码代号", "原因码说明", "原因群码", "备注","状态", "建立人员",  "建立日期",  "最后修改人员",  "最后修改日期"});
        }


        /// <summary>
        /// 用途别导出
        /// SAM 2017年5月12日10:18:16
        /// </summary>
        /// <param name="Code">代号</param>
        /// <param name="Status">状态</param>
        /// <returns></returns>
        public static byte[] USE00001Export(string Code, string Status)
        {
            DataTable dt = SYS_ParameterService.USE00001Export(Code, Status);
            return CreateExcelByte("用途别主档" + DateTime.Now.ToString("yyyyMMdd"), dt, new string[] {
                  "序号", "用途别代号", "说明", "备注","状态", "建立人员",  "建立日期",  "最后修改人员",  "最后修改日期"});
        }

        /// <summary>
        /// 资源类别导出
        /// SAM 2017年5月12日10:18:16
        /// </summary>
        /// <param name="Code">代号</param>
        /// <param name="Status">状态</param>
        /// <returns></returns>
        public static byte[] Inf00015ClassExport(string Code, string Status)
        {
            DataTable dt = SYS_ParameterService.Inf00015ClassExport(Code, Status);
            return CreateExcelByte("资源类别主档" + DateTime.Now.ToString("yyyyMMdd"), dt, new string[] {
                  "序号", "资源类别代号", "资源类别名称", "备注","状态", "建立人员",  "建立日期",  "最后修改人员",  "最后修改日期"});
        }

        /// <summary>
        /// 资源群组导出
        /// SAM 2017年5月12日11:02:01
        /// </summary>
        /// <param name="Code">代号</param>
        /// <param name="Status">状态</param>
        /// <returns></returns>
        public static byte[] Inf00015GroupExport(string Code, string Status)
        {
            DataTable dt = SYS_ParameterService.Inf00015GroupExport(Code, Status);
            return CreateExcelByte("资源群组主档" + DateTime.Now.ToString("yyyyMMdd"), dt, new string[] {
                  "序号", "资源群组代号", "资源群组名称", "备注","状态", "建立人员",  "建立日期",  "最后修改人员",  "最后修改日期"});
        }


        /// <summary>
        /// 资源导出
        /// SAM 2017年5月12日14:35:55
        /// </summary>
        /// <param name="Code">代号</param>
        /// <param name="Status">状态</param>
        /// <returns></returns>
        public static byte[] Inf00015Export(string Code, string Status)
        {
            DataTable dt = SYS_ResourcesService.Inf00015Export(Code, Status);
            return CreateExcelByte("资源主档" + DateTime.Now.ToString("yyyyMMdd"), dt, new string[] {
                  "序号", "资源类别",  "资源代号", "资源名称", "资源数量", "资源群组","备注","状态", "建立人员",  "建立日期",  "最后修改人员",  "最后修改日期"});
        }


        /// <summary>
        /// 料品的导出
        /// SAM 2017年5月17日09:24:37
        /// </summary>
        /// <param name="Code">代号</param>
        /// <param name="Status">状态</param>
        /// <returns></returns>
        public static byte[] Inf00010Export(string Code, string Status)
        {
            DataTable dt = SYS_ItemsService.Inf00010Export(Code, Status);
            return CreateExcelByte("料品主档" + DateTime.Now.ToString("yyyyMMdd"), dt, new string[] {
                  "序号", "料品代号",  "料品名称", "料品规格", "状态", "单位","分类一","分类二", "分类三",  "分类四",
                  "分类五", "辅助单位",  "辅助单位比", "切除尾数", "切除尾数小数", "供应形态","工程图号","料品来源",
                    "备注", "条码",  "检验群组码", "批号控管", "批号类别", "组批方式","超完工比率（%）","序号件","关键料号",
                  "建立人员",  "建立日期", "最后修改人员",  "最后修改日期"});
        }


        /// <summary>
        /// 批号自动编号设定的导出
        /// SAM 2017年5月17日16:30:35
        /// </summary>
        /// <param name="Code">代号</param>
        /// <param name="Status">状态</param>
        /// <returns></returns>
        public static byte[] Inf00023Export(string Code)
        {
            DataTable dt = SYS_AutoNumberService.Inf00023Export(Code);
            return CreateExcelByte("批号自动编号设定主档" + DateTime.Now.ToString("yyyyMMdd"), dt, new string[] {
                  "序号", "类别代号",  "类别说明", "预设字元", "年度码", "月份码","日期码","流水号长度", "批号长度",
                    "备注", "建立人员",  "建立日期", "最后修改人员",  "最后修改日期"});
        }


        /// <summary>
        /// 行事历的导出
        /// SAM 2017年5月18日11:32:20
        /// </summary>
        /// <param name="Code">代号</param>
        /// <returns></returns>
        public static byte[] Inf00014Export(string Code)
        {
            DataTable dt = SYS_CalendarService.Inf00014Export(Code);
            SYS_Calendar Cal = null;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dt.Rows[i][0] = i + 1;//为序号赋值
                Cal = SYS_CalendarService.getByCode(dt.Rows[i][1].ToString());//根据代号获取行事历
                if (Cal != null)
                {
                    //根据行事历和年月获取那个月的具体信息
                    IList<Hashtable> Details = SYS_CalendarDetailsService.Inf00014GetMonthList(Cal.CalendarID, dt.Rows[i][10].ToString());
                    for (int j = 0; j < Details.Count; j++)
                    {
                        dt.Rows[i][11 + j] = Details[j]["Wkhour"];//为序号赋值
                    }
                }
            }
            return CreateExcelByte("行事历主档" + DateTime.Now.ToString("yyyyMMdd"), dt, new string[] {
                  "序号", "行事历代号",  "行事历名称", "主行事历", "状态","备注", "建立人员",  "建立日期", "最后修改人员",  "最后修改日期","日期",
            "1", "2", "3", "4", "5", "6", "7", "8", "9", "10",
            "11", "12", "13", "14", "15", "16", "17", "18", "19", "20",
            "21", "22", "23", "24", "25", "26", "27", "28", "29", "30","31"});
        }

        /// <summary>
        /// 部门的导出
        /// SAM 2017年5月18日23:52:12
        /// </summary>
        /// <param name="Code">代号</param>
        /// <returns></returns>
        public static byte[] Inf00005Export(string Code, string Status)
        {
            DataTable dt = SYS_OrganizationService.Inf00005Export(Code, Status);
            return CreateExcelByte("部门主档" + DateTime.Now.ToString("yyyyMMdd"), dt, new string[] {
                  "序号", "部门代号",  "部门说明", "备注", "厂别", "最上层注记", "上层部门", "状态", "建立人员",  "建立日期", "最后修改人员",  "最后修改日期"});
        }

        /// <summary>
        /// 设备的导出
        /// SAM 2017年5月22日17:34:06
        /// </summary>
        /// <param name="Code">代号</param>
        /// <param name="Status">状态</param>
        /// <returns></returns>
        public static byte[] Ems00001Export(string Code, string Status)
        {
            DataTable dt = EMS_EquipmentService.Ems00001Export(Code, Status);
            return CreateExcelByte("设备主档" + DateTime.Now.ToString("yyyyMMdd"), dt, new string[] {
                  "序号", "设备代号",  "设备名称", "资源类别", "状态", "机况","异常状态", "固定资产编号", "厂区代号", "（厂区）说明",
                  "厂别代号", "（厂别）说明",  "备注", "购入日期", "购买厂商", "（购买厂商）名称", "有效期", "机型", "机台序号",
                  "设备分类一", "设备分类二",  "标准产能（HR）", "累计产出数", "累计使用时间（分）", "可用时间（分）", "模出数", "累计使用次数", "可用次数",
                  "是否统计次数", "保管部门",  "（保管部门）名称", "保养周期（日）", "保养周期（次）", "说明一", "说明二", "日期一", "日期二",
                  "数值一", "数值二",  "建立人员",  "建立日期", "最后修改人员",  "最后修改日期"});
        }

        /// <summary>
        /// 设备项目的导出
        /// SAM 2017年5月23日10:41:31
        /// </summary>
        /// <param name="EquipmentID"></param>
        /// <returns></returns>
        public static byte[] Ems00001ProjectExport(string EquipmentID)
        {
            DataTable dt = EMS_EquipmentProjectService.Ems00001ProjectExport(EquipmentID);
            return CreateExcelByte("设备项目主档" + DateTime.Now.ToString("yyyyMMdd"), dt, new string[] {
                  "序号", "设备代号",  "设备说明", "备注", "项目代号", "项目说明", "项目属性", "是否收集", "收集方式",
                  "感知器代号", "感知器说明",  "标准值", "上限值", "下限值", "上限警告秒数", "下限警告秒数","上限警告数值", "下限警告数值", "备注", "建立人员",  "建立日期", "最后修改人员",  "最后修改日期"});
        }


        /// <summary>
        /// 制程替代群组的导出
        /// SAM 2017年5月23日09:38:24
        /// </summary>
        /// <param name="Code">代号</param>
        /// <param name="Status">状态</param>
        /// <returns></returns>
        public static byte[] Inf00021Export(string Code, string Status)
        {
            DataTable dt = SYS_ParameterService.Inf00021Export(Code, Status);
            return CreateExcelByte("制程替代群组主档" + DateTime.Now.ToString("yyyyMMdd"), dt, new string[] {
                  "序号", "制程替代群组", "制程替代群组说明", "备注","状态", "建立人员",  "建立日期",  "最后修改人员",  "最后修改日期"});
        }


        /// <summary>
        /// 感知器的导出
        /// SAM 2017年5月23日14:21:00
        /// </summary>
        /// <param name="Code">代号</param>
        /// <param name="Status">状态</param>
        /// <returns></returns>
        public static byte[] Iot00001Export(string Code, string Status)
        {
            DataTable dt = IOT_SensorService.Iot00001Export(Code, Status);
            return CreateExcelByte("设备项目主档" + DateTime.Now.ToString("yyyyMMdd"), dt, new string[] {
                  "序号", "感知器代号",  "感知器说明", "备注", "状态", "启用日期", "失效日期", "品牌", "型号",
                  "厂商代号", "厂商名称", "上限值", "下限值",   "画面是否警示", "上限警告秒数", "下限警告秒数",  "建立人员",  "建立日期", "最后修改人员",  "最后修改日期"});
        }


        /// <summary>
        /// 工序的导出
        /// SAM 2017年5月23日14:21:00
        /// </summary>
        /// <param name="Code">代号</param>
        /// <param name="Status">状态</param>
        /// <returns></returns>
        public static byte[] Inf00018OperationExport(string Code, string Status)
        {
            DataTable dt = SYS_ParameterService.GeneralExport("0191213000016", Code, Status);
            return CreateExcelByte("工序主档" + DateTime.Now.ToString("yyyyMMdd"), dt, new string[] {
                  "序号", "工序代号",  "工序说明", "备注", "状态", "建立人员",  "建立日期", "最后修改人员",  "最后修改日期"});
        }

        /// <summary>
        /// 制程的导出
        /// SAM 2017年5月25日10:20:05
        /// </summary>
        /// <param name="Code">代号</param>
        /// <param name="Status">状态</param>
        /// <returns></returns>
        public static byte[] Inf00018ProcessExport(string Code, string Status)
        {
            DataTable dt = SYS_ParameterService.Inf00018OProcessExport(Code, Status);
            return CreateExcelByte("制程主档" + DateTime.Now.ToString("yyyyMMdd"), dt, new string[] {
                  "序号", "制程代号",  "制程说明","启用工序", "状态", "备注", "建立人员",  "建立日期", "最后修改人员",  "最后修改日期"});
        }


        /// <summary>
        /// 工作中心的导出
        /// SAM 2017年5月25日11:09:20
        /// </summary>
        /// <param name="Code">代号</param>
        /// <param name="Status">状态</param>
        /// <returns></returns>
        public static byte[] Inf00018WorkCenterExport(string Code, string Status)
        {
            DataTable dt = SYS_WorkCenterService.Inf00018OWorkCenterExport(Code, Status);
            return CreateExcelByte("工作中心主档" + DateTime.Now.ToString("yyyyMMdd"), dt, new string[] {
                  "序号", "工作中心代号",  "工作中心说明","内外制","部门/厂商", "（部门/厂商）名称", "行事历代号", "启用班别","资源报工","派工模式", "状态", "备注", "建立人员",  "建立日期", "最后修改人员",  "最后修改日期"});
        }


        /// <summary>
        /// 检验群组码的导出
        /// SAM 2017年5月26日15:25:46
        /// </summary>
        /// <param name="Code">代号</param>
        /// <param name="Status">状态</param>
        /// <returns></returns>
        public static byte[] Qcs00003Export(string Code, string Status)
        {
            DataTable dt = SYS_ParameterService.GeneralExport("0191213000018", Code, Status);
            return CreateExcelByte("检验群组码主档" + DateTime.Now.ToString("yyyyMMdd"), dt, new string[] {
                  "序号", "检验群码",  "检验群码说明", "备注", "状态", "建立人员",  "建立日期", "最后修改人员",  "最后修改日期"});
        }

        /// <summary>
        /// 单据子轨设定的导出
        /// SAM 2017年6月2日14:58:19
        /// </summary>
        /// <param name="TypeCode"></param>
        /// <param name="Code">代号</param>
        /// <param name="Status">状态</param>
        /// <returns></returns>
        public static byte[] Inf00016Export(string TypeCode, string Code, string Status)
        {
            DataTable dt = SYS_DocumentTypeSettingService.Inf00016Export(TypeCode, Code, Status);
            return CreateExcelByte("单据子轨设定主档" + DateTime.Now.ToString("yyyyMMdd"), dt, new string[] {
                  "序号", "单据种别代号",  "单据种别名称","类别代号","说明", "给号方式", "年度码", "月份码","日期码","流水号长度","单据号码长度","年碼格式","预设","状态", "属性", "备注", "建立人员",  "建立日期", "最后修改人员",  "最后修改日期"});
        }

        /// <summary>
        /// 抽样检验设定导出
        /// SAM 2017年6月6日14:29:21
        /// </summary>
        /// <param name="Type"></param>
        /// <returns></returns>
        public static byte[] Qcs00001Export(string Type)
        {
            DataTable dt = QCS_CheckTestSettingService.Qcs00001Export(Type);
            return CreateExcelByte("抽样检验设定维护" + DateTime.Now.ToString("yyyyMMdd"), dt, new string[] {
                  "序号", "检验编号",  "检验水准","检验方式", "AQL", "备注", "状态", "序号", "起始批量", "结束批量",  "抽样数", "Ac数量", "Re数量"});
        }

        /// <summary>
        /// 设备巡检维护导出
        /// SAM 2017年6月8日16:33:01
        /// </summary>
        /// <param name="Code"></param>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <returns></returns>
        public static byte[] Ems00003Export(string Code, string StartDate, string EndDate)
        {
            DataTable dt = EMS_EquipmentInspectionRecordService.Ems00003Export(Code, StartDate, EndDate);
            return CreateExcelByte("设备巡检维护" + DateTime.Now.ToString("yyyyMMdd"), dt, new string[] {
                  "序号", "巡检单号",  "设备代号", "设备名称", "日期", "班别", "记录人", "任务单","制品代号", "制品名称", "排序", "设备项目", "项目说明", "现值", "标准值",  "上限", "下限", "备注", "建立人员",  "建立日期", "最后修改人员",  "最后修改日期"});
        }

        /// <summary>
        /// 检验类别的导出
        /// SAM 2017年6月9日11:30:00
        /// </summary>
        /// <param name="Code"></param>
        /// <returns></returns>
        public static byte[] Qcs00002TypeExport(string Code)
        {
            DataTable dt = QCS_SamplingSettingService.Qcs00002TypeExport(Code);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dt.Rows[i]["RowNumber"] = i + 1;
            }
            return CreateExcelByte("检验类别主档" + DateTime.Now.ToString("yyyyMMdd"), dt, new string[] {
                  "序号", "类别代号",  "类别说明", "备注", "建立人员",  "建立日期", "最后修改人员",  "最后修改日期",
                  "抽验方式", "A级严重", "B级严重", "主要","次要", "建立人员",  "建立日期", "最后修改人员",  "最后修改日期", });
        }

        /// <summary>
        /// 检验项目的导出
        /// SAM 2017年6月11日13:13:431
        /// </summary>
        /// <param name="Code"></param>
        /// <param name="Status"></param>
        /// <returns></returns>
        public static byte[] Qcs00002ProjectExport(string Code, string Status)
        {
            DataTable dt = QCS_InspectionProjectService.Qcs00002ProjectExport(Code, Status);
            return CreateExcelByte("检验项目主档" + DateTime.Now.ToString("yyyyMMdd"), dt, new string[] {
                  "序号", "检验项目代号",  "检验项目说明", "实测值设定", "检验标准", "检验水准", "缺点等级", "备注", "状态",  "建立人员",  "建立日期", "最后修改人员",  "最后修改日期"});
        }


        /// <summary>
        /// 客诉单的导出
        /// SAM 2017年6月15日10:21:26
        /// </summary>
        /// <param name="StartCode"></param>
        /// <param name="EndCode"></param>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="StartCustCode"></param>
        /// <param name="EndCustCode"></param>
        /// <param name="Status"></param>
        /// <returns></returns>
        public static byte[] Qcs00009Export(string StartCode, string EndCode, string StartDate, string EndDate,
            string StartCustCode, string EndCustCode, string Status, string StartOrderCode, string EndOrderCode)
        {
            DataTable dt = QCS_ComplaintService.Qcs00009Export(StartCode, EndCode, StartDate, EndDate, StartCustCode, EndCustCode, Status, StartOrderCode, EndOrderCode);
            return CreateExcelByte("客诉单主档" + DateTime.Now.ToString("yyyyMMdd"), dt, new string[] {
                  "序号", "客诉单号",  "单据日期", "客户代号", "客户名称", "投诉人", "状态", "备注", "申请人员", "姓名",  "建立人员",  "建立日期", "最后修改人员",  "最后修改日期",  "序号",  "料品代号", "批号",  "出货单号",  "订单号码",  "客诉数量",  "客诉说明",  "状态",  "料品名称",  "料品规格"});
        }

        /// <summary>
        /// 客诉单原因的导出
        /// SAM 2017年6月15日14:25:13
        /// </summary>
        /// <param name="ComplaintDetailID"></param>
        /// <param name="GroupCode"></param>
        /// <returns></returns>
        public static byte[] Qcs00010ReasonExport(string ComplaintDetailID, string GroupCode)
        {
            DataTable dt = QCS_ComplaintReasonService.Qcs00010ReasonExport(ComplaintDetailID, GroupCode);
            return CreateExcelByte("客诉原因" + DateTime.Now.ToString("yyyyMMdd"), dt, new string[] {
                  "序号", "客诉单号",  "原因群码", "不良原因", "原因说明", "客诉数量"});
        }

        /// <summary>
        /// 保养项目的导出
        /// SAM 2017年7月5日15:49:59
        /// </summary>
        /// <param name="Code"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static byte[] Ems00008ProjectExport(string Code, string name)
        {
            DataTable dt = SYS_ParameterService.Ems00008ProjectExport(Code, name);
            return CreateExcelByte("保养项目主档" + DateTime.Now.ToString("yyyyMMdd"), dt, new string[] {
                  "序号", "保养项目",  "保养说明", "输入属性",  "备注", "状态", "建立人员",  "建立日期", "最后修改人员",  "最后修改日期"});
        }

        /// <summary>
        /// 保养类型导出
        /// SAM 2017年7月5日15:55:06
        /// </summary>
        /// <param name="Code"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static byte[] Ems00008TypeExport(string Code, string name)
        {
            DataTable dt = SYS_ParameterService.Ems00008TypeExport(Code, name);
            return CreateExcelByte("保养类型主档" + DateTime.Now.ToString("yyyyMMdd"), dt, new string[] {
                  "序号", "保养类型",  "保养说明", "备注", "状态",  "建立人员",  "建立日期", "最后修改人员",  "最后修改日期"});
        }

        /// <summary>
        /// 设备保养清单设定导出
        /// SAM 2017年7月5日16:32:20
        /// </summary>
        /// <param name="Code"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static byte[] Ems00008Export(string Code, string name)
        {
            DataTable dt = EMS_EquipmentMaintenanceListService.Ems00008Export(Code, name);
            return CreateExcelByte("设备保养清单设定主档" + DateTime.Now.ToString("yyyyMMdd"), dt, new string[] {
                  "序号", "清单代号",  "清册名称", "保养类型", "保养项目","项目说明", "输入属性",  "备注"});
        }
        /// <summary>
        /// 制程检验维护导出
        /// Joint
        /// </summary>
        /// <param name="InspectionNo"></param>
        /// <returns></returns>
        public static byte[] Qcs00005Export(string InspectionNo, string Status)
        {
            DataTable dt = QCS_InspectionDocumentService.Qcs00005Export(InspectionNo, Status);
            return CreateExcelByte("制程检验维护主档" + DateTime.Now.ToString("yyyyMMdd"), dt, new string[] {
                 "序号","检验单号","单据日期","检验种类","完工单号","完工序号","料品代号","料品名称/规格","任务单单号","单据数量","免检","单位","检验人员","检验日期","品质判定","检验数量","报废数量","NG数量","状态","可转移量","制令单号","制程","工序","备注","建立人员","建立日期","最后修改人员","最后修改日期",
                "序号","排序","检验项目代号","检验项目说明","检验标准","缺点等级","抽样数量","AQL","AC","RE","不良数","实测值","判定","备注" });
        }

        /// <summary>
        /// 制程首件检验维护导出
        /// Joint
        /// </summary>
        /// <param name="InspectionNo"></param>
        /// <returns></returns>
        public static byte[] Qcs00007Export(string InspectionNo)
        {
            DataTable dt = QCS_InspectionDocumentService.Qcs00007Export(InspectionNo);
            return CreateExcelByte("制程首件检验维护主档" + DateTime.Now.ToString("yyyyMMdd"), dt, new string[] {
                 "序号","检验单号","单据日期","检验种类","任务单单号","料品代号","料品名称/规格","单据数量","免检","单位","检验人员","检验日期","品质判定","检验数量","报废数量","NG数量","OK数量","状态","制令单号","制程代号","工序代号","备注","建立人员","建立日期","最后修改人员","最后修改日期",
                 "序号","排序","检验项目代号","检验项目说明","检验标准","缺点等级","抽样数量","AQL","AC","RE","不良数","实测值","判定","备注","建立人员","建立日期","最后修改人员","最后修改日期" });
        }

        /// <summary>
        /// 制程巡检检验维护导出
        /// Joint
        /// </summary>
        /// <param name="InspectionNo"></param>
        /// <returns></returns>
        public static byte[] Qcs00008Export(string InspectionNo)
        {
            DataTable dt = QCS_InspectionDocumentService.Qcs00008Export(InspectionNo);
            return CreateExcelByte("制程首件检验维护主档" + DateTime.Now.ToString("yyyyMMdd"), dt, new string[] {
                "序号","检验单号","单据日期","检验种类","任务单单号","料品代号","料品名称/规格","单据数量","免检","单位","检验人员","检验日期","品质判定","检验数量","报废数量","NG数量","OK数量","状态","制令单号","制程","工序","备注","建立人员","建立日期","最后修改人员","最后修改日期",
                 "序号","排序","检验项目代号","检验项目说明","检验标准","缺点等级","抽样数量","AQL","AC","RE","不良数","实测值","判定","备注","建立人员","建立日期","最后修改人员","最后修改日期"});
        }


        /// <summary>
        /// 完工单的导出
        /// SAM 2017年7月20日11:41:35
        /// </summary>
        /// <param name="FinishNo"></param>
        /// <param name="EndWorkD"></param>
        /// <returns></returns>
        public static byte[] Sfc00007Export(string FinishNo, string Date, string WorkCenterID, string ProcessID, string FabricatedMotherID, string Status)
        {
            DataTable dt = SFC_CompletionOrderService.Sfc00007Export(FinishNo, Date, WorkCenterID, ProcessID, FabricatedMotherID, Status);
            return CreateExcelByte("完工单" + DateTime.Now.ToString("yyyyMMdd"), dt, new string[] {
                 "序号","完工單號","完工日期","任务单号","工单号码","料号","料品名称","规格","制造单位","辅助单位","辅助单位比率","可完工量",
                "完工数量","报废量","差异量","返修量","有效人工工时","有效机器工时","无效人工工时","无效机器工时","备注","状态","创建人","创建时间","修改人","修改时间" });
        }

        /// <summary>
        /// 调整单的导出
        /// SAM 2017年9月18日10:16:22
        /// </summary>
        /// <param name="FinishNo"></param>
        /// <returns></returns>
        public static byte[] Sfc00008Export(string FinishNo)
        {
            DataTable dt = SFC_CompletionOrderService.Sfc00008Export(FinishNo);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dt.Rows[i][17] = UtilBussinessService.HourConversionStr(dt.Rows[i][17].ToString());
                dt.Rows[i][18] = UtilBussinessService.HourConversionStr(dt.Rows[i][18].ToString());
                dt.Rows[i][19] = UtilBussinessService.HourConversionStr(dt.Rows[i][19].ToString());
                dt.Rows[i][20] = UtilBussinessService.HourConversionStr(dt.Rows[i][20].ToString());
            }
            return CreateExcelByte("完工调整作业" + DateTime.Now.ToString("yyyyMMdd"), dt, new string[] {
                 "序号","完工单号","完工日期","原完工单号","任务单号","工单号码","料号","料品名称","规格","制造单位","辅助单位","辅助单位比率","可完工量",
                "完工数量","报废量","差异量","返修量","有效人工工时","有效机器工时","无效人工工时","无效机器工时","备注","状态","创建人","创建时间","修改人","修改时间" });
        }


        /// <summary>
        /// 人工工時統計分析导出
        /// SAM 2017年7月22日18:50:12
        /// </summary>
        /// <param name="StartWorkCenterCode"></param>
        /// <param name="EndWorkCenterCode"></param>
        /// <param name="StartUserCode"></param>
        /// <param name="EndUserCode"></param>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <returns></returns>
        public static byte[] Sfc00014Export(string StartWorkCenterCode, string EndWorkCenterCode, string StartUserCode, string EndUserCode, string StartDate, string EndDate)
        {
            DataTable dt = SFC_CompletionResourceService.Sfc00014Export(StartWorkCenterCode, EndWorkCenterCode, StartUserCode, EndUserCode, StartDate, EndDate);
            return CreateExcelByte("完工单" + DateTime.Now.ToString("yyyyMMdd"), dt, new string[] {
                 "序号","工作中心","中心說明","部門","部門名稱","員工工號","姓名","員工部門","部門名稱","有效人工工時","無效人工工時","總人工工時" });
        }

        /// <summary>
        /// 机器工时统计分析导出
        /// SAM 2017年7月22日18:51:53
        /// </summary>
        /// <param name="StartWorkCenterCode"></param>
        /// <param name="EndWorkCenterCode"></param>
        /// <param name="StartEquipmentCode"></param>
        /// <param name="EndEquipmentCode"></param>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <returns></returns>
        public static byte[] Sfc00015Export(string StartWorkCenterCode, string EndWorkCenterCode, string StartEquipmentCode, string EndEquipmentCode, string StartDate, string EndDate)
        {
            DataTable dt = SFC_CompletionResourceService.Sfc00015Export(StartWorkCenterCode, EndWorkCenterCode, StartEquipmentCode, EndEquipmentCode, StartDate, EndDate);
            return CreateExcelByte("完工单" + DateTime.Now.ToString("yyyyMMdd"), dt, new string[] {
                 "序号","工作中心","中心說明","部門","部門名稱","設備代號","設備名稱","有效機器工時","無效機器工時","總機器工時" });
        }

        /// <summary>
        /// 工作站导出
        /// Tom 2017年7月25日14:45:30
        /// </summary>
        /// <param name="taskNo"></param>
        /// <param name="moNo"></param>
        /// <param name="workcenterID"></param>
        /// <param name="processID"></param>
        /// <param name="operationID"></param>
        /// <param name="classID"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public static byte[] Sfc00006Export(string taskNo, string moNo, string workcenterID, string processID, string operationID, string classID, string startDate, string endDate, string status)
        {
            DataTable dt = SFC_TaskDispatchService.Sfc00006GetExprotList(taskNo, moNo, workcenterID, processID, operationID, classID, startDate, endDate, status);
            return CreateExcelByte("工作站信息" + DateTime.Now.ToString("yyyyMMdd"), dt, new string[] {
                "任務卡單號", "工單單號", "料號", "料品名稱", "料品規格", "分派數量", "完工量", "報廢量", "報廢量", "返修量", "開工日期", "完工日期", "備註",
                "下一站制程序号", "下一站制程代号", "下一站制程名称", "下一站工序序号", "下一站工序代号", "下一站工序名称",
                "状态", "建立人", "建立时间", "修改人", "修改时间"});
        }
        /// <summary>
        /// 設備保養資料維護导出
        /// Joint 2017年8月1日17:00:11
        /// </summary>
        /// <param name="MainternanceDate"></param>
        /// <param name="Type"></param>
        /// <param name="MaintenanceNo"></param>
        /// <param name="Status"></param>
        /// <param name="Emplno"></param>
        /// <param name="CodeName"></param>
        /// <returns></returns>
        public static byte[] Ems00010Export(string StartDate, string EndDate, string TypeCode, string StartCode, string EndCode, string Status, string UserCode, string EquipmentCode)
        {
            //DataTable dt = EMS_MaintenanceOrderService.Ems00010Export(StartMaintenanceDate, EndMaintenanceDate, Type, StartMaintenanceNo, EndMaintenanceNo, Status, Emplno, Code);
            DataTable dt = EMS_MaiOrderProjectService.Ems00010Export(StartDate, EndDate, TypeCode, StartCode, EndCode, Status, UserCode, EquipmentCode);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dt.Rows[i][0] = i + 1;//为序号赋值
            }
            return CreateExcelByte("設備保養資料維護" + DateTime.Now.ToString("yyyyMMdd"), dt, new string[] {
                "序号","保养日期","保养类型","保养单号","状态","序号","设备代号","设备名称","负责人工号","姓名","保养部门","保养厂商",
                "保管部门","机型","机器序号","资产代号","有效期","保养项目","项目说明","输入属性","实际值","备注" });
        }


        /// <summary>
        /// 设备保养工单的导出
        /// SAM 2017年8月2日15:34:25
        /// </summary>
        /// <param name="Type"></param>
        /// <param name="Status"></param>
        /// <param name="EquipmentID"></param>
        /// <param name="UserID"></param>
        /// <param name="StartCode"></param>
        /// <param name="EndCode"></param>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <returns></returns>
        public static byte[] Ems00009Export(string Type, string Status, string EquipmentID, string UserID, string StartCode, string EndCode, string StartDate, string EndDate)
        {
            DataTable dt = EMS_MaintenanceOrderService.Ems00009Export(Type, Status, EquipmentID, UserID, StartCode, EndCode, StartDate, EndDate);
            return CreateExcelByte("设备保养工单维护" + DateTime.Now.ToString("yyyyMMdd"), dt, new string[] {
                "序号","保养单号","单据日期","保养日期","保养类型","保养清单","保养部门","保养厂商","负责人工号","姓名","备注","状态","建立人员", "建立日期", "最后修改人员", "最后修改日期","序号","设备代号","设备说明","保管部门","状态","机型","有效期","建立人员", "建立日期", "最后修改人员", "最后修改日期" });
        }

    }

}
