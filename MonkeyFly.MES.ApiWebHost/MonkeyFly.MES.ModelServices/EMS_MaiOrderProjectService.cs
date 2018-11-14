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
    public class EMS_MaiOrderProjectService : SuperModel<EMS_MaiOrderProject>
    {
        /// <summary>
        /// 新增
        /// SAM 2017年7月9日16:02:22
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="Model"></param>
        /// <returns></returns>
        public static bool insert(string userId, EMS_MaiOrderProject Model)
        {
            try
            {
                string sql = string.Format(@"insert[EMS_MaiOrderProject]([MaiOrderProjectID],[MaintenanceOrderID],[MaiOrderEquipmentID],
                [Sequence],[MaiProjectID],[Attribute],[AttributeValue],
                [Status],[Comments],[Modifier],[ModifiedTime],[ModifiedLocalTime],[Creator],[CreateTime],[CreateLocalTime],[SystemID]) values
                 (@MaiOrderProjectID,@MaintenanceOrderID,
                @MaiOrderEquipmentID,@Sequence,@MaiProjectID,
                @Attribute,@AttributeValue,@Status,
                @Comments,'{0}','{1}','{2}','{0}','{1}','{2}','{3}')", userId, DateTime.Now, DateTime.Now, Framework.SystemID);

                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@MaiOrderProjectID",SqlDbType.VarChar),
                    new SqlParameter("@MaintenanceOrderID",SqlDbType.VarChar),
                    new SqlParameter("@MaiOrderEquipmentID",SqlDbType.VarChar),
                    new SqlParameter("@Sequence",SqlDbType.Int),
                    new SqlParameter("@MaiProjectID",SqlDbType.VarChar),
                    new SqlParameter("@Attribute",SqlDbType.VarChar),
                    new SqlParameter("@AttributeValue",SqlDbType.NVarChar),
                    new SqlParameter("@Status",SqlDbType.VarChar),
                    new SqlParameter("@Comments",SqlDbType.NVarChar),
                    };

                parameters[0].Value = (Object)Model.MaiOrderProjectID ?? DBNull.Value;
                parameters[1].Value = (Object)Model.MaintenanceOrderID ?? DBNull.Value;
                parameters[2].Value = (Object)Model.MaiOrderEquipmentID ?? DBNull.Value;
                parameters[3].Value = (Object)Model.Sequence ?? DBNull.Value;
                parameters[4].Value = (Object)Model.MaiProjectID ?? DBNull.Value;
                parameters[5].Value = (Object)Model.Attribute ?? DBNull.Value;
                parameters[6].Value = (Object)Model.AttributeValue ?? DBNull.Value;
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

        /// <summary>
        /// 更新
        /// SAM 2017年7月9日16:02:29
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="Model"></param>
        /// <returns></returns>
        public static bool update(string userId, EMS_MaiOrderProject Model)
        {
            try
            {
                string sql = String.Format(@"update[EMS_MaiOrderProject] set {0},
                [Sequence]=@Sequence,
                [MaiProjectID]=@MaiProjectID,[Attribute]=@Attribute,[AttributeValue]=@AttributeValue,[Status]=@Status,
                [Comments]=@Comments where [MaiOrderProjectID]=@MaiOrderProjectID", UniversalService.getUpdateUTC(userId));
                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@MaiOrderProjectID",SqlDbType.VarChar),
                    new SqlParameter("@Sequence",SqlDbType.Int),
                    new SqlParameter("@MaiProjectID",SqlDbType.VarChar),
                    new SqlParameter("@Attribute",SqlDbType.VarChar),
                    new SqlParameter("@AttributeValue",SqlDbType.NVarChar),
                    new SqlParameter("@Status",SqlDbType.VarChar),
                    new SqlParameter("@Comments",SqlDbType.NVarChar),
                    };

                parameters[0].Value = Model.MaiOrderProjectID;
                parameters[1].Value = (Object)Model.Sequence ?? DBNull.Value;
                parameters[2].Value = (Object)Model.MaiProjectID ?? DBNull.Value;
                parameters[3].Value = (Object)Model.Attribute ?? DBNull.Value;
                parameters[4].Value = (Object)Model.AttributeValue ?? DBNull.Value;
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
        /// 获取单一实体
        /// SAM 2017年7月9日16:02:36
        /// </summary>
        /// <param name="MaiOrderProjectID"></param>
        /// <returns></returns>
        public static EMS_MaiOrderProject get(string MaiOrderProjectID)
        {
            string sql = string.Format(@"select Top 1 * from [EMS_MaiOrderProject] where [MaiOrderProjectID] = '{0}'  and [SystemID] = '{1}' ", MaiOrderProjectID, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return null;
            else
                return ToEntity(dt.Rows[0]);
        }

        /// <summary>
        /// 根据保养工单设备获取保养项目列表
        /// SAM 2017年7月9日16:01:15
        /// </summary>
        /// <param name="MaiOrderEquipmentID"></param>
        /// <param name="Code"></param>
        /// <param name="Name"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> Ems00009GetProjectList(string MaiOrderEquipmentID, string Code, string Name, int page, int rows, ref int count)
        {
            string select = string.Format(@"select A.MaiOrderProjectID,A.MaintenanceOrderID,A.Sequence,A.MaiOrderEquipmentID,
            A.MaiProjectID,A.Attribute,A.AttributeValue,A.Status,A.Comments,
            D.Code as ProjectCode,D.Name as ProjectName,
            E.Name as StatusName,F.Name as AttributeName,
            (CASE WHEN B.Emplno is null or B.Emplno = '' THEN B.Account else B.Emplno END)+'-'+B.UserName as Creator,A.CreateLocalTime as CreateTime,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else (CASE WHEN C.Emplno is null or C.Emplno = '' THEN C.Account else C.Emplno END)+'-'+C.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ");

            string sql = string.Format(@"  from [EMS_MaiOrderProject] A 
            left join [SYS_MESUsers] B on A.[Creator] = B.[MESUserID]
            left join [SYS_MESUsers] C on A.[Modifier] = C.[MESUserID]
            left join [SYS_Parameters] D on A.[MaiProjectID] = D.[ParameterID]
            left join [SYS_Parameters] E on A.[Status] = E.[ParameterID]
            left join [SYS_Parameters] F on A.[Attribute] = F.[ParameterID]
            where A.[SystemID]='{0}' and A.[Status] = '{0}0201213000001' and A.[MaiOrderEquipmentID] = '{1}'", Framework.SystemID, MaiOrderEquipmentID);

            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@Code",SqlDbType.VarChar),
                new SqlParameter("@Name",SqlDbType.VarChar)
            };
            parameters[0].Value = DBNull.Value;
            parameters[1].Value = DBNull.Value;

            SqlParameter[] Parcount = new SqlParameter[]{
                new SqlParameter("@Code",SqlDbType.VarChar),
                new SqlParameter("@Name",SqlDbType.VarChar)
            };
            Parcount[0].Value = DBNull.Value;
            Parcount[1].Value = DBNull.Value;


            if (!string.IsNullOrWhiteSpace(Code))
            {
                sql = sql + string.Format(@" and D.[Code] = @Code ");
                parameters[0].Value = Code;
                Parcount[0].Value = Code;
            }

            if (!string.IsNullOrWhiteSpace(Name))
            {
                sql = sql + string.Format(@" and D.[Name] = @Name ");
                parameters[1].Value = Name;
                Parcount[1].Value = Name;
            }

            count = UniversalService.getCount(sql, Parcount);

            string orderby = " A.[Sequence] ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, parameters, page, rows);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// 判断保养项目是否存在于保养工单中
        /// SAM 2017年7月19日12:04:10
        /// </summary>
        /// <param name="MaiProjectID"></param>
        /// <returns></returns>
        public static bool CheckMaiProject(string MaiProjectID)
        {
            string sql = string.Format(@"select * from [EMS_MaiOrderProject] where [SystemID] = '{1}' and [MaiProjectID]='{0}' and [Status] <> '{1}0201213000003'", MaiProjectID, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return false;
            else
                return true;
        }

        /// <summary>
        /// 查找保养单设备流水号和保养项目流水号
        /// Joint 2017年8月2日15:37:32
        /// </summary>
        /// <param name="MaiOrderEquipmentID"></param>
        /// <param name="MaiProjectID"></param>
        /// <returns></returns>
        public static EMS_MaiOrderProject GetMaiProject(string MaiOrderEquipmentID, string MaiProjectID)
        {
            string sql = string.Format(@"select * from [EMS_MaiOrderProject] where [SystemID] = '{2}' and [MaiOrderEquipmentID]='{1}' and [MaiProjectID]='{0}' and [Status] <> '{2}0201213000003'", MaiProjectID, MaiOrderEquipmentID, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return null;
            else
                return ToEntity(dt.Rows[0]);
        }

        /// <summary>
        /// 設備保養資料維護导出
        /// SAM 2017年8月28日23:33:20
        /// </summary>
        /// <param name="StartMaintenanceDate"></param>
        /// <param name="EndMaintenanceDate"></param>
        /// <param name="Type"></param>
        /// <param name="StartMaintenanceNo"></param>
        /// <param name="EndMaintenanceNo"></param>
        /// <param name="Status"></param>
        /// <param name="Emplno"></param>
        /// <param name="Code"></param>
        /// <returns></returns>
        public static DataTable Ems00010Export(string StartMaintenanceDate, string EndMaintenanceDate, string TypeCode,
            string StartMaintenanceNo, string EndMaintenanceNo, string Status, string Emplno, string Code)
        {
            string select = string.Format(@"select null as RowNumber,
            CONVERT(varchar(10), B.MaintenanceDate, 20),
            (select [Name] from [SYS_Parameters] where B.[Type]=ParameterID) as Type,
            B.MaintenanceNo,
            (select [Name] from [SYS_Parameters] where B.[Status]=ParameterID) as Status,
            C.Sequence,
            D.[Code] as EquipmentIDCode,D.[Name] as EquipmentIDName,
            (select [Emplno] from [SYS_MESUsers] where B.[MESUserID]=[MESUserID]) as Emplno,
            (select [UserName] from [SYS_MESUsers] where B.[MESUserID]=[MESUserID]) as UserName,
            (select [Name] from [SYS_Organization] where B.[OrganizationID]=[OrganizationID]),
            (select [Name] from [SYS_Manufacturers] where B.[ManufacturerID]=[ManufacturerID]),
            (select [Name] from [SYS_Organization] where D.[OrganizationID]=[OrganizationID]),
            D.[Model],D.[MachineNo],D.[FixedAssets],
            CONVERT(varchar(10), D.[ExpireDate], 20),
            E.[Code] as MaiProjectCode,E.[Name] as MaiProjectName,
            (select [Name] from [SYS_Parameters] where A.[Attribute]=ParameterID) as Attribute,
            A.AttributeValue,B.Comments ");

            string sql = string.Format(@"  from [EMS_MaiOrderProject] A 
            left join [EMS_MaintenanceOrder] B on A.[MaintenanceOrderID]=B.[MaintenanceOrderID] 
            left join [EMS_MaiOrderEquipment] C on A.[MaiOrderEquipmentID]=C.[MaiOrderEquipmentID]
            left join [EMS_Equipment] D on C.[EquipmentID]=D.[EquipmentID]
            left join [SYS_Parameters] E on A.[MaiProjectID]=E.[ParameterID]
            left join [SYS_MESUsers] F on B.[MESUserID]=F.[MESUserID]
            left join [SYS_Parameters] G on B.[Type]=G.[ParameterID]
            where A.[SystemID]='{0}' and A.[Status] ='{0}0201213000001' 
             and B.[Status] in ('{0}0201213000029','{0}020121300002A') 
             and C.[Status] in ('{0}0201213000029','{0}020121300002A') ", Framework.SystemID);

            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@StartMaintenanceDate",SqlDbType.VarChar),
                new SqlParameter("@EndMaintenanceDate",SqlDbType.VarChar),
                new SqlParameter("@TypeCode",SqlDbType.VarChar),
                new SqlParameter("@StartMaintenanceNo",SqlDbType.VarChar),
                new SqlParameter("@EndMaintenanceNo",SqlDbType.VarChar),
                new SqlParameter("@Status",SqlDbType.VarChar),
                new SqlParameter("@Emplno",SqlDbType.VarChar),
                new SqlParameter("@CodeName",SqlDbType.VarChar)

            };
            parameters[0].Value = DBNull.Value;
            parameters[1].Value = DBNull.Value;
            parameters[2].Value = DBNull.Value;
            parameters[3].Value = DBNull.Value;
            parameters[4].Value = DBNull.Value;
            parameters[5].Value = DBNull.Value;
            parameters[6].Value = DBNull.Value;
            parameters[7].Value = DBNull.Value;

            SqlParameter[] Parcount = new SqlParameter[]{
                new SqlParameter("@StartMaintenanceDate",SqlDbType.VarChar),
                new SqlParameter("@EndMaintenanceDate",SqlDbType.VarChar),
                new SqlParameter("@TypeCode",SqlDbType.VarChar),
                new SqlParameter("@StartMaintenanceNo",SqlDbType.VarChar),
                new SqlParameter("@EndMaintenanceNo",SqlDbType.VarChar),
                new SqlParameter("@Status",SqlDbType.VarChar),
                new SqlParameter("@Emplno",SqlDbType.VarChar),
                new SqlParameter("@CodeName",SqlDbType.VarChar)
            };
            Parcount[0].Value = DBNull.Value;
            Parcount[1].Value = DBNull.Value;
            Parcount[2].Value = DBNull.Value;
            Parcount[3].Value = DBNull.Value;
            Parcount[4].Value = DBNull.Value;
            Parcount[5].Value = DBNull.Value;
            Parcount[6].Value = DBNull.Value;
            Parcount[7].Value = DBNull.Value;


            if (!string.IsNullOrWhiteSpace(StartMaintenanceDate))
            {
                sql = sql + String.Format(@" and B.[MaintenanceDate]  >=  @StartMaintenanceDate ");
                parameters[0].Value = StartMaintenanceDate;
                Parcount[0].Value = StartMaintenanceDate;
            }
            if (!string.IsNullOrWhiteSpace(EndMaintenanceDate))
            {
                sql = sql + String.Format(@" and B.[MaintenanceDate]  <=  @EndMaintenanceDate ");
                parameters[1].Value = EndMaintenanceDate;
                Parcount[1].Value = EndMaintenanceDate;
            }
            if (!string.IsNullOrWhiteSpace(TypeCode))
            {
                sql = sql + String.Format(@" and G.[Code] like @TypeCode ");
                parameters[2].Value = TypeCode;
                Parcount[2].Value = TypeCode;
            }
            if (!string.IsNullOrWhiteSpace(StartMaintenanceNo))
            {
                sql = sql + String.Format(@" and B.[MaintenanceNo]  >=  @StartMaintenanceNo ");
                parameters[3].Value = StartMaintenanceNo;
                Parcount[3].Value = StartMaintenanceNo;
            }
            if (!string.IsNullOrWhiteSpace(EndMaintenanceNo))
            {
                sql = sql + String.Format(@" and B.[MaintenanceNo] <= @EndMaintenanceNo ");
                parameters[4].Value = EndMaintenanceNo;
                Parcount[4].Value = EndMaintenanceNo;
            }

            if (!string.IsNullOrWhiteSpace(Status))
            {
                sql = sql + String.Format(@" and B.[Status] = @Status ");
                parameters[5].Value = Status;
                Parcount[5].Value = Status;
            }

            if (!string.IsNullOrWhiteSpace(Emplno))
            {
                Emplno = "%" + Emplno + "%";
                sql = sql + String.Format(@" and F.[Emplno] like @Emplno ");
                parameters[6].Value = Emplno;
                Parcount[6].Value = Emplno;
            }
            if (!string.IsNullOrWhiteSpace(Code))
            {
                Code = "%" + Code + "%";
                sql = sql + String.Format(@" and D.[Code]  like  @Code ");
                parameters[7].Value = Code;
                Parcount[7].Value = Code;
            }
            string orderBy = "order By B.[MaintenanceDate],B.[Type],B.[MaintenanceNo],C.[Sequence] ";

            return SQLHelper.ExecuteDataTable(select + sql + orderBy, CommandType.Text, parameters);
        }
    }
}

