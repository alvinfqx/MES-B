using MonkeyFly.Core;
using MonkeyFly.MES.BasicService;
using MonkeyFly.MES.Models;
using MonkeyFly.MES.ModelServices;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;
using System.Drawing;
using Newtonsoft.Json.Linq;
using System.Drawing.Imaging;

namespace MonkeyFly.MES.Services
{
    public class ImportFileBussinessService
    {
        /// <summary>
        /// 导入厂别
        /// SAM 2017年4月26日16:36:35
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static object plantImport(string token, string filename)
        {
            string userId = UtilBussinessService.detoken(token);
            string SysID = Framework.SystemID;
            string status = "200", msg = "共{0}条信息,{1}条信息导入成功,{2}条信息导入失败！{3}";
            string reason = null;
            int total = 0;
            int success = 0;
            int nosuccess = 0;
            if (filename.EndsWith(".xls") || filename.EndsWith(".xlsx"))
            {
                IWorkbook wk = null;
                SYS_Organization model = null;
                using (FileStream fs = File.OpenRead(@filename))   //打开.xls文件
                {
                    if (filename.EndsWith(".xls"))
                        wk = new HSSFWorkbook(fs); //把xls文件中的数据写入wk中
                    else if (filename.EndsWith(".xlsx"))
                        wk = new XSSFWorkbook(fs);//把xlsx文件中的数据写入wk中

                    ISheet sheet = wk.GetSheetAt(0);   //读取第一表数据
                    for (int j = 2; j <= sheet.LastRowNum; j++)  //LastRowNum 是当前表的总行数
                    {
                        IRow row = sheet.GetRow(j);  //读取当前行数据
                        if (row.GetCell(0) == null) //如果当前行的第一列没有数据，停止读取
                            break;
                        //如果当前行的第一列没有数据，停止读取，这是判断当row.GetCell不能null然后实际是空字符
                        if (string.IsNullOrWhiteSpace(row.GetCell(0).ToString()))
                            break;
                        total++;
                        ICell cell = null;
                        if (row != null)
                        {
                            model = new SYS_Organization();

                            cell = row.GetCell(0);
                            if (cell != null)
                                model.Code = cell.ToString();
                            cell = row.GetCell(1);

                            if (cell != null)
                            {
                                model.Name = cell.ToString();
                            }
                            else
                            {
                                reason += "代号[" + model.Code + "]:说明为空;";
                                nosuccess++;
                                continue;
                            }
                            cell = row.GetCell(2);
                            if (cell != null)
                                model.Comments = cell.ToString();
                            cell = row.GetCell(3);
                            if (cell != null)
                            {
                                if (cell.ToString() == "正常")
                                    model.Status = 1;
                                else if (cell.ToString() == "作废")
                                    model.Status = 0;
                                else
                                {
                                    model.Status = 1;
                                    reason += "代号[" + model.Code + "]的状态错误,不存在的状态;";
                                }
                            }
                            else
                            {
                                model.Status = 1;
                                reason += "代号[" + model.Code + "]的状态为空;";
                            }
                        }
                        if (!SYS_OrganizationService.CheckCode(model.Code, SysID + "020121300001E", null))
                        {
                            model.OrganizationID = UniversalService.GetSerialNumber("SYS_Organization");
                            model.ParentOrganizationID = "0";
                            model.Type = SysID + "020121300001E";
                            if (SYS_OrganizationService.insert(userId, model))
                                success++;
                            else
                            {
                                reason += "代号[" + model.Code + "]:添加失败，未知原因;";
                                nosuccess++;
                            }
                        }
                        else
                        {
                            reason += "代号[" + model.Code + "]重复;";
                            nosuccess++;
                        }
                    }

                }
            }

            if (nosuccess == total)
                status = "410";

            msg = string.Format(msg, total, success, nosuccess, reason);

            return new { status = status, msg = msg };
        }

        /// <summary>
        /// 导入厂区
        /// SAM 2017年4月26日16:37:38
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static object PlantAreaImport(string token, string filename)
        {
            string userId = UtilBussinessService.detoken(token);
            string SysID = Framework.SystemID;
            string status = "200", msg = "共{0}条信息,{1}条信息导入成功,{2}条信息导入失败！{3}";
            string reason = null;
            int total = 0;
            int success = 0;
            int nosuccess = 0;
            if (filename.EndsWith(".xls") || filename.EndsWith(".xlsx"))
            {
                IWorkbook wk = null;
                bool Isright = true;
                SYS_PlantArea model = null;
                using (FileStream fs = File.OpenRead(@filename))   //打开.xls文件
                {
                    if (filename.EndsWith(".xls"))
                        wk = new HSSFWorkbook(fs); //把xls文件中的数据写入wk中
                    else if (filename.EndsWith(".xlsx"))
                        wk = new XSSFWorkbook(fs);//把xlsx文件中的数据写入wk中

                    ISheet sheet = wk.GetSheetAt(0);   //读取第一表数据
                    for (int j = 2; j <= sheet.LastRowNum; j++)  //LastRowNum 是当前表的总行数
                    {
                        IRow row = sheet.GetRow(j);  //读取当前行数据
                        if (row.GetCell(0) == null) //如果当前行的第一列没有数据，停止读取
                            break;
                        if (string.IsNullOrWhiteSpace(row.GetCell(0).ToString()))//如果当前行的第一列没有数据，停止读取，这是判断当row.GetCell不能null然后实际是空字符
                            break;
                        total++;
                        ICell cell = null;
                        Isright = true;
                        if (row != null)
                        {
                            Isright = true;
                            model = new SYS_PlantArea();

                            cell = row.GetCell(0);
                            if (cell != null)
                                model.Code = cell.ToString();
                            cell = row.GetCell(1);
                            if (cell != null)
                            {
                                model.Name = cell.ToString();
                            }
                            else
                            {
                                reason += "代号[" + model.Code + "]说明为空;";
                                break;
                            }

                            cell = row.GetCell(2);//厂别代号
                            if (cell != null)
                            {
                                SYS_Organization orgModel = SYS_OrganizationService.getByCode(cell.ToString(), SysID + "020121300001E");
                                if (orgModel != null)
                                    model.PlantID = orgModel.OrganizationID;
                            }
                            cell = row.GetCell(4);
                            if (cell != null)
                                model.Comments = cell.ToString();
                            cell = row.GetCell(5);
                            if (cell != null)
                            {
                                if (cell.ToString() == "正常")
                                    model.Status = SysID + "0201213000001";
                                else
                                    model.Status = SysID + "0201213000002";
                            }
                        }
                        if (Isright)
                        {
                            if (!SYS_PlantAreaService.CheckCode(model.Code, null))
                            {
                                model.PlantAreaID = UniversalService.GetSerialNumber("SYS_PlantArea");
                                if (SYS_PlantAreaService.insert(userId, model))
                                    success++;
                                else
                                {
                                    reason += "代号[" + model.Code + "]:添加失败，未知原因;";
                                    nosuccess++;
                                }
                            }
                            else
                            {
                                reason += "代号[" + model.Code + "]重复;";
                                nosuccess++;
                            }
                        }
                        else
                            nosuccess++;
                    }

                }
            }

            if (nosuccess == total)
                status = "410";

            msg = string.Format(msg, total, success, nosuccess, reason);

            return new { status = status, msg = msg };
        }

        /// <summary>
        /// 客户导入
        /// SAM 2017年4月27日10:35:11
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static object CustomerImport(string token, string filename)
        {
            string userId = UtilBussinessService.detoken(token);
            string SysID = Framework.SystemID;
            string status = "200", msg = "共{0}条信息,{1}条信息导入成功,{2}条信息导入失败！{3}";
            string reason = null;
            int total = 0;
            int success = 0;
            int nosuccess = 0;
            if (filename.EndsWith(".xls") || filename.EndsWith(".xlsx"))
            {
                IWorkbook wk = null;
                SYS_Customers model = null;
                using (FileStream fs = File.OpenRead(@filename))   //打开.xls文件
                {
                    if (filename.EndsWith(".xls"))
                        wk = new HSSFWorkbook(fs); //把xls文件中的数据写入wk中
                    else if (filename.EndsWith(".xlsx"))
                        wk = new XSSFWorkbook(fs);//把xlsx文件中的数据写入wk中

                    ISheet sheet = wk.GetSheetAt(0);   //读取第一表数据
                    for (int j = 2; j <= sheet.LastRowNum; j++)  //LastRowNum 是当前表的总行数
                    {
                        IRow row = sheet.GetRow(j);  //读取当前行数据
                        if (row.GetCell(0) == null) //如果当前行的第一列没有数据，停止读取
                            break;
                        if (string.IsNullOrWhiteSpace(row.GetCell(0).ToString()))//如果当前行的第一列没有数据，停止读取，这是判断当row.GetCell不能null然后实际是空字符
                            break;
                        total++;
                        ICell cell = null;
                        if (row != null)
                        {
                            model = new SYS_Customers();

                            cell = row.GetCell(0);
                            if (cell != null)
                            {
                                if (cell.ToString().Length <= 20)
                                    model.Code = cell.ToString();
                                else
                                {
                                    reason += "代号[" + model.Code + "]超过了20个字;";
                                    nosuccess++;
                                    continue; //结束本次循环
                                }
                            }
                            cell = row.GetCell(1);
                            if (cell != null)
                            {
                                if (cell.ToString().Length <= 120)
                                    model.Name = cell.ToString();
                                else
                                {
                                    reason += "代号[" + model.Code + "]:客户名称超过了120个字;";
                                    nosuccess++;
                                    continue; //结束本次循环
                                }
                            }
                            else
                            {
                                reason += "代号[" + model.Code + "]说明为空;";
                                nosuccess++;
                                continue; //结束本次循环
                            }
                            cell = row.GetCell(2);
                            if (cell != null)
                                model.Contacts = cell.ToString();
                            cell = row.GetCell(3);
                            if (cell != null)
                            {
                                if (UtilBussinessService.IsEmail(cell.ToString()))
                                    model.Email = cell.ToString();
                                else
                                    reason += "代号[" + model.Code + "]邮箱格式错误;";
                            }

                            cell = row.GetCell(4);//业务员代号
                            if (cell != null)
                            {
                                SYS_MESUsers MESModel = SYS_MESUserService.getByCode(cell.ToString());
                                if (MESModel != null)
                                    model.MESUserID = MESModel.MESUserID;
                                else
                                {
                                    reason += "代号[" + model.Code + "]的业务员代号错误,不存在的业务员;";
                                }
                            }
                            cell = row.GetCell(6);//分类一
                            if (cell != null)
                            {
                                SYS_Parameters OneModel = SYS_ParameterService.getByCode(cell.ToString(), Framework.SystemID + "019121300000B");
                                if (OneModel != null)
                                    model.ClassOne = OneModel.ParameterID;
                                else
                                {
                                    reason += "代号[" + model.Code + "]的分类一错误,不存在的分类;";
                                }
                            }
                            cell = row.GetCell(7);//分类二
                            if (cell != null)
                            {
                                SYS_Parameters TwoModel = SYS_ParameterService.getByCode(cell.ToString(), Framework.SystemID + "019121300000B");
                                if (TwoModel != null)
                                    model.ClassTwo = TwoModel.ParameterID;
                                else
                                {
                                    reason += "代号[" + model.Code + "]的分类二错误,不存在的分类;";
                                }
                            }

                            cell = row.GetCell(8);//备注
                            if (cell != null)
                            {
                                if (cell.ToString().Length <= 150)
                                    model.Comments = cell.ToString();
                                else
                                {
                                    reason += "代号[" + model.Code + "]:备注超过150个字;";
                                }
                            }

                            cell = row.GetCell(9);
                            if (cell != null)
                            {
                                if (cell.ToString() == "正常")
                                    model.Status = SysID + "0201213000001";
                                else if (cell.ToString() == "作废")
                                    model.Status = SysID + "0201213000002";
                                else
                                {
                                    model.Status = SysID + "0201213000001";
                                    reason += "代号[" + model.Code + "]的状态错误,不存在的状态;";
                                }

                            }
                            else
                            {
                                model.Status = SysID + "0201213000001";
                                reason += "代号[" + model.Code + "]的状态错误,不存在的状态;";
                            }
                        }

                        if (!SYS_CustomersService.CheckCode(model.Code, null))
                        {
                            model.CustomerID = UniversalService.GetSerialNumber("SYS_Customers");
                            if (SYS_CustomersService.insert(userId, model))
                                success++;
                            else
                            {
                                reason += "代号[" + model.Code + "]:添加失败，未知原因;";
                                nosuccess++;
                            }
                        }
                        else
                        {
                            reason += "代号[" + model.Code + "]重复;";
                            nosuccess++;
                        }
                    }

                }
            }

            if (nosuccess == total)
                status = "410";

            msg = string.Format(msg, total, success, nosuccess, reason);

            return new { status = status, msg = msg };
        }

        /// <summary>
        /// 厂商导入
        /// SAM 2017年4月27日11:46:27
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static object ManufacturerImport(string token, string filename)
        {
            string userId = UtilBussinessService.detoken(token);
            string SysID = Framework.SystemID;
            string status = "200", msg = "共{0}条信息,{1}条信息导入成功,{2}条信息导入失败！{3}";
            string reason = null;
            int total = 0;
            int success = 0;
            int nosuccess = 0;
            if (filename.EndsWith(".xls") || filename.EndsWith(".xlsx"))
            {
                IWorkbook wk = null;
                string Sequence = null;
                SYS_Manufacturers model = null;
                using (FileStream fs = File.OpenRead(@filename))   //打开.xls文件
                {
                    if (filename.EndsWith(".xls"))
                        wk = new HSSFWorkbook(fs); //把xls文件中的数据写入wk中
                    else if (filename.EndsWith(".xlsx"))
                        wk = new XSSFWorkbook(fs);//把xlsx文件中的数据写入wk中
                    ISheet sheet = wk.GetSheetAt(0);   //读取第一表数据
                    for (int j = 2; j <= sheet.LastRowNum; j++)  //LastRowNum 是当前表的总行数
                    {
                        IRow row = sheet.GetRow(j);  //读取当前行数据
                        if (row.GetCell(0) == null) //如果当前行的第一列没有数据，停止读取
                            break;
                        if (string.IsNullOrWhiteSpace(row.GetCell(0).ToString()))//如果当前行的第一列没有数据，停止读取，这是判断当row.GetCell不能null然后实际是空字符
                            break;
                        total++;
                        ICell cell = null;
                        if (row != null)
                        {
                            model = new SYS_Manufacturers();

                            cell = row.GetCell(0);//序号
                            Sequence = cell.ToString();

                            cell = row.GetCell(1);//类型
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                SYS_Parameters TypeModel = SYS_ParameterService.getByName(cell.ToString(), Framework.SystemID + "0191213000050");
                                if (TypeModel != null)
                                    model.Type = TypeModel.ParameterID;
                                else
                                {
                                    reason += "序号[" + Sequence + "]:类型错误，不存在类型的类型;";
                                    nosuccess++;
                                    continue; //结束本次循环
                                }
                            }
                            else
                            {
                                reason += "序号[" + Sequence + "]:类型不能为空;";
                                nosuccess++;
                                continue; //结束本次循环
                            }

                            cell = row.GetCell(2);//代号
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                if (cell.ToString().Length <= 30)
                                    model.Code = cell.ToString();
                                else
                                {
                                    reason += "序号[" + Sequence + "]:代号超过30个字;";
                                    nosuccess++;
                                    continue; //结束本次循环
                                }
                            }
                            else
                            {
                                reason += "序号[" + Sequence + "]:代号不能为空;";
                                nosuccess++;
                                continue; //结束本次循环
                            }

                            cell = row.GetCell(3); //说明
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                if (cell.ToString().Length <= 120)
                                    model.Name = cell.ToString();
                                else
                                {
                                    reason += "序号[" + Sequence + "]:说明超过120个字;";
                                    nosuccess++;
                                    continue; //结束本次循环
                                }
                            }
                            else
                            {
                                reason += "序号[" + Sequence + "]:说明不能为空;";
                                nosuccess++;
                                continue; //结束本次循环
                            }
                            cell = row.GetCell(4); //联络人
                            if (cell != null)
                                model.Contacts = cell.ToString();

                            cell = row.GetCell(5);//邮箱
                            if (cell != null)
                            {
                                if (UtilBussinessService.IsEmail(cell.ToString()))
                                    model.Email = cell.ToString();
                                else
                                    reason += "序号[" + Sequence + "]:邮箱格式错误;";
                            }

                            cell = row.GetCell(6);//业务员代号
                            if (cell != null)
                            {
                                SYS_MESUsers MESModel = SYS_MESUserService.getByCode(cell.ToString());
                                if (MESModel != null)
                                    model.MESUserID = MESModel.MESUserID;
                                else
                                {
                                    reason += "序号[" + Sequence + "]:业务员代号错误,不存在的代号;";
                                }
                            }

                            cell = row.GetCell(7);//业务员名字
                            if (cell != null)
                            {
                                SYS_MESUsers MESModel = SYS_MESUserService.getByName(cell.ToString());
                                if (MESModel != null)
                                    model.MESUserID = MESModel.MESUserID;
                                else
                                {
                                    reason += "序号[" + Sequence + "]:业务员名字错误,不存在的名字;";
                                }
                            }

                            cell = row.GetCell(8);//分类一
                            if (cell != null)
                            {
                                SYS_Parameters OneModel = SYS_ParameterService.getByCode(cell.ToString(), Framework.SystemID + "019121300000B");
                                if (OneModel != null)
                                    model.ClassOne = OneModel.ParameterID;
                                else
                                {
                                    reason += "序号[" + Sequence + "]:分类一错误,不存在的分类;";
                                }
                            }

                            cell = row.GetCell(9);//分类二
                            if (cell != null)
                            {
                                SYS_Parameters TwoModel = SYS_ParameterService.getByCode(cell.ToString(), Framework.SystemID + "019121300000B");
                                if (TwoModel != null)
                                    model.ClassTwo = TwoModel.ParameterID;
                                else
                                {
                                    reason += "序号[" + Sequence + "]:分类二错误,不存在的分类;";
                                }
                            }
                            cell = row.GetCell(10);
                            if (cell != null)
                                model.Comments = cell.ToString();

                            cell = row.GetCell(11);
                            if (cell != null)
                            {
                                if (cell.ToString() == "1")
                                    model.Status = SysID + "0201213000001";
                                else if (cell.ToString() == "0")
                                    model.Status = SysID + "0201213000002";
                                else
                                {
                                    reason += "序号[" + Sequence + "]:状态错误,不存在的状态;";
                                    model.Status = SysID + "0201213000001";
                                }
                            }
                        }

                        if (!SYS_ManufacturersService.CheckCode(model.Code, null))
                        {
                            model.ManufacturerID = UniversalService.GetSerialNumber("SYS_Manufacturers");
                            if (SYS_ManufacturersService.insert(userId, model))
                                success++;
                            else
                            {
                                reason += "序号[" + Sequence + "]:添加失败，未知原因;";
                                nosuccess++;
                            }
                        }
                        else
                        {
                            reason += "序号[" + Sequence + "]:代号重复;";
                            nosuccess++;
                        }
                    }
                }
            }
            if (nosuccess == total)
                status = "410";

            msg = string.Format(msg, total, success, nosuccess, reason);

            return new { status = status, msg = msg };
        }

        /// <summary>
        /// 语序导入
        /// SAM 2017年5月3日11:08:09
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static object Lan00000Import(string token, string filename)
        {
            string userId = UtilBussinessService.detoken(token);
            string SysID = Framework.SystemID;
            string status = "200", msg = "共{0}条信息,{1}条信息导入成功,{2}条信息导入失败！{3}";
            string typeID = Framework.SystemID + "019121300000A";
            string reason = null;
            int total = 0;
            int success = 0;
            int nosuccess = 0;
            if (filename.EndsWith(".xls") || filename.EndsWith(".xlsx"))
            {
                IWorkbook wk = null;
                string Sequence = null;
                SYS_Parameters model = null;
                using (FileStream fs = File.OpenRead(@filename))   //打开.xls文件
                {
                    if (filename.EndsWith(".xls"))
                        wk = new HSSFWorkbook(fs); //把xls文件中的数据写入wk中
                    else if (filename.EndsWith(".xlsx"))
                        wk = new XSSFWorkbook(fs);//把xlsx文件中的数据写入wk中

                    ISheet sheet = wk.GetSheetAt(0);   //读取第一表数据
                    for (int j = 2; j <= sheet.LastRowNum; j++)  //LastRowNum 是当前表的总行数
                    {
                        IRow row = sheet.GetRow(j);  //读取当前行数据
                        if (row.GetCell(0) == null) //如果当前行的第一列没有数据，停止读取
                            break;
                        if (string.IsNullOrWhiteSpace(row.GetCell(0).ToString()))//如果当前行的第一列没有数据，停止读取，这是判断当row.GetCell不能null然后实际是空字符
                            break;
                        total++;
                        ICell cell = null;

                        if (row != null)
                        {
                            model = new SYS_Parameters();

                            cell = row.GetCell(0);//序号
                            Sequence = cell.ToString();

                            cell = row.GetCell(1);//代号
                            if (cell != null)
                                model.Code = cell.ToString();

                            cell = row.GetCell(2);//说明
                            if (cell != null)
                                model.Name = cell.ToString();
                            else
                            {
                                reason += "序号[" + Sequence + "]:说明为空;";
                                nosuccess++;
                                continue; //结束本次循环
                            }
                            cell = row.GetCell(3);//状态
                            if (cell != null)
                            {
                                if (cell.ToString() == "1")
                                    model.IsEnable = 1;
                                else if (cell.ToString() == "0")
                                    model.IsEnable = 0;
                                else
                                {
                                    model.IsEnable = 1;
                                    reason += "序号[" + Sequence + "]:状态错误,不存在的状态;";
                                }
                            }
                            else
                            {
                                model.IsEnable = 1;
                                reason += "序号[" + Sequence + "]:状态为空;";
                            }
                            cell = row.GetCell(4);//备注
                            if (cell != null)
                                model.Comments = cell.ToString();
                        }
                        if (!SYS_ParameterService.CheckParameter(model.Code, null, null, typeID, null))
                        {
                            model.ParameterID = UniversalService.GetSerialNumber("SYS_Parameters");
                            model.ParameterTypeID = typeID;
                            model.IsDefault = false;
                            model.Sequence = 0;//暂时后台定死0
                            model.UsingType = 0;//这个字段暂未知有何用处
                            if (SYS_ParameterService.insert(userId, model))
                                success++;
                            else
                            {
                                reason += "序号[" + Sequence + "]:添加失败，未知原因;";
                                nosuccess++;
                            }
                        }
                        else
                        {
                            reason += "序号[" + Sequence + "]:代号重复;";
                            nosuccess++;
                        }
                    }
                }
            }
            if (nosuccess == total)
                status = "410";

            msg = string.Format(msg, total, success, nosuccess, reason);

            return new { status = status, msg = msg };
        }

        /// <summary>
        /// 分类群码导入
        /// Tom 2017年5月4日10:20:35
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="v"></param>
        /// <returns></returns>
        public static object Inf00009GroupCodeImport(string userId, string filename, Stream stream)
        {
            string SysID = Framework.SystemID;
            string status = "200", msg = "共{0}条信息,{1}条信息导入成功,{2}条信息导入失败！{3}";
            string typeID = Framework.SystemID + "019121300000E";
            string reason = null;
            int total = 0;
            int success = 0;
            int nosuccess = 0;
            if (filename.EndsWith(".xls") || filename.EndsWith(".xlsx"))
            {
                IWorkbook wk = null;
                string Sequence = null;
                SYS_Parameters model = null;

                if (filename.EndsWith(".xls"))
                    wk = new HSSFWorkbook(stream); //把xls文件中的数据写入wk中
                else if (filename.EndsWith(".xlsx"))
                    wk = new XSSFWorkbook(stream);//把xlsx文件中的数据写入wk中

                ISheet sheet = wk.GetSheetAt(0);   //读取第一表数据
                for (int j = 2; j <= sheet.LastRowNum; j++)  //LastRowNum 是当前表的总行数
                {
                    IRow row = sheet.GetRow(j);  //读取当前行数据
                    if (row.GetCell(0) == null) //如果当前行的第一列没有数据，停止读取
                        break;
                    if (string.IsNullOrWhiteSpace(row.GetCell(0).ToString()))//如果当前行的第一列没有数据，停止读取，这是判断当row.GetCell不能null然后实际是空字符
                        break;
                    total++;
                    ICell cell = null;

                    if (row != null)
                    {

                        model = new SYS_Parameters();

                        cell = row.GetCell(0);//序号
                        Sequence = cell.ToString();

                        cell = row.GetCell(1);//代号
                        if (cell != null)
                            model.Code = cell.ToString();
                        cell = row.GetCell(2);//说明
                        if (cell != null)
                            model.Name = cell.ToString();
                        else
                        {
                            reason += "序号[" + Sequence + "]:说明为空;";
                            nosuccess++;
                            continue; //结束本次循环
                        }

                        cell = row.GetCell(3);//备注
                        if (cell != null)
                            model.Comments = cell.ToString();

                        cell = row.GetCell(4);//状态
                        if (cell != null)
                        {
                            if (cell.ToString() == "1")
                                model.IsEnable = 1;
                            else if (cell.ToString() == "0")
                                model.IsEnable = 0;
                            else
                            {
                                model.IsEnable = 1;
                                reason += "序号[" + Sequence + "]:状态错误,不存在的状态;";
                            }
                        }
                        else
                        {
                            model.IsEnable = 1;
                            reason += "序号[" + Sequence + "]:状态为空;";
                        }

                    }

                    if (!SYS_ParameterService.CheckParameter(model.Code, null, null, typeID, null))
                    {
                        model.ParameterID = UniversalService.GetSerialNumber("SYS_Parameters");
                        model.ParameterTypeID = typeID;
                        model.IsDefault = false;
                        model.Sequence = 0;//暂时后台定死0
                        model.UsingType = 0;//这个字段暂未知有何用处
                        if (SYS_ParameterService.insert(userId, model))
                            success++;
                        else
                        {
                            reason += "序号[" + Sequence + "]:添加失败，未知原因;";
                            nosuccess++;
                        }
                    }
                    else
                    {
                        reason += "序号[" + Sequence + "]:代号重复;";
                        nosuccess++;
                    }
                }
            }

            if (nosuccess == total)
                status = "410";

            msg = string.Format(msg, total, success, nosuccess, reason);

            return new { status = status, msg = msg };
        }

        /// <summary>
        /// 分类导入
        /// Tom 2017年5月4日10:26:12
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="v"></param>
        /// <returns></returns>
        public static object Inf00009ClassImport(string userId, string filename, Stream stream)
        {
            string SysID = Framework.SystemID;
            string status = "200", msg = "共{0}条信息,{1}条信息导入成功,{2}条信息导入失败！{3}";
            string typeID = Framework.SystemID + "019121300000B";
            string reason = null;
            int total = 0;
            int success = 0;
            int nosuccess = 0;
            if (filename.EndsWith(".xls") || filename.EndsWith(".xlsx"))
            {
                IWorkbook wk = null;
                SYS_Parameters model = null;
                string Sequence = null;
                if (filename.EndsWith(".xls"))
                    wk = new HSSFWorkbook(stream); //把xls文件中的数据写入wk中
                else if (filename.EndsWith(".xlsx"))
                    wk = new XSSFWorkbook(stream);//把xlsx文件中的数据写入wk中

                ISheet sheet = wk.GetSheetAt(0);   //读取第一表数据
                for (int j = 2; j <= sheet.LastRowNum; j++)  //LastRowNum 是当前表的总行数
                {
                    IRow row = sheet.GetRow(j);  //读取当前行数据
                    if (row.GetCell(0) == null) //如果当前行的第一列没有数据，停止读取
                        break;
                    if (string.IsNullOrWhiteSpace(row.GetCell(0).ToString()))//如果当前行的第一列没有数据，停止读取，这是判断当row.GetCell不能null然后实际是空字符
                        break;
                    total++;
                    ICell cell = null;
                    if (row != null)
                    {
                        model = new SYS_Parameters();

                        cell = row.GetCell(0);//序号
                        Sequence = cell.ToString();

                        cell = row.GetCell(1);
                        if (cell != null)
                        {
                            string useCode = cell.ToString();
                            SYS_Parameters sp = SYS_ParameterService.getByCode(useCode, SysID + "019121300000D");
                            if (sp != null)
                            {
                                model.Description = sp.ParameterID; ;
                            }
                            else
                            {
                                reason += "序号[" + Sequence + "]: 没有" + useCode + "的用途别;";
                                nosuccess++;
                                continue; //结束本次循环
                            }
                        }

                        cell = row.GetCell(2);//代号
                        if (cell != null)
                            model.Code = cell.ToString();
                        cell = row.GetCell(3);//说明
                        if (cell != null)
                            model.Name = cell.ToString();
                        else
                        {
                            reason += "序号[" + Sequence + "]:说明为空;";
                            nosuccess++;
                            continue; //结束本次循环
                        }

                        cell = row.GetCell(4);//备注
                        if (cell != null)
                            model.Comments = cell.ToString();

                        cell = row.GetCell(5);//状态
                        if (cell != null)
                        {
                            if (cell.ToString() == "1")
                                model.IsEnable = 1;
                            else if (cell.ToString() == "0")
                                model.IsEnable = 0;
                            else
                            {
                                model.IsEnable = 1;
                                reason += "序号[" + Sequence + "]:状态错误,不存在的状态;";
                            }
                        }
                        else
                        {
                            model.IsEnable = 1;
                            reason += "序号[" + Sequence + "]:状态为空;";
                        }

                        cell = row.GetCell(6); // 群码
                        if (cell != null)
                        {
                            string groupCode = cell.ToString();
                            SYS_Parameters sp = SYS_ParameterService.getByCode(groupCode, SysID + "019121300000E");
                            if (sp != null)
                            {
                                model.DescriptionOne = sp.ParameterID; ;
                            }
                            else
                            {
                                reason += "序号[" + Sequence + "]:没有" + groupCode + "的群码;";
                            }
                        }
                    }
                    if (!SYS_ParameterService.CheckParameter(model.Code, null, null, typeID, null))
                    {
                        model.ParameterID = UniversalService.GetSerialNumber("SYS_Parameters");
                        model.ParameterTypeID = typeID;
                        model.IsDefault = false;
                        model.Sequence = 0;//暂时后台定死0
                        model.UsingType = 0;//这个字段暂未知有何用处
                        if (SYS_ParameterService.insert(userId, model))
                            success++;
                        else
                        {
                            reason += "序号[" + Sequence + "]:添加失败，未知原因;";
                            nosuccess++;
                        }
                    }
                    else
                    {
                        reason += "序号[" + Sequence + "]:代号重复;";
                        nosuccess++;
                    }
                }

            }

            if (nosuccess == total)
                status = "410";

            msg = string.Format(msg, total, success, nosuccess, reason);

            return new { status = status, msg = msg };
        }

        /// <summary>
        /// 单位导入
        /// Tom 2017年5月4日11:52:25
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="fileName"></param>
        /// <param name="inputStream"></param>
        /// <returns></returns>
        public static object Inf00011Import(string userId, string filename, Stream stream)
        {
            string SysID = Framework.SystemID;
            string status = "200", msg = "共{0}条信息,{1}条信息导入成功,{2}条信息导入失败！{3}";
            string typeID = Framework.SystemID + "019121300000C";
            string reason = null;
            int total = 0;
            int success = 0;
            int nosuccess = 0;
            if (filename.EndsWith(".xls") || filename.EndsWith(".xlsx"))
            {
                IWorkbook wk = null;
                string Sequence = null;
                SYS_Parameters model = null;

                if (filename.EndsWith(".xls"))
                    wk = new HSSFWorkbook(stream); //把xls文件中的数据写入wk中
                else if (filename.EndsWith(".xlsx"))
                    wk = new XSSFWorkbook(stream);//把xlsx文件中的数据写入wk中

                ISheet sheet = wk.GetSheetAt(0);   //读取第一表数据
                for (int j = 2; j <= sheet.LastRowNum; j++)  //LastRowNum 是当前表的总行数
                {
                    IRow row = sheet.GetRow(j);  //读取当前行数据
                    if (row.GetCell(0) == null) //如果当前行的第一列没有数据，停止读取
                        break;
                    if (string.IsNullOrWhiteSpace(row.GetCell(0).ToString()))//如果当前行的第一列没有数据，停止读取，这是判断当row.GetCell不能null然后实际是空字符
                        break;
                    total++;
                    ICell cell = null;
                    if (row != null)
                    {
                        model = new SYS_Parameters();
                        cell = row.GetCell(0);//序号
                        Sequence = cell.ToString();

                        cell = row.GetCell(1);//代号
                        if (cell != null)
                            model.Code = cell.ToString();
                        cell = row.GetCell(2);//说明
                        if (cell != null)
                            model.Name = cell.ToString();
                        else
                        {
                            reason += "序号[" + Sequence + "]:说明为空;";
                            nosuccess++;
                            continue; //结束本次循环
                        }



                        cell = row.GetCell(3);//备注
                        if (cell != null)
                            model.Comments = cell.ToString();

                        cell = row.GetCell(4);//状态
                        if (cell != null)
                        {
                            if (cell.ToString() == "1")
                                model.IsEnable = 1;
                            else if (cell.ToString() == "0")
                                model.IsEnable = 0;
                            else
                            {
                                model.IsEnable = 1;
                                reason += "序号[" + Sequence + "]:状态错误,不存在的状态;";
                            }
                        }
                        else
                        {
                            model.IsEnable = 1;
                            reason += "序号[" + Sequence + "]:状态为空;";
                        }
                    }
                    if (!SYS_ParameterService.CheckParameter(model.Code, null, null, typeID, null))
                    {
                        model.ParameterID = UniversalService.GetSerialNumber("SYS_Parameters");
                        model.ParameterTypeID = typeID;
                        model.IsDefault = false;
                        model.Sequence = 0;//暂时后台定死0
                        model.UsingType = 0;//这个字段暂未知有何用处
                        if (SYS_ParameterService.insert(userId, model))
                            success++;
                        else
                        {
                            reason += "序号[" + Sequence + "]:添加失败，未知原因;";
                            nosuccess++;
                        }
                    }
                    else
                    {
                        reason += "序号[" + Sequence + "]:代号重复;";
                        nosuccess++;
                    }
                }
            }

            if (nosuccess == total)
                status = "410";

            msg = string.Format(msg, total, success, nosuccess, reason);

            return new { status = status, msg = msg };
        }

        /// <summary>
        /// 导入仓库
        /// SAM 2017年5月4日11:50:45
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static object WarehouseImport(string token, string filename)
        {
            string userId = UtilBussinessService.detoken(token);
            string SysID = Framework.SystemID;
            string status = "200", msg = "共{0}条信息,{1}条信息导入成功,{2}条信息导入失败！{3}";
            string reason = null;
            int total = 0;
            int success = 0;
            int nosuccess = 0;
            if (filename.EndsWith(".xls") || filename.EndsWith(".xlsx"))
            {
                IWorkbook wk = null;
                string Sequence = null;
                SYS_Organization model = null;
                using (FileStream fs = File.OpenRead(@filename))   //打开.xls文件
                {
                    if (filename.EndsWith(".xls"))
                        wk = new HSSFWorkbook(fs); //把xls文件中的数据写入wk中
                    else if (filename.EndsWith(".xlsx"))
                        wk = new XSSFWorkbook(fs);//把xlsx文件中的数据写入wk中

                    ISheet sheet = wk.GetSheetAt(0);   //读取第一表数据
                    for (int j = 2; j <= sheet.LastRowNum; j++)  //LastRowNum 是当前表的总行数
                    {
                        IRow row = sheet.GetRow(j);  //读取当前行数据
                        if (row.GetCell(0) == null) //如果当前行的第一列没有数据，停止读取
                            break;
                        if (string.IsNullOrWhiteSpace(row.GetCell(0).ToString()))//如果当前行的第一列没有数据，停止读取，这是判断当row.GetCell不能null然后实际是空字符
                            break;
                        total++;
                        ICell cell = null;
                        if (row != null)
                        {
                            model = new SYS_Organization();

                            cell = row.GetCell(0);//序号
                            Sequence = cell.ToString();

                            cell = row.GetCell(1);//代号
                            if (cell != null)
                                model.Code = cell.ToString();
                            cell = row.GetCell(2);//说明
                            if (cell != null)
                            {
                                model.Name = cell.ToString();
                            }
                            else
                            {
                                reason += "序号[" + Sequence + "]:说明为空;";
                                nosuccess++;
                                continue; //结束本次循环
                            }

                            cell = row.GetCell(3);//隶属厂别
                            if (cell != null)
                            {
                                SYS_Organization OrgModel = SYS_OrganizationService.getByCode(cell.ToString(), SysID + "020121300001E");
                                if (OrgModel != null)
                                    model.ParentOrganizationID = OrgModel.OrganizationID;
                                else
                                    reason += "序号[" + Sequence + "]:隶属厂别代号错误，找不到厂别;";
                            }
                            cell = row.GetCell(4);//备注
                            if (cell != null)
                                model.Comments = cell.ToString();
                            cell = row.GetCell(5);//状态
                            if (cell != null)
                            {
                                if (cell.ToString() == "1")
                                    model.Status = 1;
                                else if (cell.ToString() == "0")
                                    model.Status = 0;
                                else
                                {
                                    model.Status = 1;
                                    reason += "序号[" + Sequence + "]:状态错误,不存在的状态;";
                                }
                            }
                            else
                            {
                                model.Status = 1;
                                reason += "序号[" + Sequence + "]:状态为空;";
                            }
                        }

                        if (!SYS_OrganizationService.CheckCode(model.Code, SysID + "020121300001F", null))
                        {
                            model.OrganizationID = UniversalService.GetSerialNumber("SYS_Organization");
                            model.Type = SysID + "020121300001F";
                            if (SYS_OrganizationService.insert(userId, model))
                                success++;
                            else
                            {
                                reason += "序号[" + Sequence + "]:添加失败，未知原因;";
                                nosuccess++;
                            }
                        }
                        else
                        {
                            reason += "序号[" + Sequence + "]:代号重复;";
                            nosuccess++;
                        }
                    }

                }
            }

            if (nosuccess == total)
                status = "410";

            msg = string.Format(msg, total, success, nosuccess, reason);

            return new { status = status, msg = msg };
        }

        /// <summary>
        /// 导入账户
        /// SAM 2017年5月4日15:45:35
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static object UserImport(string token, string filename)
        {
            string userId = UtilBussinessService.detoken(token);
            string SysID = Framework.SystemID;
            string status = "200", msg = "共{0}条信息,{1}条信息导入成功,{2}条信息导入失败！{3}";
            string reason = null;
            int total = 0;
            int success = 0;
            int nosuccess = 0;
            if (filename.EndsWith(".xls") || filename.EndsWith(".xlsx"))
            {
                IWorkbook wk = null;
                bool Isright = true;
                SYS_MESUsers model = null;
                string OrganizationID = null;
                using (FileStream fs = File.OpenRead(@filename))   //打开.xls文件
                {
                    if (filename.EndsWith(".xls"))
                        wk = new HSSFWorkbook(fs); //把xls文件中的数据写入wk中
                    else if (filename.EndsWith(".xlsx"))
                        wk = new XSSFWorkbook(fs);//把xlsx文件中的数据写入wk中

                    ISheet sheet = wk.GetSheetAt(0);   //读取第一表数据
                    for (int j = 2; j <= sheet.LastRowNum; j++)  //LastRowNum 是当前表的总行数
                    {
                        IRow row = sheet.GetRow(j);  //读取当前行数据
                        if (row.GetCell(0) == null) //如果当前行的第一列没有数据，停止读取
                            break;
                        if (string.IsNullOrWhiteSpace(row.GetCell(0).ToString()))//如果当前行的第一列没有数据，停止读取，这是判断当row.GetCell不能null然后实际是空字符
                            break;
                        total++;
                        ICell cell = null;
                        Isright = true;
                        if (row != null)
                        {
                            Isright = true;
                            model = new SYS_MESUsers();
                            OrganizationID = null;
                            cell = row.GetCell(0);//账号 20
                            if (cell != null)
                            {
                                if (cell.ToString().Length <= 20)
                                    model.Account = cell.ToString();
                                else
                                {
                                    reason += "账号[" + model.Account + "]账号超过20个字;";
                                    nosuccess++;
                                    continue;
                                }
                            }
                            else
                            {
                                reason += "账号不能为空;";
                                nosuccess++;
                                continue;
                            }

                            cell = row.GetCell(1);//姓名 20
                            if (cell != null)
                            {
                                if (cell.ToString().Length <= 20)
                                    model.UserName = cell.ToString();
                                else
                                {
                                    reason += "账号[" + model.Account + "]姓名超过20个字;";
                                    nosuccess++;
                                    continue;
                                }
                            }
                            else
                            {
                                reason += "账号[" + model.Account + "]姓名为空;";
                                nosuccess++;
                                continue;
                            }
                            cell = row.GetCell(2);//英文名 60
                            if (cell != null)
                            {
                                if (cell.ToString().Length <= 60)
                                    model.EnglishName = cell.ToString();
                                else
                                    reason += "账号[" + model.Account + "]英文名超过60个字;";
                            }
                            cell = row.GetCell(3);//性别
                            if (cell != null)
                            {
                                if (cell.ToString() == "女")
                                    model.Sex = false;
                                else if (cell.ToString() == "男")
                                    model.Sex = true;
                                else
                                    reason += "账号[" + model.Account + "]:性别错误;";
                            }
                            else
                            {
                                reason += "账号[" + model.Account + "]:性别错误;";
                            }
                            cell = row.GetCell(4);//出生日期
                            if (cell != null)
                                model.Brith = cell.ToString();
                            cell = row.GetCell(5);//工号  20
                            if (cell != null)
                            {
                                if (cell.ToString().Length <= 30)
                                    model.Emplno = cell.ToString();
                                else
                                {
                                    reason += "账号[" + model.Account + "]:工号超过了30个字;";
                                }
                            }

                            cell = row.GetCell(6);//部门
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                SYS_Organization orgModel = SYS_OrganizationService.getByCode(cell.ToString(), SysID + "020121300001D");
                                if (orgModel == null)
                                    reason += "账号[" + model.Account + "]:部门字段错误，不存在的部门;";
                                else
                                    OrganizationID = orgModel.OrganizationID;
                            }

                            cell = row.GetCell(7);//身份证
                            if (cell != null)
                                model.IDcard = cell.ToString();
                            cell = row.GetCell(8);//手机号码 20
                            if (cell != null)
                            {
                                if (cell.ToString().Length <= 11)
                                    model.Mobile = cell.ToString();
                                else
                                    reason += "账号[" + model.Account + "]:错误的手机号码;";
                            }
                            cell = row.GetCell(9);//E-mail 60
                            if (cell != null)
                            {
                                if (cell.ToString().Length <= 60)
                                    model.Email = cell.ToString();
                                else
                                    reason += "账号[" + model.Account + "]:邮件超过了60个字;";
                            }
                            cell = row.GetCell(10);//职工类型
                            if (cell != null)
                            {
                                SYS_Parameters parModel = SYS_ParameterService.getByName(cell.ToString(), SysID + "0191213000051");
                                if (parModel != null)
                                    model.Type = parModel.ParameterID;
                                else
                                {
                                    model.Type = Framework.SystemID + "0201213000013";
                                    reason += "账号[" + model.Account + "]:职工类型错误，不存在的职工类型;";
                                }
                            }
                            else
                            {
                                model.Type = Framework.SystemID + "0201213000013";
                                reason += "账号[" + model.Account + "]:职工类型为空;";
                            }
                            cell = row.GetCell(11);//入职时间
                            if (cell != null)
                                model.InTime = cell.ToString();
                            cell = row.GetCell(12);//卡号 50
                            if (cell != null)
                            {
                                if (cell.ToString().Length <= 50)
                                    model.CardCode = cell.ToString();
                                else
                                {
                                    reason += "账号[" + model.Account + "]:卡号超过了50个字;";
                                }
                            }
                            cell = row.GetCell(13);//备注 120
                            if (cell != null)
                            {
                                if (cell.ToString().Length <= 120)
                                    model.Comments = cell.ToString();
                                else
                                    reason += "账号[" + model.Account + "]:备注超过了120个字;";
                            }
                            cell = row.GetCell(14);//状态
                            if (cell != null)
                            {
                                if (cell.ToString() == "正常")
                                    model.Status = 1;
                                else if (cell.ToString() == "作废")
                                    model.Status = 0;
                                else
                                {
                                    model.Status = 1;
                                    reason += "账号[" + model.Account + "]的状态错误,不存在的状态;";
                                }
                            }
                            else
                            {
                                model.Status = 1;
                                reason += "账号[" + model.Account + "]的状态为空;";
                            }
                        }
                        if (Isright)
                        {
                            if (!SYS_MESUserService.CheckCode(model.Account, null, null, null))
                            {
                                //if (!SYS_MESUserService.CheckCode(null, model.Emplno, null, null))
                                //{
                                //    if (!SYS_MESUserService.CheckCode(null, null, model.CardCode, null))
                                //    {
                                model.MESUserID = UniversalService.GetSerialNumber("SYS_MESUsers");
                                model.Password = "3d9188577cc9bfe9291ac66b5cc872b7";
                                model.UserType = 10;
                                if (SYS_MESUserService.insert(userId, model))
                                {
                                    if (!string.IsNullOrWhiteSpace(OrganizationID))
                                        SYS_UserOrganizationMappingService.add(userId, model.MESUserID, OrganizationID);
                                    success++;
                                }
                                else
                                {
                                    reason += "账号[" + model.Account + "]:添加失败，请联系开发人员;";
                                    nosuccess++;
                                }
                                //}
                                //else
                                //{
                                //    reason += "卡号[" + model.CardCode + "]重复;";
                                //    nosuccess++;
                                //}
                                //}
                                //else
                                //{
                                //    reason += "工号[" + model.Emplno + "]重复;";
                                //    nosuccess++;
                                //}
                            }
                            else
                            {
                                reason += "账号[" + model.Account + "]重复;";
                                nosuccess++;
                            }
                        }
                        else
                            nosuccess++;
                    }
                }
            }
            if (nosuccess == total)
                status = "410";

            msg = string.Format(msg, total, success, nosuccess, reason);

            return new { status = status, msg = msg };
        }

        /// <summary>
        /// 班别导入
        /// Tom 2017年5月5日17:56:49
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="fileName"></param>
        /// <param name="inputStream"></param>
        /// <returns></returns>
        public static object Inf00013Import(string userID, string filename, Stream stream)
        {
            string SysID = Framework.SystemID;
            string status = "200", msg = "共{0}条信息,{1}条信息导入成功,{2}条信息导入失败！{3}";
            string typeID = Framework.SystemID + "019121300000C";
            string reason = null;
            int total = 0;
            int success = 0;
            int nosuccess = 0;
            if (filename.EndsWith(".xls") || filename.EndsWith(".xlsx"))
            {
                IWorkbook wk = null;
                string Sequence = null;
                SYS_Class model = null;
                if (filename.EndsWith(".xls"))
                    wk = new HSSFWorkbook(stream); //把xls文件中的数据写入wk中
                else if (filename.EndsWith(".xlsx"))
                    wk = new XSSFWorkbook(stream);//把xlsx文件中的数据写入wk中

                ISheet sheet = wk.GetSheetAt(0);   //读取第一表数据
                for (int j = 2; j <= sheet.LastRowNum; j++)  //LastRowNum 是当前表的总行数
                {
                    IRow row = sheet.GetRow(j);  //读取当前行数据
                    if (row.GetCell(0) == null) //如果当前行的第一列没有数据，停止读取
                        break;
                    if (string.IsNullOrWhiteSpace(row.GetCell(0).ToString()))//如果当前行的第一列没有数据，停止读取，这是判断当row.GetCell不能null然后实际是空字符
                        break;
                    total++;
                    ICell cell = null;
                    if (row != null)
                    {
                        model = new SYS_Class();


                        cell = row.GetCell(0);//序号
                        Sequence = cell.ToString();

                        cell = row.GetCell(1);//代号
                        if (cell != null)
                        {
                            if (cell.ToString().Length <= 30)
                                model.Code = cell.ToString();
                            else
                            {
                                reason += "序号[" + Sequence + "]:代号超过了30个字;";
                                nosuccess++;
                                continue;
                            }
                        }
                        cell = row.GetCell(2);//说明
                        if (cell != null)
                        {
                            if (cell.ToString().Length <= 120)
                                model.Name = cell.ToString();
                            else
                            {
                                reason += "序号[" + Sequence + "]:说明超过了120个字;";
                                nosuccess++;
                                continue;
                            }
                        }
                        else
                        {
                            reason += "序号[" + Sequence + "]:说明为空;";
                            nosuccess++;
                            continue;
                        }


                        cell = row.GetCell(3);//备注
                        if (cell != null)
                        {
                            if (cell.ToString().Length <= 120)
                                model.Comments = cell.ToString();
                            else
                            {
                                reason += "序号[" + Sequence + "]:备注超过了120个字;";
                            }
                        }

                        cell = row.GetCell(4);//跨日
                        if (cell != null)
                        {
                            string CrossDay = cell.ToString();
                            if (CrossDay == "N")
                                model.CrossDay = false;
                            else if (CrossDay == "Y")
                                model.CrossDay = true;
                            else
                            {
                                reason += "序号[" + Sequence + "]:跨日信息错误;";
                            }
                        }
                        else
                        {
                            model.CrossDay = false;
                            reason += "序号[" + Sequence + "]:跨日为空";
                        }

                        cell = row.GetCell(5);//上班时间

                        if (cell != null)
                        {
                            if (Regex.IsMatch(cell.ToString(), @"^([01]\d|2[0123]):([0-5]\d)$"))
                                model.OnTime = cell.ToString();
                            else
                            {
                                reason += "序号[" + Sequence + "]:上班时间错误;";
                                nosuccess++;
                                continue;
                            }
                        }
                        else
                        {
                            reason += "序号[" + Sequence + "]:上班时间为空;";
                            nosuccess++;
                            continue;
                        }

                        cell = row.GetCell(6);//下班时间

                        if (cell != null)
                        {
                            if (Regex.IsMatch(cell.ToString(), @"^([01]\d|2[0123]):([0-5]\d)$"))
                            {
                                model.OffTime = cell.ToString();
                            }
                            else
                            {
                                reason += "序号[" + Sequence + "]:下班时间错误;";
                                nosuccess++;
                                continue;
                            }
                        }
                        else
                        {
                            reason += "序号[" + Sequence + "]:下班时间为空;";
                            nosuccess++;
                            continue;
                        }

                        cell = row.GetCell(7);//休息时间
                        decimal dcell = Convert.ToDecimal(cell.ToString());
                        if (cell != null)
                        {
                            if (dcell > 0 || dcell == 0)
                                model.OffHour = dcell;
                            else
                            {
                                reason += "序号[" + Sequence + "]:休息时间错误;";
                            }
                        }
                        else
                        {
                            model.OffHour = 0;
                            reason += "序号[" + Sequence + "]:休息时间为空;";
                        }

                        cell = row.GetCell(8);//工作时间
                        string ontime = model.OnTime;
                        string offtime = model.OffTime;
                        decimal offhour = model.OffHour;
                        DateTime dtOntime = new DateTime(Convert.ToDateTime(ontime).Ticks);
                        DateTime dtOfftime = new DateTime(Convert.ToDateTime(offtime).Ticks);

                        if (cell != null)
                        {
                            if (decimal.Parse(cell.ToString()) > 0)
                                model.WorkHour = decimal.Parse(cell.ToString());
                            else
                            {
                                reason += "序号[" + Sequence + "]:工作时间为错误;";
                            }
                        }
                        else
                        {
                            if (model.CrossDay == false)
                            {
                                if (DateTime.Compare(dtOntime, dtOfftime) < 0 || DateTime.Compare(dtOntime, dtOfftime) == 0)
                                {
                                    decimal secound = Convert.ToDecimal((dtOfftime - dtOntime).TotalHours);
                                    decimal workhour = secound - offhour;
                                    model.WorkHour = workhour;
                                    reason += "序号[" + Sequence + "]:工作时间为空;";
                                }
                            }
                            else
                            {
                                if (DateTime.Compare(dtOntime, dtOfftime) > 0 || DateTime.Compare(dtOntime, dtOfftime) == 0)
                                {
                                    decimal secound = Convert.ToDecimal((dtOntime - dtOfftime).TotalHours);
                                    decimal workhour = 24 - secound - offhour;
                                    model.WorkHour = workhour;
                                    reason += "序号[" + Sequence + "]:工作时间为空;";
                                }
                            }
                        }

                        cell = row.GetCell(9);//状态
                        if (cell != null)
                        {
                            if (cell.ToString() == "1")
                                model.Status = SysID + "0201213000001";
                            else if (cell.ToString() == "0")
                                model.Status = SysID + "0201213000002";
                            else
                            {
                                model.Status = SysID + "0201213000001";
                                reason += "序号[" + Sequence + "]:状态错误,不存在的状态;";
                            }
                        }
                        else
                        {
                            model.Status = SysID + "0201213000001";
                            reason += "序号[" + Sequence + "]:状态为空;";
                        }
                    }
                    if (!SYS_ClassService.ChecCode(model.Code))
                    {
                        model.ClassID = UniversalService.GetSerialNumber("SYS_Class");
                        if (SYS_ClassService.insert(userID, model))
                            success++;
                        else
                        {
                            reason += "序号[" + Sequence + "]:添加失败，未知原因;";
                            nosuccess++;
                        }
                    }
                    else
                    {
                        reason += "序号[" + Sequence + "]:代号重复;";
                        nosuccess++;
                    }
                }
            }

            if (nosuccess == total)
                status = "410";

            msg = string.Format(msg, total, success, nosuccess, reason);

            return new { status = status, msg = msg };
        }

        /// <summary>
        /// 项目导入
        /// SAM 2017年6月6日22:08:18
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="filename"></param>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static object Inf00019Import(string userID, string filename, Stream stream)
        {
            string SysID = Framework.SystemID;
            string status = "200", msg = "共{0}条信息,{1}条信息导入成功,{2}条信息导入失败！{3}";
            string reason = null;
            int total = 0;
            int success = 0;
            int nosuccess = 0;
            if (filename.EndsWith(".xls") || filename.EndsWith(".xlsx"))
            {
                IWorkbook wk = null;
                SYS_Projects model = null;
                SYS_Parameters ParModel = null;
                if (filename.EndsWith(".xls"))
                    wk = new HSSFWorkbook(stream); //把xls文件中的数据写入wk中
                else if (filename.EndsWith(".xlsx"))
                    wk = new XSSFWorkbook(stream);//把xlsx文件中的数据写入wk中

                ISheet sheet = wk.GetSheetAt(0);   //读取第一表数据
                for (int j = 2; j <= sheet.LastRowNum; j++)  //LastRowNum 是当前表的总行数
                {
                    IRow row = sheet.GetRow(j);  //读取当前行数据
                    if (row == null)
                        break;
                    if (row.GetCell(0) == null) //如果当前行的第一列没有数据，停止读取
                        break;
                    if (string.IsNullOrWhiteSpace(row.GetCell(0).ToString()))//如果当前行的第一列没有数据，停止读取，这是判断当row.GetCell不能null然后实际是空字符
                        break;
                    total++;
                    ICell cell = null;
                    if (row != null)
                    {
                        model = new SYS_Projects();
                        cell = row.GetCell(0);//代号
                        if (cell != null)
                        {
                            if (cell.ToString().Length <= 30)
                                model.Code = cell.ToString();
                            else
                            {
                                reason += "代号[" + model.Code + "]超过了30个字;";
                                nosuccess++;
                                continue; //结束本次循环
                            }
                        }

                        cell = row.GetCell(1);//说明
                        if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            if (cell.ToString().Length <= 120)
                                model.Description = cell.ToString();
                            else
                            {
                                reason += "代号[" + model.Code + "]的说明超过了120个字;";
                                nosuccess++;
                                continue; //结束本次循环
                            }
                        else
                        {
                            reason += "代号[" + model.Code + "]的说明不能为空;";
                            nosuccess++;
                            continue; //结束本次循环
                        }

                        cell = row.GetCell(2);//属性
                        if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                        {
                            ParModel = SYS_ParameterService.getByCode(cell.ToString(), Framework.SystemID + "0191213000052");
                            if (ParModel != null)
                                model.Attribute = ParModel.ParameterID;
                            else
                            {
                                reason += "代号[" + model.Code + "]的属性[" + cell.ToString() + "]错误;";
                            }
                        }
                        else
                        {
                            reason += "代号[" + model.Code + "]的属性不能为空;";
                            nosuccess++;
                            continue; //结束本次循环
                        }

                        cell = row.GetCell(3);//备注
                        if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                        {
                            if (cell.ToString().Length <= 120)
                                model.Comments = cell.ToString();
                            else
                                reason += "代号[" + model.Code + "]的备注超过了120个字;";
                        }

                        cell = row.GetCell(4);//状态
                        if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                        {
                            if (cell.ToString() == "1")
                                model.Status = SysID + "0201213000001";
                            else if (cell.ToString() == "0")
                                model.Status = SysID + "0201213000002";
                            else
                            {
                                model.Status = SysID + "0201213000001";
                                reason += "代号[" + model.Code + "]的状态错误,不存在的状态;";
                            }
                        }
                        else
                        {
                            model.Status = SysID + "0201213000001";
                            reason += "代号[" + model.Code + "]的状态为空;";
                        }
                    }
                    if (!SYS_ProjectsService.ChecCode(model.Code))
                    {
                        model.ProjectID = UniversalService.GetSerialNumber("SYS_Projects");
                        if (SYS_ProjectsService.insert(userID, model))
                            success++;
                        else
                        {
                            reason += "代号[" + model.Code + "]:添加失败,出现了未知错误,请找研发人员查找问题;";
                            nosuccess++;
                        }
                    }
                    else
                    {
                        reason += "代号[" + model.Code + "]重复;";
                        nosuccess++;
                    }
                }
            }

            if (nosuccess == total)
                status = "410";

            msg = string.Format(msg, total, success, nosuccess, reason);

            return new { status = status, msg = msg };
        }

        /// <summary>
        /// 料品属性导入
        /// SAM 2017年5月10日11:12:59
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static object Inf00024Import(string token, string filename)
        {
            string userId = UtilBussinessService.detoken(token);
            string SysID = Framework.SystemID;
            string status = "200", msg = "共{0}条信息,{1}条信息导入成功,{2}条信息导入失败！{3}";
            string reason = null;
            int total = 0;
            int success = 0;
            int nosuccess = 0;
            if (filename.EndsWith(".xls") || filename.EndsWith(".xlsx"))
            {
                IWorkbook wk = null;
                bool Isright = true;
                string Sequence = null;
                SYS_Parameters model = null;
                string Type = Framework.SystemID + "019121300000F";
                using (FileStream fs = File.OpenRead(@filename))   //打开.xls文件
                {
                    if (filename.EndsWith(".xls"))
                        wk = new HSSFWorkbook(fs); //把xls文件中的数据写入wk中
                    else if (filename.EndsWith(".xlsx"))
                        wk = new XSSFWorkbook(fs);//把xlsx文件中的数据写入wk中

                    ISheet sheet = wk.GetSheetAt(0);   //读取第一表数据
                    for (int j = 2; j <= sheet.LastRowNum; j++)  //LastRowNum 是当前表的总行数
                    {
                        IRow row = sheet.GetRow(j);  //读取当前行数据
                        if (row.GetCell(0) == null) //如果当前行的第一列没有数据，停止读取
                            break;
                        if (string.IsNullOrWhiteSpace(row.GetCell(0).ToString()))//如果当前行的第一列没有数据，停止读取，这是判断当row.GetCell不能null然后实际是空字符
                            break;
                        total++;
                        ICell cell = null;
                        Isright = true;
                        if (row != null)
                        {
                            Isright = true;
                            model = new SYS_Parameters();
                            Sequence = null;
                            cell = row.GetCell(0);//序号
                            Sequence = cell.ToString();
                            cell = row.GetCell(1);//代号
                            if (cell != null)
                                model.Code = cell.ToString();
                            else
                            {
                                reason += "序号[" + Sequence + "]代号为空;";
                                nosuccess++;
                                continue; //结束本次循环
                            }
                            cell = row.GetCell(2);//说明
                            if (cell != null)
                                model.Name = cell.ToString();
                            else
                            {
                                reason += "序号[" + Sequence + "]说明为空;";
                                nosuccess++;
                                continue; //结束本次循环
                            }
                            Sequence = cell.ToString();
                            cell = row.GetCell(3);//手动输入
                            if (cell != null)
                            {
                                if (cell.ToString() == "Y")
                                    model.IsDefault = true;
                                else if (cell.ToString() == "N")
                                    model.IsDefault = false;
                                else
                                {
                                    model.IsDefault = false;
                                    reason += "序号[" + Sequence + "]的手动输入错误,值只能是“Y”或“N”;";
                                }
                            }
                            else
                            {
                                model.IsDefault = false;
                                reason += "序号[" + Sequence + "]的手动输入为空;";
                            }
                            cell = row.GetCell(4);//备注
                            if (cell != null)
                                model.Comments = cell.ToString();

                            cell = row.GetCell(5);//状态
                            if (cell != null)
                                model.IsEnable = int.Parse(cell.ToString());
                        }
                        if (Isright)
                        {
                            if (!SYS_ParameterService.CheckParameter(model.Code, null, null, Type, null))
                            {
                                model.ParameterID = UniversalService.GetSerialNumber("SYS_Parameters");
                                model.ParameterTypeID = Type;
                                model.Sequence = 0;
                                model.UsingType = 0;
                                if (SYS_ParameterService.insert(userId, model))
                                    success++;
                                else
                                {
                                    reason += "序号[" + Sequence + "]:添加失败，未知原因;";
                                    nosuccess++;
                                }
                            }
                            else
                            {
                                reason += "序号[" + Sequence + "]的代号重复;";
                                nosuccess++;
                            }
                        }
                        else
                            nosuccess++;
                    }

                }
            }
            if (nosuccess == total)
                status = "410";

            msg = string.Format(msg, total, success, nosuccess, reason);

            return new { status = status, msg = msg };
        }

        /// <summary>
        /// 原因码导入
        /// SAM 2017年5月11日11:27:40
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static object Inf00017Import(string token, string filename)
        {
            string userId = UtilBussinessService.detoken(token);
            string SysID = Framework.SystemID;
            string status = "200", msg = "共{0}条信息,{1}条信息导入成功,{2}条信息导入失败！{3}";
            string reason = null;
            int total = 0;
            int success = 0;
            int nosuccess = 0;
            if (filename.EndsWith(".xls") || filename.EndsWith(".xlsx"))
            {
                IWorkbook wk = null;
                bool Isright = true;
                string Sequence = null;
                SYS_Parameters model = null;
                string Type = Framework.SystemID + "0191213000012";
                using (FileStream fs = File.OpenRead(@filename))   //打开.xls文件
                {
                    if (filename.EndsWith(".xls"))
                        wk = new HSSFWorkbook(fs); //把xls文件中的数据写入wk中
                    else if (filename.EndsWith(".xlsx"))
                        wk = new XSSFWorkbook(fs);//把xlsx文件中的数据写入wk中

                    ISheet sheet = wk.GetSheetAt(0);   //读取第一表数据
                    for (int j = 2; j <= sheet.LastRowNum; j++)  //LastRowNum 是当前表的总行数
                    {
                        IRow row = sheet.GetRow(j);  //读取当前行数据
                        if (row.GetCell(0) == null) //如果当前行的第一列没有数据，停止读取
                            break;
                        if (string.IsNullOrWhiteSpace(row.GetCell(0).ToString()))//如果当前行的第一列没有数据，停止读取，这是判断当row.GetCell不能null然后实际是空字符
                            break;
                        total++;
                        ICell cell = null;
                        Isright = true;
                        if (row != null)
                        {
                            Isright = true;
                            model = new SYS_Parameters();
                            Sequence = null;
                            cell = row.GetCell(0);//序号
                            Sequence = cell.ToString();
                            cell = row.GetCell(1);//用途
                            if (cell != null)
                            {
                                SYS_Parameters ParModel = SYS_ParameterService.getByCode(cell.ToString(), SysID + "019121300000D");
                                if (ParModel != null)
                                    model.Description = ParModel.ParameterID;
                                else
                                {
                                    reason += "序号[" + Sequence + "]用途错误，不存在的用途;";
                                    nosuccess++;
                                    continue; //结束本次循环
                                }
                            }
                            else
                            {
                                reason += "序号[" + Sequence + "]用途为空;";
                                nosuccess++;
                                continue; //结束本次循环
                            }


                            cell = row.GetCell(2);//代号
                            if (cell != null)
                                model.Code = cell.ToString();
                            else
                            {
                                reason += "序号[" + Sequence + "]代号为空;";
                                nosuccess++;
                                continue; //结束本次循环
                            }
                            cell = row.GetCell(3);//说明
                            if (cell != null)
                                model.Name = cell.ToString();
                            else
                            {
                                reason += "序号[" + Sequence + "]说明为空;";
                                nosuccess++;
                                continue; //结束本次循环
                            }


                            Sequence = cell.ToString();
                            cell = row.GetCell(4);//原因群码
                            if (cell != null)
                            {
                                SYS_Parameters ParModel = SYS_ParameterService.getByCode(cell.ToString(), SysID + "0191213000011");
                                if (ParModel != null)
                                    model.DescriptionOne = ParModel.ParameterID;
                                else
                                {
                                    reason += "序号[" + Sequence + "]原因群码错误，不存在的原因群码;";
                                }
                            }
                            else
                            {
                                reason += "序号[" + Sequence + "]原因群码为空;";
                            }

                            cell = row.GetCell(5);//备注
                            if (cell != null)
                                model.Comments = cell.ToString();
                            cell = row.GetCell(6);//状态
                            if (cell != null)
                            {
                                if (cell.ToString() == "正常")
                                    model.IsEnable = 1;
                                else if (cell.ToString() == "作废")
                                    model.IsEnable = 2;
                                else
                                {
                                    model.IsEnable = 1;
                                    reason += "序号[" + Sequence + "]的状态错误,不存在的状态;";
                                }
                            }
                            else
                            {
                                model.IsEnable = 1;
                                reason += "序号[" + Sequence + "]的状态为空;";
                            }
                        }
                        if (Isright)
                        {
                            if (!SYS_ParameterService.CheckParameter(model.Code, null, null, Type, null))
                            {
                                model.ParameterID = UniversalService.GetSerialNumber("SYS_Parameters");
                                model.ParameterTypeID = Type;
                                model.Sequence = 0;
                                model.UsingType = 0;
                                if (SYS_ParameterService.insert(userId, model))
                                    success++;
                                else
                                {
                                    reason += "序号[" + Sequence + "]:添加失败，未知原因;";
                                    nosuccess++;
                                }
                            }
                            else
                            {
                                reason += "序号[" + Sequence + "]的代号重复;";
                                nosuccess++;
                            }
                        }
                        else
                            nosuccess++;
                    }

                }
            }
            if (nosuccess == total)
                status = "410";

            msg = string.Format(msg, total, success, nosuccess, reason);

            return new { status = status, msg = msg };
        }

        /// <summary>
        /// 通用参数导入(Code,Name,Comments,Status)
        /// SAM 2017年5月12日10:18:18
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static object ParImport(string token, string filename, string Type)
        {
            string userId = UtilBussinessService.detoken(token);
            string SysID = Framework.SystemID;
            string status = "200", msg = "共{0}条信息,{1}条信息导入成功,{2}条信息导入失败！{3}";
            string reason = null;
            int total = 0;
            int success = 0;
            int nosuccess = 0;
            if (filename.EndsWith(".xls") || filename.EndsWith(".xlsx"))
            {
                IWorkbook wk = null;
                bool Isright = true;
                string Sequence = null;
                SYS_Parameters model = null;
                using (FileStream fs = File.OpenRead(@filename))   //打开.xls文件
                {
                    if (filename.EndsWith(".xls"))
                        wk = new HSSFWorkbook(fs); //把xls文件中的数据写入wk中
                    else if (filename.EndsWith(".xlsx"))
                        wk = new XSSFWorkbook(fs);//把xlsx文件中的数据写入wk中

                    ISheet sheet = wk.GetSheetAt(0);   //读取第一表数据
                    for (int j = 2; j <= sheet.LastRowNum; j++)  //LastRowNum 是当前表的总行数
                    {
                        IRow row = sheet.GetRow(j);  //读取当前行数据
                        if (row.GetCell(0) == null) //如果当前行的第一列没有数据，停止读取
                            break;
                        if (string.IsNullOrWhiteSpace(row.GetCell(0).ToString()))//如果当前行的第一列没有数据，停止读取，这是判断当row.GetCell不能null然后实际是空字符
                            break;
                        total++;
                        ICell cell = null;
                        Isright = true;
                        if (row != null)
                        {
                            Isright = true;
                            model = new SYS_Parameters();

                            cell = row.GetCell(0);//序号
                            Sequence = cell.ToString();
                            cell = row.GetCell(1);//代号
                            if (cell != null)
                            {
                                if (cell.ToString().Length <= 20)
                                {
                                    model.Code = cell.ToString();
                                }
                                else
                                {
                                    reason += "序号[" + Sequence + "]:代号超过20个字;";
                                    nosuccess++;
                                    continue; //结束本次循环
                                }
                            }
                            else
                            {
                                reason += "序号[" + Sequence + "]代号为空;";
                                nosuccess++;
                                continue; //结束本次循环
                            }
                            cell = row.GetCell(2);//说明
                            if (cell != null)
                            {
                                if (cell.ToString().Length <= 60)
                                {
                                    model.Name = cell.ToString();
                                }
                                else
                                {
                                    reason += "序号[" + Sequence + "]:说明超过60个字;";
                                    nosuccess++;
                                    continue; //结束本次循环
                                }
                            }
                            else
                            {
                                reason += "序号[" + Sequence + "]说明为空;";
                                nosuccess++;
                                continue; //结束本次循环
                            }
                            cell = row.GetCell(3);//备注
                            if (cell != null)
                            {
                                if (cell.ToString().Length <= 120)
                                {
                                    model.Comments = cell.ToString();
                                }
                                else
                                {
                                    reason += "序号[" + Sequence + "]:备注超过120个字;";
                                }
                            }
                            cell = row.GetCell(4);//状态
                            if (cell != null)
                            {
                                if (cell.ToString() == "正常" || cell.ToString() == "1")
                                    model.IsEnable = 1;
                                else if (cell.ToString() == "作废" || cell.ToString() == "0")
                                    model.IsEnable = 0;
                                else
                                {
                                    model.IsEnable = 1;
                                    reason += "序号[" + Sequence + "]的状态错误,不存在的状态;";
                                }
                            }
                            else
                            {
                                model.IsEnable = 1;
                                reason += "序号[" + Sequence + "]的状态为空;";
                            }
                        }
                        if (Isright)
                        {
                            if (!SYS_ParameterService.CheckParameter(model.Code, null, null, Type, null))
                            {
                                model.ParameterID = UniversalService.GetSerialNumber("SYS_Parameters");
                                model.ParameterTypeID = Type;
                                model.Sequence = 0;
                                model.UsingType = 0;
                                if (SYS_ParameterService.insert(userId, model))
                                    success++;
                                else
                                {
                                    reason += "序号[" + Sequence + "]:添加失败，未知原因;";
                                    nosuccess++;
                                }
                            }
                            else
                            {
                                reason += "序号[" + Sequence + "]的代号重复;";
                                nosuccess++;
                            }
                        }
                        else
                            nosuccess++;
                    }

                }
            }
            if (nosuccess == total)
                status = "410";

            msg = string.Format(msg, total, success, nosuccess, reason);

            return new { status = status, msg = msg };
        }

        /// <summary>
        /// 资源导入
        /// SAM 2017年5月12日14:36:04
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static object Inf00015Import(string token, string filename)
        {
            string userId = UtilBussinessService.detoken(token);
            string SysID = Framework.SystemID;
            string status = "200", msg = "共{0}条信息,{1}条信息导入成功,{2}条信息导入失败！{3}";
            string reason = null;
            int total = 0;
            int success = 0;
            int nosuccess = 0;
            if (filename.EndsWith(".xls") || filename.EndsWith(".xlsx"))
            {
                IWorkbook wk = null;
                bool Isright = true;
                string Sequence = null;
                SYS_Resources model = null;
                SYS_Parameters classModel = null;
                SYS_Parameters GroupModel = null;
                using (FileStream fs = File.OpenRead(@filename))   //打开.xls文件
                {
                    if (filename.EndsWith(".xls"))
                        wk = new HSSFWorkbook(fs); //把xls文件中的数据写入wk中
                    else if (filename.EndsWith(".xlsx"))
                        wk = new XSSFWorkbook(fs);//把xlsx文件中的数据写入wk中

                    ISheet sheet = wk.GetSheetAt(0);   //读取第一表数据
                    for (int j = 2; j <= sheet.LastRowNum; j++)  //LastRowNum 是当前表的总行数
                    {
                        IRow row = sheet.GetRow(j);  //读取当前行数据
                        if (row.GetCell(0) == null) //如果当前行的第一列没有数据，停止读取
                            break;
                        if (string.IsNullOrWhiteSpace(row.GetCell(0).ToString()))//如果当前行的第一列没有数据，停止读取，这是判断当row.GetCell不能null然后实际是空字符
                            break;
                        total++;
                        ICell cell = null;
                        Isright = true;
                        if (row != null)
                        {
                            Isright = true;
                            model = new SYS_Resources();

                            cell = row.GetCell(0);//序号
                            Sequence = cell.ToString();

                            cell = row.GetCell(1);//资源类别
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                classModel = SYS_ParameterService.getByCode(cell.ToString(), Framework.SystemID + "0191213000013");
                                if (classModel != null)
                                    model.ClassID = classModel.ParameterID;
                                else
                                    reason += "序号[" + Sequence + "]:资源类别错误,不存在的资源类别;";
                            }

                            cell = row.GetCell(2);//代号
                            if (cell != null)
                                model.Code = cell.ToString();
                            else
                            {
                                reason += "序号[" + Sequence + "]代号为空;";
                                nosuccess++;
                                continue; //结束本次循环
                            }
                            cell = row.GetCell(3);//说明
                            if (cell != null)
                                model.Description = cell.ToString();
                            else
                            {
                                reason += "序号[" + Sequence + "]说明为空;";
                                nosuccess++;
                                continue; //结束本次循环
                            }

                            cell = row.GetCell(4);//数量
                            if (cell != null)
                            {
                                try
                                {
                                    model.Quantity = decimal.Parse(cell.ToString());
                                }
                                catch
                                {
                                    model.Quantity = 1;
                                    reason += "序号[" + Sequence + "]:数量错误;";
                                }
                            }
                            else
                            {
                                model.Quantity = 1;
                                reason += "序号[" + Sequence + "]:数量为空;";
                            }

                            cell = row.GetCell(5);//资源群组
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                GroupModel = SYS_ParameterService.getByCode(cell.ToString(), Framework.SystemID + "0191213000014");
                                if (GroupModel != null)
                                    model.GroupID = GroupModel.ParameterID;
                            }

                            cell = row.GetCell(6);//备注
                            if (cell != null)
                                model.Comments = cell.ToString();
                            cell = row.GetCell(7);//状态
                            if (cell != null)
                            {
                                if (cell.ToString() == "正常")
                                    model.Status = SysID + "0201213000001";
                                else if (cell.ToString() == "作废")
                                    model.Status = SysID + "0201213000002";
                                else
                                {
                                    model.Status = SysID + "0201213000001";
                                    reason += "序号[" + Sequence + "]的状态错误,不存在的状态;";
                                }
                            }
                            else
                            {
                                model.Status = SysID + "0201213000001";
                                reason += "序号[" + Sequence + "]的状态为空;";
                            }
                        }
                        if (Isright)
                        {
                            if (!SYS_ResourcesService.CheckCode(model.Code, null))
                            {
                                model.ResourceID = UniversalService.GetSerialNumber("SYS_Resources");
                                if (SYS_ResourcesService.insert(userId, model))
                                    success++;
                                else
                                {
                                    reason += "序号[" + Sequence + "]:添加失败，未知原因;";
                                    nosuccess++;
                                }
                            }
                            else
                            {
                                reason += "序号[" + Sequence + "]的代号重复;";
                                nosuccess++;
                            }
                        }
                        else
                            nosuccess++;
                    }

                }
            }
            if (nosuccess == total)
                status = "410";

            msg = string.Format(msg, total, success, nosuccess, reason);

            return new
            {
                status = status,
                msg = msg
            };
        }

        /// <summary>
        /// 料品的导入
        /// SAM 2017年5月17日09:27:00
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static object Inf00010Import(string token, string filename)
        {
            string userId = UtilBussinessService.detoken(token);
            string SysID = Framework.SystemID;
            string status = "200", msg = "共{0}条信息,{1}条信息导入成功,{2}条信息导入失败！{3}";
            string reason = null;
            int total = 0;
            int success = 0;
            int nosuccess = 0;
            if (filename.EndsWith(".xls") || filename.EndsWith(".xlsx"))
            {
                IWorkbook wk = null;
                bool Isright = true;
                string Sequence = null;
                SYS_Items model = null;
                SYS_Parameters ParModel = null;
                SYS_AutoNumber AutoModel = null;
                List<string> ClassList = null;
                using (FileStream fs = File.OpenRead(@filename))   //打开.xls文件
                {
                    if (filename.EndsWith(".xls"))
                        wk = new HSSFWorkbook(fs); //把xls文件中的数据写入wk中
                    else if (filename.EndsWith(".xlsx"))
                        wk = new XSSFWorkbook(fs);//把xlsx文件中的数据写入wk中

                    ISheet sheet = wk.GetSheetAt(0);   //读取第一表数据
                    for (int j = 2; j <= sheet.LastRowNum; j++)  //LastRowNum 是当前表的总行数
                    {
                        IRow row = sheet.GetRow(j);  //读取当前行数据
                        if (row.GetCell(0) == null) //如果当前行的第一列没有数据，停止读取
                            break;
                        if (string.IsNullOrWhiteSpace(row.GetCell(0).ToString()))//如果当前行的第一列没有数据，停止读取，这是判断当row.GetCell不能null然后实际是空字符
                            break;
                        total++;
                        ICell cell = null;
                        Isright = true;
                        if (row != null)
                        {
                            Isright = true;
                            model = new SYS_Items();
                            ClassList = new List<string>();

                            cell = row.GetCell(0);//序号
                            Sequence = cell.ToString();

                            cell = row.GetCell(1);//料品代号
                            if (cell != null)
                            {
                                if (cell.ToString().Length <= 60)
                                    model.Code = cell.ToString();
                                else
                                {
                                    reason += "序号[" + Sequence + "]代号超过了60个字;";
                                    nosuccess++;
                                    continue; //结束本次循环
                                }
                            }
                            else
                            {
                                reason += "序号[" + Sequence + "]代号为空;";
                                nosuccess++;
                                continue; //结束本次循环
                            }
                            cell = row.GetCell(2);//名称
                            if (cell != null)
                            {
                                if (cell.ToString().Length <= 120)
                                    model.Name = cell.ToString();
                                else
                                {
                                    reason += "序号[" + Sequence + "]说明超过了120个字;";
                                    nosuccess++;
                                    continue; //结束本次循环
                                }
                            }
                            else
                            {
                                reason += "序号[" + Sequence + "]说明为空;";
                                nosuccess++;
                                continue; //结束本次循环
                            }
                            cell = row.GetCell(3);//规格
                            if (cell != null)
                            {
                                if (cell.ToString().Length <= 120)
                                    model.Specification = cell.ToString();
                                else
                                    reason += "序号[" + Sequence + "]规格超过了120个字;";
                            }
                            cell = row.GetCell(4);//状态
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                ParModel = SYS_ParameterService.getByCode(cell.ToString(), SysID + "0191213000001");
                                if (ParModel != null)
                                    model.Status = ParModel.ParameterID;
                                else
                                {
                                    model.Status = SysID + "0201213000001";
                                    reason += "序号[" + Sequence + "]的状态错误,不存在的状态;";
                                }
                            }
                            else
                            {
                                model.Status = SysID + "0201213000001";
                                reason += "序号[" + Sequence + "]的状态为空;";
                            }

                            cell = row.GetCell(5);//单位
                            if (cell != null)
                            {
                                ParModel = SYS_ParameterService.getByCode(cell.ToString(), Framework.SystemID + "019121300000C");
                                if (ParModel != null)
                                    model.Unit = ParModel.ParameterID;
                                else
                                    reason += "序号[" + Sequence + "]:单位错误,不存在的单位;";
                            }
                            else
                                reason += "序号[" + Sequence + "]:单位错误,不存在的单位;";

                            cell = row.GetCell(6);//分类一
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                ParModel = SYS_ParameterService.getByCode(cell.ToString(), Framework.SystemID + "019121300000B");
                                if (ParModel != null)
                                {
                                    model.ClassOne = ParModel.ParameterID;
                                    ClassList.Add(model.ClassOne);
                                }
                                else
                                    reason += "序号[" + Sequence + "]:分类一错误,不存在的分类;";
                            }


                            cell = row.GetCell(7);//分类二
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                ParModel = SYS_ParameterService.getByCode(cell.ToString(), Framework.SystemID + "019121300000B");
                                if (ParModel != null)
                                {
                                    model.ClassTwo = ParModel.ParameterID;
                                    ClassList.Add(model.ClassTwo);
                                }
                                else
                                    reason += "序号[" + Sequence + "]:分类二错误,不存在的分类;";
                            }


                            cell = row.GetCell(8);//分类三
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                ParModel = SYS_ParameterService.getByCode(cell.ToString(), Framework.SystemID + "019121300000B");
                                if (ParModel != null)
                                {
                                    model.ClassThree = ParModel.ParameterID;
                                    ClassList.Add(model.ClassThree);
                                }
                                else
                                    reason += "序号[" + Sequence + "]:分类三错误,不存在的分类;";
                            }

                            cell = row.GetCell(9);//分类四
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                ParModel = SYS_ParameterService.getByCode(cell.ToString(), Framework.SystemID + "019121300000B");
                                if (ParModel != null)
                                {
                                    model.ClassFour = ParModel.ParameterID;
                                    ClassList.Add(model.ClassFour);
                                }
                                else
                                    reason += "序号[" + Sequence + "]:分类四错误,不存在的分类;";
                            }

                            cell = row.GetCell(10);//分类五
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                ParModel = SYS_ParameterService.getByCode(cell.ToString(), Framework.SystemID + "019121300000B");
                                if (ParModel != null)
                                {
                                    model.ClassFive = ParModel.ParameterID;
                                    ClassList.Add(model.ClassFive);
                                }
                                else
                                    reason += "序号[" + Sequence + "]:分类五错误,不存在的分类;";
                            }


                            cell = row.GetCell(11);//辅助单位
                            if (cell != null)
                            {
                                ParModel = SYS_ParameterService.getByCode(cell.ToString(), Framework.SystemID + "019121300000C");
                                if (ParModel != null)
                                    model.AuxUnit = ParModel.ParameterID;
                                else
                                    reason += "序号[" + Sequence + "]:单位错误,不存在的单位;";
                            }

                            cell = row.GetCell(12);//辅助单位比
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                try
                                {
                                    model.AuxUnitRatio = decimal.Parse(cell.ToString());
                                    //if (model.AuxUnitRatio > decimal.Parse("99999,9999") || model.AuxUnitRatio <= decimal.Parse("0,0001"))
                                    if (model.AuxUnitRatio > decimal.Parse("99999,9999"))
                                    {
                                        model.AuxUnitRatio = null;
                                        reason += "序号[" + Sequence + "]:辅助单位比错误，超出了范围;";
                                    }
                                }
                                catch
                                {
                                    reason += "序号[" + Sequence + "]:辅助单位比错误,格式错误;";
                                }
                            }

                            cell = row.GetCell(13);//切除尾数
                            if (cell != null)
                            {
                                if (cell.ToString() == "1")
                                    model.IsCutMantissa = true;
                                else if (cell.ToString() == "0")
                                    model.IsCutMantissa = false;
                                else
                                {
                                    model.IsCutMantissa = false;
                                    reason += "序号[" + Sequence + "]:切除尾数字段错误,数据并不是“1”或“0”;";
                                }
                            }
                            else
                            {
                                model.IsCutMantissa = false;
                                reason += "序号[" + Sequence + "]:切除尾数字段错误,数据为空;";
                            }

                            cell = row.GetCell(14);//切除尾数小数位
                            if (cell != null)
                            {
                                ParModel = SYS_ParameterService.getByCode(cell.ToString(), Framework.SystemID + "0191213000002");
                                if (ParModel != null)
                                    model.CutMantissa = ParModel.ParameterID;
                                else
                                {
                                    model.CutMantissa = SysID + "020121300000C";
                                    reason += "序号[" + Sequence + "]:切除尾数小数错误,不存在的位数;";
                                }
                            }
                            else
                                reason += "序号[" + Sequence + "]:切除尾数小数错误,不存在的位数;";


                            cell = row.GetCell(15);//供应型态
                            if (cell != null)
                            {
                                ParModel = SYS_ParameterService.getByCode(cell.ToString(), Framework.SystemID + "0191213000003");
                                if (ParModel != null)
                                    model.Type = ParModel.ParameterID;
                                else
                                {
                                    reason += "序号[" + Sequence + "]:供应型态错误,不存在的供应型态;";
                                }
                            }
                            else
                                reason += "序号[" + Sequence + "]:供应型态错误,不存在的供应型态;";

                            cell = row.GetCell(16);//工程图号
                            if (cell != null)
                            {
                                if (cell.ToString().Length <= 30)
                                    model.Drawing = cell.ToString();
                                else
                                    reason += "序号[" + Sequence + "]:工程图号超过了30个字;";
                            }
                            cell = row.GetCell(17);//料品来源
                            if (cell != null)
                            {
                                ParModel = SYS_ParameterService.getByCode(cell.ToString(), Framework.SystemID + "0191213000053");
                                if (ParModel != null)
                                    model.PartSource = ParModel.ParameterID;
                                else
                                {
                                    reason += "序号[" + Sequence + "]:料品来源错误,不存在的料品来源;";
                                }
                            }
                            else
                                reason += "序号[" + Sequence + "]:料品来源错误,不存在的料品来源;";

                            cell = row.GetCell(18);//备注
                            if (cell != null)
                            {
                                if (cell.ToString().Length <= 120)
                                    model.Comments = cell.ToString();
                                else
                                    reason += "序号[" + Sequence + "]:备注超过了120个字;";
                            }

                            cell = row.GetCell(19);//条码
                            if (cell != null)
                            {
                                if (cell.ToString().Length <= 30)
                                    model.BarCord = cell.ToString();
                                else
                                    reason += "序号[" + Sequence + "]:条码超过了30个字;";
                            }
                            cell = row.GetCell(20);//检验群组
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                ParModel = SYS_ParameterService.getByCode(cell.ToString(), Framework.SystemID + "0191213000018");
                                if (ParModel != null)
                                    model.GroupID = ParModel.ParameterID;
                                else
                                {
                                    reason += "序号[" + Sequence + "]:检验群组错误,不存在的检验群组;";
                                }
                            }

                            cell = row.GetCell(21);//批号管控
                            if (cell != null)
                            {
                                if (cell.ToString() == "是")
                                    model.Lot = true;
                                else if (cell.ToString() == "否")
                                    model.Lot = false;
                                else
                                {
                                    model.Lot = false;
                                    reason += "序号[" + Sequence + "]:批号管控字段错误,数据并不是“是”或“否”;";
                                }
                            }
                            else
                                model.Lot = false;

                            cell = row.GetCell(22);//批号类别
                            if (cell != null)
                            {
                                AutoModel = SYS_AutoNumberService.getByCode(cell.ToString());
                                if (AutoModel != null)
                                    model.LotClassID = AutoModel.AutoNumberID;
                                else
                                {
                                    reason += "序号[" + Sequence + "]:批号类别错误,不存在的批号类别;";
                                }
                            }

                            cell = row.GetCell(23);//组批方式
                            if (cell != null)
                            {
                                ParModel = SYS_ParameterService.getByName(cell.ToString(), Framework.SystemID + "0191213000060");
                                if (ParModel != null)
                                    model.LotMethod = ParModel.ParameterID;
                                else
                                {
                                    reason += "序号[" + Sequence + "]:组批方式错误,不存在的组批方式;";
                                }
                            }


                            cell = row.GetCell(24);//超完工比率（%）
                            if (cell != null)
                            {
                                try
                                {
                                    model.OverRate = decimal.Parse(cell.ToString().Replace("%", ""));
                                    //if (model.OverRate > decimal.Parse("999.99") || model.OverRate <= decimal.Parse("0.01"))
                                    if (model.OverRate > decimal.Parse("999.99"))
                                    {
                                        model.OverRate = 0;
                                        reason += "序号[" + Sequence + "]:超完工比率错误，超出了范围;";
                                    }
                                }
                                catch
                                {
                                    reason += "序号[" + Sequence + "]:超完工比率错误,格式错误;";
                                }
                            }

                            cell = row.GetCell(25);//序号件
                            if (cell != null)
                            {
                                if (cell.ToString() == "是")
                                    model.SerialPart = true;
                                else if (cell.ToString() == "否")
                                    model.SerialPart = false;
                                else
                                {
                                    model.SerialPart = false;
                                    reason += "序号[" + Sequence + "]:序号件字段错误,数据并不是“是”或“否”;";
                                }
                            }
                            else
                                model.SerialPart = false;

                            cell = row.GetCell(26);//关键料号
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                if (cell.ToString() == "是")
                                    model.KeyPart = true;
                                else if (cell.ToString() == "否")
                                    model.KeyPart = false;
                                else
                                {
                                    model.KeyPart = false;
                                    reason += "序号[" + Sequence + "]:关键料号字段错误,数据并不是“是”或“否”;";
                                }
                            }
                            else
                                model.KeyPart = false;
                        }


                        int count = ClassList.Count;
                        if (count != 0)
                        {
                            int NCount = ClassList.Distinct().ToList().Count;
                            if (count != NCount)
                            {
                                Isright = false;
                                reason += "序号[" + Sequence + "]:5个分类存在重复项;";
                            }
                        }
                        if (Isright)
                        {
                            if (!SYS_ItemsService.CheckCode(model.Code, null))
                            {
                                model.ItemID = UniversalService.GetSerialNumber("SYS_Items");
                                if (SYS_ItemsService.insert(userId, model))
                                    success++;
                                else
                                {
                                    reason += "序号[" + Sequence + "]:添加失败，未知原因;";
                                    nosuccess++;
                                }
                            }
                            else
                            {
                                reason += "序号[" + Sequence + "]的代号重复;";
                                nosuccess++;
                            }
                        }
                        else
                            nosuccess++;
                    }

                }
            }
            if (nosuccess == total)
                status = "410";

            msg = string.Format(msg, total, success, nosuccess, reason);

            return new { status = status, msg = msg };
        }

        /// <summary>
        /// 批号自动编号设定的导入
        /// SAM 2017年5月17日16:12:46
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static object Inf00023Import(string token, string filename)
        {
            string userId = UtilBussinessService.detoken(token);
            string SysID = Framework.SystemID;
            string status = "200", msg = "共{0}条信息,{1}条信息导入成功,{2}条信息导入失败！{3}";
            string reason = null;
            int total = 0;
            int success = 0;
            int nosuccess = 0;
            if (filename.EndsWith(".xls") || filename.EndsWith(".xlsx"))
            {
                IWorkbook wk = null;
                bool Isright = true;
                string Sequence = null;
                SYS_AutoNumber model = null;
                using (FileStream fs = File.OpenRead(@filename))   //打开.xls文件
                {
                    if (filename.EndsWith(".xls"))
                        wk = new HSSFWorkbook(fs); //把xls文件中的数据写入wk中
                    else if (filename.EndsWith(".xlsx"))
                        wk = new XSSFWorkbook(fs);//把xlsx文件中的数据写入wk中

                    ISheet sheet = wk.GetSheetAt(0);   //读取第一表数据
                    for (int j = 2; j <= sheet.LastRowNum; j++)  //LastRowNum 是当前表的总行数
                    {
                        IRow row = sheet.GetRow(j);  //读取当前行数据
                        if (row.GetCell(0) == null) //如果当前行的第一列没有数据，停止读取
                            break;
                        if (string.IsNullOrWhiteSpace(row.GetCell(0).ToString()))//如果当前行的第一列没有数据，停止读取，这是判断当row.GetCell不能null然后实际是空字符
                            break;
                        total++;
                        ICell cell = null;
                        Isright = true;
                        if (row != null)
                        {
                            Isright = true;
                            model = new SYS_AutoNumber();

                            cell = row.GetCell(0);//序号
                            Sequence = cell.ToString();

                            cell = row.GetCell(1);//代号
                            if (cell != null)
                                model.Code = cell.ToString();
                            else
                            {
                                reason += "序号[" + Sequence + "]代号为空;";
                                nosuccess++;
                                continue; //结束本次循环
                            }
                            cell = row.GetCell(2);//名称
                            if (cell != null)
                                model.Description = cell.ToString();
                            else
                            {
                                reason += "序号[" + Sequence + "]说明为空;";
                                nosuccess++;
                                continue; //结束本次循环
                            }
                            cell = row.GetCell(3);//预设字元
                            if (cell != null)
                                model.DefaultCharacter = cell.ToString();
                            cell = row.GetCell(4);//年度码
                            if (cell != null)
                            {
                                if (cell.ToString() == "N")
                                    model.YearLength = 0;
                                else if (cell.ToString() == "Y")
                                    model.YearLength = 4;
                                else
                                {
                                    model.YearLength = 0;
                                    reason += "序号[" + Sequence + "]的年度码错误,不存在的年度码;";
                                }
                            }
                            else
                            {
                                model.YearLength = 0;
                                reason += "序号[" + Sequence + "]的年度码为空;";
                            }

                            cell = row.GetCell(5);//月度码
                            if (cell != null)
                            {
                                if (cell.ToString() == "N")
                                    model.MonthLength = 0;
                                else if (cell.ToString() == "Y")
                                    model.MonthLength = 2;
                                else
                                {
                                    model.MonthLength = 0;
                                    reason += "序号[" + Sequence + "]的月度码错误,不存在的月度码;";
                                }
                            }
                            else
                            {
                                model.MonthLength = 0;
                                reason += "序号[" + Sequence + "]的月度码为空;";
                            }

                            cell = row.GetCell(6);//日期码
                            if (cell != null)
                            {
                                if (cell.ToString() == "N")
                                    model.DateLength = 0;
                                else if (cell.ToString() == "Y")
                                    model.DateLength = 2;
                                else
                                {
                                    model.DateLength = 0;
                                    reason += "序号[" + Sequence + "]的日期码错误,不存在的日期码;";
                                }
                            }
                            else
                            {
                                model.DateLength = 0;
                                reason += "序号[" + Sequence + "]的日期码为空;";
                            }

                            cell = row.GetCell(7);//流水码长度
                            if (cell != null)
                            {
                                try
                                {
                                    model.NumLength = int.Parse(cell.ToString());
                                }
                                catch
                                {
                                    model.NumLength = 0;
                                    reason += "序号[" + Sequence + "]的流水码长度错误,格式错误;";
                                }
                            }

                            cell = row.GetCell(9);//备注
                            if (cell != null)
                                model.Comments = cell.ToString();
                        }
                        if (Isright)
                        {
                            if (!SYS_AutoNumberService.CheckCode(model.Code, null, null))
                            {
                                if (!SYS_AutoNumberService.CheckCode(null, model.Description, null))
                                {
                                    model.AutoNumberID = UniversalService.GetSerialNumber("SYS_AutoNumber");
                                    model.Length = model.DefaultCharacter.Length + model.YearLength + model.MonthLength + model.DateLength + model.NumLength;
                                    model.Status = SysID + "0201213000001";
                                    if (SYS_AutoNumberService.insert(userId, model))
                                        success++;
                                    else
                                    {
                                        reason += "序号[" + Sequence + "]:添加失败，未知原因;";
                                        nosuccess++;
                                    }
                                }
                                else
                                {
                                    reason += "序号[" + Sequence + "]的说明重复;";
                                    nosuccess++;
                                }
                            }
                            else
                            {
                                reason += "序号[" + Sequence + "]的代号重复;";
                                nosuccess++;
                            }
                        }
                        else
                            nosuccess++;
                    }
                }
            }
            if (nosuccess == total)
                status = "410";

            msg = string.Format(msg, total, success, nosuccess, reason);

            return new { status = status, msg = msg };
        }

        /// <summary>
        /// 行事历的导入
        /// SAM 2017年5月18日11:32:37
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static object Inf00014Import(string token, string filename)
        {
            string userId = UtilBussinessService.detoken(token);
            string SysID = Framework.SystemID;
            string status = "200", msg = "共{0}条信息,{1}条信息导入成功,{2}条信息导入失败！{3}";
            string reason = null;
            int total = 0;
            int success = 0;
            int nosuccess = 0;
            if (filename.EndsWith(".xls") || filename.EndsWith(".xlsx"))
            {
                IWorkbook wk = null;
                string Sequence = null;
                SYS_Calendar model = null;
                SYS_Parameters ParModel = null;
                SYS_Calendar Calendar = null;
                string Date = null;
                int Year = 0;
                int Month = 0;

                using (FileStream fs = File.OpenRead(@filename))   //打开.xls文件
                {
                    if (filename.EndsWith(".xls"))
                        wk = new HSSFWorkbook(fs); //把xls文件中的数据写入wk中
                    else if (filename.EndsWith(".xlsx"))
                        wk = new XSSFWorkbook(fs);//把xlsx文件中的数据写入wk中

                    ISheet sheet = wk.GetSheetAt(0);   //读取第一表数据
                    for (int j = 2; j <= sheet.LastRowNum; j++)  //LastRowNum 是当前表的总行数
                    {
                        IRow row = sheet.GetRow(j);  //读取当前行数据
                        if (row == null)
                            break;
                        if (row.GetCell(0) == null) //如果当前行的第一列没有数据，停止读取
                            break;
                        if (string.IsNullOrWhiteSpace(row.GetCell(0).ToString()))//如果当前行的第一列没有数据，停止读取，这是判断当row.GetCell不能null然后实际是空字符
                            break;
                        total++;
                        ICell cell = null;

                        model = new SYS_Calendar();

                        cell = row.GetCell(0);//序号
                        Sequence = cell.ToString();

                        cell = row.GetCell(1);//代号
                        if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                        {
                            if (cell.ToString().Length <= 30)
                                model.Code = cell.ToString();
                            else
                            {
                                reason += "序号[" + Sequence + "]代号超过30个字;";
                                nosuccess++;
                                continue; //结束本次循环
                            }
                        }
                        else
                        {
                            reason += "序号[" + Sequence + "]代号为空;";
                            nosuccess++;
                            continue; //结束本次循环
                        }
                        cell = row.GetCell(2);//名称
                        if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                        {
                            if (cell.ToString().Length <= 120)
                                model.Name = cell.ToString();
                            else
                            {
                                reason += "序号[" + Sequence + "]说明超过120个字;";
                                nosuccess++;
                                continue; //结束本次循环
                            }
                        }
                        else
                        {
                            reason += "序号[" + Sequence + "]说明为空;";
                            nosuccess++;
                            continue; //结束本次循环
                        }
                        cell = row.GetCell(3);//主行事历
                        if (cell != null)
                        {
                            if (cell.ToString() == "是")
                                model.Ifdefault = true;
                            else if (cell.ToString() == "否")
                                model.Ifdefault = false;
                            else
                            {
                                model.Ifdefault = false;
                                reason += "序号[" + Sequence + "]:是否主行事历错误,只能是“是”或者“否”;";
                            }
                        }
                        else
                        {
                            model.Ifdefault = false;
                            reason += "序号[" + Sequence + "]:是否主行事历为空;";
                        }
                        cell = row.GetCell(4);//状态
                        if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                        {
                            ParModel = SYS_ParameterService.getByCode(cell.ToString(), Framework.SystemID + "0191213000001");
                            if (ParModel != null)
                                model.Status = ParModel.ParameterID;
                            else
                            {
                                model.Status = SysID + "0201213000001";
                                reason += "序号[" + Sequence + "]的状态错误,不存在的状态;";
                            }
                        }
                        else
                        {
                            model.Status = SysID + "0201213000001";
                            reason += "序号[" + Sequence + "]的状态为空;";
                        }

                        cell = row.GetCell(5);//备注
                        if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                        {
                            if (cell.ToString().Length <= 120)
                                model.Comments = cell.ToString();
                            else
                            {
                                reason += "序号[" + Sequence + "]备注超过120个字;";
                            }
                        }

                        cell = row.GetCell(6);//日期
                        if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                        {
                            Date = cell.ToString();
                            try
                            {
                                DateTime T = DateTime.Parse(Date + "-01");
                                Year = int.Parse(Date.Split('-')[0]);
                                Month = int.Parse(Date.Split('-')[1]);
                            }
                            catch
                            {
                                Date = null;
                                reason += "序号[" + Sequence + "]:日期格式错误;";
                                nosuccess++;
                                continue; //结束本次循环
                            }
                        }

                        Calendar = SYS_CalendarService.getByCode(model.Code);
                        SYS_CalendarDetails Detail = null;
                        string sql = null;

                        if (Calendar == null)
                        {
                            if (model.Ifdefault)
                            {
                                if (SYS_CalendarService.CheckDefault(null))
                                {
                                    reason += "序号[" + Sequence + "]:已存在主行事历;";
                                    nosuccess++;
                                    continue; //结束本次循环
                                }
                            }

                            model.CalendarID = UniversalService.GetSerialNumber("SYS_Calendar");

                            if (SYS_CalendarService.insert(userId, model))
                            {
                                if (!string.IsNullOrWhiteSpace(Date))
                                {
                                    Detail = new SYS_CalendarDetails();
                                    Detail.CalendarDetailID = UniversalService.GetSerialNumber("SYS_CalendarDetails");
                                    Detail.CalendarID = model.CalendarID;
                                    Detail.Status = model.Status;
                                    int Month_number = DateTime.DaysInMonth(Year, Month);
                                    for (int i = 1; i <= Month_number; i++)
                                    {

                                        Detail.Yeardate = DateTime.Parse(Date + "-" + i.ToString().PadLeft(2, '0'));
                                        cell = row.GetCell(6 + i);
                                        if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                                        {
                                            try
                                            {
                                                Detail.Wkhour = decimal.Parse(cell.ToString());
                                                if (Detail.Wkhour < decimal.Parse("0.01") || Detail.Wkhour > decimal.Parse("24"))
                                                {
                                                    reason += "序号[" + Sequence + "]:日期" + i + "工时超过范围;";
                                                    Detail.Wkhour = 0;
                                                }

                                            }
                                            catch
                                            {
                                                reason += "序号[" + Sequence + "]:日期" + i + "工时格式错误;";
                                                Detail.Wkhour = 0;
                                            }
                                        }
                                        else
                                            Detail.Wkhour = 0;


                                        Detail.CalendarDetailID = UniversalService.GetSerialNumber("SYS_CalendarDetails");
                                        sql += SYS_CalendarDetailsService.insertSQL(userId, Detail);


                                    }
                                    if (!string.IsNullOrWhiteSpace(sql))
                                        UtilBussinessService.RunSQL(sql);
                                }

                                success++;
                            }
                            else
                            {
                                reason += "序号[" + Sequence + "]:添加失败，未知原因;";
                                nosuccess++;
                            }
                        }
                        else if (!string.IsNullOrWhiteSpace(Date))
                        {
                            Detail = new SYS_CalendarDetails();
                            Detail.CalendarDetailID = UniversalService.GetSerialNumber("SYS_CalendarDetails");
                            Detail.CalendarID = Calendar.CalendarID;
                            Detail.Status = Calendar.Status;
                            int Month_number = DateTime.DaysInMonth(Year, Month);
                            for (int i = 1; i <= Month_number; i++)
                            {

                                Detail.Yeardate = DateTime.Parse(Date + "-" + i.ToString().PadLeft(2, '0'));
                                cell = row.GetCell(6 + i);
                                if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                                {
                                    try
                                    {
                                        Detail.Wkhour = decimal.Parse(cell.ToString());
                                        if (Detail.Wkhour <= decimal.Parse("0.01") || Detail.Wkhour >= decimal.Parse("9.99"))
                                        {
                                            reason += "序号[" + Sequence + "]:日期" + 6 + i + "工时超过范围;";
                                            Detail.Wkhour = 0;
                                        }

                                    }
                                    catch
                                    {
                                        reason += "序号[" + Sequence + "]:日期" + 6 + i + "工时格式错误;";
                                        Detail.Wkhour = 0;
                                    }
                                }
                                else
                                    Detail.Wkhour = 0;


                                Detail.CalendarDetailID = UniversalService.GetSerialNumber("SYS_CalendarDetails");
                                sql += SYS_CalendarDetailsService.insertSQL(userId, Detail);


                            }
                            if (!string.IsNullOrWhiteSpace(sql))
                            {
                                UtilBussinessService.RunSQL(sql);
                                success++;
                            }
                            else
                            {
                                reason += "序号[" + Sequence + "]:添加失败，未知原因;";
                                nosuccess++;
                            }
                        }
                        else
                        {
                            reason += "序号[" + Sequence + "]的代号重复;";
                            nosuccess++;
                        }
                    }
                }
            }
            if (nosuccess == total)
                status = "410";

            msg = string.Format(msg, total, success, nosuccess, reason);

            return new { status = status, msg = msg };
        }

        /// <summary>
        /// 部门的导入
        /// SAM 2017年5月18日23:54:49
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static object Inf00005Import(string token, string filename)
        {
            string userId = UtilBussinessService.detoken(token);
            string SysID = Framework.SystemID;
            string status = "200", msg = "共{0}条信息,{1}条信息导入成功,{2}条信息导入失败！{3}";
            string reason = null;
            int total = 0;
            int success = 0;
            int nosuccess = 0;
            if (filename.EndsWith(".xls") || filename.EndsWith(".xlsx"))
            {
                IWorkbook wk = null;
                string Sequence = null;
                SYS_Organization model = null;
                //SYS_Parameters ParModel = null;
                SYS_Organization Plantmodel = null;
                //bool IfTop = false;
                using (FileStream fs = File.OpenRead(@filename))   //打开.xls文件
                {
                    if (filename.EndsWith(".xls"))
                        wk = new HSSFWorkbook(fs); //把xls文件中的数据写入wk中
                    else if (filename.EndsWith(".xlsx"))
                        wk = new XSSFWorkbook(fs);//把xlsx文件中的数据写入wk中

                    ISheet sheet = wk.GetSheetAt(0);   //读取第一表数据


                    for (int j = 2; j <= sheet.LastRowNum; j++)  //LastRowNum 是当前表的总行数
                    {
                        IRow row = sheet.GetRow(j);  //读取当前行数据
                        if (row.GetCell(0) == null) //如果当前行的第一列没有数据，停止读取
                            break;
                        if (string.IsNullOrWhiteSpace(row.GetCell(0).ToString()))//如果当前行的第一列没有数据，停止读取，这是判断当row.GetCell不能null然后实际是空字符
                            break;
                        total++;
                        ICell cell = null;
                        if (row != null)
                        {
                            model = new SYS_Organization();

                            cell = row.GetCell(0);//序号
                            Sequence = cell.ToString();

                            cell = row.GetCell(1);//代号
                            if (cell != null)
                                model.Code = cell.ToString();
                            else
                            {
                                reason += "序号[" + Sequence + "]代号为空;";
                                nosuccess++;
                                continue; //结束本次循环
                            }
                            cell = row.GetCell(2);//名称
                            if (cell != null)
                                model.Name = cell.ToString();
                            else
                            {
                                reason += "序号[" + Sequence + "]说明为空;";
                                nosuccess++;
                                continue; //结束本次循环
                            }

                            cell = row.GetCell(3);//备注
                            if (cell != null)
                                model.Comments = cell.ToString();

                            cell = row.GetCell(4);//厂别
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {

                                Plantmodel = SYS_OrganizationService.getByCode(cell.ToString(), SysID + "020121300001E");
                                if (Plantmodel != null)
                                    model.PlantID = Plantmodel.OrganizationID;
                                else
                                    reason += "序号[" + Sequence + "]:厂别错误,不存在的厂别;";
                            }
                            else
                            {
                                reason += "序号[" + Sequence + "]:厂别为空;";
                            }

                            cell = row.GetCell(5);//最上层註记
                            if (cell != null)
                            {
                                if (cell.ToString() == "N")
                                    model.IfTop = false;
                                else if (cell.ToString() == "Y")
                                    model.IfTop = true;
                                else
                                {
                                    model.IfTop = false;
                                    reason += "序号[" + Sequence + "]:最上层註记错误,只能是“N”或者“Y”;";
                                }
                            }
                            else
                            {
                                model.IfTop = false;
                                reason += "序号[" + Sequence + "]的最上层註记为空;";
                            }

                            cell = row.GetCell(6);//上层部门
                            if (cell != null)
                            {
                                Plantmodel = SYS_OrganizationService.getByCode(cell.ToString(), SysID + "020121300001D");
                                if (Plantmodel != null)
                                    model.ParentOrganizationID = Plantmodel.OrganizationID;
                                else
                                {
                                    reason += "序号[" + Sequence + "]:上层部门错误,不存在的上层部门;";
                                }

                            }
                            else
                            {
                                reason += "序号[" + Sequence + "]:上层部门为空;";
                            }

                            cell = row.GetCell(7);
                            if (cell != null)
                            {
                                if (cell.ToString() == "正常")
                                    model.Status = 1;
                                else if (cell.ToString() == "作废")
                                    model.Status = 0;
                                else
                                {
                                    model.Status = 1;
                                    reason += "序号[" + Sequence + "]:状态错误,不存在的状态;";
                                }
                            }
                            else
                            {
                                model.Status = 1;
                                reason += "序号[" + Sequence + "]:状态为空;";
                            }
                        }

                        if (!string.IsNullOrWhiteSpace(model.PlantID))
                        {
                            if (model.IfTop)
                            {
                                if (SYS_OrganizationService.CheckIfTop(model.PlantID, null))
                                {
                                    reason += "序号[" + Sequence + "]:的厂别已存在最上层部门;";
                                    nosuccess++;
                                    continue; //结束本次循环
                                }
                            }
                        }
                        if (!SYS_OrganizationService.CheckCode(model.Code, SysID + "020121300001D", null))
                        {
                            model.OrganizationID = UniversalService.GetSerialNumber("SYS_Organization");
                            model.Sequence = 0;
                            model.Type = Framework.SystemID + "020121300001D";
                            if (SYS_OrganizationService.insert(userId, model))
                                success++;
                            else
                            {
                                reason += "序号[" + Sequence + "]:添加失败，未知原因;";
                                nosuccess++;
                            }
                        }
                        else
                        {
                            reason += "序号[" + Sequence + "]的代号重复;";
                            nosuccess++;
                        }

                    }
                }
            }
            if (nosuccess == total)
                status = "410";

            msg = string.Format(msg, total, success, nosuccess, reason);

            return new { status = status, msg = msg };
        }

        /// <summary>
        /// 设备的导入
        /// SAM 2017年5月22日17:44:41
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static object Ems00001Import(string token, string filename)
        {
            string userId = UtilBussinessService.detoken(token);
            string SysID = Framework.SystemID;
            string status = "200", msg = "共{0}条信息,{1}条信息导入成功,{2}条信息导入失败！{3}";
            string reason = null;
            int total = 0;
            int success = 0;
            int nosuccess = 0;
            if (filename.EndsWith(".xls") || filename.EndsWith(".xlsx"))
            {
                IWorkbook wk = null;
                string Sequence = null;
                EMS_Equipment model = null;
                SYS_Parameters ParModel = null;
                SYS_PlantArea PlantAreamodel = null;
                SYS_Manufacturers MauModel = null;
                SYS_Organization OrgModel = null;
                using (FileStream fs = File.OpenRead(@filename))   //打开.xls文件
                {
                    if (filename.EndsWith(".xls"))
                        wk = new HSSFWorkbook(fs); //把xls文件中的数据写入wk中
                    else if (filename.EndsWith(".xlsx"))
                        wk = new XSSFWorkbook(fs);//把xlsx文件中的数据写入wk中
                    ISheet sheet = wk.GetSheetAt(0);   //读取第一表数据
                    for (int j = 2; j <= sheet.LastRowNum; j++)  //LastRowNum 是当前表的总行数
                    {
                        IRow row = sheet.GetRow(j);  //读取当前行数据
                        if (row.GetCell(0) == null) //如果当前行的第一列没有数据，停止读取
                            break;
                        if (string.IsNullOrWhiteSpace(row.GetCell(0).ToString()))//如果当前行的第一列没有数据，停止读取，这是判断当row.GetCell不能null然后实际是空字符
                            break;
                        total++;
                        ICell cell = null;
                        if (row != null)
                        {
                            model = new EMS_Equipment();

                            cell = row.GetCell(0);//序号
                            Sequence = cell.ToString();

                            cell = row.GetCell(1);//代号
                            if (cell != null)
                                model.Code = cell.ToString();
                            else
                            {
                                reason += "序号[" + Sequence + "]代号为空;";
                                nosuccess++;
                                continue; //结束本次循环
                            }
                            cell = row.GetCell(2);//名称
                            if (cell != null)
                                model.Name = cell.ToString();
                            else
                            {
                                reason += "序号[" + Sequence + "]说明为空;";
                                nosuccess++;
                                continue; //结束本次循环
                            }

                            cell = row.GetCell(3);//资源类别
                            if (cell != null)
                            {
                                ParModel = SYS_ParameterService.getByCode(cell.ToString(), Framework.SystemID + "0191213000013");
                                if (ParModel != null)
                                    model.ResourceCategory = ParModel.ParameterID;
                                else
                                {
                                    reason += "序号[" + Sequence + "]:资源类别错误,不存在的资源类别;";
                                }
                            }

                            cell = row.GetCell(4);//状态
                            if (cell != null)
                            {
                                ParModel = SYS_ParameterService.getByCode(cell.ToString(), Framework.SystemID + "0191213000001");
                                if (ParModel != null)
                                    model.Status = ParModel.ParameterID;
                                else
                                {
                                    model.Status = SysID + "0201213000001";
                                    reason += "序号[" + Sequence + "]:状态错误,不存在的状态;";
                                }
                            }
                            else
                            {
                                model.Status = SysID + "0201213000001";
                                reason += "序号[" + Sequence + "]:状态为空;";
                            }

                            cell = row.GetCell(5);//机况
                            if (cell != null)
                            {
                                ParModel = SYS_ParameterService.getByCode(cell.ToString(), Framework.SystemID + "0191213000055");
                                if (ParModel != null)
                                    model.Condition = ParModel.ParameterID;
                                else
                                {
                                    model.Condition = Framework.SystemID + "0201213000021";
                                    reason += "序号[" + Sequence + "]:机况错误,不存在的机况;";
                                }
                            }
                            else
                            {
                                model.Condition = Framework.SystemID + "0201213000021";
                                reason += "序号[" + Sequence + "]:机况为空;";
                            }

                            cell = row.GetCell(6);//异常状态
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                if (cell.ToString() == "正常")
                                    model.AbnormalStatus = false;
                                else if (cell.ToString() == "异常")
                                    model.AbnormalStatus = true;
                                else
                                {
                                    model.AbnormalStatus = false;
                                    reason += "序号[" + Sequence + "]:异常状态错误,不存在的异常状态;";
                                }
                            }
                            else
                            {
                                model.AbnormalStatus = false;
                                reason += "序号[" + Sequence + "]:异常状态为空;";
                            }

                            cell = row.GetCell(7);//固定资产编号
                            if (cell != null)
                                model.FixedAssets = cell.ToString();

                            cell = row.GetCell(8);//厂区
                            if (cell != null)
                            {
                                PlantAreamodel = SYS_PlantAreaService.getByCode(cell.ToString());
                                if (PlantAreamodel != null)
                                    model.PlantAreaID = PlantAreamodel.PlantAreaID;
                                else
                                {
                                    reason += "序号[" + Sequence + "]:厂区错误,不存在的厂区;";
                                }
                            }

                            cell = row.GetCell(9);//备注
                            if (cell != null)
                                model.Comments = cell.ToString();

                            cell = row.GetCell(10);//购入日期
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                try
                                {
                                    model.PurchaseDate = DateTime.Parse(cell.ToString());
                                }
                                catch
                                {
                                    reason += "序号[" + Sequence + "]:购入日期格式错误;";
                                }
                            }

                            cell = row.GetCell(11);//购买厂商
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                MauModel = SYS_ManufacturersService.getByCode(cell.ToString());
                                if (MauModel != null)
                                    model.ManufacturerID = MauModel.ManufacturerID;
                                else
                                    reason += "序号[" + Sequence + "]:厂商错误,不存在的厂商;";
                            }

                            cell = row.GetCell(12);//有效日期
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                try
                                {
                                    model.ExpireDate = DateTime.Parse(cell.ToString());
                                }
                                catch
                                {
                                    model.ExpireDate = DateTime.Parse("2099-12-31");
                                    reason += "序号[" + Sequence + "]:有效日期格式错误;";
                                }
                            }
                            else
                            {
                                model.ExpireDate = DateTime.Parse("2099-12-31");
                                reason += "序号[" + Sequence + "]:有效日期为空;";
                            }


                            cell = row.GetCell(13);//机型
                            if (cell != null)
                                model.Model = cell.ToString();

                            cell = row.GetCell(14);//机台序号
                            if (cell != null)
                                model.MachineNo = cell.ToString();

                            cell = row.GetCell(15);//设备分类一
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                ParModel = SYS_ParameterService.getClass(cell.ToString(), SysID + "0201213000041");
                                if (ParModel != null)
                                    model.ClassOne = ParModel.ParameterID;
                                else
                                {
                                    reason += "序号[" + Sequence + "]:分类一错误,不存在的分类一;";
                                }
                            }

                            cell = row.GetCell(16);//设备分类二
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                ParModel = SYS_ParameterService.getClass(cell.ToString(), SysID + "0201213000041");
                                if (ParModel != null)
                                    model.ClassTwo = ParModel.ParameterID;
                                else
                                {
                                    reason += "序号[" + Sequence + "]:分类二错误,不存在的分类二;";
                                }
                            }

                            cell = row.GetCell(17);//标准产能（HR）
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                try
                                {
                                    model.StdCapacity = decimal.Parse(cell.ToString());
                                }
                                catch
                                {
                                    model.StdCapacity = 0;
                                    reason += "序号[" + Sequence + "]:标准产能格式错误;";
                                }
                            }
                            else
                                model.StdCapacity = 0;

                            cell = row.GetCell(18);//累计产出数
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                try
                                {
                                    model.TotalOutput = decimal.Parse(cell.ToString());
                                }
                                catch
                                {
                                    model.TotalOutput = 0;
                                    reason += "序号[" + Sequence + "]:累计产出数格式错误;";
                                }
                            }
                            else
                                model.TotalOutput = 0;


                            cell = row.GetCell(19);//累计使用时间（分）
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                try
                                {
                                    model.UsedTime = decimal.Parse(cell.ToString());
                                }
                                catch
                                {
                                    model.UsedTime = 0;
                                    reason += "序号[" + Sequence + "]:累计使用时间格式错误;";
                                }
                            }
                            else
                                model.UsedTime = 0;

                            cell = row.GetCell(20);//可用時間(分)
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                try
                                {
                                    model.UsableTime = decimal.Parse(cell.ToString());
                                }
                                catch
                                {
                                    model.UsableTime = 0;
                                    reason += "序号[" + Sequence + "]:可用時間格式错误;";
                                }
                            }
                            else
                                model.UsableTime = 0;


                            cell = row.GetCell(21);//模出數
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                try
                                {
                                    model.CavityMold = decimal.Parse(cell.ToString());
                                }
                                catch
                                {
                                    model.CavityMold = 0;
                                    reason += "序号[" + Sequence + "]:模出數格式错误;";
                                }
                            }
                            else
                                model.CavityMold = 0;


                            cell = row.GetCell(22);//累計使用次數
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                try
                                {
                                    model.UsedTimes = decimal.Parse(cell.ToString());
                                }
                                catch
                                {
                                    model.UsedTimes = 0;
                                    reason += "序号[" + Sequence + "]:累計使用次數格式错误;";
                                }
                            }
                            else
                                model.UsedTimes = 0;

                            cell = row.GetCell(23);//可用次数
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                try
                                {
                                    model.UsableTimes = decimal.Parse(cell.ToString());
                                }
                                catch
                                {
                                    model.UsableTimes = 0;
                                    reason += "序号[" + Sequence + "]:累計使用次數格式错误;";
                                }
                            }
                            else
                                model.UsableTimes = 0;

                            cell = row.GetCell(24);//是否统计次数
                            if (cell != null)
                            {
                                if (cell.ToString() == "Y")
                                    model.StatisticsFlag = true;
                                else if (cell.ToString() == "N")
                                    model.StatisticsFlag = false;
                                else
                                {
                                    model.StatisticsFlag = true;
                                    reason += "序号[" + Sequence + "]:是否统计次数字段错误,不存在的数据;";
                                }
                            }
                            else
                            {
                                model.StatisticsFlag = true;
                                reason += "序号[" + Sequence + "]:是否统计次数字段为空;";
                            }

                            cell = row.GetCell(25);//保管部门
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                OrgModel = SYS_OrganizationService.getByCode(cell.ToString(), SysID + "020121300001D");
                                if (OrgModel != null)
                                    model.OrganizationID = OrgModel.OrganizationID;
                                else
                                    reason += "序号[" + Sequence + "]:部门错误,不存在的部门;";
                            }


                            cell = row.GetCell(26);//保养周期（日）
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                try
                                {
                                    model.MaintenanceTime = decimal.Parse(cell.ToString());
                                }
                                catch
                                {
                                    model.MaintenanceTime = 0;
                                    reason += "序号[" + Sequence + "]:保养周期（日）格式错误;";
                                }
                            }
                            else
                                model.MaintenanceTime = 0;


                            cell = row.GetCell(27);//保养周期（日）
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                try
                                {
                                    model.MaintenanceNum = decimal.Parse(cell.ToString());
                                }
                                catch
                                {
                                    model.MaintenanceNum = 0;
                                    reason += "序号[" + Sequence + "]:保养周期（次）格式错误;";
                                }
                            }
                            else
                                model.MaintenanceNum = 0;

                            cell = row.GetCell(28);//說明一
                            if (cell != null)
                                model.DescriptionOne = cell.ToString();

                            cell = row.GetCell(29);//說明二
                            if (cell != null)
                                model.DescriptionTwo = cell.ToString();

                            cell = row.GetCell(30);//日期一
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                try
                                {
                                    model.DateOne = DateTime.Parse(cell.ToString());
                                }
                                catch
                                {
                                    reason += "序号[" + Sequence + "]:日期一格式错误;";
                                }
                            }

                            cell = row.GetCell(31);//日期二
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                try
                                {
                                    model.DateTwo = DateTime.Parse(cell.ToString());
                                }
                                catch
                                {
                                    reason += "序号[" + Sequence + "]:日期二格式错误;";
                                }
                            }

                            cell = row.GetCell(32);//數值一
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                try
                                {
                                    model.NumOne = decimal.Parse(cell.ToString());
                                }
                                catch
                                {
                                    reason += "序号[" + Sequence + "]:數值一格式错误;";
                                }
                            }

                            cell = row.GetCell(33);//數值二
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                try
                                {
                                    model.NumTwo = decimal.Parse(cell.ToString());
                                }
                                catch
                                {
                                    reason += "序号[" + Sequence + "]:數值二格式错误;";
                                }
                            }
                        }
                        if (!EMS_EquipmentService.CheckCode(model.Code, null))
                        {
                            model.EquipmentID = UniversalService.GetSerialNumber("EMS_Equipment");
                            model.AttachmentID = UniversalService.GetSerialNumber("SYS_Attachments");
                            if (EMS_EquipmentService.insert(userId, model))
                                success++;
                            else
                            {
                                reason += "序号[" + Sequence + "]:添加失败，未知原因;";
                                nosuccess++;
                            }
                        }
                        else
                        {
                            reason += "序号[" + Sequence + "]:代号重复;";
                            nosuccess++;
                        }
                    }
                }
            }
            if (nosuccess == total)
                status = "410";

            msg = string.Format(msg, total, success, nosuccess, reason);

            return new { status = status, msg = msg };
        }

        /// <summary>
        /// 设备项目的导入
        /// SAM 2017年5月23日11:05:38
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static object Ems00001ProjectImport(string token, string filename)
        {
            string userId = UtilBussinessService.detoken(token);
            string SysID = Framework.SystemID;
            string status = "200", msg = "共{0}条信息,{1}条信息导入成功,{2}条信息导入失败！{3}";
            string reason = null;
            int total = 0;
            int success = 0;
            int nosuccess = 0;
            if (filename.EndsWith(".xls") || filename.EndsWith(".xlsx"))
            {
                IWorkbook wk = null;
                bool Isright = true;
                string Sequence = null;
                EMS_EquipmentProject model = null;
                EMS_Equipment Eqmodel = null;
                SYS_Parameters ParModel = null;
                SYS_Projects Promodel = null;
                IOT_Sensor SenModel = null;
                using (FileStream fs = File.OpenRead(@filename))   //打开.xls文件
                {
                    if (filename.EndsWith(".xls"))
                        wk = new HSSFWorkbook(fs); //把xls文件中的数据写入wk中
                    else if (filename.EndsWith(".xlsx"))
                        wk = new XSSFWorkbook(fs);//把xlsx文件中的数据写入wk中

                    ISheet sheet = wk.GetSheetAt(0);   //读取第一表数据


                    for (int j = 2; j <= sheet.LastRowNum; j++)  //LastRowNum 是当前表的总行数
                    {
                        IRow row = sheet.GetRow(j);  //读取当前行数据
                        if (row.GetCell(0) == null) //如果当前行的第一列没有数据，停止读取
                            break;
                        if (string.IsNullOrWhiteSpace(row.GetCell(0).ToString()))//如果当前行的第一列没有数据，停止读取，这是判断当row.GetCell不能null然后实际是空字符
                            break;
                        total++;
                        ICell cell = null;
                        Isright = true;
                        if (row != null)
                        {
                            Isright = true;
                            model = new EMS_EquipmentProject();

                            cell = row.GetCell(0);//序号
                            Sequence = cell.ToString();

                            cell = row.GetCell(1);//设备代号
                            if (cell != null)
                            {
                                Eqmodel = EMS_EquipmentService.getByCode(cell.ToString());
                                if (Eqmodel != null)
                                    model.EquipmentID = Eqmodel.EquipmentID;
                                else
                                {
                                    reason += "序号[" + Sequence + "]设备代号错误，不存在的设备;";
                                    nosuccess++;
                                    continue; //结束本次循环
                                }
                            }
                            else
                            {
                                reason += "序号[" + Sequence + "]设备代号为空;";
                                nosuccess++;
                                continue; //结束本次循环
                            }

                            cell = row.GetCell(4);//项目代号
                            if (cell != null)
                            {
                                Promodel = SYS_ProjectsService.getByCode(cell.ToString());
                                if (Promodel != null)
                                    model.ProjectID = Promodel.ProjectID;
                                else
                                {
                                    reason += "序号[" + Sequence + "]项目代号错误，不存在的项目代号;";
                                    nosuccess++;
                                    continue; //结束本次循环
                                }
                            }
                            else
                            {
                                reason += "序号[" + Sequence + "]项目代号为空;";
                                nosuccess++;
                                continue; //结束本次循环
                            }

                            cell = row.GetCell(7);//是否收集
                            if (cell != null)
                            {
                                if (cell.ToString() == "是")
                                    model.IfCollection = true;
                                else if (cell.ToString() == "否")
                                    model.IfCollection = false;
                                else
                                {
                                    model.IfCollection = true;
                                    reason += "序号[" + Sequence + "]是否收集字段错误，只能是“是”或者“否”;";
                                }
                            }
                            else
                            {
                                model.IfCollection = true;
                                reason += "序号[" + Sequence + "]是否收集字段为空;";
                            }


                            cell = row.GetCell(8);//收集方式
                            if (cell != null)
                            {
                                ParModel = SYS_ParameterService.getByCode(cell.ToString(), Framework.SystemID + "0191213000056");
                                if (ParModel != null)
                                    model.CollectionWay = ParModel.ParameterID;
                                else
                                {
                                    reason += "序号[" + Sequence + "]:收集方式错误,不存在的收集方式;";
                                }
                            }

                            cell = row.GetCell(9);//感知器代号
                            if (cell != null)
                            {
                                SenModel = IOT_SensorService.getByCode(cell.ToString());
                                if (SenModel != null)
                                    model.SensorID = SenModel.SensorID;
                                else
                                {
                                    reason += "序号[" + Sequence + "]:感知器错误,不存在的感知器;";
                                }
                            }

                            cell = row.GetCell(11);//标准值
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                if (Promodel.Attribute == "0201213000017")
                                {
                                    try
                                    {
                                        int Attribute = int.Parse(cell.ToString());
                                        model.StandardValue = cell.ToString();
                                    }
                                    catch
                                    {
                                        reason += "序号[" + Sequence + "]:标准值格式错误,只能是数字类型;";
                                    }
                                }
                                else
                                {
                                    model.StandardValue = cell.ToString();
                                }
                            }
                            cell = row.GetCell(12);//上限值
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                if (Promodel.Attribute == "0201213000017")
                                {
                                    try
                                    {
                                        int Attribute = int.Parse(cell.ToString());
                                        model.MaxValue = cell.ToString();
                                    }
                                    catch
                                    {
                                        reason += "序号[" + Sequence + "]:标准值格式错误,只能是数字类型;";
                                    }
                                }
                                else
                                {
                                    model.MaxValue = cell.ToString();
                                }
                            }
                            cell = row.GetCell(13);//下限值
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                if (Promodel.Attribute == "0201213000017")
                                {
                                    try
                                    {
                                        int Attribute = int.Parse(cell.ToString());
                                        model.MinValue = cell.ToString();
                                    }
                                    catch
                                    {
                                        reason += "序号[" + Sequence + "]:标准值格式错误,只能是数字类型;";
                                    }
                                }
                                else
                                {
                                    model.MinValue = cell.ToString();
                                }
                            }
                            cell = row.GetCell(14);//上限警告秒數
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                try
                                {
                                    model.MaxAlarmTime = int.Parse(cell.ToString());
                                }
                                catch
                                {
                                    model.MaxAlarmTime = 0;
                                    reason += "序号[" + Sequence + "]:上限警告秒數格式错误;";
                                }
                            }


                            cell = row.GetCell(15);//下限警告秒數
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                try
                                {
                                    model.MinAlarmTime = int.Parse(cell.ToString());
                                }
                                catch
                                {
                                    model.MinAlarmTime = 0;
                                    reason += "序号[" + Sequence + "]:上限警告秒數格式错误;";
                                }
                            }

                            cell = row.GetCell(16);//上限警告數值
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                if (cell.ToString().Length <= 120)
                                    model.MaxAlarmValue = cell.ToString();
                                else
                                    reason += "序号[" + Sequence + "]:上限警告數值大于120个字;";
                            }


                            cell = row.GetCell(17);//下限警告數值
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                if (cell.ToString().Length <= 120)
                                    model.MinAlarmValue = cell.ToString();
                                else
                                    reason += "序号[" + Sequence + "]:下限警告數值大于120个字;";
                            }

                            cell = row.GetCell(18);//备注
                            if (cell != null)
                                model.Comments = cell.ToString();
                        }

                        if (Isright)
                        {
                            if (!EMS_EquipmentProjectService.Check(model.SensorID, model.EquipmentID, model.ProjectID, null))
                            {
                                model.EquipmentProjectID = UniversalService.GetSerialNumber("EMS_EquipmentProject");
                                model.Status = SysID + "0201213000001";
                                if (EMS_EquipmentProjectService.insert(userId, model))
                                    success++;
                                else
                                {
                                    reason += "序号[" + Sequence + "]:添加失败，未知原因;";
                                    nosuccess++;
                                }
                            }
                            else
                            {
                                reason += "序号[" + Sequence + "]:项目资料重复;";
                                nosuccess++;
                            }
                        }
                        else
                            nosuccess++;
                    }
                }
            }
            if (nosuccess == total)
                status = "410";

            msg = string.Format(msg, total, success, nosuccess, reason);

            return new { status = status, msg = msg };
        }

        /// <summary>
        /// 设备的图样新增
        /// SAM 2017年5月23日10:02:17
        /// </summary>
        /// <param name="file"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static object Ems00001PatternAdd(Image file, JObject data)
        {
            try
            {
                string Token = data.Value<String>("Token");
                string EquipmentID = data.Value<String>("EquipmentID");
                string Name = data.Value<String>("Name");
                bool Default = data.Value<bool>("Default");

                string userId = UtilBussinessService.detoken(Token);
                string SysID = Framework.SystemID;


                if (!SYS_AttachmentsService.CheckName(Name, EquipmentID, null))
                {
                    string filePath = "Upload/Ems00001Pattern/" + EquipmentID + "/";
                    string strPath = HttpContext.Current.Server.MapPath("~/");
                    //假如文件夹不存在则新建
                    if (!Directory.Exists(strPath + filePath))
                    {
                        Directory.CreateDirectory(strPath + filePath);
                    }
                    int Sequence = SYS_AttachmentsService.getCount(EquipmentID);

                    SYS_Attachments model = new SYS_Attachments();
                    model.AttachmentID = UniversalService.GetSerialNumber("SYS_Attachments");
                    model.ObjectID = EquipmentID;
                    model.Name = Name;
                    model.OriginalName = Name;
                    model.Path = filePath + Name + ".png";
                    model.UploadTime = DateTime.Now;
                    model.IsNotInit = false;
                    model.Sequence = Sequence;
                    model.Default = Default;
                    if (model.Default)
                    {
                        SYS_AttachmentsService.updateDefault(EquipmentID);
                    }
                    model.Status = Framework.SystemID + "0201213000001";
                    //httpPostedFile.SaveAs(Path.Combine(strPath + model.Path)); //上传图片
                    file.Save(Path.Combine(strPath + filePath, Name + ".png"), ImageFormat.Png);

                    if (SYS_AttachmentsService.insert(userId, model))
                    {
                        return new { status = "200", msg = "新增成功！" };
                    }
                    else
                        return new { status = "410", msg = "新增失败！" };
                }
                else
                    return new { status = "410", msg = "新增失败！已存在名称！" };
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return new { status = "410", msg = "新增失败！" + ex.ToString() };
            }
        }



        /// <summary>
        /// 设备的图样编辑
        /// SAM 2017年5月27日15:09:10
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="AttachmentID"></param>
        /// <param name="Name"></param>
        /// <param name="Default"></param>
        /// <param name="httpPostedFile"></param>
        /// <returns></returns>
        public static object Ems00001PatternUpdate(Image file, JObject data)
        {

            try
            {
                string Token = data.Value<String>("Token");
                string AttachmentID = data.Value<String>("AttachmentID");
                string Name = data.Value<String>("Name");
                bool Default = data.Value<bool>("Default");

                SYS_Attachments attModel = SYS_AttachmentsService.get(AttachmentID);
                if (attModel == null)
                    return new { status = "410", msg = "不存在的设备图样信息" };

                if (!SYS_AttachmentsService.CheckName(Name, attModel.ObjectID, AttachmentID))
                {
                    string userId = UtilBussinessService.detoken(Token);
                    string SysID = Framework.SystemID;

                    string filePath = "Upload/Ems00001Pattern/" + attModel.ObjectID + "/";
                    string strPath = HttpContext.Current.Server.MapPath("~/");
                    //假如文件夹不存在则新建
                    if (!Directory.Exists(strPath + filePath))
                    {
                        Directory.CreateDirectory(strPath + filePath);
                    }
                    attModel.OriginalName = Name;
                    attModel.Path = filePath + Name + ".png";

                    attModel.Name = Name;
                    attModel.Default = Default;
                    if (attModel.Default)
                    {
                        SYS_AttachmentsService.updateDefault(attModel.ObjectID);
                    }
                    //File.Delete(Path.Combine(strPath + filePath, Name + ".png"));
                    file.Save(Path.Combine(strPath + filePath, Name + ".png"), ImageFormat.Png);
                    if (SYS_AttachmentsService.update(userId, attModel))
                        return new { status = "200", msg = "编辑成功！" };
                    else
                        return new { status = "410", msg = "编辑失败！" };
                }
                else
                    return new { status = "410", msg = "编辑失败！已存在名称！" };
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return new { status = "410", msg = "编辑失败！" + ex.ToString() };
            }
        }

        /// <summary>
        /// 感知器的导入
        /// SAM 2017年5月23日14:22:16
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static object Iot00001Import(string token, string filename)
        {
            string userId = UtilBussinessService.detoken(token);
            string SysID = Framework.SystemID;
            string status = "200", msg = "共{0}条信息,{1}条信息导入成功,{2}条信息导入失败！{3}";
            string reason = null;
            decimal MaxValue = decimal.Parse("999999999999.9999");
            decimal MinValue = decimal.Parse("0.0001");
            int total = 0;
            int success = 0;
            int nosuccess = 0;
            if (filename.EndsWith(".xls") || filename.EndsWith(".xlsx"))
            {
                IWorkbook wk = null;
                bool Isright = true;
                string Sequence = null;
                IOT_Sensor model = null;
                SYS_Manufacturers Manmodel = null;
                using (FileStream fs = File.OpenRead(@filename))   //打开.xls文件
                {
                    if (filename.EndsWith(".xls"))
                        wk = new HSSFWorkbook(fs); //把xls文件中的数据写入wk中
                    else if (filename.EndsWith(".xlsx"))
                        wk = new XSSFWorkbook(fs);//把xlsx文件中的数据写入wk中

                    ISheet sheet = wk.GetSheetAt(0);   //读取第一表数据
                    for (int j = 2; j <= sheet.LastRowNum; j++)  //LastRowNum 是当前表的总行数
                    {
                        IRow row = sheet.GetRow(j);  //读取当前行数据
                        if (row.GetCell(0) == null) //如果当前行的第一列没有数据，停止读取
                            break;
                        if (string.IsNullOrWhiteSpace(row.GetCell(0).ToString()))//如果当前行的第一列没有数据，停止读取，这是判断当row.GetCell不能null然后实际是空字符
                            break;
                        total++;
                        ICell cell = null;
                        Isright = true;
                        if (row != null)
                        {
                            Isright = true;
                            model = new IOT_Sensor();

                            cell = row.GetCell(0);//序号
                            Sequence = cell.ToString();

                            cell = row.GetCell(1);//代号
                            if (cell != null)
                            {
                                if (cell.ToString().Length <= 20)
                                    model.Code = cell.ToString();
                                else
                                {
                                    reason += "序号[" + Sequence + "]设备代号超过20个字;";
                                    nosuccess++;
                                    continue; //结束本次循环
                                }
                            }
                            else
                            {
                                reason += "序号[" + Sequence + "]设备代号为空;";
                                nosuccess++;
                                continue; //结束本次循环
                            }

                            cell = row.GetCell(2);//说明
                            if (cell != null)
                            {
                                if (cell.ToString().Length <= 60)
                                    model.Name = cell.ToString();
                                else
                                {
                                    reason += "序号[" + Sequence + "]:说明超过60个字;";
                                    nosuccess++;
                                    continue; //结束本次循环
                                }
                            }
                            else
                            {
                                reason += "序号[" + Sequence + "]:说明为空;";
                                nosuccess++;
                                continue; //结束本次循环
                            }

                            cell = row.GetCell(3);//备注
                            if (cell != null)
                            {
                                if (cell.ToString().Length <= 120)
                                    model.Comments = cell.ToString();
                                else
                                {
                                    reason += "序号[" + Sequence + "]:备注超过120个字;";
                                }
                            }

                            cell = row.GetCell(4);//状态
                            if (cell != null)
                            {
                                if (cell.ToString() == "正常")
                                    model.Status = SysID + "0201213000001";
                                else if (cell.ToString() == "作废")
                                    model.Status = SysID + "0201213000002";
                                else
                                {
                                    model.Status = SysID + "0201213000001";
                                    reason += "序号[" + Sequence + "]:状态错误,不存在的状态;";
                                }
                            }
                            else
                            {
                                model.Status = SysID + "0201213000001";
                                reason += "序号[" + Sequence + "]:状态为空;";
                            }



                            cell = row.GetCell(5);//启用日期
                            if (cell != null)
                            {
                                try
                                {
                                    model.EnabledDate = DateTime.Parse(cell.ToString());
                                }
                                catch
                                {
                                    reason += "序号[" + Sequence + "]:启用日期格式错误;";
                                }
                            }

                            cell = row.GetCell(6);//失效日期
                            if (cell != null)
                            {
                                try
                                {
                                    model.FailureDate = DateTime.Parse(cell.ToString());
                                }
                                catch
                                {
                                    reason += "序号[" + Sequence + "]:失效日期格式错误;";
                                }
                            }

                            cell = row.GetCell(7);//品牌
                            if (cell != null)
                            {
                                if (cell.ToString().Length <= 60)
                                    model.Brand = cell.ToString();
                                else
                                {
                                    reason += "序号[" + Sequence + "]:品牌超过60个字;";
                                }
                            }

                            cell = row.GetCell(8);//型号
                            if (cell != null)
                            {
                                if (cell.ToString().Length <= 60)
                                    model.Type = cell.ToString();
                                else
                                {
                                    reason += "序号[" + Sequence + "]:型号超过60个字;";
                                }
                            }

                            cell = row.GetCell(9);//厂商代号
                            if (cell != null)
                            {
                                Manmodel = SYS_ManufacturersService.getByCode(cell.ToString());
                                if (Manmodel != null)
                                    model.ManufacturerID = Manmodel.ManufacturerID;
                                else
                                {
                                    reason += "序号[" + Sequence + "]:厂商代号错误，不存在的厂商代号;";
                                }
                            }

                            cell = row.GetCell(11);//上限值
                            if (cell != null)
                            {
                                try
                                {
                                    model.MaxValue = decimal.Parse(cell.ToString());
                                    if (model.MaxValue > MaxValue || model.MaxValue < MinValue)
                                    {
                                        model.MaxValue = null;
                                        reason += "序号[" + Sequence + "]:上限值不在范围值内;";
                                    }
                                }
                                catch
                                {
                                    reason += "序号[" + Sequence + "]:上限值格式错误;";
                                }
                            }

                            cell = row.GetCell(12);//下限值
                            if (cell != null)
                            {
                                try
                                {
                                    model.MinValue = decimal.Parse(cell.ToString());
                                    if (model.MinValue > MaxValue || model.MinValue < MinValue)
                                    {
                                        model.MinValue = null;
                                        reason += "序号[" + Sequence + "]:下限值不在范围值内;";
                                    }
                                }
                                catch
                                {
                                    reason += "序号[" + Sequence + "]:下限值格式错误;";
                                }
                            }

                            cell = row.GetCell(13);//画面是否警示
                            if (cell != null)
                            {
                                if (cell.ToString() == "是")
                                    model.IsWarning = true;
                                else if (cell.ToString() == "否")
                                    model.IsWarning = false;
                                else
                                {
                                    model.IsWarning = false;
                                    reason += "序号[" + Sequence + "]:画面是否警示错误,只能是“是”或者“否”;";
                                }
                            }
                            else
                            {
                                model.IsWarning = false;
                                reason += "序号[" + Sequence + "]:画面是否警示为空;";
                            }


                            cell = row.GetCell(14);//上限警告秒數
                            if (cell != null)
                            {
                                try
                                {
                                    if (cell.ToString().Length <= 10)
                                        model.MaxAlarmTime = int.Parse(cell.ToString());
                                    else
                                    {
                                        model.MaxAlarmTime = 0;
                                        reason += "序号[" + Sequence + "]:上限警告秒數超过10位数;";
                                    }
                                }
                                catch
                                {
                                    model.MaxAlarmTime = 0;
                                    reason += "序号[" + Sequence + "]:上限警告秒數格式错误;";
                                }
                            }
                            else
                                model.MaxAlarmTime = 0;

                            cell = row.GetCell(15);//下限警告秒數
                            if (cell != null)
                            {
                                try
                                {
                                    if (cell.ToString().Length <= 10)
                                        model.MinAlarmTime = int.Parse(cell.ToString());
                                    else
                                    {
                                        model.MinAlarmTime = 0;
                                        reason += "序号[" + Sequence + "]:下限警告秒數超过10位数;";
                                    }

                                }
                                catch
                                {
                                    model.MinAlarmTime = 0;
                                    reason += "序号[" + Sequence + "]:下限警告秒數格式错误;";
                                }
                            }
                            else
                                model.MinAlarmTime = 0;

                        }

                        if (Isright)
                        {
                            if (!IOT_SensorService.CheckCode(model.Code, null))
                            {
                                model.SensorID = UniversalService.GetSerialNumber("IOT_Sensor");
                                if (IOT_SensorService.insert(userId, model))
                                    success++;
                                else
                                {
                                    reason += "序号[" + Sequence + "]:添加失败，未知原因;";
                                    nosuccess++;
                                }
                            }
                            else
                            {
                                reason += "序号[" + Sequence + "]的代号重复;";
                                nosuccess++;
                            }
                        }
                        else
                            nosuccess++;
                    }
                }
            }
            if (nosuccess == total)
                status = "410";

            msg = string.Format(msg, total, success, nosuccess, reason);

            return new { status = status, msg = msg };
        }

        /// <summary>
        /// 制程的导入
        /// SAM 2017年5月25日10:34:49
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static object Inf00018ProcessImport(string token, string filename)
        {
            string userId = UtilBussinessService.detoken(token);
            string SysID = Framework.SystemID;
            string status = "200", msg = "共{0}条信息,{1}条信息导入成功,{2}条信息导入失败！{3}";
            string reason = null;
            int total = 0;
            int success = 0;
            int nosuccess = 0;
            String Type = Framework.SystemID + "0191213000017";
            if (filename.EndsWith(".xls") || filename.EndsWith(".xlsx"))
            {
                IWorkbook wk = null;
                bool Isright = true;
                string Sequence = null;
                SYS_Parameters model = null;
                using (FileStream fs = File.OpenRead(@filename))   //打开.xls文件
                {
                    if (filename.EndsWith(".xls"))
                        wk = new HSSFWorkbook(fs); //把xls文件中的数据写入wk中
                    else if (filename.EndsWith(".xlsx"))
                        wk = new XSSFWorkbook(fs);//把xlsx文件中的数据写入wk中

                    ISheet sheet = wk.GetSheetAt(0);   //读取第一表数据
                    for (int j = 2; j <= sheet.LastRowNum; j++)  //LastRowNum 是当前表的总行数
                    {
                        IRow row = sheet.GetRow(j);  //读取当前行数据
                        if (row.GetCell(0) == null) //如果当前行的第一列没有数据，停止读取
                            break;
                        if (string.IsNullOrWhiteSpace(row.GetCell(0).ToString()))//如果当前行的第一列没有数据，停止读取，这是判断当row.GetCell不能null然后实际是空字符
                            break;
                        total++;
                        ICell cell = null;
                        Isright = true;
                        if (row != null)
                        {
                            Isright = true;
                            model = new SYS_Parameters();

                            cell = row.GetCell(0);//序号
                            Sequence = cell.ToString();
                            cell = row.GetCell(1);//代号
                            if (cell != null)
                            {
                                if (cell.ToString().Length <= 20)
                                    model.Code = cell.ToString();
                                else
                                {
                                    reason += "代号[" + model.Code + "]超过了20个字;";
                                    nosuccess++;
                                    continue; //结束本次循环
                                }
                            }
                            else
                            {
                                reason += "序号[" + Sequence + "]代号为空;";
                                nosuccess++;
                                continue; //结束本次循环
                            }
                            cell = row.GetCell(2);//说明
                            if (cell != null)
                            {
                                if (cell.ToString().Length <= 60)
                                    model.Name = cell.ToString();
                                else
                                {
                                    reason += "代号[" + model.Name + "]超过了60个字;";
                                    nosuccess++;
                                    continue; //结束本次循环
                                }
                            }
                            else
                            {
                                reason += "序号[" + Sequence + "]说明为空;";
                                nosuccess++;
                                continue; //结束本次循环
                            }

                            cell = row.GetCell(3);//备注
                            if (cell != null)//启用工序
                            {
                                if (cell.ToString() == "Y")
                                    model.IsDefault = true;
                                else if (cell.ToString() == "N")
                                    model.IsDefault = false;
                                else
                                {
                                    model.IsDefault = false;
                                    reason += "序号[" + Sequence + "]:启用工序错误,只能是N或者Y;";
                                }
                            }
                            else
                            {
                                model.IsDefault = false;
                                reason += "序号[" + Sequence + "]:启用工序为空;";
                            }

                            cell = row.GetCell(4);//状态
                            if (cell != null)
                            {
                                if (cell.ToString() == "正常")
                                    model.IsEnable = 1;
                                else if (cell.ToString() == "作废")
                                    model.IsEnable = 2;
                                else
                                {
                                    model.IsEnable = 1;
                                    reason += "序号[" + Sequence + "]的状态错误,不存在的状态;";
                                }
                            }
                            else
                            {
                                model.IsEnable = 1;
                                reason += "序号[" + Sequence + "]的状态为空;";
                            }

                            cell = row.GetCell(5);//备注
                            if (cell != null)
                            {
                                if (cell.ToString().Length <= 120)
                                    model.Comments = cell.ToString();
                                else
                                {
                                    reason += "序号[" + Sequence + "]:备注超过了120个字;";
                                }
                            }

                        }
                        if (Isright)
                        {
                            if (!SYS_ParameterService.CheckParameter(model.Code, null, null, Type, null))
                            {
                                model.ParameterID = UniversalService.GetSerialNumber("SYS_Parameters");
                                model.ParameterTypeID = Type;
                                model.Sequence = 0;
                                model.UsingType = 0;
                                if (SYS_ParameterService.insert(userId, model))
                                    success++;
                                else
                                {
                                    reason += "序号[" + Sequence + "]:添加失败，未知原因;";
                                    nosuccess++;
                                }
                            }
                            else
                            {
                                reason += "序号[" + Sequence + "]的代号重复;";
                                nosuccess++;
                            }
                        }
                        else
                            nosuccess++;
                    }

                }
            }
            if (nosuccess == total)
                status = "410";

            msg = string.Format(msg, total, success, nosuccess, reason);

            return new { status = status, msg = msg };
        }

        /// <summary>
        /// 工作中心的导入
        /// SAM 2017年5月25日11:16:42
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static object Inf00018WorkCenterImport(string token, string filename)
        {
            string userId = UtilBussinessService.detoken(token);
            string SysID = Framework.SystemID;
            string status = "200", msg = "共{0}条信息,{1}条信息导入成功,{2}条信息导入失败！{3}";
            string reason = null;
            int total = 0;
            int success = 0;
            int nosuccess = 0;
            if (filename.EndsWith(".xls") || filename.EndsWith(".xlsx"))
            {
                IWorkbook wk = null;
                bool Isright = true;
                string Sequence = null;
                SYS_WorkCenter model = null;
                SYS_Parameters ParModel = null;
                SYS_Organization Orgmodel = null;
                SYS_Manufacturers Manumodel = null;
                SYS_Calendar CalModel = null;
                using (FileStream fs = File.OpenRead(@filename))   //打开.xls文件
                {
                    if (filename.EndsWith(".xls"))
                        wk = new HSSFWorkbook(fs); //把xls文件中的数据写入wk中
                    else if (filename.EndsWith(".xlsx"))
                        wk = new XSSFWorkbook(fs);//把xlsx文件中的数据写入wk中

                    ISheet sheet = wk.GetSheetAt(0);   //读取第一表数据
                    for (int j = 2; j <= sheet.LastRowNum; j++)  //LastRowNum 是当前表的总行数
                    {
                        IRow row = sheet.GetRow(j);  //读取当前行数据
                        if (row.GetCell(0) == null) //如果当前行的第一列没有数据，停止读取
                            break;
                        if (string.IsNullOrWhiteSpace(row.GetCell(0).ToString()))//如果当前行的第一列没有数据，停止读取，这是判断当row.GetCell不能null然后实际是空字符
                            break;
                        total++;
                        ICell cell = null;
                        Isright = true;
                        if (row != null)
                        {
                            Isright = true;
                            model = new SYS_WorkCenter();

                            cell = row.GetCell(0);//序号
                            Sequence = cell.ToString();

                            cell = row.GetCell(1);//代号
                            if (cell != null)
                            {
                                if (cell.ToString().Length <= 20)
                                    model.Code = cell.ToString();
                                else
                                {
                                    reason += "代号[" + model.Code + "]超过了20个字;";
                                    nosuccess++;
                                    continue; //结束本次循环
                                }
                            }
                            else
                            {
                                reason += "序号[" + Sequence + "]代号为空;";
                                nosuccess++;
                                continue; //结束本次循环
                            }
                            cell = row.GetCell(2);//说明
                            if (cell != null)
                            {
                                if (cell.ToString().Length <= 60)
                                    model.Name = cell.ToString();
                                else
                                {
                                    reason += "代号[" + model.Name + "]超过了60个字;";
                                    nosuccess++;
                                    continue; //结束本次循环
                                }
                            }
                            else
                            {
                                reason += "序号[" + Sequence + "]说明为空;";
                                nosuccess++;
                                continue; //结束本次循环
                            }

                            cell = row.GetCell(3);//内外制
                            if (cell != null)
                            {
                                ParModel = SYS_ParameterService.getByCode(cell.ToString(), SysID + "0191213000058");
                                if (ParModel != null)
                                    model.InoutMark = ParModel.ParameterID;
                                else
                                {
                                    model.InoutMark = SysID + "020121300002E";
                                    reason += "序号[" + Sequence + "]:内外制错误,只能是內製或者外製;";
                                }
                            }
                            else
                            {
                                model.InoutMark = SysID + "020121300002E";
                                reason += "序号[" + Sequence + "]:内外制为空;";
                            }
                            cell = row.GetCell(4);//部门/厂商
                            if (cell != null)
                            {
                                if (model.InoutMark == SysID + "020121300002E")
                                {
                                    Orgmodel = SYS_OrganizationService.getByCode(cell.ToString(), SysID + "020121300001D");
                                    if (Orgmodel != null)
                                        model.DepartmentID = Orgmodel.OrganizationID;
                                    else
                                        reason += "序号[" + Sequence + "]:部门/厂商错误,不存在的部门;";
                                }
                                else
                                {
                                    Manumodel = SYS_ManufacturersService.getByCode(cell.ToString());
                                    if (Manumodel != null)
                                        model.DepartmentID = Manumodel.ManufacturerID;
                                    else
                                        reason += "序号[" + Sequence + "]:部门/厂商错误,不存在的厂商;";
                                }
                            }
                            cell = row.GetCell(5);//行事历
                            if (cell != null)
                            {
                                CalModel = SYS_CalendarService.getByCode(cell.ToString());
                                if (CalModel != null)
                                    model.CalendarID = CalModel.CalendarID;
                                else
                                    reason += "序号[" + Sequence + "]:行事历错误,不存在的行事历;";
                            }

                            cell = row.GetCell(6);//启用班别
                            if (cell != null)
                            {
                                if (cell.ToString() == "Y")
                                    model.IsClass = true;
                                else if (cell.ToString() == "N")
                                    model.IsClass = false;
                                else
                                {
                                    model.IsClass = false;
                                    reason += "序号[" + Sequence + "]:启用班别格式不对,现导入默认值;";
                                }
                            }
                            else
                            {
                                model.IsClass = false;
                                reason += "序号[" + Sequence + "]:启用班别为空,现导入默认值;";
                            }
                            cell = row.GetCell(7);//资源报工
                            if (cell != null)
                            {
                                if (cell.ToString() == "Y")
                                    model.ResourceReport = true;
                                else if (cell.ToString() == "N")
                                    model.ResourceReport = false;
                                else
                                {
                                    model.ResourceReport = false;
                                    reason += "序号[" + Sequence + "]:资源报工格式不对,现导入默认值;";
                                }
                            }
                            else
                            {
                                model.ResourceReport = false;
                                reason += "序号[" + Sequence + "]:资源报工为空,现导入默认值;";
                            }
                            cell = row.GetCell(8);//派工模式
                            if (cell != null)
                            {
                                ParModel = SYS_ParameterService.getByCode(cell.ToString(), SysID + "0191213000066");
                                if (ParModel != null)
                                    model.DispatchMode = ParModel.ParameterID;
                                else
                                    reason += "序号[" + Sequence + "]:派工模式错误,不存在的派工模式;";
                            }
                            else
                            {
                                model.DispatchMode = null;
                                reason += "序号[" + Sequence + "]:派工模式为空;";
                            }

                            cell = row.GetCell(9);//状态
                            if (cell != null)
                            {
                                if (cell.ToString() == "1")
                                    model.Status = SysID + "0201213000001";
                                else if (cell.ToString() == "0")
                                    model.Status = SysID + "0201213000002";
                                else
                                {
                                    model.Status = SysID + "0201213000001";
                                    reason += "序号[" + Sequence + "]:状态错误,不存在的状态;";
                                }
                            }
                            else
                            {
                                model.Status = SysID + "0201213000001";
                                reason += "序号[" + Sequence + "]:状态为空;";
                            }

                            cell = row.GetCell(10);//备注
                            if (cell != null)
                            {
                                if (cell.ToString().Length <= 120)
                                    model.Comments = cell.ToString();
                                else
                                {
                                    reason += "序号[" + Sequence + "]:备注超过了120个字;";
                                }
                            }
                        }
                        if (Isright)
                        {
                            if (!SYS_WorkCenterService.CheckCode(model.Code, null))
                            {
                                model.WorkCenterID = UniversalService.GetSerialNumber("SYS_WorkCenter");
                                if (SYS_WorkCenterService.insert(userId, model))
                                    success++;
                                else
                                {
                                    reason += "序号[" + Sequence + "]:添加失败，未知原因;";
                                    nosuccess++;
                                }
                            }
                            else
                            {
                                reason += "序号[" + Sequence + "]的代号重复;";
                                nosuccess++;
                            }
                        }
                        else
                            nosuccess++;
                    }
                }
            }
            if (nosuccess == total)
                status = "410";

            msg = string.Format(msg, total, success, nosuccess, reason);

            return new { status = status, msg = msg };
        }

        /// <summary>
        /// 单据子轨设定的导入
        /// SAM 2017年6月2日15:01:11
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static object Inf00016Import(string token, string filename)
        {
            string userId = UtilBussinessService.detoken(token);
            string SysID = Framework.SystemID;
            string status = "200", msg = "共{0}条信息,{1}条信息导入成功,{2}条信息导入失败！{3}";
            string reason = null;
            int total = 0;
            int success = 0;
            int nosuccess = 0;
            if (filename.EndsWith(".xls") || filename.EndsWith(".xlsx"))
            {
                IWorkbook wk = null;
                bool Isright = true;
                string Sequence = null;
                SYS_DocumentTypeSetting model = null;
                SYS_Parameters ParModel = null;
                using (FileStream fs = File.OpenRead(@filename))   //打开.xls文件
                {
                    if (filename.EndsWith(".xls"))
                        wk = new HSSFWorkbook(fs); //把xls文件中的数据写入wk中
                    else if (filename.EndsWith(".xlsx"))
                        wk = new XSSFWorkbook(fs);//把xlsx文件中的数据写入wk中

                    ISheet sheet = wk.GetSheetAt(0);   //读取第一表数据
                    for (int j = 2; j <= sheet.LastRowNum; j++)  //LastRowNum 是当前表的总行数
                    {
                        IRow row = sheet.GetRow(j);  //读取当前行数据
                        if (row.GetCell(0) == null) //如果当前行的第一列没有数据，停止读取
                            break;
                        if (string.IsNullOrWhiteSpace(row.GetCell(0).ToString()))//如果当前行的第一列没有数据，停止读取，这是判断当row.GetCell不能null然后实际是空字符
                            break;
                        total++;
                        ICell cell = null;
                        Isright = true;
                        if (row != null)
                        {
                            Isright = true;
                            model = new SYS_DocumentTypeSetting();

                            cell = row.GetCell(0);//序号
                            Sequence = cell.ToString();


                            cell = row.GetCell(1);//单据种别代号
                            if (cell != null)
                            {
                                ParModel = SYS_ParameterService.getByCode(cell.ToString(), SysID + "0191213000019");
                                if (ParModel != null)
                                    model.TypeID = ParModel.ParameterID;
                                else
                                {
                                    reason += "序号[" + Sequence + "]:单据种别错误,不存在的单据种别;";
                                    nosuccess++;
                                    continue; //结束本次循环
                                }
                            }
                            else
                            {
                                reason += "序号[" + Sequence + "]:单据种别不能为空;";
                                nosuccess++;
                                continue; //结束本次循环
                            }

                            cell = row.GetCell(3);//代号
                            if (cell != null)
                            {
                                if (cell.ToString().Length <= 4)
                                    model.Code = cell.ToString();
                                else
                                {
                                    reason += "代号[" + model.Code + "]超过了4码;";
                                    nosuccess++;
                                    continue; //结束本次循环
                                }
                            }
                            else
                            {
                                reason += "序号[" + Sequence + "]代号为空;";
                                nosuccess++;
                                continue; //结束本次循环
                            }
                            cell = row.GetCell(4);//说明
                            if (cell != null)
                            {
                                if (cell.ToString().Length <= 120)
                                    model.Name = cell.ToString();
                                else
                                {
                                    reason += "代号[" + model.Name + "]超过了120个字;";
                                    nosuccess++;
                                    continue; //结束本次循环
                                }
                            }


                            cell = row.GetCell(5);//给号方式
                            if (cell != null)
                            {
                                ParModel = SYS_ParameterService.getByName(cell.ToString(), SysID + "0191213000059");
                                if (ParModel != null)
                                    model.GiveWay = ParModel.ParameterID;
                                else
                                {
                                    model.GiveWay = SysID + "0201213000031";
                                    reason += "序号[" + Sequence + "]:给号方式错误,不存在的给号方式;";
                                }
                            }
                            else
                            {
                                reason += "序号[" + Sequence + "]:给号方式不能为空;";
                                nosuccess++;
                                continue; //结束本次循环
                            }

                            cell = row.GetCell(6);//年碼
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                try
                                {
                                    model.YearLength = int.Parse(cell.ToString());
                                }
                                catch
                                {
                                    model.YearLength = 2;
                                    reason += "序号[" + Sequence + "]:年碼格式错误;";
                                }
                            }

                            cell = row.GetCell(7);//月碼
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                try
                                {
                                    model.MonthLength = int.Parse(cell.ToString());
                                }
                                catch
                                {
                                    model.MonthLength = 2;
                                    reason += "序号[" + Sequence + "]:月碼格式错误;";
                                }
                            }

                            cell = row.GetCell(8);//日碼
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                try
                                {
                                    model.DateLength = int.Parse(cell.ToString());
                                }
                                catch
                                {
                                    model.DateLength = 0;
                                    reason += "序号[" + Sequence + "]:日碼格式错误;";
                                }
                            }


                            cell = row.GetCell(9);//流水号长度
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                try
                                {
                                    model.CodeLength = int.Parse(cell.ToString());
                                }
                                catch
                                {
                                    model.CodeLength = 4;
                                    reason += "序号[" + Sequence + "]:流水号长度错误;";
                                }
                            }

                            cell = row.GetCell(11);//年碼格式
                            if (cell != null)
                            {
                                ParModel = SYS_ParameterService.getByName(cell.ToString(), SysID + "019121300005A");
                                if (ParModel != null)
                                    model.YearType = ParModel.ParameterID;
                                else
                                {
                                    model.YearType = SysID + "0201213000033";
                                    reason += "序号[" + Sequence + "]:年碼格式错误,不存在的年碼格式;";
                                }
                            }
                            else
                            {
                                model.YearType = SysID + "0201213000033";
                                reason += "序号[" + Sequence + "]:年碼格式为空;";
                            }


                            cell = row.GetCell(12);//预设
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {

                                if (cell.ToString() == "是")
                                    model.IfDefault = true;
                                else if (cell.ToString() == "否")
                                    model.IfDefault = false;
                                else
                                {
                                    model.IfDefault = false;
                                    reason += "序号[" + Sequence + "]:预设字段错误，只能是“是”或者“否”;";
                                }
                            }
                            else
                            {
                                model.IfDefault = false;
                                reason += "序号[" + Sequence + "]:预设字段为空;";
                            }


                            cell = row.GetCell(13);//状态
                            if (cell != null)
                            {
                                if (cell.ToString() == "正常")
                                    model.Status = SysID + "0201213000001";
                                else if (cell.ToString() == "作废")
                                    model.Status = SysID + "0201213000002";
                                else
                                {
                                    model.Status = SysID + "0201213000001";
                                    reason += "序号[" + Sequence + "]:状态错误,不存在的状态;";
                                }
                            }
                            else
                            {
                                model.Status = SysID + "0201213000001";
                                reason += "序号[" + Sequence + "]:状态为空;";
                            }



                            cell = row.GetCell(14);//属性
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {

                                if (cell.ToString() == "专用")
                                    model.Attribute = true;
                                else if (cell.ToString() == "共用")
                                    model.Attribute = false;
                                else
                                {
                                    model.Attribute = false;
                                    reason += "序号[" + Sequence + "]:属性错误，只能是“专用”或者“共用”;";
                                }
                            }
                            else
                            {
                                model.Attribute = false;
                                reason += "序号[" + Sequence + "]:属性为空;";
                            }


                            cell = row.GetCell(15);//备注
                            if (cell != null)
                            {
                                if (cell.ToString().Length <= 120)
                                    model.Comments = cell.ToString();
                                else
                                {
                                    reason += "序号[" + Sequence + "]:备注超过了120个字;";
                                }
                            }
                        }

                        if (Isright)
                        {
                            if (!SYS_DocumentTypeSettingService.CheckCode(model.Code, null))
                            {
                                model.DTSID = UniversalService.GetSerialNumber("SYS_DocumentTypeSetting");
                                model.Length = model.Code.Length + model.YearLength + model.MonthLength + model.DateLength + model.CodeLength;
                                if (SYS_DocumentTypeSettingService.insert(userId, model))
                                    success++;
                                else
                                {
                                    reason += "序号[" + Sequence + "]:添加失败，未知原因;";
                                    nosuccess++;
                                }
                            }
                            else
                            {
                                reason += "序号[" + Sequence + "]的代号重复;";
                                nosuccess++;
                            }
                        }
                        else
                            nosuccess++;
                    }
                }
            }
            if (nosuccess == total)
                status = "410";

            msg = string.Format(msg, total, success, nosuccess, reason);

            return new { status = status, msg = msg };
        }

        /// <summary>
        /// 抽样检验设定导入
        /// SAM 2017年6月6日14:31:493
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static object Qcs00001Import(string token, string filename)
        {
            string userId = UtilBussinessService.detoken(token);
            string SysID = Framework.SystemID;
            string status = "200", msg = "共{0}条信息,{1}条信息导入成功,{2}条信息导入失败！{3}";
            string reason = null;
            int total = 0;
            int success = 0;
            int nosuccess = 0;
            if (filename.EndsWith(".xls") || filename.EndsWith(".xlsx"))
            {
                IWorkbook wk = null;
                bool Isright = true;
                bool IsDetail = true;
                string Sequence = null;
                QCS_CheckTestSetting model = null;
                QCS_CheckTestSettingDetails Dmodel = null;
                SYS_Parameters ParModel = null;
                QCS_CheckTestSetting Getmodel = null;
                String DSequence = null; //明细的序号
                using (FileStream fs = File.OpenRead(@filename))   //打开.xls文件
                {
                    if (filename.EndsWith(".xls"))
                        wk = new HSSFWorkbook(fs); //把xls文件中的数据写入wk中
                    else if (filename.EndsWith(".xlsx"))
                        wk = new XSSFWorkbook(fs);//把xlsx文件中的数据写入wk中

                    ISheet sheet = wk.GetSheetAt(0);   //读取第一表数据
                    for (int j = 2; j <= sheet.LastRowNum; j++)  //LastRowNum 是当前表的总行数
                    {
                        IRow row = sheet.GetRow(j);  //读取当前行数据
                        if (row.GetCell(0) == null) //如果当前行的第一列没有数据，停止读取
                            break;
                        if (string.IsNullOrWhiteSpace(row.GetCell(0).ToString()))//如果当前行的第一列没有数据，停止读取，这是判断当row.GetCell不能null然后实际是空字符
                            break;
                        total++;
                        ICell cell = null;
                        Isright = true;
                        IsDetail = true;
                        if (row != null)
                        {
                            Isright = true;
                            model = new QCS_CheckTestSetting();
                            Dmodel = new QCS_CheckTestSettingDetails();

                            cell = row.GetCell(0);//序号
                            Sequence = cell.ToString();

                            cell = row.GetCell(1);//检验编号
                            if (cell != null)
                                model.InspectionStandard = cell.ToString();

                            cell = row.GetCell(2);//检验水准
                            if (cell != null)
                            {
                                ParModel = SYS_ParameterService.getByCode(cell.ToString(), SysID + "019121300001A");
                                if (ParModel != null)
                                    model.InspectionLevel = ParModel.ParameterID;
                                else
                                {
                                    reason += "序号[" + Sequence + "]:检验水准错误,不存在的检验水准;";
                                    nosuccess++;
                                    continue; //结束本次循环
                                }
                            }
                            else
                            {
                                reason += "序号[" + Sequence + "]:检验水准不能为空;";
                                nosuccess++;
                                continue; //结束本次循环
                            }

                            cell = row.GetCell(3);//检验方式
                            if (cell != null)
                            {
                                ParModel = SYS_ParameterService.getByCode(cell.ToString(), SysID + "019121300001B");
                                if (ParModel != null)
                                    model.InspectionMethod = ParModel.ParameterID;
                                else
                                {
                                    reason += "序号[" + Sequence + "]:检验方式错误,不存在的检验方式;";
                                    nosuccess++;
                                    continue; //结束本次循环
                                }
                            }
                            else
                            {
                                reason += "序号[" + Sequence + "]:检验方式不能为空;";
                                nosuccess++;
                                continue; //结束本次循环
                            }


                            cell = row.GetCell(4);//AQL
                            if (cell != null)
                            {
                                ParModel = SYS_ParameterService.getByName(cell.ToString(), SysID + "019121300001C");
                                if (ParModel != null)
                                    model.AQL = ParModel.ParameterID;
                                else
                                {
                                    reason += "序号[" + Sequence + "]:AQL错误,不存在的AQL;";
                                    nosuccess++;
                                    continue; //结束本次循环
                                }
                            }
                            else
                            {
                                reason += "序号[" + Sequence + "]:AQL不能为空;";
                                nosuccess++;
                                continue; //结束本次循环
                            }

                            cell = row.GetCell(5);//备注
                            if (cell != null)
                            {
                                if (cell.ToString().Length <= 120)
                                    model.Comments = cell.ToString();
                                else
                                {
                                    reason += "序号[" + Sequence + "]:备注超过了120个字;";
                                }
                            }

                            cell = row.GetCell(6);//状态
                            if (cell != null)
                            {
                                ParModel = SYS_ParameterService.getByCode(cell.ToString(), SysID + "0191213000001");
                                if (ParModel != null)
                                    model.Status = ParModel.ParameterID;
                                else
                                {
                                    model.Status = SysID + "0201213000001";
                                    reason += "序号[" + Sequence + "]:状态错误,不存在的状态;";
                                }
                            }
                            else
                            {
                                model.Status = SysID + "0201213000001";
                                reason += "序号[" + Sequence + "]:状态为空;";
                            }


                            cell = row.GetCell(7);//序号
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                DSequence = cell.ToString();
                            }
                            else
                            {
                                IsDetail = false;
                            }

                            cell = row.GetCell(8);//起始批号
                            if (cell != null)
                            {
                                try
                                {
                                    Dmodel.StartBatch = decimal.Parse(cell.ToString());
                                }
                                catch
                                {
                                    Dmodel.StartBatch = 0;
                                    reason += "序号[" + Sequence + "]:起始批号格式错误;";
                                }
                            }
                            else
                                Dmodel.StartBatch = 0;

                            cell = row.GetCell(9);//结束批号
                            if (cell != null)
                            {
                                try
                                {
                                    Dmodel.EndBatch = decimal.Parse(cell.ToString());
                                }
                                catch
                                {
                                    Dmodel.EndBatch = 0;
                                    reason += "序号[" + Sequence + "]:结束批号格式错误;";
                                }
                            }
                            else
                                Dmodel.EndBatch = 0;



                            cell = row.GetCell(10);//抽样数
                            if (cell != null)
                            {
                                try
                                {
                                    Dmodel.SamplingQuantity = decimal.Parse(cell.ToString());
                                }
                                catch
                                {
                                    Dmodel.SamplingQuantity = 0;
                                    reason += "序号[" + Sequence + "]:抽样数格式错误;";
                                }
                            }
                            else
                                Dmodel.SamplingQuantity = 0;


                            cell = row.GetCell(11);//Ac数量
                            if (cell != null)
                            {
                                try
                                {
                                    Dmodel.AcQuantity = decimal.Parse(cell.ToString());
                                }
                                catch
                                {
                                    Dmodel.AcQuantity = 0;
                                    reason += "序号[" + Sequence + "]:Ac数量格式错误;";
                                }
                            }
                            else
                                Dmodel.AcQuantity = 0;

                            cell = row.GetCell(12);//Re数量
                            if (cell != null)
                            {
                                try
                                {
                                    Dmodel.ReQuantity = decimal.Parse(cell.ToString());
                                }
                                catch
                                {
                                    Dmodel.ReQuantity = 0;
                                    reason += "序号[" + Sequence + "]:Re数量格式错误;";
                                }
                            }
                            else
                                Dmodel.ReQuantity = 0;
                        }

                        if (Isright)
                        {
                            Getmodel = QCS_CheckTestSettingService.get(model.InspectionLevel, model.InspectionMethod, model.AQL);
                            if (Getmodel == null)
                            {
                                if (!QCS_CheckTestSettingService.Check(model.InspectionStandard, model.InspectionLevel, model.InspectionMethod, model.AQL, null))
                                {
                                    model.CheckTestSettingID = UniversalService.GetSerialNumber("QCS_CheckTestSetting");
                                    if (QCS_CheckTestSettingService.insert(userId, model))
                                    {
                                        if (IsDetail)
                                        {
                                            Dmodel.CTSDID = UniversalService.GetSerialNumber("QCS_CheckTestSettingDetails");
                                            Dmodel.CheckTestSettingID = model.CheckTestSettingID;
                                            Dmodel.Status = Framework.SystemID + "0201213000001";
                                            try
                                            {
                                                Dmodel.Sequence = int.Parse(DSequence);
                                                if (Dmodel.Sequence <= 0)
                                                {
                                                    Dmodel.Sequence = 1;
                                                    reason += "序号[" + Sequence + "]:序号格式错误;";
                                                }
                                            }
                                            catch
                                            {
                                                Dmodel.Sequence = 1;
                                                reason += "序号[" + Sequence + "]:序号格式错误;";
                                            }
                                            QCS_CheckTestSettingDetailsService.insert(userId, Dmodel);
                                        }
                                        success++;
                                    }
                                    else
                                    {
                                        reason += "序号[" + Sequence + "]:添加失败，未知原因;";
                                        nosuccess++;
                                    }
                                }
                                else
                                {
                                    reason += "序号[" + Sequence + "]:资料重复;";
                                    nosuccess++;
                                }
                            }
                            else
                            {
                                if (IsDetail)
                                {
                                    Dmodel.CTSDID = UniversalService.GetSerialNumber("QCS_CheckTestSettingDetails");
                                    Dmodel.CheckTestSettingID = Getmodel.CheckTestSettingID;
                                    Dmodel.Status = Framework.SystemID + "0201213000001";
                                    try
                                    {
                                        Dmodel.Sequence = int.Parse(DSequence);
                                        if (Dmodel.Sequence <= 0)
                                        {
                                            if (QCS_CheckTestSettingDetailsService.getSequence(Getmodel.CheckTestSettingID) == null)
                                                Dmodel.Sequence = 1;
                                            else
                                                Dmodel.Sequence = QCS_CheckTestSettingDetailsService.getSequence(Getmodel.CheckTestSettingID).Sequence + 1;
                                            reason += "序号[" + Sequence + "]:序号格式错误;";
                                        }
                                    }
                                    catch
                                    {
                                        if (QCS_CheckTestSettingDetailsService.getSequence(Getmodel.CheckTestSettingID) == null)
                                            Dmodel.Sequence = 1;
                                        else
                                            Dmodel.Sequence = QCS_CheckTestSettingDetailsService.getSequence(Getmodel.CheckTestSettingID).Sequence + 1;
                                        reason += "序号[" + Sequence + "]:序号格式错误;";
                                    }
                                    if (!QCS_CheckTestSettingDetailsService.CheckSequence(Dmodel.Sequence.ToString(), Getmodel.CheckTestSettingID, null))
                                    {
                                        if (QCS_CheckTestSettingDetailsService.insert(userId, Dmodel))
                                            success++;
                                        else
                                        {
                                            reason += "序号[" + Sequence + "]:添加失败，未知原因;";
                                            nosuccess++;
                                        }
                                    }
                                    else
                                    {
                                        reason += "序号[" + Sequence + "]:序号重复;";
                                        nosuccess++;
                                    }
                                }
                            }
                        }
                        else
                            nosuccess++;
                    }
                }
            }
            if (nosuccess == total)
                status = "410";

            msg = string.Format(msg, total, success, nosuccess, reason);

            return new { status = status, msg = msg };
        }

        /// <summary>
        /// 检验类别的导入
        /// SAM 2017年6月9日11:38:25
        /// </summary>
        /// <param name="token"></param>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static object Qcs00002TypeImport(string token, string filename)
        {
            string userId = UtilBussinessService.detoken(token);
            string SysID = Framework.SystemID;
            string status = "200", msg = "共{0}条信息,{1}条信息导入成功,{2}条信息导入失败！{3}";
            string reason = null;
            string Type = Framework.SystemID + "019121300001E";
            int total = 0;
            int success = 0;
            int nosuccess = 0;
            if (filename.EndsWith(".xls") || filename.EndsWith(".xlsx"))
            {
                IWorkbook wk = null;
                bool Isright = true;
                string Sequence = null;
                SYS_Parameters model = null;
                QCS_SamplingSetting SModel = null;
                using (FileStream fs = File.OpenRead(@filename))   //打开.xls文件
                {
                    if (filename.EndsWith(".xls"))
                        wk = new HSSFWorkbook(fs); //把xls文件中的数据写入wk中
                    else if (filename.EndsWith(".xlsx"))
                        wk = new XSSFWorkbook(fs);//把xlsx文件中的数据写入wk中

                    ISheet sheet = wk.GetSheetAt(0);   //读取第一表数据
                    for (int j = 2; j <= sheet.LastRowNum; j++)  //LastRowNum 是当前表的总行数
                    {
                        IRow row = sheet.GetRow(j);  //读取当前行数据
                        if (row.GetCell(0) == null) //如果当前行的第一列没有数据，停止读取
                            break;
                        if (string.IsNullOrWhiteSpace(row.GetCell(0).ToString()))//如果当前行的第一列没有数据，停止读取，这是判断当row.GetCell不能null然后实际是空字符
                            break;
                        total++;
                        ICell cell = null;
                        Isright = true;
                        if (row != null)
                        {
                            Isright = true;
                            model = new SYS_Parameters();

                            cell = row.GetCell(0);//序号
                            Sequence = cell.ToString();
                            cell = row.GetCell(1);//代号
                            if (cell != null)
                            {
                                if (cell.ToString().Length <= 30)
                                    model.Code = cell.ToString();
                                else
                                {
                                    reason += "序号[" + Sequence + "]:代号超过30个字;";
                                    nosuccess++;
                                    continue; //结束本次循环
                                }
                            }
                            else
                            {
                                reason += "序号[" + Sequence + "]代号为空;";
                                nosuccess++;
                                continue; //结束本次循环
                            }
                            cell = row.GetCell(2);//说明
                            if (cell != null)
                            {
                                if (cell.ToString().Length <= 120)
                                    model.Name = cell.ToString();
                                else
                                {
                                    reason += "序号[" + Sequence + "]:说明超过120个字;";
                                    nosuccess++;
                                    continue; //结束本次循环
                                }
                            }
                            else
                            {
                                reason += "序号[" + Sequence + "]说明为空;";
                                nosuccess++;
                                continue; //结束本次循环
                            }
                            cell = row.GetCell(3);//备注
                            if (cell != null)
                            {
                                if (cell.ToString().Length <= 120)
                                    model.Comments = cell.ToString();
                                else
                                {
                                    reason += "序号[" + Sequence + "]:备注超过120个字;";
                                    nosuccess++;
                                    continue; //结束本次循环
                                }
                            }
                            else
                            {
                                reason += "序号[" + Sequence + "]备注为空;";
                                nosuccess++;
                                continue; //结束本次循环
                            }

                        }
                        if (Isright)
                        {

                            if (!SYS_ParameterService.CheckParameter(model.Code, null, null, Type, null))
                            {
                                model.ParameterID = UniversalService.GetSerialNumber("SYS_Parameters");
                                model.ParameterTypeID = Type;
                                model.Sequence = 0;
                                model.UsingType = 0;
                                model.IsEnable = 1;
                                if (SYS_ParameterService.insert(userId, model))
                                {
                                    DateTime Now = DateTime.Now;
                                    DateTime UTCNow = DateTime.UtcNow;
                                    IList<Hashtable> MethodList = SYS_ParameterService.GetLists(Framework.SystemID + "019121300001B");
                                    IList<Hashtable> DisadvantagesList = SYS_ParameterService.GetLists(Framework.SystemID + "019121300001D");
                                    foreach (Hashtable MItem in MethodList)
                                    {
                                        foreach (Hashtable DItem in DisadvantagesList)
                                        {
                                            SModel = new QCS_SamplingSetting();
                                            SModel.SamplingSettingID = UniversalService.GetSerialNumber("QCS_SamplingSetting");
                                            SModel.CategoryID = model.ParameterID;
                                            SModel.InspectionMethod = MItem["value"].ToString();
                                            SModel.Disadvantages = DItem["value"].ToString();
                                            SModel.Status = Framework.SystemID + "0201213000001";
                                            SModel.CreateTime = UTCNow;
                                            SModel.CreateLocalTime = Now;
                                            QCS_SamplingSettingService.insert(userId, SModel);
                                        }
                                    }
                                    success++;
                                }
                                else
                                {
                                    reason += "序号[" + Sequence + "]:添加失败，未知原因;";
                                    nosuccess++;
                                }
                            }
                            else
                            {
                                reason += "序号[" + Sequence + "]的代号重复;";
                                nosuccess++;
                            }
                        }
                        else
                            nosuccess++;
                    }

                }
            }
            if (nosuccess == total)
                status = "410";

            msg = string.Format(msg, total, success, nosuccess, reason);

            return new { status = status, msg = msg };
        }

        /// <summary>
        /// 检验项目的导入
        /// SAM 2017年6月11日13:17:27
        /// </summary>
        /// <param name="token"></param>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static object Qcs00002ProjectImport(string token, string filename)
        {
            string userId = UtilBussinessService.detoken(token);
            string SysID = Framework.SystemID;
            string status = "200", msg = "共{0}条信息,{1}条信息导入成功,{2}条信息导入失败！{3}";
            string reason = null;
            int total = 0;
            int success = 0;
            int nosuccess = 0;
            if (filename.EndsWith(".xls") || filename.EndsWith(".xlsx"))
            {
                IWorkbook wk = null;
                bool Isright = true;
                string Sequence = null;
                QCS_InspectionProject model = null;
                SYS_Parameters ParModel = null;
                using (FileStream fs = File.OpenRead(@filename))   //打开.xls文件
                {
                    if (filename.EndsWith(".xls"))
                        wk = new HSSFWorkbook(fs); //把xls文件中的数据写入wk中
                    else if (filename.EndsWith(".xlsx"))
                        wk = new XSSFWorkbook(fs);//把xlsx文件中的数据写入wk中

                    ISheet sheet = wk.GetSheetAt(0);   //读取第一表数据
                    for (int j = 2; j <= sheet.LastRowNum; j++)  //LastRowNum 是当前表的总行数
                    {
                        IRow row = sheet.GetRow(j);  //读取当前行数据
                        if (row.GetCell(0) == null) //如果当前行的第一列没有数据，停止读取
                            break;
                        if (string.IsNullOrWhiteSpace(row.GetCell(0).ToString()))//如果当前行的第一列没有数据，停止读取，这是判断当row.GetCell不能null然后实际是空字符
                            break;
                        total++;
                        ICell cell = null;
                        Isright = true;
                        if (row != null)
                        {
                            Isright = true;
                            model = new QCS_InspectionProject();

                            cell = row.GetCell(0);//序号
                            Sequence = cell.ToString();

                            cell = row.GetCell(1);//代号
                            if (cell != null)
                            {
                                if (cell.ToString().Length <= 30)
                                    model.Code = cell.ToString();
                                else
                                {
                                    reason += "序号[" + Sequence + "]:代号超过30个字;";
                                    nosuccess++;
                                    continue; //结束本次循环
                                }
                            }
                            else
                            {
                                reason += "序号[" + Sequence + "]代号为空;";
                                nosuccess++;
                                continue; //结束本次循环
                            }
                            cell = row.GetCell(2);//说明
                            if (cell != null)
                            {
                                if (cell.ToString().Length <= 120)
                                    model.Name = cell.ToString();
                                else
                                {
                                    reason += "序号[" + Sequence + "]:说明超过120个字;";
                                    nosuccess++;
                                    continue; //结束本次循环
                                }
                            }
                            else
                            {
                                reason += "序号[" + Sequence + "]说明为空;";
                                nosuccess++;
                                continue; //结束本次循环
                            }

                            cell = row.GetCell(3);//实测值设定
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                ParModel = SYS_ParameterService.getByCode(cell.ToString(), SysID + "0191213000052");
                                if (ParModel != null)
                                    model.Attribute = ParModel.ParameterID;
                                else
                                {
                                    model.Attribute = SysID + "0201213000019";
                                    reason += "序号[" + Sequence + "]:实测值设定错误,不存在的实测值设定;";
                                }
                            }
                            else
                            {
                                model.Attribute = SysID + "0201213000019";
                                reason += "序号[" + Sequence + "]:实测值设定为空;";
                            }


                            cell = row.GetCell(4);//检验标准
                            if (cell != null)
                            {
                                if (cell.ToString().Length <= 120)
                                    model.InspectionStandard = cell.ToString();
                                else
                                {
                                    reason += "序号[" + Sequence + "]:检验标准超过120个字;";
                                }
                            }

                            cell = row.GetCell(5);//检验水准
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                ParModel = SYS_ParameterService.getByCode(cell.ToString(), SysID + "019121300001A");
                                if (ParModel != null)
                                    model.InspectionLevel = ParModel.ParameterID;
                                else
                                {
                                    model.InspectionLevel = SysID + "020121300004C";
                                    reason += "序号[" + Sequence + "]:检验水准错误,不存在的检验水准;";
                                }
                            }
                            else
                            {
                                model.InspectionLevel = SysID + "020121300004C";
                                reason += "序号[" + Sequence + "]:检验水准为空;";
                            }

                            cell = row.GetCell(6);//缺点等级
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                ParModel = SYS_ParameterService.getByCode(cell.ToString(), SysID + "019121300001D");
                                if (ParModel != null)
                                    model.Disadvantages = ParModel.ParameterID;
                                else
                                {
                                    model.Disadvantages = SysID + "0201213000077";
                                    reason += "序号[" + Sequence + "]:缺点等级错误,不存在的缺点等级;";
                                }
                            }
                            else
                            {
                                model.Disadvantages = SysID + "0201213000077";
                                reason += "序号[" + Sequence + "]:缺点等级为空;";
                            }


                            cell = row.GetCell(7);//备注
                            if (cell != null)
                            {
                                if (cell.ToString().Length <= 120)
                                    model.Comments = cell.ToString();
                                else
                                    reason += "序号[" + Sequence + "]:备注超过120个字;";
                            }

                            cell = row.GetCell(8);//状态
                            if (cell != null)
                            {
                                if (cell.ToString() == "1")
                                    model.Status = SysID + "0201213000001";
                                else if (cell.ToString() == "0")
                                    model.Status = SysID + "0201213000002";
                                else
                                {
                                    model.Status = SysID + "0201213000001";
                                    reason += "序号[" + Sequence + "]:状态错误,不存在的状态;";
                                }
                            }
                            else
                            {
                                model.Status = SysID + "0201213000001";
                                reason += "序号[" + Sequence + "]:状态为空;";
                            }
                        }
                        if (Isright)
                        {

                            if (!QCS_InspectionProjectService.Check(model.Code, null))
                            {
                                model.InspectionProjectID = UniversalService.GetSerialNumber("QCS_InspectionProject");
                                if (QCS_InspectionProjectService.insert(userId, model))
                                    success++;
                                else
                                {
                                    reason += "序号[" + Sequence + "]:添加失败，未知原因;";
                                    nosuccess++;
                                }
                            }
                            else
                            {
                                reason += "序号[" + Sequence + "]:代号重复;";
                                nosuccess++;
                            }
                        }
                        else
                            nosuccess++;
                    }

                }
            }
            if (nosuccess == total)
                status = "410";

            msg = string.Format(msg, total, success, nosuccess, reason);

            return new { status = status, msg = msg };
        }

        /// <summary>
        /// 客诉单明细的导入
        /// SAM 2017年6月15日09:55:34
        /// </summary>
        /// <param name="token"></param>
        /// <param name="filename"></param>
        /// <param name="ComplaintID"></param>
        /// <returns></returns>
        public static object Qcs00009Import(string token, string filename, string ComplaintID)
        {
            string userId = UtilBussinessService.detoken(token);
            string SysID = Framework.SystemID;
            string status = "200", msg = "共{0}条信息,{1}条信息导入成功,{2}条信息导入失败！{3}";
            string reason = null;
            int total = 0;
            int success = 0;
            int nosuccess = 0;
            if (filename.EndsWith(".xls") || filename.EndsWith(".xlsx"))
            {
                IWorkbook wk = null;
                string Sequence = null;
                QCS_ComplaintDetails model = null;
                SYS_Items item = null;

                QCS_Complaint Headermodel = QCS_ComplaintService.get(ComplaintID);
                if (Headermodel == null)
                    return new { status = "440", msg = "表头流水号错误，不存在的流水号！" };

                using (FileStream fs = File.OpenRead(@filename))   //打开.xls文件
                {
                    if (filename.EndsWith(".xls"))
                        wk = new HSSFWorkbook(fs); //把xls文件中的数据写入wk中
                    else if (filename.EndsWith(".xlsx"))
                        wk = new XSSFWorkbook(fs);//把xlsx文件中的数据写入wk中

                    ISheet sheet = wk.GetSheetAt(0);   //读取第一表数据
                    for (int j = 2; j <= sheet.LastRowNum; j++)  //LastRowNum 是当前表的总行数
                    {
                        IRow row = sheet.GetRow(j);  //读取当前行数据
                        if (row.GetCell(0) == null) //如果当前行的第一列没有数据，停止读取
                            break;
                        if (string.IsNullOrWhiteSpace(row.GetCell(0).ToString()))//如果当前行的第一列没有数据，停止读取，这是判断当row.GetCell不能null然后实际是空字符
                            break;
                        total++;
                        ICell cell = null;
                        if (row != null)
                        {
                            model = new QCS_ComplaintDetails();
                            cell = row.GetCell(0);//序号
                            Sequence = cell.ToString();

                            cell = row.GetCell(1);//料品代号
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                item = SYS_ItemsService.getByCode(cell.ToString());
                                if (item != null)
                                {
                                    model.ItemID = item.ItemID;
                                }
                                else
                                {
                                    reason += "序号[" + Sequence + "]:料品代号错误,不存在的料品代号;";
                                    nosuccess++;
                                    continue; //结束本次循环
                                }
                            }
                            else
                            {
                                reason += "序号[" + Sequence + "]:料品代号为空;";
                                nosuccess++;
                                continue; //结束本次循环
                            }

                            cell = row.GetCell(2);//批号
                            if (cell != null)
                            {
                                if (cell.ToString().Length <= 30)
                                    model.BatchNumber = cell.ToString();
                                else
                                {
                                    reason += "序号[" + Sequence + "]:批号超过30个字;";
                                }
                            }


                            cell = row.GetCell(3);//出货单号
                            if (cell != null)
                            {
                                if (cell.ToString().Length <= 30)
                                    model.ShipperNo = cell.ToString();
                                else
                                {
                                    reason += "序号[" + Sequence + "]:出货单号超过30个字;";
                                }
                            }


                            cell = row.GetCell(4);//订单号码
                            if (cell != null)
                            {
                                if (cell.ToString().Length <= 30)
                                    model.OrderNo = cell.ToString();
                                else
                                {
                                    reason += "序号[" + Sequence + "]:出货单号超过30个字;";
                                }
                            }

                            cell = row.GetCell(5);//客诉数量
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                try
                                {
                                    model.Quantity = decimal.Parse(cell.ToString());
                                }
                                catch
                                {
                                    model.Quantity = 0;
                                    reason += "序号[" + Sequence + "]:客诉数量格式错误;";
                                }

                            }
                            else
                                model.Quantity = 0;

                            cell = row.GetCell(6);//客诉说明
                            if (cell != null)
                            {
                                if (cell.ToString().Length <= 255)
                                    model.Description = cell.ToString();
                                else
                                {
                                    reason += "序号[" + Sequence + "]:客诉说明超过255个字;";
                                }
                            }
                            //测试报告要求去掉
                            //cell = row.GetCell(7);//状态
                            //if (cell != null)
                            //{
                            //    if (cell.ToString() == "立單")
                            //        model.Status = SysID + "0201213000028";
                            //    else if (cell.ToString() == "核准")
                            //        model.Status = SysID + "0201213000029";
                            //    else if (cell.ToString() == "結案")
                            //        model.Status = SysID + "020121300002A";
                            //    else if (cell.ToString() == "註銷")
                            //        model.Status = SysID + "020121300002B";
                            //    else
                            //    {
                            //        model.Status = SysID + "0201213000028";
                            //        reason += "序号[" + Sequence + "]:状态错误,不存在的状态;";
                            //    }
                            //}
                            //else
                            //{
                            //    model.Status = SysID + "0201213000028";
                            //    reason += "序号[" + Sequence + "]:状态为空;";
                            //}
                        }
                        model.ComplaintDetailID = UniversalService.GetSerialNumber("QCS_ComplaintDetails");
                        model.ComplaintID = ComplaintID;
                        model.Sequence = int.Parse(Sequence);
                        model.Status = Headermodel.Status;
                        if (QCS_ComplaintDetailsService.insert(userId, model))
                            success++;
                        else
                        {
                            reason += "序号[" + Sequence + "]:添加失败，未知原因;";
                            nosuccess++;
                        }
                    }

                }
            }
            if (nosuccess == total)
                status = "410";

            msg = string.Format(msg, total, success, nosuccess, reason);

            return new { status = status, msg = msg };
        }

        /// <summary>
        /// 客诉原因的导入
        /// SAM 2017年9月5日14:47:293
        /// </summary>
        /// <param name="token"></param>
        /// <param name="filename"></param>
        /// <param name="ComplaintDetailID"></param>
        /// <returns></returns>
        public static object Qcs00010ReasonImport(string token, string filename, string ComplaintDetailID)
        {
            string userId = UtilBussinessService.detoken(token);
            string SysID = Framework.SystemID;
            string status = "200", msg = "共{0}条信息,{1}条信息导入成功,{2}条信息导入失败！{3}";
            string reason = null;
            int total = 0;
            int success = 0;
            int nosuccess = 0;
            if (filename.EndsWith(".xls") || filename.EndsWith(".xlsx"))
            {
                IWorkbook wk = null;
                bool Isright = true;
                string Sequence = null;
                QCS_ComplaintReason model = null;
                QCS_Complaint Headmodel = null;
                SYS_Parameters ParModel = null;
                QCS_ComplaintDetails Detailmodel = QCS_ComplaintDetailsService.get(ComplaintDetailID);
                if (Detailmodel == null)
                    return new { status = "440", msg = "明细流水号错误，不存在的流水号！" };

                using (FileStream fs = File.OpenRead(@filename))   //打开.xls文件
                {
                    if (filename.EndsWith(".xls"))
                        wk = new HSSFWorkbook(fs); //把xls文件中的数据写入wk中
                    else if (filename.EndsWith(".xlsx"))
                        wk = new XSSFWorkbook(fs);//把xlsx文件中的数据写入wk中

                    ISheet sheet = wk.GetSheetAt(0);   //读取第一表数据
                    for (int j = 2; j <= sheet.LastRowNum; j++)  //LastRowNum 是当前表的总行数
                    {
                        IRow row = sheet.GetRow(j);  //读取当前行数据
                        if (row.GetCell(0) == null) //如果当前行的第一列没有数据，停止读取
                            break;
                        if (string.IsNullOrWhiteSpace(row.GetCell(0).ToString()))//如果当前行的第一列没有数据，停止读取，这是判断当row.GetCell不能null然后实际是空字符
                            break;
                        total++;
                        ICell cell = null;
                        Isright = true;
                        if (row != null)
                        {
                            Isright = true;
                            model = new QCS_ComplaintReason();
                            cell = row.GetCell(0);//序号
                            Sequence = cell.ToString();

                            cell = row.GetCell(1);//客诉单号
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                Headmodel = QCS_ComplaintService.getByCode(cell.ToString());
                                if (Headmodel == null)
                                {
                                    reason += "序号[" + Sequence + "]:客诉单号错误,不存在的客诉单号;";
                                    nosuccess++;
                                    continue; //结束本次循环
                                }
                                else if (Headmodel.Status != SysID + "0201213000029")
                                {
                                    reason += "序号[" + Sequence + "]:客诉单号处于非OP状态，不能导入数据;";
                                    nosuccess++;
                                    continue; //结束本次循环
                                }
                                else
                                {
                                    model.ComplaintID = Headmodel.ComplaintID;
                                }
                            }
                            else
                            {
                                reason += "序号[" + Sequence + "]:客诉单号为空;";
                                nosuccess++;
                                continue; //结束本次循环
                            }

                            cell = row.GetCell(2);//原因群码
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                ParModel = SYS_ParameterService.getByCode(cell.ToString(), SysID + "0191213000011");
                                if (ParModel != null)
                                    model.ReasonGroupID = ParModel.ParameterID;
                                else
                                    reason += "序号[" + Sequence + "]:原因群码错误,不存在的原因群码;";
                            }

                            cell = row.GetCell(3);//不良原因
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                ParModel = SYS_ParameterService.getByCode(cell.ToString(), SysID + "0191213000012");
                                if (ParModel != null)
                                    model.ReasonID = ParModel.ParameterID;
                                else
                                {
                                    reason += "序号[" + Sequence + "]:不良原因错误,不存在的不良原因;";
                                    nosuccess++;
                                    continue; //结束本次循环
                                }
                            }
                            else
                            {
                                reason += "序号[" + Sequence + "]:不良原因不能为空;";
                                nosuccess++;
                                continue; //结束本次循环
                            }

                            cell = row.GetCell(5);//客诉数量
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                try
                                {
                                    model.Quantity = decimal.Parse(cell.ToString());
                                }
                                catch
                                {
                                    model.Quantity = 0;
                                    reason += "序号[" + Sequence + "]:客诉数量格式错误;";
                                }

                            }
                            else
                                model.Quantity = 0;
                        }
                        if (Isright)
                        {
                            model.ComplaintReasonID = UniversalService.GetSerialNumber("QCS_ComplaintReason");
                            model.ComplaintDetailID = ComplaintDetailID;
                            model.Sequence = int.Parse(Sequence);
                            model.Status = SysID + "0201213000001";
                            if (QCS_ComplaintReasonService.insert(userId, model))
                                success++;
                            else
                            {
                                reason += "序号[" + Sequence + "]:添加失败，未知原因;";
                                nosuccess++;
                            }
                        }
                        else
                            nosuccess++;
                    }

                }
            }
            if (nosuccess == total)
                status = "410";

            msg = string.Format(msg, total, success, nosuccess, reason);

            return new { status = status, msg = msg };
        }

        /// <summary>
        /// 处理对策上传保存
        /// SAM 2017年6月22日14:23:15
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="ComplaintDetailID"></param>
        /// <param name="httpPostedFile"></param>
        /// <returns></returns>
        public static object Qcs00010FileAdd(string Token, string ComplaintDetailID, HttpPostedFile httpPostedFile)
        {
            try
            {
                QCS_ComplaintDetails DeMoel = QCS_ComplaintDetailsService.get(ComplaintDetailID);
                if (DeMoel == null)
                    return new { status = "410", msg = "上传失败！客诉单明细流水号错误！" };

                string userId = UtilBussinessService.detoken(Token);
                string SysID = Framework.SystemID;
                string filePath = "Upload/Qcs00010Detail/" + ComplaintDetailID + "/";
                string strPath = HttpContext.Current.Server.MapPath("~/");
                //假如文件夹不存在则新建
                if (!Directory.Exists(strPath + filePath))
                {
                    Directory.CreateDirectory(strPath + filePath);
                }
                SYS_Attachments model = new SYS_Attachments();
                model.AttachmentID = UniversalService.GetSerialNumber("SYS_Attachments");
                model.ObjectID = ComplaintDetailID;
                model.Path = filePath + httpPostedFile.FileName;
                model.OriginalName = httpPostedFile.FileName;
                httpPostedFile.SaveAs(Path.Combine(strPath + model.Path)); //上传图片
                model.Name = httpPostedFile.FileName;
                model.Default = true;
                model.Type = SysID + "0201213000083";
                model.UploadTime = DateTime.Now;
                model.Status = Framework.SystemID + "0201213000001";
                if (SYS_AttachmentsService.insert(userId, model))
                    return new { status = "200", msg = "上传成功！" };
                else
                    return new { status = "410", msg = "上传失败！" };

            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return new { status = "410", msg = "上传失败！" + ex.ToString() };
            }
        }

        /// <summary>
        /// 客诉单附件—上傳保存
        /// SAM 2017年6月21日17:23:01
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="ComplaintDetailID"></param>
        /// <param name="httpPostedFile"></param>
        /// <returns></returns>
        public static object Qcs00009FileAdd(string Token, string ComplaintDetailID, HttpPostedFile httpPostedFile)
        {
            try
            {
                QCS_ComplaintDetails DeMoel = QCS_ComplaintDetailsService.get(ComplaintDetailID);
                if (DeMoel == null)
                    return new { status = "410", msg = "上传失败！客诉单明细流水号错误！" };

                string userId = UtilBussinessService.detoken(Token);
                string SysID = Framework.SystemID;
                string filePath = "Upload/Qcs00009Detail/" + ComplaintDetailID + "/";
                string strPath = HttpContext.Current.Server.MapPath("~/");
                //假如文件夹不存在则新建
                if (!Directory.Exists(strPath + filePath))
                {
                    Directory.CreateDirectory(strPath + filePath);
                }
                SYS_Attachments model = new SYS_Attachments();
                model.AttachmentID = UniversalService.GetSerialNumber("SYS_Attachments");
                model.ObjectID = ComplaintDetailID;
                model.Path = filePath + httpPostedFile.FileName;
                model.OriginalName = httpPostedFile.FileName;
                httpPostedFile.SaveAs(Path.Combine(strPath + model.Path)); //上传图片
                model.Name = httpPostedFile.FileName;
                model.Default = true;
                model.Type = SysID + "0201213000082";
                model.UploadTime = DateTime.Now;
                model.Status = Framework.SystemID + "0201213000001";
                if (SYS_AttachmentsService.insert(userId, model))
                    return new { status = "200", msg = "上传成功！" };
                else
                    return new { status = "410", msg = "上传失败！" };

            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return new { status = "410", msg = "上传失败！" + ex.ToString() };
            }
        }

        /// <summary>
        /// 保养项目的导入
        /// SAM 2017年7月5日15:58:27
        /// </summary>
        /// <param name="token"></param>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static object Ems00008ProjectImport(string token, string filename)
        {
            string userId = UtilBussinessService.detoken(token);
            string SysID = Framework.SystemID;
            string status = "200", msg = "共{0}条信息,{1}条信息导入成功,{2}条信息导入失败！{3}";
            string reason = null;
            int total = 0;
            int success = 0;
            int nosuccess = 0;
            string Type = SysID + "0191213000022";
            if (filename.EndsWith(".xls") || filename.EndsWith(".xlsx"))
            {
                IWorkbook wk = null;
                bool Isright = true;
                string Sequence = null;
                SYS_Parameters model = null;
                SYS_Parameters Attributemodel = null;
                using (FileStream fs = File.OpenRead(@filename))   //打开.xls文件
                {
                    if (filename.EndsWith(".xls"))
                        wk = new HSSFWorkbook(fs); //把xls文件中的数据写入wk中
                    else if (filename.EndsWith(".xlsx"))
                        wk = new XSSFWorkbook(fs);//把xlsx文件中的数据写入wk中

                    ISheet sheet = wk.GetSheetAt(0);   //读取第一表数据
                    for (int j = 2; j <= sheet.LastRowNum; j++)  //LastRowNum 是当前表的总行数
                    {
                        IRow row = sheet.GetRow(j);  //读取当前行数据
                        if (row.GetCell(0) == null) //如果当前行的第一列没有数据，停止读取
                            break;
                        if (string.IsNullOrWhiteSpace(row.GetCell(0).ToString()))//如果当前行的第一列没有数据，停止读取，这是判断当row.GetCell不能null然后实际是空字符
                            break;
                        total++;
                        ICell cell = null;
                        Isright = true;
                        if (row != null)
                        {
                            Isright = true;
                            model = new SYS_Parameters();

                            cell = row.GetCell(0);//序号
                            Sequence = cell.ToString();
                            cell = row.GetCell(1);//代号
                            if (cell != null)
                            {
                                if (cell.ToString().Length <= 20)
                                {
                                    model.Code = cell.ToString();
                                }
                                else
                                {
                                    reason += "序号[" + Sequence + "]:代号超过20个字;";
                                    nosuccess++;
                                    continue; //结束本次循环
                                }
                            }
                            else
                            {
                                reason += "序号[" + Sequence + "]代号为空;";
                                nosuccess++;
                                continue; //结束本次循环
                            }
                            cell = row.GetCell(2);//说明
                            if (cell != null)
                            {
                                if (cell.ToString().Length <= 60)
                                {
                                    model.Name = cell.ToString();
                                }
                                else
                                {
                                    reason += "序号[" + Sequence + "]:说明超过60个字;";
                                    nosuccess++;
                                    continue; //结束本次循环
                                }
                            }
                            else
                            {
                                reason += "序号[" + Sequence + "]说明为空;";
                                nosuccess++;
                                continue; //结束本次循环
                            }

                            cell = row.GetCell(3);//说明
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                Attributemodel = SYS_ParameterService.getByCode(cell.ToString(), SysID + "0191213000062");
                                if (Attributemodel == null)
                                {
                                    reason += "序号[" + Sequence + "]:属性错误，不存在的属性;";
                                    model.Description = SysID + "0201213000094";
                                }
                                else
                                    model.Description = Attributemodel.ParameterID;

                            }
                            else
                            {
                                model.Description = SysID + "0201213000094";
                                reason += "序号[" + Sequence + "]:属性为空;";
                            }


                            cell = row.GetCell(4);//备注
                            if (cell != null)
                            {
                                if (cell.ToString().Length <= 120)
                                {
                                    model.Comments = cell.ToString();
                                }
                                else
                                {
                                    reason += "序号[" + Sequence + "]:备注超过120个字;";
                                }
                            }
                            cell = row.GetCell(5);//状态
                            if (cell != null)
                            {
                                if (cell.ToString() == "1")
                                    model.IsEnable = 1;
                                else if (cell.ToString() == "0")
                                    model.IsEnable = 0;
                                else
                                {
                                    model.IsEnable = 1;
                                    reason += "序号[" + Sequence + "]的状态错误,不存在的状态;";
                                }
                            }
                            else
                            {
                                model.IsEnable = 1;
                                reason += "序号[" + Sequence + "]的状态为空;";
                            }
                        }
                        if (Isright)
                        {
                            if (!SYS_ParameterService.CheckParameter(model.Code, null, null, Type, null))
                            {
                                model.ParameterID = UniversalService.GetSerialNumber("SYS_Parameters");
                                model.ParameterTypeID = Type;
                                model.Sequence = 0;
                                model.UsingType = 0;
                                if (SYS_ParameterService.insert(userId, model))
                                    success++;
                                else
                                {
                                    reason += "序号[" + Sequence + "]:添加失败，未知原因;";
                                    nosuccess++;
                                }
                            }
                            else
                            {
                                reason += "序号[" + Sequence + "]的代号重复;";
                                nosuccess++;
                            }
                        }
                        else
                            nosuccess++;
                    }

                }
            }
            if (nosuccess == total)
                status = "410";

            msg = string.Format(msg, total, success, nosuccess, reason);

            return new { status = status, msg = msg };
        }


        /// <summary>
        /// 设备保养清单设定导入
        /// SAM 2017年7月5日16:07:55
        /// </summary>
        /// <param name="token"></param>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static object Ems00008Import(string token, string filename)
        {
            string userId = UtilBussinessService.detoken(token);
            string SysID = Framework.SystemID;
            string status = "200", msg = "共{0}条信息,{1}条信息导入成功,{2}条信息导入失败！{3}";
            string reason = null;
            int total = 0;
            int success = 0;
            int nosuccess = 0;
            if (filename.EndsWith(".xls") || filename.EndsWith(".xlsx"))
            {
                IWorkbook wk = null;
                string Sequence = null;
                EMS_EquipmentMaintenanceList model = null;
                EMS_EquipmentMaintenanceListDetails Detailmodel = null;
                SYS_Parameters ParModel = null;
                using (FileStream fs = File.OpenRead(@filename))   //打开.xls文件
                {
                    if (filename.EndsWith(".xls"))
                        wk = new HSSFWorkbook(fs); //把xls文件中的数据写入wk中
                    else if (filename.EndsWith(".xlsx"))
                        wk = new XSSFWorkbook(fs);//把xlsx文件中的数据写入wk中

                    ISheet sheet = wk.GetSheetAt(0);   //读取第一表数据
                    for (int j = 2; j <= sheet.LastRowNum; j++)  //LastRowNum 是当前表的总行数
                    {
                        IRow row = sheet.GetRow(j);  //读取当前行数据
                        if (row.GetCell(0) == null) //如果当前行的第一列没有数据，停止读取
                            break;
                        if (string.IsNullOrWhiteSpace(row.GetCell(0).ToString()))//如果当前行的第一列没有数据，停止读取，这是判断当row.GetCell不能null然后实际是空字符
                            break;
                        total++;
                        ICell cell = null;
                        if (row != null)
                        {
                            model = new EMS_EquipmentMaintenanceList();
                            Detailmodel = new EMS_EquipmentMaintenanceListDetails();

                            cell = row.GetCell(0);//序号
                            Sequence = cell.ToString();
                            cell = row.GetCell(1);//代号
                            if (cell != null)
                            {
                                if (cell.ToString().Length <= 20)
                                {
                                    model.Code = cell.ToString();
                                }
                                else
                                {
                                    reason += "序号[" + Sequence + "]:代号超过20个字;";
                                    nosuccess++;
                                    continue; //结束本次循环
                                }
                            }
                            else
                            {
                                reason += "序号[" + Sequence + "]代号为空;";
                                nosuccess++;
                                continue; //结束本次循环
                            }
                            cell = row.GetCell(2);//说明
                            if (cell != null)
                            {
                                if (cell.ToString().Length <= 60)
                                {
                                    model.Name = cell.ToString();
                                }
                                else
                                {
                                    reason += "序号[" + Sequence + "]:说明超过60个字;";
                                    nosuccess++;
                                    continue; //结束本次循环
                                }
                            }
                            else
                            {
                                reason += "序号[" + Sequence + "]说明为空;";
                                nosuccess++;
                                continue; //结束本次循环
                            }

                            cell = row.GetCell(3);//保养类型
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                ParModel = SYS_ParameterService.getByCode(cell.ToString(), SysID + "0191213000023");
                                if (ParModel == null)
                                {
                                    reason += "序号[" + Sequence + "]:保养类型错误，不存在的保养类型;";
                                    nosuccess++;
                                    continue; //结束本次循环
                                }
                                else
                                    model.Type = ParModel.ParameterID;
                            }
                            else
                            {
                                reason += "序号[" + Sequence + "]保养类型不能为空;";
                                nosuccess++;
                                continue; //结束本次循环
                            }


                            cell = row.GetCell(4);//保养项目
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                ParModel = SYS_ParameterService.getByCode(cell.ToString(), SysID + "0191213000022");
                                if (ParModel == null)
                                {
                                    reason += "序号[" + Sequence + "]:保养项目错误，不存在的保养项目;";
                                    nosuccess++;
                                    continue; //结束本次循环
                                }
                                else
                                    Detailmodel.DetailID = ParModel.ParameterID;
                            }
                        }


                        EMS_EquipmentMaintenanceList EModel = EMS_EquipmentMaintenanceListService.getByCode(model.Code);
                        //首先判断是否已经存在保养清单代号
                        if (EModel == null)//不存在，那就添加保养清单
                        {
                            model.EquipmentMaintenanceListID = UniversalService.GetSerialNumber("EMS_EquipmentMaintenanceList");
                            model.Status = Framework.SystemID + "0201213000001";
                            if (EMS_EquipmentMaintenanceListService.insert(userId, model))
                            {
                                if (!string.IsNullOrWhiteSpace(Detailmodel.DetailID))//同时存在明细,那就添加明细
                                {
                                    Detailmodel.EquipmentMaintenanceListDetailID = UniversalService.GetSerialNumber("EMS_EquipmentMaintenanceListDetails");
                                    Detailmodel.EquipmentMaintenanceListID = model.EquipmentMaintenanceListID;
                                    Detailmodel.Sequence = 0;
                                    Detailmodel.Type = 1;
                                    Detailmodel.Status = Framework.SystemID + "0201213000001"; //状态       
                                    EMS_EquipmentMaintenanceListDetailsService.insert(userId, Detailmodel);
                                }
                                success++;
                            }
                            else
                            {
                                reason += "序号[" + Sequence + "]:添加失败，未知原因;";
                                nosuccess++;
                            }
                        }
                        else if (!string.IsNullOrWhiteSpace(Detailmodel.DetailID))  //当保养清单已存在时，判断是否存在项目
                        {
                            if (EMS_EquipmentMaintenanceListDetailsService.CheckDetail(Detailmodel.DetailID, 1, EModel.EquipmentMaintenanceListID, null))
                            {
                                reason += "序号[" + Sequence + "]:保养项目重复";
                                nosuccess++;
                            }
                            else
                            {
                                Detailmodel.EquipmentMaintenanceListDetailID = UniversalService.GetSerialNumber("EMS_EquipmentMaintenanceListDetails");
                                Detailmodel.EquipmentMaintenanceListID = EModel.EquipmentMaintenanceListID;
                                Detailmodel.Sequence = 0;
                                Detailmodel.Type = 1;
                                Detailmodel.Status = Framework.SystemID + "0201213000001"; //状态       
                                EMS_EquipmentMaintenanceListDetailsService.insert(userId, Detailmodel);
                                success++;
                            }
                        }
                        else //当保养清单存在，但是却没有保养项目时
                        {
                            reason += "序号[" + Sequence + "]:保养清单重复";
                            nosuccess++;
                        }

                    }

                }
            }
            if (nosuccess == total)
                status = "410";

            msg = string.Format(msg, total, success, nosuccess, reason);

            return new { status = status, msg = msg };
        }


        /// <summary>
        /// 设备巡检维护
        /// SAM 2017年9月4日14:38:19
        /// </summary>
        /// <param name="token"></param>
        /// <param name="filename"></param>
        /// <param name="EquipmentInspectionRecordID"></param>
        /// <returns></returns>
        public static object Ems00003Import(string token, string filename, string EquipmentInspectionRecordID)
        {
            string userId = UtilBussinessService.detoken(token);
            string SysID = Framework.SystemID;
            string status = "200", msg = "共{0}条信息,{1}条信息导入成功,{2}条信息导入失败！{3}";
            string reason = null;
            int total = 0;
            int success = 0;
            int nosuccess = 0;

            EMS_EquipmentInspectionRecord Header = EMS_EquipmentInspectionRecordService.get(EquipmentInspectionRecordID);
            if (Header == null)
                return new { status = "440", msg = "表头流水号错误" };

            if (filename.EndsWith(".xls") || filename.EndsWith(".xlsx"))
            {
                IWorkbook wk = null;
                string Sequence = null;
                EMS_EquipmentInspectionRecordDetails model = null;
                using (FileStream fs = File.OpenRead(@filename))   //打开.xls文件
                {
                    if (filename.EndsWith(".xls"))
                        wk = new HSSFWorkbook(fs); //把xls文件中的数据写入wk中
                    else if (filename.EndsWith(".xlsx"))
                        wk = new XSSFWorkbook(fs);//把xlsx文件中的数据写入wk中

                    ISheet sheet = wk.GetSheetAt(0);   //读取第一表数据
                    for (int j = 2; j <= sheet.LastRowNum; j++)  //LastRowNum 是当前表的总行数
                    {
                        IRow row = sheet.GetRow(j);  //读取当前行数据
                        if (row.GetCell(0) == null) //如果当前行的第一列没有数据，停止读取
                            break;
                        if (string.IsNullOrWhiteSpace(row.GetCell(0).ToString()))//如果当前行的第一列没有数据，停止读取，这是判断当row.GetCell不能null然后实际是空字符
                            break;
                        total++;
                        ICell cell = null;
                        if (row != null)
                        {
                            model = new EMS_EquipmentInspectionRecordDetails();

                            cell = row.GetCell(0);//序号
                            Sequence = cell.ToString();

                            cell = row.GetCell(1);//设备项目
                            if (cell != null && string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                SYS_Projects Promodel = SYS_ProjectsService.getByCode(cell.ToString());
                                if (Promodel != null)
                                    model.ProjectID = Promodel.ProjectID;
                                else
                                {
                                    reason += "序号[" + Sequence + "]:项目错误，不存在的项目代号;";
                                    nosuccess++;
                                    continue; //结束本次循环
                                }
                            }
                            else
                            {
                                reason += "序号[" + Sequence + "]项目为空;";
                                nosuccess++;
                                continue; //结束本次循环
                            }

                            cell = row.GetCell(2);//现值
                            if (cell != null && string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                if (cell.ToString().Length <= 20)
                                    model.Value = cell.ToString();
                                else
                                    reason += "序号[" + Sequence + "]:现值超过20个字;";
                            }

                            cell = row.GetCell(3);//备注
                            if (cell != null)
                            {
                                if (cell.ToString().Length <= 120)
                                    model.Comments = cell.ToString();
                                else
                                    reason += "序号[" + Sequence + "]:备注超过120个字;";
                            }

                            model.EIRDID = UniversalService.GetSerialNumber("EMS_EquipmentInspectionRecordDetails");
                            model.EquipmentInspectionRecordID = EquipmentInspectionRecordID;
                            model.Status = SysID + "0201213000001";
                            if (EMS_EquipmentInspectionRecordDetailsService.insert(userId, model))
                                success++;
                            else
                            {
                                reason += "序号[" + Sequence + "]:添加失败，未知原因;";
                                nosuccess++;
                            }
                        }

                    }
                }
            }
            if (nosuccess == total)
                status = "410";

            msg = string.Format(msg, total, success, nosuccess, reason);

            return new { status = status, msg = msg };
        }
        /// <summary>
        /// 制程检验单导入
        /// Joint 2017年8月2日18:16:41
        /// </summary>
        /// <param name="token"></param>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static object Qcs00005Import(string token, string filename)
        {
            string userId = UtilBussinessService.detoken(token);
            string SysID = Framework.SystemID;
            string status = "200", msg = "共{0}条信息,{1}条信息导入成功,{2}条信息导入失败！{3}";
            string reason = null;
            int total = 0;
            int success = 0;
            int nosuccess = 0;
            //判定方式，先查找对应检验项目判定方式是什么
            MES_Parameter model2 = new MES_Parameter();//创建Inf00020实体
            model2.ParameterID = Framework.SystemID + "1101213000003";//赋值对应明细的检验项目判定方式流水号
            model2 = MES_ParameterService.get(model2.ParameterID);//获取对应实体
            MES_Parameter model3 = new MES_Parameter();//表头的品质判定方式
            model3.ParameterID = Framework.SystemID + "1101213000002";
            model3 = MES_ParameterService.get(model3.ParameterID);
            if (filename.EndsWith(".xls") || filename.EndsWith(".xlsx"))
            {
                IWorkbook wk = null;
                string Sequence = null;
                QCS_InspectionDocument model = null;
                string AutoNumberID = null;

                using (FileStream fs = File.OpenRead(@filename))   //打开.xls文件
                {
                    if (filename.EndsWith(".xls"))
                        wk = new HSSFWorkbook(fs); //把xls文件中的数据写入wk中
                    else if (filename.EndsWith(".xlsx"))
                        wk = new XSSFWorkbook(fs);//把xlsx文件中的数据写入wk中

                    ISheet sheet = wk.GetSheetAt(0);   //读取第一表数据
                    for (int j = 2; j <= sheet.LastRowNum; j++)  //LastRowNum 是当前表的总行数
                    {
                        IRow row = sheet.GetRow(j);  //读取当前行数据
                        if (row.GetCell(0) == null) //如果当前行的第一列没有数据，停止读取
                            break;
                        if (string.IsNullOrWhiteSpace(row.GetCell(0).ToString()))//如果当前行的第一列没有数据，停止读取，这是判断当row.GetCell不能null然后实际是空字符
                            break;
                        total++;
                        ICell cell = null;
                        if (row != null)
                        {
                            model = new QCS_InspectionDocument();
                            AutoNumberID = null;
                            cell = row.GetCell(0);//序号
                            Sequence = cell.ToString();

                            cell = row.GetCell(1);//单据日期
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                try
                                {
                                    model.DocumentDate = DateTime.Parse(cell.ToString());
                                }
                                catch
                                {
                                    reason += "序号[" + Sequence + "]:单据日期格式错误;";
                                    nosuccess++;
                                    continue; //结束本次循环
                                }
                            }
                            else
                            {
                                reason += "序号[" + Sequence + "]:单据日期不能为空;";
                                nosuccess++;
                                continue; //结束本次循环
                            }

                            cell = row.GetCell(2);//完工单号
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                SFC_CompletionOrder Order = SFC_CompletionOrderService.getByCode(cell.ToString());
                                if (Order == null)
                                {
                                    reason += "序号[" + Sequence + "]:完工单不存在;";
                                    nosuccess++;
                                    continue; //结束本次循环
                                }
                                model.CompletionOrderID = Order.CompletionOrderID;
                                model.ItemID = Order.ItemID;
                                model.TaskDispatchID = Order.TaskDispatchID;
                            }
                            else
                            {
                                reason += "序号[" + Sequence + "]:完工单号不能为空;";
                                nosuccess++;
                                continue; //结束本次循环
                            }

                            cell = row.GetCell(3);//免检
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                if (cell.ToString() == "Y")
                                    model.InspectionFlag = true;
                                else if (cell.ToString() == "N")
                                    model.InspectionFlag = false;
                            }
                            else
                            {
                                reason += "序号[" + Sequence + "]:免检为空;";
                                nosuccess++;
                            }

                            cell = row.GetCell(4);//检验人员
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                SYS_MESUsers user = SYS_MESUserService.getByName(cell.ToString());
                                if (user == null)
                                {
                                    reason += "序号[" + Sequence + "]:检验人员不存在;";
                                    nosuccess++;
                                    continue; //结束本次循环
                                }
                                model.InspectionUserID = user.MESUserID;
                            }
                            else
                            {
                                reason += "序号[" + Sequence + "]:检验人员为空;";
                                nosuccess++;
                                continue; //结束本次循环
                            }

                            cell = row.GetCell(5);//检验日期
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                try
                                {
                                    model.InspectionDate = DateTime.Parse(cell.ToString());
                                }
                                catch
                                {
                                    reason += "序号[" + Sequence + "]:检验日期格式错误;";
                                    nosuccess++;
                                }
                            }
                            else
                            {
                                reason += "序号[" + Sequence + "]:检验日期为空;";
                                nosuccess++;
                            }
                            if (model3.Value == Framework.SystemID + "02012130000B1")
                            {//品质判定为自动
                                model.QualityControlDecision = Framework.SystemID + "0201213000091";//自动判定时，默认为允收
                            }
                            else
                            { //品质判定为人工
                                cell = row.GetCell(6);//品质判定
                                if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                                {
                                    SYS_Parameters meter = SYS_ParameterService.getByCode(cell.ToString(), Framework.SystemID + "0191213000061");
                                    if (meter == null)
                                    {
                                        reason += "序号[" + Sequence + "]:品质判定输入错误;";
                                        nosuccess++;
                                    }
                                    model.QualityControlDecision = meter.ParameterID;
                                }
                                else
                                {
                                    reason += "序号[" + Sequence + "]:品质判定为空;";
                                    nosuccess++;
                                }
                            }
                            cell = row.GetCell(7);//检验数量
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                try
                                {
                                    decimal check = decimal.Parse(cell.ToString());
                                    if (check > 0)
                                    {
                                        string[] str = cell.ToString().Split('.');//将整数和小数拆开
                                        if (str.Length == 1)
                                        {
                                            if (str[0].Length <= 12)
                                                model.InspectionQuantity = check;
                                        }
                                        else
                                        {
                                            if (str[0].Length <= 12 && str[1].Length <= 6)
                                                model.InspectionQuantity = check;
                                        }

                                    }
                                    else
                                    {
                                        reason += "序号[" + Sequence + "]:检验数量;";
                                        nosuccess++;
                                        continue;
                                    }
                                }
                                catch
                                {
                                    reason += "序号[" + Sequence + "]:检验数量格式错误;";
                                    nosuccess++;
                                    continue;
                                }

                            }
                            else
                            {
                                reason += "序号[" + Sequence + "]:检验数量不能为空;";
                                nosuccess++;
                                continue;
                            }

                            cell = row.GetCell(8);//报废数量
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                try
                                {
                                    decimal check = decimal.Parse(cell.ToString());
                                    if (check >= 0 && check < model.InspectionQuantity)//数值大于等于0且小于检验数量
                                    {
                                        string[] str = cell.ToString().Split('.');//将整数和小数拆开
                                        if (str.Length == 1)
                                        {
                                            if (str[0].Length <= 12)
                                                model.ScrappedQuantity = check;
                                        }
                                        else
                                        {
                                            if (str[0].Length <= 12 && str[1].Length <= 6)
                                                model.ScrappedQuantity = check;
                                        }
                                    }
                                    else
                                    {
                                        reason += "序号[" + Sequence + "]:报废数量格式错误;";
                                        nosuccess++;
                                        model.ScrappedQuantity = 0;
                                    }
                                }
                                catch
                                {
                                    reason += "序号[" + Sequence + "]:报废数量格式错误;";
                                    nosuccess++;
                                    model.ScrappedQuantity = 0;
                                }
                            }
                            else
                            {
                                reason += "序号[" + Sequence + "]:报废数量为空;";
                                nosuccess++;
                                model.ScrappedQuantity = 0;
                            }

                            cell = row.GetCell(9);//NG数量
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                try
                                {
                                    decimal check = decimal.Parse(cell.ToString());
                                    if (check >= 0 && check < model.InspectionQuantity)//数值大于等于0且小于检验数量
                                    {
                                        string[] str = cell.ToString().Split('.');//将整数和小数拆开
                                        if (str.Length == 1)
                                        {
                                            if (str[0].Length <= 12)
                                                model.NGquantity = check;
                                        }
                                        else
                                        {
                                            if (str[0].Length <= 12 && str[1].Length <= 6)
                                                model.NGquantity = check;
                                        }
                                    }
                                    else
                                    {
                                        reason += "序号[" + Sequence + "]:NG数量格式错误;";
                                        nosuccess++;
                                        model.NGquantity = 0;
                                    }
                                }
                                catch
                                {
                                    reason += "序号[" + Sequence + "]:NG数量格式错误;";
                                    nosuccess++;
                                    model.NGquantity = 0;
                                }
                            }
                            else
                            {
                                reason += "序号[" + Sequence + "]:NG数量为空;";
                                nosuccess++;
                                model.NGquantity = 0;
                            }


                            model.Status = Framework.SystemID + "020121300008D";//新增状态皆为立单
                            model.OKQuantity = 0;
                            if (model.QualityControlDecision == Framework.SystemID + "0201213000090")//如果表头判定为拒收可转移量预设为0
                            {
                                model.OKQuantity = 0;

                            }
                            else//为OK或者SA时可转移量为
                            {
                                model.OKQuantity = model.InspectionQuantity - model.ScrappedQuantity - model.NGquantity;
                            }
                            if (model.OKQuantity > model.InspectionQuantity)
                            {
                                reason += "计算的可移转量大于检验数量";
                                nosuccess++;
                                continue; //结束本次循环
                            }
                            cell = row.GetCell(10);//备注
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                if (cell.ToString().Length <= 120)
                                    model.Comments = cell.ToString();
                                else
                                    reason += "序号[" + Sequence + "]:备注超过120个字;";
                            }
                            else
                            {
                                reason += "序号[" + Sequence + "]:备注为空;";
                                nosuccess++;
                            }

                            if (model.ScrappedQuantity + model.NGquantity + model.OKQuantity != model.InspectionQuantity)
                            {
                                reason += "序号[" + Sequence + "]:可移转量+报废数量+NG数量不等于检验数量;";
                                nosuccess++;
                                continue; //结束本次循环
                            }

                            model.InspectionDocumentID = UniversalService.GetSerialNumber("QCS_InspectionDocument");
                            model.InspectionMethod = SysID + "020121300007E";//检验种类
                            //在表头新增，单据编号更新之前先增加明细数据
                            //上面以获取对应完工单流水号，任务单流水号，料品流水号
                            SFC_TaskDispatch TdModel = SFC_TaskDispatchService.get(model.TaskDispatchID);//任务单流水号拿到对应的制程，工序流水号
                            model.FinQuantity = TdModel.DispatchQuantity;
                            //通过任务单的制程，工序，获取的对应明细
                            List<QCS_StaInsSpeSetting> StaInsSpeSetting = QCS_StaInsSpeSettingService.GetListByItemID(model.ItemID, model.InspectionMethod, TdModel.ProcessID, TdModel.OperationID);
                            SFC_CompletionOrder COmodel = SFC_CompletionOrderService.get(model.CompletionOrderID);
                            if (StaInsSpeSetting.Count == 0)
                            {
                                reason = reason + "完工单号为" + COmodel.CompletionNo + "：明细为空";
                                nosuccess++;
                                continue;
                            }
                            //检验明细新增
                            foreach (QCS_StaInsSpeSetting Sta in StaInsSpeSetting)
                            {
                                QCS_InspectionProject Sampling = QCS_InspectionProjectService.get(Sta.InspectionProjectID);
                                QCS_InspectionDocumentDetails DetailsModel = new QCS_InspectionDocumentDetails();
                                DetailsModel.InspectionDocumentDetailID = UniversalService.GetSerialNumber("QCS_InspectionDocumentDetails");
                                DetailsModel.InspectionDocumentID = model.InspectionDocumentID;
                                DetailsModel.Sequence = Sta.Sequence;//排序
                                DetailsModel.InspectionStandard = Sta.InspectionStandard;//检验标准
                                DetailsModel.InspectionItemID = Sta.InspectionProjectID;//检验项目流水号用于获取项目代号与说明
                                DetailsModel.InspectionMethod = Sta.InspectionType;//检验种类
                                DetailsModel.InspectionClassID = Sta.CategoryID;//检验类别
                                DetailsModel.InspectionMethodID = Sta.InspectionMethod;//检验方式
                                DetailsModel.InspectionLevelID = Sampling.InspectionLevel;//检验水准
                                DetailsModel.InspectionFaultID = Sampling.Disadvantages;//缺点等级
                                DetailsModel.Status = Framework.SystemID + "0201213000001";
                                DetailsModel.Aql = Sta.AQL;
                                DetailsModel.AttributeType = Sta.Attribute;
                                //当没有AQL值默认为0
                                if (Sta.AQL == null)
                                {
                                    DetailsModel.SampleQuantity = 0;
                                    DetailsModel.AcQuantity = 0;
                                    DetailsModel.ReQuantity = 0;
                                }
                                //当QCS000001对应的数据没有时，AC,RE,抽样数量设置为0
                                else
                                {
                                    QCS_CheckTestSetting Check = QCS_CheckTestSettingService.getByAql(DetailsModel.InspectionLevelID, DetailsModel.InspectionMethodID, Sta.AQL);//获取抽检检验设定实体
                                    if (Check == null)
                                    {
                                        nosuccess++;
                                        reason += "部分新增失败，QCS000001没有与检验方式，检验水平，AQL对应的数据，AC,RE，抽样数量将默认为0";
                                        DetailsModel.SampleQuantity = 0;
                                        DetailsModel.AcQuantity = 0;
                                        DetailsModel.ReQuantity = 0;
                                    }
                                    else
                                    {
                                        //当QCS000001对应的明细数据没有时，AC,RE,抽样数量设置为0
                                        QCS_CheckTestSettingDetails CheckDetail = QCS_CheckTestSettingDetailsService.getCTSDetails(Check.CheckTestSettingID);//获取抽检检验设定明细实体
                                        if (Check == null)
                                        {
                                            nosuccess++;
                                            reason += "有部分QCS000001明细没有与检验方式，检验水平，AQL对应的明细数据，AC,RE，抽样数量将默认为0";
                                            DetailsModel.SampleQuantity = 0;
                                            DetailsModel.AcQuantity = 0;
                                            DetailsModel.ReQuantity = 0;
                                        }
                                        //decimal p = model.InspectionQuantity / CheckDetail.EndBatch;//检验数量自动计算？检验数量除以结束批量？
                                        //DetailsModel.SampleQuantity = p * CheckDetail.SamplingQuantity;
                                        //DetailsModel.AcQuantity = p * CheckDetail.AcQuantity;
                                        //DetailsModel.ReQuantity = p * CheckDetail.ReQuantity;
                                        //不确定P的计算方式，暂时注释掉
                                        else
                                        {
                                            DetailsModel.SampleQuantity = CheckDetail.SamplingQuantity;
                                            DetailsModel.AcQuantity = CheckDetail.AcQuantity;
                                            DetailsModel.ReQuantity = CheckDetail.ReQuantity;
                                        }
                                    }
                                }
                                DetailsModel.NGquantity = 0;//不良数？

                                if (model2.Value == Framework.SystemID + "02012130000B3")//查看系统设置的判定方式
                                {
                                    if (DetailsModel.NGquantity >= DetailsModel.ReQuantity)
                                        DetailsModel.QualityControlDecision = Framework.SystemID + "0201213000090";
                                    else
                                        DetailsModel.QualityControlDecision = Framework.SystemID + "0201213000091";
                                }
                                else
                                {
                                    DetailsModel.QualityControlDecision = "人工判定";
                                }
                                if (model3.Value == Framework.SystemID + "02012130000B1" && DetailsModel.QualityControlDecision == Framework.SystemID + "0201213000090")
                                {//品质判定为自动且新增的明细判定为拒收
                                    model.QualityControlDecision = Framework.SystemID + "0201213000090";//假如系统判定方式为自动，明细有一个为拒收，那么表头也为拒收

                                    QCS_InspectionDocumentService.update(userId, model);//更新表头数据
                                }
                                QCS_InspectionDocumentDetailsService.insert(userId, DetailsModel);

                            }
                            //明细新增结束
                            IList<Hashtable> TypeList = SYS_DocumentTypeSettingService.GetTypeList(userId, SysID + "020121300003B");
                            if (TypeList.Count != 0)
                            {
                                model.InspectionNo = UtilBussinessService.GetDocumentAutoNumber(userId, TypeList[0]["value"].ToString(), model.DocumentDate.ToString(), ref AutoNumberID);
                            }
                            else
                            {
                                reason += "序号[" + Sequence + "]:当前用户并没有自动编号权限;";
                                nosuccess++;
                                continue; //结束本次循环
                            }
                            if (QCS_InspectionDocumentService.insert(userId, model))
                            {
                                UtilBussinessService.UpdateDocumentAutoNumber(userId, AutoNumberID);
                                success++;
                            }
                            else
                            {
                                reason += "序号[" + Sequence + "]:添加失败，未知原因;";
                                nosuccess++;
                            }
                        }


                    }
                }
            }
            if (nosuccess == total)
                status = "410";

            msg = string.Format(msg, total, success, nosuccess, reason);

            return new { status = status, msg = msg };
        }




        /// <summary>
        /// 制程首件检验导入
        /// Joint 2017年8月2日14:23:20
        /// </summary>
        /// <param name="token"></param>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static object Qcs00007Import(string token, string filename)
        {
            string userId = UtilBussinessService.detoken(token);
            string SysID = Framework.SystemID;
            string status = "200", msg = "共{0}条信息,{1}条信息导入成功,{2}条信息导入失败！{3}";
            string reason = null;
            int total = 0;
            int success = 0;
            int nosuccess = 0;
            //判定方式，先查找对应检验项目判定方式是什么
            MES_Parameter model2 = new MES_Parameter();//创建Inf00020实体
            model2.ParameterID = Framework.SystemID + "1101213000003";//赋值对应明细的检验项目判定方式流水号
            model2 = MES_ParameterService.get(model2.ParameterID);//获取对应实体
            MES_Parameter model3 = new MES_Parameter();//表头的品质判定方式
            model3.ParameterID = Framework.SystemID + "1101213000002";
            model3 = MES_ParameterService.get(model3.ParameterID);
            if (filename.EndsWith(".xls") || filename.EndsWith(".xlsx"))
            {
                IWorkbook wk = null;
                string Sequence = null;
                QCS_InspectionDocument model = null;
                string AutoNumberID = null;

                using (FileStream fs = File.OpenRead(@filename))   //打开.xls文件
                {
                    if (filename.EndsWith(".xls"))
                        wk = new HSSFWorkbook(fs); //把xls文件中的数据写入wk中
                    else if (filename.EndsWith(".xlsx"))
                        wk = new XSSFWorkbook(fs);//把xlsx文件中的数据写入wk中

                    ISheet sheet = wk.GetSheetAt(0);   //读取第一表数据
                    for (int j = 2; j <= sheet.LastRowNum; j++)  //LastRowNum 是当前表的总行数
                    {
                        IRow row = sheet.GetRow(j);  //读取当前行数据
                        if (row.GetCell(0) == null) //如果当前行的第一列没有数据，停止读取
                            break;
                        if (string.IsNullOrWhiteSpace(row.GetCell(0).ToString()))//如果当前行的第一列没有数据，停止读取，这是判断当row.GetCell不能null然后实际是空字符
                            break;
                        total++;
                        ICell cell = null;
                        if (row != null)
                        {
                            model = new QCS_InspectionDocument();
                            AutoNumberID = null;
                            cell = row.GetCell(0);//序号
                            Sequence = cell.ToString();

                            cell = row.GetCell(1);//单据日期
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                try
                                {
                                    model.DocumentDate = DateTime.Parse(cell.ToString());
                                }
                                catch
                                {
                                    reason += "序号[" + Sequence + "]:单据日期格式错误;";
                                    nosuccess++;
                                    continue; //结束本次循环
                                }
                            }
                            else
                            {
                                reason += "序号[" + Sequence + "]:单据日期不能为空;";
                                nosuccess++;
                                continue; //结束本次循环
                            }

                            cell = row.GetCell(2);//任务单号
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                SFC_TaskDispatch Order = SFC_TaskDispatchService.getByCode(cell.ToString());
                                if (Order == null)
                                {
                                    reason += "序号[" + Sequence + "]:任务单不存在;";
                                    nosuccess++;
                                    continue; //结束本次循环
                                }
                                model.TaskDispatchID = Order.TaskDispatchID;
                                model.ItemID = Order.ItemID;

                            }
                            else
                            {
                                reason += "序号[" + Sequence + "]:任务单号不能为空;";
                                nosuccess++;
                                continue; //结束本次循环
                            }

                            cell = row.GetCell(3);//免检
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                if (cell.ToString() == "Y")
                                    model.InspectionFlag = true;
                                else if (cell.ToString() == "N")
                                    model.InspectionFlag = false;
                            }
                            else
                            {
                                reason += "序号[" + Sequence + "]:免检为空;";
                                nosuccess++;
                            }

                            cell = row.GetCell(4);//检验人员
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                SYS_MESUsers user = SYS_MESUserService.getByName(cell.ToString());
                                if (user == null)
                                {
                                    reason += "序号[" + Sequence + "]:检验人员不存在;";
                                    nosuccess++;
                                }
                                model.InspectionUserID = user.MESUserID;
                            }
                            else
                            {
                                reason += "序号[" + Sequence + "]:检验人员为空;";
                                nosuccess++;
                            }

                            cell = row.GetCell(5);//检验日期
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                try
                                {
                                    model.InspectionDate = DateTime.Parse(cell.ToString());
                                }
                                catch
                                {
                                    reason += "序号[" + Sequence + "]:检验日期格式错误;";
                                    nosuccess++;
                                }
                            }
                            else
                            {
                                reason += "序号[" + Sequence + "]:检验日期为空;";
                                nosuccess++;
                            }
                            if (model3.Value == Framework.SystemID + "02012130000B1")
                            {//品质判定为自动
                                model.QualityControlDecision = Framework.SystemID + "0201213000091";//自动判定时，默认为允收
                            }
                            else
                            { //品质判定为人工
                                cell = row.GetCell(6);//品质判定
                                if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                                {
                                    SYS_Parameters meter = SYS_ParameterService.getByCode(cell.ToString(), Framework.SystemID + "0191213000061");
                                    if (meter == null)
                                    {
                                        reason += "序号[" + Sequence + "]:品质判定代号输入错误;";
                                        nosuccess++;
                                    }
                                    model.QualityControlDecision = meter.ParameterID;
                                }
                                else
                                {
                                    reason += "序号[" + Sequence + "]:品质判定为空;";
                                    nosuccess++;
                                }
                            }
                            cell = row.GetCell(7);//检验数量
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                try
                                {
                                    decimal check = decimal.Parse(cell.ToString());
                                    if (check > 0)
                                    {
                                        string[] str = cell.ToString().Split('.');//将整数和小数拆开
                                        if (str.Length == 1)
                                        {
                                            if (str[0].Length <= 12)
                                                model.InspectionQuantity = check;
                                        }
                                        else
                                        {
                                            if (str[0].Length <= 12 && str[1].Length <= 6)
                                                model.InspectionQuantity = check;
                                        }

                                    }
                                    else
                                    {
                                        reason += "序号[" + Sequence + "]:检验数量不得小于0;";
                                        nosuccess++;
                                        continue;
                                    }
                                }
                                catch
                                {
                                    reason += "序号[" + Sequence + "]:检验数量格式错误;";
                                    nosuccess++;
                                    continue;
                                }

                            }
                            else
                            {
                                reason += "序号[" + Sequence + "]:检验数量不能为空;";
                                nosuccess++;
                                continue;
                            }

                            cell = row.GetCell(8);//报废数量
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                try
                                {
                                    decimal check = decimal.Parse(cell.ToString());
                                    if (check >= 0 && check < model.InspectionQuantity)//数值大于等于0且小于检验数量
                                    {
                                        string[] str = cell.ToString().Split('.');//将整数和小数拆开
                                        if (str.Length == 1)
                                        {
                                            if (str[0].Length <= 12)
                                                model.ScrappedQuantity = check;
                                        }
                                        else
                                        {
                                            if (str[0].Length <= 12 && str[1].Length <= 6)
                                                model.ScrappedQuantity = check;
                                        }
                                    }
                                    else
                                    {
                                        reason += "序号[" + Sequence + "]:报废数量格式错误;";
                                        nosuccess++;
                                        model.ScrappedQuantity = 0;
                                    }
                                }
                                catch
                                {
                                    reason += "序号[" + Sequence + "]:报废数量格式错误;";
                                    nosuccess++;
                                    model.ScrappedQuantity = 0;
                                }
                            }
                            else
                            {
                                reason += "序号[" + Sequence + "]:报废数量为空;";
                                nosuccess++;
                                model.ScrappedQuantity = 0;
                            }

                            cell = row.GetCell(9);//NG数量
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                try
                                {
                                    decimal check = decimal.Parse(cell.ToString());
                                    if (check >= 0 && check < model.InspectionQuantity)//数值大于等于0且小于检验数量
                                    {
                                        string[] str = cell.ToString().Split('.');//将整数和小数拆开
                                        if (str.Length == 1)
                                        {
                                            if (str[0].Length <= 12)
                                                model.NGquantity = check;
                                        }
                                        else
                                        {
                                            if (str[0].Length <= 12 && str[1].Length <= 6)
                                                model.NGquantity = check;
                                        }
                                    }
                                    else
                                    {
                                        reason += "序号[" + Sequence + "]:NG数量格式错误;";
                                        nosuccess++;
                                        model.NGquantity = 0;
                                    }
                                }
                                catch
                                {
                                    reason += "序号[" + Sequence + "]:NG数量格式错误;";
                                    nosuccess++;
                                    model.NGquantity = 0;
                                }
                            }
                            else
                            {
                                reason += "序号[" + Sequence + "]:NG数量为空;";
                                nosuccess++;
                                model.NGquantity = 0;
                            }


                            model.Status = Framework.SystemID + "020121300008D";//新增状态皆为立单
                            model.OKQuantity = 0;
                            if (model.QualityControlDecision == Framework.SystemID + "0201213000090")//如果表头判定为拒收可转移量预设为0
                            {
                                model.OKQuantity = 0;

                            }
                            else//为OK或者SA时可转移量为
                            {
                                model.OKQuantity = model.InspectionQuantity - model.ScrappedQuantity - model.NGquantity;
                            }
                            if (model.OKQuantity > model.InspectionQuantity)
                            {
                                reason += "计算的可移转量大于检验数量";
                                nosuccess++;
                                continue; //结束本次循环
                            }
                            cell = row.GetCell(10);//备注
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                if (cell.ToString().Length <= 120)
                                    model.Comments = cell.ToString();
                                else
                                    reason += "序号[" + Sequence + "]:备注超过120个字;";
                            }
                            else
                            {
                                reason += "序号[" + Sequence + "]:备注为空;";
                                nosuccess++;
                            }

                            if (model.ScrappedQuantity + model.NGquantity + model.OKQuantity != model.InspectionQuantity)
                            {
                                reason += "序号[" + Sequence + "]:可移转量+报废数量+NG数量不等于检验数量;";
                                nosuccess++;
                                continue; //结束本次循环
                            }

                            model.InspectionDocumentID = UniversalService.GetSerialNumber("QCS_InspectionDocument");
                            model.InspectionMethod = SysID + "0201213000080";//检验种类
                            //在单据编号更新之前先增加明细数据
                            //上面以获取对应完工单流水号，任务单流水号，料品流水号
                            SFC_TaskDispatch TdModel = SFC_TaskDispatchService.get(model.TaskDispatchID);//任务单流水号拿到对应的制程，工序流水号
                            model.FinQuantity = TdModel.DispatchQuantity;
                            //通过任务单的制程，工序，获取的对应明细
                            List<QCS_StaInsSpeSetting> StaInsSpeSetting = QCS_StaInsSpeSettingService.GetListByItemID(model.ItemID, model.InspectionMethod, TdModel.ProcessID, TdModel.OperationID);

                            if (StaInsSpeSetting.Count == 0)
                            {
                                reason = reason + "任务单号为" + TdModel.TaskNo + "：明细为空";
                                nosuccess++;
                                continue;
                            }
                            //检验明细新增
                            foreach (QCS_StaInsSpeSetting Sta in StaInsSpeSetting)
                            {
                                QCS_InspectionProject Sampling = QCS_InspectionProjectService.get(Sta.InspectionProjectID);
                                QCS_InspectionDocumentDetails DetailsModel = new QCS_InspectionDocumentDetails();
                                DetailsModel.InspectionDocumentDetailID = UniversalService.GetSerialNumber("QCS_InspectionDocumentDetails");
                                DetailsModel.InspectionDocumentID = model.InspectionDocumentID;
                                DetailsModel.Sequence = Sta.Sequence;//排序
                                DetailsModel.InspectionStandard = Sta.InspectionStandard;//检验标准
                                DetailsModel.InspectionItemID = Sta.InspectionProjectID;//检验项目流水号用于获取项目代号与说明
                                DetailsModel.InspectionMethod = Sta.InspectionType;//检验种类
                                DetailsModel.InspectionClassID = Sta.CategoryID;//检验类别
                                DetailsModel.InspectionMethodID = Sta.InspectionMethod;//检验方式
                                DetailsModel.InspectionLevelID = Sampling.InspectionLevel;//检验水准
                                DetailsModel.InspectionFaultID = Sampling.Disadvantages;//缺点等级
                                DetailsModel.Status = Framework.SystemID + "0201213000001";
                                DetailsModel.Aql = Sta.AQL;
                                DetailsModel.AttributeType = Sta.Attribute;
                                //当没有AQL值默认为0
                                if (Sta.AQL == null)
                                {
                                    DetailsModel.SampleQuantity = 0;
                                    DetailsModel.AcQuantity = 0;
                                    DetailsModel.ReQuantity = 0;
                                }
                                //当QCS000001对应的数据没有时，AC,RE,抽样数量设置为0
                                else
                                {
                                    QCS_CheckTestSetting Check = QCS_CheckTestSettingService.getByAql(DetailsModel.InspectionLevelID, DetailsModel.InspectionMethodID, Sta.AQL);//获取抽检检验设定实体
                                    if (Check == null)
                                    {
                                        nosuccess++;
                                        reason += "部分新增失败，QCS000001没有与检验方式，检验水平，AQL对应的数据，AC,RE，抽样数量将默认为0";
                                        DetailsModel.SampleQuantity = 0;
                                        DetailsModel.AcQuantity = 0;
                                        DetailsModel.ReQuantity = 0;
                                    }
                                    else
                                    {
                                        //当QCS000001对应的明细数据没有时，AC,RE,抽样数量设置为0
                                        QCS_CheckTestSettingDetails CheckDetail = QCS_CheckTestSettingDetailsService.getCTSDetails(Check.CheckTestSettingID);//获取抽检检验设定明细实体
                                        if (Check == null)
                                        {
                                            nosuccess++;
                                            reason += "有部分QCS000001明细没有与检验方式，检验水平，AQL对应的明细数据，AC,RE，抽样数量将默认为0";
                                            DetailsModel.SampleQuantity = 0;
                                            DetailsModel.AcQuantity = 0;
                                            DetailsModel.ReQuantity = 0;
                                        }
                                        //decimal p = model.InspectionQuantity / CheckDetail.EndBatch;//检验数量自动计算？检验数量除以结束批量？
                                        //DetailsModel.SampleQuantity = p * CheckDetail.SamplingQuantity;
                                        //DetailsModel.AcQuantity = p * CheckDetail.AcQuantity;
                                        //DetailsModel.ReQuantity = p * CheckDetail.ReQuantity;
                                        //不确定P的计算方式，暂时注释掉
                                        else
                                        {
                                            DetailsModel.SampleQuantity = CheckDetail.SamplingQuantity;
                                            DetailsModel.AcQuantity = CheckDetail.AcQuantity;
                                            DetailsModel.ReQuantity = CheckDetail.ReQuantity;
                                        }
                                    }
                                }
                                DetailsModel.NGquantity = 0;//不良数？

                                if (model2.Value == Framework.SystemID + "02012130000B3")//查看系统设置的判定方式
                                {
                                    if (DetailsModel.NGquantity >= DetailsModel.ReQuantity)
                                        DetailsModel.QualityControlDecision = Framework.SystemID + "0201213000090";
                                    else
                                        DetailsModel.QualityControlDecision = Framework.SystemID + "0201213000091";
                                }
                                else
                                {
                                    DetailsModel.QualityControlDecision = "人工判定";
                                }
                                if (model3.Value == Framework.SystemID + "02012130000B1" && DetailsModel.QualityControlDecision == Framework.SystemID + "0201213000090")
                                {//品质判定为自动且新增的明细判定为拒收
                                    model.QualityControlDecision = Framework.SystemID + "0201213000090";//假如系统判定方式为自动，明细有一个为拒收，那么表头也为拒收

                                    QCS_InspectionDocumentService.update(userId, model);//更新表头数据
                                }
                                QCS_InspectionDocumentDetailsService.insert(userId, DetailsModel);

                            }
                            //明细新增结束
                            IList<Hashtable> TypeList = SYS_DocumentTypeSettingService.GetTypeList(userId, SysID + "020121300003C");
                            if (TypeList.Count != 0)
                            {
                                model.InspectionNo = UtilBussinessService.GetDocumentAutoNumber(userId, TypeList[0]["value"].ToString(), model.DocumentDate.ToString(), ref AutoNumberID);
                            }
                            else
                            {
                                reason += "序号[" + Sequence + "]:当前用户并没有自动编号权限;";
                                nosuccess++;
                                continue; //结束本次循环
                            }
                            if (QCS_InspectionDocumentService.insert(userId, model))
                            {
                                UtilBussinessService.UpdateDocumentAutoNumber(userId, AutoNumberID);
                                success++;
                            }
                            else
                            {
                                reason += "序号[" + Sequence + "]:添加失败，未知原因;";
                                nosuccess++;
                            }
                        }


                    }
                }
            }
            if (nosuccess == total)
                status = "410";

            msg = string.Format(msg, total, success, nosuccess, reason);

            return new { status = status, msg = msg };
        }

        /// <summary>
        /// 制程巡检检验导入
        /// Joint 2017年8月4日12:02:28
        /// </summary>
        /// <param name="token"></param>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static object Qcs00008Import(string token, string filename)
        {
            string userId = UtilBussinessService.detoken(token);
            string SysID = Framework.SystemID;
            string status = "200", msg = "共{0}条信息,{1}条信息导入成功,{2}条信息导入失败！{3}";
            string reason = null;
            int total = 0;
            int success = 0;
            int nosuccess = 0;
            //判定方式，先查找对应检验项目判定方式是什么
            MES_Parameter model2 = new MES_Parameter();//创建Inf00020实体
            model2.ParameterID = Framework.SystemID + "1101213000003";//赋值对应明细的检验项目判定方式流水号
            model2 = MES_ParameterService.get(model2.ParameterID);//获取对应实体
            MES_Parameter model3 = new MES_Parameter();//表头的品质判定方式
            model3.ParameterID = Framework.SystemID + "1101213000002";
            model3 = MES_ParameterService.get(model3.ParameterID);
            if (filename.EndsWith(".xls") || filename.EndsWith(".xlsx"))
            {
                IWorkbook wk = null;
                string Sequence = null;
                QCS_InspectionDocument model = null;
                string AutoNumberID = null;

                using (FileStream fs = File.OpenRead(@filename))   //打开.xls文件
                {
                    if (filename.EndsWith(".xls"))
                        wk = new HSSFWorkbook(fs); //把xls文件中的数据写入wk中
                    else if (filename.EndsWith(".xlsx"))
                        wk = new XSSFWorkbook(fs);//把xlsx文件中的数据写入wk中

                    ISheet sheet = wk.GetSheetAt(0);   //读取第一表数据
                    for (int j = 2; j <= sheet.LastRowNum; j++)  //LastRowNum 是当前表的总行数
                    {
                        IRow row = sheet.GetRow(j);  //读取当前行数据
                        if (row.GetCell(0) == null) //如果当前行的第一列没有数据，停止读取
                            break;
                        if (string.IsNullOrWhiteSpace(row.GetCell(0).ToString()))//如果当前行的第一列没有数据，停止读取，这是判断当row.GetCell不能null然后实际是空字符
                            break;
                        total++;
                        ICell cell = null;
                        if (row != null)
                        {
                            model = new QCS_InspectionDocument();
                            AutoNumberID = null;
                            cell = row.GetCell(0);//序号
                            Sequence = cell.ToString();

                            cell = row.GetCell(1);//单据日期
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                try
                                {
                                    model.DocumentDate = DateTime.Parse(cell.ToString());
                                }
                                catch
                                {
                                    reason += "序号[" + Sequence + "]:单据日期格式错误;";
                                    nosuccess++;
                                    continue; //结束本次循环
                                }
                            }
                            else
                            {
                                reason += "序号[" + Sequence + "]:单据日期不能为空;";
                                nosuccess++;
                                continue; //结束本次循环
                            }

                            cell = row.GetCell(2);//任务单号
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                SFC_TaskDispatch Order = SFC_TaskDispatchService.getByCode(cell.ToString());
                                if (Order == null)
                                {
                                    reason += "序号[" + Sequence + "]:任务单不存在;";
                                    nosuccess++;
                                    continue; //结束本次循环
                                }
                                model.TaskDispatchID = Order.TaskDispatchID;
                                model.ItemID = Order.ItemID;

                            }
                            else
                            {
                                reason += "序号[" + Sequence + "]:任务单号不能为空;";
                                nosuccess++;
                                continue; //结束本次循环
                            }

                            cell = row.GetCell(3);//免检
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                if (cell.ToString() == "Y")
                                    model.InspectionFlag = true;
                                else if (cell.ToString() == "N")
                                    model.InspectionFlag = false;
                            }
                            else
                            {
                                reason += "序号[" + Sequence + "]:免检为空;";
                                nosuccess++;
                            }

                            cell = row.GetCell(4);//检验人员
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                SYS_MESUsers user = SYS_MESUserService.getByName(cell.ToString());
                                if (user == null)
                                {
                                    reason += "序号[" + Sequence + "]:检验人员不存在;";
                                    nosuccess++;
                                }
                                model.InspectionUserID = user.MESUserID;
                            }
                            else
                            {
                                reason += "序号[" + Sequence + "]:检验人员为空;";
                                nosuccess++;
                            }

                            cell = row.GetCell(5);//检验日期
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                try
                                {
                                    model.InspectionDate = DateTime.Parse(cell.ToString());
                                }
                                catch
                                {
                                    reason += "序号[" + Sequence + "]:检验日期格式错误;";
                                    nosuccess++;
                                }
                            }
                            else
                            {
                                reason += "序号[" + Sequence + "]:检验日期为空;";
                                nosuccess++;
                            }
                            if (model3.Value == Framework.SystemID + "02012130000B1")
                            {//品质判定为自动
                                model.QualityControlDecision = Framework.SystemID + "0201213000091";//自动判定时，默认为允收
                            }
                            else
                            { //品质判定为人工
                                cell = row.GetCell(6);//品质判定
                                if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                                {
                                    SYS_Parameters meter = SYS_ParameterService.getByCode(cell.ToString(), Framework.SystemID + "0191213000061");
                                    if (meter == null)
                                    {
                                        reason += "序号[" + Sequence + "]:品质判定输入错误;";
                                        nosuccess++;
                                    }
                                    model.QualityControlDecision = meter.ParameterID;
                                }
                                else
                                {
                                    reason += "序号[" + Sequence + "]:品质判定为空;";
                                    nosuccess++;
                                }
                            }
                            cell = row.GetCell(7);//检验数量
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                try
                                {
                                    decimal check = decimal.Parse(cell.ToString());
                                    if (check > 0)
                                    {
                                        string[] str = cell.ToString().Split('.');//将整数和小数拆开
                                        if (str.Length == 1)
                                        {
                                            if (str[0].Length <= 12)
                                                model.InspectionQuantity = check;
                                        }
                                        else
                                        {
                                            if (str[0].Length <= 12 && str[1].Length <= 6)
                                                model.InspectionQuantity = check;
                                        }

                                    }
                                    else
                                    {
                                        reason += "序号[" + Sequence + "]:检验数量不得小于0;";
                                        nosuccess++;
                                        continue;
                                    }
                                }
                                catch
                                {
                                    reason += "序号[" + Sequence + "]:检验数量格式错误;";
                                    nosuccess++;
                                    continue;
                                }

                            }
                            else
                            {
                                reason += "序号[" + Sequence + "]:检验数量不能为空;";
                                nosuccess++;
                                continue;
                            }

                            cell = row.GetCell(8);//报废数量
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                try
                                {
                                    decimal check = decimal.Parse(cell.ToString());
                                    if (check >= 0 && check < model.InspectionQuantity)//数值大于等于0且小于检验数量
                                    {
                                        string[] str = cell.ToString().Split('.');//将整数和小数拆开
                                        if (str.Length == 1)
                                        {
                                            if (str[0].Length <= 12)
                                                model.ScrappedQuantity = check;
                                        }
                                        else
                                        {
                                            if (str[0].Length <= 12 && str[1].Length <= 6)
                                                model.ScrappedQuantity = check;
                                        }
                                    }
                                    else
                                    {
                                        reason += "序号[" + Sequence + "]:报废数量格式错误;";
                                        nosuccess++;
                                        model.ScrappedQuantity = 0;
                                    }
                                }
                                catch
                                {
                                    reason += "序号[" + Sequence + "]:报废数量格式错误;";
                                    nosuccess++;
                                    model.ScrappedQuantity = 0;
                                }
                            }
                            else
                            {
                                reason += "序号[" + Sequence + "]:报废数量为空;";
                                nosuccess++;
                                model.ScrappedQuantity = 0;
                            }

                            cell = row.GetCell(9);//NG数量
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                try
                                {
                                    decimal check = decimal.Parse(cell.ToString());
                                    if (check >= 0 && check < model.InspectionQuantity)//数值大于等于0且小于检验数量
                                    {
                                        string[] str = cell.ToString().Split('.');//将整数和小数拆开
                                        if (str.Length == 1)
                                        {
                                            if (str[0].Length <= 12)
                                                model.NGquantity = check;
                                        }
                                        else
                                        {
                                            if (str[0].Length <= 12 && str[1].Length <= 6)
                                                model.NGquantity = check;
                                        }
                                    }
                                    else
                                    {
                                        reason += "序号[" + Sequence + "]:NG数量格式错误;";
                                        nosuccess++;
                                        model.NGquantity = 0;
                                    }
                                }
                                catch
                                {
                                    reason += "序号[" + Sequence + "]:NG数量格式错误;";
                                    nosuccess++;
                                    model.NGquantity = 0;
                                }
                            }
                            else
                            {
                                reason += "序号[" + Sequence + "]:NG数量为空;";
                                nosuccess++;
                                model.NGquantity = 0;
                            }


                            model.Status = Framework.SystemID + "020121300008D";//新增状态皆为立单
                            model.OKQuantity = 0;
                            if (model.QualityControlDecision == Framework.SystemID + "0201213000090")//如果表头判定为拒收可转移量预设为0
                            {
                                model.OKQuantity = 0;

                            }
                            else//为OK或者SA时可转移量为
                            {
                                model.OKQuantity = model.InspectionQuantity - model.ScrappedQuantity - model.NGquantity;
                            }
                            if (model.OKQuantity > model.InspectionQuantity)
                            {
                                reason += "计算的可移转量大于检验数量";
                                nosuccess++;
                                continue; //结束本次循环
                            }
                            cell = row.GetCell(10);//备注
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                if (cell.ToString().Length <= 120)
                                    model.Comments = cell.ToString();
                                else
                                    reason += "序号[" + Sequence + "]:备注超过120个字;";
                            }
                            else
                            {
                                reason += "序号[" + Sequence + "]:备注为空;";
                                nosuccess++;
                            }

                            if (model.ScrappedQuantity + model.NGquantity + model.OKQuantity != model.InspectionQuantity)
                            {
                                reason += "序号[" + Sequence + "]:可移转量+报废数量+NG数量不等于检验数量;";
                                nosuccess++;
                                continue; //结束本次循环
                            }

                            model.InspectionDocumentID = UniversalService.GetSerialNumber("QCS_InspectionDocument");
                            model.InspectionMethod = SysID + "0201213000081";//检验种类
                            //在单据编号更新之前先增加明细数据
                            //上面以获取对应完工单流水号，任务单流水号，料品流水号
                            SFC_TaskDispatch TdModel = SFC_TaskDispatchService.get(model.TaskDispatchID);//任务单流水号拿到对应的制程，工序流水号
                            model.FinQuantity = TdModel.DispatchQuantity;
                            //通过任务单的制程，工序，获取的对应明细
                            List<QCS_StaInsSpeSetting> StaInsSpeSetting = QCS_StaInsSpeSettingService.GetListByItemID(model.ItemID, model.InspectionMethod, TdModel.ProcessID, TdModel.OperationID);
                            if (StaInsSpeSetting.Count == 0)
                            {
                                reason = reason + "任务单号为" + TdModel.TaskNo + "：明细为空";
                                nosuccess++;
                                continue;
                            }
                            //检验明细新增
                            foreach (QCS_StaInsSpeSetting Sta in StaInsSpeSetting)
                            {
                                QCS_InspectionProject Sampling = QCS_InspectionProjectService.get(Sta.InspectionProjectID);
                                QCS_InspectionDocumentDetails DetailsModel = new QCS_InspectionDocumentDetails();
                                DetailsModel.InspectionDocumentDetailID = UniversalService.GetSerialNumber("QCS_InspectionDocumentDetails");
                                DetailsModel.InspectionDocumentID = model.InspectionDocumentID;
                                DetailsModel.Sequence = Sta.Sequence;//排序
                                DetailsModel.InspectionStandard = Sta.InspectionStandard;//检验标准
                                DetailsModel.InspectionItemID = Sta.InspectionProjectID;//检验项目流水号用于获取项目代号与说明
                                DetailsModel.InspectionMethod = Sta.InspectionType;//检验种类
                                DetailsModel.InspectionClassID = Sta.CategoryID;//检验类别
                                DetailsModel.InspectionMethodID = Sta.InspectionMethod;//检验方式
                                DetailsModel.InspectionLevelID = Sampling.InspectionLevel;//检验水准
                                DetailsModel.InspectionFaultID = Sampling.Disadvantages;//缺点等级
                                DetailsModel.Status = Framework.SystemID + "0201213000001";
                                DetailsModel.Aql = Sta.AQL;
                                DetailsModel.AttributeType = Sta.Attribute;
                                //当没有AQL值默认为0
                                if (Sta.AQL == null)
                                {
                                    DetailsModel.SampleQuantity = 0;
                                    DetailsModel.AcQuantity = 0;
                                    DetailsModel.ReQuantity = 0;
                                }
                                //当QCS000001对应的数据没有时，AC,RE,抽样数量设置为0
                                else
                                {
                                    QCS_CheckTestSetting Check = QCS_CheckTestSettingService.getByAql(DetailsModel.InspectionLevelID, DetailsModel.InspectionMethodID, Sta.AQL);//获取抽检检验设定实体
                                    if (Check == null)
                                    {
                                        nosuccess++;
                                        reason += "部分新增失败，QCS000001没有与检验方式，检验水平，AQL对应的数据，AC,RE，抽样数量将默认为0";
                                        DetailsModel.SampleQuantity = 0;
                                        DetailsModel.AcQuantity = 0;
                                        DetailsModel.ReQuantity = 0;
                                    }
                                    else
                                    {
                                        //当QCS000001对应的明细数据没有时，AC,RE,抽样数量设置为0
                                        QCS_CheckTestSettingDetails CheckDetail = QCS_CheckTestSettingDetailsService.getCTSDetails(Check.CheckTestSettingID);//获取抽检检验设定明细实体
                                        if (Check == null)
                                        {
                                            nosuccess++;
                                            reason += "有部分QCS000001明细没有与检验方式，检验水平，AQL对应的明细数据，AC,RE，抽样数量将默认为0";
                                            DetailsModel.SampleQuantity = 0;
                                            DetailsModel.AcQuantity = 0;
                                            DetailsModel.ReQuantity = 0;
                                        }
                                        //decimal p = model.InspectionQuantity / CheckDetail.EndBatch;//检验数量自动计算？检验数量除以结束批量？
                                        //DetailsModel.SampleQuantity = p * CheckDetail.SamplingQuantity;
                                        //DetailsModel.AcQuantity = p * CheckDetail.AcQuantity;
                                        //DetailsModel.ReQuantity = p * CheckDetail.ReQuantity;
                                        //不确定P的计算方式，暂时注释掉
                                        else
                                        {
                                            DetailsModel.SampleQuantity = CheckDetail.SamplingQuantity;
                                            DetailsModel.AcQuantity = CheckDetail.AcQuantity;
                                            DetailsModel.ReQuantity = CheckDetail.ReQuantity;
                                        }
                                    }
                                }
                                DetailsModel.NGquantity = 0;//不良数？

                                if (model2.Value == Framework.SystemID + "02012130000B3")//查看系统设置的判定方式
                                {
                                    if (DetailsModel.NGquantity >= DetailsModel.ReQuantity)
                                        DetailsModel.QualityControlDecision = Framework.SystemID + "0201213000090";
                                    else
                                        DetailsModel.QualityControlDecision = Framework.SystemID + "0201213000091";
                                }
                                else
                                {
                                    DetailsModel.QualityControlDecision = "人工判定";
                                }
                                if (model3.Value == Framework.SystemID + "02012130000B1" && DetailsModel.QualityControlDecision == Framework.SystemID + "0201213000090")
                                {//品质判定为自动且新增的明细判定为拒收
                                    model.QualityControlDecision = Framework.SystemID + "0201213000090";//假如系统判定方式为自动，明细有一个为拒收，那么表头也为拒收

                                    QCS_InspectionDocumentService.update(userId, model);//更新表头数据
                                }
                                QCS_InspectionDocumentDetailsService.insert(userId, DetailsModel);

                            }
                            //明细新增结束
                            IList<Hashtable> TypeList = SYS_DocumentTypeSettingService.GetTypeList(userId, SysID + "020121300003D");
                            if (TypeList.Count != 0)
                            {
                                model.InspectionNo = UtilBussinessService.GetDocumentAutoNumber(userId, TypeList[0]["value"].ToString(), model.DocumentDate.ToString(), ref AutoNumberID);
                            }
                            else
                            {
                                reason += "序号[" + Sequence + "]:当前用户并没有自动编号权限;";
                                nosuccess++;
                                continue; //结束本次循环
                            }
                            if (QCS_InspectionDocumentService.insert(userId, model))
                            {
                                UtilBussinessService.UpdateDocumentAutoNumber(userId, AutoNumberID);
                                success++;
                            }
                            else
                            {
                                reason += "序号[" + Sequence + "]:添加失败，未知原因;";
                                nosuccess++;
                            }
                        }


                    }
                }
            }
            if (nosuccess == total)
                status = "410";

            msg = string.Format(msg, total, success, nosuccess, reason);

            return new { status = status, msg = msg };
        }

        /// <summary>
        /// 设备保养工单的导入
        /// SAM 2017年8月4日00:08:02
        /// </summary>
        /// <param name="token"></param>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static object Ems00009Import(string token, string filename)
        {
            string userId = UtilBussinessService.detoken(token);
            string SysID = Framework.SystemID;
            string status = "200", msg = "共{0}条信息,{1}条信息导入成功,{2}条信息导入失败！{3}";
            string reason = null;
            int total = 0;
            int success = 0;
            int nosuccess = 0;
            if (filename.EndsWith(".xls") || filename.EndsWith(".xlsx"))
            {
                IWorkbook wk = null;
                string Sequence = null;
                EMS_MaintenanceOrder model = null;

                SYS_Parameters ParModel = null;
                using (FileStream fs = File.OpenRead(@filename))   //打开.xls文件
                {
                    if (filename.EndsWith(".xls"))
                        wk = new HSSFWorkbook(fs); //把xls文件中的数据写入wk中
                    else if (filename.EndsWith(".xlsx"))
                        wk = new XSSFWorkbook(fs);//把xlsx文件中的数据写入wk中

                    ISheet sheet = wk.GetSheetAt(0);   //读取第一表数据

                    IRow row = sheet.GetRow(2);  //读取表头部分数据
                    if (row.GetCell(0) == null) //如果当前行的第一列没有数据，停止读取
                        return null;
                    if (string.IsNullOrWhiteSpace(row.GetCell(0).ToString()))//如果当前行的第一列没有数据，停止读取，这是判断当row.GetCell不能null然后实际是空字符
                        return null;
                    ICell cell = null;
                    model = new EMS_MaintenanceOrder();

                    cell = row.GetCell(0);//序号
                    Sequence = cell.ToString();

                    cell = row.GetCell(1);//单据日期
                    if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                    {
                        try
                        {
                            model.Date = DateTime.Parse(cell.ToString());
                        }
                        catch
                        {
                            return new { status = "410", msg = "导入失败，表头单据日期格式错误！" };
                        }
                    }
                    else
                        return new { status = "410", msg = "导入失败，表头单据日期为空！" };


                    cell = row.GetCell(2);//保养日期
                    if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                    {
                        try
                        {
                            model.MaintenanceDate = DateTime.Parse(cell.ToString());
                        }
                        catch
                        {
                            return new { status = "410", msg = "导入失败，表头单据日期格式错误！" };
                        }
                    }
                    else
                        return new { status = "410", msg = "导入失败，表头单据日期为空！" };

                    cell = row.GetCell(3);//保养类型
                    if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                    {
                        ParModel = SYS_ParameterService.getByName(cell.ToString(), SysID + "0191213000023");
                        if (ParModel != null)
                            model.Type = ParModel.ParameterID;
                        else
                            return new { status = "410", msg = "导入失败，不存在的保养类型！" };
                    }
                    else
                        return new { status = "410", msg = "导入失败，保养类型为空！" };

                    cell = row.GetCell(4);//保养清单
                    if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                    {
                        EMS_EquipmentMaintenanceList List = EMS_EquipmentMaintenanceListService.getByCode(cell.ToString());
                        if (List != null)
                            model.EquipmentMaintenanceListID = List.EquipmentMaintenanceListID;
                        else
                            return new { status = "410", msg = "导入失败，不存在的保养清单！" };
                    }
                    else
                        return new { status = "410", msg = "导入失败，保养清单为空！" };

                    cell = row.GetCell(5);//保养厂商
                    if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                    {
                        SYS_Manufacturers Man = SYS_ManufacturersService.getByCode(cell.ToString());
                        if (Man != null)
                            model.ManufacturerID = Man.ManufacturerID;
                    }

                    cell = row.GetCell(6);//负责人工号
                    if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                    {
                        SYS_MESUsers User = SYS_MESUserService.getByCode(cell.ToString());
                        if (User != null)
                            model.MESUserID = User.MESUserID;
                    }

                    cell = row.GetCell(7);//备注
                    if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                    {
                        if (cell.ToString().Length <= 120)
                            model.Comments = cell.ToString();
                    }

                    cell = row.GetCell(8);//状态
                    if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                    {
                        ParModel = SYS_ParameterService.getByName(cell.ToString(), SysID + "0191213000004");
                        if (ParModel != null)
                            model.Status = ParModel.ParameterID;
                        else
                            model.Status = SysID + "0201213000028";
                    }
                    else
                        model.Status = SysID + "0201213000028";

                    string AutoNumberID = null;
                    model.MaintenanceOrderID = UniversalService.GetSerialNumber("EMS_MaintenanceOrder");
                    IList<Hashtable> TypeList = SYS_DocumentTypeSettingService.GetTypeList(userId, SysID + "0201213000039");
                    if (TypeList.Count != 0)
                    {
                        string AutoNumber = UtilBussinessService.GetDocumentAutoNumber(userId, TypeList[0]["value"].ToString(), model.Date.ToString(), ref AutoNumberID);
                        if (!string.IsNullOrWhiteSpace(AutoNumber))
                            model.MaintenanceNo = AutoNumber;
                        else
                            return new { status = "410", msg = "导入失败，当前登录用户并无单据权限！" };
                    }
                    else
                        return new { status = "410", msg = "导入失败，当前登录用户并无单据权限！" };

                    if (EMS_MaintenanceOrderService.insert(userId, model))//表头部分导入成功，开始导入明细部分
                    {
                        UtilBussinessService.UpdateDocumentAutoNumber(userId, AutoNumberID);
                        EMS_Equipment EquModel = null;
                        EMS_MaiOrderEquipment Detailmodel = null;
                        for (int j = 4; j <= sheet.LastRowNum; j++)  //LastRowNum 是当前表的总行数
                        {
                            row = sheet.GetRow(j);  //读取当前行数据
                            if (row.GetCell(0) == null) //如果当前行的第一列没有数据，停止读取
                                break;
                            if (string.IsNullOrWhiteSpace(row.GetCell(0).ToString()))//如果当前行的第一列没有数据，停止读取，这是判断当row.GetCell不能null然后实际是空字符
                                break;
                            total++;
                            cell = null;
                            if (row != null)
                            {
                                Detailmodel = new EMS_MaiOrderEquipment();

                                cell = row.GetCell(0);//序号
                                Sequence = cell.ToString();

                                cell = row.GetCell(1);//设备代号
                                if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                                {
                                    EquModel = EMS_EquipmentService.getByCode(cell.ToString());
                                    if (EquModel == null)
                                        reason += "序号[" + Sequence + "]:不存在此设备代号[" + cell.ToString() + "];";
                                    else
                                        Detailmodel.EquipmentID = EquModel.EquipmentID;
                                }
                                else
                                {
                                    reason += "序号[" + Sequence + "]:设备代号为空;";
                                }
                            }
                            if (!EMS_MaiOrderEquipmentService.CheckEquipment(Detailmodel.EquipmentID, model.MaintenanceOrderID))
                            {
                                Detailmodel.MaiOrderEquipmentID = UniversalService.GetSerialNumber("EMS_MaiOrderEquipment");
                                Detailmodel.MaintenanceOrderID = model.MaintenanceOrderID;
                                Detailmodel.Sequence = int.Parse(Sequence);
                                //Detailmodel.StartDate = null;
                                //Detailmodel.EndDate = null;
                                Detailmodel.Status = Framework.SystemID + "0201213000001";
                                if (EMS_MaiOrderEquipmentService.insert(userId, Detailmodel))
                                    success++;
                                else
                                {
                                    reason += "序号[" + Sequence + "]:添加失败，未知原因;";
                                    nosuccess++;
                                }
                            }
                            else
                            {
                                reason += "序号[" + Sequence + "]:工单已存在设备设置;";
                                nosuccess++;
                            }
                        }
                    }
                    else
                    {
                        reason += "表头部分导入失败！;";
                    }
                }
            }
            if (nosuccess == total)
                status = "410";

            msg = string.Format(msg, total, success, nosuccess, reason);

            return new { status = status, msg = msg };
        }

        /// <summary>
        /// 設備保養資料維護导入
        /// Joint 2017年8月1日18:17:02
        /// 
        /// SAM 2017年8月28日23:38:32
        /// 完善了相关的导入设定
        /// </summary>
        /// <param name="token"></param>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static object Ems00010Import(string token, string filename)
        {
            string userId = UtilBussinessService.detoken(token);
            string SysID = Framework.SystemID;
            string status = "200", msg = "共{0}条信息,{1}条信息导入成功,{2}条信息导入失败！{3}";
            string reason = null;
            int total = 0;
            int success = 0;
            int nosuccess = 0;
            if (filename.EndsWith(".xls") || filename.EndsWith(".xlsx"))
            {
                IWorkbook wk = null;
                string Sequence = null;
                EMS_MaiOrderProject model = null;
                using (FileStream fs = File.OpenRead(@filename))   //打开.xls文件
                {
                    if (filename.EndsWith(".xls"))
                        wk = new HSSFWorkbook(fs); //把xls文件中的数据写入wk中
                    else if (filename.EndsWith(".xlsx"))
                        wk = new XSSFWorkbook(fs);//把xlsx文件中的数据写入wk中

                    ISheet sheet = wk.GetSheetAt(0);   //读取第一表数据
                    for (int j = 2; j <= sheet.LastRowNum; j++)  //LastRowNum 是当前表的总行数
                    {
                        IRow row = sheet.GetRow(j);  //读取当前行数据
                        if (row.GetCell(0) == null) //如果当前行的第一列没有数据，停止读取
                            break;
                        if (string.IsNullOrWhiteSpace(row.GetCell(0).ToString()))//如果当前行的第一列没有数据，停止读取，这是判断当row.GetCell不能null然后实际是空字符
                            break;
                        total++;
                        ICell cell = null;
                        if (row != null)
                        {
                            model = new EMS_MaiOrderProject();
                            cell = row.GetCell(0);//序号
                            Sequence = cell.ToString();

                            cell = row.GetCell(1);//保养单号
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                EMS_MaintenanceOrder Order = EMS_MaintenanceOrderService.GetByCode(cell.ToString());

                                if (Order == null)
                                {
                                    reason += "序号[" + Sequence + "]:保养单号不存在;";
                                    nosuccess++;
                                    continue; //结束本次循环

                                }
                                model.MaintenanceOrderID = Order.MaintenanceOrderID;
                            }
                            else
                            {
                                reason += "序号[" + Sequence + "]:保养单号不能为空;";
                                nosuccess++;
                                continue; //结束本次循环
                            }

                            cell = row.GetCell(2);//设备代号
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                EMS_Equipment Equipment = EMS_EquipmentService.getByCode(cell.ToString());
                                if (Equipment == null)
                                {
                                    reason += "序号[" + Sequence + "]:设备不存在;";
                                    nosuccess++;
                                    continue; //结束本次循环
                                }
                                EMS_MaiOrderEquipment OrderEquipment = EMS_MaiOrderEquipmentService.GetEquipment(Equipment.EquipmentID, model.MaintenanceOrderID);
                                if (OrderEquipment == null)
                                {
                                    //model.MaiOrderEquipmentID = UniversalService.GetSerialNumber("EMS_MaiOrderEquipment");
                                    reason += "序号[" + Sequence + "]:对应的清单并没有对应设备的设定;";
                                    nosuccess++;
                                    continue; //结束本次循环
                                }
                                else
                                    model.MaiOrderEquipmentID = OrderEquipment.MaiOrderEquipmentID;
                            }
                            else
                            {
                                reason += "序号[" + Sequence + "]:设备代号不能为空;";
                                nosuccess++;
                                continue; //结束本次循环
                            }

                            cell = row.GetCell(3);//保养项目
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                SYS_Parameters Parameter = SYS_ParameterService.getByCode(cell.ToString(), Framework.SystemID + "0191213000022");
                                if (Parameter == null)
                                {
                                    reason += "序号[" + Sequence + "]:保养项目不存在;";
                                    nosuccess++;
                                    continue; //结束本次循环
                                }
                                model.MaiProjectID = Parameter.ParameterID;
                            }
                            else
                            {
                                reason += "序号[" + Sequence + "]:保养项目不能为空;";
                                nosuccess++;
                                continue; //结束本次循环
                            }

                            cell = row.GetCell(4);//实际值
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                try
                                {
                                    model.AttributeValue = cell.ToString();
                                }
                                catch
                                {
                                    reason += "序号[" + Sequence + "]:实际值格式错误;";
                                    nosuccess++;
                                    continue; //结束本次循环
                                }
                            }
                            else
                            {
                                reason += "序号[" + Sequence + "]:保养项目不能为空;";
                                nosuccess++;
                                continue; //结束本次循环
                            }

                            EMS_MaiOrderProject Project = EMS_MaiOrderProjectService.GetMaiProject(model.MaiOrderEquipmentID, model.MaiProjectID);
                            if (Project != null)
                            {
                                reason += "序号[" + Sequence + "]:数据已存在;";
                                nosuccess++;
                                continue; //结束本次循环
                            }
                            model.MaiOrderProjectID = UniversalService.GetSerialNumber("EMS_MaiOrderProject");
                            model.Status = Framework.SystemID + "0201213000001";
                            if (EMS_MaiOrderProjectService.insert(userId, model))
                                success++;
                            else
                            {
                                reason += "序号[" + Sequence + "]:添加失败，未知原因;";
                                nosuccess++;
                            }
                        }
                    }
                }
            }
            if (nosuccess == total)
                status = "410";

            msg = string.Format(msg, total, success, nosuccess, reason);

            return new { status = status, msg = msg };

        }



        /// <summary>
        /// 完工单的导入
        /// SAM 2017年8月1日15:14:40
        /// </summary>
        /// <param name="token"></param>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static object Sfc00007Import(string token, string filename)
        {
            string userId = UtilBussinessService.detoken(token);
            string SysID = Framework.SystemID;
            string status = "200", msg = "共{0}条信息,{1}条信息导入成功,{2}条信息导入失败！{3}";
            string reason = null;
            int total = 0;
            int success = 0;
            int nosuccess = 0;
            if (filename.EndsWith(".xls") || filename.EndsWith(".xlsx"))
            {
                IWorkbook wk = null;
                string Sequence = null;
                SFC_CompletionOrder model = null;
                string AutoNumberID = null;
                SFC_TaskDispatch Taskmodel = null;
                using (FileStream fs = File.OpenRead(@filename))   //打开.xls文件
                {
                    if (filename.EndsWith(".xls"))
                        wk = new HSSFWorkbook(fs); //把xls文件中的数据写入wk中
                    else if (filename.EndsWith(".xlsx"))
                        wk = new XSSFWorkbook(fs);//把xlsx文件中的数据写入wk中

                    ISheet sheet = wk.GetSheetAt(0);   //读取第一表数据
                    for (int j = 2; j <= sheet.LastRowNum; j++)  //LastRowNum 是当前表的总行数
                    {
                        IRow row = sheet.GetRow(j);  //读取当前行数据
                        if (row.GetCell(0) == null) //如果当前行的第一列没有数据，停止读取
                            break;
                        if (string.IsNullOrWhiteSpace(row.GetCell(0).ToString()))//如果当前行的第一列没有数据，停止读取，这是判断当row.GetCell不能null然后实际是空字符
                            break;
                        total++;
                        ICell cell = null;
                        if (row != null)
                        {
                            model = new SFC_CompletionOrder();
                            AutoNumberID = null;
                            cell = row.GetCell(0);//序号
                            Sequence = cell.ToString();

                            cell = row.GetCell(1);//完工日期
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                try
                                {
                                    model.Date = DateTime.Parse(cell.ToString());
                                }
                                catch
                                {
                                    reason += "序号[" + Sequence + "]:完工日期格式错误;";
                                    nosuccess++;
                                    continue; //结束本次循环
                                }
                            }
                            else
                            {
                                reason += "序号[" + Sequence + "]:完工日期不能为空;";
                                nosuccess++;
                                continue; //结束本次循环
                            }


                            cell = row.GetCell(2);//任务卡号
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                Taskmodel = SFC_TaskDispatchService.getByCode(cell.ToString());
                                if (Taskmodel == null)
                                {
                                    reason += "序号[" + Sequence + "]:任务卡号错误，不存在的任务卡号;";
                                    nosuccess++;
                                    continue; //结束本次循环
                                }

                                if (Taskmodel.Status == SysID + "020121300008B" || Taskmodel.Status == SysID + "020121300008C")
                                {
                                    reason += "序号[" + Sequence + "]:此任务卡处于CL或者CA状态;";
                                    nosuccess++;
                                    continue; //结束本次循环
                                }

                                model.FabricatedMotherID = Taskmodel.TaskDispatchID;
                                model.FabricatedMotherID = Taskmodel.FabricatedMotherID;
                                model.FabMoProcessID = Taskmodel.FabMoProcessID;
                                model.FabMoOperationID = Taskmodel.FabMoOperationID;
                                model.ItemID = Taskmodel.ItemID;
                                model.ProcessID = Taskmodel.ProcessID;
                                model.OperationID = Taskmodel.OperationID;
                                model.IsIF = Taskmodel.IsFPI;
                            }
                            else
                            {
                                reason += "序号[" + Sequence + "]:任务卡号不能为空;";
                                nosuccess++;
                                continue; //结束本次循环
                            }
                        }


                        cell = row.GetCell(3);//完工数量
                        if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                        {
                            try
                            {
                                model.FinProQuantity = decimal.Parse(cell.ToString());
                            }
                            catch
                            {
                                model.FinProQuantity = 0;
                                reason += "序号[" + Sequence + "]:完工数量格式错误;";
                            }
                        }

                        cell = row.GetCell(4);//报废量
                        if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                        {
                            try
                            {
                                model.ScrappedQuantity = decimal.Parse(cell.ToString());
                            }
                            catch
                            {
                                model.ScrappedQuantity = 0;
                                reason += "序号[" + Sequence + "]:返修量格式错误;";
                            }
                        }

                        cell = row.GetCell(5);//差异量
                        if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                        {
                            try
                            {
                                model.DifferenceQuantity = decimal.Parse(cell.ToString());
                            }
                            catch
                            {
                                model.DifferenceQuantity = 0;
                                reason += "序号[" + Sequence + "]:返修量格式错误;";
                            }
                        }

                        cell = row.GetCell(6);//返修量
                        if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                        {
                            try
                            {
                                model.RepairQuantity = decimal.Parse(cell.ToString());
                            }
                            catch
                            {
                                model.RepairQuantity = 0;
                                reason += "序号[" + Sequence + "]:返修量格式错误;";
                            }
                        }

                        cell = row.GetCell(7);//有效人工工时
                        if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                        {
                            model.LaborHour = UtilBussinessService.StrConversionHour(cell.ToString());
                            if (model.LaborHour == -1)
                            {
                                model.LaborHour = 0;
                                reason += "序号[" + Sequence + "]:有效人工工时格式错误;";
                            }
                        }

                        cell = row.GetCell(8);//无效人工工时
                        if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                        {
                            model.UnLaborHour = UtilBussinessService.StrConversionHour(cell.ToString());
                            if (model.UnLaborHour == -1)
                            {
                                model.UnLaborHour = 0;
                                reason += "序号[" + Sequence + "]:有效人工工时格式错误;";
                            }
                        }

                        cell = row.GetCell(9);//机器有效工时
                        if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                        {
                            model.MachineHour = UtilBussinessService.StrConversionHour(cell.ToString());
                            if (model.MachineHour == -1)
                            {
                                model.MachineHour = 0;
                                reason += "序号[" + Sequence + "]:有效人工工时格式错误;";
                            }
                        }

                        cell = row.GetCell(10);//无效机器工时
                        if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                        {
                            model.UnMachineHour = UtilBussinessService.StrConversionHour(cell.ToString());
                            if (model.UnMachineHour == -1)
                            {
                                model.UnMachineHour = 0;
                                reason += "序号[" + Sequence + "]:有效人工工时格式错误;";
                            }
                        }


                        if (model.FinProQuantity + model.DifferenceQuantity + model.ScrappedQuantity + model.RepairQuantity == 0 &&
                            model.LaborHour + model.MachineHour + model.UnLaborHour + model.UnMachineHour == 0)
                        {
                            reason += "序号[" + Sequence + "]:加总数量和加总工时不能同时为零;";
                            nosuccess++;
                            continue; //结束本次循环
                        }


                        if (model.ScrappedQuantity > model.FinProQuantity)
                        {
                            reason += "序号[" + Sequence + "]:报废量大于完工量！";
                            nosuccess++;
                            continue; //结束本次循环
                        }

                        if (model.FinProQuantity + Taskmodel.FinishQuantity > Taskmodel.DispatchQuantity)
                        {
                            reason += "序号[" + Sequence + "]:累計報工量(數量)> 任務單分派量！";
                            nosuccess++;
                            continue; //结束本次循环
                        }

                        if (model.FinProQuantity + Taskmodel.FinishQuantity < 0)
                        {
                            reason += "序号[" + Sequence + "]:累計報工量(數量) < 0！";
                            nosuccess++;
                            continue; //结束本次循环
                        }

                        model.CompletionOrderID = UniversalService.GetSerialNumber("SFC_CompletionOrder");
                        IList<Hashtable> TypeList = SYS_DocumentTypeSettingService.GetTypeList(userId, SysID + "0201213000036");
                        if (TypeList.Count != 0)
                        {
                            model.CompletionNo = UtilBussinessService.GetDocumentAutoNumber(userId, TypeList[0]["value"].ToString(), model.Date.ToString(), ref AutoNumberID);
                        }
                        else
                        {
                            reason += "序号[" + Sequence + "]:当前用户并没有自动编号权限;";
                            nosuccess++;
                            continue; //结束本次循环
                        }
                        model.DTSID = TypeList[0]["value"].ToString();
                        model.Status = Framework.SystemID + "0201213000029";
                        model.Type = Framework.SystemID + "02012130000A0";

                        if (SFC_CompletionOrderService.insert(userId, model))
                        {
                            UtilBussinessService.UpdateDocumentAutoNumber(userId, AutoNumberID);
                            success++;
                        }
                        else
                        {
                            reason += "序号[" + Sequence + "]:添加失败，未知原因;";
                            nosuccess++;
                        }
                    }

                }
            }
            if (nosuccess == total)
                status = "410";

            msg = string.Format(msg, total, success, nosuccess, reason);

            return new { status = status, msg = msg };
        }

        /// <summary>
        /// 调整单的导入
        /// SAM 2017年8月1日16:19:52
        /// </summary>
        /// <param name="token"></param>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static object Sfc00008Import(string token, string filename)
        {
            string userId = UtilBussinessService.detoken(token);
            string SysID = Framework.SystemID;
            string status = "200", msg = "共{0}条信息,{1}条信息导入成功,{2}条信息导入失败！{3}";
            string reason = null;
            int total = 0;
            int success = 0;
            int nosuccess = 0;
            if (filename.EndsWith(".xls") || filename.EndsWith(".xlsx"))
            {
                IWorkbook wk = null;
                string Sequence = null;
                SFC_CompletionOrder model = null;
                string AutoNumberID = null;
                SFC_TaskDispatch Taskmodel = null;
                SFC_CompletionOrder Originalmodel = null;
                using (FileStream fs = File.OpenRead(@filename))   //打开.xls文件
                {
                    if (filename.EndsWith(".xls"))
                        wk = new HSSFWorkbook(fs); //把xls文件中的数据写入wk中
                    else if (filename.EndsWith(".xlsx"))
                        wk = new XSSFWorkbook(fs);//把xlsx文件中的数据写入wk中

                    ISheet sheet = wk.GetSheetAt(0);   //读取第一表数据
                    for (int j = 2; j <= sheet.LastRowNum; j++)  //LastRowNum 是当前表的总行数
                    {
                        IRow row = sheet.GetRow(j);  //读取当前行数据
                        if (row.GetCell(0) == null) //如果当前行的第一列没有数据，停止读取
                            break;
                        if (string.IsNullOrWhiteSpace(row.GetCell(0).ToString()))//如果当前行的第一列没有数据，停止读取，这是判断当row.GetCell不能null然后实际是空字符
                            break;
                        total++;
                        ICell cell = null;
                        if (row != null)
                        {
                            model = new SFC_CompletionOrder();
                            AutoNumberID = null;
                            cell = row.GetCell(0);//序号
                            Sequence = cell.ToString();

                            cell = row.GetCell(1);//完工日期
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                try
                                {
                                    model.Date = DateTime.Parse(cell.DateCellValue.ToString());
                                }
                                catch
                                {
                                    reason += "序号[" + Sequence + "]:完工日期格式错误;";
                                    nosuccess++;
                                    continue; //结束本次循环
                                }
                            }
                            else
                            {
                                reason += "序号[" + Sequence + "]:完工日期不能为空;";
                                nosuccess++;
                                continue; //结束本次循环
                            }


                            cell = row.GetCell(2);//原完工单号
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                Originalmodel = SFC_CompletionOrderService.getByCode(cell.ToString());
                                if (Originalmodel == null)
                                {
                                    reason += "序号[" + Sequence + "]:原完工单号错误，不存在的完工单号;";
                                    nosuccess++;
                                    continue; //结束本次循环
                                }

                                if (Originalmodel.Status != SysID + "020121300002A")
                                {
                                    reason += "序号[" + Sequence + "]:此原完工单号非CL状态;";
                                    nosuccess++;
                                    continue; //结束本次循环
                                }
                                if (Originalmodel.Type != SysID + "02012130000A0")
                                {
                                    reason += "序号[" + Sequence + "]:此原完工单号非正常完工的完工单;";
                                    nosuccess++;
                                    continue; //结束本次循环
                                }
                                Taskmodel = SFC_TaskDispatchService.get(Originalmodel.TaskDispatchID);
                                model.OriginalCompletionOrderID = Originalmodel.CompletionOrderID;
                                model.TaskDispatchID = Taskmodel.TaskDispatchID;
                                model.FabricatedMotherID = Taskmodel.FabricatedMotherID;
                                model.FabMoProcessID = Taskmodel.FabMoProcessID;
                                model.FabMoOperationID = Taskmodel.FabMoOperationID;
                                model.ItemID = Taskmodel.ItemID;
                                model.ProcessID = Taskmodel.ProcessID;
                                model.OperationID = Taskmodel.OperationID;
                                model.IsIF = Taskmodel.IsFPI;
                            }
                            else
                            {
                                reason += "序号[" + Sequence + "]:任务卡号不能为空;";
                                nosuccess++;
                                continue; //结束本次循环
                            }
                        }


                        cell = row.GetCell(3);//完工数量
                        if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                        {
                            try
                            {
                                model.FinProQuantity = decimal.Parse(cell.ToString());
                            }
                            catch
                            {
                                model.FinProQuantity = 0;
                                reason += "序号[" + Sequence + "]:完工数量格式错误;";
                            }
                        }

                        cell = row.GetCell(4);//报废量
                        if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                        {
                            try
                            {
                                model.ScrappedQuantity = decimal.Parse(cell.ToString());
                            }
                            catch
                            {
                                model.ScrappedQuantity = 0;
                                reason += "序号[" + Sequence + "]:返修量格式错误;";
                            }
                        }

                        cell = row.GetCell(5);//差异量
                        if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                        {
                            try
                            {
                                model.DifferenceQuantity = decimal.Parse(cell.ToString());
                            }
                            catch
                            {
                                model.DifferenceQuantity = 0;
                                reason += "序号[" + Sequence + "]:返修量格式错误;";
                            }
                        }

                        cell = row.GetCell(6);//返修量
                        if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                        {
                            try
                            {
                                model.RepairQuantity = decimal.Parse(cell.ToString());
                            }
                            catch
                            {
                                model.RepairQuantity = 0;
                                reason += "序号[" + Sequence + "]:返修量格式错误;";
                            }
                        }

                        cell = row.GetCell(7);//有效人工工时
                        if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                        {
                            try
                            {
                                model.LaborHour = UtilBussinessService.StrConversionHour(cell.DateCellValue.ToString().Split(' ')[1]);
                                if (model.LaborHour == null)
                                {
                                    model.LaborHour = 0;
                                    reason += "序号[" + Sequence + "]:有效人工工时格式错误;";
                                }
                            }
                            catch
                            {
                                reason += "序号[" + Sequence + "]:有效人工工时格式错误;";
                            }
                        }

                        cell = row.GetCell(8);//无效人工工时
                        if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                        {
                            try
                            {
                                model.UnLaborHour = UtilBussinessService.StrConversionHour(cell.DateCellValue.ToString().Split(' ')[1]);
                                if (model.UnLaborHour == null)
                                {
                                    model.UnLaborHour = 0;
                                    reason += "序号[" + Sequence + "]:无效人工工时格式错误;";
                                }
                            }
                            catch
                            {
                                reason += "序号[" + Sequence + "]:无效人工工时格式错误;";
                            }
                        }

                        cell = row.GetCell(9);//机器有效工时
                        if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                        {
                            try
                            {
                                model.MachineHour = UtilBussinessService.StrConversionHour(cell.DateCellValue.ToString().Split(' ')[1]);
                                if (model.MachineHour == null)
                                {
                                    model.MachineHour = 0;
                                    reason += "序号[" + Sequence + "]:机器有效工时格式错误;";
                                }
                            }
                            catch
                            {
                                reason += "序号[" + Sequence + "]:机器有效工时格式错误;";
                            }
                        }

                        cell = row.GetCell(10);//无效机器工时
                        if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                        {
                            try
                            {
                                model.UnMachineHour = UtilBussinessService.StrConversionHour(cell.DateCellValue.ToString().Split(' ')[1]);
                                if (model.UnMachineHour == null)
                                {
                                    model.UnMachineHour = 0;
                                    reason += "序号[" + Sequence + "]:无效机器工时格式错误;";
                                }
                            }
                            catch
                            {
                                reason += "序号[" + Sequence + "]:无效机器工时格式错误;";
                            }
                        }

                        cell = row.GetCell(11);//备注
                        if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                        {
                            if (cell.ToString().Length <= 120)
                                model.Comments = cell.ToString();
                            else
                                reason += "序号[" + Sequence + "]:备注超过了120个字;";
                        }


                        if (model.FinProQuantity + model.DifferenceQuantity + model.ScrappedQuantity + model.RepairQuantity == 0 &&
                            model.LaborHour + model.MachineHour + model.UnLaborHour + model.UnMachineHour == 0)
                        {
                            reason += "序号[" + Sequence + "]:加总数量和加总工时不能同时为零;";
                            nosuccess++;
                            continue; //结束本次循环
                        }


                        if (model.ScrappedQuantity > model.FinProQuantity)
                        {
                            reason += "序号[" + Sequence + "]:报废量大于完工量！";
                            nosuccess++;
                            continue; //结束本次循环
                        }

                        if (model.FinProQuantity + Taskmodel.FinishQuantity > Taskmodel.DispatchQuantity)
                        {
                            reason += "序号[" + Sequence + "]:累計報工量(數量)> 任務單分派量！";
                            nosuccess++;
                            continue; //结束本次循环
                        }

                        if (model.FinProQuantity + Taskmodel.FinishQuantity < 0)
                        {
                            reason += "序号[" + Sequence + "]:累計報工量(數量) < 0！";
                            nosuccess++;
                            continue; //结束本次循环
                        }

                        model.CompletionOrderID = UniversalService.GetSerialNumber("SFC_CompletionOrder");
                        IList<Hashtable> TypeList = SYS_DocumentTypeSettingService.GetTypeList(userId, SysID + "02012130000AA");
                        if (TypeList.Count != 0)
                        {
                            model.CompletionNo = UtilBussinessService.GetDocumentAutoNumber(userId, TypeList[0]["value"].ToString(), model.Date.ToString(), ref AutoNumberID);
                        }
                        else
                        {
                            reason += "序号[" + Sequence + "]:当前用户并没有自动编号权限;";
                            nosuccess++;
                            continue; //结束本次循环
                        }
                        model.DTSID = TypeList[0]["value"].ToString();
                        model.Status = Framework.SystemID + "0201213000029";
                        model.Type = Framework.SystemID + "02012130000A1";

                        if (SFC_CompletionOrderService.insert(userId, model))
                        {
                            UtilBussinessService.UpdateDocumentAutoNumber(userId, AutoNumberID);
                            success++;
                        }
                        else
                        {
                            reason += "序号[" + Sequence + "]:添加失败，未知原因;";
                            nosuccess++;
                        }
                    }
                }
            }
            if (nosuccess == total)
                status = "410";

            msg = string.Format(msg, total, success, nosuccess, reason);

            return new { status = status, msg = msg };
        }

        /// <summary>
        /// 制品制程的导入
        /// SAM 2017年9月5日09:59:16
        /// </summary>
        /// <param name="token"></param>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static object Sfc00001Import(string token, string filename)
        {
            string userId = UtilBussinessService.detoken(token);
            string SysID = Framework.SystemID;
            string status = "200", msg = "共{0}条信息,{1}条信息导入成功,{2}条信息导入失败！{3}";
            string reason = null;
            string Allmsg = null;
            List<Hashtable> resultList = new List<Hashtable>();
            Hashtable result = null;
            int total = 0;
            int success = 0;
            int nosuccess = 0;
            if (filename.EndsWith(".xls") || filename.EndsWith(".xlsx"))
            {
                IWorkbook wk = null;
                using (FileStream fs = File.OpenRead(@filename))   //打开.xls文件
                {
                    if (filename.EndsWith(".xls"))
                        wk = new HSSFWorkbook(fs); //把xls文件中的数据写入wk中
                    else if (filename.EndsWith(".xlsx"))
                        wk = new XSSFWorkbook(fs);//把xlsx文件中的数据写入wk中

                    //制品制程
                    SFC_ItemProcess model = null;
                    SYS_Items Itemmodel = null;
                    SYS_Parameters Parmodel = null;
                    SYS_WorkCenter WCmodel = null;
                    try
                    {
                        ISheet sheet = wk.GetSheetAt(0);   //读取第一表数据
                        for (int j = 2; j <= sheet.LastRowNum; j++)  //LastRowNum 是当前表的总行数
                        {
                            IRow row = sheet.GetRow(j);  //读取当前行数据
                            if (row.GetCell(0) == null) //如果当前行的第一列没有数据，停止读取
                                break;
                            //如果当前行的第一列没有数据，停止读取，这是判断当row.GetCell不能null然后实际是空字符
                            if (string.IsNullOrWhiteSpace(row.GetCell(0).ToString()))
                                break;
                            total++;
                            ICell cell = null;
                            if (row != null)
                            {
                                model = new SFC_ItemProcess();
                                //cell = row.GetCell(0);//序号
                                //Sequence = cell.ToString();

                                cell = row.GetCell(0);//料號
                                if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                                {
                                    Itemmodel = SYS_ItemsService.getByCode(cell.ToString());
                                    if (Itemmodel != null)
                                        model.ItemID = Itemmodel.ItemID;
                                    else
                                    {
                                        reason += "第" + (j + 1) + "行:料号错误,不存在的料号;";
                                        nosuccess++;
                                        continue; //结束本次循环
                                    }
                                }
                                else
                                {
                                    reason += "第" + (j + 1) + "行:料号不能为空;";
                                    nosuccess++;
                                    continue; //结束本次循环
                                }


                                cell = row.GetCell(1);//製程序號[4]
                                if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                                {
                                    if (cell.ToString().Length <= 4)
                                        model.Sequence = cell.ToString();
                                    else
                                    {
                                        reason += "第" + (j + 1) + "行:製程序號超出范围限制;";
                                        nosuccess++;
                                        continue; //结束本次循环
                                    }
                                }
                                else
                                {
                                    reason += "第" + (j + 1) + "行:製程序號不能为空;";
                                    nosuccess++;
                                    continue; //结束本次循环
                                }


                                cell = row.GetCell(2);//製程代號
                                if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                                {
                                    Parmodel = SYS_ParameterService.getByCode(cell.ToString(), SysID + "0191213000017");
                                    if (Parmodel != null)
                                        model.ProcessID = Parmodel.ParameterID;
                                    else
                                    {
                                        reason += "第" + (j + 1) + "行:不存在的製程代號;";
                                        nosuccess++;
                                        continue; //结束本次循环
                                    }
                                }
                                else
                                {
                                    reason += "第" + (j + 1) + "行:製程代號不能为空;";
                                    nosuccess++;
                                    continue; //结束本次循环
                                }

                                cell = row.GetCell(3);//工作中心
                                if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                                {
                                    WCmodel = SYS_WorkCenterService.getByCode(cell.ToString());
                                    if (WCmodel != null)
                                        model.WorkCenterID = WCmodel.WorkCenterID;
                                    else
                                        reason += "第" + (j + 1) + "行:不存在的工作中心;";
                                }

                                cell = row.GetCell(4);//備註[120]
                                if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                                {
                                    if (cell.ToString().Length <= 120)
                                        model.Comments = cell.ToString();
                                    else
                                        reason += "第" + (j + 1) + "行:備註字段不能超过120;";
                                }

                                cell = row.GetCell(5);//製程輔助單位[4]
                                if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                                {
                                    Parmodel = SYS_ParameterService.getByCode(cell.ToString(), SysID + "019121300000C");
                                    if (Parmodel != null)
                                        model.AuxUnit = Parmodel.ParameterID;
                                    else
                                        reason += "第" + (j + 1) + "行:不存在的輔助單位;";
                                }

                                cell = row.GetCell(6);//製程輔助單位比[10,4]
                                if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                                {
                                    try
                                    {
                                        model.AuxUnitRatio = decimal.Parse(cell.ToString());
                                    }
                                    catch
                                    {
                                        reason += "第" + (j + 1) + "行:製程輔助單位比格式错误;";
                                    }
                                }


                                cell = row.GetCell(7);//委外單價[10,4]
                                if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                                {
                                    try
                                    {
                                        model.Price = decimal.Parse(cell.ToString());
                                    }
                                    catch
                                    {
                                        reason += "第" + (j + 1) + "行:委外單價格式错误;";
                                    }
                                }


                                cell = row.GetCell(9);//標準工時(秒)[int]
                                if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                                {
                                    try
                                    {
                                        model.StandardTime = int.Parse(cell.ToString());
                                    }
                                    catch
                                    {
                                        reason += "第" + (j + 1) + "行:標準工時格式错误;";
                                    }
                                }

                                cell = row.GetCell(10);//整備工時(秒)[int]
                                if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                                {
                                    try
                                    {
                                        model.PrepareTime = int.Parse(cell.ToString());
                                    }
                                    catch
                                    {
                                        reason += "第" + (j + 1) + "行:整備工時格式错误;";
                                    }
                                }

                                cell = row.GetCell(10);//制程檢驗註記(Y/N)
                                if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                                {
                                    if (cell.ToString() == "Y")
                                        model.IsIP = true;
                                    else if (cell.ToString() == "N")
                                        model.IsIP = false;
                                    else
                                    {
                                        model.IsIP = false;
                                        reason += "第" + (j + 1) + "行:制程檢驗註記格式错误;";
                                    }
                                }
                                else
                                    model.IsIP = false;

                                cell = row.GetCell(11);//首檢驗註記(Y/N)
                                if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                                {
                                    if (cell.ToString() == "Y")
                                        model.IsFPI = true;
                                    else if (cell.ToString() == "N")
                                        model.IsFPI = false;
                                    else
                                    {
                                        model.IsFPI = false;
                                        reason += "第" + (j + 1) + "行:首檢驗註記格式错误;";
                                    }
                                }
                                else
                                    model.IsFPI = false;

                                cell = row.GetCell(12);//巡檢驗註記(Y/N)
                                if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                                {
                                    if (cell.ToString() == "Y")
                                        model.IsOSI = true;
                                    else if (cell.ToString() == "N")
                                        model.IsOSI = false;
                                    else
                                    {
                                        model.IsOSI = false;
                                        reason += "第" + (j + 1) + "行:巡檢驗註記格式错误;";
                                    }
                                }
                                else
                                    model.IsOSI = false;
                            }


                            if (!SFC_ItemProcessService.Check(model.ProcessID, model.ItemID, null))
                            {
                                model.ItemProcessID = UniversalService.GetSerialNumber("SFC_ItemProcess");
                                model.Status = SysID + "0201213000001";
                                if (SFC_ItemProcessService.insert(userId, model))
                                    success++;
                                else
                                {
                                    nosuccess++;
                                    reason += "第" + (j + 1) + "行:添加失败，未知原因;";
                                }
                            }
                            else
                            {
                                nosuccess++;
                                reason += "第" + (j + 1) + "行:已存在的制品制程信息;";
                            }
                        }

                        //Allmsg += "[制品制程:";
                        //Allmsg += string.Format(msg, total, success, nosuccess, reason);
                        //Allmsg += "]\r\n";
                    }
                    catch (Exception ex)
                    {
                        reason = ex.ToString();
                    }
                    result = new Hashtable();
                    result["Name"] = "制品制程";
                    result["total"] = total;
                    result["success"] = success;
                    result["nosuccess"] = nosuccess;
                    result["reason"] = reason;
                    resultList.Add(result);


                    reason = null;
                    total = 0;
                    success = 0;
                    nosuccess = 0;
                    //制程替代
                    SFC_ItemProcessAlternativeRelationShip IPARShipModel = null;
                    string ItemID = null;
                    string ProcessID = null;
                    try
                    {
                        ISheet sheetTwo = wk.GetSheetAt(1);   //读取第二表数据
                        for (int j = 2; j <= sheetTwo.LastRowNum; j++)  //LastRowNum 是当前表的总行数
                        {
                            IRow row = sheetTwo.GetRow(j);  //读取当前行数据
                                                            //如果当前行的第一列没有数据，停止读取
                            if (row.GetCell(0) == null)
                                break;
                            //如果当前行的第一列没有数据，停止读取，这是判断当row.GetCell不能null然后实际是空字符
                            if (string.IsNullOrWhiteSpace(row.GetCell(0).ToString()))
                                break;
                            total++;
                            ICell cell = null;
                            if (row != null)
                            {
                                IPARShipModel = new SFC_ItemProcessAlternativeRelationShip();
                                cell = row.GetCell(0);//料號
                                if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                                {
                                    Itemmodel = SYS_ItemsService.getByCode(cell.ToString());
                                    if (Itemmodel != null)
                                        ItemID = Itemmodel.ItemID;
                                    else
                                    {
                                        reason += "第" + (j + 1) + "行:料号错误,不存在的料号;";
                                        nosuccess++;
                                        continue; //结束本次循环
                                    }
                                }
                                else
                                {
                                    reason += "第" + (j + 1) + "行:料号不能为空;";
                                    nosuccess++;
                                    continue; //结束本次循环
                                }

                                cell = row.GetCell(2);//製程代號
                                if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                                {
                                    Parmodel = SYS_ParameterService.getByCode(cell.ToString(), SysID + "0191213000017");
                                    if (Parmodel != null)
                                        ProcessID = Parmodel.ParameterID;
                                    else
                                    {
                                        reason += "第" + (j + 1) + "行:不存在的製程代號;";
                                        nosuccess++;
                                        continue; //结束本次循环
                                    }
                                }
                                else
                                {
                                    reason += "第" + (j + 1) + "行:製程代號不能为空;";
                                    nosuccess++;
                                    continue; //结束本次循环
                                }

                                model = SFC_ItemProcessService.getByItemProcess(ItemID, ProcessID);
                                if (model == null)
                                {
                                    reason += "第" + (j + 1) + "行:不存在的制品制程信息;";
                                    nosuccess++;
                                    continue; //结束本次循环
                                }
                                IPARShipModel.ItemProcessID = model.ItemProcessID;

                                cell = row.GetCell(3);//替代序號[4]
                                if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                                {
                                    if (cell.ToString().Length <= 4)
                                        IPARShipModel.Sequence = cell.ToString();
                                    else
                                    {
                                        reason += "第" + (j + 1) + "行:製程序號超出范围限制;";
                                        nosuccess++;
                                        continue; //结束本次循环
                                    }
                                }
                                else
                                {
                                    reason += "第" + (j + 1) + "行:製程序號不能为空;";
                                    nosuccess++;
                                    continue; //结束本次循环
                                }

                                cell = row.GetCell(4);//替代製程
                                if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                                {
                                    Parmodel = SYS_ParameterService.getByCode(cell.ToString(), SysID + "0191213000017");
                                    if (Parmodel != null)
                                        IPARShipModel.ProcessID = Parmodel.ParameterID;
                                    else
                                    {
                                        reason += "第" + (j + 1) + "行:不存在的製程代號;";
                                        nosuccess++;
                                        continue; //结束本次循环
                                    }
                                }
                                else
                                {
                                    reason += "第" + (j + 1) + "行:製程代號不能为空;";
                                    nosuccess++;
                                    continue; //结束本次循环
                                }


                                cell = row.GetCell(5);//替代工作中心
                                if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                                {
                                    WCmodel = SYS_WorkCenterService.getByCode(cell.ToString());
                                    if (WCmodel != null)
                                        IPARShipModel.WorkCenterID = WCmodel.WorkCenterID;
                                    else
                                        reason += "第" + (j + 1) + "行:不存在的工作中心;";
                                }

                                cell = row.GetCell(6);//備註[120]
                                if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                                {
                                    if (cell.ToString().Length <= 120)
                                        IPARShipModel.Comments = cell.ToString();
                                    else
                                        reason += "第" + (j + 1) + "行:備註字段不能超过120;";
                                }

                                cell = row.GetCell(7);//製程單位[4]
                                if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                                {
                                    Parmodel = SYS_ParameterService.getByCode(cell.ToString(), SysID + "019121300000C");
                                    if (Parmodel != null)
                                        IPARShipModel.Unit = Parmodel.ParameterID;
                                    else
                                        reason += "第" + (j + 1) + "行:不存在的輔助單位;";
                                }

                                cell = row.GetCell(8);//製程母件單位比[10,4]
                                if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                                {
                                    try
                                    {
                                        IPARShipModel.UnitRatio = decimal.Parse(cell.ToString());
                                    }
                                    catch
                                    {
                                        reason += "第" + (j + 1) + "行:製程輔助單位比格式错误;";
                                    }
                                }


                                cell = row.GetCell(9);//委外單價[10,4]
                                if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                                {
                                    try
                                    {
                                        IPARShipModel.Price = decimal.Parse(cell.ToString());
                                    }
                                    catch
                                    {
                                        reason += "第" + (j + 1) + "行:委外單價格式错误;";
                                    }
                                }
                            }

                            if (!SFC_ItemProcessAlternativeRelationShipService.Check(IPARShipModel.ProcessID, IPARShipModel.ItemProcessID, null))
                            {
                                IPARShipModel.IPARSID = UniversalService.GetSerialNumber("SFC_ItemProcessAlternativeRelationShip");
                                IPARShipModel.Status = SysID + "0201213000001";
                                if (SFC_ItemProcessAlternativeRelationShipService.insert(userId, IPARShipModel))
                                    success++;
                                else
                                {
                                    nosuccess++;
                                    reason += "第" + (j + 1) + "行:添加失败，未知原因;";
                                }
                            }
                            else
                            {
                                nosuccess++;
                                reason += "第" + (j + 1) + "行:已存在的制程替代信息;";
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        reason = ex.ToString();
                    }
                    //Allmsg += "[制程替代:";
                    //Allmsg += string.Format(msg, total, success, nosuccess, reason);
                    //Allmsg += "]\r\n";
                    result = new Hashtable();
                    result["Name"] = "制程替代";
                    result["total"] = total;
                    result["success"] = success;
                    result["nosuccess"] = nosuccess;
                    result["reason"] = reason;
                    resultList.Add(result);

                    reason = null;
                    total = 0;
                    success = 0;
                    nosuccess = 0;
                    //制程工序
                    SFC_ItemOperation OpeModel = null;
                    try
                    {
                        ISheet sheetThree = wk.GetSheetAt(2);   //读取第三表数据
                        for (int j = 2; j <= sheetThree.LastRowNum; j++)  //LastRowNum 是当前表的总行数
                        {
                            IRow row = sheetThree.GetRow(j);  //读取当前行数据
                            if (row.GetCell(0) == null) //如果当前行的第一列没有数据，停止读取
                                break;
                            if (string.IsNullOrWhiteSpace(row.GetCell(0).ToString()))//如果当前行的第一列没有数据，停止读取，这是判断当row.GetCell不能null然后实际是空字符
                                break;
                            total++;
                            ICell cell = null;
                            if (row != null)
                            {
                                ItemID = null;
                                ProcessID = null;
                                OpeModel = new SFC_ItemOperation();
                                //cell = row.GetCell(0);//序号
                                //Sequence = cell.ToString();

                                cell = row.GetCell(0);//料號
                                if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                                {
                                    Itemmodel = SYS_ItemsService.getByCode(cell.ToString());
                                    if (Itemmodel != null)
                                        ItemID = Itemmodel.ItemID;
                                    else
                                    {
                                        reason += "第" + (j + 1) + "行:料号错误,不存在的料号;";
                                        nosuccess++;
                                        continue; //结束本次循环
                                    }
                                }
                                else
                                {
                                    reason += "第" + (j + 1) + "行:料号不能为空;";
                                    nosuccess++;
                                    continue; //结束本次循环
                                }

                                cell = row.GetCell(2);//製程代號
                                if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                                {
                                    Parmodel = SYS_ParameterService.getByCode(cell.ToString(), SysID + "0191213000017");
                                    if (Parmodel != null)
                                        ProcessID = Parmodel.ParameterID;
                                    else
                                    {
                                        reason += "第" + (j + 1) + "行:不存在的製程代號;";
                                        nosuccess++;
                                        continue; //结束本次循环
                                    }
                                }
                                else
                                {
                                    reason += "第" + (j + 1) + "行:製程代號不能为空;";
                                    nosuccess++;
                                    continue; //结束本次循环
                                }


                                model = SFC_ItemProcessService.getByItemProcess(ItemID, ProcessID);
                                if (model == null)
                                {
                                    reason += "第" + (j + 1) + "行:不存在的制品制程信息;";
                                    nosuccess++;
                                    continue; //结束本次循环
                                }
                                OpeModel.ItemProcessID = model.ItemProcessID;


                                cell = row.GetCell(3);//工序序號[4]
                                if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                                {
                                    if (cell.ToString().Length <= 4)
                                        OpeModel.Sequence = cell.ToString();
                                    else
                                    {
                                        reason += "第" + (j + 1) + "行:工序序號超出范围限制;";
                                        nosuccess++;
                                        continue; //结束本次循环
                                    }
                                }
                                else
                                {
                                    reason += "第" + (j + 1) + "行:工序序號不能为空;";
                                    nosuccess++;
                                    continue; //结束本次循环
                                }

                                cell = row.GetCell(4);//工序代號
                                if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                                {
                                    Parmodel = SYS_ParameterService.getByCode(cell.ToString(), SysID + "0191213000016");
                                    if (Parmodel != null)
                                        OpeModel.OperationID = Parmodel.ParameterID;
                                    else
                                    {
                                        reason += "第" + (j + 1) + "行:不存在的工序代號;";
                                        nosuccess++;
                                        continue; //结束本次循环
                                    }
                                }
                                else
                                {
                                    reason += "第" + (j + 1) + "行:工序代號不能为空;";
                                    nosuccess++;
                                    continue; //结束本次循环
                                }

                                cell = row.GetCell(5);//工序單位[4]
                                if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                                {
                                    Parmodel = SYS_ParameterService.getByCode(cell.ToString(), SysID + "019121300000C");
                                    if (Parmodel != null)
                                        OpeModel.Unit = Parmodel.ParameterID;
                                    else
                                        reason += "第" + (j + 1) + "行:不存在的工序單位;";
                                }

                                cell = row.GetCell(6);//工序母件單位比[10,4]
                                if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                                {
                                    try
                                    {
                                        OpeModel.UnitRatio = decimal.Parse(cell.ToString());
                                    }
                                    catch
                                    {
                                        reason += "第" + (j + 1) + "行:工序母件單位比格式错误;";
                                    }
                                }


                                cell = row.GetCell(8);//標準工時(秒)[int]
                                if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                                {
                                    try
                                    {
                                        OpeModel.StandardTime = int.Parse(cell.ToString());
                                    }
                                    catch
                                    {
                                        reason += "第" + (j + 1) + "行:標準工時格式错误;";
                                    }
                                }

                                cell = row.GetCell(9);//整備工時(秒)[int]
                                if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                                {
                                    try
                                    {
                                        OpeModel.PrepareTime = int.Parse(cell.ToString());
                                    }
                                    catch
                                    {
                                        reason += "第" + (j + 1) + "行:整備工時格式错误;";
                                    }
                                }

                                cell = row.GetCell(10);//制程檢驗註記(Y/N)
                                if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                                {
                                    if (cell.ToString() == "Y")
                                        OpeModel.IsIP = true;
                                    else if (cell.ToString() == "N")
                                        OpeModel.IsIP = false;
                                    else
                                    {
                                        OpeModel.IsIP = false;
                                        reason += "第" + (j + 1) + "行:制程檢驗註記格式错误;";
                                    }
                                }
                                else
                                    OpeModel.IsIP = false;

                                cell = row.GetCell(11);//首檢驗註記(Y/N)
                                if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                                {
                                    if (cell.ToString() == "Y")
                                        OpeModel.IsFPI = true;
                                    else if (cell.ToString() == "N")
                                        OpeModel.IsFPI = false;
                                    else
                                    {
                                        OpeModel.IsFPI = false;
                                        reason += "第" + (j + 1) + "行:首檢驗註記格式错误;";
                                    }
                                }
                                else
                                    OpeModel.IsFPI = false;

                                cell = row.GetCell(12);//巡檢驗註記(Y/N)
                                if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                                {
                                    if (cell.ToString() == "Y")
                                        OpeModel.IsOSI = true;
                                    else if (cell.ToString() == "N")
                                        OpeModel.IsOSI = false;
                                    else
                                    {
                                        OpeModel.IsOSI = false;
                                        reason += "第" + (j + 1) + "行:巡檢驗註記格式错误;";
                                    }
                                }
                                else
                                    OpeModel.IsOSI = false;

                                cell = row.GetCell(13);//備註[120]
                                if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                                {
                                    if (cell.ToString().Length <= 120)
                                        OpeModel.Comments = cell.ToString();
                                    else
                                        reason += "第" + (j + 1) + "行:備註字段不能超过120;";
                                }
                            }

                            if (!SFC_ItemOperationService.CheckOperation(OpeModel.OperationID, OpeModel.ItemProcessID, null))
                            {
                                OpeModel.ItemOperationID = UniversalService.GetSerialNumber("SFC_ItemOperation");
                                OpeModel.Status = SysID + "0201213000001";
                                if (SFC_ItemOperationService.insert(userId, OpeModel))
                                    success++;
                                else
                                {
                                    nosuccess++;
                                    reason += "第" + (j + 1) + "行:添加失败，位置原因;";
                                }
                            }
                            else
                            {
                                nosuccess++;
                                reason += "第" + (j + 1) + "行:已存在相同的制品制程工序信息;";
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        reason = ex.ToString();
                    }

                    //Allmsg += "[制程工序:";
                    //Allmsg += string.Format(msg, total, success, nosuccess, reason);
                    //Allmsg += "]\r\n";
                    result = new Hashtable();
                    result["Name"] = "制程工序";
                    result["total"] = total;
                    result["success"] = success;
                    result["nosuccess"] = nosuccess;
                    result["reason"] = reason;
                    resultList.Add(result);

                    reason = null;
                    total = 0;
                    success = 0;
                    nosuccess = 0;
                    //制品BOM
                    SFC_ItemMaterial MaterialModel = null;
                    string OperationID = null;
                    try
                    {
                        ISheet sheetFour = wk.GetSheetAt(3);   //读取第四表数据
                        for (int j = 2; j <= sheetFour.LastRowNum; j++)  //LastRowNum 是当前表的总行数
                        {
                            IRow row = sheetFour.GetRow(j);  //读取当前行数据
                            if (row.GetCell(0) == null) //如果当前行的第一列没有数据，停止读取
                                break;
                            if (string.IsNullOrWhiteSpace(row.GetCell(0).ToString()))//如果当前行的第一列没有数据，停止读取，这是判断当row.GetCell不能null然后实际是空字符
                                break;
                            total++;
                            ICell cell = null;
                            if (row != null)
                            {
                                ItemID = null;
                                ProcessID = null;
                                MaterialModel = new SFC_ItemMaterial();
                                //cell = row.GetCell(0);//序号
                                //Sequence = cell.ToString();

                                cell = row.GetCell(0);//料號
                                if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                                {
                                    Itemmodel = SYS_ItemsService.getByCode(cell.ToString());
                                    if (Itemmodel != null)
                                        ItemID = Itemmodel.ItemID;
                                    else
                                    {
                                        reason += "第" + (j + 1) + "行:料号错误,不存在的料号;";
                                        nosuccess++;
                                        continue; //结束本次循环
                                    }
                                }
                                else
                                {
                                    reason += "第" + (j + 1) + "行:料号不能为空;";
                                    nosuccess++;
                                    continue; //结束本次循环
                                }

                                cell = row.GetCell(2);//製程代號
                                if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                                {
                                    Parmodel = SYS_ParameterService.getByCode(cell.ToString(), SysID + "0191213000017");
                                    if (Parmodel != null)
                                        ProcessID = Parmodel.ParameterID;
                                    else
                                    {
                                        reason += "第" + (j + 1) + "行:不存在的製程代號;";
                                        nosuccess++;
                                        continue; //结束本次循环
                                    }
                                }
                                else
                                {
                                    reason += "第" + (j + 1) + "行:製程代號不能为空;";
                                    nosuccess++;
                                    continue; //结束本次循环
                                }


                                model = SFC_ItemProcessService.getByItemProcess(ItemID, ProcessID);
                                if (model == null)
                                {
                                    reason += "第" + (j + 1) + "行:不存在的制品制程信息;";
                                    nosuccess++;
                                    continue; //结束本次循环
                                }
                                MaterialModel.ItemProcessID = model.ItemProcessID;


                                cell = row.GetCell(4);//工序代號
                                if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                                {
                                    Parmodel = SYS_ParameterService.getByCode(cell.ToString(), SysID + "0191213000016");
                                    if (Parmodel != null)
                                        OperationID = Parmodel.ParameterID;
                                    else
                                    {
                                        reason += "第" + (j + 1) + "行:不存在的工序代號;";
                                        nosuccess++;
                                        continue; //结束本次循环
                                    }
                                }

                                if (!string.IsNullOrWhiteSpace(OperationID))
                                {
                                    OpeModel = SFC_ItemOperationService.getByItemProcess(MaterialModel.ItemProcessID, OperationID);
                                    if (model == null)
                                    {
                                        reason += "第" + (j + 1) + "行:不存在的制品制程工序信息;";
                                        nosuccess++;
                                        continue; //结束本次循环
                                    }
                                    MaterialModel.ItemOperationID = OpeModel.ItemOperationID;
                                    MaterialModel.ItemProcessID = null;
                                }


                                cell = row.GetCell(5);//子件排序[4]
                                if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                                {
                                    if (cell.ToString().Length <= 4)
                                        MaterialModel.Sequence = cell.ToString();
                                    else
                                    {
                                        reason += "第" + (j + 1) + "行:子件排序超出范围限制;";
                                        nosuccess++;
                                        continue; //结束本次循环
                                    }
                                }
                                else
                                {
                                    reason += "第" + (j + 1) + "行:子件排序不能为空;";
                                    nosuccess++;
                                    continue; //结束本次循环
                                }

                                cell = row.GetCell(6);//子件料號
                                if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                                {
                                    Itemmodel = SYS_ItemsService.getByCode(cell.ToString());
                                    if (Parmodel != null)
                                        MaterialModel.ItemID = Itemmodel.ItemID;
                                    else
                                    {
                                        reason += "第" + (j + 1) + "行:不存在的子件料號;";
                                        nosuccess++;
                                        continue; //结束本次循环
                                    }
                                }
                                else
                                {
                                    reason += "第" + (j + 1) + "行:子件料號不能为空;";
                                    nosuccess++;
                                    continue; //结束本次循环
                                }

                                cell = row.GetCell(7);//基本用量[16,8]
                                if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                                {
                                    try
                                    {
                                        MaterialModel.BasicQuantity = decimal.Parse(cell.ToString());
                                    }
                                    catch
                                    {
                                        reason += "第" + (j + 1) + "行:基本用量错误;";
                                    }
                                }


                                cell = row.GetCell(8);//損耗率[5,3]
                                if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                                {
                                    try
                                    {
                                        MaterialModel.AttritionRate = decimal.Parse(cell.ToString());
                                    }
                                    catch
                                    {
                                        reason += "第" + (j + 1) + "行:損耗率格式错误;";
                                    }
                                }

                                cell = row.GetCell(9);//使用數量[16,8]
                                if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                                {
                                    try
                                    {
                                        MaterialModel.UseQuantity = decimal.Parse(cell.ToString());
                                    }
                                    catch
                                    {
                                        reason += "第" + (j + 1) + "行:使用數量错误;";
                                    }
                                }

                                cell = row.GetCell(10);//備註[120]
                                if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                                {
                                    if (cell.ToString().Length <= 120)
                                        MaterialModel.Comments = cell.ToString();
                                    else
                                        reason += "第" + (j + 1) + "行:備註字段不能超过120;";
                                }
                            }


                            if (!SFC_ItemMaterialService.Check(MaterialModel.ItemID, MaterialModel.ItemProcessID, MaterialModel.ItemOperationID, null))
                            {
                                MaterialModel.ItemMaterialID = UniversalService.GetSerialNumber("SFC_ItemMaterial");
                                MaterialModel.Status = SysID + "0201213000001";
                                if (SFC_ItemMaterialService.insert(userId, MaterialModel))
                                    success++;
                                else
                                {
                                    nosuccess++;
                                    reason += "第" + (j + 1) + "行:添加失败，位置原因;";
                                }
                            }
                            else
                            {
                                nosuccess++;
                                reason += "第" + (j + 1) + "行:已存在的BOM信息;";
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        reason = ex.ToString();
                    }

                    //Allmsg += "[制品BOM:";
                    //Allmsg += string.Format(msg, total, success, nosuccess, reason);
                    //Allmsg += "]\r\n";
                    result = new Hashtable();
                    result["Name"] = "制品BOM";
                    result["total"] = total;
                    result["success"] = success;
                    result["nosuccess"] = nosuccess;
                    result["reason"] = reason;
                    resultList.Add(result);

                    reason = null;
                    total = 0;
                    success = 0;
                    nosuccess = 0;
                    //制品资源
                    SYS_Resources Resmodel = null;
                    SFC_ItemResource ItemResmodel = null;
                    try
                    {
                        ISheet sheetFive = wk.GetSheetAt(4);   //读取第五表数据
                        for (int j = 2; j <= sheetFive.LastRowNum; j++)  //LastRowNum 是当前表的总行数
                        {
                            IRow row = sheetFive.GetRow(j);  //读取当前行数据
                            if (row.GetCell(0) == null) //如果当前行的第一列没有数据，停止读取
                                break;
                            if (string.IsNullOrWhiteSpace(row.GetCell(0).ToString()))//如果当前行的第一列没有数据，停止读取，这是判断当row.GetCell不能null然后实际是空字符
                                break;
                            total++;
                            ICell cell = null;
                            if (row != null)
                            {
                                ItemID = null;
                                ProcessID = null;
                                OperationID = null;
                                ItemResmodel = new SFC_ItemResource();
                                //cell = row.GetCell(0);//序号
                                //Sequence = cell.ToString();

                                cell = row.GetCell(0);//料號
                                if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                                {
                                    Itemmodel = SYS_ItemsService.getByCode(cell.ToString());
                                    if (Itemmodel != null)
                                        ItemID = Itemmodel.ItemID;
                                    else
                                    {
                                        reason += "第" + (j + 1) + "行:料号错误,不存在的料号;";
                                        nosuccess++;
                                        continue; //结束本次循环
                                    }
                                }
                                else
                                {
                                    reason += "第" + (j + 1) + "行:料号不能为空;";
                                    nosuccess++;
                                    continue; //结束本次循环
                                }

                                cell = row.GetCell(2);//製程代號
                                if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                                {
                                    Parmodel = SYS_ParameterService.getByCode(cell.ToString(), SysID + "0191213000017");
                                    if (Parmodel != null)
                                        ProcessID = Parmodel.ParameterID;
                                    else
                                    {
                                        reason += "第" + (j + 1) + "行:不存在的製程代號;";
                                        nosuccess++;
                                        continue; //结束本次循环
                                    }
                                }
                                else
                                {
                                    reason += "第" + (j + 1) + "行:製程代號不能为空;";
                                    nosuccess++;
                                    continue; //结束本次循环
                                }

                                model = SFC_ItemProcessService.getByItemProcess(ItemID, ProcessID);
                                if (model == null)
                                {
                                    reason += "第" + (j + 1) + "行:不存在的制品制程信息;";
                                    nosuccess++;
                                    continue; //结束本次循环
                                }
                                ItemResmodel.ItemProcessID = model.ItemProcessID;



                                cell = row.GetCell(4);//工序代號
                                if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                                {
                                    Parmodel = SYS_ParameterService.getByCode(cell.ToString(), SysID + "0191213000016");
                                    if (Parmodel != null)
                                        OperationID = Parmodel.ParameterID;
                                    else
                                    {
                                        reason += "第" + (j + 1) + "行:不存在的工序代號;";
                                        nosuccess++;
                                        continue; //结束本次循环
                                    }
                                }

                                if (!string.IsNullOrWhiteSpace(OperationID))
                                {
                                    OpeModel = SFC_ItemOperationService.getByItemProcess(MaterialModel.ItemProcessID, OperationID);
                                    if (model == null)
                                    {
                                        reason += "第" + (j + 1) + "行:不存在的制品制程工序信息;";
                                        nosuccess++;
                                        continue; //结束本次循环
                                    }
                                    ItemResmodel.ItemOperationID = OpeModel.ItemOperationID;
                                    ItemResmodel.ItemProcessID = null;
                                }

                                cell = row.GetCell(5);//資源代號
                                if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                                {
                                    Resmodel = SYS_ResourcesService.getByCode(cell.ToString());
                                    if (Parmodel != null)
                                    {
                                        ItemResmodel.ResourceID = Resmodel.ResourceID;
                                        if (Resmodel.ClassID == SysID + "0201213000047")
                                            ItemResmodel.Type = SysID + "0201213000085";
                                        else if (Resmodel.ClassID == SysID + "0201213000048")
                                            ItemResmodel.Type = SysID + "0201213000084";
                                        else
                                            ItemResmodel.Type = SysID + "0201213000086";
                                    }
                                    else
                                    {
                                        reason += "第" + (j + 1) + "行:不存在的資源代號;";
                                        nosuccess++;
                                        continue; //结束本次循环
                                    }
                                }
                                else
                                {
                                    reason += "第" + (j + 1) + "行:子件料號不能为空;";
                                    nosuccess++;
                                    continue; //结束本次循环
                                }



                                cell = row.GetCell(6);//是否主資源(Y/N)
                                if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                                {
                                    if (cell.ToString() == "Y")
                                        ItemResmodel.IfMain = true;
                                    else if (cell.ToString() == "N")
                                        ItemResmodel.IfMain = false;
                                    else
                                    {
                                        ItemResmodel.IfMain = false;
                                        reason += "第" + (j + 1) + "行:是否主資源格式错误;";
                                    }
                                }

                                cell = row.GetCell(7);//備註[120]
                                if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                                {
                                    if (cell.ToString().Length <= 120)
                                        ItemResmodel.Comments = cell.ToString();
                                    else
                                        reason += "第" + (j + 1) + "行:備註字段不能超过120;";
                                }
                            }


                            if (!SFC_ItemResourceService.Check(ItemResmodel.ResourceID, ItemResmodel.ItemProcessID, ItemResmodel.ItemOperationID, ItemResmodel.Type, null))
                            {
                                ItemResmodel.ItemResourceID = UniversalService.GetSerialNumber("SFC_ItemResource");
                                ItemResmodel.Status = SysID + "0201213000001";
                                if (SFC_ItemResourceService.insert(userId, ItemResmodel))
                                    success++;
                                else
                                {
                                    nosuccess++;
                                    reason += "第" + (j + 1) + "行:添加失败，未知原因;";
                                }
                            }
                            else
                            {
                                nosuccess++;
                                reason += "第" + (j + 1) + "行:已存在的资源信息;";
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        reason = ex.ToString();
                    }

                    //Allmsg += "[制品资源:";
                    //Allmsg += string.Format(msg, total, success, nosuccess, reason);
                    //Allmsg += "]\r\n";
                    result = new Hashtable();
                    result["Name"] = "制品资源";
                    result["total"] = total;
                    result["success"] = success;
                    result["nosuccess"] = nosuccess;
                    result["reason"] = reason;
                    resultList.Add(result);

                    reason = null;
                    total = 0;
                    success = 0;
                    nosuccess = 0;
                    //制程关系
                    SFC_ItemProcessRelationShip ItemProShip = null;
                    try
                    {
                        ISheet sheetSix = wk.GetSheetAt(5);   //读取第六表数据
                        for (int j = 2; j <= sheetSix.LastRowNum; j++)  //LastRowNum 是当前表的总行数
                        {
                            IRow row = sheetSix.GetRow(j);  //读取当前行数据
                            if (row.GetCell(0) == null) //如果当前行的第一列没有数据，停止读取
                                break;
                            if (string.IsNullOrWhiteSpace(row.GetCell(0).ToString()))//如果当前行的第一列没有数据，停止读取，这是判断当row.GetCell不能null然后实际是空字符
                                break;
                            total++;
                            ICell cell = null;
                            if (row != null)
                            {
                                ItemID = null;
                                ProcessID = null;
                                OperationID = null;
                                ItemProShip = new SFC_ItemProcessRelationShip();
                                //cell = row.GetCell(0);//序号
                                //Sequence = cell.ToString();

                                cell = row.GetCell(0);//料號
                                if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                                {
                                    Itemmodel = SYS_ItemsService.getByCode(cell.ToString());
                                    if (Itemmodel != null)
                                        ItemProShip.ItemID = Itemmodel.ItemID;
                                    else
                                    {
                                        reason += "第" + (j + 1) + "行:料号错误,不存在的料号;";
                                        nosuccess++;
                                        continue; //结束本次循环
                                    }
                                }
                                else
                                {
                                    reason += "第" + (j + 1) + "行:料号不能为空;";
                                    nosuccess++;
                                    continue; //结束本次循环
                                }

                                cell = row.GetCell(2);//製程代號
                                if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                                {
                                    Parmodel = SYS_ParameterService.getByCode(cell.ToString(), SysID + "0191213000017");
                                    if (Parmodel != null)
                                    {
                                        ProcessID = Parmodel.ParameterID;
                                        model = SFC_ItemProcessService.getByItemProcess(ItemProShip.ItemID, ProcessID);
                                        if (model != null)
                                        {
                                            ItemProShip.ItemProcessID = model.ItemProcessID;
                                        }
                                        else
                                        {
                                            reason += "第" + (j + 1) + "行:不存在的制品制程;";
                                            nosuccess++;
                                            continue; //结束本次循环
                                        }
                                    }
                                    else
                                    {
                                        reason += "第" + (j + 1) + "行:不存在的製程代號;";
                                        nosuccess++;
                                        continue; //结束本次循环
                                    }
                                }
                                else
                                {
                                    reason += "第" + (j + 1) + "行:製程代號不能为空;";
                                    nosuccess++;
                                    continue; //结束本次循环
                                }
                                cell = row.GetCell(4);//前站的製程代號
                                if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                                {
                                    Parmodel = SYS_ParameterService.getByCode(cell.ToString(), SysID + "0191213000017");
                                    if (Parmodel != null)
                                    {
                                        ProcessID = Parmodel.ParameterID;
                                        model = SFC_ItemProcessService.getByItemProcess(ItemProShip.ItemID, ProcessID);
                                        if (model != null)
                                        {
                                            ItemProShip.PreItemProcessID = model.ItemProcessID;
                                        }
                                        else
                                        {
                                            reason += "第" + (j + 1) + "行:不存在的制品制程;";
                                            nosuccess++;
                                            continue; //结束本次循环
                                        }
                                    }
                                    else
                                    {
                                        reason += "第" + (j + 1) + "行:不存在的製程代號;";
                                        nosuccess++;
                                        continue; //结束本次循环
                                    }
                                }
                                else
                                {
                                    reason += "第" + (j + 1) + "行:製程代號不能为空;";
                                    nosuccess++;
                                    continue; //结束本次循环
                                }


                                cell = row.GetCell(6);//最終製程[Y/N]
                                if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                                {
                                    if (cell.ToString() == "Y")
                                        ItemProShip.FinishProcess = true;
                                    else if (cell.ToString() == "N")
                                        ItemProShip.FinishProcess = false;
                                    else
                                    {
                                        ItemProShip.FinishProcess = false;
                                        reason += "第" + (j + 1) + "行:最終製程格式错误;";
                                    }
                                }

                                cell = row.GetCell(6);//主流程[Y/N]
                                if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                                {
                                    if (cell.ToString() == "Y")
                                        ItemProShip.IfMain = true;
                                    else if (cell.ToString() == "N")
                                        ItemProShip.IfMain = false;
                                    else
                                    {
                                        ItemProShip.IfMain = false;
                                        reason += "第" + (j + 1) + "行:主流程格式错误;";
                                    }
                                }

                                cell = row.GetCell(7);//備註[120]
                                if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                                {
                                    if (cell.ToString().Length <= 120)
                                        ItemProShip.Comments = cell.ToString();
                                    else
                                        reason += "第" + (j + 1) + "行:備註字段不能超过120;";
                                }
                            }

                            //if (!SFC_ItemProcessRelationShipService.CheckOperation(OpeModel.OperationID, OpeModel.ItemProcessID, null))
                            //{
                            ItemProShip.IPRSID = UniversalService.GetSerialNumber("SFC_ItemProcessRelationShip");
                            ItemProShip.Status = SysID + "0201213000001";
                            if (SFC_ItemProcessRelationShipService.insert(userId, ItemProShip))
                                success++;
                            else
                            {
                                nosuccess++;
                                reason += "第" + (j + 1) + "行:添加失败，未知原因;";
                            }
                            //}
                            //else
                            //{

                            //}

                        }
                    }
                    catch (Exception ex)
                    {
                        reason = ex.ToString();
                    }
                    //Allmsg += "[制程关系:";
                    //Allmsg += string.Format(msg, total, success, nosuccess, reason);
                    //Allmsg += "]\r\n";
                    result = new Hashtable();
                    result["Name"] = "制程关系";
                    result["total"] = total;
                    result["success"] = success;
                    result["nosuccess"] = nosuccess;
                    result["reason"] = reason;
                    resultList.Add(result);

                    reason = null;
                    total = 0;
                    success = 0;
                    nosuccess = 0;
                    //制程工序关系
                    SFC_ProcessOperationRelationShip ProOpeShip = null;
                    try
                    {
                        ISheet sheetSeven = wk.GetSheetAt(6);   //读取第七表数据
                        for (int j = 2; j <= sheetSeven.LastRowNum; j++)  //LastRowNum 是当前表的总行数
                        {
                            IRow row = sheetSeven.GetRow(j);  //读取当前行数据
                                                              //如果当前行的第一列没有数据，停止读取
                            if (row.GetCell(0) == null)
                                break;
                            //如果当前行的第一列没有数据，停止读取，这是判断当row.GetCell不能null然后实际是空字符
                            if (string.IsNullOrWhiteSpace(row.GetCell(0).ToString()))
                                break;
                            total++;
                            ICell cell = null;
                            if (row != null)
                            {
                                ItemID = null;
                                ProcessID = null;
                                OperationID = null;
                                ProOpeShip = new SFC_ProcessOperationRelationShip();
                                //cell = row.GetCell(0);//序号
                                //Sequence = cell.ToString();

                                cell = row.GetCell(0);//料號
                                if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                                {
                                    Itemmodel = SYS_ItemsService.getByCode(cell.ToString());
                                    if (Itemmodel != null)
                                        ItemID = Itemmodel.ItemID;
                                    else
                                    {
                                        reason += "第" + (j + 1) + "行:料号错误,不存在的料号;";
                                        nosuccess++;
                                        continue; //结束本次循环
                                    }
                                }
                                else
                                {
                                    reason += "第" + (j + 1) + "行:料号不能为空;";
                                    nosuccess++;
                                    continue; //结束本次循环
                                }

                                cell = row.GetCell(2);//製程代號
                                if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                                {
                                    Parmodel = SYS_ParameterService.getByCode(cell.ToString(), SysID + "0191213000017");
                                    if (Parmodel != null)
                                        ProcessID = Parmodel.ParameterID;
                                    else
                                    {
                                        reason += "第" + (j + 1) + "行:不存在的製程代號;";
                                        nosuccess++;
                                        continue; //结束本次循环
                                    }
                                }
                                else
                                {
                                    reason += "第" + (j + 1) + "行:製程代號不能为空;";
                                    nosuccess++;
                                    continue; //结束本次循环
                                }

                                model = SFC_ItemProcessService.getByItemProcess(ItemID, ProcessID);
                                if (model == null)
                                {
                                    reason += "第" + (j + 1) + "行:并不存在的制品制程信息;";
                                    nosuccess++;
                                    continue; //结束本次循环
                                }
                                ProOpeShip.ItemProcessID = model.ItemProcessID;

                                cell = row.GetCell(4);//工序代號
                                if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                                {
                                    Parmodel = SYS_ParameterService.getByCode(cell.ToString(), SysID + "0191213000016");
                                    if (Parmodel != null)
                                    {
                                        OperationID = Parmodel.ParameterID;
                                        OpeModel = SFC_ItemOperationService.getByItemProcess(model.ItemProcessID, OperationID);
                                        if (OpeModel != null)
                                        {
                                            ProOpeShip.ItemOperationID = OpeModel.ItemOperationID;
                                        }
                                        else
                                        {
                                            reason += "第" + (j + 1) + "行:不存在的工序代號;";
                                            nosuccess++;
                                            continue; //结束本次循环
                                        }
                                    }
                                    else
                                    {
                                        reason += "第" + (j + 1) + "行:不存在的工序代號;";
                                        nosuccess++;
                                        continue; //结束本次循环
                                    }
                                }
                                else
                                {
                                    reason += "第" + (j + 1) + "行:製程代號不能为空;";
                                    nosuccess++;
                                    continue; //结束本次循环
                                }

                                cell = row.GetCell(6);//前站的製程工序代號
                                if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                                {
                                    Parmodel = SYS_ParameterService.getByCode(cell.ToString(), SysID + "0191213000016");
                                    if (Parmodel != null)
                                    {
                                        OperationID = Parmodel.ParameterID;
                                        OpeModel = SFC_ItemOperationService.getByItemProcess(model.ItemProcessID, OperationID);
                                        if (OpeModel != null)
                                        {
                                            ProOpeShip.PreItemOperationID = OpeModel.ItemOperationID;
                                        }
                                        else
                                        {
                                            reason += "第" + (j + 1) + "行:不存在的工序代號;";
                                            nosuccess++;
                                            continue; //结束本次循环
                                        }
                                    }
                                    else
                                    {
                                        reason += "第" + (j + 1) + "行:存在的工序代號;";
                                        nosuccess++;
                                        continue; //结束本次循环
                                    }
                                }
                                else
                                {
                                    reason += "第" + (j + 1) + "行:製程代號不能为空;";
                                    nosuccess++;
                                    continue; //结束本次循环
                                }

                                cell = row.GetCell(6);//最終工序(Y/N)
                                if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                                {
                                    if (cell.ToString() == "Y")
                                        ProOpeShip.FinishOperation = true;
                                    else if (cell.ToString() == "N")
                                        ProOpeShip.FinishOperation = false;
                                    else
                                    {
                                        ProOpeShip.FinishOperation = false;
                                        reason += "第" + (j + 1) + "行:最終工序格式错误;";
                                    }
                                }

                                cell = row.GetCell(7);//備註[120]
                                if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                                {
                                    if (cell.ToString().Length <= 120)
                                        ProOpeShip.Comments = cell.ToString();
                                    else
                                        reason += "第" + (j + 1) + "行:備註字段不能超过120;";
                                }
                            }

                            ProOpeShip.PORSID = UniversalService.GetSerialNumber("SFC_ProcessOperationRelationShip");
                            ProOpeShip.Status = SysID + "0201213000001";
                            if (SFC_ProcessOperationRelationShipService.insert(userId, ProOpeShip))
                                success++;
                            else
                            {
                                nosuccess++;
                                reason += "第" + (j + 1) + "行:添加失败，位置原因;";
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        reason = ex.ToString();
                    }
                    //Allmsg += "[制程关系:";
                    //Allmsg += string.Format(msg, total, success, nosuccess, reason);
                    //Allmsg += "]\r\n";
                    result = new Hashtable();
                    result["Name"] = "制程关系";
                    result["total"] = total;
                    result["success"] = success;
                    result["nosuccess"] = nosuccess;
                    result["reason"] = reason;
                    resultList.Add(result);
                }
            }
            //if (nosuccess == total)
            //    status = "410";

            msg = Allmsg;

            return new { status = status, msg = resultList };

        }


        /// <summary>
        /// Alvin 2017年9月5日16:06:33
        /// QCS00004 标准检验规范设定（制程）导入
        /// </summary>
        /// <param name="token"></param>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static object Qcs00004Import(string token, string filename, string partID, string inspectionType, string settingType, string processID, string operationID)
        {
            string userId = UtilBussinessService.detoken(token);
            string SysID = Framework.SystemID;
            string status = "200", msg = "共{0}条信息,{1}条信息导入成功,{2}条信息导入失败！{3}";
            string reason = null;
            int total = 0;
            int success = 0;
            int nosuccess = 0;
            if (filename.EndsWith(".xls") || filename.EndsWith(".xlsx"))
            {
                IWorkbook wk = null;
                String Sequence = null;
                QCS_StaInsSpeSetting model = null;
                QCS_InspectionProject InspectionModel = null;
                using (FileStream fs = File.OpenRead(@filename))   //打开.xls文件
                {
                    if (filename.EndsWith(".xls"))
                        wk = new HSSFWorkbook(fs); //把xls文件中的数据写入wk中
                    else if (filename.EndsWith(".xlsx"))
                        wk = new XSSFWorkbook(fs);//把xlsx文件中的数据写入wk中

                    ISheet sheet = wk.GetSheetAt(0);   //读取第一表数据
                    for (int j = 2; j <= sheet.LastRowNum; j++)  //LastRowNum 是当前表的总行数
                    {
                        IRow row = sheet.GetRow(j);  //读取当前行数据
                        if (row.GetCell(0) == null) //如果当前行的第一列没有数据，停止读取
                            break;
                        if (string.IsNullOrWhiteSpace(row.GetCell(0).ToString()))//如果当前行的第一列没有数据，停止读取，这是判断当row.GetCell不能null然后实际是空字符
                            break;
                        total++;
                        ICell cell = null;
                        if (row != null)
                        {
                            model = new QCS_StaInsSpeSetting();
                            cell = row.GetCell(0);//序号


                            Sequence = cell.ToString();
                            model.Sequence = int.Parse(cell.ToString());

                            cell = row.GetCell(1);//檢驗類別代號
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                SYS_Parameters Parameter = SYS_ParameterService.getByCode(cell.ToString(), Framework.SystemID + "019121300001E");
                                if (Parameter == null)
                                {
                                    reason += "序号[" + Sequence + "]:檢驗類別代號不存在;";
                                    nosuccess++;
                                    continue; //结束本次循环
                                }
                                model.CategoryID = Parameter.ParameterID;
                            }
                            else
                            {
                                reason += "序号[" + Sequence + "]:檢驗類別代號不能为空;";
                                nosuccess++;
                                continue; //结束本次循环
                            }

                            cell = row.GetCell(2);//檢驗方式（代号）
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                SYS_Parameters Parameter = SYS_ParameterService.getByCode(cell.ToString(), Framework.SystemID + "019121300001B");
                                if (Parameter == null)
                                {
                                    reason += "序号[" + Sequence + "]:檢驗方式不存在;";
                                    nosuccess++;
                                    continue; //结束本次循环
                                }
                                model.InspectionMethod = Parameter.ParameterID;
                            }
                            else
                            {
                                reason += "序号[" + Sequence + "]:檢驗方式不能为空;";
                                nosuccess++;
                                continue; //结束本次循环
                            }

                            cell = row.GetCell(3);//檢驗項目代號
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                InspectionModel = QCS_InspectionProjectService.getByCode(cell.ToString());

                                if (InspectionModel == null)
                                {
                                    reason += "序号[" + Sequence + "]:檢驗項目代號错误，不存在的檢驗項目代號;";
                                    nosuccess++;
                                    continue; //结束本次循环
                                }

                                if (InspectionModel.Status == SysID + "0201213000002")
                                {
                                    reason += "序号[" + Sequence + "]:此檢驗項目代號处于作废状态;";
                                    nosuccess++;
                                    continue; //结束本次循环
                                }
                                model.InspectionProjectID = InspectionModel.InspectionProjectID;
                            }
                            else
                            {
                                reason += "序号[" + Sequence + "]:檢驗項目代號不能为空;";
                                nosuccess++;
                                continue; //结束本次循环
                            }
                        }

                        cell = row.GetCell(4);//檢驗標準
                        if (cell != null)
                        {
                            model.InspectionStandard = cell.ToString();
                        }

                        cell = row.GetCell(5);//實測值設定(代号)
                        if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                        {
                            SYS_Parameters Parameter = SYS_ParameterService.getByCode(cell.ToString(), Framework.SystemID + "0191213000052");
                            if (Parameter == null)
                            {
                                reason += "序号[" + Sequence + "]:實測值設定不存在;";
                                nosuccess++;
                                continue; //结束本次循环
                            }
                            model.Attribute = Parameter.ParameterID;
                        }
                        else
                        {
                            reason += "序号[" + Sequence + "]:實測值設定不能为空;";
                            nosuccess++;
                            continue; //结束本次循环
                        }

                        cell = row.GetCell(6);//备注
                        if (cell != null)
                        {
                            if (cell.ToString().Length <= 150)
                                model.Comments = cell.ToString();
                            else
                            {
                                reason += "序号[" + Sequence + "]:备注超过150个字;";
                            }
                        }

                        model.StaInsSpeSettingID = UniversalService.GetSerialNumber("QCS_StaInsSpeSetting");
                        model.Status = Framework.SystemID + "0201213000001";//状态
                        model.PartID = partID;// 料號,檢驗群組参数流水号
                        model.InspectionType = inspectionType;//檢驗種類
                        model.SettingType = settingType;//檢驗設定型態
                        model.ProcessID = processID;//制程流水号
                        model.OperationID = operationID;//工序流水号

                        if (QCS_StaInsSpeSettingService.insert(userId, model))
                        {
                            success++;
                        }
                        else
                        {
                            reason += "序号[" + Sequence + "]:添加失败，未知原因;";
                            nosuccess++;
                        }
                    }
                }
            }

            if (nosuccess == total)
                status = "410";
            msg = string.Format(msg, total, success, nosuccess, reason);
            return new { status = status, msg = msg };
        }


        /// <summary>
        /// 制令单的导入
        /// SAM 2017年9月29日15:02:15
        /// </summary>
        /// <param name="token"></param>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static object Sfc00002Import(string token, string filename)
        {
            string userId = UtilBussinessService.detoken(token);
            string SysID = Framework.SystemID;
            string status = "200", msg = "共{0}条信息,{1}条信息导入成功,{2}条信息导入失败！{3}";
            string reason = null;
            int total = 0;
            int success = 0;
            int nosuccess = 0;
            if (filename.EndsWith(".xls") || filename.EndsWith(".xlsx"))
            {
                IWorkbook wk = null;
                string Sequence = null;
                using (FileStream fs = File.OpenRead(@filename))   //打开.xls文件
                {
                    if (filename.EndsWith(".xls"))
                        wk = new HSSFWorkbook(fs); //把xls文件中的数据写入wk中
                    else if (filename.EndsWith(".xlsx"))
                        wk = new XSSFWorkbook(fs);//把xlsx文件中的数据写入wk中

                    ISheet sheet = wk.GetSheetAt(0);   //读取第一表数据
                    SFC_FabricatedMother FabMo = null;
                    SYS_Customers CustModel = null;
                    SYS_Items ItemModel = null;
                    for (int j = 2; j <= sheet.LastRowNum; j++)  //LastRowNum 是当前表的总行数
                    {
                        IRow row = sheet.GetRow(j);  //读取当前行数据
                        if (row.GetCell(0) == null) //如果当前行的第一列没有数据，停止读取
                            break;
                        if (string.IsNullOrWhiteSpace(row.GetCell(0).ToString()))//如果当前行的第一列没有数据，停止读取，这是判断当row.GetCell不能null然后实际是空字符
                            break;
                        total++;
                        ICell cell = null;
                        if (row != null)
                        {
                            FabMo = new SFC_FabricatedMother();
                            CustModel = null;
                            ItemModel = null;
                            cell = row.GetCell(0);//序号
                            Sequence = cell.ToString();

                            cell = row.GetCell(1);//投产日期/預計開工日
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                try
                                {
                                    FabMo.StartDate = DateTime.Parse(cell.DateCellValue.ToString());
                                }
                                catch
                                {
                                    reason += "序号[" + Sequence + "]:投产日期格式错误;";
                                    nosuccess++;
                                    continue; //结束本次循环
                                }
                            }
                            else
                            {
                                reason += "序号[" + Sequence + "]:完工日期不能为空;";
                                nosuccess++;
                                continue; //结束本次循环
                            }

                            cell = row.GetCell(2);//投产比例/超完工比率
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                try
                                {
                                    FabMo.OverRate = 100 - decimal.Parse(cell.ToString());
                                }
                                catch
                                {
                                    reason += "序号[" + Sequence + "]:投产比例格式错误;";
                                    nosuccess++;
                                    continue; //结束本次循环
                                }
                            }


                            cell = row.GetCell(3);//客户名/客戶代號
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                CustModel = SYS_CustomersService.getByCode(cell.ToString());
                                if (CustModel != null)
                                    FabMo.CustomerID = CustModel.CustomerID;
                                else
                                    reason += "序号[" + Sequence + "]:客户代号错误,不存在的客户信息;";
                            }

                            cell = row.GetCell(4);//工单号/製令單號
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                FabMo.MoNo = cell.ToString();
                            }
                            else
                            {
                                reason += "序号[" + Sequence + "]:工单号不能为空;";
                                nosuccess++;
                                continue; //结束本次循环
                            }

                            cell = row.GetCell(5);//订单号/訂單號碼
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                                FabMo.OrderNo = cell.ToString();

                            cell = row.GetCell(6);//产品型号/料品代號
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                ItemModel = SYS_ItemsService.getByCode(cell.ToString());
                                if (ItemModel != null)
                                    FabMo.ItemID = ItemModel.ItemID;
                                else
                                {
                                    reason += "序号[" + Sequence + "]:产品型号错误，不存在的产品信息;";
                                    nosuccess++;
                                    continue; //结束本次循环
                                }
                            }
                            else
                            {
                                reason += "序号[" + Sequence + "]:产品型号不能为空;";
                                nosuccess++;
                                continue; //结束本次循环
                            }

                            cell = row.GetCell(7);//投单数量/製造數量
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                try
                                {
                                    FabMo.Quantity = decimal.Parse(cell.ToString());
                                }
                                catch
                                {
                                    reason += "序号[" + Sequence + "]:投单数量格式错误;";
                                    nosuccess++;
                                    continue; //结束本次循环
                                }
                            }
                            else
                            {
                                reason += "序号[" + Sequence + "]:投单数量不能为空;";
                                nosuccess++;
                                continue; //结束本次循环
                            }

                            cell = row.GetCell(17);//预定交期/預計出貨日
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                try
                                {
                                    FabMo.ShipmentDate = DateTime.Parse(cell.DateCellValue.ToString());
                                }
                                catch
                                {
                                    reason += "序号[" + Sequence + "]:预定交期格式错误;";
                                }
                            }

                            cell = row.GetCell(18);//交货数量/訂單數量
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                try
                                {
                                    FabMo.OrderQuantity = decimal.Parse(cell.ToString());
                                }
                                catch
                                {
                                    reason += "序号[" + Sequence + "]:交货数量格式错误;";
                                    nosuccess++;
                                    continue; //结束本次循环
                                }
                            }

                            cell = row.GetCell(26);//特殊要求/备注
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                if (cell.ToString().Length <= 120)
                                    FabMo.Comments = cell.ToString();
                                else
                                    reason += "序号[" + Sequence + "]:特殊要求超过了120个字;";
                            }
                        }


                        if (SFC_FabricatedMotherService.Check(FabMo.MoNo))
                        {
                            reason += "序号[" + Sequence + "]:已存在的工单号;";
                            nosuccess++;
                            continue; //结束本次循环
                        }

                        FabMo.FabricatedMotherID = UniversalService.GetSerialNumber("SFC_FabricatedMother");
                        FabMo.Status = Framework.SystemID + "0201213000028";
                        FabMo.Date = DateTime.Now;
                        FabMo.Version = "0";
                        FabMo.UnitID = ItemModel.Unit;
                        if (SFC_FabricatedMotherService.insert(userId, FabMo))
                        {
                            //根据制令单添加下面所有的东西
                            UtilBussinessService.FabMo(userId, FabMo);
                            success++;
                        }
                        else
                        {
                            reason += "序号[" + Sequence + "]:添加失败，未知原因;";
                            nosuccess++;
                        }
                    }
                }
            }
            if (nosuccess == total)
                status = "410";

            msg = string.Format(msg, total, success, nosuccess, reason);

            return new { status = status, msg = msg };
        }

        /// <summary>
        /// 账户的导入，包括对应角色的导入
        /// Sam 2017年10月17日09:47:27
        /// </summary>
        /// <param name="token"></param>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static object Inf00003Import(string token, string filename)
        {
            DateTime StartTime = DateTime.Now;
            string userId = UtilBussinessService.detoken(token);
            string SysID = Framework.SystemID;
            string status = "200", msg = "共{0}条信息,{1}条信息导入成功,{2}条信息导入失败！";
            int total = 0;
            int success = 0;
            int nosuccess = 0;
            string reason = null;
            string Log = null;
            bool Isreason = false;
            if (filename.EndsWith(".xls") || filename.EndsWith(".xlsx"))
            {
                IWorkbook wk = null;
                SYS_MESUsers model = null;
                IList<SYS_Roles> RoleList = SYS_RoleService.GetList();
                IList<SYS_Parameters> ParList = SYS_ParameterService.GetList(SysID + "0191213000051");
                IList<SYS_Organization> OrgList = SYS_OrganizationService.GetList();
                SYS_Roles RoleModel = null;
                List<string> AddRole = null;
                string OrganizationID = null;
                string sql = null;
                using (FileStream fs = File.OpenRead(@filename))   //打开.xls文件
                {
                    if (filename.EndsWith(".xls"))
                        wk = new HSSFWorkbook(fs); //把xls文件中的数据写入wk中
                    else if (filename.EndsWith(".xlsx"))
                        wk = new XSSFWorkbook(fs);//把xlsx文件中的数据写入wk中

                    ISheet sheet = wk.GetSheetAt(0);   //读取第一表数据
                    for (int j = 2; j <= sheet.LastRowNum; j++)  //LastRowNum 是当前表的总行数
                    {
                        IRow row = sheet.GetRow(j);  //读取当前行数据
                        if (row == null)
                            break;
                        if (row.GetCell(0) == null) //如果当前行的第一列没有数据，停止读取
                            break;
                        if (string.IsNullOrWhiteSpace(row.GetCell(0).ToString()))//如果当前行的第一列没有数据，停止读取，这是判断当row.GetCell不能null然后实际是空字符
                            break;
                        total++;
                        ICell cell = null;
                        if (row != null)
                        {
                            model = new SYS_MESUsers();
                            OrganizationID = null;
                            sql = null;
                            AddRole = new List<string>();
                            reason = null;
                            cell = row.GetCell(0);//账号 20
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                if (cell.ToString().Length <= 20)
                                {
                                    if (SYS_MESUserService.CheckCode(cell.ToString().Trim(), null, null, null))
                                    {
                                        Log += "第[" + (j + 1) + "]行：账号[" + cell.ToString().Trim() + "]重复。\r\n";
                                        nosuccess++;
                                        continue;
                                    }
                                    else
                                        model.Account = cell.ToString().Trim();
                                }
                                else
                                {
                                    Log += "第[" + (j + 1) + "]行：账号[" + cell.ToString() + "]超过20个字。\r\n";
                                    nosuccess++;
                                    continue;
                                }
                            }
                            else
                            {
                                Log += "第[" + (j + 1) + "]行：账号[" + cell.ToString() + "]不能为空！\r\n";
                                nosuccess++;
                                continue;
                            }

                            cell = row.GetCell(1);//姓名 20
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                if (cell.ToString().Length <= 20)
                                    model.UserName = cell.ToString();
                                else
                                {
                                    Log += "第[" + (j + 1) + "]行：姓名[" + cell.ToString() + "]超过20个字;";
                                    nosuccess++;
                                    continue;
                                }
                            }
                            else
                            {
                                Log += "第[" + (j + 1) + "]行：姓名[" + cell.ToString() + "]为空;";
                                nosuccess++;
                                continue;
                            }
                            cell = row.GetCell(2);//英文名 60
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                if (cell.ToString().Length <= 60)
                                    model.EnglishName = cell.ToString();
                                else
                                    reason += "英文名[" + cell.ToString() + "]超过60个字;";
                            }
                            cell = row.GetCell(3);//性别
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                if (cell.ToString() == "女")
                                    model.Sex = false;
                                else if (cell.ToString() == "男")
                                    model.Sex = true;
                                else
                                    reason += "性别错误;";
                            }

                            cell = row.GetCell(4);//出生日期
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                                model.Brith = cell.ToString();

                            cell = row.GetCell(5);//工号  20
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                if (cell.ToString().Length <= 30)
                                    model.Emplno = cell.ToString();
                                else
                                    reason += "工号[" + cell.ToString() + "]超过了30个字;";
                            }

                            cell = row.GetCell(6);//部门
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {

                                SYS_Organization orgModel = OrgList.Where(w => w.Code == cell.ToString()).FirstOrDefault();
                                if (orgModel == null)
                                    reason += "部门[" + cell.ToString() + "]错误，不存在的部门;";
                                else
                                    OrganizationID = orgModel.OrganizationID;
                            }

                            cell = row.GetCell(7);//身份证
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                                model.IDcard = cell.ToString();

                            cell = row.GetCell(8);//手机号码 20
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                if (cell.ToString().Length <= 11)
                                    model.Mobile = cell.ToString();
                                else
                                    reason += "错误的手机号码[" + cell.ToString() + "];";
                            }
                            cell = row.GetCell(9);//E-mail 60
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                if (cell.ToString().Length <= 60)
                                    model.Email = cell.ToString();
                                else
                                    reason += "邮件[" + cell.ToString() + "]超过了60个字;";
                            }
                            cell = row.GetCell(10);//职工类型
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                SYS_Parameters parModel = ParList.Where(w => w.Name == cell.ToString() && w.ParameterTypeID == SysID + "0191213000051").FirstOrDefault();
                                if (parModel != null)
                                    model.Type = parModel.ParameterID;
                                else
                                {
                                    model.Type = Framework.SystemID + "0201213000013";
                                    reason += "职工类型[" + cell.ToString() + "]错误，不存在的职工类型;";
                                }
                            }
                            else
                            {
                                model.Type = Framework.SystemID + "0201213000013";
                            }
                            cell = row.GetCell(11);//入职时间
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                                model.InTime = cell.ToString();
                            cell = row.GetCell(12);//卡号 50
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                if (cell.ToString().Length <= 50)
                                    model.CardCode = cell.ToString();
                                else
                                {
                                    reason += "卡号[" + cell.ToString() + "]超过了50个字;";
                                }
                            }
                            cell = row.GetCell(13);//备注 120
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                if (cell.ToString().Length <= 120)
                                    model.Comments = cell.ToString();
                                else
                                    reason += "备注[" + cell.ToString() + "]超过了120个字;";
                            }
                            cell = row.GetCell(14);//状态
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                if (cell.ToString() == "正常")
                                    model.Status = 1;
                                else if (cell.ToString() == "作废")
                                    model.Status = 0;
                                else
                                {
                                    model.Status = 1;
                                    reason += "状态[" + cell.ToString() + "]错误,不存在的状态;";
                                }
                            }
                            else
                            {
                                model.Status = 1;
                            }

                            cell = row.GetCell(15);//角色一
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                RoleModel = RoleList.Where(w => w.Name == cell.ToString().Trim()).FirstOrDefault();
                                if (RoleModel != null)
                                    AddRole.Add(RoleModel.RoleID);
                                else
                                {
                                    model.Status = 1;
                                    reason += "不存在的角色一[" + cell.ToString() + "];";
                                }
                            }

                            cell = row.GetCell(16);//角色二
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                RoleModel = RoleList.Where(w => w.Name == cell.ToString().Trim()).FirstOrDefault();
                                if (RoleModel != null)
                                    AddRole.Add(RoleModel.RoleID);
                                else
                                {
                                    model.Status = 1;
                                    reason += "不存在的角色二[" + cell.ToString() + "];";
                                }
                            }

                            cell = row.GetCell(17);//角色三
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                RoleModel = RoleList.Where(w => w.Name == cell.ToString().Trim()).FirstOrDefault();
                                if (RoleModel != null)
                                    AddRole.Add(RoleModel.RoleID);
                                else
                                {
                                    model.Status = 1;
                                    reason += "不存在的角色三[" + cell.ToString() + "];";
                                }
                            }

                            cell = row.GetCell(18);//角色四
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                RoleModel = RoleList.Where(w => w.Name == cell.ToString().Trim()).FirstOrDefault();
                                if (RoleModel != null)
                                    AddRole.Add(RoleModel.RoleID);
                                else
                                {
                                    model.Status = 1;
                                    reason += "不存在的角色四[" + cell.ToString() + "];";
                                }
                            }

                            cell = row.GetCell(19);//角色五
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                RoleModel = RoleList.Where(w => w.Name == cell.ToString().Trim()).FirstOrDefault();
                                if (RoleModel != null)
                                    AddRole.Add(RoleModel.RoleID);
                                else
                                {
                                    model.Status = 1;
                                    reason += "不存在的角色五[" + cell.ToString() + "];";
                                }
                            }
                        }

                        model.MESUserID = UniversalService.GetSerialNumber("SYS_MESUsers");
                        model.Password = "3d9188577cc9bfe9291ac66b5cc872b7";
                        model.UserType = 10;
                        if (SYS_MESUserService.CheckCode(model.Account, null, null, null))
                        {
                            Log += "第[" + (j + 1) + "]行：账号[" + model.Account + "]重复\r\n";
                            nosuccess++;
                            continue;
                        }
                        if (SYS_MESUserService.insert(userId, model))
                        {
                            if (!string.IsNullOrWhiteSpace(OrganizationID))
                                SYS_UserOrganizationMappingService.add(userId, model.MESUserID, OrganizationID);
                            AddRole = AddRole.Distinct().ToList();
                            if (AddRole.Count != 0)
                            {
                                for (int Z = 0; Z < AddRole.Count; Z++)
                                    sql += SYS_RoleService.InsertSQL(userId, model.MESUserID, AddRole[Z]);
                            }
                            UtilBussinessService.RunSQL(sql);

                            if (!string.IsNullOrWhiteSpace(reason))
                                Log += "第[" + (j + 1) + "]行：" + reason + "\r\n";
                            success++;
                        }
                        else
                        {
                            Log += "第[" + (j + 1) + "]行：添加失败，未知错误，请联系开发人员\r\n";
                            nosuccess++;
                        }
                    }
                }
            }

            DateTime EndTime = DateTime.Now;

            if (nosuccess == total)
                status = "410";

            msg = string.Format(msg, total, success, nosuccess);

            //如果存在错误报告
            if (!string.IsNullOrWhiteSpace(Log))
            {
                Isreason = true;
                TimeSpan ts = EndTime - StartTime;
                string Hour = UtilBussinessService.HourConversionStr(((int)ts.TotalSeconds).ToString());
                DataLogerService.ImportWriteErrLog(StartTime, EndTime, Hour, total, success, nosuccess, "Inf00003ImportErrorLog", Log);
            }

            return new { status = status, msg = msg, Isreason = Isreason, FileName = "Inf00003ImportErrorLog" };
        }

        /// <summary>
        /// Qcs00004新的导入
        /// SAM 2017年10月19日09:36:29
        /// </summary>
        /// <param name="token"></param>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static object Qcs00004ImportV1(string token, string filename)
        {
            DateTime StartTime = DateTime.Now;
            string userId = UtilBussinessService.detoken(token);
            string SysID = Framework.SystemID;
            string status = "200", msg = "共{0}条信息,{1}条信息导入成功,{2}条信息导入失败！";
            string reason = null;
            int total = 0;
            int success = 0;
            int nosuccess = 0;
            string Log = null;
            bool Isreason = false;
            if (filename.EndsWith(".xls") || filename.EndsWith(".xlsx"))
            {
                IWorkbook wk = null;
                String Sequence = null;
                QCS_StaInsSpeSetting model = null;
                QCS_InspectionProject InspectionModel = null;
                SYS_Parameters ParModel = null;
                IList<SYS_Parameters> ParList = SYS_ParameterService.GetList(SysID + "019121300005C','" + SysID + "0191213000018','" + SysID + "0191213000017','" + SysID + "0191213000016','" + SysID + "019121300001E','" + SysID + "0191213000052','" + SysID + "019121300001B");
                IList<QCS_InspectionProject> InspectionProjectList = QCS_InspectionProjectService.GetList();
                bool IsInspection = true;
                using (FileStream fs = File.OpenRead(@filename))   //打开.xls文件
                {
                    if (filename.EndsWith(".xls"))
                        wk = new HSSFWorkbook(fs); //把xls文件中的数据写入wk中
                    else if (filename.EndsWith(".xlsx"))
                        wk = new XSSFWorkbook(fs);//把xlsx文件中的数据写入wk中

                    ISheet sheet = wk.GetSheetAt(0);   //读取第一表数据
                    for (int j = 2; j <= sheet.LastRowNum; j++)  //LastRowNum 是当前表的总行数
                    {
                        IRow row = sheet.GetRow(j);  //读取当前行数据
                        if (row == null)
                            break;
                        if (row.GetCell(0) == null) //如果当前行的第一列没有数据，停止读取
                            break;
                        if (string.IsNullOrWhiteSpace(row.GetCell(0).ToString()))//如果当前行的第一列没有数据，停止读取，这是判断当row.GetCell不能null然后实际是空字符
                            break;
                        total++;
                        ICell cell = null;
                        if (row != null)
                        {
                            reason = null;
                            model = new QCS_StaInsSpeSetting();
                            cell = row.GetCell(0);//序号
                            Sequence = cell.ToString();

                            cell = row.GetCell(1);//檢驗種類
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                ParModel = ParList.Where(w => w.Code == cell.ToString() && w.ParameterTypeID == SysID + "019121300005C").FirstOrDefault();
                                if (ParModel == null)
                                {
                                    Log += "序号[" + Sequence + "]:不存在的检验种类\r\n";
                                    nosuccess++;
                                    continue; //结束本次循环
                                }
                                model.InspectionType = ParModel.ParameterID;
                            }
                            else
                            {
                                Log += "序号[" + Sequence + "]:检验种类不能为空\r\n";
                                nosuccess++;
                                continue; //结束本次循环
                            }

                            cell = row.GetCell(2);//檢驗群碼
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                ParModel = ParList.Where(w => w.Code == cell.ToString() && w.ParameterTypeID == SysID + "0191213000018").FirstOrDefault();
                                if (ParModel == null)
                                {
                                    Log += "序号[" + Sequence + "]:不存在的检验群码\r\n";
                                    nosuccess++;
                                    continue; //结束本次循环
                                }
                                model.PartID = ParModel.ParameterID;
                            }
                            else
                            {
                                Log += "序号[" + Sequence + "]:检验群码不能为空\r\n";
                                nosuccess++;
                                continue; //结束本次循环
                            }


                            cell = row.GetCell(3);//製程代號
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                ParModel = ParList.Where(w => w.Code == cell.ToString() && w.ParameterTypeID == SysID + "0191213000017").FirstOrDefault();
                                if (ParModel == null)
                                {
                                    Log += "序号[" + Sequence + "]:不存在的制程代号\r\n";
                                    nosuccess++;
                                    continue; //结束本次循环
                                }
                                model.ProcessID = ParModel.ParameterID;
                            }
                            else
                            {
                                Log += "序号[" + Sequence + "]:制程代号不能为空\r\n";
                                nosuccess++;
                                continue; //结束本次循环
                            }
                            //判断对应检验群码下是否存在对应制程的制品制程
                            if (!SFC_ItemProcessService.Qcs04GroupCheck(model.PartID, model.ProcessID))
                            {
                                Log += "序号[" + Sequence + "]:此检验群码下并没有对应的制品制程信息\r\n";
                                nosuccess++;
                                continue; //结束本次循环
                            }

                            cell = row.GetCell(4);//工序代號
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                ParModel = ParList.Where(w => w.Code == cell.ToString() && w.ParameterTypeID == SysID + "0191213000016").FirstOrDefault();
                                if (ParModel == null)
                                {
                                    Log += "序号[" + Sequence + "]:不存在的制程代号\r\n";
                                    nosuccess++;
                                    continue; //结束本次循环
                                }
                                model.OperationID = ParModel.ParameterID;

                                if (!SFC_ItemOperationService.Qcs04GroupCheck(model.PartID, model.ProcessID, model.OperationID))
                                {
                                    Log += "序号[" + Sequence + "]:此检验群码下并没有对应的制品制程工序信息\r\n";
                                    nosuccess++;
                                    continue; //结束本次循环
                                }
                            }

                            cell = row.GetCell(5);//是否檢驗
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                if (cell.ToString() == "N")
                                    IsInspection = false;
                            }

                            cell = row.GetCell(6);//序号
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                try
                                {
                                    model.Sequence = int.Parse(cell.ToString());
                                }
                                catch
                                {
                                    reason += "序号格式错误;";
                                }
                            }

                            cell = row.GetCell(7);//檢驗類別代號
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                ParModel = ParList.Where(w => w.Code == cell.ToString() && w.ParameterTypeID == SysID + "019121300001E").FirstOrDefault();
                                if (ParModel == null)
                                {
                                    Log += "序号[" + Sequence + "]:检验类别代号不存在\r\n";
                                    nosuccess++;
                                    continue; //结束本次循环
                                }
                                model.CategoryID = ParModel.ParameterID;
                            }
                            else
                            {
                                Log += "序号[" + Sequence + "]:检验类别代号不能为空\r\n";
                                nosuccess++;
                                continue; //结束本次循环
                            }

                            cell = row.GetCell(8);//檢驗方式（代号）
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                ParModel = ParList.Where(w => w.Code == cell.ToString() && w.ParameterTypeID == SysID + "019121300001B").FirstOrDefault();
                                if (ParModel == null)
                                {
                                    Log += "序号[" + Sequence + "]:检验方式不存在\r\n";
                                    nosuccess++;
                                    continue; //结束本次循环
                                }
                                model.InspectionMethod = ParModel.ParameterID;
                            }
                            else
                            {
                                Log += "序号[" + Sequence + "]:检验方式不能为空\r\n";
                                nosuccess++;
                                continue; //结束本次循环
                            }

                            cell = row.GetCell(9);//檢驗項目代號
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                InspectionModel = InspectionProjectList.Where(w => w.Code == cell.ToString()).FirstOrDefault();
                                if (InspectionModel == null)
                                {
                                    Log += "序号[" + Sequence + "]:检验项目代号错误，不存在的檢驗項目代號\r\n";
                                    nosuccess++;
                                    continue; //结束本次循环
                                }
                                model.InspectionProjectID = InspectionModel.InspectionProjectID;
                            }
                            else
                            {
                                Log += "序号[" + Sequence + "]:检验项目代号不能为空\r\n";
                                nosuccess++;
                                continue; //结束本次循环
                            }
                        }

                        cell = row.GetCell(10);//檢驗標準
                        if (cell != null)
                            model.InspectionStandard = cell.ToString();

                        cell = row.GetCell(11);//實測值設定(代号)
                        if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                        {
                            ParModel = ParList.Where(w => w.Code == cell.ToString() && w.ParameterTypeID == SysID + "0191213000052").FirstOrDefault();
                            if (ParModel == null)
                            {
                                Log += "序号[" + Sequence + "]:實測值設定不存在\r\n";
                                nosuccess++;
                                continue; //结束本次循环
                            }
                            model.Attribute = ParModel.ParameterID;
                        }
                        else
                        {
                            Log += "序号[" + Sequence + "]:實測值設定不能为空\r\n";
                            nosuccess++;
                            continue; //结束本次循环
                        }

                        cell = row.GetCell(12);//备注
                        if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                        {
                            if (cell.ToString().Length <= 150)
                                model.Comments = cell.ToString();
                            else
                            {
                                reason += "备注超过150个字;";
                            }
                        }

                        model.StaInsSpeSettingID = UniversalService.GetSerialNumber("QCS_StaInsSpeSetting");
                        model.Status = Framework.SystemID + "0201213000001";//状态
                        model.SettingType = Framework.SystemID + "020121300007C";
                        //判断唯一性
                        if (QCS_StaInsSpeSettingService.Check(model))
                        {
                            Log += "序号[" + Sequence + "]:资料重复\r\n";
                            nosuccess++;
                            continue; //结束本次循环
                        }

                        model.AQL = QCS_SamplingSettingService.getAQL(model.CategoryID, model.InspectionMethod, InspectionModel.Disadvantages);
                        if (QCS_StaInsSpeSettingService.insert(userId, model))
                        {
                            if (!string.IsNullOrWhiteSpace(reason))
                                Log += "序号[" + Sequence + "]：" + reason + "\r\n";

                            //更新此检验群码下的料品的制程/工序的检验标记
                            string FieldName = null;
                            if (model.InspectionType == Framework.SystemID + "020121300007E")
                                FieldName = "IsIP";
                            else if (model.InspectionType == Framework.SystemID + "0201213000080")
                                FieldName = "IsFPI";
                            else if (model.InspectionType == Framework.SystemID + "0201213000081")
                                FieldName = "IsOSI";

                            if (string.IsNullOrWhiteSpace(model.OperationID))
                                SFC_ItemProcessService.Qcs04Update(userId, model.ProcessID, model.PartID, FieldName, IsInspection);
                            else
                                SFC_ItemOperationService.Qcs04Update(userId, model.OperationID, model.ProcessID, model.PartID, FieldName, IsInspection);


                            success++;
                        }
                        else
                        {
                            Log += "序号[" + Sequence + "]:添加失败，未知原因,请联系开发人员\r\n";
                            nosuccess++;
                        }
                    }
                }
            }

            DateTime EndTime = DateTime.Now;

            if (nosuccess == total)
                status = "410";
            msg = string.Format(msg, total, success, nosuccess, reason);

            //如果存在错误报告
            if (!string.IsNullOrWhiteSpace(Log))
            {
                Isreason = true;
                TimeSpan ts = EndTime - StartTime;
                string Hour = UtilBussinessService.HourConversionStr(((int)ts.TotalSeconds).ToString());
                DataLogerService.ImportWriteErrLog(StartTime, EndTime, Hour, total, success, nosuccess, "Qcs00004ImportErrorLog", Log);
            }

            return new { status = status, msg = msg, Isreason = Isreason, FileName = "Qcs00004ImportErrorLog" };
        }

        /// <summary>
        /// QCS00004的导入（料品）
        /// Sam 2017年10月19日20:36:09
        /// </summary>
        /// <param name="token"></param>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static object Qcs00004ImportV2(string token, string filename)
        {
            DateTime StartTime = DateTime.Now;
            string userId = UtilBussinessService.detoken(token);
            string SysID = Framework.SystemID;
            string status = "200", msg = "共{0}条信息,{1}条信息导入成功,{2}条信息导入失败！";
            string reason = null;
            int total = 0;
            int success = 0;
            int nosuccess = 0;
            string Log = null;
            bool Isreason = false;
            if (filename.EndsWith(".xls") || filename.EndsWith(".xlsx"))
            {
                IWorkbook wk = null;
                String Sequence = null;
                QCS_StaInsSpeSetting model = null;
                QCS_InspectionProject InspectionModel = null;
                SYS_Parameters ParModel = null;
                SYS_Items ItemModel = null;
                IList<SYS_Items> ItemList = SYS_ItemsService.GetList();
                IList<SYS_Parameters> ParList = SYS_ParameterService.GetList(SysID + "019121300005C','" + SysID + "0191213000018','" + SysID + "0191213000017','" + SysID + "0191213000016','" + SysID + "019121300001E','" + SysID + "0191213000052','" + SysID + "019121300001B");
                IList<QCS_InspectionProject> InspectionProjectList = QCS_InspectionProjectService.GetList();
                SFC_ItemProcess ItemProcessModel = null;
                SFC_ItemOperation ItemOperationModel = null;
                bool IsInspection = true;
                using (FileStream fs = File.OpenRead(@filename))   //打开.xls文件
                {
                    if (filename.EndsWith(".xls"))
                        wk = new HSSFWorkbook(fs); //把xls文件中的数据写入wk中
                    else if (filename.EndsWith(".xlsx"))
                        wk = new XSSFWorkbook(fs);//把xlsx文件中的数据写入wk中

                    ISheet sheet = wk.GetSheetAt(0);   //读取第一表数据
                    for (int j = 2; j <= sheet.LastRowNum; j++)  //LastRowNum 是当前表的总行数
                    {
                        IRow row = sheet.GetRow(j);  //读取当前行数据
                        if (row == null)
                            break;
                        if (row.GetCell(0) == null) //如果当前行的第一列没有数据，停止读取
                            break;
                        if (string.IsNullOrWhiteSpace(row.GetCell(0).ToString()))//如果当前行的第一列没有数据，停止读取，这是判断当row.GetCell不能null然后实际是空字符
                            break;
                        total++;
                        ICell cell = null;
                        if (row != null)
                        {
                            reason = null;
                            model = new QCS_StaInsSpeSetting();
                            cell = row.GetCell(0);//序号
                            Sequence = cell.ToString();

                            cell = row.GetCell(1);//檢驗種類
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                ParModel = ParList.Where(w => w.Code == cell.ToString() && w.ParameterTypeID == SysID + "019121300005C").FirstOrDefault();
                                if (ParModel == null)
                                {
                                    Log += "序号[" + Sequence + "]:不存在的检验种类\r\n";
                                    nosuccess++;
                                    continue; //结束本次循环
                                }
                                model.InspectionType = ParModel.ParameterID;
                            }
                            else
                            {
                                Log += "序号[" + Sequence + "]:检验种类不能为空\r\n";
                                nosuccess++;
                                continue; //结束本次循环
                            }

                            cell = row.GetCell(2);//料号
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                ItemModel = ItemList.Where(w => w.Code == cell.ToString()).FirstOrDefault();
                                if (ItemModel == null)
                                {
                                    Log += "序号[" + Sequence + "]:不存在的料号\r\n";
                                    nosuccess++;
                                    continue; //结束本次循环
                                }
                                model.PartID = ItemModel.ItemID;
                            }
                            else
                            {
                                Log += "序号[" + Sequence + "]:料号不能为空\r\n";
                                nosuccess++;
                                continue; //结束本次循环
                            }

                            cell = row.GetCell(3);//製程代號
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                ParModel = ParList.Where(w => w.Code == cell.ToString() && w.ParameterTypeID == SysID + "0191213000017").FirstOrDefault();
                                if (ParModel == null)
                                {
                                    Log += "序号[" + Sequence + "]:不存在的制程代号\r\n";
                                    nosuccess++;
                                    continue; //结束本次循环
                                }
                                model.ProcessID = ParModel.ParameterID;
                            }
                            else
                            {
                                Log += "序号[" + Sequence + "]:制程代号不能为空\r\n";
                                nosuccess++;
                                continue; //结束本次循环
                            }
                            //根据制品+制程获取制品制程实体
                            ItemProcessModel = SFC_ItemProcessService.getByItemProcess(model.PartID, model.ProcessID);
                            if (ItemProcessModel == null)
                            {
                                Log += "序号[" + Sequence + "]:此制品下，并无此制程设定\r\n";
                                nosuccess++;
                                continue; //结束本次循环
                            }

                            cell = row.GetCell(4);//工序代號
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                ParModel = ParList.Where(w => w.Code == cell.ToString() && w.ParameterTypeID == SysID + "0191213000016").FirstOrDefault();
                                if (ParModel == null)
                                {
                                    Log += "序号[" + Sequence + "]:不存在的制程代号\r\n";
                                    nosuccess++;
                                    continue; //结束本次循环
                                }
                                model.OperationID = ParModel.ParameterID;
                                ItemOperationModel = SFC_ItemOperationService.getByItemProcess(ItemProcessModel.ItemProcessID, model.OperationID);
                                if (ItemOperationModel == null)
                                {
                                    Log += "序号[" + Sequence + "]:此制品下，并无此工序设定\r\n";
                                    nosuccess++;
                                    continue; //结束本次循环
                                }
                            }

                            cell = row.GetCell(5);//是否檢驗
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                if (cell.ToString() == "N")
                                    IsInspection = false;
                            }

                            cell = row.GetCell(6);//序号
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                try
                                {
                                    model.Sequence = int.Parse(cell.ToString());
                                }
                                catch
                                {
                                    reason += "序号格式错误;";
                                }
                            }

                            cell = row.GetCell(7);//檢驗類別代號
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                ParModel = ParList.Where(w => w.Code == cell.ToString() && w.ParameterTypeID == SysID + "019121300001E").FirstOrDefault();
                                if (ParModel == null)
                                {
                                    Log += "序号[" + Sequence + "]:检验类别代号不存在\r\n";
                                    nosuccess++;
                                    continue; //结束本次循环
                                }
                                model.CategoryID = ParModel.ParameterID;
                            }
                            else
                            {
                                Log += "序号[" + Sequence + "]:检验类别代号不能为空\r\n";
                                nosuccess++;
                                continue; //结束本次循环
                            }

                            cell = row.GetCell(8);//檢驗方式（代号）
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                ParModel = ParList.Where(w => w.Code == cell.ToString() && w.ParameterTypeID == SysID + "019121300001B").FirstOrDefault();
                                if (ParModel == null)
                                {
                                    Log += "序号[" + Sequence + "]:检验方式不存在\r\n";
                                    nosuccess++;
                                    continue; //结束本次循环
                                }
                                model.InspectionMethod = ParModel.ParameterID;
                            }
                            else
                            {
                                Log += "序号[" + Sequence + "]:检验方式不能为空\r\n";
                                nosuccess++;
                                continue; //结束本次循环
                            }

                            cell = row.GetCell(9);//檢驗項目代號
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                InspectionModel = InspectionProjectList.Where(w => w.Code == cell.ToString()).FirstOrDefault();
                                if (InspectionModel == null)
                                {
                                    Log += "序号[" + Sequence + "]:检验项目代号错误，不存在的檢驗項目代號\r\n";
                                    nosuccess++;
                                    continue; //结束本次循环
                                }
                                model.InspectionProjectID = InspectionModel.InspectionProjectID;
                            }
                            else
                            {
                                Log += "序号[" + Sequence + "]:检验项目代号不能为空\r\n";
                                nosuccess++;
                                continue; //结束本次循环
                            }
                        }

                        cell = row.GetCell(10);//檢驗標準
                        if (cell != null)
                            model.InspectionStandard = cell.ToString();

                        cell = row.GetCell(11);//實測值設定(代号)
                        if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                        {
                            ParModel = ParList.Where(w => w.Code == cell.ToString() && w.ParameterTypeID == SysID + "0191213000052").FirstOrDefault();
                            if (ParModel == null)
                            {
                                Log += "序号[" + Sequence + "]:實測值設定不存在\r\n";
                                nosuccess++;
                                continue; //结束本次循环
                            }
                            model.Attribute = ParModel.ParameterID;
                        }
                        else
                        {
                            Log += "序号[" + Sequence + "]:實測值設定不能为空\r\n";
                            nosuccess++;
                            continue; //结束本次循环
                        }

                        cell = row.GetCell(12);//备注
                        if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                        {
                            if (cell.ToString().Length <= 150)
                                model.Comments = cell.ToString();
                            else
                            {
                                reason += "备注超过150个字;";
                            }
                        }

                        model.StaInsSpeSettingID = UniversalService.GetSerialNumber("QCS_StaInsSpeSetting");
                        model.Status = Framework.SystemID + "0201213000001";//状态
                        model.SettingType = Framework.SystemID + "020121300007B";

                        //判断唯一性
                        if (QCS_StaInsSpeSettingService.Check(model))
                        {
                            Log += "序号[" + Sequence + "]:资料重复\r\n";
                            nosuccess++;
                            continue; //结束本次循环
                        }
                        model.AQL = QCS_SamplingSettingService.getAQL(model.CategoryID, model.InspectionMethod, InspectionModel.Disadvantages);
                        if (QCS_StaInsSpeSettingService.insert(userId, model))
                        {
                            if (!string.IsNullOrWhiteSpace(reason))
                                Log += "序号[" + Sequence + "]：" + reason + "\r\n";

                            //更新此检验群码下的料品的制程/工序的检验标记
                            string FieldName = null;
                            if (model.InspectionType == Framework.SystemID + "020121300007E")
                                FieldName = "IsIP";
                            else if (model.InspectionType == Framework.SystemID + "0201213000080")
                                FieldName = "IsFPI";
                            else if (model.InspectionType == Framework.SystemID + "0201213000081")
                                FieldName = "IsOSI";

                            if (string.IsNullOrWhiteSpace(model.OperationID))
                                SFC_ItemProcessService.Qcs04ItemUpdate(userId, ItemProcessModel.ItemProcessID, FieldName, IsInspection);
                            else
                                SFC_ItemOperationService.Qcs04ItemUpdate(userId, ItemOperationModel.ItemOperationID, FieldName, IsInspection);

                            success++;
                        }
                        else
                        {
                            Log += "序号[" + Sequence + "]:添加失败，未知原因,请联系开发人员\r\n";
                            nosuccess++;
                        }
                    }
                }
            }

            DateTime EndTime = DateTime.Now;

            if (nosuccess == total)
                status = "410";
            msg = string.Format(msg, total, success, nosuccess, reason);

            //如果存在错误报告
            if (!string.IsNullOrWhiteSpace(Log))
            {
                Isreason = true;
                TimeSpan ts = EndTime - StartTime;
                string Hour = UtilBussinessService.HourConversionStr(((int)ts.TotalSeconds).ToString());
                DataLogerService.ImportWriteErrLog(StartTime, EndTime, Hour, total, success, nosuccess, "Qcs00004ImportErrorLog", Log);
            }

            return new { status = status, msg = msg, Isreason = Isreason, FileName = "Qcs00004ImportErrorLog" };
        }

        /// <summary>
        /// 料品的导入
        /// Sam 2017年10月19日16:14:20
        /// 具体逻辑调整
        /// </summary>
        /// <param name="token"></param>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static object Inf00010ImportV1(string token, string filename)
        {
            DateTime StartTime = DateTime.Now;
            string userId = UtilBussinessService.detoken(token);
            string SysID = Framework.SystemID;
            string status = "200", msg = "共{0}条信息,{1}条信息导入成功,{2}条信息导入失败！";
            int total = 0;
            int success = 0;
            int nosuccess = 0;
            string reason = null;
            string Log = null;
            bool Isreason = false;
            if (filename.EndsWith(".xls") || filename.EndsWith(".xlsx"))
            {
                IWorkbook wk = null;
                string Sequence = null;
                SYS_Items model = null;
                SYS_Parameters ParModel = null;
                SYS_AutoNumber AutoModel = null;
                IList<SYS_Parameters> ParList = SYS_ParameterService.GetList(
                        SysID + "0191213000001','" + SysID + "019121300000C','" +
                        SysID + "019121300000B','" + SysID + "0191213000002','" +
                        SysID + "0191213000003','" + SysID + "0191213000053','" +
                        SysID + "0191213000060','" + SysID + "0191213000018");
                List<string> ClassList = null;
                using (FileStream fs = File.OpenRead(@filename))   //打开.xls文件
                {
                    if (filename.EndsWith(".xls"))
                        wk = new HSSFWorkbook(fs); //把xls文件中的数据写入wk中
                    else if (filename.EndsWith(".xlsx"))
                        wk = new XSSFWorkbook(fs);//把xlsx文件中的数据写入wk中

                    ISheet sheet = wk.GetSheetAt(0);   //读取第一表数据
                    for (int j = 2; j <= sheet.LastRowNum; j++)  //LastRowNum 是当前表的总行数
                    {
                        IRow row = sheet.GetRow(j);  //读取当前行数据
                        if (row == null)
                            break;
                        if (row.GetCell(0) == null) //如果当前行的第一列没有数据，停止读取
                            break;
                        if (string.IsNullOrWhiteSpace(row.GetCell(0).ToString()))//如果当前行的第一列没有数据，停止读取，这是判断当row.GetCell不能null然后实际是空字符
                            break;
                        total++;
                        ICell cell = null;
                        if (row != null)
                        {
                            model = new SYS_Items();
                            ClassList = new List<string>();

                            cell = row.GetCell(0);//序号
                            Sequence = cell.ToString();
                            reason = null;
                            cell = row.GetCell(1);//料品代号
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                if (cell.ToString().Length <= 60)
                                {
                                    if (!SYS_ItemsService.CheckCode(cell.ToString().Trim(), null))
                                    {
                                        model.Code = cell.ToString();
                                    }
                                    else
                                    {
                                        Log += "序号[" + Sequence + "]：料品代号已存在。\r\n";
                                        nosuccess++;
                                        continue;
                                    }
                                }
                                else
                                {
                                    Log += "序号[" + Sequence + "]:料品代号超过了60个字。\r\n";
                                    nosuccess++;
                                    continue; //结束本次循环
                                }
                            }
                            else
                            {
                                Log += "序号[" + Sequence + "]:料品代号为空。\r\n";
                                nosuccess++;
                                continue; //结束本次循环
                            }
                            cell = row.GetCell(2);//名称
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                if (cell.ToString().Length <= 120)
                                    model.Name = cell.ToString();
                                else
                                {
                                    Log += "序号[" + Sequence + "]：料品名称超过了120个字。\r\n";
                                    nosuccess++;
                                    continue; //结束本次循环
                                }
                            }
                            else
                            {
                                Log += "序号[" + Sequence + "]:料品名称为空。\r\n";
                                nosuccess++;
                                continue; //结束本次循环
                            }

                            cell = row.GetCell(3);//料品规格
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                if (cell.ToString().Length <= 120)
                                    model.Specification = cell.ToString();
                                else
                                    reason += "料品规格超过了120个字;";
                            }

                            cell = row.GetCell(4);//状态
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                ParModel = ParList.Where(w => w.Code == cell.ToString() && w.ParameterTypeID == SysID + "0191213000001").FirstOrDefault();
                                if (ParModel != null)
                                    model.Status = ParModel.ParameterID;
                                else
                                {
                                    model.Status = SysID + "0201213000001";
                                    reason += "状态错误,不存在的状态;";
                                }
                            }
                            else
                            {
                                model.Status = SysID + "0201213000001";
                                reason += "状态为空;";
                            }

                            cell = row.GetCell(5);//单位
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                ParModel = ParList.Where(w => w.Code == cell.ToString() && w.ParameterTypeID == SysID + "019121300000C").FirstOrDefault();
                                if (ParModel != null)
                                    model.Unit = ParModel.ParameterID;
                                else
                                {
                                    Log += "序号[" + Sequence + "]:单位错误,不存在的单位。\r\n";
                                    nosuccess++;
                                    continue; //结束本次循环
                                }
                            }
                            else
                            {
                                Log += "序号[" + Sequence + "]:单位错误,不存在的单位。\r\n";
                                nosuccess++;
                                continue; //结束本次循环
                            }

                            cell = row.GetCell(6);//分类一
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                ParModel = ParList.Where(w => w.Code == cell.ToString() && w.ParameterTypeID == SysID + "019121300000B").FirstOrDefault();
                                if (ParModel != null)
                                {
                                    model.ClassOne = ParModel.ParameterID;
                                    ClassList.Add(model.ClassOne);
                                }
                                else
                                    reason += "分类一错误,不存在的分类;";
                            }


                            cell = row.GetCell(7);//分类二
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                ParModel = ParList.Where(w => w.Code == cell.ToString() && w.ParameterTypeID == SysID + "019121300000B").FirstOrDefault();
                                if (ParModel != null)
                                {
                                    model.ClassTwo = ParModel.ParameterID;
                                    ClassList.Add(model.ClassTwo);
                                }
                                else
                                    reason += "分类二错误,不存在的分类;";
                            }


                            cell = row.GetCell(8);//分类三
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                ParModel = ParList.Where(w => w.Code == cell.ToString() && w.ParameterTypeID == SysID + "019121300000B").FirstOrDefault();
                                if (ParModel != null)
                                {
                                    model.ClassThree = ParModel.ParameterID;
                                    ClassList.Add(model.ClassThree);
                                }
                                else
                                    reason += "分类三错误,不存在的分类;";
                            }

                            cell = row.GetCell(9);//分类四
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                ParModel = ParList.Where(w => w.Code == cell.ToString() && w.ParameterTypeID == SysID + "019121300000B").FirstOrDefault();
                                if (ParModel != null)
                                {
                                    model.ClassFour = ParModel.ParameterID;
                                    ClassList.Add(model.ClassFour);
                                }
                                else
                                    reason += "分类四错误,不存在的分类;";
                            }

                            cell = row.GetCell(10);//分类五
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                ParModel = ParList.Where(w => w.Code == cell.ToString() && w.ParameterTypeID == SysID + "019121300000B").FirstOrDefault();
                                if (ParModel != null)
                                {
                                    model.ClassFive = ParModel.ParameterID;
                                    ClassList.Add(model.ClassFive);
                                }
                                else
                                    reason += "分类五错误,不存在的分类;";
                            }


                            cell = row.GetCell(11);//辅助单位
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                ParModel = ParList.Where(w => w.Code == cell.ToString() && w.ParameterTypeID == SysID + "019121300000C").FirstOrDefault();
                                if (ParModel != null)
                                    model.AuxUnit = ParModel.ParameterID;
                                else
                                {
                                    Log += "序号[" + Sequence + "]:辅助单位错误,不存在的辅助单位。\r\n";
                                    nosuccess++;
                                    continue; //结束本次循环
                                }
                            }
                            else
                            {
                                Log += "序号[" + Sequence + "]:辅助单位不能为空。\r\n";
                                nosuccess++;
                                continue; //结束本次循环
                            }

                            cell = row.GetCell(12);//辅助单位比
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                try
                                {
                                    if (model.AuxUnit == model.Unit)
                                    {
                                        model.AuxUnitRatio = 1;
                                    }
                                    else
                                    {
                                        model.AuxUnitRatio = decimal.Parse(cell.ToString());

                                        if (model.AuxUnitRatio == 1 || model.AuxUnitRatio == 0)
                                        {
                                            Log += "序号[" + Sequence + "]:辅助单位比错误，辅助单位跟单位不一致，不能等于1或者0。\r\n";
                                            nosuccess++;
                                            continue; //结束本次循环
                                        }
                                        if (model.AuxUnitRatio > decimal.Parse("99999.9999") || model.AuxUnitRatio <= decimal.Parse("0.0001"))
                                        {
                                            Log += "序号[" + Sequence + "]:辅助单位比错误，超出了范围。\r\n";
                                            nosuccess++;
                                            continue; //结束本次循环
                                        }
                                    }
                                }
                                catch
                                {
                                    Log += "序号[" + Sequence + "]:辅助单位比错误,格式错误。\r\n";
                                    nosuccess++;
                                    continue; //结束本次循环
                                }
                            }
                            else
                            {
                                Log += "序号[" + Sequence + "]:辅助单位比不能为空。\r\n";
                                nosuccess++;
                                continue; //结束本次循环
                            }

                            cell = row.GetCell(13);//切除尾数
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                if (cell.ToString() == "1")
                                    model.IsCutMantissa = true;
                                else if (cell.ToString() == "0")
                                    model.IsCutMantissa = false;
                                else
                                {
                                    model.IsCutMantissa = false;
                                    reason += "切除尾数字段错误,数据并不是“1”或“0”;";
                                }
                            }
                            else
                            {
                                model.IsCutMantissa = false;
                                reason += "切除尾数字段错误,数据为空;";
                            }

                            cell = row.GetCell(14);//切除尾数小数位
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                ParModel = ParList.Where(w => w.Code == cell.ToString() && w.ParameterTypeID == SysID + "0191213000002").FirstOrDefault();
                                if (ParModel != null)
                                    model.CutMantissa = ParModel.ParameterID;
                                else
                                {
                                    model.CutMantissa = SysID + "020121300000C";
                                    reason += "切除尾数小数错误,不存在的位数;";
                                }
                            }
                            else
                                model.CutMantissa = SysID + "020121300000C";


                            cell = row.GetCell(15);//供应型态
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                ParModel = ParList.Where(w => w.Code == cell.ToString() && w.ParameterTypeID == SysID + "0191213000003").FirstOrDefault();
                                if (ParModel != null)
                                    model.Type = ParModel.ParameterID;
                                else
                                {
                                    model.Type = SysID + "0201213000008";
                                    reason += "供应型态错误,不存在的供应型态;";
                                }
                            }
                            else
                                model.Type = SysID + "0201213000008";

                            cell = row.GetCell(16);//工程图号
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                if (cell.ToString().Length <= 30)
                                    model.Drawing = cell.ToString();
                                else
                                    reason += "工程图号超过了30个字;";
                            }
                            cell = row.GetCell(17);//料品来源
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                ParModel = ParList.Where(w => w.Code == cell.ToString() && w.ParameterTypeID == SysID + "0191213000053").FirstOrDefault();
                                if (ParModel != null)
                                    model.PartSource = ParModel.ParameterID;
                                else
                                {
                                    model.PartSource = SysID + "020121300001A";
                                    reason += "料品来源错误,不存在的料品来源;";
                                }
                            }
                            else
                                model.PartSource = SysID + "020121300001A";

                            cell = row.GetCell(18);//备注
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                if (cell.ToString().Length <= 120)
                                    model.Comments = cell.ToString();
                                else
                                    reason += "备注超过了120个字;";
                            }

                            cell = row.GetCell(19);//条码
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                if (cell.ToString().Length <= 30)
                                    model.BarCord = cell.ToString();
                                else
                                    reason += "条码超过了30个字;";
                            }
                            cell = row.GetCell(20);//检验群组
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                ParModel = ParList.Where(w => w.Code == cell.ToString() && w.ParameterTypeID == SysID + "0191213000018").FirstOrDefault();
                                if (ParModel != null)
                                    model.GroupID = ParModel.ParameterID;
                                else
                                {
                                    reason += "检验群组错误,不存在的检验群组;";
                                }
                            }

                            cell = row.GetCell(21);//批号管控
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                if (cell.ToString() == "是")
                                    model.Lot = true;
                                else if (cell.ToString() == "否")
                                    model.Lot = false;
                                else
                                {
                                    model.Lot = false;
                                    reason += "批号管控字段错误,数据并不是“是”或“否”;";
                                }
                            }
                            else
                                model.Lot = false;

                            cell = row.GetCell(22);//批号类别
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                AutoModel = SYS_AutoNumberService.getByCode(cell.ToString());
                                if (AutoModel != null)
                                    model.LotClassID = AutoModel.AutoNumberID;
                                else
                                {
                                    reason += "批号类别错误,不存在的批号类别;";
                                }
                            }

                            cell = row.GetCell(23);//组批方式
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                ParModel = ParList.Where(w => w.Code == cell.ToString() && w.ParameterTypeID == SysID + "0191213000060").FirstOrDefault();
                                if (ParModel != null)
                                    model.LotMethod = ParModel.ParameterID;
                                else
                                {
                                    reason += "组批方式错误,不存在的组批方式;";
                                }
                            }


                            cell = row.GetCell(24);//超完工比率（%）
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                try
                                {
                                    model.OverRate = decimal.Parse(cell.ToString().Replace("%", ""));
                                    if (model.OverRate != 0)
                                    {
                                        if (model.OverRate > decimal.Parse("999.99") || model.OverRate <= decimal.Parse("0.01"))
                                        {
                                            model.OverRate = 0;
                                            reason += "超完工比率错误，超出了范围;";
                                        }
                                    }
                                }
                                catch
                                {
                                    reason += "超完工比率错误,格式错误;";
                                }
                            }

                            cell = row.GetCell(25);//序号件
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                if (cell.ToString() == "是")
                                    model.SerialPart = true;
                                else if (cell.ToString() == "否")
                                    model.SerialPart = false;
                                else
                                {
                                    model.SerialPart = false;
                                    reason += "序号件字段错误,数据并不是“是”或“否”;";
                                }
                            }
                            else
                                model.SerialPart = false;

                            cell = row.GetCell(26);//关键料号
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                if (cell.ToString() == "是")
                                    model.KeyPart = true;
                                else if (cell.ToString() == "否")
                                    model.KeyPart = false;
                                else
                                {
                                    model.KeyPart = false;
                                    reason += "关键料号字段错误,数据并不是“是”或“否”;";
                                }
                            }
                            else
                                model.KeyPart = false;
                        }


                        int count = ClassList.Count;
                        if (count != 0)
                        {
                            int NCount = ClassList.Distinct().ToList().Count;
                            if (count != NCount)
                            {
                                Log += "序号[" + Sequence + "]:5个分类存在重复项\r\n";
                                nosuccess++;
                                continue; //结束本次循环
                            }
                        }

                        model.ItemID = UniversalService.GetSerialNumber("SYS_Items");
                        if (SYS_ItemsService.CheckCode(model.Code, null))
                        {
                            Log += "序号[" + Sequence + "]：料品代号已存在。\r\n";
                            nosuccess++;
                            continue;
                        }
                        if (SYS_ItemsService.insert(userId, model))
                        {
                            if (!string.IsNullOrWhiteSpace(reason))
                                Log += "序号[" + Sequence + "]：" + reason + "。\r\n";
                            success++;
                        }
                        else
                        {
                            Log += "序号[" + Sequence + "]:添加失败，未知原因,请与开发人员联系。\r\n";
                            nosuccess++;
                        }
                    }
                }
            }
            DateTime EndTime = DateTime.Now;

            if (nosuccess == total)
                status = "410";

            //如果存在错误报告
            if (!string.IsNullOrWhiteSpace(Log))
            {
                Isreason = true;
                TimeSpan ts = EndTime - StartTime;
                string Hour = UtilBussinessService.HourConversionStr(((int)ts.TotalSeconds).ToString());
                DataLogerService.ImportWriteErrLog(StartTime, EndTime, Hour, total, success, nosuccess, "Inf00010ImportErrorLog", Log);
            }

            msg = string.Format(msg, total, success, nosuccess, reason);

            return new { status = status, msg = msg, Isreason = Isreason, FileName = "Inf00010ImportErrorLog" };
        }

        /// <summary>
        /// 设备项目的导入
        /// SAM 2017年10月23日20:29:35
        /// 根据要求，导入模板调整
        /// </summary>
        /// <param name="token"></param>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static object Ems00001ProjectImportV1(string token, string filename)
        {
            DateTime StartTime = DateTime.Now;
            string userId = UtilBussinessService.detoken(token);
            string SysID = Framework.SystemID;
            string status = "200", msg = "共{0}条信息,{1}条信息导入成功,{2}条信息导入失败！";
            string reason = null;
            int total = 0;
            int success = 0;
            int nosuccess = 0;
            string Log = null;
            bool Isreason = false;
            if (filename.EndsWith(".xls") || filename.EndsWith(".xlsx"))
            {
                IWorkbook wk = null;
                string Sequence = null;
                EMS_EquipmentProject model = null;
                EMS_Equipment Eqmodel = null;
                SYS_Parameters ParModel = null;
                SYS_Projects Promodel = null;
                IOT_Sensor SenModel = null;
                using (FileStream fs = File.OpenRead(@filename))   //打开.xls文件
                {
                    if (filename.EndsWith(".xls"))
                        wk = new HSSFWorkbook(fs); //把xls文件中的数据写入wk中
                    else if (filename.EndsWith(".xlsx"))
                        wk = new XSSFWorkbook(fs);//把xlsx文件中的数据写入wk中

                    ISheet sheet = wk.GetSheetAt(0);   //读取第一表数据

                    for (int j = 2; j <= sheet.LastRowNum; j++)  //LastRowNum 是当前表的总行数
                    {
                        IRow row = sheet.GetRow(j);  //读取当前行数据
                        if (row == null)
                            break;
                        if (row.GetCell(0) == null) //如果当前行的第一列没有数据，停止读取
                            break;
                        if (string.IsNullOrWhiteSpace(row.GetCell(0).ToString()))//如果当前行的第一列没有数据，停止读取，这是判断当row.GetCell不能null然后实际是空字符
                            break;
                        total++;
                        ICell cell = null;
                        if (row != null)
                        {
                            model = new EMS_EquipmentProject();
                            cell = row.GetCell(0);//序号
                            Sequence = cell.ToString();
                            reason = null;
                            cell = row.GetCell(1);//设备代号
                            if (cell != null)
                            {
                                Eqmodel = EMS_EquipmentService.getByCode(cell.ToString());
                                if (Eqmodel != null)
                                    model.EquipmentID = Eqmodel.EquipmentID;
                                else
                                {
                                    Log += "序号[" + Sequence + "]:设备代号错误，不存在的设备。\r\n";
                                    nosuccess++;
                                    continue; //结束本次循环
                                }
                            }
                            else
                            {
                                Log += "序号[" + Sequence + "]:设备代号为空。\r\n";
                                nosuccess++;
                                continue; //结束本次循环
                            }

                            cell = row.GetCell(2);//项目代号
                            if (cell != null)
                            {
                                Promodel = SYS_ProjectsService.getByCode(cell.ToString());
                                if (Promodel != null)
                                    model.ProjectID = Promodel.ProjectID;
                                else
                                {
                                    Log += "序号[" + Sequence + "]项目代号错误，不存在的项目代号。\r\n";
                                    nosuccess++;
                                    continue; //结束本次循环
                                }
                            }
                            else
                            {
                                Log += "序号[" + Sequence + "]项目代号为空。\r\n";
                                nosuccess++;
                                continue; //结束本次循环
                            }

                            cell = row.GetCell(3);//是否收集
                            if (cell != null)
                            {
                                if (cell.ToString() == "Y")
                                    model.IfCollection = true;
                                else if (cell.ToString() == "N")
                                    model.IfCollection = false;
                                else
                                {
                                    model.IfCollection = true;
                                    reason += "是否收集字段错误，只能是“Y”或者“N”;";
                                }
                            }
                            else
                            {
                                model.IfCollection = true;
                                reason += "是否收集字段为空;";
                            }


                            cell = row.GetCell(4);//收集方式
                            if (cell != null)
                            {
                                ParModel = SYS_ParameterService.getByCode(cell.ToString(), Framework.SystemID + "0191213000056");
                                if (ParModel != null)
                                    model.CollectionWay = ParModel.ParameterID;
                                else
                                {
                                    reason += "收集方式错误,不存在的收集方式;";
                                }
                            }

                            cell = row.GetCell(5);//感知器代号
                            if (cell != null)
                            {
                                SenModel = IOT_SensorService.getByCode(cell.ToString());
                                if (SenModel != null)
                                    model.SensorID = SenModel.SensorID;
                                else
                                {
                                    reason += "感知器错误,不存在的感知器;";
                                }
                            }

                            cell = row.GetCell(6);//标准值
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                if (Promodel.Attribute == "0201213000017")
                                {
                                    try
                                    {
                                        int Attribute = int.Parse(cell.ToString());
                                        model.StandardValue = cell.ToString();
                                    }
                                    catch
                                    {
                                        reason += "标准值格式错误,只能是数字类型;";
                                    }
                                }
                                else
                                {
                                    model.StandardValue = cell.ToString();
                                }
                            }
                            cell = row.GetCell(7);//上限值
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                if (Promodel.Attribute == "0201213000017")
                                {
                                    try
                                    {
                                        int Attribute = int.Parse(cell.ToString());
                                        model.MaxValue = cell.ToString();
                                    }
                                    catch
                                    {
                                        reason += "标准值格式错误,只能是数字类型;";
                                    }
                                }
                                else
                                {
                                    model.MaxValue = cell.ToString();
                                }
                            }
                            cell = row.GetCell(8);//下限值
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                if (Promodel.Attribute == "0201213000017")
                                {
                                    try
                                    {
                                        int Attribute = int.Parse(cell.ToString());
                                        model.MinValue = cell.ToString();
                                    }
                                    catch
                                    {
                                        reason += "标准值格式错误,只能是数字类型;";
                                    }
                                }
                                else
                                {
                                    model.MinValue = cell.ToString();
                                }
                            }

                            cell = row.GetCell(9);//上限警告數值
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                if (cell.ToString().Length <= 120)
                                    model.MaxAlarmValue = cell.ToString();
                                else
                                    reason += "上限警告數值大于120个字;";
                            }


                            cell = row.GetCell(10);//下限警告數值
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                if (cell.ToString().Length <= 120)
                                    model.MinAlarmValue = cell.ToString();
                                else
                                    reason += "下限警告數值大于120个字;";
                            }


                            cell = row.GetCell(11);//上限警告秒數
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                try
                                {
                                    model.MaxAlarmTime = int.Parse(cell.ToString());
                                }
                                catch
                                {
                                    model.MaxAlarmTime = 0;
                                    reason += "上限警告秒數格式错误;";
                                }
                            }


                            cell = row.GetCell(12);//下限警告秒數
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                try
                                {
                                    model.MinAlarmTime = int.Parse(cell.ToString());
                                }
                                catch
                                {
                                    model.MinAlarmTime = 0;
                                    reason += "上限警告秒數格式错误;";
                                }
                            }

                            cell = row.GetCell(13);//备注
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                if (cell.ToString().Length <= 120)
                                    model.Comments = cell.ToString();
                                else
                                    reason += "备注大于120个字;";
                            }
                        }

                        if (!EMS_EquipmentProjectService.Check(model.SensorID, model.EquipmentID, model.ProjectID, null))
                        {
                            model.EquipmentProjectID = UniversalService.GetSerialNumber("EMS_EquipmentProject");
                            model.Status = SysID + "0201213000001";
                            if (EMS_EquipmentProjectService.insert(userId, model))
                            {
                                if (!string.IsNullOrWhiteSpace(reason))
                                    Log += "序号[" + Sequence + "]：" + reason + "\r\n";
                                success++;
                            }
                            else
                            {
                                Log += "序号[" + Sequence + "]:添加失败，未知原因,请与开发人员联系。\r\n";
                                nosuccess++;
                            }
                        }
                        else
                        {
                            Log += "序号[" + Sequence + "]:设备项目资料重复。\r\n";
                            nosuccess++;
                        }
                    }
                }
            }
            DateTime EndTime = DateTime.Now;

            if (nosuccess == total)
                status = "410";

            //如果存在错误报告
            if (!string.IsNullOrWhiteSpace(Log))
            {
                Isreason = true;
                TimeSpan ts = EndTime - StartTime;
                string Hour = UtilBussinessService.HourConversionStr(((int)ts.TotalSeconds).ToString());
                DataLogerService.ImportWriteErrLog(StartTime, EndTime, Hour, total, success, nosuccess, "EMS00001ImportErrorLog", Log);
            }

            msg = string.Format(msg, total, success, nosuccess, reason);

            return new { status = status, msg = msg, Isreason = Isreason, FileName = "EMS00001ImportErrorLog" };
        }

        /// <summary>
        /// 制令单的导入
        /// SAM 2017年9月29日15:02:15
        /// </summary>
        /// <param name="token"></param>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static object Sfc00002ImportV1(string token, string filename)
        {
            string userId = UtilBussinessService.detoken(token);
            string SysID = Framework.SystemID;
            string status = "200", msg = "共{0}条信息,{1}条信息导入成功,{2}条信息导入失败！{3}";
            string reason = null;
            int total = 0;
            int success = 0;
            int nosuccess = 0;
            if (filename.EndsWith(".xls") || filename.EndsWith(".xlsx"))
            {
                IWorkbook wk = null;
                string Sequence = null;
                string AddReason = null;
                using (FileStream fs = File.OpenRead(@filename))   //打开.xls文件
                {
                    if (filename.EndsWith(".xls"))
                        wk = new HSSFWorkbook(fs); //把xls文件中的数据写入wk中
                    else if (filename.EndsWith(".xlsx"))
                        wk = new XSSFWorkbook(fs);//把xlsx文件中的数据写入wk中

                    ISheet sheet = wk.GetSheetAt(0);   //读取第一表数据
                    SFC_FabricatedMother FabMo = null;
                    SYS_Customers CustModel = null;
                    SYS_Items ItemModel = null;
                    //改进点1：事先把所有的客户和所有的料品都查询出来，舰少判断时查询数据库的次数
                    IList<SYS_Customers> CustList = SYS_CustomersService.GetList();
                    IList<SYS_Items> ItemList = SYS_ItemsService.GetList();
                    //改进点2：在这里执行一个函数，将所有的制品制程等信息在这里直接获取好。
                    Sfc00002ImportBussinessService.Initialization();
                    for (int j = 2; j <= sheet.LastRowNum; j++)  //LastRowNum 是当前表的总行数
                    {
                        IRow row = sheet.GetRow(j);  //读取当前行数据
                        if (row == null)
                            break;
                        if (row.GetCell(0) == null) //如果当前行的第一列没有数据，停止读取
                            break;
                        if (string.IsNullOrWhiteSpace(row.GetCell(0).ToString()))//如果当前行的第一列没有数据，停止读取，这是判断当row.GetCell不能null然后实际是空字符
                            break;
                        total++;
                        ICell cell = null;
                        if (row != null)
                        {
                            FabMo = new SFC_FabricatedMother();
                            CustModel = null;
                            ItemModel = null;
                            cell = row.GetCell(0);//序号
                            Sequence = cell.ToString();

                            cell = row.GetCell(1);//投产日期/預計開工日
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                try
                                {
                                    FabMo.StartDate = DateTime.Parse(cell.DateCellValue.ToString());
                                }
                                catch
                                {
                                    reason += "序号[" + Sequence + "]:投产日期格式错误;";
                                    nosuccess++;
                                    continue; //结束本次循环
                                }
                            }
                            else
                            {
                                reason += "序号[" + Sequence + "]:完工日期不能为空;";
                                nosuccess++;
                                continue; //结束本次循环
                            }

                            cell = row.GetCell(2);//投产比例/超完工比率
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                try
                                {
                                    FabMo.OverRate = 100 - decimal.Parse(cell.ToString());
                                }
                                catch
                                {
                                    reason += "序号[" + Sequence + "]:投产比例格式错误;";
                                    nosuccess++;
                                    continue; //结束本次循环
                                }
                            }


                            cell = row.GetCell(3);//客户名/客戶代號
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                CustModel = CustList.Where(w => w.Code == cell.ToString()).FirstOrDefault();
                                //CustModel = SYS_CustomersService.getByCode(cell.ToString());
                                if (CustModel != null)
                                    FabMo.CustomerID = CustModel.CustomerID;
                                else
                                    reason += "序号[" + Sequence + "]:客户代号错误,不存在的客户信息;";
                            }

                            cell = row.GetCell(4);//工单号/製令單號
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                FabMo.MoNo = cell.ToString();
                            }
                            else
                            {
                                reason += "序号[" + Sequence + "]:工单号不能为空;";
                                nosuccess++;
                                continue; //结束本次循环
                            }

                            cell = row.GetCell(5);//订单号/訂單號碼
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                                FabMo.OrderNo = cell.ToString();

                            cell = row.GetCell(6);//产品型号/料品代號
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                ItemModel = ItemList.Where(w => w.Code == cell.ToString()).FirstOrDefault();
                                //ItemModel = SYS_ItemsService.getByCode(cell.ToString());
                                if (ItemModel != null)
                                    FabMo.ItemID = ItemModel.ItemID;
                                else
                                {
                                    reason += "序号[" + Sequence + "]:产品型号错误，不存在的产品信息;";
                                    nosuccess++;
                                    continue; //结束本次循环
                                }
                            }
                            else
                            {
                                reason += "序号[" + Sequence + "]:产品型号不能为空;";
                                nosuccess++;
                                continue; //结束本次循环
                            }

                            cell = row.GetCell(7);//投单数量/製造數量
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                try
                                {
                                    FabMo.Quantity = decimal.Parse(cell.ToString());
                                }
                                catch
                                {
                                    reason += "序号[" + Sequence + "]:投单数量格式错误;";
                                    nosuccess++;
                                    continue; //结束本次循环
                                }
                            }
                            else
                            {
                                reason += "序号[" + Sequence + "]:投单数量不能为空;";
                                nosuccess++;
                                continue; //结束本次循环
                            }

                            cell = row.GetCell(17);//预定交期/預計出貨日
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                try
                                {
                                    FabMo.ShipmentDate = DateTime.Parse(cell.DateCellValue.ToString());
                                }
                                catch
                                {
                                    reason += "序号[" + Sequence + "]:预定交期格式错误;";
                                }
                            }

                            cell = row.GetCell(18);//交货数量/訂單數量
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                try
                                {
                                    FabMo.OrderQuantity = decimal.Parse(cell.ToString());
                                }
                                catch
                                {
                                    reason += "序号[" + Sequence + "]:交货数量格式错误;";
                                    nosuccess++;
                                    continue; //结束本次循环
                                }
                            }

                            cell = row.GetCell(26);//特殊要求/备注
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                if (cell.ToString().Length <= 120)
                                    FabMo.Comments = cell.ToString();
                                else
                                    reason += "序号[" + Sequence + "]:特殊要求超过了120个字;";
                            }
                        }


                        if (SFC_FabricatedMotherService.Check(FabMo.MoNo))
                        {
                            reason += "序号[" + Sequence + "]:已存在的工单号;";
                            nosuccess++;
                            continue; //结束本次循环
                        }

                        FabMo.FabricatedMotherID = UniversalService.GetSerialNumber("SFC_FabricatedMother");
                        FabMo.Status = Framework.SystemID + "0201213000028";
                        FabMo.Date = DateTime.Now;
                        FabMo.Version = "0";
                        FabMo.UnitID = ItemModel.Unit;
                        AddReason = null;
                        if (Sfc00002ImportBussinessService.FabMo(userId, FabMo, ItemModel, ref AddReason))
                            success++;
                        else
                        {
                            reason += "序号[" + Sequence + "]:导入失败!" + AddReason + ";";
                            nosuccess++;
                        }
                    }
                }
            }
            if (nosuccess == total)
                status = "410";

            msg = string.Format(msg, total, success, nosuccess, reason);

            return new { status = status, msg = msg };
        }

        /// <summary>
        /// 制品制程的导入
        /// SAM 2017年10月26日14:35:16 
        /// V1版本,添加错误日志的产生
        /// </summary>
        /// <param name="token"></param>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static object Sfc00001ImportV1(string token, string filename)
        {
            DateTime StartTime = DateTime.Now;
            DateTime EndTime = DateTime.Now;
            string userId = UtilBussinessService.detoken(token);
            string SysID = Framework.SystemID;
            string status = "200", msg = "共{0}条信息,{1}条信息导入成功,{2}条信息导入失败！";
            string reason = null;
            string Log = null;
            string Allmsg = null;
            bool Isreason = false;
            bool New = true;
            bool IsImport = false;
            int total = 0;
            int success = 0;
            int nosuccess = 0;
            IList<SYS_Items> ItemList = SYS_ItemsService.GetList();
            IList<SYS_Parameters> ParList = SYS_ParameterService.GetList(SysID + "0191213000017','" + SysID + "019121300000C','" + SysID + "0191213000016");
            IList<SYS_WorkCenter> WCList = SYS_WorkCenterService.GetList();
            string Sequence = null;//标识当前行

            string ItemID = null;
            string ProcessID = null;
            string OperationID = null;
            if (filename.EndsWith(".xls") || filename.EndsWith(".xlsx"))
            {
                IWorkbook wk = null;
                using (FileStream fs = File.OpenRead(@filename))   //打开.xls文件
                {
                    if (filename.EndsWith(".xls"))
                        wk = new HSSFWorkbook(fs); //把xls文件中的数据写入wk中
                    else if (filename.EndsWith(".xlsx"))
                        wk = new XSSFWorkbook(fs);//把xlsx文件中的数据写入wk中

                    //制品制程
                    SFC_ItemProcess model = null;
                    SYS_Items Itemmodel = null;
                    SYS_Parameters Parmodel = null;
                    SYS_WorkCenter WCmodel = null;
                    SFC_ItemOperation OpeModel = null;
                    try
                    {
                        ISheet sheet = wk.GetSheetAt(0);   //读取第一表数据
                        for (int j = 2; j <= sheet.LastRowNum; j++)  //LastRowNum 是当前表的总行数
                        {
                            IRow row = sheet.GetRow(j);  //读取当前行数据
                            if (row == null) //如果当前行的第一列没有数据，停止读取
                                break;
                            if (row.GetCell(0) == null) //如果当前行的第一列没有数据，停止读取
                                break;
                            //如果当前行的第一列没有数据，停止读取，这是判断当row.GetCell不能null然后实际是空字符
                            if (string.IsNullOrWhiteSpace(row.GetCell(0).ToString()))
                                break;
                            total++;
                            IsImport = true;

                            ICell cell = null;
                            if (row != null)
                            {
                                model = new SFC_ItemProcess();
                                reason = null;
                                Sequence = (j + 1).ToString();
                                cell = row.GetCell(0);//料號
                                if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                                {
                                    Itemmodel = ItemList.Where(w => w.Code == cell.ToString().Trim()).FirstOrDefault();
                                    if (Itemmodel != null)
                                        model.ItemID = Itemmodel.ItemID;
                                    else
                                    {
                                        Log += "第" + Sequence + "行:料号错误,不存在的料号。\r\n";
                                        nosuccess++;
                                        continue; //结束本次循环
                                    }
                                }
                                else
                                {
                                    Log += "第" + Sequence + "行:料号不能为空。\r\n";
                                    nosuccess++;
                                    continue; //结束本次循环
                                }


                                cell = row.GetCell(1);//製程序號[4]
                                if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                                {
                                    if (cell.ToString().Length <= 4)
                                        model.Sequence = cell.ToString();
                                    else
                                    {
                                        Log += "第" + Sequence + "行:製程序號超出范围限制。\r\n";
                                        nosuccess++;
                                        continue; //结束本次循环
                                    }
                                }
                                else
                                {
                                    Log += "第" + Sequence + "行:製程序號不能为空。\r\n";
                                    nosuccess++;
                                    continue; //结束本次循环
                                }


                                cell = row.GetCell(2);//製程代號
                                if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                                {
                                    Parmodel = ParList.Where(w => w.Code == cell.ToString().Trim() && w.ParameterTypeID == SysID + "0191213000017").FirstOrDefault();
                                    if (Parmodel != null)
                                        model.ProcessID = Parmodel.ParameterID;
                                    else
                                    {
                                        Log += "第" + Sequence + "行:不存在的製程代號。\r\n";
                                        nosuccess++;
                                        continue; //结束本次循环
                                    }
                                }
                                else
                                {
                                    Log += "第" + Sequence + "行:製程代號不能为空。\r\n";
                                    nosuccess++;
                                    continue; //结束本次循环
                                }

                                cell = row.GetCell(3);//工作中心
                                if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                                {
                                    WCmodel = WCList.Where(w => w.Code == cell.ToString().Trim()).FirstOrDefault();
                                    if (WCmodel != null)
                                        model.WorkCenterID = WCmodel.WorkCenterID;
                                    else
                                    {
                                        Log += "第" + Sequence + "行:不存在的工作中心。\r\n";
                                        nosuccess++;
                                        continue; //结束本次循环
                                    }
                                }
                                else
                                {
                                    Log += "第" + Sequence + "行:工作中心不能为空。\r\n";
                                    nosuccess++;
                                    continue; //结束本次循环
                                }

                                cell = row.GetCell(4);//備註[120]
                                if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                                {
                                    if (cell.ToString().Length <= 120)
                                        model.Comments = cell.ToString();
                                    else
                                        reason += "備註字段不能超过120;";
                                }

                                cell = row.GetCell(5);//製程輔助單位[4]
                                if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                                {
                                    Parmodel = ParList.Where(w => w.Code == cell.ToString().Trim() && w.ParameterTypeID == SysID + "019121300000C").FirstOrDefault();
                                    if (Parmodel != null)
                                        model.AuxUnit = Parmodel.ParameterID;
                                    else
                                        reason += "不存在的輔助單位;";
                                }

                                cell = row.GetCell(6);//製程輔助單位比[10,4]
                                if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                                {
                                    try
                                    {
                                        model.AuxUnitRatio = decimal.Parse(cell.ToString());
                                    }
                                    catch
                                    {
                                        reason += "製程輔助單位比格式错误;";
                                    }
                                }


                                cell = row.GetCell(7);//委外單價[10,4]
                                if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                                {
                                    try
                                    {
                                        model.Price = decimal.Parse(cell.ToString());
                                    }
                                    catch
                                    {
                                        model.Price = 0;
                                        reason += "委外單價格式错误;";
                                    }
                                }
                                else
                                    model.Price = 0;


                                cell = row.GetCell(8);//標準工時(秒)[int]
                                if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                                {
                                    try
                                    {
                                        model.StandardTime = int.Parse(cell.ToString());
                                    }
                                    catch
                                    {
                                        model.StandardTime = 0;
                                        reason += "標準工時格式错误;";
                                    }
                                }
                                else
                                    model.StandardTime = 0;


                                cell = row.GetCell(9);//整備工時(秒)[int]
                                if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                                {
                                    try
                                    {
                                        model.PrepareTime = int.Parse(cell.ToString());
                                    }
                                    catch
                                    {
                                        model.PrepareTime = 0;
                                        reason += "整備工時格式错误;";
                                    }
                                }
                                else
                                    model.PrepareTime = 0;

                                cell = row.GetCell(10);//制程檢驗註記(Y/N)
                                if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                                {
                                    if (cell.ToString() == "Y")
                                        model.IsIP = true;
                                    else if (cell.ToString() == "N")
                                        model.IsIP = false;
                                    else
                                    {
                                        model.IsIP = false;
                                        reason += "制程檢驗註記格式错误;";
                                    }
                                }
                                else
                                    model.IsIP = false;

                                cell = row.GetCell(11);//首檢驗註記(Y/N)
                                if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                                {
                                    if (cell.ToString() == "Y")
                                        model.IsFPI = true;
                                    else if (cell.ToString() == "N")
                                        model.IsFPI = false;
                                    else
                                    {
                                        model.IsFPI = false;
                                        reason += "首檢驗註記格式错误;";
                                    }
                                }
                                else
                                    model.IsFPI = false;

                                cell = row.GetCell(12);//巡檢驗註記(Y/N)
                                if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                                {
                                    if (cell.ToString() == "Y")
                                        model.IsOSI = true;
                                    else if (cell.ToString() == "N")
                                        model.IsOSI = false;
                                    else
                                    {
                                        model.IsOSI = false;
                                        reason += "巡檢驗註記格式错误;";
                                    }
                                }
                                else
                                    model.IsOSI = false;
                            }

                            if (SFC_ItemProcessService.Check(model.ProcessID, model.ItemID, null))
                            {
                                reason += "第" + Sequence + "行:已存在的制品制程信息;";
                                nosuccess++;
                                continue; //结束本次循环
                            }
                            model.ItemProcessID = UniversalService.GetSerialNumber("SFC_ItemProcess");
                            model.Status = SysID + "0201213000001";

                            if (SFC_ItemProcessService.insert(userId, model))
                            {
                                if (!string.IsNullOrWhiteSpace(reason))
                                    Log += "第" + Sequence + "行：" + reason + "\r\n";

                                success++;
                            }
                            else
                            {
                                nosuccess++;
                                reason += "第" + Sequence + "行:添加失败，未知原因,请联系研发人员查询问题。\r\n";
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        DataLogerService.writeerrlog(ex);
                        Log = "第" + Sequence + "行:发生异常,报错时间为：" + DateTime.Now + ",请将导入模板+报错行号告知研发人员查询问题。\r\n";
                    }
                    if (IsImport)
                    {
                        EndTime = DateTime.Now;

                        //如果存在错误报告
                        if (!string.IsNullOrWhiteSpace(Log))
                        {
                            Isreason = true;
                            TimeSpan ts = EndTime - StartTime;
                            string Hour = UtilBussinessService.HourConversionStr(((int)ts.TotalSeconds).ToString());
                            DataLogerService.Sfc01ImportErrLog("制品制程", StartTime, EndTime, Hour, total, success, nosuccess, Log, New);
                            New = false;
                        }


                        Allmsg += "[制品制程:";
                        Allmsg += string.Format(msg, total, success, nosuccess);
                        Allmsg += "]\r\n";
                    }

                    IsImport = false;
                    try
                    {
                        ISheet sheetFive = wk.GetSheetAt(4);   //读取第五表数据
                        for (int j = 2; j <= sheetFive.LastRowNum; j++)  //LastRowNum 是当前表的总行数
                        {
                            IRow row = sheetFive.GetRow(j);  //读取当前行数据
                            if (row == null)
                                break;
                            if (row.GetCell(0) == null) //如果当前行的第一列没有数据，停止读取
                                break;
                            if (string.IsNullOrWhiteSpace(row.GetCell(0).ToString()))//如果当前行的第一列没有数据，停止读取，这是判断当row.GetCell不能null然后实际是空字符
                                break;

                            Log = null;
                            reason = null;
                            total = 0;
                            success = 0;
                            nosuccess = 0;
                            //制品资源
                            IList<SFC_ItemProcess> ItemProcessList = SFC_ItemProcessService.GetList();
                            IList<SFC_ItemOperation> ItemOperationList = SFC_ItemOperationService.GetList();
                            IList<SYS_Resources> ResourceList = SYS_ResourcesService.GetList();
                            SYS_Resources Resmodel = null;
                            SFC_ItemResource ItemResmodel = null;

                            StartTime = DateTime.Now;

                            total++;
                            IsImport = true;

                            ICell cell = null;
                            if (row != null)
                            {
                                ItemID = null;
                                ProcessID = null;
                                OperationID = null;
                                ItemResmodel = new SFC_ItemResource();

                                Sequence = (j + 1).ToString();//当前行

                                cell = row.GetCell(0);//料號
                                if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                                {
                                    Itemmodel = ItemList.Where(w => w.Code == cell.ToString().Trim()).FirstOrDefault();
                                    if (Itemmodel != null)
                                        ItemID = Itemmodel.ItemID;
                                    else
                                    {
                                        Log += "第" + Sequence + "行:料号错误,不存在的料号。\r\n";
                                        nosuccess++;
                                        continue; //结束本次循环
                                    }
                                }
                                else
                                {
                                    Log += "第" + Sequence + "行:料号不能为空。\r\n";
                                    nosuccess++;
                                    continue; //结束本次循环
                                }

                                cell = row.GetCell(2);//製程代號
                                if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                                {
                                    Parmodel = ParList.Where(w => w.Code == cell.ToString().Trim() && w.ParameterTypeID == SysID + "0191213000017").FirstOrDefault();
                                    if (Parmodel != null)
                                        ProcessID = Parmodel.ParameterID;
                                    else
                                    {
                                        Log += "第" + Sequence + "行:不存在的製程代號;。\r\n";
                                        nosuccess++;
                                        continue; //结束本次循环
                                    }
                                }
                                else
                                {
                                    Log += "第" + Sequence + "行:製程代號不能为空。\r\n";
                                    nosuccess++;
                                    continue; //结束本次循环
                                }
                                model = ItemProcessList.Where(w => w.ItemID == ItemID && w.ProcessID == ProcessID).FirstOrDefault();
                                if (model == null)
                                {
                                    Log += "第" + Sequence + "行:不存在的制品制程信息。\r\n";
                                    nosuccess++;
                                    continue; //结束本次循环
                                }
                                ItemResmodel.ItemProcessID = model.ItemProcessID;

                                cell = row.GetCell(4);//工序代號
                                if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                                {
                                    Parmodel = ParList.Where(w => w.Code == cell.ToString().Trim() && w.ParameterTypeID == SysID + "0191213000016").FirstOrDefault();
                                    if (Parmodel != null)
                                        OperationID = Parmodel.ParameterID;
                                    else
                                    {
                                        Log += "第" + Sequence + "行:不存在的工序代號。\r\n";
                                        nosuccess++;
                                        continue; //结束本次循环
                                    }
                                }

                                if (!string.IsNullOrWhiteSpace(OperationID))
                                {
                                    OpeModel = ItemOperationList.Where(w => w.ItemProcessID == ItemResmodel.ItemProcessID && w.OperationID == OperationID).FirstOrDefault();
                                    if (OpeModel == null)
                                    {
                                        Log += "第" + Sequence + "行:不存在的制品制程工序信息。\r\n";
                                        nosuccess++;
                                        continue; //结束本次循环
                                    }
                                    ItemResmodel.ItemOperationID = OpeModel.ItemOperationID;
                                    ItemResmodel.ItemProcessID = null;
                                }

                                cell = row.GetCell(5);//資源代號
                                if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                                {
                                    Resmodel = ResourceList.Where(w => w.Code == cell.ToString().Trim()).FirstOrDefault();
                                    if (Resmodel != null)
                                    {
                                        ItemResmodel.ResourceID = Resmodel.ResourceID;
                                        if (Resmodel.ClassID == SysID + "0201213000047")
                                            ItemResmodel.Type = SysID + "0201213000085";
                                        else if (Resmodel.ClassID == SysID + "0201213000048")
                                            ItemResmodel.Type = SysID + "0201213000084";
                                        else
                                            ItemResmodel.Type = SysID + "0201213000086";
                                    }
                                    else
                                    {
                                        Log += "第" + Sequence + "行:不存在的資源代號。\r\n";
                                        nosuccess++;
                                        continue; //结束本次循环
                                    }
                                }
                                else
                                {
                                    Log += "第" + Sequence + "行:資源代號不能为空。\r\n";
                                    nosuccess++;
                                    continue; //结束本次循环
                                }

                                cell = row.GetCell(6);//是否主資源(Y/N)
                                if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                                {
                                    if (cell.ToString() == "Y")
                                        ItemResmodel.IfMain = true;
                                    else if (cell.ToString() == "N")
                                        ItemResmodel.IfMain = false;
                                    else
                                    {
                                        ItemResmodel.IfMain = false;
                                        reason += "是否主資源格式错误;";
                                    }
                                }

                                cell = row.GetCell(7);//備註[120]
                                if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                                {
                                    if (cell.ToString().Length <= 120)
                                        ItemResmodel.Comments = cell.ToString();
                                    else
                                        reason += "備註字段不能超过120;";
                                }
                            }

                            if (!SFC_ItemResourceService.Check(ItemResmodel.ResourceID, ItemResmodel.ItemProcessID, ItemResmodel.ItemOperationID, ItemResmodel.Type, null))
                            {
                                ItemResmodel.ItemResourceID = UniversalService.GetSerialNumber("SFC_ItemResource");
                                ItemResmodel.Status = SysID + "0201213000001";
                                if (SFC_ItemResourceService.insert(userId, ItemResmodel))
                                {
                                    if (!string.IsNullOrWhiteSpace(reason))
                                        Log += "第" + Sequence + "行：" + reason + "\r\n";

                                    success++;
                                }
                                else
                                {
                                    nosuccess++;
                                    Log += "第" + Sequence + "行:添加失败，未知原因,请联系研发人员查询。\r\n";
                                }
                            }
                            else
                            {
                                nosuccess++;
                                Log += "第" + Sequence + "行:已存在的资源信息。\r\n";
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        DataLogerService.writeerrlog(ex);
                        Log = "第" + Sequence + "行:发生异常,报错时间为：" + DateTime.Now + ",请将导入模板+报错行号告知研发人员查询问题。\r\n";
                    }

                    EndTime = DateTime.Now;
                    if (IsImport)
                    {
                        //如果存在错误报告
                        if (!string.IsNullOrWhiteSpace(Log))
                        {
                            Isreason = true;
                            TimeSpan ts = EndTime - StartTime;
                            string Hour = UtilBussinessService.HourConversionStr(((int)ts.TotalSeconds).ToString());
                            DataLogerService.Sfc01ImportErrLog("制品资源", StartTime, EndTime, Hour, total, success, nosuccess, Log, New);
                            New = false;
                        }

                        Allmsg += "[制品资源:";
                        Allmsg += string.Format(msg, total, success, nosuccess);
                        Allmsg += "]\r\n";
                    }
                }
            }
            //if (nosuccess == total)
            //    status = "410";

            if (string.IsNullOrWhiteSpace(Allmsg))
                Allmsg = "并无导入数据";

            msg = Allmsg;

            return new { status = status, msg = msg, Isreason = Isreason, FileName = "Sfc00001ImportErrorLog" };

        }

        /// <summary>
        /// 制令单的导入
        /// Sam 2017年10月27日11:11:58
        /// 在V1的版本上 添加了条件的数据过滤
        /// </summary>
        /// <param name="token"></param>
        /// <param name="filename"></param>
        /// <param name="Date"></param>
        /// <param name="StartMoNo"></param>
        /// <param name="EndMoNo"></param>
        /// <param name="Cust"></param>
        /// <returns></returns>
        public static object Sfc00002ImportV2(string token, string filename, string Date, string StartMoNo, string EndMoNo, string Cust)
        {
            DateTime StartTime = DateTime.Now;
            string userId = UtilBussinessService.detoken(token);
            string SysID = Framework.SystemID;
            string status = "200", msg = "共{0}条信息,{1}条信息导入成功,{2}条信息导入失败！";
            string reason = null;
            int total = 0;
            int success = 0;
            int nosuccess = 0;
            string Log = null;
            bool Isreason = false;
            if (filename.EndsWith(".xls") || filename.EndsWith(".xlsx"))
            {
                IWorkbook wk = null;
                string Sequence = null;
                string AddReason = null;
                using (FileStream fs = File.OpenRead(@filename))   //打开.xls文件
                {
                    if (filename.EndsWith(".xls"))
                        wk = new HSSFWorkbook(fs); //把xls文件中的数据写入wk中
                    else if (filename.EndsWith(".xlsx"))
                        wk = new XSSFWorkbook(fs);//把xlsx文件中的数据写入wk中

                    ISheet sheet = wk.GetSheetAt(0);   //读取第一表数据
                    SFC_FabricatedMother FabMo = null;
                    SYS_Customers CustModel = null;
                    SYS_Items ItemModel = null;
                    //改进点1：事先把所有的客户和所有的料品都查询出来，舰少判断时查询数据库的次数
                    IList<SYS_Customers> CustList = SYS_CustomersService.GetList();
                    IList<SYS_Items> ItemList = SYS_ItemsService.GetList();
                    //改进点2：在这里执行一个函数，将所有的制品制程等信息在这里直接获取好。
                    Sfc00002ImportBussinessService.Initialization();
                    for (int j = 2; j <= sheet.LastRowNum; j++)  //LastRowNum 是当前表的总行数
                    {
                        IRow row = sheet.GetRow(j);  //读取当前行数据
                        if (row == null)
                            break;
                        if (row.GetCell(0) == null) //如果当前行的第一列没有数据，停止读取
                            break;
                        if (string.IsNullOrWhiteSpace(row.GetCell(0).ToString()))//如果当前行的第一列没有数据，停止读取，这是判断当row.GetCell不能null然后实际是空字符
                            break;

                        ICell cell = null;
                        if (row != null)
                        {
                            FabMo = new SFC_FabricatedMother();
                            CustModel = null;
                            ItemModel = null;
                            reason = null;
                            cell = row.GetCell(0);//序号
                            Sequence = cell.ToString();

                            cell = row.GetCell(1);//投产日期/預計開工日
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                try
                                {
                                    if (DateTime.Parse(Date) == DateTime.Parse(cell.DateCellValue.ToString()))
                                    {
                                        FabMo.StartDate = DateTime.Parse(cell.DateCellValue.ToString());
                                    }
                                    else
                                        continue; //不在筛选条件内，跳过此数据
                                }
                                catch//假如，使用DateCellValue无法转换，则用直接ToString试试，如果再不行，就报错
                                {
                                    try
                                    {
                                        if (DateTime.Parse(Date) == DateTime.Parse(cell.ToString()))
                                        {
                                            FabMo.StartDate = DateTime.Parse(cell.ToString());
                                        }
                                        else
                                            continue; //不在筛选条件内，跳过此数据
                                    }
                                    catch
                                    {
                                        Log += "序号[" + Sequence + "]:投产日期[" + cell.ToString() + "]格式有误,无法进行日期筛选。\r\n";
                                        total++;
                                        nosuccess++;
                                        continue; //不在筛选条件内，跳过此数据
                                    }
                                }
                            }
                            else
                                continue; //不在筛选条件内，跳过此数据


                            cell = row.GetCell(3);//客户名/客戶代號
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                CustModel = CustList.Where(w => w.Code == cell.ToString()).FirstOrDefault();
                                if (string.IsNullOrWhiteSpace(Cust))
                                {
                                    if (CustModel != null)
                                        FabMo.CustomerID = CustModel.CustomerID;
                                    else
                                        reason += "客户代号错误,不存在的客户信息;";
                                }
                                else
                                {
                                    if (cell.ToString().Trim() == Cust)
                                    {
                                        if (CustModel != null)
                                            FabMo.CustomerID = CustModel.CustomerID;
                                        else
                                            reason += "客户代号错误,不存在的客户信息;";
                                    }
                                    else
                                        continue; //不在筛选条件内，跳过此数据
                                }
                            }

                            cell = row.GetCell(4);//工单号/製令單號
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                FabMo.MoNo = cell.ToString().Trim();
                                if (string.Compare(FabMo.MoNo, StartMoNo) < 0)
                                    continue;   //不在筛选条件内，跳过此数据

                                if (string.Compare(EndMoNo, FabMo.MoNo) < 0)
                                    continue;   //不在筛选条件内，跳过此数据
                            }
                            else
                                continue;   //不在筛选条件内，跳过此数据

                            total++;
                            cell = row.GetCell(2);//投产比例/超完工比率
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                try
                                {
                                    FabMo.OverRate = 100 - decimal.Parse(cell.ToString());
                                }
                                catch
                                {
                                    Log += "序号[" + Sequence + "]:投产比例格式错误。\r\n";
                                    nosuccess++;
                                    continue; //结束本次循环
                                }
                            }

                            cell = row.GetCell(5);//订单号/訂單號碼
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                                FabMo.OrderNo = cell.ToString();

                            cell = row.GetCell(6);//产品型号/料品代號
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                ItemModel = ItemList.Where(w => w.Code == cell.ToString()).FirstOrDefault();
                                if (ItemModel != null)
                                    FabMo.ItemID = ItemModel.ItemID;
                                else
                                {
                                    Log += "序号[" + Sequence + "]:产品型号错误，不存在的产品信息。\r\n";
                                    nosuccess++;
                                    continue; //结束本次循环
                                }
                            }
                            else
                            {
                                Log += "序号[" + Sequence + "]:产品型号不能为空。\r\n";
                                nosuccess++;
                                continue; //结束本次循环
                            }

                            cell = row.GetCell(7);//投单数量/製造數量
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                try
                                {
                                    FabMo.Quantity = decimal.Parse(cell.ToString());
                                }
                                catch
                                {
                                    Log += "序号[" + Sequence + "]:投单数量格式错误。\r\n";
                                    nosuccess++;
                                    continue; //结束本次循环
                                }
                            }
                            else
                            {
                                Log += "序号[" + Sequence + "]:投单数量不能为空。\r\n";
                                nosuccess++;
                                continue; //结束本次循环
                            }

                            cell = row.GetCell(17);//预定交期/預計出貨日
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                try
                                {
                                    FabMo.ShipmentDate = DateTime.Parse(cell.DateCellValue.ToString());
                                }
                                catch
                                {
                                    reason += "预定交期格式错误;";
                                }
                            }

                            cell = row.GetCell(18);//交货数量/訂單數量
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                try
                                {
                                    FabMo.OrderQuantity = decimal.Parse(cell.ToString());
                                }
                                catch
                                {
                                    Log += "序号[" + Sequence + "]:交货数量格式错误。\r\n";
                                    nosuccess++;
                                    continue; //结束本次循环
                                }
                            }

                            cell = row.GetCell(26);//特殊要求/备注
                            if (cell != null && !string.IsNullOrWhiteSpace(cell.ToString()))
                            {
                                if (cell.ToString().Length <= 120)
                                    FabMo.Comments = cell.ToString();
                                else
                                    reason += "特殊要求超过了120个字;";
                            }
                        }


                        if (SFC_FabricatedMotherService.Check(FabMo.MoNo))
                        {
                            Log += "序号[" + Sequence + "]:已存在的工单号。\r\n";
                            nosuccess++;
                            continue; //结束本次循环
                        }

                        FabMo.FabricatedMotherID = UniversalService.GetSerialNumber("SFC_FabricatedMother");
                        FabMo.Status = Framework.SystemID + "0201213000028";
                        FabMo.Date = DateTime.Now;
                        FabMo.Version = "0";
                        FabMo.UnitID = ItemModel.Unit;
                        AddReason = null;
                        if (Sfc00002ImportBussinessService.FabMo(userId, FabMo, ItemModel, ref AddReason))
                        {
                            if (!string.IsNullOrWhiteSpace(reason))
                                Log += "序号[" + Sequence + "]:" + reason + "\r\n";

                            success++;
                        }
                        else
                        {
                            Log += "序号[" + Sequence + "]:导入失败!" + AddReason + "\r\n";
                            nosuccess++;
                        }
                    }
                }
            }

            if (total == 0)
                return new { status = "440", msg = "Excel文档无符合条件的数据可导入！" };

            DateTime EndTime = DateTime.Now;

            if (nosuccess == total)
                status = "410";

            //如果存在错误报告
            if (!string.IsNullOrWhiteSpace(Log))
            {
                Isreason = true;
                TimeSpan ts = EndTime - StartTime;
                string Hour = UtilBussinessService.HourConversionStr(((int)ts.TotalSeconds).ToString());
                DataLogerService.ImportWriteErrLog(StartTime, EndTime, Hour, total, success, nosuccess, "Sfc00002ImportErrorLog", Log);
            }

            msg = string.Format(msg, total, success, nosuccess);

            return new { status = status, msg = msg, Isreason = Isreason, FileName = "Sfc00002ImportErrorLog" };
        }
    }
}