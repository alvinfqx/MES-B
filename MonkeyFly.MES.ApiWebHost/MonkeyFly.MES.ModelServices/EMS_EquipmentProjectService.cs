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
    public class EMS_EquipmentProjectService : SuperModel<EMS_EquipmentProject>
    {
        /// <summary>
        /// 新增
        /// SAM 2017年5月22日11:49:57
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="Model"></param>
        /// <returns></returns>
        public static bool insert(string userId, EMS_EquipmentProject Model)
        {
            try
            {
                string sql = string.Format(@"insert[EMS_EquipmentProject]([EquipmentProjectID],[EquipmentID],[ProjectID],[IfCollection],[CollectionWay],
                [SensorID],[StandardValue],[MaxValue],[MinValue],[MaxAlarmTime],[MinAlarmTime],[Status],[MaxAlarmValue],[MinAlarmValue],
                [Comments],[Modifier],[ModifiedTime],[ModifiedLocalTime],[Creator],[CreateTime],[CreateLocalTime],[SystemID]) values
                 (@EquipmentProjectID,@EquipmentID,@ProjectID,@IfCollection,@CollectionWay,
                 @SensorID,@StandardValue,@MaxValue,@MinValue,@MaxAlarmTime,@MinAlarmTime,
                @Status,@MaxAlarmValue,@MinAlarmValue,@Comments,'{0}','{1}','{2}','{0}','{1}','{2}','{3}')", userId, DateTime.Now, DateTime.Now, Framework.SystemID);

                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@EquipmentProjectID",SqlDbType.VarChar),
                    new SqlParameter("@EquipmentID",SqlDbType.VarChar),
                    new SqlParameter("@ProjectID",SqlDbType.VarChar),
                    new SqlParameter("@IfCollection",SqlDbType.Bit),
                    new SqlParameter("@CollectionWay",SqlDbType.VarChar),
                    new SqlParameter("@SensorID",SqlDbType.VarChar),
                    new SqlParameter("@StandardValue",SqlDbType.NVarChar),
                    new SqlParameter("@MaxValue",SqlDbType.NVarChar),
                    new SqlParameter("@MinValue",SqlDbType.NVarChar),
                    new SqlParameter("@MaxAlarmTime",SqlDbType.Int),
                    new SqlParameter("@MinAlarmTime",SqlDbType.Int),
                    new SqlParameter("@Comments",SqlDbType.VarChar),
                    new SqlParameter("@Status",SqlDbType.VarChar),
                    new SqlParameter("@MaxAlarmValue",SqlDbType.NVarChar),
                    new SqlParameter("@MinAlarmValue",SqlDbType.NVarChar)
                    };

                parameters[0].Value = (Object)Model.EquipmentProjectID ?? DBNull.Value;
                parameters[1].Value = (Object)Model.EquipmentID ?? DBNull.Value;
                parameters[2].Value = (Object)Model.ProjectID ?? DBNull.Value;
                parameters[3].Value = (Object)Model.IfCollection ?? DBNull.Value;
                parameters[4].Value = (Object)Model.CollectionWay ?? DBNull.Value;
                parameters[5].Value = (Object)Model.SensorID ?? DBNull.Value;
                parameters[6].Value = (Object)Model.StandardValue ?? DBNull.Value;
                parameters[7].Value = (Object)Model.MaxValue ?? DBNull.Value;
                parameters[8].Value = (Object)Model.MinValue ?? DBNull.Value;
                parameters[9].Value = (Object)Model.MaxAlarmTime ?? DBNull.Value;
                parameters[10].Value = (Object)Model.MinAlarmTime ?? DBNull.Value;
                parameters[11].Value = (Object)Model.Comments ?? DBNull.Value;
                parameters[12].Value = (Object)Model.Status ?? DBNull.Value;
                parameters[13].Value = (Object)Model.MaxAlarmValue ?? DBNull.Value;
                parameters[14].Value = (Object)Model.MinAlarmValue ?? DBNull.Value;

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
        /// SAM 2017年5月22日11:50:03
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="Model"></param>
        /// <returns></returns>
        public static bool update(string userId, EMS_EquipmentProject Model)
        {
            try
            {
                string sql = String.Format(@"update[EMS_EquipmentProject] set {0},
                [ProjectID]=@ProjectID,[IfCollection]=@IfCollection,[CollectionWay]=@CollectionWay,[Status]=@Status,
                [SensorID]=@SensorID,[StandardValue]=@StandardValue,[MaxValue]=@MaxValue,[MinValue]=@MinValue,[MaxAlarmValue]=@MaxAlarmValue,[MinAlarmValue]=@MinAlarmValue,
                [MaxAlarmTime]=@MaxAlarmTime,[MinAlarmTime]=@MinAlarmTime,[Comments]=@Comments where [EquipmentProjectID]=@EquipmentProjectID", UniversalService.getUpdateUTC(userId));
                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@EquipmentProjectID",SqlDbType.VarChar),
                    new SqlParameter("@ProjectID",SqlDbType.VarChar),
                    new SqlParameter("@IfCollection",SqlDbType.Bit),
                    new SqlParameter("@CollectionWay",SqlDbType.VarChar),
                    new SqlParameter("@SensorID",SqlDbType.VarChar),
                    new SqlParameter("@StandardValue",SqlDbType.NVarChar),
                    new SqlParameter("@MaxValue",SqlDbType.NVarChar),
                    new SqlParameter("@MinValue",SqlDbType.NVarChar),
                    new SqlParameter("@MaxAlarmTime",SqlDbType.Int),
                    new SqlParameter("@MinAlarmTime",SqlDbType.Int),
                    new SqlParameter("@Comments",SqlDbType.VarChar),
                    new SqlParameter("@Status",SqlDbType.VarChar),
                    new SqlParameter("@MaxAlarmValue",SqlDbType.NVarChar),
                    new SqlParameter("@MinAlarmValue",SqlDbType.NVarChar)
                    };

                parameters[0].Value = Model.EquipmentProjectID;
                parameters[1].Value = (Object)Model.ProjectID ?? DBNull.Value;
                parameters[2].Value = (Object)Model.IfCollection ?? DBNull.Value;
                parameters[3].Value = (Object)Model.CollectionWay ?? DBNull.Value;
                parameters[4].Value = (Object)Model.SensorID ?? DBNull.Value;
                parameters[5].Value = (Object)Model.StandardValue ?? DBNull.Value;
                parameters[6].Value = (Object)Model.MaxValue ?? DBNull.Value;
                parameters[7].Value = (Object)Model.MinValue ?? DBNull.Value;
                parameters[8].Value = (Object)Model.MaxAlarmTime ?? DBNull.Value;
                parameters[9].Value = (Object)Model.MinAlarmTime ?? DBNull.Value;
                parameters[10].Value = (Object)Model.Comments ?? DBNull.Value;
                parameters[11].Value = (Object)Model.Status ?? DBNull.Value;
                parameters[12].Value = (Object)Model.MaxAlarmValue ?? DBNull.Value;
                parameters[13].Value = (Object)Model.MinAlarmValue ?? DBNull.Value;

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
        /// SAM 2017年5月22日11:50:12
        /// </summary>
        /// <param name="EquipmentProjectID"></param>
        /// <returns></returns>
        public static EMS_EquipmentProject get(string EquipmentProjectID)
        {
            string sql = string.Format(@"select Top 1 * from [EMS_EquipmentProject] where [EquipmentProjectID] = '{0}'  and [SystemID] = '{1}' ", EquipmentProjectID, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return null;
            else
                return ToEntity(dt.Rows[0]);
        }

        /// <summary>
        /// 判断重复
        /// SAM 2017年5月22日23:06:25
        /// </summary>
        /// <param name="SensorID"></param>
        /// <param name="EquipmentID"></param>
        /// <param name="ProjectID"></param>
        /// <param name="EquipmentProjectID"></param>
        /// <returns></returns>
        public static bool Check(string SensorID, string EquipmentID,string ProjectID, string EquipmentProjectID)
        {
            string sql = String.Format(@"select Top 1 * from [EMS_EquipmentProject] where [SystemID]='{0}' and [Status] <> '{0}0201213000003' ", Framework.SystemID);

            if (!string.IsNullOrWhiteSpace(EquipmentID))
                sql = sql + string.Format(@" and [EquipmentID]= '{0}' ", EquipmentID);

            if (!string.IsNullOrWhiteSpace(ProjectID))
                sql = sql + string.Format(@" and [ProjectID] = '{0}' ", ProjectID);

            if (!string.IsNullOrWhiteSpace(SensorID))
                sql = sql + string.Format(@" and [SensorID] = '{0}' ", SensorID);

            /*EquipmentProjectID（参数流水号）用于在更新时，排除他自己*/
            if (!string.IsNullOrWhiteSpace(EquipmentProjectID))
                sql = sql + string.Format(@" and [EquipmentProjectID] <> '{0}' ", EquipmentProjectID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text, null);

            if (dt.Rows.Count == 0)
                return false;
            else
                return true;
        }


        /// <summary>
        /// 获取一个设备对应的设备项目列表
        /// SAM 2017年5月22日22:37:59
        /// </summary>
        /// <param name="EquipmentID"></param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> Ems00001GetProjectList(string EquipmentID, int page, int rows, ref int count)
        {
            string select = string.Format(@"select A.EquipmentProjectID,A.EquipmentID,A.ProjectID,
            D.Code as ProjectCode,D.Description as ProjectDescription,
            (Select [Code] from [SYS_Parameters] where ParameterID = D.[Attribute]) as AttributeCode,
            (Select [Name] from [SYS_Parameters] where ParameterID = D.[Attribute]) as AttributeName,
            A.IfCollection,A.CollectionWay,A.SensorID,A.StandardValue,A.MaxValue,A.MinValue,A.MaxAlarmTime,A.MinAlarmTime,
            A.Comments,G.Code as SensorCode,G.Name as SensorName,A.MaxAlarmValue,A.MinAlarmValue,
            B.UserName as Creator,A.CreateLocalTime as CreateTime,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else C.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ");

            string sql = string.Format(@"  from [EMS_EquipmentProject] A 
            left join [SYS_MESUsers] B on A.[Creator] = B.[MESUserID]
            left join [SYS_MESUsers] C on A.[Modifier] = C.[MESUserID]
            left join [SYS_Projects] D on A.[ProjectID] = D.[ProjectID]
            left join [IOT_Sensor] G on A.[SensorID] = G.[SensorID]
            where A.[SystemID]='{0}' and A.[Status] <> '{0}0201213000003' and A.[EquipmentID] = '{1}' ", Framework.SystemID, EquipmentID);

          
            count = UniversalService.getCount(sql, null);

            String orderby = " D.[Code] ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, null, page, rows);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// 设备项目的导出
        /// SAM 2017年5月23日14:45:44
        /// </summary>
        /// <param name="EquipmentID"></param>
        /// <returns></returns>
        public static DataTable Ems00001ProjectExport(string EquipmentID)
        {
            string select = string.Format(@"select ROW_NUMBER() OVER (ORDER BY D.[Code]),
            E.Code as EquipmentCode,E.Name as EquipmentName,E.Comments as EquipmentComments,
            D.Code as ProjectCode,D.Description as ProjectDescription,           
            (Select [Name] from [SYS_Parameters] where ParameterID = D.[Attribute]) as AttributeName,
            (CASE WHEN A.IfCollection=1 THEN '是' ELSE '否' END),F.Name as CollectionWay,
            G.Code as SensorCode,G.Name as SensorName,A.StandardValue,A.MaxValue,A.MinValue,A.MaxAlarmTime,A.MinAlarmTime,
            A.MaxAlarmValue,A.MinAlarmValue,A.Comments,     
            B.UserName as Creator,A.CreateLocalTime as CreateTime,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else C.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ");

            string sql = string.Format(@"  from [EMS_EquipmentProject] A 
            left join [SYS_MESUsers] B on A.[Creator] = B.[MESUserID]
            left join [SYS_MESUsers] C on A.[Modifier] = C.[MESUserID]
            left join [SYS_Projects] D on A.[ProjectID] = D.[ProjectID]
            left join [EMS_Equipment] E on A.[EquipmentID] = E.[EquipmentID]
            left join [SYS_Parameters] F on A.[CollectionWay] = F.[ParameterID]
            left join [IOT_Sensor]  G on A.SensorID = G.SensorID
            where A.[SystemID]='{0}' and A.[Status] <> '{0}0201213000003'  and A.[EquipmentID] = '{1}' ", Framework.SystemID, EquipmentID);

            string orderBy = "order By D.[Code] ";

            return SQLHelper.ExecuteDataTable(select + sql + orderBy, CommandType.Text, null);
        }

        /// <summary>
        /// 判断设备项目中是否有感知器的设定
        /// SAM 2017年5月23日14:46:37
        /// </summary>
        /// <param name="SensorID"></param>
        /// <returns></returns>
        public static bool CheckSensor(string SensorID)
        {
            string sql = string.Format(@"select * from [EMS_EquipmentProject] where  [SensorID] = '{0}' and [SystemID] = '{1}' and [Status] <> '{1}0201213000003' ", SensorID, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return false;
            else
                return true;
        }


        /// <summary>
        /// 根据设备获取他的项目列表
        /// SAM 2017年5月23日16:05:48
        /// </summary>
        /// <param name="EquipmentID"></param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> EquipmentProjectList(string EquipmentID, string Code,int page, int rows, ref int count)
        {
            string select = string.Format(@"select A.EquipmentProjectID,A.EquipmentID,A.ProjectID,
            D.Code as ProjectCode,D.Description as ProjectDescription,D.Attribute,
            E.Code as EquipmentCode,E.Name as EquipmentName,F.Name as Status,
            (Select [Name] from [SYS_Parameters] where ParameterID = D.[Attribute]) as AttributeName,
            A.IfCollection,A.CollectionWay,A.SensorID,A.StandardValue,A.MaxValue,A.MinValue,A.MaxAlarmTime,A.MinAlarmTime,
            A.Comments,     
            B.UserName as Creator,A.CreateLocalTime as CreateTime,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else C.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ");

            string sql = string.Format(@"  from [EMS_EquipmentProject] A 
            left join [SYS_MESUsers] B on A.[Creator] = B.[MESUserID]
            left join [SYS_MESUsers] C on A.[Modifier] = C.[MESUserID]
            left join [SYS_Projects] D on A.[ProjectID] = D.[ProjectID]
            left join [EMS_Equipment] E on A.[EquipmentID] = E.[EquipmentID]
            left join [SYS_Parameters] F on A.[Status] = F.[ParameterID]
            where A.[SystemID]='{0}' and A.[Status] = '{0}0201213000001' and A.[EquipmentID] = '{1}' ", Framework.SystemID, EquipmentID);

            if (!string.IsNullOrWhiteSpace(Code))
                sql += string.Format(@" and D.Code like '%{0}%'",Code);

            count = UniversalService.getCount(sql, null);

            string orderby = " D.[Code] ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, null, page, rows);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// 判断项目设定中，是否存在项目的设定
        /// SAM 2017年7月24日10:08:31
        /// </summary>
        /// <param name="ProjectID"></param>
        /// <returns></returns>
        public static bool CheckProject(string ProjectID)
        {
            string sql = string.Format(@"select * from [EMS_EquipmentProject] where  [ProjectID] = '{0}' and [SystemID] = '{1}' and [Status] <> '{1}0201213000003' ", ProjectID, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return false;
            else
                return true;
        }

        /// <summary>
        /// 判断指定设备是否已存在设备项目设定
        /// SAM 2017年8月3日11:58:36
        /// </summary>
        /// <param name="EquipmentID"></param>
        /// <returns></returns>
        public static bool CheckEquipment(string EquipmentID)
        {
            string sql = string.Format(@"select * from [EMS_EquipmentProject] where  [EquipmentID] = '{0}' and [SystemID] = '{1}' and [Status] <> '{1}0201213000003' ", EquipmentID, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return false;
            else
                return true;
        }
    }
}

