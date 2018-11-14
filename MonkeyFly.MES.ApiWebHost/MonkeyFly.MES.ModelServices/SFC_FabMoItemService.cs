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
    public class SFC_FabMoItemService : SuperModel<SFC_FabMoItem>
    {
        /// <summary>
        /// 新增
        /// sam 2017年7月13日20:20:09
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="Model"></param>
        /// <returns></returns>
        public static bool insert(string userId, SFC_FabMoItem Model)
        {
            try
            {
                string sql = string.Format(@"insert[SFC_FabMoItem]([FabMoItemID],[FabMoProcessID],[FabMoOperationID],[Sequence],
                [ItemID],[BaseQuantity],[AttritionRate],[UseQuantity],[ActualQuantity],[Crityn],[Status],
                [Comments],[Modifier],[ModifiedTime],[ModifiedLocalTime],[Creator],[CreateTime],[CreateLocalTime],[SystemID]) values
                 (@FabMoItemID,@FabMoProcessID,
                @FabMoOperationID,@Sequence,@ItemID,
                @BaseQuantity,@AttritionRate,@UseQuantity,
                @ActualQuantity,@Crityn,@Status,
                @Comments,'{0}','{1}','{2}','{0}','{1}','{2}','{3}')", userId, DateTime.Now, DateTime.Now, Framework.SystemID);

                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@FabMoItemID",SqlDbType.VarChar),
                    new SqlParameter("@FabMoProcessID",SqlDbType.VarChar),
                    new SqlParameter("@FabMoOperationID",SqlDbType.VarChar),
                    new SqlParameter("@Sequence",SqlDbType.VarChar),
                    new SqlParameter("@ItemID",SqlDbType.VarChar),
                    new SqlParameter("@BaseQuantity",SqlDbType.Decimal),
                    new SqlParameter("@AttritionRate",SqlDbType.Decimal),
                    new SqlParameter("@UseQuantity",SqlDbType.Decimal),
                    new SqlParameter("@ActualQuantity",SqlDbType.Decimal),
                    new SqlParameter("@Crityn",SqlDbType.Bit),
                    new SqlParameter("@Status",SqlDbType.VarChar),
                    new SqlParameter("@Comments",SqlDbType.NVarChar),
                    };

                parameters[0].Value = (Object)Model.FabMoItemID ?? DBNull.Value;
                parameters[1].Value = (Object)Model.FabMoProcessID ?? DBNull.Value;
                parameters[2].Value = (Object)Model.FabMoOperationID ?? DBNull.Value;
                parameters[3].Value = (Object)Model.Sequence ?? DBNull.Value;
                parameters[4].Value = (Object)Model.ItemID ?? DBNull.Value;
                parameters[5].Value = (Object)Model.BaseQuantity ?? DBNull.Value;
                parameters[6].Value = (Object)Model.AttritionRate ?? DBNull.Value;
                parameters[7].Value = (Object)Model.UseQuantity ?? DBNull.Value;
                parameters[8].Value = (Object)Model.ActualQuantity ?? DBNull.Value;
                parameters[9].Value = (Object)Model.Crityn ?? DBNull.Value;
                parameters[10].Value = (Object)Model.Status ?? DBNull.Value;
                parameters[11].Value = (Object)Model.Comments ?? DBNull.Value;

                return SQLHelper.ExecuteNonQuery(sql, CommandType.Text, parameters) > 0;
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return false;
            }
        }

        /// <summary>
        ///更新
        ///SAM 2017年7月13日20:20:27
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="Model"></param>
        /// <returns></returns>
        public static bool update(string userId, SFC_FabMoItem Model)
        {
            try
            {
                string sql = String.Format(@"update[SFC_FabMoItem] set {0},
                [Sequence]=@Sequence,[ItemID]=@ItemID,
            [BaseQuantity]=@BaseQuantity,[Status]=@Status,[AttritionRate]=@AttritionRate,[UseQuantity]=@UseQuantity,
            [ActualQuantity]=@ActualQuantity,[Crityn]=@Crityn,[Comments]=@Comments 
            where [FabMoItemID]=@FabMoItemID", UniversalService.getUpdateUTC(userId));
                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@FabMoItemID",SqlDbType.VarChar),
                    new SqlParameter("@Sequence",SqlDbType.VarChar),
                    new SqlParameter("@ItemID",SqlDbType.VarChar),
                    new SqlParameter("@BaseQuantity",SqlDbType.Decimal),
                    new SqlParameter("@Status",SqlDbType.VarChar),
                    new SqlParameter("@AttritionRate",SqlDbType.Decimal),
                    new SqlParameter("@UseQuantity",SqlDbType.Decimal),
                    new SqlParameter("@ActualQuantity",SqlDbType.Decimal),
                    new SqlParameter("@Crityn",SqlDbType.Bit),
                    new SqlParameter("@Comments",SqlDbType.NVarChar),
                    };

                parameters[0].Value = Model.FabMoItemID;
                parameters[1].Value = (Object)Model.Sequence ?? DBNull.Value;
                parameters[2].Value = (Object)Model.ItemID ?? DBNull.Value;
                parameters[3].Value = (Object)Model.BaseQuantity ?? DBNull.Value;
                parameters[4].Value = (Object)Model.Status ?? DBNull.Value;
                parameters[5].Value = (Object)Model.AttritionRate ?? DBNull.Value;
                parameters[6].Value = (Object)Model.UseQuantity ?? DBNull.Value;
                parameters[7].Value = (Object)Model.ActualQuantity ?? DBNull.Value;
                parameters[8].Value = (Object)Model.Crityn ?? DBNull.Value;
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
        /// 获取单一实体
        /// SAM 2017年7月13日20:20:34
        /// </summary>
        /// <param name="FabMoItemID"></param>
        /// <returns></returns>
        public static SFC_FabMoItem get(string FabMoItemID)
        {
            string sql = string.Format(@"select Top 1 * from [SFC_FabMoItem] where [FabMoItemID] = '{0}'  and [SystemID] = '{1}' ", FabMoItemID, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return null;
            else
                return ToEntity(dt.Rows[0]);
        }

        public static bool delete(string FabMoItemID)
        {
            try
            {
                string sql = string.Format(@"delete from [SFC_FabMoItem] where [FabMoItemID] = '{0}'  and [SystemID] = '{1}' ", FabMoItemID, Framework.SystemID);

                return SQLHelper.ExecuteNonQuery(sql, CommandType.Text) > 0;
            }

            catch (Exception ex)
            {

                DataLogerService.writeerrlog(ex);
                return false;
            }
        }

        public static bool delete(string uid, SFC_FabMoItem m)
        {
            try
            {
                string orgID = m.FabMoItemID;
                m = get(orgID);
                if (m != null)
                {
                    m.Status = Framework.SystemID + "0201213000003";
                    return update(uid, m);
                }
                return true;
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return false;
            }
        }

        public static bool CheckInsertArgs(SFC_FabMoItem model)
        {
            try
            {
                string sql = string.Format(
                    @"from SFC_FabMoItem
                  where FabMoProcessID = '{0}' and ItemID = '{1}' and Status <> '{2}0201213000003'",
                    model.FabMoProcessID, model.ItemID, Framework.SystemID);

                return UniversalService.getCount(sql, null) <= 0;
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return false;
            }
        }

        public static bool CheckUpdateArgs(SFC_FabMoItem model)
        {
            try
            {
                string sql = string.Format(
                @"from SFC_FabMoItem
                  where FabMoProcessID = '{0}' and ItemID = '{1}' and
                        FabMoItemID <> '{2}' and Status <> '{3}0201213000003'",
                model.FabMoProcessID, model.ItemID, model.FabMoItemID,
                Framework.SystemID);

                return UniversalService.getCount(sql, null) <= 0;
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return false;
            }
        }

        /// <summary>
        /// 工序判断用料是否重复
        /// SAM 2017年7月13日20:22:35
        /// </summary>
        /// <param name="ItemID"></param>
        /// <param name="FabMoOperationID"></param>
        /// <returns></returns>
        public static bool CheckOperation(string ItemID, string FabMoOperationID, string FabMoItemID)
        {
            string sql = String.Format(@"select Top 1 * from [SFC_FabMoItem] where [SystemID]='{0}' and Status <> '{0}0201213000003'  and [FabMoProcessID] is null ", Framework.SystemID);

            /*先定义ItemID，默认给DbNull,以免参数找不到*/
            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@ItemID",SqlDbType.VarChar)
            };
            parameters[0].Value = DBNull.Value;

            if (!string.IsNullOrWhiteSpace(ItemID))
            {
                sql = sql + String.Format(@" and [ItemID] =@ItemID ");
                parameters[0].Value = ItemID;
            }

            if (!string.IsNullOrWhiteSpace(FabMoOperationID))
                sql = sql + String.Format(@" and [FabMoOperationID] = '{0}' ", FabMoOperationID);

            /*FabMoItemID（流水号）用于在更新时，排除他自己*/
            if (!string.IsNullOrWhiteSpace(FabMoItemID))
                sql = sql + String.Format(@" and [FabMoItemID] <> '{0}' ", FabMoItemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text, parameters);

            if (dt.Rows.Count == 0)
                return false;
            else
                return true;
        }

        /// <summary>
        /// 判断重复性
        /// SAM 2017年9月6日14:40:56
        /// </summary>
        /// <param name="ItemID"></param>
        /// <param name="FabMoProcessID"></param>
        /// <param name="FabMoOperationID"></param>
        /// <param name="FabMoItemID"></param>
        /// <returns></returns>
        public static bool Check(string ItemID, string FabMoProcessID, string FabMoOperationID, string FabMoItemID)
        {
            string sql = String.Format(@"select Top 1 * from [SFC_FabMoItem] where [SystemID]='{0}' and Status <> '{0}0201213000003' ", Framework.SystemID);

            /*先定义ItemID，默认给DbNull,以免参数找不到*/
            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@ItemID",SqlDbType.VarChar)
            };
            parameters[0].Value = DBNull.Value;

            if (!string.IsNullOrWhiteSpace(ItemID))
            {
                sql = sql + String.Format(@" and [ItemID] =@ItemID ");
                parameters[0].Value = ItemID;
            }

            if (!string.IsNullOrWhiteSpace(FabMoOperationID))
                sql = sql + String.Format(@" and [FabMoOperationID] = '{0}' ", FabMoOperationID);

            if (!string.IsNullOrWhiteSpace(FabMoProcessID))
                sql = sql + String.Format(@" and [FabMoProcessID] = '{0}' ", FabMoProcessID);

            /*FabMoItemID（流水号）用于在更新时，排除他自己*/
            if (!string.IsNullOrWhiteSpace(FabMoItemID))
                sql = sql + String.Format(@" and [FabMoItemID] <> '{0}' ", FabMoItemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text, parameters);

            if (dt.Rows.Count == 0)
                return false;
            else
                return true;
        }


        /// <summary>
        /// 获取制令用料列表
        /// SAM 2017年7月13日11:03:14
        /// </summary>
        /// <param name="FabMoProcessID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> GetList(string FabMoProcessID, int page, int rows, ref int count)
        {
            string select = string.Format(
                @"select A.FabMoItemID, A.FabMoProcessID, B.Code as ItemCode, D.Name as Unit, A.ItemID,
                         B.Name + '/' + B.Specification as NameSpecification, A.BaseQuantity, A.AttritionRate, A.ActualQuantity,
                         A.UseQuantity, B.Name as ItemName, B.Drawing, E.Name as PartSource, A.Sequence, A.Comments,
                         F.UserName as Modifier, G.UserName as Creator, A.Status,
                         A.CreateLocalTime as CreateTime, A.ModifiedLocalTime as ModifiedTime", Framework.SystemID);

            string sql = string.Format(
                @"from [SFC_FabMoItem] A
                  left join [SYS_Items] B on B.ItemID = A.ItemID
                  left join [SFC_FabMoProcess] C on A.FabMoProcessID = C.FabMoProcessID
                  left join [SYS_Parameters] D on C.UnitID = D.ParameterID
                  left join [SYS_Parameters] E on B.PartSource = E.ParameterID 
                  left join [SYS_MESUsers] F on A.Modifier = F.MESUserID
                  left join [SYS_MESUsers] G on A.Creator = G.MESUserID                  
                  where A.[SystemID] = '{0}' and  A.[FabMoProcessID] = @FabMoProcessID and
                        A.Status <> '{0}0201213000003'", Framework.SystemID);

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@FabMoProcessID", FabMoProcessID));

            SqlParameter[] paramArray = parameters.ToArray();
            count = UniversalService.getCount(sql, paramArray);

            string orderby = "A.[CreateLocalTime]";

            DataTable dt = UniversalService.getTable(select, sql, orderby, paramArray, page, rows);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// 获取工序的用料列表
        /// SAM 2017年7月13日17:55:09
        /// </summary>
        /// <param name="FabMoOperationID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> GetOperationItemList(string FabMoOperationID, int page, int rows, ref int count)
        {
            string select = string.Format(
                @"select A.FabMoItemID, A.FabMoOperationID, D.Code as ItemCode, E.Name as Unit, A.ItemID,
                         D.Name + '/' + D.Specification as NameSpecification, A.BaseQuantity, A.AttritionRate, A.ActualQuantity,
                         A.UseQuantity, D.Name as ItemName, D.Drawing, F.Name as PartSource, A.Sequence, A.Comments,
                          B.Emplno+'-'+B.UserName as Creator,A.CreateLocalTime as CreateTime,
                            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else C.Emplno+'-'+C.UserName END) as Modifier,
                            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ");

            string sql = string.Format(
                @"from [SFC_FabMoItem] A
                  left join [SYS_MESUsers] B on A.Modifier = B.MESUserID
                  left join [SYS_MESUsers] C on A.Creator = C.MESUserID                 
                  left join [SYS_Items] D on A.ItemID = D.ItemID
                  left join [SFC_FabMoOperation] G on A.FabMoOperationID = G.FabMoOperationID       
                  left join [SYS_Parameters] E on G.UnitID = E.ParameterID
                  left join [SYS_Parameters] F on D.PartSource = F.ParameterID        
                  where A.[SystemID] = '{0}' and  A.[FabMoOperationID] = @FabMoOperationID and
                        A.Status <> '{0}0201213000003'", Framework.SystemID);

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@FabMoOperationID", FabMoOperationID));

            SqlParameter[] paramArray = parameters.ToArray();
            count = UniversalService.getCount(sql, paramArray);

            string orderby = "A.[CreateLocalTime]";

            DataTable dt = UniversalService.getTable(select, sql, orderby, paramArray, page, rows);

            return ToHashtableList(dt);
        }




        /// <summary>
        /// 根据制令制程或者制令制程工序流水号获取用料列表
        /// SAM 2017年7月13日21:53:49
        /// </summary>
        /// <param name="FabMoProcessID"></param>
        /// <param name="FabMoOperationID"></param>
        /// <returns></returns>
        public static IList<Hashtable> GetItemList(string FabMoProcessID, string FabMoOperationID)
        {
            string sql = string.Format(
                @"select A.*,D.Code,D.Name+'/'+D.Specification as NameSpecification,A.ItemID,
               (CASE WHEN A.[FabMoProcessID] is null THEN (Select Sequence from [SFC_FabMoProcess] where [FabMoProcessID]=C.[FabMoProcessID])
                ELSE B.Sequence END) as ProcessSequence,
                (CASE WHEN A.[FabMoProcessID] is null THEN (select [Code] from [SYS_Parameters] where ParameterID = (Select Top 1 ProcessID from [SFC_FabMoProcess] where [FabMoProcessID]=C.[FabMoProcessID]))
                ELSE(select [Code] from [SYS_Parameters] where ProcessID = ParameterID) END) as ProcessCode,
                (CASE WHEN A.[FabMoProcessID] is null THEN (select [Name] from [SYS_Parameters] where ParameterID = (Select Top 1 ProcessID from [SFC_FabMoProcess] where [FabMoProcessID]=C.[FabMoProcessID]))
                ELSE(select [Code] from [SYS_Parameters] where ProcessID = ParameterID) END) as ProcessName,
                C.Sequence as OperationSequence,
                (select [Code] from [SYS_Parameters] where C.OperationID = ParameterID) as OperationIDCode,
                (select [Name] from [SYS_Parameters] where C.OperationID = ParameterID) as OperationIDName
                from [SFC_FabMoItem] A
                left join [SFC_FabMoProcess] B on B.[FabMoProcessID] = A.[FabMoProcessID]
                left join [SFC_FabMoOperation] C on C.[FabMoOperationID] = A.[FabMoOperationID]
                left join [SYS_Items] D on D.[ItemID] = A.[ItemID]
                  where A.[SystemID] = '{0}' and A.[Status] = '{0}0201213000001'", Framework.SystemID);

            string orderby = " order by [Sequence]";

            if (!string.IsNullOrWhiteSpace(FabMoProcessID))
                sql += string.Format(@" and A.[FabMoProcessID] ='{0}'", FabMoProcessID);

            if (!string.IsNullOrWhiteSpace(FabMoOperationID))
                sql += string.Format(@" and A.[FabMoOperationID] ='{0}'", FabMoOperationID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql + orderby, CommandType.Text);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// 根据所有用到的制令制程或者制令制程工序流水号获取用料列表
        /// Joint
        /// </summary>
        /// <param name="FabMoProcessID"></param>
        /// <param name="FabMoOperationID"></param>
        /// <returns></returns>
        public static List<SFC_FabMoItem> GetFabMoItemList(string FPID, string FOID)
        {
            string sql = string.Format(
                @"select * from [SFC_FabMoItem]        
                  where [SystemID] = '{0}' and [Status] = '{0}0201213000001'", Framework.SystemID);

            string orderby = " order by [CreateLocalTime] desc";

            if (!string.IsNullOrWhiteSpace(FPID))
                sql += string.Format(@" and [FabMoProcessID] in ('{0}')", FPID);

            if (!string.IsNullOrWhiteSpace(FOID))
                sql += string.Format(@" and [FabMoOperationID] in ('{0}')", FOID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql + orderby, CommandType.Text);

            return ToList(dt);
        }

        /// <summary>
        /// 获取用料列表
        /// Joint 2017年7月21日09:40:10
        /// </summary>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> Sfc00006GetItemList(string ProcessID, string OperationID, int page,int rows,ref int count)
        {
            string select = string.Format(
                @" select A.Sequence,B.Code as Part,B.Name as Description,B.Specification,
                    (select Name from SYS_Parameters where ParameterID=B.Unit) as Unit,B.Drawing,A.AttritionRate as ScrapRate,
                    (select Name from SYS_Parameters where ParameterID=B.PartSource) as PartSource,
                    A.Comments,A.BaseQuantity as BaseQty,A.UseQuantity as UseQty,
                    C.Emplno+'-'+C.UserName as Creator,A.CreateLocalTime as CreateTime,
                    (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else D.Emplno+'-'+D.UserName END) as Modifier,
                    (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ", Framework.SystemID);           
            string sql = string.Format(
                @"from [SFC_FabMoItem] A 
                    left join [SYS_Items] B on A.ItemID = B.ItemID 
                    left join [SYS_MESUsers] C on A.Modifier = C.MESUserID
                    left join [SYS_MESUsers] D on A.Creator = D.MESUserID    
                    where  A.[SystemID] = '{0}' and A.Status <> '{0}0201213000003'", Framework.SystemID);


            if (!string.IsNullOrWhiteSpace(OperationID))
                sql += string.Format(@" and A.[FabMoOperationID] = '{0}' ", OperationID);
            else
                 sql += string.Format(@" and A.[FabMoProcessID] = '{0}' ", ProcessID);

            count = UniversalService.getCount(sql,null);

            string orderby = "A.[Sequence] ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, null, page, rows);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// 制令用料耗用分析
        /// SAM 2017年9月3日22:00:11
        /// </summary>
        /// <param name="StartFabMoCode"></param>
        /// <param name="EndFabMoCode"></param>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="Status"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> Sfc00018GetList(string StartFabMoCode, string EndFabMoCode,
        string StartDate, string EndDate,string Status, int page, int rows, ref int count)
        {
            string select = string.Format(
                @"select A.FabMoItemID,(CASE WHEN A.[FabMoProcessID] is null THEN E.[MoNo]+'-'+convert(varchar(20),E.[SplitSequence]) ELSE D.[MoNo]+'-'+convert(varchar(20),D.[SplitSequence]) END) as MoNo,F.Code,F.Name,F.Specification,
                (CASE WHEN A.[FabMoProcessID] is null THEN (select [Code] from [SYS_Parameters] where [ParameterID] = (select Top 1 [ProcessID] from [SFC_FabMoProcess] where [FabMoProcessID]=C.[FabMoProcessID]))
                ELSE (select [Code] from [SYS_Parameters] where [ParameterID] = B.[ProcessID]) END) as ProcessCode,
                A.UseQuantity,A.ActualQuantity,A.UseQuantity-A.ActualQuantity as DifferenceNum,
                (CASE WHEN A.[FabMoProcessID] is null THEN (select [Code] from [SYS_Items] where [ItemID] = E.[ItemID])
                ELSE (select [Code] from [SYS_Items] where [ItemID] = D.[ItemID]) END) as ItemCode,
                (CASE WHEN A.[FabMoProcessID] is null THEN (select [Code] from [SYS_Items] where [ItemID] = E.[ItemID])
                ELSE (select [Name] from [SYS_Items] where [ItemID] = D.[ItemID]) END) as ItemName,
                (CASE WHEN A.[FabMoProcessID] is null THEN (select [Code] from [SYS_Items] where [ItemID] = E.[ItemID])
                ELSE (select [Specification] from [SYS_Items] where [ItemID] = D.[ItemID]) END) as ItemSpecification,
                (CASE WHEN A.[FabMoProcessID] is null THEN E.[Quantity] ELSE D.[Quantity] END) as Quantity ");

            string sql = string.Format(
                @"from [SFC_FabMoItem] A
                   left join [SFC_FabMoProcess] B on A.[FabMoProcessID] = B.[FabMoProcessID]
                   left join [SFC_FabMoOperation] C on A.[FabMoOperationID] = C.[FabMoOperationID]
                   left Join [SFC_FabricatedMother] D on B.[FabricatedMotherID] = D.[FabricatedMotherID]
                   left Join [SFC_FabricatedMother] E on C.[FabricatedMotherID] = E.[FabricatedMotherID]
                   left join [SYS_Items] F on A.[ItemID] = F.[ItemID]
                  where A.[SystemID] = '{0}' and A.[Status] = '{0}0201213000001' ", Framework.SystemID);

            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@StartFabMoCode",SqlDbType.VarChar),
                new SqlParameter("@EndFabMoCode",SqlDbType.VarChar),
                new SqlParameter("@StartDate",SqlDbType.VarChar),
                new SqlParameter("@EndDate",SqlDbType.VarChar)
            };
            parameters[0].Value = DBNull.Value;
            parameters[1].Value = DBNull.Value;
            parameters[2].Value = DBNull.Value;
            parameters[3].Value = DBNull.Value;

            SqlParameter[] Parcount = new SqlParameter[]{
                new SqlParameter("@StartFabMoCode",SqlDbType.VarChar),
                new SqlParameter("@EndFabMoCode",SqlDbType.VarChar),
                new SqlParameter("@StartDate",SqlDbType.VarChar),
                new SqlParameter("@EndDate",SqlDbType.VarChar),
            };
            Parcount[0].Value = DBNull.Value;
            Parcount[1].Value = DBNull.Value;
            Parcount[2].Value = DBNull.Value;
            Parcount[3].Value = DBNull.Value;

            if (!string.IsNullOrWhiteSpace(StartFabMoCode))
            {
                sql = sql + String.Format(@" and (CASE WHEN A.[FabMoProcessID] is null THEN E.[MoNo] ELSE D.[MoNo] END) >= @StartFabMoCode ");
                parameters[0].Value = StartFabMoCode;
                Parcount[0].Value = StartFabMoCode;
            }

            if (!string.IsNullOrWhiteSpace(EndFabMoCode))
            {
                sql = sql + String.Format(@" and (CASE WHEN A.[FabMoProcessID] is null THEN E.[MoNo] ELSE D.[MoNo] END) <= @EndFabMoCode ");
                parameters[1].Value = EndFabMoCode;
                Parcount[1].Value = EndFabMoCode;
            }

            if (!string.IsNullOrWhiteSpace(StartDate))
            {
                sql = sql + String.Format(@" and (CASE WHEN A.[FabMoProcessID] is null THEN E.[Date] ELSE D.[Date] END) >= @StartDate ");
                parameters[2].Value = StartDate;
                Parcount[2].Value = StartDate;
            }

            if (!string.IsNullOrWhiteSpace(EndDate))
            {
                sql = sql + String.Format(@" and (CASE WHEN A.[FabMoProcessID] is null THEN E.[Date] ELSE D.[Date] END) <= @EndDate ");
                parameters[3].Value = EndDate;
                Parcount[3].Value = EndDate;
            }

            if (!string.IsNullOrWhiteSpace(Status))
            {
                sql = sql + String.Format(@" and (CASE WHEN A.[FabMoProcessID] is null THEN E.[Status] ELSE D.[Status] END) in ('{0}') ",Status.Replace(",","','"));
                parameters[3].Value = Status;
                Parcount[3].Value = Status;
            }

            count = UniversalService.getCount(sql, Parcount);

            string orderby = "(CASE WHEN A.[FabMoProcessID] is null THEN E.[MoNo] ELSE D.[MoNo] END),(CASE WHEN A.[FabMoProcessID] is null THEN E.[SplitSequence] ELSE D.[SplitSequence] END),F.[Code] ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, parameters, page, rows);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// 拼接制令制程/制令制程工序用料的SQL语句
        /// SAM 2017年10月4日00:44:14
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="Model"></param>
        /// <returns></returns>
        public static string InsertSQL(string userId, SFC_FabMoItem Model)
        {
            try
            {

                string sql = string.Format(
                   @"insert [SFC_FabMoItem]([SystemID],[FabMoItemID],[FabMoProcessID],[FabMoOperationID],[Sequence],
                    [ItemID],[BaseQuantity],[AttritionRate],[UseQuantity],[Status],[Comments],
                    [Modifier],[ModifiedTime],[ModifiedLocalTime],[Creator],[CreateTime],[CreateLocalTime]) values(
                    '{0}','{1}',{2},{3},'{4}',
                    '{5}',{6},{7},{8},'{9}','{10}',           
                    '{11}','{12}','{13}','{11}','{12}','{13}');",
                    Framework.SystemID, Model.FabMoItemID, UniversalService.checkNullForSQL(Model.FabMoProcessID),
                    UniversalService.checkNullForSQL(Model.FabMoOperationID), Model.Sequence,
                    Model.ItemID, Model.BaseQuantity, Model.AttritionRate, Model.UseQuantity, Model.Status, Model.Comments,
                    userId, DateTime.UtcNow, DateTime.Now);


                return sql;
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                throw;
            }
        }
    }
}

