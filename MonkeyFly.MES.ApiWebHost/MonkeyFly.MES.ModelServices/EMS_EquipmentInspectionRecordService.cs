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
    public class EMS_EquipmentInspectionRecordService : SuperModel<EMS_EquipmentInspectionRecord>
    {

        public static bool insert(string userId, EMS_EquipmentInspectionRecord Model)
        {
            try
            {
                string sql = string.Format(@"insert[EMS_EquipmentInspectionRecord]([EquipmentInspectionRecordID],[Code],[Date],[EquipmentID],[ClassID],
            [MESUserID],[TaskID],[ItemID],[Status],[Comments],[Modifier],[ModifiedTime],[ModifiedLocalTime],[Creator],[CreateTime],[CreateLocalTime],[SystemID]) values
             (@EquipmentInspectionRecordID,@Code,@Date,@EquipmentID,@ClassID,@MESUserID,@TaskID,@ItemID,
            @Status,@Comments,'{0}','{1}','{2}','{0}','{1}','{2}','{3}')", userId, DateTime.Now, DateTime.Now, Framework.SystemID);

                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@EquipmentInspectionRecordID",SqlDbType.VarChar),
                    new SqlParameter("@Code",SqlDbType.NVarChar),
                    new SqlParameter("@Date",SqlDbType.DateTime),
                    new SqlParameter("@EquipmentID",SqlDbType.VarChar),
                    new SqlParameter("@ClassID",SqlDbType.VarChar),
                    new SqlParameter("@MESUserID",SqlDbType.VarChar),
                    new SqlParameter("@TaskID",SqlDbType.VarChar),
                    new SqlParameter("@ItemID",SqlDbType.VarChar),
                    new SqlParameter("@Status",SqlDbType.VarChar),
                    new SqlParameter("@Comments",SqlDbType.NVarChar),
                    };

                parameters[0].Value = (Object)Model.EquipmentInspectionRecordID ?? DBNull.Value;
                parameters[1].Value = (Object)Model.Code ?? DBNull.Value;
                parameters[2].Value = (Object)Model.Date ?? DBNull.Value;
                parameters[3].Value = (Object)Model.EquipmentID ?? DBNull.Value;
                parameters[4].Value = (Object)Model.ClassID ?? DBNull.Value;
                parameters[5].Value = (Object)Model.MESUserID ?? DBNull.Value;
                parameters[6].Value = (Object)Model.TaskID ?? DBNull.Value;
                parameters[7].Value = (Object)Model.ItemID ?? DBNull.Value;
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

        public static bool update(string userId, EMS_EquipmentInspectionRecord Model)
        {
            try
            {
                string sql = String.Format(@"update[EMS_EquipmentInspectionRecord] set {0},
            [Date]=@Date,[EquipmentID]=@EquipmentID,
            [ClassID]=@ClassID,[MESUserID]=@MESUserID,[TaskID]=@TaskID,[ItemID]=@ItemID,
            [Status]=@Status,[Comments]=@Comments where [EquipmentInspectionRecordID]=@EquipmentInspectionRecordID", UniversalService.getUpdateUTC(userId));
                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@EquipmentInspectionRecordID",SqlDbType.VarChar),
                    new SqlParameter("@Date",SqlDbType.DateTime),
                    new SqlParameter("@EquipmentID",SqlDbType.VarChar),
                    new SqlParameter("@ClassID",SqlDbType.VarChar),
                    new SqlParameter("@MESUserID",SqlDbType.VarChar),
                    new SqlParameter("@TaskID",SqlDbType.VarChar),
                    new SqlParameter("@ItemID",SqlDbType.VarChar),
                    new SqlParameter("@Status",SqlDbType.VarChar),
                    new SqlParameter("@Comments",SqlDbType.NVarChar),
                    };

                parameters[0].Value = Model.EquipmentInspectionRecordID;
                parameters[1].Value = (Object)Model.Date ?? DBNull.Value;
                parameters[2].Value = (Object)Model.EquipmentID ?? DBNull.Value;
                parameters[3].Value = (Object)Model.ClassID ?? DBNull.Value;
                parameters[4].Value = (Object)Model.MESUserID ?? DBNull.Value;
                parameters[5].Value = (Object)Model.TaskID ?? DBNull.Value;
                parameters[6].Value = (Object)Model.ItemID ?? DBNull.Value;
                parameters[7].Value = (Object)Model.Status ?? DBNull.Value;
                parameters[8].Value = (Object)Model.Comments ?? DBNull.Value;

                return SQLHelper.ExecuteNonQuery(sql, CommandType.Text, parameters) > 0;
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return false;
            }
        }

        public static EMS_EquipmentInspectionRecord get(string EquipmentInspectionRecordID)
        {
            string sql = string.Format(@"select Top 1 * from [EMS_EquipmentInspectionRecord] where [EquipmentInspectionRecordID] = '{0}'  and [SystemID] = '{1}' ", EquipmentInspectionRecordID, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return null;
            else
                return ToEntity(dt.Rows[0]);
        }

        /// <summary>
        /// 设备巡检维护表头列表
        /// SAM 2017年6月8日14:55:071
        /// </summary>
        /// <param name="Code"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public static IList<Hashtable> EMS00003GetList(string Code, string startDate, string endDate)
        {
            string select = string.Format(@"select A.EquipmentInspectionRecordID,A.Code,A.Date,A.EquipmentID,
            A.ClassID,A.MESUserID,A.TaskID,A.ItemID,A.Status,H.TaskNo,
            D.Code as EquipmentCode,D.Name as EquipmentName,E.Code as ClassCode,E.Name as ClassName,
            F.Emplno as MESUserCode,F.UserName as MESUserName,
            G.Code as ItemCode,G.Name as ItemName,
            B.UserName as Creator,A.CreateLocalTime as CreateTime,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else C.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ");

            string sql = string.Format(@"  from [EMS_EquipmentInspectionRecord] A 
            left join [SYS_MESUsers] B on A.[Creator] = B.[MESUserID]
            left join [SYS_MESUsers] C on A.[Modifier] = C.[MESUserID]
            left join [EMS_Equipment] D on A.[EquipmentID] = D.[EquipmentID]
            left join [SYS_Class] E on A.[ClassID] = E.[ClassID]
            left join [SYS_MESUsers] F on A.[MESUserID] = F.[MESUserID]
            left join [SYS_Items] G on A.[ItemID] = G.[ItemID]
            left join [SFC_TaskDispatch] H on A.[TaskID] = H.[TaskDispatchID]
            where A.[SystemID]='{0}' and A.[Status] = '{0}0201213000001' ", Framework.SystemID);

            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@Code",SqlDbType.VarChar),
                new SqlParameter("@startDate",SqlDbType.VarChar),
                new SqlParameter("@endDate",SqlDbType.VarChar)
            };
            parameters[0].Value = DBNull.Value;
            parameters[1].Value = DBNull.Value;
            parameters[2].Value = DBNull.Value;


            if (!string.IsNullOrWhiteSpace(Code))
            {
                Code = "%" + Code + "%";
                sql = sql + string.Format(@" and D.[Code] like @Code ");
                parameters[0].Value = Code;
            }

            if (!string.IsNullOrWhiteSpace(startDate))
            {
                sql = sql + string.Format(@" and A.[Date] >= @startDate ");
                parameters[1].Value = startDate;
            }

            if (!string.IsNullOrWhiteSpace(endDate))
            {
                sql = sql + string.Format(@" and A.[Date] <= @endDate ");
                parameters[2].Value = endDate;
            }

            string orderby = "order By A.[Code] desc ";

            DataTable dt = SQLHelper.ExecuteDataTable(select+sql +orderby,CommandType.Text,parameters);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// 设备巡检维护导出
        /// SAM 2017年6月8日16:34:54
        /// </summary>
        /// <param name="Code"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public static DataTable Ems00003Export(string Code, string startDate, string endDate)
        {
            string select = string.Format(@"select ROW_NUMBER() OVER (ORDER BY A.[Status],A.[Code]),A.Code,
            D.Code as EquipmentCode,D.Name as EquipmentName,convert(char(10),A.Date,111) as Date,
            E.Code as ClassCode,F.Emplno as MESUserCode,A.TaskID,G.Code as ItemCode,G.Name as ItemName,H.Sequence,
            (Select Code from [SYS_Projects] where [ProjectID] = H.[ProjectID]) as ProjectCode,
            (Select Description from [SYS_Projects] where [ProjectID] =H.[ProjectID]) as ProjectDescription,
            H.Value,H.StandardValue,H.MaxValue,H.MinValue,A.Comments,
            B.UserName as Creator,A.CreateLocalTime as CreateTime,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else C.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ");

            string sql = string.Format(@"  from [EMS_EquipmentInspectionRecord] A 
            left join [SYS_MESUsers] B on A.[Creator] = B.[MESUserID]
            left join [SYS_MESUsers] C on A.[Modifier] = C.[MESUserID]
            left join [EMS_Equipment] D on A.[EquipmentID] = D.[EquipmentID]
            left join [SYS_Class] E on A.[ClassID] = E.[ClassID]
            left join [SYS_MESUsers] F on A.[MESUserID] = F.[MESUserID]
            left join [SYS_Items] G on A.[ItemID] = G.[ItemID]
            left join [EMS_EquipmentInspectionRecordDetails] H on A.[EquipmentInspectionRecordID] = H.[EquipmentInspectionRecordID]
         
            where A.[SystemID]='{0}' and A.[Status] = '{0}0201213000001' ", Framework.SystemID);

            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@Code",SqlDbType.VarChar),
                new SqlParameter("@startDate",SqlDbType.VarChar),
                new SqlParameter("@endDate",SqlDbType.VarChar)
            };
            parameters[0].Value = DBNull.Value;
            parameters[1].Value = DBNull.Value;
            parameters[2].Value = DBNull.Value;


            if (!string.IsNullOrWhiteSpace(Code))
            {
                Code = "%" + Code + "%";
                sql = sql + string.Format(@" and A.[Code] like @Code ");
                parameters[0].Value = Code;
            }

            if (!string.IsNullOrWhiteSpace(startDate))
            {
                sql = sql + string.Format(@" and A.[Date] >= @startDate ");
                parameters[1].Value = startDate;
            }

            if (!string.IsNullOrWhiteSpace(endDate))
            {
                sql = sql + string.Format(@" and A.[Date] <= @endDate ");
                parameters[2].Value = endDate;
            }

            string orderby = "order By A.[Status],A.[Code] ";

            return SQLHelper.ExecuteDataTable(select + sql + orderby, CommandType.Text);   
        }
    }
}

