using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using MonkeyFly.MES.ModelServices;
using Newtonsoft.Json.Linq;
using MonkeyFly.Core;
using MonkeyFly.MES.Models;
using MonkeyFly.MES.BasicService;
using System.Data;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using System.Drawing;
using System.Web;

namespace MonkeyFly.MES.Services
{
    public class UtilBussinessService
    {
        /// <summary>
        /// 分页配置
        /// SAM 2016年10月18日16:42:31
        /// </summary>
        /// <param name="rows">查出来的数据</param>
        /// <param name="total">总行数</param>
        /// <returns></returns>
        public static object getPaginationModel(IList<Hashtable> rows, int total)
        {
            return new { total = total, rows = rows };
        }

        /// <summary>
        /// 解析token，返回userid
        /// SAM 2016年10月18日16:42:50
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <returns></returns>
        public static string detoken(string Token)
        {
            Token = Token.Replace(" ", "+");
            string ken = Utils.EncryptHelper.DESDecrypt(Token);//解析Token
            return ken.Split('-')[0];
        }

        /// <summary>
        /// 拼接字符串
        /// SAM 2016年10月14日11:26:08
        /// </summary>
        /// <param name="str"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string str(string str, string id)
        {
            if (str == null)
                str = id;
            else
                str = str + "," + id;
            return str;
        }

        /// <summary>
        /// 获取这个月的第一天是星期几
        /// SAM 2017年5月9日09:33:28
        /// </summary>
        /// <param name="week"></param>
        /// <returns></returns>
        public static string GetDayByWeek(string week)
        {
            Dictionary<string, string> openWith = new Dictionary<string, string>();
            openWith.Add("Monday", "2");
            openWith.Add("Tuesday", "3");
            openWith.Add("Wednesday", "4");
            openWith.Add("Thursday", "5");
            openWith.Add("Friday", "6");
            openWith.Add("Saturday", "7");
            openWith.Add("Sunday", "1");
            return openWith[week];
        }

        /// <summary>
        /// 根据年月日获取那天是星期几
        /// SAM 2016年10月25日19:16:47
        /// 0是星期一，6是星期天
        /// 摘自：http://blog.csdn.net/a4562834/article/details/7264319
        /// </summary>
        /// <param name="y"></param>
        /// <param name="m"></param>
        /// <param name="d"></param>
        /// <returns></returns>
        public static int Whether_Weekend(int y, int m, int d)
        {
            if (m < 3)
            {
                m += 12;
                y--;
            }
            int week = (d + 2 * m + 3 * (m + 1) / 5 + y + y / 4 - y / 100 + y / 400) % 7;   // 基姆拉尔森公式     
            return week;
        }

        /// <summary>
        /// 根据批号类别ID获取自动批号编号
        /// Tom 2017年6月27日20点43分
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="ClassID"></param>
        /// <returns></returns>
        public static string GetAutoLotNumber(string userID, string classID)
        {
            try
            {
                SYS_AutoNumber lotClass = SYS_AutoNumberService.get(classID);
                if (lotClass == null)
                {
                    return "";
                }

                string year = lotClass.YearLength == 0 ? "" : DateTime.Now.Year + "";
                string month = lotClass.MonthLength == 0 ? "" : DateTime.Now.ToString("MM");
                string day = lotClass.DateLength == 0 ? "" : DateTime.Now.ToString("dd");
                string prevchar = lotClass.DefaultCharacter + year + month + day;

                int num = 0;
                SYS_AutoNumberRecord record = SYS_AutoNumberRecordService.getByAutoNumber(lotClass.AutoNumberID, prevchar);
                if (record != null)
                {
                    //record = new SYS_AutoNumberRecord();
                    //record.AutoNumberID = lotClass.AutoNumberID;
                    //record.AutoNumberRecordID = UniversalService.GetSerialNumber("SYS_AutoNumberRecord");
                    //record.Num = 1;
                    //record.Prevchar = prevchar;
                    //record.Status = Framework.SystemID + "0201213000001";
                    //SYS_AutoNumberRecordService.insert(userID, record);
                    //return prevchar + record.Num.ToString().PadLeft(lotClass.Length, '0');

                    num = record.Num;
                }

                return prevchar + (num + 1).ToString().PadLeft(lotClass.NumLength, '0');
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return "";
            }
        }


        /// <summary>  
        /// 获取指定日期的月份第一天  
        /// </summary>  
        /// <param name="dateTime"></param>  
        /// <returns></returns>  
        public static DateTime GetDateTimeMonthFirstDay(int year, int month)
        {
            return new DateTime(year, month, 1);
        }

        /// <summary>
        /// 获取当前登录用户的语序
        /// SAM 2017年7月30日16:37:02
        /// </summary>
        /// <returns></returns>
        public static string GetLan()
        {
            string Lan = null;
            CurrentUser user = Framework.Instance().getCurrentUser();
            if (user.language == 1)
                Lan = Framework.SystemID + "020121300000D";
            else if (user.language == 2)
                Lan = Framework.SystemID + "020121300000E";
            else if (user.language == 3)
                Lan = Framework.SystemID + "020121300000F";
            return Lan;
        }

        /// <summary>
        /// 当状态为01时，转换到正常的语序
        /// SAM 2017年7月30日23:07:23
        /// </summary>
        /// <param name="Status"></param>
        /// <returns></returns>
        public static string GetStatus(string Status)
        {
            string Str = null;
            CurrentUser user = Framework.Instance().getCurrentUser();
            if (user.language == 1) //简
            {
                if (Status == "1")
                    Str = "正常";
                else
                    Str = "作废";
            }
            else if (user.language == 2)//繁
            {
                if (Status == "1")
                    Str = "正常";
                else
                    Str = "作廢";
            }
            else if (user.language == 3)//英
            {
                if (Status == "1")
                    Str = "Normal";
                else
                    Str = "Obsolete";
            }
            return Str;
        }

        /// <summary>
        /// 根据参数类型集合获取参数
        /// SAM 2017年4月28日11:23:22
        /// </summary>
        /// <param name="Token">授权码</param>
        /// <param name="typeIDs"></param>
        /// <returns></returns>
        public static IList<Hashtable> GetParameters(string Token, string typeIDs)
        {
            IList<Hashtable> result = SYS_ParameterService.GetLists(typeIDs);
            foreach (Hashtable itemMode in result)
            {
                //根据语序及参数流水号获取语序设定，不存在不替换，存在则替换
                string Name = SYS_LanguageLibService.GetLan(itemMode["value"].ToString(), "Name", 20);
                if (!string.IsNullOrWhiteSpace(Name))
                {
                    itemMode["text"] = Name;
                    itemMode["Newvalue"] = itemMode["Code"] + "-" + itemMode["text"];
                }
            }
            return result;
        }

        /// <summary>
        /// 用户及单据种别，获取该用户在该种别下，拥有的单据类别下拉框
        /// SAM 2017年7月30日20:32:35
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="TypeID"></param>
        /// <returns></returns>
        public static IList<Hashtable> GetTypeList(string UserID, string TypeID)
        {
            IList<Hashtable> result = SYS_DocumentTypeSettingService.GetTypeList(UserID, TypeID);
            foreach (Hashtable itemMode in result)
            {
                //根据语序及参数流水号获取语序设定，不存在不替换，存在则替换
                string Name = SYS_LanguageLibService.GetLan(itemMode["value"].ToString(), "Name", 59);
                string Code = SYS_LanguageLibService.GetLan(itemMode["value"].ToString(), "Code", 59);
                if (!string.IsNullOrWhiteSpace(Code))
                {
                    itemMode["Code"] = Code;
                    itemMode["Newvalue"] = itemMode["Code"] + "-" + itemMode["Name"];
                }
                if (!string.IsNullOrWhiteSpace(Name))
                {
                    itemMode["Name"] = Name;
                    itemMode["Newvalue"] = itemMode["Code"] + "-" + itemMode["Name"];
                }
            }
            return result;
        }

        /// <summary>
        /// 执行SQL，返回执行结果
        /// SAM 2016年10月10日19:49:04
        /// TODO
        /// </summary>
        /// <param name="SQL"></param>
        /// <returns></returns>
        public static bool RunSQL(string SQL)
        {
            try
            {
                return SQLHelper.ExecuteNonQuery(SQL, CommandType.Text) > 0;
            }
            catch (Exception ex)
            {

                DataLogerService.writeerrlog(ex);
                DataLogerService.writeerrlog(new Exception(SQL));
                return false;
            }
        }

        /// <summary>
        /// 写操作日志
        /// SAM 2017年4月28日10:48:36
        /// </summary>
        /// <param name="userID">当前登陆用户</param>
        /// <param name="Postion">日志触发时所作的页面</param>
        /// <param name="Target">用户操作的目标</param>
        /// <param name="Type">操作类型</param>
        /// <param name="Message">操作的详细信息</param>
        public static void WriteLog(string userID, string Postion, string Target, string Type, string Message)
        {
            try
            {
                SYS_MFCLog model = new SYS_MFCLog();
                model.UserID = userID;
                model.Postion = Postion;
                model.Target = Target;
                model.Type = Type;
                model.Message = Message;
                model.SystemID = Framework.SystemID;
                SYS_MFCLogService.insert(model);
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
            }
        }

        /// <summary>
        /// 判断是否正确邮箱
        /// SAM 2017年5月23日15:04:42
        /// </summary>
        /// <param name="emailStr"></param>
        /// <returns></returns>
        public static bool IsEmail(string emailStr)
        {
            return Regex.IsMatch(emailStr, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
        }

        /// <summary>
        /// 根据单据流水号+日期获取自动编号
        /// SAM 2017年7月30日21:32:06
        /// 使用JSON格式
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="DocumentID"></param>
        /// <returns></returns>
        public static object GetDocumentAutoNumber(string userid, string DocumentID, string Date)
        {
            string AutoNumber = null;
            string DocumentAutoNumberID = null;
            string Prevchar = null;
            SYS_DocumentTypeSetting number = SYS_DocumentTypeSettingService.get(DocumentID);
            if (number == null)
                return new { AutoNumber = AutoNumber, DocumentAutoNumberID = DocumentAutoNumberID };

            DateTime Now = DateTime.Now;
            if (!string.IsNullOrWhiteSpace(Date))
            {
                try
                {
                    Now = DateTime.Parse(Date);
                }
                catch (Exception ex)
                {
                    DataLogerService.writeerrlog(ex);
                }
            }
            SYS_Parameters ParModel = SYS_ParameterService.get(number.GiveWay);
            if (ParModel.Code == "M")
                Prevchar = number.Code + Now.ToString("yyMM");
            else if (ParModel.Code == "Y")
                Prevchar = number.Code + Now.ToString("yy");
            else if (ParModel.Code == "D")
                Prevchar = number.Code + Now.ToString("yyMMdd");

            SYS_DocumentAutoNumber model = SYS_DocumentAutoNumberService.getByAutoNumber(number.DTSID, Prevchar);
            if (model == null)
            {
                model = new SYS_DocumentAutoNumber();
                model.DocumentAutoNumberID = UniversalService.GetSerialNumber("SYS_DocumentAutoNumber");
                model.ClassID = number.DTSID;
                model.Num = 0;
                model.DefaultCharacter = Prevchar;
                model.Attribute = number.Attribute;
                model.Status = Framework.SystemID + "0201213000001";
                DateTime now = DateTime.Now;
                SYS_DocumentAutoNumberService.insert(userid, model);
            }
            DocumentAutoNumberID = model.DocumentAutoNumberID;
            AutoNumber = model.DefaultCharacter + (model.Num + 1).ToString().PadLeft(number.CodeLength, '0');

            return new { AutoNumber = AutoNumber, DocumentAutoNumberID = DocumentAutoNumberID };
        }

        /// <summary>
        /// 根据单据类别流水号+日期获取自动编号，并返回记录流水号
        /// SAM 2017年8月1日15:40:12
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="DocumentID"></param>
        /// <param name="Date"></param>
        /// <param name="AutoNumberID"></param>
        /// <returns></returns>
        public static string GetDocumentAutoNumber(string userid, string DocumentID, string Date, ref string AutoNumberID)
        {
            string AutoNumber = null;
            string Prevchar = null;
            SYS_DocumentTypeSetting number = SYS_DocumentTypeSettingService.get(DocumentID);
            if (number == null)
                return null;

            DateTime Now = DateTime.Now;
            if (!string.IsNullOrWhiteSpace(Date))
            {
                try
                {
                    Now = DateTime.Parse(Date);
                }
                catch (Exception ex)
                {
                    DataLogerService.writeerrlog(ex);
                }
            }
            SYS_Parameters ParModel = SYS_ParameterService.get(number.GiveWay);
            if (ParModel.Code == "M")
                Prevchar = number.Code + Now.ToString("yyMM");
            else if (ParModel.Code == "Y")
                Prevchar = number.Code + Now.ToString("yy");
            else if (ParModel.Code == "D")
                Prevchar = number.Code + Now.ToString("yyMMdd");

            SYS_DocumentAutoNumber model = SYS_DocumentAutoNumberService.getByAutoNumber(number.DTSID, Prevchar);
            if (model == null)
            {
                model = new SYS_DocumentAutoNumber();
                model.DocumentAutoNumberID = UniversalService.GetSerialNumber("SYS_DocumentAutoNumber");
                model.ClassID = number.DTSID;
                model.Num = 0;
                model.DefaultCharacter = Prevchar;
                model.Attribute = number.Attribute;
                model.Status = Framework.SystemID + "0201213000001";
                DateTime now = DateTime.Now;
                SYS_DocumentAutoNumberService.insert(userid, model);
            }
            AutoNumberID = model.DocumentAutoNumberID;
            AutoNumber = model.DefaultCharacter + (model.Num + 1).ToString().PadLeft(number.CodeLength, '0');

            return AutoNumber;
        }

        /// <summary>
        /// 根据批号类别流水号+日期，并返回记录流水号
        /// SAM 2017年9月19日16:04:56
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="AutoNumberID"></param>
        /// <param name="Date"></param>
        /// <param name="AutoNumberRecordID"></param>
        /// <returns></returns>
        public static string GetLotAutoNumber(string userid, string AutoNumberID, string Date, ref string AutoNumberRecordID)
        {
            SYS_AutoNumber Automodel = SYS_AutoNumberService.get(AutoNumberID);
            if (Automodel == null)
                return null;

            string Prevchar = null;

            DateTime Now = DateTime.Now;
            if (!string.IsNullOrWhiteSpace(Date))
            {
                try
                {
                    Now = DateTime.Parse(Date);
                }
                catch (Exception ex)
                {
                    DataLogerService.writeerrlog(ex);
                }
            }

            string Year = Automodel.YearLength == 4 ? Now.ToString("yy") : null;
            string Month = Automodel.MonthLength == 2 ? Now.ToString("MM") : null;
            string day = Automodel.DateLength == 2 ? Now.ToString("dd") : null;

            Prevchar = Automodel.DefaultCharacter + Year + Month + day;
            try
            {
                SYS_AutoNumberRecord Recordmodel = SYS_AutoNumberRecordService.getByAutoNumber(Automodel.AutoNumberID, Prevchar);
                if (Recordmodel == null)
                {
                    Recordmodel = new SYS_AutoNumberRecord();
                    Recordmodel.AutoNumberRecordID = UniversalService.GetSerialNumber("SYS_AutoNumberRecord");
                    Recordmodel.AutoNumberID = Automodel.AutoNumberID;
                    Recordmodel.Num = 0;
                    Recordmodel.Prevchar = Prevchar;
                    Recordmodel.Status = Framework.SystemID + "0201213000001";
                    SYS_AutoNumberRecordService.insert(userid, Recordmodel);
                }
                AutoNumberRecordID = Recordmodel.AutoNumberRecordID;
                return Recordmodel.Prevchar + (Recordmodel.Num+1).ToString().PadLeft(Automodel.NumLength, '0');
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return null;
            }
        }

        /// <summary>
        /// 完工单产生检验单时，产生检验单号
        /// SAM 2017年8月23日23:54:08
        /// 使用”單據類別字軌設定表INFO_DOC_CLASS”，單據種別為QCS01，
        /// 且”預設”欄位為Y之單據類別自動編碼，不須檢核單據部門員工權限表。自動編碼方式請參考MES規格00-單據取號邏輯。
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="Date"></param>
        /// <param name="AutoNumberID"></param>
        /// <returns></returns>
        public static string SFC07GetDocumentAutoNumber(string userid, string Date, ref string AutoNumberID)
        {
            string AutoNumber = null;
            string Prevchar = null;
            SYS_DocumentTypeSetting number = SYS_DocumentTypeSettingService.getByType(Framework.SystemID + "020121300003B");
            if (number == null)
                return null;

            DateTime Now = DateTime.Now;
            if (!string.IsNullOrWhiteSpace(Date))
            {
                try
                {
                    Now = DateTime.Parse(Date);
                }
                catch (Exception ex)
                {
                    DataLogerService.writeerrlog(ex);
                }
            }
            SYS_Parameters ParModel = SYS_ParameterService.get(number.GiveWay);
            if (ParModel.Code == "M")
                Prevchar = number.Code + Now.ToString("yyMM");
            else if (ParModel.Code == "Y")
                Prevchar = number.Code + Now.ToString("yy");
            else if (ParModel.Code == "D")
                Prevchar = number.Code + Now.ToString("yyMMdd");

            SYS_DocumentAutoNumber model = SYS_DocumentAutoNumberService.getByAutoNumber(number.DTSID, Prevchar);
            if (model == null)
            {
                model = new SYS_DocumentAutoNumber();
                model.DocumentAutoNumberID = UniversalService.GetSerialNumber("SYS_DocumentAutoNumber");
                model.ClassID = number.DTSID;
                model.Num = 0;
                model.DefaultCharacter = Prevchar;
                model.Attribute = number.Attribute;
                model.Status = Framework.SystemID + "0201213000001";
                DateTime now = DateTime.Now;
                SYS_DocumentAutoNumberService.insert(userid, model);
            }
            AutoNumberID = model.DocumentAutoNumberID;
            AutoNumber = model.DefaultCharacter + (model.Num + 1).ToString().PadLeft(number.CodeLength, '0');

            return AutoNumber;
        }

        /// <summary>
        /// 根据单据记录更新流水号
        /// SAM 2017年7月30日21:35:09
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="DocumentAutoNumberID"></param>
        public static void UpdateDocumentAutoNumber(string userid, string DocumentAutoNumberID)
        {
            try
            {
                SYS_DocumentAutoNumber model = SYS_DocumentAutoNumberService.get(DocumentAutoNumberID);

                model.Num = model.Num + 1;
                SYS_DocumentAutoNumberService.update(userid, model);
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
            }
        }

        /// <summary>
        /// 根据批号记录流水号更新批号记录
        /// SAM 2017年9月19日15:46:50
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="LotAutoNumberID"></param>
        public static void UpdateLotAutoNumber(string userid, string LotAutoNumberID)
        {
            try
            {
                //SYS_DocumentAutoNumber model = SYS_DocumentAutoNumberService.get(DocumentAutoNumberID);
                SYS_AutoNumberRecord model = SYS_AutoNumberRecordService.get(LotAutoNumberID);
                model.Num = model.Num + 1;
                SYS_AutoNumberRecordService.update(userid, model);
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
            }
        }

        /// <summary>
        /// 获取当前登录用户的个人信息
        /// SAM 2017年6月14日00:41:03
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public static Hashtable GetUser(string Token)
        {
            string userid = detoken(Token);
            Hashtable result = SYS_MESUserService.GetUser(userid);
            result["IsHM"] = SYS_UserOrganizationMappingService.CheckUserRole(result["MESUserID"].ToString(), Framework.SystemID + "0601213000001");//是否生管
            return result;
        }

        /// <summary>
        /// 判断当前登录用户是否为品管
        /// SAM 2017年6月14日00:41:03
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public static bool CheckUserRole(string Token)
        {
            string userid = detoken(Token);
            SYS_MESUsers model1=SYS_MESUserService.GetUser2(userid);
             return SYS_UserOrganizationMappingService.CheckUserRole(model1.MESUserID,Framework.SystemID+ "060121300000B");
        }

        /// <summary>
        /// 根据制品制程流水号获取相关信息
        /// SAM 2017年6月23日10:30:32
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="ItemProcessID"></param>
        /// <returns></returns>
        public static Hashtable GetItemProcess(string Token, string ItemProcessID)
        {
            return SFC_ItemProcessService.GetItemProcess(ItemProcessID);
        }

        ///// <summary>
        ///// 将时分秒格式转换成秒，-1代表转换异常
        ///// SAM 2017年7月26日21:46:05
        ///// </summary>
        ///// <param name="Str"></param>
        ///// <returns></returns>
        //public static int StrConversionHour(string Str)
        //{
        //    try
        //    {
        //        if (string.IsNullOrWhiteSpace(Str))
        //            return 0;
        //        else
        //        {
        //            string[] Hour = Str.Split(':');
        //            return int.Parse(Hour[0].ToString()) * 3600 + int.Parse(Hour[1].ToString()) * 60 + int.Parse(Hour[2].ToString());
        //        }
        //    }
        //    catch
        //    {
        //        return -1;
        //    }
        //}

        /// <summary>
        /// 将时分秒格式转换成秒(存在负数的情况)，null代表转换异常
        /// SAM 2017年8月23日16:42:51
        /// </summary>
        /// <param name="Str"></param>
        /// <returns></returns>
        public static int? StrConversionHour(string Str)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(Str))
                    return 0;
                else
                {
                    if (Str.StartsWith("-"))
                    {
                        string[] Hour = Str.Split(':');
                        return -(int.Parse(Hour[0].ToString().Replace("-","")) * 3600 + int.Parse(Hour[1].ToString()) * 60 + int.Parse(Hour[2].ToString()));
                    }
                    else
                    {
                        string[] Hour = Str.Split(':');
                        return int.Parse(Hour[0].ToString()) * 3600 + int.Parse(Hour[1].ToString()) * 60 + int.Parse(Hour[2].ToString());
                    }
                }
            }
            catch(Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return null;
            }
        }

        /// <summary>
        /// 将秒转成时分秒格式
        /// SAM 2017年8月23日16:31:12
        /// 格式补零至00:00:00
        /// </summary>
        /// <param name="Hour"></param>
        /// <returns></returns>
        public static string HourConversionStr(string Hour)
        {
            try
            {
                string HourStr = null;
                string MinuteStr = null;
                string SecondStr = null;
                if (string.IsNullOrWhiteSpace(Hour))
                    return "00:00:00";

                if (Hour.StartsWith("-"))
                {
                    HourStr = (-int.Parse(Hour) / 3600).ToString();
                    HourStr = HourStr.Length >= 2 ? HourStr : HourStr.PadLeft(2,'0');
                    HourStr = "-" + HourStr;
                    MinuteStr = (-int.Parse(Hour) % 3600 / 60).ToString().PadLeft(2, '0');
                    SecondStr= (- int.Parse(Hour) % 3600 % 60).ToString().PadLeft(2, '0');
                }
                else
                {
                    HourStr = (int.Parse(Hour) / 3600).ToString();
                    HourStr = HourStr.Length >= 2 ? HourStr : HourStr.PadLeft(2, '0');
                    MinuteStr = (int.Parse(Hour) % 3600 / 60).ToString().PadLeft(2, '0');
                    SecondStr = (int.Parse(Hour) % 3600 % 60).ToString().PadLeft(2, '0');
                }

                return HourStr + ":" + MinuteStr + ":" + SecondStr;
            }
            catch
            {
                return "00:00:00";
            }
        }

        /// <summary>
        /// 将base64格式字符串转换为图片
        /// SAM 2017年7月31日22:50:27
        /// </summary>
        /// <param name="base64string"></param>
        /// <returns></returns>
        public static Bitmap GetImageFromBase64(String base64string)
        {
            byte[] b = Convert.FromBase64String(base64string);
            MemoryStream ms = new MemoryStream(b);
            Bitmap bitmap = new Bitmap(ms);
            return bitmap;
        }

        /// <summary>
        /// 图片转为base64编码的字符串  
        /// </summary>
        /// <param name="Imagefilename"></param>
        /// <returns></returns>
        public static string ImgToBase64String(string AttachmentID)
        {
            try
            {
                SYS_Attachments model = SYS_AttachmentsService.get(AttachmentID);
                if (model == null)
                    return null;

                string strPath = HttpContext.Current.Server.MapPath("~/");

                Bitmap bmp = new Bitmap(strPath + model.Path);

                MemoryStream ms = new MemoryStream();
                bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                byte[] arr = new byte[ms.Length];
                ms.Position = 0;
                ms.Read(arr, 0, (int)ms.Length);
                ms.Close();
                ms.Dispose();
                bmp.Dispose();
                return Convert.ToBase64String(arr);
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return null;
            }
        }



        /// <summary>
        /// 根据代号，精准查询单个料品信息
        /// SAM 2017年9月23日15:18:52
        /// </summary>
        /// <param name="Code"></param>
        /// <returns></returns>
        public static Hashtable GetItem(string Code)
        {
            return SYS_ItemsService.GetItem(Code);
        }



        //public static bool IsDate(string Str)
        //{
        //    return Regex.IsMatch(Str, @"^((((1[6-9]|[2-9]\d)\d{2})-(0?[13578]|1[02])-(0?[1-9]|[12]\d|3[01]))|(((1[6-9]|[2-9]\d)\d{2})-(0?[13456789]|1[012])-(0?[1-9]|[12]\d|30))|(((1[6-9]|[2-9]\d)\d{2})-0?2-(0?[1-9]|1\d|2[0-9]))|(((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))-0?2-29-))$");
        //}


        public class OperationResult
        {
            public int success { get; set; }
            public int fail { get; set; }
            public string failIDs { get; set; }
            public string msg { get; set; }
        }

        public class SaveResult
        {
            public OperationResult inserted { get; set; }
            public OperationResult updated { get; set; }
            public OperationResult deleted { get; set; }
        }

        public static object Save<T>(
            JObject request,
            string userID,
            Func<T, string> GetID,
            Action<T> SetID,
            Func<string, T, bool> insertCallback,
            Func<string, T, bool> updateCallback,
            Func<string, T, bool> deleteCallback,
            Func<T, bool> insertCheckArgsCallBack = null,
            string ArgsInsertErrorTip = null,
            Func<T, bool> updateCheckArgsCallBack = null,
            string ArgsUpdateErrorTip = null)
        {
            JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings();
            jsonSerializerSettings.MissingMemberHandling = MissingMemberHandling.Ignore;
            jsonSerializerSettings.NullValueHandling = NullValueHandling.Ignore;

            JArray insertedArray = request.Value<JArray>("inserted");
            OperationResult insertedResult = new OperationResult();
            if (insertedArray != null && insertedArray.Count > 0)
            {
                insertedResult.fail = 0;
                insertedResult.success = 0;
                foreach (JObject obj in insertedArray)
                {
                    T model = JsonConvert.DeserializeObject<T>(obj.ToString(), jsonSerializerSettings);

                    if (insertCheckArgsCallBack != null && !insertCheckArgsCallBack(model))
                    {
                        insertedResult.fail++;
                        insertedResult.failIDs += "," + GetID(model);
                        insertedResult.msg += string.Format("<br/>{0}", ArgsInsertErrorTip);
                        continue;
                    }

                    string orgID = GetID(model);
                    SetID(model);
                    if (insertCallback(userID, model))
                    {
                        insertedResult.success++;
                    }
                    else
                    {
                        insertedResult.fail++;
                        insertedResult.failIDs += "," + orgID;
                        insertedResult.msg += string.Format("<br/>未知原因添加失败");
                    }
                }
                if (insertedResult.fail > 0)
                {
                    insertedResult.failIDs = insertedResult.failIDs.Substring(1);
                }
            }

            JArray updateArray = request.Value<JArray>("updated");
            OperationResult updatedResult = new OperationResult();
            if (updateArray != null && updateArray.Count > 0)
            {
                updatedResult.fail = 0;
                updatedResult.success = 0;
                foreach (JObject obj in updateArray)
                {
                    T model = JsonConvert.DeserializeObject<T>(obj.ToString(), jsonSerializerSettings);

                    if (updateCheckArgsCallBack != null && !updateCheckArgsCallBack(model))
                    {
                        updatedResult.fail++;
                        updatedResult.failIDs += "," + GetID(model);
                        updatedResult.msg += string.Format("<br/>{0}", ArgsInsertErrorTip);
                        continue;
                    }

                    if (updateCallback(userID, model))
                    {
                        updatedResult.success++;
                    }
                    else
                    {
                        updatedResult.fail++;
                        updatedResult.failIDs += "," + GetID(model);
                        updatedResult.msg += string.Format("<br/>未知原因更新失败");
                    }
                }
                if (updatedResult.fail > 0)
                {
                    updatedResult.failIDs = updatedResult.failIDs.Substring(1);
                }
            }


            JArray deleteArray = request.Value<JArray>("deleted");
            OperationResult deleteResult = new OperationResult();
            if (deleteArray != null && deleteArray.Count > 0)
            {
                deleteResult.fail = 0;
                deleteResult.success = 0;
                foreach (JObject obj in deleteArray)
                {
                    T model = JsonConvert.DeserializeObject<T>(obj.ToString(), jsonSerializerSettings);
                    if (deleteCallback(userID, model))
                    {
                        deleteResult.success++;
                    }
                    else
                    {
                        deleteResult.fail++;
                        deleteResult.failIDs += "," + GetID(model);
                        deleteResult.msg += string.Format("<br/>未知原因删除失败");
                    }
                }
                if (deleteResult.fail > 0)
                {
                    deleteResult.failIDs = deleteResult.failIDs.Substring(1);
                }
            }

            return new SaveResult()
            {
                inserted = insertedResult,
                updated = updatedResult,
                deleted = deleteResult
            };
        }
        /// <summary>
        /// 获取登入者的角色
        /// Mouse 2017年9月5日09:51:51
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public static object GetRoles(string UserID)
        {
            return SYS_UserOrganizationMappingService.GetRoles(UserID);
        }


        /// <summary>
        /// 根据制令单生成下面所有信息
        /// SAM 2017年9月29日15:32:54
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="model"></param>
        public static void FabMo(string userId, SFC_FabricatedMother model)
        {
            try
            {
                SYS_Items itemmodel = SYS_ItemsService.get(model.ItemID);
                //制令单添加成功后
                //需要根据料品添加制品制程
                //然后根据制品制程分别添加制程用料，制程资源，制程关系，制程工序，制程工序用料，制程工序资源，制程工序关系
                //根据料品获取制品制程并添加
                List<SFC_ItemProcess> ItemProcess = SFC_ItemProcessService.GetListByItemID(model.ItemID);
                foreach (SFC_ItemProcess Process in ItemProcess)
                {
                    SFC_FabMoProcess Promodel = new SFC_FabMoProcess();
                    Promodel.FabMoProcessID = UniversalService.GetSerialNumber("SFC_FabMoProcess");
                    Promodel.FabricatedMotherID = model.FabricatedMotherID;
                    Promodel.Quantity = model.Quantity;
                    Promodel.AssignQuantity = 0;
                    Promodel.DifferenceQuantity = 0;
                    Promodel.Sequence = Process.Sequence;
                    Promodel.Status = model.Status;
                    Promodel.ProcessID = Process.ProcessID;
                    Promodel.WorkCenterID = Process.WorkCenterID;
                    Promodel.StartDate = model.StartDate;
                    Promodel.FinishDate = model.FinishDate;
                    Promodel.FinProQuantity = 0;
                    Promodel.OutProQuantity = 0;
                    Promodel.ScrappedQuantity = 0;
                    Promodel.DifferenceQuantity = 0;
                    Promodel.UnitID = model.UnitID;
                    Promodel.UnitRate = 1;
                    Promodel.StandardTime = Process.StandardTime * (int)model.Quantity;
                    Promodel.PrepareTime = Process.PrepareTime;
                    Promodel.SourceID = Process.ItemProcessID;//用于查询关系
                    if (SFC_FabMoProcessService.insert(userId, Promodel))
                    {
                        //根据制程获取制程用料
                        IList<SFC_ItemMaterial> ItemMaterial = SFC_ItemMaterialService.GetMaterialList(Process.ItemProcessID, null);
                        foreach (SFC_ItemMaterial item in ItemMaterial)
                        {
                            SFC_FabMoItem Matmodel = new SFC_FabMoItem();
                            Matmodel.FabMoItemID = UniversalService.GetSerialNumber("SFC_FabMoItem");
                            Matmodel.FabMoProcessID = Promodel.FabMoProcessID;
                            Matmodel.Sequence = item.Sequence.ToString();
                            Matmodel.ItemID = item.ItemID;
                            Matmodel.BaseQuantity = (int)model.Quantity * item.BasicQuantity;
                            Matmodel.AttritionRate = item.AttritionRate;
                            Matmodel.UseQuantity = item.UseQuantity * (int)model.Quantity;
                            if (itemmodel.IsCutMantissa)//是否切除尾数
                            {
                                SYS_Parameters Parmodel = SYS_ParameterService.get(itemmodel.CutMantissa);
                                if (Parmodel != null)
                                {
                                    Matmodel.BaseQuantity = Math.Round(Matmodel.BaseQuantity, Parmodel.Code == "N" ? 0 : int.Parse(Parmodel.Code));
                                    Matmodel.UseQuantity = Math.Round(Matmodel.UseQuantity, Parmodel.Code == "N" ? 0 : int.Parse(Parmodel.Code));
                                }
                            }
                            Matmodel.Status = Framework.SystemID + "0201213000001";
                            Matmodel.Comments = item.Comments;
                            SFC_FabMoItemService.insert(userId, Matmodel);
                        }
                        //根据制程获取制程资源
                        IList<SFC_ItemResource> ItemResource = SFC_ItemResourceService.GetResourceList(Process.ItemProcessID, null, null);
                        foreach (SFC_ItemResource item in ItemResource)
                        {
                            SFC_FabMoResource Resmodel = new SFC_FabMoResource();
                            Resmodel.FabMoResourceID = UniversalService.GetSerialNumber("SFC_FabMoResource");
                            Resmodel.FabMoProcessID = Promodel.FabMoProcessID;
                            Resmodel.ResourceID = item.ResourceID;
                            Resmodel.Type = item.Type;
                            Resmodel.IfMain = item.IfMain;
                            Resmodel.Status = Framework.SystemID + "0201213000001";
                            Resmodel.Comments = item.Comments;
                            SFC_FabMoResourceService.insert(userId, Resmodel);
                        }

                        //根据制程获取制程工序
                        IList<SFC_ItemOperation> ItemOperation = SFC_ItemOperationService.GetOperationList(Process.ItemProcessID);
                        foreach (SFC_ItemOperation item in ItemOperation)
                        {
                            SFC_FabMoOperation Opmodel = new SFC_FabMoOperation();
                            Opmodel.FabMoOperationID = UniversalService.GetSerialNumber("SFC_FabMoOperation");
                            Opmodel.FabricatedMotherID = Promodel.FabricatedMotherID;
                            Opmodel.FabMoProcessID = Promodel.FabMoProcessID;
                            Opmodel.OperationID = item.OperationID;
                            Opmodel.Sequence = item.Sequence;
                            Opmodel.UnitID = item.Unit;
                            Opmodel.UnitRate = item.UnitRatio;
                            Opmodel.Quantity = Promodel.Quantity;
                            Opmodel.StandardTime = item.StandardTime * (int)model.Quantity;
                            Opmodel.PrepareTime = item.PrepareTime;
                            Opmodel.StartDate = Promodel.StartDate;
                            Opmodel.FinishDate = Promodel.FinishDate;
                            Opmodel.Status = model.Status;
                            Opmodel.Comments = item.Comments;
                            Opmodel.SourceID = item.ItemOperationID;
                            SFC_FabMoOperationService.insert(userId, Opmodel);
                            //根据制程工序获取工序用料
                            IList<SFC_ItemMaterial> OpItemMaterial = SFC_ItemMaterialService.GetMaterialList(null, item.ItemOperationID);
                            foreach (SFC_ItemMaterial Opitem in OpItemMaterial)
                            {
                                SFC_FabMoItem Matmodel = new SFC_FabMoItem();
                                Matmodel.FabMoItemID = UniversalService.GetSerialNumber("SFC_FabMoItem");
                                Matmodel.FabMoOperationID = Opmodel.FabMoOperationID;
                                Matmodel.Sequence = Opitem.Sequence.ToString();
                                Matmodel.ItemID = Opitem.ItemID;
                                Matmodel.BaseQuantity = Opitem.BasicQuantity * (int)model.Quantity;
                                Matmodel.AttritionRate = Opitem.AttritionRate;
                                Matmodel.UseQuantity = Opitem.UseQuantity * (int)model.Quantity;
                                if (itemmodel.IsCutMantissa)//是否切除尾数
                                {
                                    SYS_Parameters Parmodel = SYS_ParameterService.get(itemmodel.CutMantissa);
                                    if (Parmodel != null)
                                    {
                                        Matmodel.BaseQuantity = Math.Round(Matmodel.BaseQuantity, Parmodel.Code == "N" ? 0 : int.Parse(Parmodel.Code));
                                        Matmodel.UseQuantity = Math.Round(Matmodel.UseQuantity, Parmodel.Code == "N" ? 0 : int.Parse(Parmodel.Code));
                                    }
                                }
                                Matmodel.Status = Framework.SystemID + "0201213000001";
                                Matmodel.Comments = item.Comments;
                                SFC_FabMoItemService.insert(userId, Matmodel);
                            }
                            //根据制程工序获取工序资源
                            IList<SFC_ItemResource> OpItemResource = SFC_ItemResourceService.GetResourceList(null, item.ItemOperationID, null);
                            foreach (SFC_ItemResource Opitem in ItemResource)
                            {
                                SFC_FabMoResource Resmodel = new SFC_FabMoResource();
                                Resmodel.FabMoResourceID = UniversalService.GetSerialNumber("SFC_FabMoResource");
                                Resmodel.FabMoOperationID = Opmodel.FabMoOperationID;
                                Resmodel.ResourceID = Opitem.ResourceID;
                                Resmodel.Type = Opitem.Type;
                                Resmodel.IfMain = Opitem.IfMain;
                                Resmodel.Status = Framework.SystemID + "0201213000001";
                                Resmodel.Comments = item.Comments;
                                SFC_FabMoResourceService.insert(userId, Resmodel);
                            }
                        }

                        //首先根据制令制程获取刚刚添加好的制令制程工序列表
                        //然后循环工序列表，根据每一道工序对应的制品制程工序，获取设定好的关系列表
                        //循环关系列表，再新增成制令单的制程工序关系

                        IList<SFC_ProcessOperationRelationShip> OperationShip = SFC_ProcessOperationRelationShipService.GetOperationShipList(Process.ItemProcessID);
                        foreach (SFC_ProcessOperationRelationShip shipmodel in OperationShip)
                        {
                            SFC_FabMoOperation OpModel = null;
                            SFC_FabMoOperation PreOpModel = null;
                            SFC_FabMoOperationRelationship Resmodel = new SFC_FabMoOperationRelationship();
                            Resmodel.FabMoOperationRelationshipID = UniversalService.GetSerialNumber("SFC_FabMoOperationRelationship");
                            Resmodel.FabricatedMotherID = Promodel.FabricatedMotherID;
                            Resmodel.FabMoProcessID = Promodel.FabMoProcessID;
                            OpModel = SFC_FabMoOperationService.get(Resmodel.FabMoProcessID, shipmodel.ItemOperationID);

                            if (OpModel == null)
                                continue;
                            Resmodel.FabMoOperationID = OpModel.FabMoOperationID;

                            PreOpModel = SFC_FabMoOperationService.get(Resmodel.FabMoProcessID, shipmodel.PreItemOperationID);
                            if (PreOpModel == null)
                                continue;
                            Resmodel.PreFabMoOperationID = PreOpModel.FabMoOperationID;
                            Resmodel.IsLastOperation = shipmodel.FinishOperation;
                            Resmodel.IfMain = shipmodel.IfMain;
                            Resmodel.Status = Framework.SystemID + "0201213000001";
                            Resmodel.Comments = shipmodel.Comments;
                            SFC_FabMoOperationRelationshipService.insert(userId, Resmodel);
                        }
                    }
                }/*制程添加的循环在这里结束*/

                //然后循环制程列表，根据每一道制程对应的制品制程，获取设定好的制品制程关系列表
                //循环关系列表，再新增成制令单的制程关系
                IList<SFC_ItemProcessRelationShip> ProcessShip = SFC_ItemProcessRelationShipService.GetProcessShipList(model.ItemID);
                foreach (SFC_ItemProcessRelationShip shipmodel in ProcessShip)
                {
                    SFC_FabMoProcess ProModel = null;
                    SFC_FabMoProcess PreProModel = null;
                    SFC_FabMoRelationship Resmodel = new SFC_FabMoRelationship();
                    Resmodel.FabMoRelationshipID = UniversalService.GetSerialNumber("SFC_FabMoRelationship");
                    Resmodel.FabricatedMotherID = model.FabricatedMotherID;
                    ProModel = SFC_FabMoProcessService.get(model.FabricatedMotherID, shipmodel.ItemProcessID);
                    if (ProModel == null)
                        continue;
                    Resmodel.FabMoProcessID = ProModel.FabMoProcessID;

                    PreProModel = SFC_FabMoProcessService.get(model.FabricatedMotherID, shipmodel.PreItemProcessID);
                    if (PreProModel == null)
                        continue;
                    Resmodel.PreFabMoProcessID = PreProModel.FabMoProcessID;
                    Resmodel.IfLastProcess = shipmodel.FinishProcess;
                    Resmodel.IfMain = shipmodel.IfMain;
                    Resmodel.Status = Framework.SystemID + "0201213000001";
                    Resmodel.Comments = shipmodel.Comments;
                    SFC_FabMoRelationshipService.insert(userId, Resmodel);
                }
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
            }
        }


        /// <summary>
        /// 系统设置--上传底图获取列表
        /// Alvin 2017年10月19日10:31:33
        /// </summary>
        /// <param name="token"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public static object GetBaseMapList(string token, int page, int rows)
        {
            int count = 0;
            IList<Hashtable> result = SYS_AttachmentsService.GetBaseMapList(token, page, rows, ref count);
            return getPaginationModel(result, count);
        }


        /// <summary>
        /// 系统设置--上传底图单条删除
        /// Alvin 2017年10月19日11:10:31
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static object MapDelete(JObject request)
        {
            string Token = request.Value<string>("Token");
            string userid = UtilBussinessService.detoken(Token);
            string attacment_id = request.Value<string>("AttachmentID");
            SYS_Attachments model = SYS_AttachmentsService.get(attacment_id);
            if (model == null)
            {
                return new { status = "410", msg = "删除失败!无效数据" };
            }

            if (model.AttachmentID == Framework.SystemID + "0071213000030")
            {
                return new { status = "410", msg = "系统默认底图,无法删除!" };
            }

            model.Status = Framework.SystemID + "0201213000003";
            if (SYS_AttachmentsService.update(userid, model))
            {
                return new { status = "200", msg = "删除成功!" };
            }
            else
            {
                return new { status = "410", msg = "删除失败!" };
            }

        }

        /// <summary>
        /// 上传底图
        /// Alvin 2017年10月19日15:18:13
        /// </summary>
        /// <param name="token"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public static object UploadMap(string token, HttpPostedFile file, string comment)
        {
            try
            {
                string userId = UtilBussinessService.detoken(token);

                string SysID = Framework.SystemID;

                string filePath = "BackgroundMap/";

                string strPath = HttpContext.Current.Server.MapPath("~/");


                //假如文件夹不存在则新建
                if (!Directory.Exists(strPath + filePath))
                {
                    Directory.CreateDirectory(strPath + filePath);
                }


                //  string filePath = AppDomain.CurrentDomain.BaseDirectory + "BackgroundMap";

                // DirectoryInfo di = new DirectoryInfo(filePath);

                // if (!di.Exists)//创建目录
                //{
                //    di.Create();
                //}

                string filename = file.FileName;

                //file.SaveAs(Path.Combine(filePath, filename));


                if (!(filename.EndsWith(".jpg") || filename.EndsWith(".gif") ||
                      filename.EndsWith(".bmp") || filename.EndsWith(".pic") ||
                      filename.EndsWith(".png") || filename.EndsWith(".tiff") ||
                      filename.EndsWith(".jpeg") || filename.EndsWith(".tif")))
                {
                    return new { status = "410", msg = "上传的文件不是图片格式!" };
                }

                int Sequence = SYS_AttachmentsService.getCount(SysID + "007121300000A");
                SYS_Attachments model = new SYS_Attachments();
                model.AttachmentID = UniversalService.GetSerialNumber("SYS_Attachments");
                model.ObjectID = SysID + "007121300000A";
                model.Path = filePath + filename; ;

                file.SaveAs(Path.Combine(strPath + model.Path)); //上传图片

                model.OriginalName = filename;
                model.Name = filename;
                model.Default = true;
                model.Type = null;
                model.Comments = comment;
                model.UploadTime = DateTime.Now;
                model.Sequence = Sequence;
                model.Status = Framework.SystemID + "0201213000001";
                if (SYS_AttachmentsService.insert(userId, model))
                {
                    return new { status = "200", msg = "上传成功！" };
                }

                else
                {
                    return new { status = "410", msg = "上传失败！" };
                }

            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return new { status = "410", msg = "上传失败！" + ex.ToString() };
            }

        }


        /// <summary>
        /// 登录之前获取所有底图
        /// </summary>
        /// <returns></returns>
        public static IList<Hashtable> GetMap()
        {
            IList<Hashtable> result = SYS_AttachmentsService.GetMap();
            return result;
        }

    }
}
