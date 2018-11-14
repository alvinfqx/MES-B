using MonkeyFly.Core;
using MonkeyFly.MES.BasicService;
using MonkeyFly.MES.Models;
using MonkeyFly.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace MonkeyFly.MES.ModelServices
{
    public class TRAN_TRANSFER_LOGService : SuperModel<TRAN_TRANSFER_LOG>
    {
        /// <summary>
        /// 新增
        /// Mouse 2017年9月8日14:10:25
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool insert(string userid,TRAN_TRANSFER_LOG model)
        {
            try {
                string sql = string.Format(@"insert ([Data],[KeyColum],[TransferDate],[Log])values(@Data,@KeyColum,@TransferDate,@Log,'{0}','{1}','{2}','{0}','{1}','{2}','{3}')", userid, DateTime.Now, DateTime.Now, Framework.SystemID);
                SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter("@Data",SqlDbType.NVarChar),
                new SqlParameter("@KeyColum", SqlDbType.NVarChar),
                new SqlParameter("@TransferDate", SqlDbType.NVarChar),
                new SqlParameter("@Log", SqlDbType.NVarChar)
                
            };
                parameters[0].Value = (Object)model.Data ?? DBNull.Value;
                parameters[1].Value = (Object)model.KeyColum ?? DBNull.Value;
                parameters[2].Value = (Object)model.TransferDate ?? DBNull.Value;
                parameters[3].Value = (Object)model.Log ?? DBNull.Value;
                return SQLHelper.ExecuteNonQuery(sql, CommandType.Text,parameters) > 0;
            }
            catch(Exception ex) {
                DataLogerService.writeerrlog(ex);
                return false;
            }
        }

        /// <summary>
        ///  MES资料转入处理-列表查询
        ///  SAM 2017年8月31日15:39:17
        /// </summary>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="StartCode"></param>
        /// <param name="EndCode"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> Trn00002GetList(string StartDate, string EndDate, string StartCode, string EndCode,string Type, int page, int rows, ref int count)
        {
            string select = string.Format(@"select Data,KeyColum,TransferDate,Log ");

            string sql = string.Format(@" from [TRAN_TRANSFER_LOG] where ([KeyColum] <> 'Start' and [KeyColum] <> 'End') ");

            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@StartDate",SqlDbType.VarChar),
                new SqlParameter("@EndDate",SqlDbType.VarChar),
                new SqlParameter("@StartCode",SqlDbType.VarChar),
                new SqlParameter("@EndCode",SqlDbType.VarChar),
                new SqlParameter("@Type",SqlDbType.VarChar)
            };
            parameters[0].Value = DBNull.Value;
            parameters[1].Value = DBNull.Value;
            parameters[2].Value = DBNull.Value;
            parameters[3].Value = DBNull.Value;
            parameters[4].Value = DBNull.Value;

            SqlParameter[] Parcount = new SqlParameter[]{
               new SqlParameter("@StartDate",SqlDbType.VarChar),
               new SqlParameter("@EndDate",SqlDbType.VarChar),
               new SqlParameter("@StartCode",SqlDbType.VarChar),
               new SqlParameter("@EndCode",SqlDbType.VarChar),
               new SqlParameter("@Type",SqlDbType.VarChar)
            };
            Parcount[0].Value = DBNull.Value;
            Parcount[1].Value = DBNull.Value;
            Parcount[2].Value = DBNull.Value;
            Parcount[3].Value = DBNull.Value;
            Parcount[4].Value = DBNull.Value;

            if (!string.IsNullOrWhiteSpace(StartDate))
            {
                sql = sql + String.Format(@" and [TransferDate] >= @StartDate ");
                parameters[0].Value = StartDate;
                Parcount[0].Value = StartDate;
            }

            if (!string.IsNullOrWhiteSpace(EndDate))
            {
                try
                {
                    DateTime End = DateTime.Parse(EndDate).AddDays(1).AddSeconds(-1);
                    sql = sql + String.Format(@" and [TransferDate] <= @EndDate ");
                    parameters[1].Value = End.ToString();
                    Parcount[1].Value = End.ToString();
                }
                catch
                {
                    //TODO
                    //输入的结束日期有误，应该做啥好呢！
                    //啥都不做吧
                }
            }

            if (!string.IsNullOrWhiteSpace(StartCode))
            {
                sql = sql + String.Format(@" and [KeyColum] >= @StartCode ");
                parameters[2].Value = StartCode;
                Parcount[2].Value = StartCode;
            }

            if (!string.IsNullOrWhiteSpace(EndCode))
            {
                sql = sql + String.Format(@" and [KeyColum] <= @EndCode ");
                parameters[3].Value = EndCode;
                Parcount[3].Value = EndCode;
            }

            if (!string.IsNullOrWhiteSpace(Type))
            {
                sql = sql + String.Format(@" and [Data] = @Type ");
                parameters[4].Value = Type;
                Parcount[4].Value = Type;
            }

            count = UniversalService.getCount(sql, Parcount);

            string orderby = "[TransferDate] desc ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, parameters, page, rows);

            return ToHashtableList(dt);
        }
    }
}
