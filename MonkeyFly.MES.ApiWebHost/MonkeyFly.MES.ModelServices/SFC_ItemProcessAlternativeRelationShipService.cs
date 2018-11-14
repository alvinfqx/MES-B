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
    public class SFC_ItemProcessAlternativeRelationShipService : SuperModel<SFC_ItemProcessAlternativeRelationShip>
    {
        /// <summary>
        /// 新增
        /// SAM 2017年6月22日15:56:00
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="Model"></param>
        /// <returns></returns>
        public static bool insert(string userId, SFC_ItemProcessAlternativeRelationShip Model)
        {
            try
            {
                string sql = string.Format(@"insert[SFC_ItemProcessAlternativeRelationShip]([IPARSID],[ItemProcessID],
            [Sequence],[ProcessID],[WorkCenterID],[Unit],[UnitRatio],
            [Price],[Status],[Comments],[Modifier],[ModifiedTime],[ModifiedLocalTime],[Creator],[CreateTime],[CreateLocalTime],[SystemID]) values
             (@IPARSID,@ItemProcessID,@Sequence,@ProcessID,@WorkCenterID,
            @Unit,@UnitRatio,@Price,@Status,@Comments,'{0}','{1}','{2}','{0}','{1}','{2}','{3}')", userId, DateTime.Now, DateTime.Now, Framework.SystemID);

                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@IPARSID",SqlDbType.VarChar),
                    new SqlParameter("@ItemProcessID",SqlDbType.VarChar),
                    new SqlParameter("@Sequence",SqlDbType.VarChar),
                    new SqlParameter("@ProcessID",SqlDbType.VarChar),
                    new SqlParameter("@WorkCenterID",SqlDbType.VarChar),
                    new SqlParameter("@Unit",SqlDbType.VarChar),
                    new SqlParameter("@UnitRatio",SqlDbType.Decimal),
                    new SqlParameter("@Price",SqlDbType.Decimal),
                    new SqlParameter("@Status",SqlDbType.VarChar),
                    new SqlParameter("@Comments",SqlDbType.NVarChar),
                    };

                parameters[0].Value = (Object)Model.IPARSID ?? DBNull.Value;
                parameters[1].Value = (Object)Model.ItemProcessID ?? DBNull.Value;
                parameters[2].Value = (Object)Model.Sequence ?? DBNull.Value;
                parameters[3].Value = (Object)Model.ProcessID ?? DBNull.Value;
                parameters[4].Value = (Object)Model.WorkCenterID ?? DBNull.Value;
                parameters[5].Value = (Object)Model.Unit ?? DBNull.Value;
                parameters[6].Value = (Object)Model.UnitRatio ?? DBNull.Value;
                parameters[7].Value = (Object)Model.Price ?? DBNull.Value;
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
        /// 更新
        /// SAM 2017年6月22日15:55:45
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="Model"></param>
        /// <returns></returns>
        public static bool update(string userId, SFC_ItemProcessAlternativeRelationShip Model)
        {
            try
            {
                string sql = String.Format(@"update[SFC_ItemProcessAlternativeRelationShip] set {0},
                [Sequence]=@Sequence,[ProcessID]=@ProcessID,
                [WorkCenterID]=@WorkCenterID,[Unit]=@Unit,[UnitRatio]=@UnitRatio,[Price]=@Price,
                [Status]=@Status,[Comments]=@Comments where [IPARSID]=@IPARSID", UniversalService.getUpdateUTC(userId));
                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@IPARSID",SqlDbType.VarChar),
                    new SqlParameter("@Sequence",SqlDbType.VarChar),
                    new SqlParameter("@ProcessID",SqlDbType.VarChar),
                    new SqlParameter("@WorkCenterID",SqlDbType.VarChar),
                    new SqlParameter("@Unit",SqlDbType.VarChar),
                    new SqlParameter("@UnitRatio",SqlDbType.Decimal),
                    new SqlParameter("@Price",SqlDbType.Decimal),
                    new SqlParameter("@Status",SqlDbType.VarChar),
                    new SqlParameter("@Comments",SqlDbType.NVarChar),
                    };

                parameters[0].Value = Model.IPARSID;
                parameters[1].Value = (Object)Model.Sequence ?? DBNull.Value;
                parameters[2].Value = (Object)Model.ProcessID ?? DBNull.Value;
                parameters[3].Value = (Object)Model.WorkCenterID ?? DBNull.Value;
                parameters[4].Value = (Object)Model.Unit ?? DBNull.Value;
                parameters[5].Value = (Object)Model.UnitRatio ?? DBNull.Value;
                parameters[6].Value = (Object)Model.Price ?? DBNull.Value;
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

        public static SFC_ItemProcessAlternativeRelationShip get(string IPARSID)
        {
            string sql = string.Format(@"select Top 1 * from [SFC_ItemProcessAlternativeRelationShip] where [IPARSID] = '{0}'  and [SystemID] = '{1}' ", IPARSID, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return null;
            else
                return ToEntity(dt.Rows[0]);
        }

        /// <summary>
        /// 判断是否重复
        /// SAM 2017年6月22日17:28:30
        /// </summary>
        /// <param name="ProcessID"></param>
        /// <param name="ItemProcessID"></param>
        /// <param name="IPARSID"></param>
        /// <returns></returns>
        public static bool Check(string ProcessID, string ItemProcessID, string IPARSID)
        {
            string sql = String.Format(@"select Top 1 * from [SFC_ItemProcessAlternativeRelationShip] where [SystemID]='{0}' and [Status] <> '{0}0201213000003' ", Framework.SystemID);

            if (!string.IsNullOrWhiteSpace(ProcessID))
                sql = sql + string.Format(@" and [ProcessID]= '{0}' ", ProcessID);

            if (!string.IsNullOrWhiteSpace(ItemProcessID))
                sql = sql + string.Format(@" and [ItemProcessID]= '{0}' ", ItemProcessID);

            /*IPARSID（参数流水号）用于在更新时，排除他自己*/
            if (!string.IsNullOrWhiteSpace(IPARSID))
                sql = sql + string.Format(@" and [IPARSID] <> '{0}' ", IPARSID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text, null);

            if (dt.Rows.Count == 0)
                return false;
            else
                return true;
        }


        /// <summary>
        /// 根据制品制程获取替代制程列表
        /// SAM 2017年6月22日16:10:24
        /// </summary>
        /// <param name="ItemProcessID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> Sfc00001GetLAlternativeRelationList(string ItemProcessID, int page, int rows, ref int count)
        {
            string select = string.Format(@"select A.IPARSID,A.ItemProcessID,A.Sequence,A.ProcessID,A.WorkCenterID,
            A.Unit,A.UnitRatio,Price,A.Status,A.Comments,
            D.Code as ProcessCode,D.Name as ProcessName,
            E.Code as WorkCenterCode,E.Name as WorkCenterName,F.Name as UtilName, F.Code as UtilCode,
            (Select Name from [SYS_Parameters] where ParameterID = E.InoutMark) as InoutMark,
            (CASE WHEN E.InoutMark='{0}020121300002E' THEN (Select [Code] from [SYS_Organization] where [OrganizationID] = E.DepartmentID) ELSE (Select [Code] from [SYS_Manufacturers] where [ManufacturerID] = E.DepartmentID) END) as DeptCode,
            (CASE WHEN E.InoutMark='{0}020121300002E' THEN (Select [Code] from [SYS_Organization] where [OrganizationID] = E.DepartmentID) ELSE (Select [Code] from [SYS_Manufacturers] where [ManufacturerID] = E.DepartmentID) END) as DeptName, 
            B.Emplno+'-'+B.UserName as Creator,A.CreateLocalTime as CreateTime,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else C.Emplno+'-'+C.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ", Framework.SystemID);

            string sql = string.Format(@" from [SFC_ItemProcessAlternativeRelationShip] A 
            left join [SYS_MESUsers] B on A.Creator = B.MESUserID
            left join [SYS_MESUsers] C on A.Modifier = C.MESUserID
            left join [SYS_Parameters] D on A.ProcessID  =D.ParameterID
            left join [SYS_WorkCenter] E on A.WorkCenterID  =E.WorkCenterID
            left join [SYS_Parameters] F on A.Unit  =F.ParameterID
            where A.[SystemID]='{0}' and A.Status= '{0}0201213000001' and A.[ItemProcessID]='{1}' ", Framework.SystemID, ItemProcessID);

            count = UniversalService.getCount(sql, null);

            string orderby = "A.[Sequence] ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, null, page, rows);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// 删除制程的替代制程
        /// SAM 2017年6月29日14:55:13
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ItemProcessID"></param>
        /// <returns></returns>
        public static bool Delete(string userId, string ItemProcessID)
        {
            try
            {
                string sql = String.Format(@"update[SFC_ItemProcessAlternativeRelationShip] set {0},[Status]='{1}0201213000003'
                where [ItemProcessID]='{2}' ", UniversalService.getUpdateUTC(userId), Framework.SystemID, ItemProcessID);

                return SQLHelper.ExecuteNonQuery(sql, CommandType.Text, null) > 0;
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return false;
            }
        }

        /// <summary>
        /// 根据制品制程获取替代列表
        /// SAM 2017年7月17日14:26:16
        /// </summary>
        /// <param name="ItemProcessID"></param>
        /// <returns></returns>
        public static IList<SFC_ItemProcessAlternativeRelationShip> GetShipList(string ItemProcessID)
        {
            string sql = string.Format(
                @"select * from [SFC_ItemProcessAlternativeRelationShip]        
                  where [SystemID] = '{0}' and [Status] = '{0}0201213000001'", Framework.SystemID);

            if (!string.IsNullOrWhiteSpace(ItemProcessID))
                sql += string.Format(@" and [ItemProcessID] ='{0}'", ItemProcessID);

            string orderby = " order by [Sequence]";

            DataTable dt = SQLHelper.ExecuteDataTable(sql + orderby, CommandType.Text);

            return ToList(dt);
        }



        /// <summary>
        /// 获取一个制令制程的替代关系列表
        /// SAM 2017年7月13日14:16:00
        /// </summary>
        /// <param name="FabMoProcessID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> Sfc00002GetFabMoAltRelShipList(string ItemProcessID,  int page, int rows, ref int count)
        {
            string select = string.Format(
                @"select A.IPARSID,A.ItemProcessID, A.Sequence, 
                 A.ProcessID,E.Code as ProcessCode, E.Name as ProcessName, 
                 A.WorkCenterID, F.Code as WorkCenterCode, F.Name as WorkCenterName, 
                (Select Name from [SYS_Parameters] where ParameterID = F.InoutMark) as InoutMark,
                (CASE WHEN F.InoutMark='{0}020121300002E' THEN (Select [Code] from [SYS_Organization] where [OrganizationID] = F.DepartmentID) ELSE (Select [Code] from [SYS_Manufacturers] where [ManufacturerID] = F.DepartmentID) END) as DeptCode,
                (CASE WHEN F.InoutMark='{0}020121300002E' THEN (Select [Code] from [SYS_Organization] where [OrganizationID] = F.DepartmentID) ELSE (Select [Code] from [SYS_Manufacturers] where [ManufacturerID] = F.DepartmentID) END) as DeptName, 
                A.[Price],A.Unit,A.UnitRatio,G.Name as UtilName, G.Code as UtilCode,A.Comments,
                B.Emplno+'-'+B.UserName as Creator,A.CreateLocalTime as CreateTime,
                (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else C.Emplno+'-'+C.UserName END) as Modifier,
                (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ",
                Framework.SystemID);

            string sql = string.Format(
                @"from [SFC_ItemProcessAlternativeRelationShip] A
                 left join [SYS_MESUsers] B on A.[Creator] = B.[MESUserID]
                 left join [SYS_MESUsers] C on A.[Modifier] =C.[MESUserID]
                 left join [SYS_Parameters] E on A.[ProcessID] = E.[ParameterID]
                 left join [SYS_WorkCenter] F on A.[WorkCenterID] = F.[WorkCenterID]
                 left join [SYS_Parameters] G on A.[Unit] = G.[ParameterID]
                  where A.[SystemID] = '{0}' and  A.[ItemProcessID] = '{1}' and
                        A.Status = '{0}0201213000001'", Framework.SystemID, ItemProcessID);

            string orderby = "E.[Code] ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, null, page, rows);

            return ToHashtableList(dt);
        }
    }
}

