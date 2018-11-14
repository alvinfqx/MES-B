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
    public class SYS_AutoNumberRecordService : SuperModel<SYS_AutoNumberRecord>
    {
        /// <summary>
        /// 新增
        /// SAM 2017年5月17日15:43:48
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="Model"></param>
        /// <returns></returns>
        public static bool insert(string userId, SYS_AutoNumberRecord Model)
        {
            try
            {
                string sql = string.Format(@"insert[SYS_AutoNumberRecord]([AutoNumberRecordID],[AutoNumberID],[Prevchar],[Num],[Status],
                [Comments],[Modifier],[ModifiedTime],[ModifiedLocalTime],[Creator],[CreateTime],[CreateLocalTime],[SystemID]) values
                    (@AutoNumberRecordID,@AutoNumberID,@Prevchar,@Num,@Status,
                        @Comments,'{0}','{1}','{2}','{0}','{1}','{2}','{3}')", userId, DateTime.Now, DateTime.Now, Framework.SystemID);

                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@AutoNumberRecordID",SqlDbType.VarChar),
                    new SqlParameter("@AutoNumberID",SqlDbType.VarChar),
                    new SqlParameter("@Prevchar",SqlDbType.VarChar),
                    new SqlParameter("@Num",SqlDbType.Int),
                    new SqlParameter("@Status",SqlDbType.VarChar),
                    new SqlParameter("@Comments",SqlDbType.VarChar),
                    };

                parameters[0].Value = (Object)Model.AutoNumberRecordID ?? DBNull.Value;
                parameters[1].Value = (Object)Model.AutoNumberID ?? DBNull.Value;
                parameters[2].Value = (Object)Model.Prevchar ?? DBNull.Value;
                parameters[3].Value = (Object)Model.Num ?? DBNull.Value;
                parameters[4].Value = (Object)Model.Status ?? DBNull.Value;
                parameters[5].Value = (Object)Model.Comments ?? DBNull.Value;

                return SQLHelper.ExecuteNonQuery(sql, CommandType.Text, parameters) > 0;
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return false;
            }
        }

        /// <summary>
        /// 更新
        /// SAM 2017年5月17日15:43:55
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="Model"></param>
        /// <returns></returns>
        public static bool update(string userId, SYS_AutoNumberRecord Model)
        {
            try
            {
                string sql = String.Format(@"update[SYS_AutoNumberRecord] set {0},
                [Prevchar]=@Prevchar,[Num]=@Num,[Status]=@Status where [AutoNumberRecordID]=@AutoNumberRecordID", UniversalService.getUpdateUTC(userId));
                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@AutoNumberRecordID",SqlDbType.VarChar),
                    new SqlParameter("@Prevchar",SqlDbType.VarChar),
                    new SqlParameter("@Num",SqlDbType.Int),
                    new SqlParameter("@Status",SqlDbType.VarChar),
                    };

                parameters[0].Value = Model.AutoNumberRecordID;
                parameters[1].Value = (Object)Model.Prevchar ?? DBNull.Value;
                parameters[2].Value = (Object)Model.Num ?? DBNull.Value;
                parameters[3].Value = (Object)Model.Status ?? DBNull.Value;

                return SQLHelper.ExecuteNonQuery(sql, CommandType.Text, parameters) > 0;
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return false;
            }
        }

        /// <summary>
        /// 获取单一实体
        /// SAM 2017年5月17日15:44:12
        /// </summary>
        /// <param name="AutoNumberRecordID"></param>
        /// <returns></returns>
        public static SYS_AutoNumberRecord get(string AutoNumberRecordID)
        {
            string sql = string.Format(@"select Top 1 * from [SYS_AutoNumberRecord] where [AutoNumberRecordID] = '{0}'  and [SystemID] = '{1}' ", AutoNumberRecordID, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return null;
            else
                return ToEntity(dt.Rows[0]);
        }

        /// <summary>
        /// 根据类别流水号和给号字轨获取记录
        /// SAM 2017年5月24日11:32:23
        /// </summary>
        /// <param name="AutoNumberID"></param>
        /// <returns></returns>
        public static SYS_AutoNumberRecord getByAutoNumber(string AutoNumberID,string Prevchar)
        {
            string sql = string.Format(@"select Top 1 * from [SYS_AutoNumberRecord] where [AutoNumberID] = '{0}' and [Prevchar]='{2}' and [SystemID] = '{1}' and [Status]='{1}0201213000001' ", AutoNumberID, Framework.SystemID, Prevchar);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return null;
            else
                return ToEntity(dt.Rows[0]);
        }

        public static SYS_AutoNumberRecord GetByAutoNumberID(string AutoNumberID)
        {
            string sql = string.Format(
                @"select Top 1 * 
                  from [SYS_AutoNumberRecord] 
                  where [AutoNumberID] = '{0}' and [SystemID] = '{1}' and [Status]='{1}0201213000001' ", AutoNumberID, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return null;
            else
                return ToEntity(dt.Rows[0]);
        }

        /// <summary>
        /// 批號紀錄列表
        /// SAM 2017年5月17日16:04:06
        /// </summary>
        /// <param name="Code">代号</param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> Inf00023RecordGetList(string AutoNumberID, int page, int rows, ref int count)
        {
            string select = string.Format(@"select A.AutoNumberRecordID,
            D.Code,D.Description,A.Prevchar,A.Num,A.Comments,     
            B.UserName as Creator,A.CreateLocalTime as CreateTime,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else C.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ");

            string sql = string.Format(@" from [SYS_AutoNumberRecord] A 
            left join [SYS_MESUsers] B on A.Creator = B.MESUserID
            left join [SYS_MESUsers] C on A.Modifier = C.MESUserID
            left join [SYS_AutoNumber] D on A.AutoNumberID = D.AutoNumberID
            where A.[SystemID]='{0}' and A.Status <> '{0}0201213000003' and A.[AutoNumberID]='{1}'", Framework.SystemID, AutoNumberID);

            count = UniversalService.getCount(sql, null);

            string orderby = "A.[CreateTime] desc";

            DataTable dt = UniversalService.getTable(select, sql, orderby, null, page, rows);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// 完工單回報作業-批號屬性—取號开窗
        /// SAM 2017年7月20日14:35:32
        /// </summary>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> Sfc00007GetLotAutoNumber(int page, int rows, ref int count)
        {
            string select = string.Format(@"A.AutoNumberRecordID,A.AutoNumberID,
            D.Code,D.Description,A.Prevchar,A.Num,A.Comments,
            A.Prevchar+right('0000'+convert(varchar(20),Num),D.NumLength) as Str,
            B.UserName as Creator,A.CreateLocalTime as CreateTime,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else C.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ");

            string sql = string.Format(@" from [SYS_AutoNumberRecord] A 
            left join [SYS_MESUsers] B on A.Creator = B.MESUserID
            left join [SYS_MESUsers] C on A.Modifier = C.MESUserID
            left join [SYS_AutoNumber] D on A.AutoNumberID = D.AutoNumberID
            where A.[SystemID]='{0}' and A.Status = '{0}0201213000001'", Framework.SystemID);

            count = UniversalService.getCount(sql, null);

            string orderby = "A.[CreateTime] desc";

            DataTable dt = UniversalService.getTable(select, sql, orderby, null, page, rows);

            return ToHashtableList(dt);
        }
    }
}

