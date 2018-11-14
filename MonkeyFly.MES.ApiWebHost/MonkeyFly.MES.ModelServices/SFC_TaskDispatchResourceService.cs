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
    public class SFC_TaskDispatchResourceService : SuperModel<SFC_TaskDispatchResource>
    {
        /// <summary>
        /// 新增
        /// SAM 2017年7月3日14:42:43
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="Model"></param>
        /// <returns></returns>
        public static bool insert(string userId, SFC_TaskDispatchResource Model)
        {
            try
            {
                string sql = string.Format(@"insert[SFC_TaskDispatchResource]([TaskDispatchResourceID],[FabMoProcessID],
                [FabMoOperationID],[TaskDispatchID],[Sequence],
                [ExpectedDate],[ExpectedTime],[ActualDate],[ActualTime],[Type],
                [ResourceID],[EquipmentID],[IfMain],[ResourceClassID],[Status],
                [Comments],[Modifier],[ModifiedTime],[ModifiedLocalTime],[Creator],[CreateTime],[CreateLocalTime],[SystemID]) values
                 (@TaskDispatchResourceID,@FabMoProcessID,
                @FabMoOperationID,@TaskDispatchID,@Sequence,
                @ExpectedDate,@ExpectedTime,@ActualDate,
                @ActualTime,@Type,@ResourceID,
                @EquipmentID,@IfMain,@ResourceClassID,
                @Status,@Comments,'{0}','{1}','{2}','{0}','{1}','{2}','{3}')", userId, DateTime.Now, DateTime.Now, Framework.SystemID);

                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@TaskDispatchResourceID",SqlDbType.VarChar),
                    new SqlParameter("@FabMoProcessID",SqlDbType.VarChar),
                    new SqlParameter("@FabMoOperationID",SqlDbType.VarChar),
                    new SqlParameter("@TaskDispatchID",SqlDbType.VarChar),
                    new SqlParameter("@Sequence",SqlDbType.VarChar),
                    new SqlParameter("@ExpectedDate",SqlDbType.DateTime),
                    new SqlParameter("@ExpectedTime",SqlDbType.DateTime),
                    new SqlParameter("@ActualDate",SqlDbType.DateTime),
                    new SqlParameter("@ActualTime",SqlDbType.DateTime),
                    new SqlParameter("@Type",SqlDbType.VarChar),
                    new SqlParameter("@ResourceID",SqlDbType.VarChar),
                    new SqlParameter("@EquipmentID",SqlDbType.VarChar),
                    new SqlParameter("@IfMain",SqlDbType.Bit),
                    new SqlParameter("@ResourceClassID",SqlDbType.VarChar),
                    new SqlParameter("@Status",SqlDbType.VarChar),
                    new SqlParameter("@Comments",SqlDbType.NVarChar),
                    };

                parameters[0].Value = (Object)Model.TaskDispatchResourceID ?? DBNull.Value;
                parameters[1].Value = (Object)Model.FabMoProcessID ?? DBNull.Value;
                parameters[2].Value = (Object)Model.FabMoOperationID ?? DBNull.Value;
                parameters[3].Value = (Object)Model.TaskDispatchID ?? DBNull.Value;
                parameters[4].Value = (Object)Model.Sequence ?? DBNull.Value;
                parameters[5].Value = (Object)Model.ExpectedDate ?? DBNull.Value;
                parameters[6].Value = (Object)Model.ExpectedTime ?? DBNull.Value;
                parameters[7].Value = (Object)Model.ActualDate ?? DBNull.Value;
                parameters[8].Value = (Object)Model.ActualTime ?? DBNull.Value;
                parameters[9].Value = (Object)Model.Type ?? DBNull.Value;
                parameters[10].Value = (Object)Model.ResourceID ?? DBNull.Value;
                parameters[11].Value = (Object)Model.EquipmentID ?? DBNull.Value;
                parameters[12].Value = (Object)Model.IfMain ?? DBNull.Value;
                parameters[13].Value = (Object)Model.ResourceClassID ?? DBNull.Value;
                parameters[14].Value = (Object)Model.Status ?? DBNull.Value;
                parameters[15].Value = (Object)Model.Comments ?? DBNull.Value;

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
        /// SAM 2017年7月3日14:42:48
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="Model"></param>
        /// <returns></returns>
        public static bool update(string userId, SFC_TaskDispatchResource Model)
        {
            try
            {
                string sql = String.Format(@"update[SFC_TaskDispatchResource] set {0},
            [Sequence]=@Sequence,[ExpectedDate]=@ExpectedDate,[ExpectedTime]=@ExpectedTime,[ActualDate]=@ActualDate,
            [ActualTime]=@ActualTime,[Type]=@Type,[ResourceID]=@ResourceID,[EquipmentID]=@EquipmentID,
            [IfMain]=@IfMain,[ResourceClassID]=@ResourceClassID,[Status]=@Status,[Comments]=@Comments 
            where [TaskDispatchResourceID]=@TaskDispatchResourceID", UniversalService.getUpdateUTC(userId));
                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@TaskDispatchResourceID",SqlDbType.VarChar),
                    new SqlParameter("@Sequence",SqlDbType.VarChar),
                    new SqlParameter("@ExpectedDate",SqlDbType.DateTime),
                    new SqlParameter("@ExpectedTime",SqlDbType.DateTime),
                    new SqlParameter("@ActualDate",SqlDbType.DateTime),
                    new SqlParameter("@ActualTime",SqlDbType.DateTime),
                    new SqlParameter("@Type",SqlDbType.VarChar),
                    new SqlParameter("@ResourceID",SqlDbType.VarChar),
                    new SqlParameter("@EquipmentID",SqlDbType.VarChar),
                    new SqlParameter("@IfMain",SqlDbType.Bit),
                    new SqlParameter("@ResourceClassID",SqlDbType.VarChar),
                    new SqlParameter("@Status",SqlDbType.VarChar),
                    new SqlParameter("@Comments",SqlDbType.NVarChar),
                    };

                parameters[0].Value = Model.TaskDispatchResourceID;
                parameters[1].Value = (Object)Model.Sequence ?? DBNull.Value;
                parameters[2].Value = (Object)Model.ExpectedDate ?? DBNull.Value;
                parameters[3].Value = (Object)Model.ExpectedTime ?? DBNull.Value;
                parameters[4].Value = (Object)Model.ActualDate ?? DBNull.Value;
                parameters[5].Value = (Object)Model.ActualTime ?? DBNull.Value;
                parameters[6].Value = (Object)Model.Type ?? DBNull.Value;
                parameters[7].Value = (Object)Model.ResourceID ?? DBNull.Value;
                parameters[8].Value = (Object)Model.EquipmentID ?? DBNull.Value;
                parameters[9].Value = (Object)Model.IfMain ?? DBNull.Value;
                parameters[10].Value = (Object)Model.ResourceClassID ?? DBNull.Value;
                parameters[11].Value = (Object)Model.Status ?? DBNull.Value;
                parameters[12].Value = (Object)Model.Comments ?? DBNull.Value;

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
        /// SAM 2017年7月3日14:42:05
        /// </summary>
        /// <param name="RCDispatchResourceID"></param>
        /// <returns></returns>
        public static SFC_TaskDispatchResource get(string RCDispatchResourceID)
        {
            string sql = string.Format(@"select Top 1 * from [SFC_TaskDispatchResource] where [TaskDispatchResourceID] = '{0}'  and [SystemID] = '{1}' ", RCDispatchResourceID, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return null;
            else
                return ToEntity(dt.Rows[0]);
        }

        /// <summary>
        /// 判断是否重复
        /// SAM 2017年7月20日09:41:26
        /// </summary>
        /// <param name="EquipmentID"></param>
        /// <param name="TaskDispatchID"></param>
        /// <param name="TaskDispatchResourceID"></param>
        /// <returns></returns>
        public static bool Check(string EquipmentID, string TaskDispatchID, string TaskDispatchResourceID)
        {
            string sql = String.Format(@"select Top 1 * from [SFC_TaskDispatchResource] where [SystemID]='{0}' and Status <> '{0}0201213000003'", Framework.SystemID);

            /*先定义EquipmentID，默认给DbNull,以免参数找不到*/
            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@EquipmentID",SqlDbType.VarChar)
            };
            parameters[0].Value = DBNull.Value;

            /*因为EquipmentID是通过手动输入的，所以需要用参数的形式去拼SQL*/
            if (!string.IsNullOrWhiteSpace(EquipmentID))
            {
                sql = sql + String.Format(@" and [EquipmentID] =@EquipmentID ");
                parameters[0].Value = EquipmentID;
            }

            if (!string.IsNullOrWhiteSpace(TaskDispatchID))
                sql = sql + String.Format(@" and [TaskDispatchID] = '{0}' ", TaskDispatchID);

            /*TaskDispatchResourceID用于在更新时，排除他自己*/
            if (!string.IsNullOrWhiteSpace(TaskDispatchResourceID))
                sql = sql + String.Format(@" and [TaskDispatchResourceID] <> '{0}' ", TaskDispatchResourceID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text, parameters);

            if (dt.Rows.Count == 0)
                return false;
            else
                return true;
        }



        /// <summary>
        /// 任务单资源明细列表（人工）
        /// SAM 2017年7月10日16:50:47
        /// </summary>
        /// <param name="TaskDispatchID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> Sfc00004GetLResourceList(string TaskDispatchID, int page, int rows, ref int count)
        {
            string select = string.Format(
              @"select A.TaskDispatchResourceID,A.TaskDispatchID,A.Sequence,
                A.ResourceID,A.EquipmentID,A.IfMain,A.ResourceClassID,A.Comments,
                D.Code as ClassCode,D.Name as ClassName,F.Status,
                E.Code as ResourceCode,E.Description as ResourceName,
                F.Emplno,F.UserName,
                B.Emplno+'-'+B.UserName as Creator,A.CreateLocalTime as CreateTime,
                (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else C.Emplno+'-'+C.UserName END) as Modifier,
               (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ", Framework.SystemID);

            string sql = string.Format(
                @"from [SFC_TaskDispatchResource] A               
                 left join [SYS_MESUsers] B on A.[Creator] = B.[MESUserID]
                 left join [SYS_MESUsers] C on A.[Modifier] =C.[MESUserID]
                 left join [SYS_Parameters] D on A.[ResourceClassID] = D.[ParameterID]
                 left join [SYS_Resources] E on A.[ResourceID] = E.[ResourceID]
                 left join [SYS_MESUsers] F on A.[EquipmentID] = F.[MESUserID]
                where A.[SystemID] = '{0}' and A.[Status] = '{0}0201213000001' 
                and A.[TaskDispatchID]='{1}' and A.[Type]='{0}0201213000085'", Framework.SystemID, TaskDispatchID);

            count = UniversalService.getCount(sql, null);

            string orderby = "A.[Status],A.[Sequence] ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, null, page, rows);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// 任务单资源明细列表（机器）
        /// SAM 2017年7月10日16:50:47
        /// </summary>
        /// <param name="TaskDispatchID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> Sfc00004GetMResourceList(string TaskDispatchID, int page, int rows, ref int count)
        {
            string select = string.Format(
              @"select A.TaskDispatchResourceID,A.TaskDispatchID,A.Sequence,
                A.ResourceID,A.EquipmentID,A.IfMain,A.ResourceClassID,A.Comments,A.Status,
                D.Code as ClassCode,D.Name as ClassName,
                E.Code as ResourceCode,E.Description as ResourceName,
                F.Code as EquipmentCode,F.Name as EquipmentName,
                (Select [Name] From [SYS_Parameters] where ParameterID =F.Condition) as Condition,
                B.Emplno+'-'+B.UserName as Creator,A.CreateLocalTime as CreateTime,
                (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else C.Emplno+'-'+C.UserName END) as Modifier,
               (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ", Framework.SystemID);

            string sql = string.Format(
                @"from [SFC_TaskDispatchResource] A               
                 left join [SYS_MESUsers] B on A.[Creator] = B.[MESUserID]
                 left join [SYS_MESUsers] C on A.[Modifier] =C.[MESUserID]
                 left join [SYS_Parameters] D on A.[ResourceClassID] = D.[ParameterID]
                 left join [SYS_Resources] E on A.[ResourceID] = E.[ResourceID]
                 left join [EMS_Equipment] F on A.[EquipmentID] = F.[EquipmentID]
                where A.[SystemID] = '{0}' and A.[Status] = '{0}0201213000001' 
                and A.[TaskDispatchID]='{1}' and A.[Type]='{0}0201213000084'", Framework.SystemID, TaskDispatchID);

            count = UniversalService.getCount(sql, null);

            string orderby = "A.[Status],A.[Sequence] ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, null, page, rows);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// 任务单资源明细列表（其他）
        /// SAM 2017年7月10日16:50:47
        /// </summary>
        /// <param name="TaskDispatchID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> Sfc00004GetResourceList(string TaskDispatchID, int page, int rows, ref int count)
        {
            string select = string.Format(
              @"select A.TaskDispatchResourceID,A.TaskDispatchID,A.Sequence,
                A.ResourceID,A.EquipmentID,A.IfMain,A.ResourceClassID,A.Comments,A.Status,
                D.Code as ClassCode,D.Name as ClassName,
                E.Code as ResourceCode,E.Description as ResourceName,
                F.Code as EquipmentCode,F.Name as EquipmentName,
                (Select [Name] From [SYS_Parameters] where ParameterID =F.Condition) as Condition,
                B.Emplno+'-'+B.UserName as Creator,A.CreateLocalTime as CreateTime,
                (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else C.Emplno+'-'+C.UserName END) as Modifier,
               (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ", Framework.SystemID);

            string sql = string.Format(
                @"from [SFC_TaskDispatchResource] A               
                 left join [SYS_MESUsers] B on A.[Creator] = B.[MESUserID]
                 left join [SYS_MESUsers] C on A.[Modifier] =C.[MESUserID]
                 left join [SYS_Parameters] D on A.[ResourceClassID] = D.[ParameterID]
                 left join [SYS_Resources] E on A.[ResourceID] = E.[ResourceID]
                 left join [EMS_Equipment] F on A.[EquipmentID] = F.[EquipmentID]
                where A.[SystemID] = '{0}' and A.[Status] = '{0}0201213000001' 
                and A.[TaskDispatchID]='{1}' and A.[Type]='{0}0201213000086'", Framework.SystemID, TaskDispatchID);

            count = UniversalService.getCount(sql, null);

            string orderby = "A.[Status],A.[Sequence] ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, null, page, rows);

            return ToHashtableList(dt);
        }


        /// <summary>
        /// 根据任务单号和类型获取明细(用于打印)
        /// SAM 2017年7月10日17:45:32
        /// </summary>
        /// <param name="TaskDispatchID"></param>
        /// <param name="Type"></param>
        /// <returns></returns>
        public static IList<Hashtable> Sfc00004GetResourceList(string TaskDispatchID, string Type)
        {
            string select = null;
            string sql = null;
            if (Type == Framework.SystemID + "0201213000085")
            {
                select = string.Format(@"select D.Code as ClassCode,F.Emplno as Code,F.UserName as Name,
                    (CASE WHEN A.IfMain=1 THEN '是' ELSE '否' END) as IfMain ");

                sql = string.Format(
                   @"from [SFC_TaskDispatchResource] A               
                 left join [SYS_Parameters] D on A.[ResourceClassID] = D.[ParameterID]
                 left join [SYS_Resources] E on A.[ResourceID] = E.[ResourceID]
                 left join [SYS_MESUsers] F on A.[EquipmentID] = F.[MESUserID]
                where A.[SystemID] = '{0}' and A.[Status] = '{0}0201213000001' 
                and A.[TaskDispatchID]='{1}' and A.[Type]='{0}0201213000085'", Framework.SystemID, TaskDispatchID);
            }
            else
            {
                select = string.Format(@"select D.Code as ClassCode,F.Code as Code,F.Name as Name,
                    (CASE WHEN A.IfMain=1 THEN '是' ELSE '否' END) as IfMain ");

                sql = string.Format(
                   @"from [SFC_TaskDispatchResource] A               
                 left join [SYS_Parameters] D on A.[ResourceClassID] = D.[ParameterID]
                 left join [SYS_Resources] E on A.[ResourceID] = E.[ResourceID]
                 left join [EMS_Equipment] F on A.[EquipmentID] = F.[EquipmentID]
                where A.[SystemID] = '{0}' and A.[Status] = '{0}0201213000001' 
                and A.[TaskDispatchID]='{1}' and A.[Type]='{2}' ", Framework.SystemID, TaskDispatchID, Type);
            }
            string orderby = " order by A.[Sequence] ";

            DataTable dt = SQLHelper.ExecuteDataTable(select + sql + orderby, CommandType.Text);

            if (dt.Rows.Count == 0)
                return null;

            return ToHashtableList(dt);
        }

        /// <summary>
        /// 根据资源类别获取资源
        /// Tom 2017年7月24日17:27:42 
        /// </summary>
        /// <param name="classID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> SfcGetResourceByClass(string TaskDispatchID, string classID, int page, int rows, ref int count)
        {
            string select = string.Format(
              @"select A.TaskDispatchResourceID,A.TaskDispatchID,A.Sequence,
                A.ResourceID,A.EquipmentID,A.IfMain,A.ResourceClassID,A.Comments,A.Status,
                D.Code as ClassCode,D.Name as ClassName,
                E.Code as ResourceCode,E.Description as ResourceName,
                F.Code as EquipmentCode,F.Name as EquipmentName,
                (Select [Name] From [SYS_Parameters] where ParameterID =F.Condition) as Condition,
                B.Emplno+'-'+B.UserName as Creator,A.CreateLocalTime as CreateTime,
                (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else C.Emplno+'-'+C.UserName END) as Modifier,
                (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ", Framework.SystemID);

            string sql = string.Format(
                @"from [SFC_TaskDispatchResource] A               
                 left join [SYS_MESUsers] B on A.[Creator] = B.[MESUserID]
                 left join [SYS_MESUsers] C on A.[Modifier] =C.[MESUserID]
                 left join [SYS_Parameters] D on A.[ResourceClassID] = D.[ParameterID]
                 left join [SYS_Resources] E on A.[ResourceID] = E.[ResourceID]
                 left join [EMS_Equipment] F on A.[EquipmentID] = F.[EquipmentID]
                where A.[SystemID] = '{0}' and A.[Status] = '{0}0201213000001' 
                and A.[TaskDispatchID]='{1}' and A.[ResourceClassID]='{2}'", Framework.SystemID, TaskDispatchID, classID);

            count = UniversalService.getCount(sql, null);

            string orderby = "A.[Status],A.[Sequence] ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, null, page, rows);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// 设备巡检维护任务单弹窗
        /// Tom 2017年7月28日17:40:38
        /// </summary>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> Ems0003GetTaskDispatchList(int page, int rows, ref int count)
        {
            string select = string.Format(
              @"select A.TaskDispatchResourceID,A.TaskDispatchID,A.Sequence,
                A.ResourceID,A.EquipmentID,A.IfMain,A.ResourceClassID,A.Comments,A.Status,
                D.Code as ClassCode,D.Name as ClassName,
                E.Code as ResourceCode,E.Description as ResourceName,
                F.Code as EquipmentCode,F.Name as EquipmentName,
                (Select [Name] From [SYS_Parameters] where ParameterID =F.Condition) as Condition,
                B.Emplno+'-'+B.UserName as Creator,A.CreateLocalTime as CreateTime,
                (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else C.Emplno+'-'+C.UserName END) as Modifier,
                (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ", Framework.SystemID);

            string sql = string.Format(
                @"from [SFC_TaskDispatchResource] A               
                 left join [SYS_MESUsers] B on A.[Creator] = B.[MESUserID]
                 left join [SYS_MESUsers] C on A.[Modifier] =C.[MESUserID]
                 left join [SYS_Parameters] D on A.[ResourceClassID] = D.[ParameterID]
                 left join [SYS_Resources] E on A.[ResourceID] = E.[ResourceID]
                 left join [EMS_Equipment] F on A.[EquipmentID] = F.[EquipmentID]
                where A.[SystemID] = '{0}' and A.[Status] = '{0}0201213000001' 
                and A.[TaskDispatchID]='{1}' and A.[ResourceClassID]='{2}'", Framework.SystemID);

            count = UniversalService.getCount(sql, null);

            string orderby = "A.[Status],A.[Sequence] ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, null, page, rows);

            return ToHashtableList(dt);
        }

    }
}

