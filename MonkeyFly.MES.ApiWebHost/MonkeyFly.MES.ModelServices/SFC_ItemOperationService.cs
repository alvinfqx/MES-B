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
    public class SFC_ItemOperationService : SuperModel<SFC_ItemOperation>
    {
        /// <summary>
        /// 新增
        /// SAM 2017年6月22日16:44:56
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="Model"></param>
        /// <returns></returns>
        public static bool insert(string userId, SFC_ItemOperation Model)
        {
            try
            {
                string sql = string.Format(@"insert[SFC_ItemOperation]([ItemOperationID],[ItemProcessID],[Sequence],
                [OperationID],[ProcessID],[WorkCenterID],[Unit],[UnitRatio],[StandardTime],[PrepareTime],
                [IsIP],[IsFPI],[IsOSI],[InspectionGroupID],[Status],[Comments],[Modifier],[ModifiedTime],
                [ModifiedLocalTime],[Creator],[CreateTime],[CreateLocalTime],[SystemID]) values
                 (@ItemOperationID,@ItemProcessID,@Sequence,@OperationID,@ProcessID,@WorkCenterID,@Unit,@UnitRatio,
                @StandardTime,@PrepareTime,@IsIP,@IsFPI,@IsOSI,@InspectionGroupID,@Status,@Comments,'{0}','{1}','{2}','{0}','{1}','{2}','{3}')", userId, DateTime.Now, DateTime.Now, Framework.SystemID);

                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@ItemOperationID",SqlDbType.VarChar),
                    new SqlParameter("@ItemProcessID",SqlDbType.VarChar),
                    new SqlParameter("@Sequence",SqlDbType.VarChar),
                    new SqlParameter("@OperationID",SqlDbType.VarChar),
                    new SqlParameter("@ProcessID",SqlDbType.VarChar),
                    new SqlParameter("@WorkCenterID",SqlDbType.VarChar),
                    new SqlParameter("@Unit",SqlDbType.VarChar),
                    new SqlParameter("@UnitRatio",SqlDbType.Decimal),
                    new SqlParameter("@StandardTime",SqlDbType.Int),
                    new SqlParameter("@PrepareTime",SqlDbType.Int),
                    new SqlParameter("@IsIP",SqlDbType.Bit),
                    new SqlParameter("@IsFPI",SqlDbType.Bit),
                    new SqlParameter("@IsOSI",SqlDbType.Bit),
                    new SqlParameter("@InspectionGroupID",SqlDbType.VarChar),
                    new SqlParameter("@Status",SqlDbType.VarChar),
                    new SqlParameter("@Comments",SqlDbType.NVarChar),
                    };

                parameters[0].Value = (Object)Model.ItemOperationID ?? DBNull.Value;
                parameters[1].Value = (Object)Model.ItemProcessID ?? DBNull.Value;
                parameters[2].Value = (Object)Model.Sequence ?? DBNull.Value;
                parameters[3].Value = (Object)Model.OperationID ?? DBNull.Value;
                parameters[4].Value = (Object)Model.ProcessID ?? DBNull.Value;
                parameters[5].Value = (Object)Model.WorkCenterID ?? DBNull.Value;
                parameters[6].Value = (Object)Model.Unit ?? DBNull.Value;
                parameters[7].Value = (Object)Model.UnitRatio ?? DBNull.Value;
                parameters[8].Value = (Object)Model.StandardTime ?? DBNull.Value;
                parameters[9].Value = (Object)Model.PrepareTime ?? DBNull.Value;
                parameters[10].Value = (Object)Model.IsIP ?? DBNull.Value;
                parameters[11].Value = (Object)Model.IsFPI ?? DBNull.Value;
                parameters[12].Value = (Object)Model.IsOSI ?? DBNull.Value;
                parameters[13].Value = (Object)Model.InspectionGroupID ?? DBNull.Value;
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
        /// 编辑
        /// SAM 2017年6月22日16:45:01
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="Model"></param>
        /// <returns></returns>
        public static bool update(string userId, SFC_ItemOperation Model)
        {
            try
            {
                string sql = String.Format(@"update[SFC_ItemOperation] set {0},
            [Unit]=@Unit,[UnitRatio]=@UnitRatio,
            [StandardTime]=@StandardTime,[PrepareTime]=@PrepareTime,[IsIP]=@IsIP,[IsFPI]=@IsFPI,
            [IsOSI]=@IsOSI,[InspectionGroupID]=@InspectionGroupID,[Status]=@Status,[Comments]=@Comments 
            where [ItemOperationID]=@ItemOperationID", UniversalService.getUpdateUTC(userId));
                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@ItemOperationID",SqlDbType.VarChar),
                    new SqlParameter("@Unit",SqlDbType.VarChar),
                    new SqlParameter("@UnitRatio",SqlDbType.Decimal),
                    new SqlParameter("@StandardTime",SqlDbType.Int),
                    new SqlParameter("@PrepareTime",SqlDbType.Int),
                    new SqlParameter("@IsIP",SqlDbType.Bit),
                    new SqlParameter("@IsFPI",SqlDbType.Bit),
                    new SqlParameter("@IsOSI",SqlDbType.Bit),
                    new SqlParameter("@InspectionGroupID",SqlDbType.VarChar),
                    new SqlParameter("@Status",SqlDbType.VarChar),
                    new SqlParameter("@Comments",SqlDbType.NVarChar),
                    };

                parameters[0].Value = Model.ItemOperationID;
                parameters[1].Value = (Object)Model.Unit ?? DBNull.Value;
                parameters[2].Value = (Object)Model.UnitRatio ?? DBNull.Value;
                parameters[3].Value = (Object)Model.StandardTime ?? DBNull.Value;
                parameters[4].Value = (Object)Model.PrepareTime ?? DBNull.Value;
                parameters[5].Value = (Object)Model.IsIP ?? DBNull.Value;
                parameters[6].Value = (Object)Model.IsFPI ?? DBNull.Value;
                parameters[7].Value = (Object)Model.IsOSI ?? DBNull.Value;
                parameters[8].Value = (Object)Model.InspectionGroupID ?? DBNull.Value;
                parameters[9].Value = (Object)Model.Status ?? DBNull.Value;
                parameters[10].Value = (Object)Model.Comments ?? DBNull.Value;

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
        /// SAM 2017年6月22日16:45:13
        /// </summary>
        /// <param name="ItemOperationID"></param>
        /// <returns></returns>
        public static SFC_ItemOperation get(string ItemOperationID)
        {
            string sql = string.Format(@"select Top 1 * from [SFC_ItemOperation] where [ItemOperationID] = '{0}'  and [SystemID] = '{1}' ", ItemOperationID, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return null;
            else
                return ToEntity(dt.Rows[0]);
        }

        /// <summary>
        /// 根据制品制程流水号+工序流水号获取制品制程工序实体
        /// SAM 2017年9月5日14:44:43
        /// </summary>
        /// <param name="ItemProcessID"></param>
        /// <param name="OperationID"></param>
        /// <returns></returns>
        public static SFC_ItemOperation getByItemProcess(string ItemProcessID, string OperationID)
        {
            string sql = string.Format(@"select Top 1 * from [SFC_ItemOperation] where [ItemProcessID] = '{0}' and  [OperationID]='{1}' and [SystemID] = '{2}' and [Status] ='{2}0201213000001' ", ItemProcessID, OperationID, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return null;
            else
                return ToEntity(dt.Rows[0]);
        }

        /// <summary>
        /// 检测工序是否被使用
        /// Joint 2017年7月26日11:20:22
        /// </summary>
        /// <param name="ParameterID"></param>
        /// <returns></returns>
        public static SFC_ItemOperation checkOperation(string ParameterID)
        {
            string sql = string.Format(@"select Top 1 * from [SFC_ItemOperation] where [OperationID] = '{0}'  and [SystemID] = '{1}' ", ParameterID, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return null;
            else
                return ToEntity(dt.Rows[0]);
        }
        /// <summary>
        /// 判断工序是否重复
        /// SAM 2017年6月22日17:29:47
        /// </summary>
        /// <param name="ProcessID"></param>
        /// <param name="ItemProcessID"></param>
        /// <param name="IPARSID"></param>
        /// <returns></returns>
        public static bool CheckOperation(string OperationID, string ItemProcessID, string ItemOperationID)
        {
            string sql = String.Format(@"select Top 1 * from [SFC_ItemOperation] where [SystemID]='{0}' and [Status] <> '{0}0201213000003' ", Framework.SystemID);

            if (!string.IsNullOrWhiteSpace(OperationID))
                sql = sql + string.Format(@" and [OperationID]= '{0}' ", OperationID);

            if (!string.IsNullOrWhiteSpace(ItemProcessID))
                sql = sql + string.Format(@" and [ItemProcessID]= '{0}' ", ItemProcessID);

            /*ItemOperationID（参数流水号）用于在更新时，排除他自己*/
            if (!string.IsNullOrWhiteSpace(ItemOperationID))
                sql = sql + string.Format(@" and [ItemOperationID] <> '{0}' ", ItemOperationID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text, null);

            if (dt.Rows.Count == 0)
                return false;
            else
                return true;
        }
        

        /// <summary>
        /// 判断序号是否重复
        /// SAM 2017年6月22日17:34:22
        /// </summary>
        /// <param name="OperationID"></param>
        /// <param name="ItemProcessID"></param>
        /// <param name="ItemOperationID"></param>
        /// <returns></returns>
        public static bool CheckSequence(string Sequence, string ItemProcessID, string ItemOperationID)
        {
            string sql = String.Format(@"select Top 1 * from [SFC_ItemOperation] where [SystemID]='{0}' and [Status] <> '{0}0201213000003' ", Framework.SystemID);

            if (!string.IsNullOrWhiteSpace(Sequence))
                sql = sql + string.Format(@" and [Sequence]= '{0}' ", Sequence);

            if (!string.IsNullOrWhiteSpace(ItemProcessID))
                sql = sql + string.Format(@" and [ItemProcessID]= '{0}' ", ItemProcessID);

            /*ItemOperationID（参数流水号）用于在更新时，排除他自己*/
            if (!string.IsNullOrWhiteSpace(ItemOperationID))
                sql = sql + string.Format(@" and [ItemOperationID] <> '{0}' ", ItemOperationID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text, null);

            if (dt.Rows.Count == 0)
                return false;
            else
                return true;
        }


        /// <summary>
        /// 根据制品制程获取制品工序列表
        /// SAM  2017年6月22日16:51:16
        /// </summary>
        /// <param name="ItemProcessID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> Sfc00001GetItemOperationList(string ItemProcessID, int page, int rows, ref int count)
        {
            string select = string.Format(@"select A.ItemOperationID,A.ItemProcessID,A.Sequence,A.ProcessID,A.OperationID,A.WorkCenterID,
            A.Unit,A.UnitRatio,A.Status,A.Comments,A.StandardTime,A.StandardTime as StandardHour,A.PrepareTime,A.PrepareTime as PrepareHour,
            A.IsIP,A.IsFPI,A.IsOSI,A.InspectionGroupID,
            F.Name as UtilName, F.Code as UtilCode,
            D.Code as OperationCode,D.Name as OperationName,
            E.Code as GroupCode,E.Name as GroupName,
            B.Emplno+'-'+B.UserName as Creator,A.CreateLocalTime as CreateTime,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else C.Emplno+'-'+C.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ", Framework.SystemID);

            string sql = string.Format(@" from [SFC_ItemOperation] A 
            left join [SYS_MESUsers] B on A.Creator = B.MESUserID
            left join [SYS_MESUsers] C on A.Modifier = C.MESUserID
            left join [SYS_Parameters] D on A.OperationID  =D.ParameterID
            left join [SYS_Parameters] F on A.Unit  =F.ParameterID
            left join [SYS_Parameters] E on A.InspectionGroupID  =E.ParameterID
            where A.[SystemID]='{0}' and A.Status= '{0}0201213000001' and A.[ItemProcessID]='{1}' ", Framework.SystemID, ItemProcessID);

            count = UniversalService.getCount(sql, null);

            string orderby = "A.[Sequence] ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, null, page, rows);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// 制品制程工序专属开窗
        /// SAM 2017年6月27日22:21:42
        /// </summary>
        /// <param name="ItemProcessID"></param>
        /// <param name="Code"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> Sfc00001GetProcessOperationList(string ItemProcessID,string Code, int page, int rows, ref int count)
        {
            string select = string.Format(@"select A.ItemOperationID,A.ItemProcessID,A.Sequence,A.ProcessID,A.OperationID,A.WorkCenterID,
            A.Unit,A.UnitRatio,I.Name as Status,A.Comments,
            A.StandardTime,A.StandardTime as StandardHour,A.PrepareTime,A.PrepareTime as PrepareHour,
            A.IsIP,A.IsFPI,A.IsOSI,A.InspectionGroupID,
            F.Name as UtilName, F.Code as UtilCode,
            D.Code as OperationCode,D.Name as OperationName,
            E.Code as GroupCode,E.Name as GroupName,
            B.Emplno+'-'+B.UserName as Creator,A.CreateLocalTime as CreateTime,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else C.Emplno+'-'+C.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ", Framework.SystemID);

            string sql = string.Format(@" from [SFC_ItemOperation] A 
            left join [SYS_MESUsers] B on A.Creator = B.MESUserID
            left join [SYS_MESUsers] C on A.Modifier = C.MESUserID
            left join [SYS_Parameters] D on A.OperationID  =D.ParameterID
            left join [SYS_Parameters] F on A.Unit  =F.ParameterID
            left join [SYS_Parameters] E on A.InspectionGroupID  =E.ParameterID
            left join [SYS_Parameters] I on A.Status = I.ParameterID
            where A.[SystemID]='{0}' and A.Status= '{0}0201213000001' and A.[ItemProcessID]='{1}' ", Framework.SystemID, ItemProcessID);

            if (!string.IsNullOrWhiteSpace(Code))
                sql += string.Format(@"  and D.Code like '%{0}%' ", Code);

            count = UniversalService.getCount(sql, null);

            string orderby = "A.[Sequence] ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, null, page, rows);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// 判断指定制程下，是否存在没有关系的工序
        /// SAM 2017年6月29日14:24:29
        /// </summary>
        /// <param name="ItemID"></param>
        /// <returns></returns>
        public static bool CheckItemProcessOperationRelationShip(string ItemProcessID)
        {
            string sql = string.Format(@"select * from [SFC_ItemOperation] 
            where [ItemProcessID] = '{0}'  and [SystemID] = '{1}' 
            and [ItemOperationID] not in (Select [ItemOperationID] from [SFC_ProcessOperationRelationShip] where [ItemProcessID] = '{0}' and [Status]='{1}0201213000001') 
            and [ItemOperationID] not in (Select [PreItemOperationID] from [SFC_ProcessOperationRelationShip] where [ItemProcessID] = '{0}' and [Status]='{1}0201213000001') 
            ", ItemProcessID, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return false;
            else
                return true;
        }

        /// <summary>
        /// 根据制程删除制程工序
        /// SAM 2017年6月29日14:58:06
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ItemProcessID"></param>
        /// <returns></returns>
        public static bool DeleteByProcess(string userId, string ItemProcessID)
        {
            try
            {
                string sql = String.Format(@"update[SFC_ItemOperation] set {0},[Status]='{1}0201213000003'
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
        /// 根据制程获取制程工序列表
        /// SAM 2017年7月13日23:05:20
        /// </summary>
        /// <param name="ItemProcessID"></param>
        /// <returns></returns>
        public static IList<SFC_ItemOperation> GetOperationList(string ItemProcessID)
        {
            string sql = string.Format(
                @"select * from [SFC_ItemOperation]        
                  where [SystemID] = '{0}' and [Status] = '{0}0201213000001' and [ItemProcessID] ='{1}'", Framework.SystemID, ItemProcessID);

            string orderby = " order by [Sequence]";

            DataTable dt = SQLHelper.ExecuteDataTable(sql + orderby, CommandType.Text);

            return ToList(dt);
        }



        /// <summary>
        /// 根据制程获取制程工序列表
        /// SAN 2017年7月27日11:38:19
        /// </summary>
        /// <param name="FabMoProcessID"></param>
        /// <returns></returns>
        public static IList<Hashtable> GetBomList(string ItemProcessID)
        {
            string sql = string.Format(
                @"select A.ItemOperationID,A.ItemProcessID as Parenter,A.ItemProcessID+A.ItemOperationID as ItemProcessID,A.Sequence,B.WorkCenterID,
                convert(varchar(20),A.Sequence)+' '+(select [Code] from [SYS_Parameters] where A.[OperationID] = [ParameterID])+' '+(select [Name] from [SYS_Parameters] where A.[OperationID] = [ParameterID]) as Value
                  from [SFC_ItemOperation] A
                  left Join [SFC_ItemProcess] B on A.[ItemProcessID] = B.[ItemProcessID]
                  where A.[SystemID] = '{0}' and A.[Status] <> '{0}0201213000003' and A.[ItemProcessID] ='{1}'", Framework.SystemID, ItemProcessID);

            string orderby = " order by [Sequence]";

            DataTable dt = SQLHelper.ExecuteDataTable(sql + orderby, CommandType.Text);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// 根据制程+群码，更新群码下料品对应的制程的检验注记
        /// Sam 2017年10月19日11:05:12
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="OperationID"></param>
        /// <param name="GroupID"></param>
        /// <param name="FieldName"></param>
        /// <param name="IsInspection"></param>
        /// <returns></returns>
        public static bool Qcs04Update(string userId, string OperationID,string ProcessID, string GroupID, string FieldName, bool IsInspection)
        {
            string sql = null;
            try
            {
                sql = String.Format(@"update [SFC_ItemOperation] set {0},[InspectionGroupID]='{4}',
                [{1}]='{2}' where [OperationID]='{3}' 
                and [ItemProcessID] in (select [ItemProcessID] from [SFC_ItemProcess] where [ProcessID]='{4}'
                and [ItemID] in (select [ItemID] from [SYS_Items] where [GroupID]='{5}') and [Status] <> '{6}0201213000003')",
                UniversalService.getUpdateUTC(userId), FieldName, IsInspection, OperationID, ProcessID, GroupID,Framework.SystemID);


                return SQLHelper.ExecuteNonQuery(sql, CommandType.Text) > 0;
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(new Exception(sql));
                DataLogerService.writeerrlog(ex);
                return false;
            }
        }

        /// <summary>
        /// 根据制品制程工序，更新指定注记
        /// SAM 2017年10月19日20:51:28
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ItemOperationID"></param>
        /// <param name="FieldName"></param>
        /// <param name="IsInspection"></param>
        /// <returns></returns>
        public static bool Qcs04ItemUpdate(string userId, string ItemOperationID, string FieldName, bool IsInspection)
        {
            string sql = null;
            try
            {
                sql = String.Format(@"update [SFC_ItemOperation] set {0},[{1}]='{2}' where [ItemOperationID]='{3}'",
                UniversalService.getUpdateUTC(userId), FieldName, IsInspection, ItemOperationID);

                return SQLHelper.ExecuteNonQuery(sql, CommandType.Text) > 0;
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(new Exception(sql));
                DataLogerService.writeerrlog(ex);
                return false;
            }
        }

        /// <summary>
        /// 判断制程+工序+检验群码对应的制品制程工序中，指定的注记是否为Y或者N
        /// </summary>
        /// <param name="OperationID"></param>
        /// <param name="ProcessID"></param>
        /// <param name="GroupID"></param>
        /// <param name="FieldName"></param>
        /// <param name="IsInspection">1:Y  0:N</param>
        /// <returns></returns>
        public static bool Qcs04CheckYorN(string OperationID, string ProcessID, string GroupID, string FieldName, int IsInspection)
        {
            string sql = String.Format(@" select * from  [SFC_ItemOperation] where [OperationID]='{0}'  and [{2}]={3}
           and [ItemProcessID] in (select [ItemProcessID] from [SFC_ItemProcess] where [ProcessID]='{1}' 
            and [ItemID] in (select [ItemID] from [SYS_Items] where [GroupID]='{4}') and [Status] <> '{5}0201213000003')", OperationID, ProcessID,  FieldName, IsInspection, GroupID,Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return false;

            return true;
        }

        /// <summary>
        /// 判断对应检验群码+制程+工序下，是否存在对应制程工序的制品制程工序
        /// SAM 2017年10月20日15:19:14
        /// </summary>
        /// <param name="GroupID"></param>
        /// <param name="ProcessID"></param>
        /// <param name="OperationID"></param>
        /// <returns></returns>
        public static bool Qcs04GroupCheck(string GroupID, string ProcessID, string OperationID)
        {
            string sql = String.Format(@" select * from  [SFC_ItemOperation] where [OperationID]='{0}' and [Status] = '{3}0201213000001' and [SystemID]='{3}'
           and [ItemProcessID] in (select [ItemProcessID] from [SFC_ItemProcess] where [ProcessID]='{1}' 
            and [ItemID] in (select [ItemID] from [SYS_Items] where [GroupID]='{2}' and [Status] = '{3}0201213000001' and [SystemID]='{3}') and [Status] = '{3}0201213000001')", OperationID, ProcessID, GroupID, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return false;

            return true;
        }

        /// <summary>
        /// 获取所有的正常的制程工序列表
        /// SAM 2017年10月6日13:57:40
        /// </summary>
        /// <returns></returns>
        public static IList<SFC_ItemOperation> GetList()
        {
            string sql = string.Format(
                @"select * from [SFC_ItemOperation]       
                  where [SystemID] = '{0}' and [Status] = '{0}0201213000001' ", Framework.SystemID);

            string orderby = " order by [Sequence]";

            DataTable dt = SQLHelper.ExecuteDataTable(sql + orderby, CommandType.Text);

            return ToList(dt);
        }
    }
}

