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
    public class QCS_CheckTestSettingDetailsService : SuperModel<QCS_CheckTestSettingDetails>
    {
        /// <summary>
        /// ����
        /// SAM 2017��6��5��11:22:31
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="Model"></param>
        /// <returns></returns>
        public static bool insert(string userId, QCS_CheckTestSettingDetails Model)
        {
            try
            {
                string sql = string.Format(@"insert[QCS_CheckTestSettingDetails]([CTSDID],[CheckTestSettingID],[Sequence],[StartBatch],[EndBatch],[SamplingQuantity],[AcQuantity],[ReQuantity],[Status],[Comments],[Modifier],[ModifiedTime],[ModifiedLocalTime],[Creator],[CreateTime],[CreateLocalTime],[SystemID]) values
                 (@CTSDID,@CheckTestSettingID,@Sequence,@StartBatch,@EndBatch,@SamplingQuantity,@AcQuantity,@ReQuantity,
                @Status,@Comments,'{0}','{1}','{2}','{0}','{1}','{2}','{3}')", userId, DateTime.Now, DateTime.Now, Framework.SystemID);

                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@CTSDID",SqlDbType.VarChar),
                    new SqlParameter("@CheckTestSettingID",SqlDbType.VarChar),
                    new SqlParameter("@Sequence",SqlDbType.Int),
                    new SqlParameter("@StartBatch",SqlDbType.Decimal),
                    new SqlParameter("@EndBatch",SqlDbType.Decimal),
                    new SqlParameter("@SamplingQuantity",SqlDbType.Decimal),
                    new SqlParameter("@AcQuantity",SqlDbType.Decimal),
                    new SqlParameter("@ReQuantity",SqlDbType.Decimal),
                    new SqlParameter("@Status",SqlDbType.VarChar),
                    new SqlParameter("@Comments",SqlDbType.NVarChar),
                    };

                parameters[0].Value = (Object)Model.CTSDID ?? DBNull.Value;
                parameters[1].Value = (Object)Model.CheckTestSettingID ?? DBNull.Value;
                parameters[2].Value = (Object)Model.Sequence ?? DBNull.Value;
                parameters[3].Value = (Object)Model.StartBatch ?? DBNull.Value;
                parameters[4].Value = (Object)Model.EndBatch ?? DBNull.Value;
                parameters[5].Value = (Object)Model.SamplingQuantity ?? DBNull.Value;
                parameters[6].Value = (Object)Model.AcQuantity ?? DBNull.Value;
                parameters[7].Value = (Object)Model.ReQuantity ?? DBNull.Value;
                parameters[8].Value = (Object)Model.Status ?? DBNull.Value;
                parameters[9].Value = (Object)Model.Comments ?? DBNull.Value;

                return SQLHelper.ExecuteNonQuery(sql, CommandType.Text, parameters) > 0;
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return false;
            }
        }

        /// <summary>
        /// ����
        /// SAM 2017��6��5��11:22:38
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="Model"></param>
        /// <returns></returns>
        public static bool update(string userId, QCS_CheckTestSettingDetails Model)
        {
            try
            {
                string sql = String.Format(@"update[QCS_CheckTestSettingDetails] set {0},
                [StartBatch]=@StartBatch,
                [EndBatch]=@EndBatch,[SamplingQuantity]=@SamplingQuantity,[AcQuantity]=@AcQuantity,[ReQuantity]=@ReQuantity,
                [Status]=@Status,[Comments]=@Comments where [CTSDID]=@CTSDID", UniversalService.getUpdateUTC(userId));
                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@CTSDID",SqlDbType.VarChar),
                    new SqlParameter("@StartBatch",SqlDbType.Decimal),
                    new SqlParameter("@EndBatch",SqlDbType.Decimal),
                    new SqlParameter("@SamplingQuantity",SqlDbType.Decimal),
                    new SqlParameter("@AcQuantity",SqlDbType.Decimal),
                    new SqlParameter("@ReQuantity",SqlDbType.Decimal),
                    new SqlParameter("@Status",SqlDbType.VarChar),
                    new SqlParameter("@Comments",SqlDbType.NVarChar),
                    };

                parameters[0].Value = Model.CTSDID;
                parameters[1].Value = (Object)Model.StartBatch ?? DBNull.Value;
                parameters[2].Value = (Object)Model.EndBatch ?? DBNull.Value;
                parameters[3].Value = (Object)Model.SamplingQuantity ?? DBNull.Value;
                parameters[4].Value = (Object)Model.AcQuantity ?? DBNull.Value;
                parameters[5].Value = (Object)Model.ReQuantity ?? DBNull.Value;
                parameters[6].Value = (Object)Model.Status ?? DBNull.Value;
                parameters[7].Value = (Object)Model.Comments ?? DBNull.Value;

                return SQLHelper.ExecuteNonQuery(sql, CommandType.Text, parameters) > 0;
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return false;
            }
        }

        /// <summary>
        /// ��ȡ��һʵ��
        /// SAM 2017��6��5��11:22:48
        /// </summary>
        /// <param name="CTSDID"></param>
        /// <returns></returns>
        public static QCS_CheckTestSettingDetails get(string CTSDID)
        {
            string sql = string.Format(@"select Top 1 * from [QCS_CheckTestSettingDetails] where [CTSDID] = '{0}'  and [SystemID] = '{1}' ", CTSDID, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return null;
            else
                return ToEntity(dt.Rows[0]);
        }

        /// <summary>
        /// �ж�����Ƿ��ظ�
        /// SAM 2017��6��6��15:57:00
        /// </summary>
        /// <param name="Sequence"></param>
        /// <param name="CTSDID"></param>
        /// <returns></returns>
        public static bool CheckSequence(string Sequence, string CheckTestSettingID,string CTSDID)
        {
            string sql = string.Format(@"select Top 1 * from [QCS_CheckTestSettingDetails] where [SystemID]='{0}' and Status <> '{0}0201213000003' and [CheckTestSettingID]='{1}'", Framework.SystemID, CheckTestSettingID);

            /*�ȶ���Code��Ĭ�ϸ�DbNull,��������Ҳ���*/
            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@Sequence",SqlDbType.VarChar)
            };
            parameters[0].Value = DBNull.Value;

            /*��ΪCode��ͨ���ֶ�����ģ�������Ҫ�ò�������ʽȥƴSQL*/
            if (!string.IsNullOrWhiteSpace(Sequence))
            {
                sql = sql + String.Format(@" and [Sequence] =@Sequence ");
                parameters[0].Value = Sequence;
            }

            /*CTSDID����ˮ�ţ������ڸ���ʱ���ų����Լ�*/
            if (!string.IsNullOrWhiteSpace(CTSDID))
                sql = sql + String.Format(@" and [CTSDID] <> '{0}' ", CTSDID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text, parameters);

            if (dt.Rows.Count == 0)
                return false;
            else
                return true;
        }

        /// <summary>
        /// ���ݱ�ͷ����ȡ����ϸ�������Ǹ����
        /// SAM 2017��6��7��09:43:07
        /// </summary>
        /// <param name="CTSDID"></param>
        /// <returns></returns>
        public static QCS_CheckTestSettingDetails getSequence(string CheckTestSettingID)
        {
            string sql = string.Format(@"select Top 1 * from [QCS_CheckTestSettingDetails] where [CheckTestSettingID] = '{0}'  and [SystemID] = '{1}' order By [Sequence] desc ", CheckTestSettingID, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return null;
            else
                return ToEntity(dt.Rows[0]);
        }


        /// <summary>
        /// ���ݳ��������趨��ȡ������ϸ
        /// SAM 2017��6��5��11:28:34
        /// </summary>
        /// <param name="checkTestSettingID"></param>
        /// <param name="page">ҳ��</param>
        /// <param name="rows">����</param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> Qcs00001GetDetailsList(string checkTestSettingID)
        {
            string select = string.Format(@"select A.CTSDID,A.CheckTestSettingID,A.Sequence,A.StartBatch,
            A.EndBatch,A.SamplingQuantity,A.AcQuantity,A.ReQuantity,A.Comments,
            B.UserName as Creator,A.CreateLocalTime as CreateTime,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else C.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ");

            string sql = string.Format(@"  from [QCS_CheckTestSettingDetails] A 
            left join [SYS_MESUsers] B on A.[Creator] = B.[MESUserID]
            left join [SYS_MESUsers] C on A.[Modifier] = C.[MESUserID]      
            where A.[SystemID]='{0}' and A.[Status] <> '{0}0201213000003' and [CheckTestSettingID] = '{1}' ", Framework.SystemID, checkTestSettingID);

    
            String orderby = " order by A.Sequence ";

            DataTable dt = SQLHelper.ExecuteDataTable(select+sql+orderby,CommandType.Text);

            return ToHashtableList(dt);
        }

        public static QCS_CheckTestSettingDetails getCTSDetails(string CheckTestSettingID)
        {
            string sql = string.Format(@"select Top 1 * from [QCS_CheckTestSettingDetails] where [CheckTestSettingID] = '{0}'  and [SystemID] = '{1}' ", CheckTestSettingID, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return null;
            else
                return ToEntity(dt.Rows[0]);
        }

        /// <summary>
        /// ���ݷ�������ȡָ����Χ�ڵ�����
        /// SAM 2017��10��23��15:25:02
        /// </summary>
        /// <param name="CheckTestSettingID"></param>
        /// <param name="AssignQuantity"></param>
        /// <returns></returns>
        public static QCS_CheckTestSettingDetails GetDetails(string CheckTestSettingID,decimal AssignQuantity)
        {
            string sql = string.Format(@"select Top 1 * from [QCS_CheckTestSettingDetails] 
            where [CheckTestSettingID] = '{0}'  and [SystemID] = '{1}' and [StartBatch] <= {2} and [EndBatch] >= {2} ", CheckTestSettingID, Framework.SystemID,AssignQuantity);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return null;
            else
                return ToEntity(dt.Rows[0]);
        }

    }
}

