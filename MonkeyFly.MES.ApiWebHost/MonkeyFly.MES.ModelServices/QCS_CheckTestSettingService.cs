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
    public class QCS_CheckTestSettingService : SuperModel<QCS_CheckTestSetting>
    {
        /// <summary>
        /// ����
        /// SAM 2017��6��5��10:55:34
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="Model"></param>
        /// <returns></returns>
        public static bool insert(string userId, QCS_CheckTestSetting Model)
        {
            try
            {
                string sql = string.Format(@"insert[QCS_CheckTestSetting]([CheckTestSettingID],[InspectionStandard],[InspectionLevel],[InspectionMethod],
                [AQL],[Status],[Comments],[Modifier],[ModifiedTime],[ModifiedLocalTime],[Creator],[CreateTime],[CreateLocalTime],[SystemID]) values
                 (@CheckTestSettingID,@InspectionStandard,@InspectionLevel,@InspectionMethod,@AQL,
                @Status,@Comments,'{0}','{1}','{2}','{0}','{1}','{2}','{3}')", userId, DateTime.Now, DateTime.Now, Framework.SystemID);

                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@CheckTestSettingID",SqlDbType.VarChar),
                    new SqlParameter("@InspectionStandard",SqlDbType.NVarChar),
                    new SqlParameter("@InspectionLevel",SqlDbType.VarChar),
                    new SqlParameter("@InspectionMethod",SqlDbType.VarChar),
                    new SqlParameter("@AQL",SqlDbType.VarChar),
                    new SqlParameter("@Status",SqlDbType.VarChar),
                    new SqlParameter("@Comments",SqlDbType.NVarChar),
                    };

                parameters[0].Value = (Object)Model.CheckTestSettingID ?? DBNull.Value;
                parameters[1].Value = (Object)Model.InspectionStandard ?? DBNull.Value;
                parameters[2].Value = (Object)Model.InspectionLevel ?? DBNull.Value;
                parameters[3].Value = (Object)Model.InspectionMethod ?? DBNull.Value;
                parameters[4].Value = (Object)Model.AQL ?? DBNull.Value;
                parameters[5].Value = (Object)Model.Status ?? DBNull.Value;
                parameters[6].Value = (Object)Model.Comments ?? DBNull.Value;

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
        /// SAM 2017��6��5��10:55:41
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="Model"></param>
        /// <returns></returns>
        public static bool update(string userId, QCS_CheckTestSetting Model)
        {
            try
            {
                string sql = String.Format(@"update[QCS_CheckTestSetting] set {0},
                [InspectionLevel]=@InspectionLevel,[InspectionMethod]=@InspectionMethod,
                [AQL]=@AQL,[Status]=@Status,[Comments]=@Comments where [CheckTestSettingID]=@CheckTestSettingID", UniversalService.getUpdateUTC(userId));
                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@CheckTestSettingID",SqlDbType.VarChar),
                    new SqlParameter("@InspectionLevel",SqlDbType.VarChar),
                    new SqlParameter("@InspectionMethod",SqlDbType.VarChar),
                    new SqlParameter("@AQL",SqlDbType.VarChar),
                    new SqlParameter("@Status",SqlDbType.VarChar),
                    new SqlParameter("@Comments",SqlDbType.NVarChar),
                    };

                parameters[0].Value = Model.CheckTestSettingID;
                parameters[1].Value = (Object)Model.InspectionLevel ?? DBNull.Value;
                parameters[2].Value = (Object)Model.InspectionMethod ?? DBNull.Value;
                parameters[3].Value = (Object)Model.AQL ?? DBNull.Value;
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
        /// ��ȡ��һʵ��
        /// SAM 2017��6��5��10:55:49
        /// </summary>
        /// <param name="CheckTestSettingID"></param>
        /// <returns></returns>
        public static QCS_CheckTestSetting get(string CheckTestSettingID)
        {
            string sql = string.Format(@"select Top 1 * from [QCS_CheckTestSetting] where [CheckTestSettingID] = '{0}'  and [SystemID] = '{1}' ", CheckTestSettingID, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return null;
            else
                return ToEntity(dt.Rows[0]);
        }

        /// <summary>
        /// ���ݙz�ˮ�ʣ����鷽ʽ�� AQLֵ��ȡʵ��
        /// SAM 2017��6��6��15:21:25
        /// </summary>
        /// <param name="CheckTestSettingID"></param>
        /// <returns></returns>
        public static QCS_CheckTestSetting get(string InspectionLevel,string InspectionMethod,string AQL)
        {
            string sql = string.Format(@"select Top 1 * from [QCS_CheckTestSetting] where [InspectionLevel] = '{0}'  and [SystemID] = '{1}'  and [InspectionMethod] = '{2}'  and [AQL] = '{3}' and [Status] <> '{1}0201213000003' ", InspectionLevel, Framework.SystemID, InspectionMethod,AQL);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return null;
            else
                return ToEntity(dt.Rows[0]);
        }

        /// <summary>
        /// �ж��ظ�
        /// SAM 2017��6��5��11:12:46
        /// </summary>
        /// <param name="InspectionStandard"></param>
        /// <param name="InspectionLevel"></param>
        /// <param name="InspectionMethod"></param>
        /// <param name="AQL"></param>
        /// <param name="CheckTestSettingID"></param>
        /// <returns></returns>
        public static bool Check(string InspectionStandard, string InspectionLevel, string InspectionMethod, string AQL, string CheckTestSettingID)
        {
            string sql = string.Format(@"select Top 1 * from [QCS_CheckTestSetting] where [SystemID]='{0}' and [Status] <> '{0}0201213000003' ", Framework.SystemID);

            /*�ȶ���Name��Code��Ĭ�ϸ�DbNull,��������Ҳ���*/
            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@InspectionStandard",SqlDbType.VarChar)
            };
            parameters[0].Value = DBNull.Value;

            /*��ΪInspectionStandard��ͨ���ֶ�����ģ�������Ҫ�ò�������ʽȥƴSQL*/
            if (!string.IsNullOrWhiteSpace(InspectionStandard))
            {
                sql = sql + String.Format(@" and [InspectionStandard] =@InspectionStandard ");
                parameters[0].Value = InspectionStandard;
            }

            if (!string.IsNullOrWhiteSpace(InspectionLevel))
                sql = sql + String.Format(@" and [InspectionLevel] = '{0}' ", InspectionLevel);

            if (!string.IsNullOrWhiteSpace(InspectionMethod))
                sql = sql + String.Format(@" and [InspectionMethod] = '{0}' ", InspectionMethod);

            if (!string.IsNullOrWhiteSpace(AQL))
                sql = sql + String.Format(@" and [AQL] = '{0}' ", AQL);

            /*CheckTestSettingID��������ˮ�ţ������ڸ���ʱ���ų����Լ�*/
            if (!string.IsNullOrWhiteSpace(CheckTestSettingID))
                sql = sql + String.Format(@" and [CheckTestSettingID] <> '{0}' ", CheckTestSettingID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text, parameters);

            if (dt.Rows.Count == 0)
                return false;
            else
                return true;
        }


        /// <summary>
        /// ���������趨���б�
        /// SAM 2017��6��5��10:42:173
        /// </summary>
        /// <param name="type"></param>
        /// <param name="page">ҳ��</param>
        /// <param name="rows">����</param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> Qcs00001GetList(string type, int page, int rows, ref int count)
        {
            string select = string.Format(@"select A.CheckTestSettingID,A.InspectionStandard,A.InspectionLevel,A.InspectionMethod,
            A.Status,A.AQL,A.Comments,
            B.UserName as Creator,A.CreateLocalTime as CreateTime,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else C.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ");

            string sql = string.Format(@"  from [QCS_CheckTestSetting] A 
            left join [SYS_MESUsers] B on A.[Creator] = B.[MESUserID]
            left join [SYS_MESUsers] C on A.[Modifier] = C.[MESUserID]      
            where A.[SystemID]='{0}' and A.[Status] <> '{0}0201213000003' ", Framework.SystemID);

            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@type",SqlDbType.VarChar)
            };
            parameters[0].Value = DBNull.Value;

            SqlParameter[] Parcount = new SqlParameter[]{
                new SqlParameter("@type",SqlDbType.VarChar),
            };
            Parcount[0].Value = DBNull.Value;

            if (!string.IsNullOrWhiteSpace(type))
            {
                sql = sql + string.Format(@" and A.[InspectionMethod] = @type ");
                parameters[0].Value = type;
                Parcount[0].Value = type;
            }
            count = UniversalService.getCount(sql, Parcount);

            String orderby = " A.[Status],A.[CreateTime] desc ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, parameters, page, rows);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// ��������
        /// SAM 2017��6��5��12:00:51
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static DataTable Qcs00001Export(string type)
        {
            string select = string.Format(@"select ROW_NUMBER() OVER (ORDER BY A.[Status],A.[CreateTime] desc,H.[Sequence]),
            A.[InspectionStandard],D.Name as InspectionLevel,E.Name as InspectionMethod,F.Name as AQL,A.Comments,G.Name as Status,H.Sequence,
            H.StartBatch,H.EndBatch,H.SamplingQuantity,H.AcQuantity,H.ReQuantity ");

            string sql = string.Format(@"  from [QCS_CheckTestSetting] A 
            left join [SYS_Parameters] D on A.[InspectionLevel] = D.[ParameterID]
            left join [SYS_Parameters] E on A.[InspectionMethod] = E.[ParameterID]
            left join [SYS_Parameters] F on A.[AQL] = F.[ParameterID]
            left join [SYS_Parameters] G on A.[Status] = G.[ParameterID]
            left join [QCS_CheckTestSettingDetails] H on A.[CheckTestSettingID] = H.[CheckTestSettingID]
            where A.[SystemID]='{0}' and A.[Status] <> '{0}0201213000003' ", Framework.SystemID);

            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@type",SqlDbType.VarChar)
            };
            parameters[0].Value = DBNull.Value;

            if (!string.IsNullOrWhiteSpace(type))
            {
                sql = sql + string.Format(@" and A.[InspectionMethod] = @type ");
                parameters[0].Value = type;
            }

            string orderBy = "order By A.[Status],A.[CreateTime] desc,H.[Sequence]  ";

            return SQLHelper.ExecuteDataTable(select + sql + orderBy, CommandType.Text, parameters);
        }

        /// <summary>
        /// ����Ʒ����ˮƽ+���鷽ʽ+AQLֵ�ж���������趨��
        /// Joint 2017��8��7��17:02:20
        /// </summary>
        /// <param name="InspectionLevel"></param>
        /// <param name="InspectionMethod"></param>
        /// <param name="AQL"></param>
        /// <returns></returns>
        public static QCS_CheckTestSetting getByAql(string InspectionLevelID, string InspectionMethodID, string AQL)
        {
            string sql = string.Format(@"select Top 1 * from [QCS_CheckTestSetting] where InspectionLevel='{0}' and InspectionMethod='{1}' and AQL='{2}'  and [SystemID] = '{3}' ", InspectionLevelID, InspectionMethodID, AQL, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return null;
            else
                return ToEntity(dt.Rows[0]);
        }

    }

}

