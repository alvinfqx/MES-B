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
    public class SYS_WorkCenterResourcesService : SuperModel<SYS_WorkCenterResources>
    {
        /// <summary>
        /// ����
        /// SAM 2017��5��24��17:38:35
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="Model"></param>
        /// <returns></returns>
        public static bool insert(string userId, SYS_WorkCenterResources Model)
        {
            try
            {
                string sql = string.Format(@"insert[SYS_WorkCenterResources]([WorkCenterResourcesID],[WorkCenterID],[ProcessID],[ResourceID],[IfMain],[Status],
        [Comments],[OperationID],[Modifier],[ModifiedTime],[ModifiedLocalTime],[Creator],[CreateTime],[CreateLocalTime],[SystemID]) values
         (@WorkCenterResourcesID,@WorkCenterID,@ProcessID,@ResourceID,@IfMain,@Status,@Comments,@OperationID,'{0}','{1}','{2}','{0}','{1}','{2}','{3}')", userId, DateTime.Now, DateTime.Now, Framework.SystemID);

                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@WorkCenterResourcesID",SqlDbType.VarChar),
                    new SqlParameter("@WorkCenterID",SqlDbType.VarChar),
                    new SqlParameter("@ProcessID",SqlDbType.VarChar),
                    new SqlParameter("@ResourceID",SqlDbType.VarChar),
                    new SqlParameter("@IfMain",SqlDbType.VarChar),
                    new SqlParameter("@Status",SqlDbType.VarChar),
                    new SqlParameter("@Comments",SqlDbType.NVarChar),
                    new SqlParameter("@OperationID",SqlDbType.NVarChar),
                    };

                parameters[0].Value = (Object)Model.WorkCenterResourcesID ?? DBNull.Value;
                parameters[1].Value = (Object)Model.WorkCenterID ?? DBNull.Value;
                parameters[2].Value = (Object)Model.ProcessID ?? DBNull.Value;
                parameters[3].Value = (Object)Model.ResourceID ?? DBNull.Value;
                parameters[4].Value = (Object)Model.IfMain ?? DBNull.Value;
                parameters[5].Value = (Object)Model.Status ?? DBNull.Value;
                parameters[6].Value = (Object)Model.Comments ?? DBNull.Value;
                parameters[7].Value = (Object)Model.OperationID ?? DBNull.Value;

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
        /// SAM 2017��5��24��17:38:45
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="Model"></param>
        /// <returns></returns>
        public static bool update(string userId, SYS_WorkCenterResources Model)
        {
            try
            {
                string sql = String.Format(@"update[SYS_WorkCenterResources] set {0},[ProcessID]=@ProcessID,
                [ResourceID]=@ResourceID,[IfMain]=@IfMain,[Status]=@Status,
                [Comments]=@Comments where [WorkCenterResourcesID]=@WorkCenterResourcesID", UniversalService.getUpdateUTC(userId));
                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@WorkCenterResourcesID",SqlDbType.VarChar),
                    new SqlParameter("@WorkCenterID",SqlDbType.VarChar),
                    new SqlParameter("@ProcessID",SqlDbType.VarChar),
                    new SqlParameter("@ResourceID",SqlDbType.VarChar),
                    new SqlParameter("@IfMain",SqlDbType.VarChar),
                    new SqlParameter("@Status",SqlDbType.VarChar),
                    new SqlParameter("@Comments",SqlDbType.NVarChar),
                    };

                parameters[0].Value = Model.WorkCenterResourcesID;
                parameters[1].Value = (Object)Model.WorkCenterID ?? DBNull.Value;
                parameters[2].Value = (Object)Model.ProcessID ?? DBNull.Value;
                parameters[3].Value = (Object)Model.ResourceID ?? DBNull.Value;
                parameters[4].Value = (Object)Model.IfMain ?? DBNull.Value;
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
        /// ��ȡ��һʵ��
        /// SAM 2017��5��24��17:38:52
        /// </summary>
        /// <param name="WorkCenterResourcesID"></param>
        /// <returns></returns>
        public static SYS_WorkCenterResources get(string WorkCenterResourcesID)
        {
            string sql = string.Format(@"select Top 1 * from [SYS_WorkCenterResources] where [WorkCenterResourcesID] = '{0}'  and [SystemID] = '{1}' ", WorkCenterResourcesID, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return null;
            else
                return ToEntity(dt.Rows[0]);
        }


        /// <summary>
        /// �ж�Ψһ��
        /// SAM 2017��5��24��17:37:18
        /// </summary>
        /// <param name="ProcessID"></param>
        /// <param name="WorkCenterID"></param>
        /// <param name="WorkCenterProcessID"></param>
        /// <returns></returns>
        public static bool Check(string ResourceID, string WorkCenterID, string WorkCenterResourcesID)
        {
            string sql = String.Format(@"select Top 1 * from [SYS_WorkCenterResources] where [SystemID]='{0}' and [Status] <> '{0}0201213000003' and [WorkCenterID] = '{1}' ", Framework.SystemID, WorkCenterID);

            /*�ȶ���Code��Ĭ�ϸ�DbNull,��������Ҳ���*/
            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@ResourceID",SqlDbType.VarChar),
            };
            parameters[0].Value = DBNull.Value;

            /*��ΪResourcesID��ͨ���ֶ�����ģ�������Ҫ�ò�������ʽȥƴSQL*/
            if (!string.IsNullOrWhiteSpace(ResourceID))
            {
                sql = sql + String.Format(@" and [ResourceID] =@ResourceID ");
                parameters[0].Value = ResourceID;
            }

            /*WorkCenterProcessID��������ˮ�ţ������ڸ���ʱ���ų����Լ�*/
            if (!string.IsNullOrWhiteSpace(WorkCenterResourcesID))
                sql = sql + String.Format(@" and [WorkCenterResourcesID] <> '{0}' ", WorkCenterResourcesID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text, parameters);

            if (dt.Rows.Count == 0)
                return false;
            else
                return true;
        }

        /// <summary>
        /// ���ݹ������Ļ�ȡ��Դ�б�
        /// SAM 2017��5��25��09:31:35
        /// </summary>
        /// <param name="WorkCenterID"></param>
        /// <param name="page">ҳ��</param>
        /// <param name="rows">����</param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> Inf00018WorkCenterGetResourcesList(string WorkCenterID, int page, int rows, ref int count)
        {
            string select = string.Format(@"select A.WorkCenterResourcesID,A.ResourceID,A.WorkCenterID,A.Status,
            D.Code as ResourceCode,D.Description as ResourceName,D.Comments,
            B.UserName as Creator,A.CreateLocalTime as CreateTime,
            (Select [Code]+'-'+[Name] from [SYS_Parameters] where ParameterID = D.[ClassID]) as Class,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else C.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ");

            string sql = string.Format(@" from [SYS_WorkCenterResources] A 
            left join [SYS_MESUsers] B on A.Creator = B.MESUserID
            left join [SYS_MESUsers] C on A.Modifier = C.MESUserID
            left join [SYS_Resources] D on A.ResourceID = D.ResourceID
            where A.[SystemID]='{0}' and A.Status <> '{0}0201213000003' and A.[WorkCenterID]='{1}' ", Framework.SystemID, WorkCenterID);

            count = UniversalService.getCount(sql, null);

            string orderby = "A.[Status],D.[Code] ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, null, page, rows);

            return ToHashtableList(dt);
        }



        /// <summary>
        /// �жϹ��������Ƿ�����������Դ��ϸ
        /// SAM 2017��5��28��14:27:13
        /// </summary>
        /// <param name="GroupID"></param>
        /// <returns></returns>
        public static bool CheckWorkCenter(string WorkCenterID)
        {
            string sql = string.Format(@"select * from [SYS_WorkCenterResources] where [WorkCenterID] = '{0}' and [SystemID] = '{1}' and [Status] = '{1}0201213000001'", WorkCenterID, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return false;
            else
                return true;
        }

        /// <summary>
        /// �ж���Դ�Ƿ���ڹ������ĵ��趨��
        /// SAM 2017��6��7��15:04:22
        /// </summary>
        /// <param name="ResourceID"></param>
        /// <returns></returns>
        public static bool CheckResource(string ResourceID)
        {
            string sql = string.Format(@"select * from [SYS_WorkCenterResources] where [ResourceID] = '{0}' and [SystemID] = '{1}' and [Status] = '{1}0201213000001'", ResourceID, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return false;
            else
                return true;
        }

        /// <summary>
        /// ɾ����������ʱһ��ɾ���Ƴ̹������ı������
        /// Joint 2017��7��28��14:54:35
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="WorkCenterID"></param>
        /// <returns></returns>
        public static bool DeleteWorkCenter(string userid, string WorkCenterID)
        {
            try
            {
                string sql = string.Format(@"update[SYS_WorkCenterResources] set {0},
               [Status]='{1}0201213000003' where [WorkCenterID]='{2}' ", UniversalService.getUpdateUTC(userid), Framework.SystemID, WorkCenterID);
                return SQLHelper.ExecuteNonQuery(sql, CommandType.Text) > 0;
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return false;
            }
        }

        /// <summary>
        /// �ж��Ƴ��Ƿ񱻹���������Դʹ��
        /// Joint 2017��7��28��18:11:23
        /// </summary>
        /// <param name="WorkCenterID"></param>
        /// <param name="ProcessID"></param>
        /// <returns></returns>
        public static List<SYS_WorkCenterResources> CheckProcess(string WorkCenterID, string ProcessID)
        {
            string sql = string.Format(@"select * from [SYS_WorkCenterResources] where [WorkCenterID]='{0}' and [ProcessID] = '{1}' and [SystemID] = '{2}' and [Status] = '{2}0201213000001'", WorkCenterID, ProcessID, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return null;
            else
                return ToList(dt);
        }

        /// <summary>
        /// ���ݹ������Ļ�ȡ��Դ�б�����ҳ��
        /// Joint 2017��7��31��11:53:50
        /// </summary>
        /// <param name="WorkCenterID"></param>
        /// <returns></returns>
        public static IList<Hashtable> Inf00018GetWorkCenterResourceList(string WorkCenterID)
        {
            string select = string.Format(@"select distinct A.WorkCenterResourcesID,A.WorkCenterID,A.ResourceID,A.ProcessID,A.OperationID,A.Comments,
            (case when A.IfMain='false' then '0' else '1' end) as IfMain,(select [Name] from [SYS_Parameters] where ParameterID=A.OperationID) as OperationName,
            D.Code as Code,D.Description as Name,D.[Status],

            (select [Name] from [SYS_Parameters] where [ParameterID]=E.[OperationID] )as OperationName,

            (select [Name] from [SYS_Parameters] where D.[ClassID] = [ParameterID]) as ClassName,
            B.Emplno+'-'+B.UserName as Creator,A.CreateLocalTime as CreateTime,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else C.Emplno+'-'+C.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ");

            string sql = string.Format(@"  from [SYS_WorkCenterResources] A 
            left join [SYS_MESUsers] B on A.[Creator] = B.[MESUserID]
            left join [SYS_MESUsers] C on A.[Modifier] = C.[MESUserID]
            left join [SYS_Resources] D on A.[ResourceID] = D.[ResourceID]
            left join [SYS_ProcessOperation] E on A.[OperationID]=E.[OperationID]
            where A.[SystemID]='{0}' and A.[Status] = '{0}0201213000001' and [WorkCenterID] = '{1}'", Framework.SystemID, WorkCenterID);


            String orderby = "order by D.[Code] ";

            DataTable dt = SQLHelper.ExecuteDataTable(select + sql + orderby, CommandType.Text);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// ɾ�����ڷ�Χ�ڵĹ������������Դ
        /// Joint 2017��7��31��15:21:50
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="WorkCenterResourceIDs"></param>
        /// <param name="WorkCenterID"></param>
        /// <returns></returns>
        public static bool Delete(string userId, string WorkCenterResourceIDs, string WorkCenterID)
        {
            try
            {
                string sql = string.Format(@"update[SYS_WorkCenterResources] set {0},
               [Status]='{1}0201213000003' where [WorkCenterID]='{2}' ", UniversalService.getUpdateUTC(userId), Framework.SystemID, WorkCenterID);

                if (!string.IsNullOrWhiteSpace(WorkCenterResourceIDs))
                    sql += string.Format(@" and [WorkCenterResourceID] not in ('{0}')", WorkCenterResourceIDs);

                return SQLHelper.ExecuteNonQuery(sql, CommandType.Text) > 0;
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return false;
            }
        }
    }
}

