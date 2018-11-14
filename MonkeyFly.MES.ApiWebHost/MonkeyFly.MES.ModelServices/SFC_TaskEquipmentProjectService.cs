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
    public class SFC_TaskEquipmentProjectService : SuperModel<SFC_TaskEquipmentProject>
    {

        public static bool insert(string userId, SFC_TaskEquipmentProject Model)
        {
            try
            {
                string sql = string.Format(@"insert[SFC_TaskEquipmentProject]([TaskEquipmentProjectID],[FabricatedProcessID],[FabricatedOperationID],[TaskDispatchID],[EquipmentID],[ProjectID],[IfCollection],[CollectionWay],[StandardValue],[MaxValue],[MinValue],[RecordValue],[IfEntryRecord],[Status],[Comments],[Modifier],[ModifiedTime],[ModifiedLocalTime],[Creator],[CreateTime],[CreateLocalTime],[SystemID]) values
 (






@TaskEquipmentProjectID,@FabricatedProcessID,
@FabricatedOperationID,@TaskDispatchID,@EquipmentID,
@ProjectID,@IfCollection,@CollectionWay,
@StandardValue,@MaxValue,@MinValue,
@RecordValue,@IfEntryRecord,@Status,
@Comments,'{0}','{1}','{2}','{0}','{1}','{2}','{3}')", userId, DateTime.Now, DateTime.Now, Framework.SystemID);

                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@TaskEquipmentProjectID",SqlDbType.VarChar),
                    new SqlParameter("@FabricatedProcessID",SqlDbType.VarChar),
                    new SqlParameter("@FabricatedOperationID",SqlDbType.VarChar),
                    new SqlParameter("@TaskDispatchID",SqlDbType.VarChar),
                    new SqlParameter("@EquipmentID",SqlDbType.VarChar),
                    new SqlParameter("@ProjectID",SqlDbType.NVarChar),
                    new SqlParameter("@IfCollection",SqlDbType.Bit),
                    new SqlParameter("@CollectionWay",SqlDbType.VarChar),
                    new SqlParameter("@StandardValue",SqlDbType.NVarChar),
                    new SqlParameter("@MaxValue",SqlDbType.NVarChar),
                    new SqlParameter("@MinValue",SqlDbType.NVarChar),
                    new SqlParameter("@RecordValue",SqlDbType.NVarChar),
                    new SqlParameter("@IfEntryRecord",SqlDbType.Bit),
                    new SqlParameter("@Status",SqlDbType.VarChar),
                    new SqlParameter("@Comments",SqlDbType.NVarChar),
                    };

                parameters[0].Value = (Object)Model.TaskEquipmentProjectID ?? DBNull.Value;
                parameters[1].Value = (Object)Model.FabricatedProcessID ?? DBNull.Value;
                parameters[2].Value = (Object)Model.FabricatedOperationID ?? DBNull.Value;
                parameters[3].Value = (Object)Model.TaskDispatchID ?? DBNull.Value;
                parameters[4].Value = (Object)Model.EquipmentID ?? DBNull.Value;
                parameters[5].Value = (Object)Model.ProjectID ?? DBNull.Value;
                parameters[6].Value = (Object)Model.IfCollection ?? DBNull.Value;
                parameters[7].Value = (Object)Model.CollectionWay ?? DBNull.Value;
                parameters[8].Value = (Object)Model.StandardValue ?? DBNull.Value;
                parameters[9].Value = (Object)Model.MaxValue ?? DBNull.Value;
                parameters[10].Value = (Object)Model.MinValue ?? DBNull.Value;
                parameters[11].Value = (Object)Model.RecordValue ?? DBNull.Value;
                parameters[12].Value = (Object)Model.IfEntryRecord ?? DBNull.Value;
                parameters[13].Value = (Object)Model.Status ?? DBNull.Value;
                parameters[14].Value = (Object)Model.Comments ?? DBNull.Value;

                return SQLHelper.ExecuteNonQuery(sql, CommandType.Text, parameters) > 0;
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return false;
            }
        }

        public static bool update(string userId, SFC_TaskEquipmentProject Model)
        {
            try
            {
                string sql = String.Format(@"update[SFC_TaskEquipmentProject] set {0},
[FabricatedProcessID]=@FabricatedProcessID,[FabricatedOperationID]=@FabricatedOperationID,[TaskDispatchID]=@TaskDispatchID,
[EquipmentID]=@EquipmentID,[ProjectID]=@ProjectID,[IfCollection]=@IfCollection,[CollectionWay]=@CollectionWay,
[StandardValue]=@StandardValue,[MaxValue]=@MaxValue,[MinValue]=@MinValue,[RecordValue]=@RecordValue,
[IfEntryRecord]=@IfEntryRecord,[Status]=@Status,[Comments]=@Comments where [TaskEquipmentProjectID]=@TaskEquipmentProjectID", UniversalService.getUpdateUTC(userId));
                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@TaskEquipmentProjectID",SqlDbType.VarChar),
                    new SqlParameter("@FabricatedProcessID",SqlDbType.VarChar),
                    new SqlParameter("@FabricatedOperationID",SqlDbType.VarChar),
                    new SqlParameter("@TaskDispatchID",SqlDbType.VarChar),
                    new SqlParameter("@EquipmentID",SqlDbType.VarChar),
                    new SqlParameter("@ProjectID",SqlDbType.NVarChar),
                    new SqlParameter("@IfCollection",SqlDbType.Bit),
                    new SqlParameter("@CollectionWay",SqlDbType.VarChar),
                    new SqlParameter("@StandardValue",SqlDbType.NVarChar),
                    new SqlParameter("@MaxValue",SqlDbType.NVarChar),
                    new SqlParameter("@MinValue",SqlDbType.NVarChar),
                    new SqlParameter("@RecordValue",SqlDbType.NVarChar),
                    new SqlParameter("@IfEntryRecord",SqlDbType.Bit),
                    new SqlParameter("@Status",SqlDbType.VarChar),
                    new SqlParameter("@Comments",SqlDbType.NVarChar),
                    };

                parameters[0].Value = Model.TaskEquipmentProjectID;
                parameters[1].Value = (Object)Model.FabricatedProcessID ?? DBNull.Value;
                parameters[2].Value = (Object)Model.FabricatedOperationID ?? DBNull.Value;
                parameters[3].Value = (Object)Model.TaskDispatchID ?? DBNull.Value;
                parameters[4].Value = (Object)Model.EquipmentID ?? DBNull.Value;
                parameters[5].Value = (Object)Model.ProjectID ?? DBNull.Value;
                parameters[6].Value = (Object)Model.IfCollection ?? DBNull.Value;
                parameters[7].Value = (Object)Model.CollectionWay ?? DBNull.Value;
                parameters[8].Value = (Object)Model.StandardValue ?? DBNull.Value;
                parameters[9].Value = (Object)Model.MaxValue ?? DBNull.Value;
                parameters[10].Value = (Object)Model.MinValue ?? DBNull.Value;
                parameters[11].Value = (Object)Model.RecordValue ?? DBNull.Value;
                parameters[12].Value = (Object)Model.IfEntryRecord ?? DBNull.Value;
                parameters[13].Value = (Object)Model.Status ?? DBNull.Value;
                parameters[14].Value = (Object)Model.Comments ?? DBNull.Value;

                return SQLHelper.ExecuteNonQuery(sql, CommandType.Text, parameters) > 0;
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return false;
            }
        }

        public static SFC_TaskEquipmentProject get(string TaskEquipmentProjectID)
        {
            string sql = string.Format(@"select Top 1 * from [SFC_TaskEquipmentProject] where [TaskEquipmentProjectID] = '{0}'  and [SystemID] = '{1}' ", TaskEquipmentProjectID, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return null;
            else
                return ToEntity(dt.Rows[0]);
        }

        /// <summary>
        /// 任务单项目设备列表
        /// SAM 2017年7月3日15:03:07
        /// </summary>
        /// <param name="TaskDispatchID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> Sfc00004GetEquipmentProjectList(string TaskDispatchID, int page, int rows, ref int count)
        {
            string select = string.Format(
              @"select A.TaskEquipmentProjectID,A.EquipmentID,A.ProjectID,
                A.IfCollection,A.CollectionWay,A.StandardValue,A.MaxValue,
                A.MinValue,A.RecordValue,A.IfEntryRecord,A.Comments,A.Status,
                B.Emplno+'-'+B.UserName as Creator,A.CreateLocalTime as CreateTime,
                (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else C.Emplno+'-'+C.UserName END) as Modifier,
               (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ", Framework.SystemID);

            string sql = string.Format(
                @"from [SFC_TaskEquipmentProject] A               
                 left join [SYS_MESUsers] B on A.[Creator] = B.[MESUserID]
                 left join [SYS_MESUsers] C on A.[Modifier] =C.[MESUserID]
                where A.[SystemID] = '{0}' and A.[Status] <> '{0}0201213000003' and A.[TaskDispatchID]='{1}'", Framework.SystemID, TaskDispatchID);

            count = UniversalService.getCount(sql, null);

            string orderby = "A.[Status] ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, null, page, rows);

            return ToHashtableList(dt);
        }
    }
}

