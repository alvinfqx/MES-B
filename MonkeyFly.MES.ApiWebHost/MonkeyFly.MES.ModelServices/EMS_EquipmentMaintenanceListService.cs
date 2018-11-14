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
    public class EMS_EquipmentMaintenanceListService : SuperModel<EMS_EquipmentMaintenanceList>
    {
        /// <summary>
        /// 新增
        /// SAM 2017年7月5日14:56:36
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="Model"></param>
        /// <returns></returns>
        public static bool insert(string userId, EMS_EquipmentMaintenanceList Model)
        {
            try
            {
                string sql = string.Format(@"insert[EMS_EquipmentMaintenanceList]([EquipmentMaintenanceListID],[Code],[Name],[Type],
                [Status],[Comments],[Modifier],[ModifiedTime],[ModifiedLocalTime],[Creator],[CreateTime],[CreateLocalTime],[SystemID]) values
                 (@EquipmentMaintenanceListID,@Code,@Name,@Type,@Status,
                @Comments,'{0}','{1}','{2}','{0}','{1}','{2}','{3}')", userId, DateTime.Now, DateTime.Now, Framework.SystemID);

                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@EquipmentMaintenanceListID",SqlDbType.VarChar),
                    new SqlParameter("@Code",SqlDbType.NVarChar),
                    new SqlParameter("@Name",SqlDbType.NVarChar),
                    new SqlParameter("@Type",SqlDbType.VarChar),
                    new SqlParameter("@Status",SqlDbType.VarChar),
                    new SqlParameter("@Comments",SqlDbType.NVarChar),
                    };

                parameters[0].Value = (Object)Model.EquipmentMaintenanceListID ?? DBNull.Value;
                parameters[1].Value = (Object)Model.Code ?? DBNull.Value;
                parameters[2].Value = (Object)Model.Name ?? DBNull.Value;
                parameters[3].Value = (Object)Model.Type ?? DBNull.Value;
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
        /// SAM 2017年7月5日14:56:44
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="Model"></param>
        /// <returns></returns>
        public static bool update(string userId, EMS_EquipmentMaintenanceList Model)
        {
            try
            {
                string sql = String.Format(@"update[EMS_EquipmentMaintenanceList] set {0},
                [Name]=@Name,[Status]=@Status,[Comments]=@Comments 
                where [EquipmentMaintenanceListID]=@EquipmentMaintenanceListID", UniversalService.getUpdateUTC(userId));
                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@EquipmentMaintenanceListID",SqlDbType.VarChar),
                    new SqlParameter("@Name",SqlDbType.NVarChar),
                    new SqlParameter("@Status",SqlDbType.VarChar),
                    new SqlParameter("@Comments",SqlDbType.NVarChar),
                    };

                parameters[0].Value = Model.EquipmentMaintenanceListID;
                parameters[1].Value = (Object)Model.Name ?? DBNull.Value;
                parameters[2].Value = (Object)Model.Status ?? DBNull.Value;
                parameters[3].Value = (Object)Model.Comments ?? DBNull.Value;

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
        /// SAM 2017年7月5日14:57:01
        /// </summary>
        /// <param name="EquipmentMaintenanceListID"></param>
        /// <returns></returns>
        public static EMS_EquipmentMaintenanceList get(string EquipmentMaintenanceListID)
        {
            string sql = string.Format(@"select Top 1 * from [EMS_EquipmentMaintenanceList] where [EquipmentMaintenanceListID] = '{0}'  and [SystemID] = '{1}' ", EquipmentMaintenanceListID, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return null;
            else
                return ToEntity(dt.Rows[0]);
        }

        /// <summary>
        /// 判断代号是否重复
        /// SAM 2017年7月5日15:08:24
        /// </summary>
        /// <param name="Code"></param>
        /// <param name="EquipmentMaintenanceListID"></param>
        /// <returns></returns>
        public static bool CheckCode(string Code, string EquipmentMaintenanceListID)
        {
            string sql = String.Format(@"select Top 1 * from [EMS_EquipmentMaintenanceList] where [SystemID]='{0}' and [Status] <> '{0}0201213000003'", Framework.SystemID);

            /*先定义Name和Code，默认给DbNull,以免参数找不到*/
            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@Code",SqlDbType.VarChar)
            };
            parameters[0].Value = DBNull.Value;

            if (!string.IsNullOrWhiteSpace(Code))
            {
                sql = sql + String.Format(@" and [Code] =@Code ");
                parameters[0].Value = Code;
            }

            /*EquipmentMaintenanceListID（参数流水号）用于在更新时，排除他自己*/
            if (!string.IsNullOrWhiteSpace(EquipmentMaintenanceListID))
                sql = sql + String.Format(@" and [EquipmentMaintenanceListID] <> '{0}' ", EquipmentMaintenanceListID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text, parameters);

            if (dt.Rows.Count == 0)
                return false;
            else
                return true;
        }

        /// <summary>
        /// 根据代号获取实体
        /// SAM 2017年7月5日16:25:07
        /// </summary>
        /// <param name="Code"></param>
        /// <returns></returns>
        public static EMS_EquipmentMaintenanceList getByCode(string Code)
        {
            string sql = string.Format(@"select Top 1 * from [EMS_EquipmentMaintenanceList] where [Code] = '{0}' and [SystemID] = '{1}'  and [Status]='{1}0201213000001' ", Code, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return null;
            else
                return ToEntity(dt.Rows[0]);
        }


        /// <summary>
        /// 设备保养清单设定列表
        /// SAM 2017年7月5日15:00:28
        /// </summary>
        /// <param name="Code"></param>
        /// <param name="Name"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> Ems00008GetList(string Code, string Name, int page, int rows, ref int count)
        {
            string select = string.Format(@"select A.EquipmentMaintenanceListID,A.Code,A.Name,A.Status,A.Comments,A.Type,
            D.Code as TypeCode,D.Name as TypeName,
           (CASE WHEN B.Emplno is null or B.Emplno = '' THEN B.Account else B.Emplno END)+'-'+B.UserName as Creator,A.CreateLocalTime as CreateTime,
           (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else (CASE WHEN C.Emplno is null or C.Emplno = '' THEN C.Account else C.Emplno END)+'-'+C.UserName END) as Modifier,
           (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ");

            string sql = string.Format(@" from [EMS_EquipmentMaintenanceList] A 
            left join [SYS_MESUsers] B on A.Creator = B.MESUserID
            left join [SYS_MESUsers] C on A.Modifier = C.MESUserID
            left join [SYS_Parameters] D on A.Type = D.ParameterID
            where A.[SystemID]='{0}' and  A.[Status] <> '{0}0201213000003' ", Framework.SystemID);

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
                Code = "%" + Code + "%";
                sql = sql + string.Format(@" and A.[Code] like @Code ");
                parameters[0].Value = Code;
                Parcount[0].Value = Code;
            }

            if (!string.IsNullOrWhiteSpace(Name))
            {
                Name = "%" + Name + "%";
                sql = sql + string.Format(@" and A.[Name] like @Name ");
                parameters[1].Value = Name;
                Parcount[1].Value = Name;
            }

            count = UniversalService.getCount(sql, Parcount);

            string orderby = "A.[Status],A.[Code] ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, parameters, page, rows);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// 设备保养清单设定导出
        /// SAM 2017年7月5日16:33:52
        /// </summary>
        /// <param name="Code"></param>
        /// <param name="Name"></param>
        /// <returns></returns>
        public static DataTable Ems00008Export(string Code, string Name)
        {
            string select = string.Format(@"select ROW_NUMBER() OVER (ORDER BY A.[Status],A.[Code]),
            A.Code,A.Name,
            (Select [Code] From [SYS_Parameters] Where ParameterID = A.Type) as Type,
            (Select [Code] From [SYS_Parameters] Where ParameterID = B.DetailID) as ProjectCode,
            (Select [Name] From [SYS_Parameters] Where ParameterID = B.DetailID) as ProjectName,
            (Select [Name] From [SYS_Parameters] Where ParameterID =(Select [Description] From [SYS_Parameters] Where ParameterID = B.DetailID)) as Attribute,
            (Select [Comments] From [SYS_Parameters] Where ParameterID = B.DetailID) as Comments  ");

            string sql = string.Format(@" from [EMS_EquipmentMaintenanceList] A 
             left join [EMS_EquipmentMaintenanceListDetails] B on A.[EquipmentMaintenanceListID] = B.[EquipmentMaintenanceListID] and B.[Type] = 1 and B.[Status] <> '{0}0201213000003' 
            where A.[SystemID]='{0}' and A.[Status] <> '{0}0201213000003' ", Framework.SystemID);

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
                Code = "%" + Code + "%";
                sql = sql + String.Format(@" and A.[Code] like @Code ");
                parameters[0].Value = Code;
                Parcount[0].Value = Code;
            }

            if (!string.IsNullOrWhiteSpace(Name))
            {
                Name = "%" + Name + "%";
                sql = sql + String.Format(@" and A.[Name] like @Name ");
                parameters[1].Value = Name;
                Parcount[1].Value = Name;
            }

            string orderBy = "order By A.[Status],A.[Code] ";

            return SQLHelper.ExecuteDataTable(select + sql + orderBy, CommandType.Text, parameters);
        }


        /// <summary>
        /// 根据保养类型获取保养清单
        /// SAM 2017年7月9日16:26:56
        /// </summary>
        /// <param name="Type"></param>
        /// <returns></returns>
        public static IList<Hashtable> Ems00009GetEquMaiList(string Type)
        {
            string select = string.Format(@"select A.EquipmentMaintenanceListID,A.Code,A.Name,A.Status,A.Comments,A.Type ");

            string sql = string.Format(@" from [EMS_EquipmentMaintenanceList] A 
            where A.[SystemID]='{0}' and  A.[Status] ='{0}0201213000001' and A.[Type] = '{1}'", Framework.SystemID, Type);

            string orderby = " order by A.[Code] ";

            DataTable dt = SQLHelper.ExecuteDataTable(select+sql+orderby, CommandType.Text);

            return ToHashtableList(dt);
        }


        /// <summary>
        /// 判断保养清单中是否存在保养类型设定
        /// SAM 2017年7月19日11:21:56
        /// </summary>
        /// <param name="Type"></param>
        /// <returns></returns>
        public static bool CheckType(string Type)
        {
            string sql = string.Format(@"select * from [EMS_EquipmentMaintenanceList] where [Type] = '{0}' and [SystemID] = '{1}' and [Status] <> '{1}0201213000003'", Type, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return false;
            else
                return true;
        }
    }
}

