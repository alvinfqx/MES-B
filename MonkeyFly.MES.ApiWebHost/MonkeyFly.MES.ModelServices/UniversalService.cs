using MonkeyFly.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;

namespace MonkeyFly.MES.ModelServices
{
    public class UniversalService
    {
        /// <summary>
        /// 返回列表查询的分页列表
        /// SAM 2015年6月17日15:55:11
        /// </summary>
        /// <param name="select">查询字段</param>
        /// <param name="fromwhere">from 与 where</param>
        /// <param name="orderBy">排序的字段名,可倒序,如：XXX desc；可多条件排序,如：XXX1,XXX2</param>
        /// <param name="page">第几页</param>
        /// <param name="rows">每页行数</param>
        /// <returns></returns>
        public static DataTable getTable(String select, String fromwhere, String orderBy, SqlParameter[] parameters, int page, int rows)
        {
            return SQLHelper.ExecuteDataTable(String.Format("SELECT * FROM ( {0}, ROW_NUMBER() OVER (ORDER BY {2}) AS RowNumber {1} ) sqlWord " +
                "WHERE   RowNumber > {3} AND RowNumber <= {4} ORDER BY RowNumber", select, fromwhere, orderBy, (page - 1) * rows, page * rows), CommandType.Text, parameters);
        }

        /// <summary>
        /// 返回列表查询的分页列表（去重专用）
        /// </summary>
        /// <param name="select"></param>
        /// <param name="fromwhere"></param>
        /// <param name="orderBy"></param>
        /// <param name="RoworderBy"></param>
        /// <param name="parameters"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public static DataTable GetTableDistinct(String select, String fromwhere, String orderBy, SqlParameter[] parameters, int page, int rows)
        {
            return SQLHelper.ExecuteDataTable(String.Format(@"SELECT * FROM ( select *, ROW_NUMBER() OVER (ORDER BY {2}) AS RowNumber 
               from({0} {1} ) SS ) sqlWord 
                WHERE   RowNumber > {3} AND RowNumber <= {4} ORDER BY RowNumber", select, fromwhere, orderBy,  (page - 1) * rows, page * rows), CommandType.Text, parameters);
        }

        /// <summary>
        /// 返回列表查询的总行数
        /// SAM 2015年6月17日15:55:15
        /// </summary>
        /// <param name="sql">查询的sql语句</param>
        /// <returns></returns>
        public static int getCount(String fromwhere, SqlParameter[] parameters)
        {
            DataTable table = SQLHelper.ExecuteDataTable("select Count(*) " + fromwhere, CommandType.Text, parameters);
            return int.Parse(table.Rows[0][0].ToString());
        }

        /// <summary>
        /// 获取分页Json数据
        /// SAM 2015年7月7日10:41:08
        /// </summary>
        /// <param name="total">记录总数</param>
        /// <param name="rows">数据集</param>
        /// <returns></returns>
        public static object getPaginationModel(IList<Hashtable> rows, int total)
        {
            return new { total = total, rows = rows };
        }

        /// <summary>
        /// 获取插入小尾巴
        /// SAM 2016年6月20日11:26:39
        /// </summary>
        /// <param name="emoid">当前登录用户编号</param>
        /// <returns></returns>
        public static string getInsert(string emoid)
        {
            DateTime now = DateTime.Now;
            return @"'" + Framework.SystemID + "','" + emoid + "','" + now + "',N'" + emoid + "','" + now + "'";
        }

        /// <summary>
        /// 获取插入小尾巴
        /// SAM 2016年6月20日11:26:39
        /// </summary>
        /// <param name="emoid">当前登录用户编号</param>
        /// <returns></returns>
        public static string getInsertnew(string emoid)
        {
            DateTime now = DateTime.Now;
            DateTime UTCnow = DateTime.Now;
            return @"'" + Framework.SystemID + "','" + emoid + "','" + UTCnow + "','" + now + "',N'" + emoid + "','" + UTCnow + "','" + now + "'";
        }

        /// <summary>
        /// 获取更新小尾巴
        /// SAM 2016年6月20日11:26:49
        /// </summary>
        /// <param name="emoid">当前登录用户编号</param>
        /// <returns></returns>
        public static string getUpdate(string emoid)
        {
            string result = string.Format(@"[Modifier]='{0}',[ModifiedTime]='{1}'", emoid, DateTime.Now);
            return result;
        }

        /// <summary>
        /// 获取更新小尾巴（这个小尾巴包含了ModifiedLocalTime字段）
        /// SAM 2016年12月3日10:21:46
        /// </summary>
        /// <param name="emoid"></param>
        /// <returns></returns>
        public static string getUpdateUTC(string emoid)
        {
            String result = String.Format(@"[Modifier]='{0}',[ModifiedTime]='{1}',[ModifiedLocalTime]='{2}' ", emoid, DateTime.UtcNow, DateTime.Now);
            return result;
        }

        /// <summary>
        /// 获取一个编号（根据表名）
        /// SAM 2015年8月1日14:03:58
        /// </summary>
        /// <param name="TableName">表名</param>
        /// <returns></returns>
        public static string GetSerialNumber(string TableName)
        {
            return SQLHelper.getSNID(TableName, 1)[0];
        }

        /// <summary>
        /// 获取多个编号（根据表名）
        /// SAM 2015年8月8日10:48:39
        /// </summary>
        /// <param name="TableName">表名</param>
        /// <param name="Amount">编号数量</param>
        /// <returns></returns>
        public static List<string> GetSerialNumber(string TableName, int Amount)
        {
            return SQLHelper.getSNID(TableName, Amount).ToList();
        }


        //@Title detoken 
        //@Description 解析ken，返回userid
        //@Author Sam
        //@Date 2016-5-23 06:57:16
        //@传入值：Token：Token
        //@传出值：userid
        public static String detoken(String Token)
        {
            String token = Utils.EncryptHelper.DESDecrypt(Token);//解析Token
            return token.Split('-')[0];//返回userid
        }

        /// <summary>
        /// 检查值是否为空，用于SQL语句
        /// SAM 2016年2月16日10:24:42
        /// </summary>
        /// <param name="time">时间</param>
        /// <returns></returns>
        public static string checkNullForSQL(string value)
        {
            return (String.IsNullOrWhiteSpace(value) ? "NULL" : "'" + value + "'");
        }

        /// <summary>
        /// 去掉html标签，只取文本信息
        /// SAM 2016年3月10日16:53:15
        /// </summary>
        /// <param name="text">文本</param>
        /// <returns></returns>
        public static string getText(string text)
        {
            string strText = Regex.Replace(text, "<[^>]+>", "");
            strText = Regex.Replace(strText, "&[^;]+;", "");

            return strText;
        }

        /// <summary>
        /// 处理字符串含有的'号
        ///  SAM 2016年3月25日15:50:05
        /// </summary>
        /// <param name="sqlString"></param>
        /// <returns></returns>
        public static String getString(String String)
        {
            String result = "";
            if (!String.IsNullOrWhiteSpace(String))
            {
                int count = Regex.Matches(String, "'").Count;
                if (count > 0)
                {
                    String[] temp = String.Split('\'');
                    for (int i = 0; i < count; i++)
                        result += temp[i] + "''";
                }
                else
                    result = String;
            }
            else
                result = String;
            return result;
        }

        /// <summary>
        /// 获取当前登录用户的语序
        /// SAM 2017年8月16日22:42:13
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
    }
}
