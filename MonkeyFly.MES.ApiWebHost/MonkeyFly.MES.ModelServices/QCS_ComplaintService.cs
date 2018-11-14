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
    public class QCS_ComplaintService : SuperModel<QCS_Complaint>
    {
        /// <summary>
        /// 新增
        /// SAM 2017年6月14日16:22:13
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="Model"></param>
        /// <returns></returns>
        public static bool insert(string userId, QCS_Complaint Model)
        {
            try
            {
                string sql = string.Format(@"insert[QCS_Complaint]([ComplaintID],[Code],[Date],[CustomerID],[CustomerName],
                [Complaintor],[ApplicantID],[Status],
                [Comments],[Modifier],[ModifiedTime],[ModifiedLocalTime],[Creator],[CreateTime],[CreateLocalTime],[SystemID]) values
                 (@ComplaintID,@Code,@Date,@CustomerID,@CustomerName,
                @Complaintor,@ApplicantID,@Status,@Comments,'{0}','{1}','{2}','{0}','{1}','{2}','{3}')", userId, DateTime.Now, DateTime.Now, Framework.SystemID);

                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@ComplaintID",SqlDbType.VarChar),
                    new SqlParameter("@Code",SqlDbType.NVarChar),
                    new SqlParameter("@Date",SqlDbType.DateTime),
                    new SqlParameter("@CustomerID",SqlDbType.VarChar),
                    new SqlParameter("@CustomerName",SqlDbType.NVarChar),
                    new SqlParameter("@Complaintor",SqlDbType.NVarChar),
                    new SqlParameter("@ApplicantID",SqlDbType.VarChar),
                    new SqlParameter("@Status",SqlDbType.VarChar),
                    new SqlParameter("@Comments",SqlDbType.NVarChar),
                    };

                parameters[0].Value = (Object)Model.ComplaintID ?? DBNull.Value;
                parameters[1].Value = (Object)Model.Code ?? DBNull.Value;
                parameters[2].Value = (Object)Model.Date ?? DBNull.Value;
                parameters[3].Value = (Object)Model.CustomerID ?? DBNull.Value;
                parameters[4].Value = (Object)Model.CustomerName ?? DBNull.Value;
                parameters[5].Value = (Object)Model.Complaintor ?? DBNull.Value;
                parameters[6].Value = (Object)Model.ApplicantID ?? DBNull.Value;
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
        /// SAM 2017年6月14日16:22:23
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="Model"></param>
        /// <returns></returns>
        public static bool update(string userId, QCS_Complaint Model)
        {
            try
            {
                string sql = String.Format(@"update[QCS_Complaint] set {0},
            [Date]=@Date,[CustomerID]=@CustomerID,
            [CustomerName]=@CustomerName,[Complaintor]=@Complaintor,[ApplicantID]=@ApplicantID,[Status]=@Status,
            [Comments]=@Comments where [ComplaintID]=@ComplaintID", UniversalService.getUpdateUTC(userId));
                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@ComplaintID",SqlDbType.VarChar),
                    new SqlParameter("@Date",SqlDbType.DateTime),
                    new SqlParameter("@CustomerID",SqlDbType.VarChar),
                    new SqlParameter("@CustomerName",SqlDbType.NVarChar),
                    new SqlParameter("@Complaintor",SqlDbType.NVarChar),
                    new SqlParameter("@ApplicantID",SqlDbType.VarChar),
                    new SqlParameter("@Status",SqlDbType.VarChar),
                    new SqlParameter("@Comments",SqlDbType.NVarChar),
                    };

                parameters[0].Value = Model.ComplaintID;
                parameters[1].Value = (Object)Model.Date ?? DBNull.Value;
                parameters[2].Value = (Object)Model.CustomerID ?? DBNull.Value;
                parameters[3].Value = (Object)Model.CustomerName ?? DBNull.Value;
                parameters[4].Value = (Object)Model.Complaintor ?? DBNull.Value;
                parameters[5].Value = (Object)Model.ApplicantID ?? DBNull.Value;
                parameters[6].Value = (Object)Model.Status ?? DBNull.Value;
                parameters[7].Value = (Object)Model.Comments ?? DBNull.Value;

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
        /// SAM 2017年6月14日16:22:343
        /// </summary>
        /// <param name="ComplaintID"></param>
        /// <returns></returns>
        public static QCS_Complaint get(string ComplaintID)
        {
            string sql = string.Format(@"select Top 1 * from [QCS_Complaint] where [ComplaintID] = '{0}'  and [SystemID] = '{1}' ", ComplaintID, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return null;
            else
                return ToEntity(dt.Rows[0]);
        }

        /// <summary>
        /// 根据客诉单号获取实体
        /// SAM 2017年6月15日14:36:15
        /// </summary>
        /// <param name="Code"></param>
        /// <param name="Status"></param>
        /// <returns></returns>
        public static QCS_Complaint getByCode(string Code)
        {
            string sql = string.Format(@"select Top 1 * from [QCS_Complaint] where [Code] = '{0}'  and [SystemID] = '{1}' and [Status]<> '{1}0201213000003' ", Code, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return null;
            else
                return ToEntity(dt.Rows[0]);
        }

        /// <summary>
        /// 更新客诉单表头到指定状态
        /// SAM 2017年6月15日16:09:05
        /// </summary>
        /// <param name="ComplaintID"></param>
        /// <param name="Status"></param>
        /// <returns></returns>
        public static bool updateStatus(string ComplaintID, string Status)
        {
            try
            {
                string sql = String.Format(@"update [QCS_Complaint] 
                set [Status]=@Status where [ComplaintID]=@ComplaintID");

                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@ComplaintID",SqlDbType.VarChar),
                    new SqlParameter("@Status",SqlDbType.VarChar)
                    };

                parameters[0].Value = ComplaintID;
                parameters[1].Value = Status;

                return SQLHelper.ExecuteNonQuery(sql, CommandType.Text, parameters) > 0;
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return false;
            }
        }


        /// <summary>
        /// 客诉单表头列表
        /// SAM 2017年6月14日16:58:53
        /// </summary>
        /// <param name="StartCode"></param>
        /// <param name="EndCode"></param>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="StartCustCode"></param>
        /// <param name="EndCustCode"></param>
        /// <param name="StartOrder"></param>
        /// <param name="EndOrder"></param>
        /// <param name="Status"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> Qcs00009GetList(string StartCode, string EndCode, 
            string StartDate, string EndDate, 
            string StartCustCode, string EndCustCode, string Status, 
            string StartOrderCode, string EndOrderCode,int page, int rows, ref int count)
        {
            string select = string.Format(@"select A.ComplaintID,A.Code,A.Date,A.CustomerID,A.CustomerName,A.Complaintor,
            A.ApplicantID,A.Status,A.Comments,D.Code as CustomerCode,E.Emplno,E.UserName as ApplicantName,
            B.UserName as Creator,A.CreateLocalTime as CreateTime,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else C.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ");

            string sql = string.Format(@"  from [QCS_Complaint] A 
            left join [SYS_MESUsers] B on A.[Creator] = B.[MESUserID]
            left join [SYS_MESUsers] C on A.[Modifier] = C.[MESUserID]
            left join [SYS_Customers] D on A.[CustomerID] = D.[CustomerID]
            left join [SYS_MESUsers] E on A.[ApplicantID] = E.[MESUserID]
            where A.[SystemID]='{0}' and A.[Status] <> '{0}0201213000003' ", Framework.SystemID);

            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@StartCode",SqlDbType.VarChar),
                new SqlParameter("@EndCode",SqlDbType.VarChar),
                new SqlParameter("@StartDate",SqlDbType.VarChar),
                new SqlParameter("@EndDate",SqlDbType.VarChar),
                new SqlParameter("@StartCustCode",SqlDbType.VarChar),
                new SqlParameter("@EndCustCode",SqlDbType.VarChar),
                new SqlParameter("@Status",SqlDbType.VarChar), 
                new SqlParameter("@StartOrderCode",SqlDbType.VarChar),
                new SqlParameter("@EndOrderCode",SqlDbType.VarChar)
            };
            parameters[0].Value = DBNull.Value;
            parameters[1].Value = DBNull.Value;
            parameters[2].Value = DBNull.Value;
            parameters[3].Value = DBNull.Value;
            parameters[4].Value = DBNull.Value;
            parameters[5].Value = DBNull.Value;
            parameters[6].Value = DBNull.Value;
            parameters[7].Value = DBNull.Value;
            parameters[8].Value = DBNull.Value;

            SqlParameter[] Parcount = new SqlParameter[]{
                new SqlParameter("@StartCode",SqlDbType.VarChar),
                new SqlParameter("@EndCode",SqlDbType.VarChar),
                new SqlParameter("@StartDate",SqlDbType.VarChar),
                new SqlParameter("@EndDate",SqlDbType.VarChar),
                new SqlParameter("@StartCustCode",SqlDbType.VarChar),
                new SqlParameter("@EndCustCode",SqlDbType.VarChar),
                new SqlParameter("@Status",SqlDbType.VarChar),
                new SqlParameter("@StartOrderCode",SqlDbType.VarChar),
                new SqlParameter("@EndOrderCode",SqlDbType.VarChar)
            };
            Parcount[0].Value = DBNull.Value;
            Parcount[1].Value = DBNull.Value;
            Parcount[2].Value = DBNull.Value;
            Parcount[3].Value = DBNull.Value;
            Parcount[4].Value = DBNull.Value;
            Parcount[5].Value = DBNull.Value;
            Parcount[6].Value = DBNull.Value;
            Parcount[7].Value = DBNull.Value;
            Parcount[8].Value = DBNull.Value;

            if (!string.IsNullOrWhiteSpace(StartCode))
            {
                sql = sql + string.Format(@" and A.[Code] >= @StartCode ");
                parameters[0].Value = StartCode;
                Parcount[0].Value = StartCode;
            }

            if (!string.IsNullOrWhiteSpace(EndCode))
            {
                sql = sql + string.Format(@" and A.[Code] <= @EndCode ");
                parameters[1].Value = EndCode;
                Parcount[1].Value = EndCode;
            }


            if (!string.IsNullOrWhiteSpace(StartDate))
            {
                sql = sql + string.Format(@" and A.[Date] >= @StartDate ");
                parameters[2].Value = StartDate;
                Parcount[2].Value = StartDate;
            }

            if (!string.IsNullOrWhiteSpace(EndDate))
            {
                sql = sql + string.Format(@" and A.[Date] <= @EndDate ");
                parameters[3].Value = EndDate;
                Parcount[3].Value = EndDate;
            }


            if (!string.IsNullOrWhiteSpace(StartCustCode))
            {
                sql = sql + string.Format(@" and D.[Code] >= @StartCustCode ");
                parameters[4].Value = StartCustCode;
                Parcount[4].Value = StartCustCode;
            }

            if (!string.IsNullOrWhiteSpace(EndCustCode))
            {
                sql = sql + string.Format(@" and D.[Code] <= @EndCustCode ");
                parameters[5].Value = EndCustCode;
                Parcount[5].Value = EndCustCode;
            }

            if (!string.IsNullOrWhiteSpace(Status))
            {
                sql = sql + string.Format(@" and A.[Status] = @Status ");
                parameters[6].Value = Status;
                Parcount[6].Value = Status;
            }

            if (!string.IsNullOrWhiteSpace(StartOrderCode))
            {
                sql = sql + string.Format(@" and A.[ComplaintID] in (select [ComplaintID] from [QCS_ComplaintDetails]  where [OrderNo] >=@StartOrderCode and [Status] <> '{0}0201213000003') ",Framework.SystemID);
                parameters[7].Value = StartOrderCode;
                Parcount[7].Value = StartOrderCode;
            }

            if (!string.IsNullOrWhiteSpace(EndOrderCode))
            {
                sql = sql + string.Format(@" and A.[ComplaintID] in (select [ComplaintID] from [QCS_ComplaintDetails]  where [OrderNo] <=@EndOrderCode and [Status] <> '{0}0201213000003') ", Framework.SystemID);
                parameters[8].Value = EndOrderCode;
                Parcount[8].Value = EndOrderCode;
            }

            count = UniversalService.getCount(sql, Parcount);

            String orderby = " A.[Status],A.[Date] desc ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, parameters, page, rows);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// 客诉单的导出
        /// SAM 2017年6月15日10:59:55
        /// </summary>
        /// <param name="StartCode"></param>
        /// <param name="EndCode"></param>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="StartCustCode"></param>
        /// <param name="EndCustCode"></param>
        /// <param name="Status"></param>
        /// <returns></returns>
        public static DataTable Qcs00009Export(string StartCode, string EndCode, string StartDate, string EndDate, string StartCustCode, string EndCustCode, string Status, string StartOrderCode, string EndOrderCode)
        {
            string select = string.Format(@"select ROW_NUMBER() OVER (ORDER BY A.[Status],A.[Code],G.[Sequence]),
            A.Code,A.Date,D.Code as CustomerCode,A.CustomerName,A.Complaintor,E.Name as Status,A.Comments,
            F.Emplno as ApplicantCode,F.UserName as ApplicantName,
            B.UserName as Creator,A.CreateLocalTime as CreateTime,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else C.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime,
            G.Sequence,
            (Select Code from [SYS_Items] where ItemID = G.ItemID) as ItemCode,
            G.BatchNumber,G.ShipperNo,G.OrderNo,G.Quantity,G.Description,
            (Select Name from [SYS_Parameters] where ParameterID = G.Status) as DetailStatus,
            (Select Name from [SYS_Items] where ItemID = G.ItemID) as ItemName,
            (Select Specification from [SYS_Items] where ItemID = G.ItemID) as ItemSpecification ");

            string sql = string.Format(@" from [QCS_Complaint] A 
            left join [SYS_MESUsers] B on A.[Creator] = B.[MESUserID]
            left join [SYS_MESUsers] C on A.[Modifier] = C.[MESUserID]
            left join [SYS_Customers] D on A.[CustomerID] = D.[CustomerID]
            left join [SYS_Parameters] E on A.[Status] = E.[ParameterID]
            left join [SYS_MESUsers] F on A.[ApplicantID] = F.[MESUserID]
            left join [QCS_ComplaintDetails] G on A.[ComplaintID] = G.[ComplaintID]  and G.[Status] <> '{0}0201213000003' 
            where A.[SystemID]='{0}' and A.[Status] <> '{0}0201213000003'", Framework.SystemID);

            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@StartCode",SqlDbType.VarChar),
                new SqlParameter("@EndCode",SqlDbType.VarChar),
                new SqlParameter("@StartDate",SqlDbType.VarChar),
                new SqlParameter("@EndDate",SqlDbType.VarChar),
                new SqlParameter("@StartCustCode",SqlDbType.VarChar),
                new SqlParameter("@EndCustCode",SqlDbType.VarChar),
                new SqlParameter("@Status",SqlDbType.VarChar),
                new SqlParameter("@StartOrderCode",SqlDbType.VarChar),
                new SqlParameter("@EndOrderCode",SqlDbType.VarChar)
            };
            parameters[0].Value = DBNull.Value;
            parameters[1].Value = DBNull.Value;
            parameters[2].Value = DBNull.Value;
            parameters[3].Value = DBNull.Value;
            parameters[4].Value = DBNull.Value;
            parameters[5].Value = DBNull.Value;
            parameters[6].Value = DBNull.Value;
            parameters[7].Value = DBNull.Value;
            parameters[8].Value = DBNull.Value;

            if (!string.IsNullOrWhiteSpace(StartCode))
            {
                sql = sql + string.Format(@" and A.[Code] >= @StartCode ");
                parameters[0].Value = StartCode;             
            }

            if (!string.IsNullOrWhiteSpace(EndCode))
            {
                sql = sql + string.Format(@" and A.[Code] <= @EndCode ");
                parameters[1].Value = EndCode;
            }

            if (!string.IsNullOrWhiteSpace(StartDate))
            {
                sql = sql + string.Format(@" and A.[Date] >= @StartDate ");
                parameters[2].Value = StartDate;
             }

            if (!string.IsNullOrWhiteSpace(EndDate))
            {
                sql = sql + string.Format(@" and A.[Date] <= @EndDate ");
                parameters[3].Value = EndDate;
            }


            if (!string.IsNullOrWhiteSpace(StartCustCode))
            {
                sql = sql + string.Format(@" and D.[Code] >= @StartCustCode ");
                parameters[4].Value = StartCustCode;
            }

            if (!string.IsNullOrWhiteSpace(EndCustCode))
            {
                sql = sql + string.Format(@" and D.[Code] <= @EndCustCode ");
                parameters[5].Value = EndCustCode;
            }

            if (!string.IsNullOrWhiteSpace(Status))
            {
                sql = sql + string.Format(@" and A.[Status] = @Status ");
                parameters[6].Value = Status;
            }

            if (!string.IsNullOrWhiteSpace(StartOrderCode))
            {
                sql = sql + string.Format(@" and A.[ComplaintID] in (select [ComplaintID] from [QCS_ComplaintDetails]  where [OrderNo] >=@StartOrderCode and [Status] <> '{0}0201213000003') ", Framework.SystemID);
                parameters[7].Value = StartOrderCode;
            }

            if (!string.IsNullOrWhiteSpace(EndOrderCode))
            {
                sql = sql + string.Format(@" and A.[ComplaintID] in (select [ComplaintID] from [QCS_ComplaintDetails]  where [OrderNo] <=@EndOrderCode and [Status] <> '{0}0201213000003') ", Framework.SystemID);
                parameters[8].Value = EndOrderCode;
            }


            string orderBy = "order By A.[Status],A.[Date] desc";

            return SQLHelper.ExecuteDataTable(select + sql + orderBy, CommandType.Text, parameters);
        }

        /// <summary>
        /// 客诉分析与改善列表
        /// SAM 2017年6月15日11:11:57
        /// </summary>
        /// <param name="StartCode"></param>
        /// <param name="EndCode"></param>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="StartCustCode"></param>
        /// <param name="EndCustCode"></param>
        /// <param name="Status"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> Qcs00010GetList(string StartCode, string EndCode, string StartDate, string EndDate, string StartCustCode, string EndCustCode, string StartOrderCode, string EndOrderCode, string Status, int page, int rows, ref int count)
        {
            string select = string.Format(@"select E.ComplaintDetailID,A.ComplaintID,A.Code,A.Date,A.CustomerID,A.CustomerName,A.Complaintor,
            A.Comments,D.Code as CustomerCode,F.Emplno+'-'+F.UserName as Applicant,
            (Select Code from [SYS_Items] where ItemID = E.ItemID) as ItemCode,E.ItemID,
            E.BatchNumber,E.ShipperNo,E.OrderNo,E.Quantity,E.Description,E.Status,E.Sequence,
            (select Name from [SYS_Parameters] where ParameterID = E.Status) as StatusName,
            (Select Name from [SYS_Items] where ItemID = E.ItemID) as ItemName,
            (Select Specification from [SYS_Items] where ItemID = E.ItemID) as ItemSpecification, 
            B.Emplno+'-'+B.UserName as Creator,A.CreateLocalTime as CreateTime,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else C.Emplno+'-'+C.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ");

            string sql = string.Format(@"  from [QCS_Complaint] A 
            left join [SYS_MESUsers] B on A.[Creator] = B.[MESUserID]
            left join [SYS_MESUsers] C on A.[Modifier] = C.[MESUserID]
            left join [SYS_Customers] D on A.[CustomerID] = D.[CustomerID]
            left join [QCS_ComplaintDetails] E on A.[ComplaintID] = E.[ComplaintID]
            left join [SYS_MESUsers] F on A.[ApplicantID] = F.[MESUserID]
            where A.[SystemID]='{0}' and E.[Status] in ('{0}0201213000029','{0}020121300002A','{0}020121300002B') 
            and (Select COUNT(*) from [QCS_ComplaintDetails] where [ComplaintID] = A.[ComplaintID] and [Status] <> '{0}0201213000003' ) > 0 ", Framework.SystemID);

            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@StartCode",SqlDbType.VarChar),
                new SqlParameter("@EndCode",SqlDbType.VarChar),
                new SqlParameter("@StartDate",SqlDbType.VarChar),
                new SqlParameter("@EndDate",SqlDbType.VarChar),
                new SqlParameter("@StartCustCode",SqlDbType.VarChar),
                new SqlParameter("@EndCustCode",SqlDbType.VarChar),
                new SqlParameter("@StartOrderCode",SqlDbType.VarChar),
                new SqlParameter("@EndOrderCode",SqlDbType.VarChar)
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
                new SqlParameter("@StartCode",SqlDbType.VarChar),
                new SqlParameter("@EndCode",SqlDbType.VarChar),
                new SqlParameter("@StartDate",SqlDbType.VarChar),
                new SqlParameter("@EndDate",SqlDbType.VarChar),
                new SqlParameter("@StartCustCode",SqlDbType.VarChar),
                new SqlParameter("@EndCustCode",SqlDbType.VarChar),
                new SqlParameter("@StartOrderCode",SqlDbType.VarChar),
                new SqlParameter("@EndOrderCode",SqlDbType.VarChar)
            };
            Parcount[0].Value = DBNull.Value;
            Parcount[1].Value = DBNull.Value;
            Parcount[2].Value = DBNull.Value;
            Parcount[3].Value = DBNull.Value;
            Parcount[4].Value = DBNull.Value;
            Parcount[5].Value = DBNull.Value;
            Parcount[6].Value = DBNull.Value;
            Parcount[7].Value = DBNull.Value;


            if (!string.IsNullOrWhiteSpace(StartCode))
            {
                sql = sql + string.Format(@" and A.[Code] >= @StartCode ");
                parameters[0].Value = StartCode;
                Parcount[0].Value = StartCode;
            }

            if (!string.IsNullOrWhiteSpace(EndCode))
            {
                sql = sql + string.Format(@" and A.[Code] <= @EndCode ");
                parameters[1].Value = EndCode;
                Parcount[1].Value = EndCode;
            }


            if (!string.IsNullOrWhiteSpace(StartDate))
            {
                sql = sql + string.Format(@" and A.[Date] >= @StartDate ");
                parameters[2].Value = StartDate;
                Parcount[2].Value = StartDate;
            }

            if (!string.IsNullOrWhiteSpace(EndDate))
            {
                sql = sql + string.Format(@" and A.[Date] <= @EndDate ");
                parameters[3].Value = EndDate;
                Parcount[3].Value = EndDate;
            }


            if (!string.IsNullOrWhiteSpace(StartCustCode))
            {
                sql = sql + string.Format(@" and D.[Code] >= @StartCustCode ");
                parameters[4].Value = StartCustCode;
                Parcount[4].Value = StartCustCode;
            }

            if (!string.IsNullOrWhiteSpace(EndCustCode))
            {
                sql = sql + string.Format(@" and D.[Code] <= @EndCustCode ");
                parameters[5].Value = EndCustCode;
                Parcount[5].Value = EndCustCode;
            }


            if (!string.IsNullOrWhiteSpace(StartOrderCode))
            {
                sql = sql + string.Format(@" and E.[OrderNo] >= @StartOrderCode ");
                parameters[6].Value = StartOrderCode;
                Parcount[6].Value = StartOrderCode;
            }

            if (!string.IsNullOrWhiteSpace(EndOrderCode))
            {
                sql = sql + string.Format(@" and E.[OrderNo] <= @EndOrderCode ");
                parameters[7].Value = EndOrderCode;
                Parcount[7].Value = EndOrderCode;
            }


            if (!string.IsNullOrWhiteSpace(Status))
                sql = sql + string.Format(@" and E.[Status] in ('{0}') ", Status.Replace(",","','"));

            count = UniversalService.getCount(sql, Parcount);

            String orderby = "E.[Status],A.[Date] desc ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, parameters, page, rows);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// 客诉单状态变更列表
        /// SAM 2017年6月15日15:40:37
        /// </summary>
        /// <param name="StartCode"></param>
        /// <param name="EndCode"></param>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="StartCustCode"></param>
        /// <param name="EndCustCode"></param>
        /// <param name="StartOrderCode"></param>
        /// <param name="EndOrderCode"></param>
        /// <param name="Status"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> Qcs00011GetList(string StartCode, string EndCode, string StartDate, string EndDate, string StartCustCode, string EndCustCode, string StartOrderCode, string EndOrderCode, string Status, int page, int rows, ref int count)
        {
            string select = string.Format(@"select E.ComplaintDetailID,A.ComplaintID,A.Code,A.Date,A.CustomerID,A.CustomerName,A.Complaintor,
            A.Comments,D.Code as CustomerCode,F.Emplno+'-'+F.UserName as Applicant,E.Sequence,
            (Select Code from [SYS_Items] where ItemID = E.ItemID) as ItemCode,E.ItemID,
            E.BatchNumber,E.ShipperNo,E.OrderNo,E.Quantity,E.Description,E.Status,
            (select Name from [SYS_Parameters] where ParameterID = E.Status) as StatusName,
            (Select Name from [SYS_Items] where ItemID = E.ItemID) as ItemName,
            (Select Specification from [SYS_Items] where ItemID = E.ItemID) as ItemSpecification, 
            B.Emplno+'-'+B.UserName as Creator,A.CreateLocalTime as CreateTime,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else C.Emplno+'-'+C.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ");

            string sql = string.Format(@"  from [QCS_Complaint] A 
            left join [SYS_MESUsers] B on A.[Creator] = B.[MESUserID]
            left join [SYS_MESUsers] C on A.[Modifier] = C.[MESUserID]
            left join [SYS_Customers] D on A.[CustomerID] = D.[CustomerID]
            left join [QCS_ComplaintDetails] E on A.[ComplaintID] = E.[ComplaintID]
            left join [SYS_MESUsers] F on A.[ApplicantID] = F.[MESUserID]
            where A.[SystemID]='{0}' and A.[Status] <> '{0}0201213000003' and E.[Status] <> '{0}0201213000003' 
            and (Select COUNT(*) from [QCS_ComplaintDetails] where [ComplaintID] = A.[ComplaintID] and [Status] <> '{0}0201213000003' ) > 0 ", Framework.SystemID);

            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@StartCode",SqlDbType.VarChar),
                new SqlParameter("@EndCode",SqlDbType.VarChar),
                new SqlParameter("@StartDate",SqlDbType.VarChar),
                new SqlParameter("@EndDate",SqlDbType.VarChar),
                new SqlParameter("@StartCustCode",SqlDbType.VarChar),
                new SqlParameter("@EndCustCode",SqlDbType.VarChar),
                new SqlParameter("@StartOrderCode",SqlDbType.VarChar),
                new SqlParameter("@EndOrderCode",SqlDbType.VarChar)
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
                new SqlParameter("@StartCode",SqlDbType.VarChar),
                new SqlParameter("@EndCode",SqlDbType.VarChar),
                new SqlParameter("@StartDate",SqlDbType.VarChar),
                new SqlParameter("@EndDate",SqlDbType.VarChar),
                new SqlParameter("@StartCustCode",SqlDbType.VarChar),
                new SqlParameter("@EndCustCode",SqlDbType.VarChar),
                new SqlParameter("@StartOrderCode",SqlDbType.VarChar),
                new SqlParameter("@EndOrderCode",SqlDbType.VarChar)
            };
            Parcount[0].Value = DBNull.Value;
            Parcount[1].Value = DBNull.Value;
            Parcount[2].Value = DBNull.Value;
            Parcount[3].Value = DBNull.Value;
            Parcount[4].Value = DBNull.Value;
            Parcount[5].Value = DBNull.Value;
            Parcount[6].Value = DBNull.Value;
            Parcount[7].Value = DBNull.Value;


            if (!string.IsNullOrWhiteSpace(StartCode))
            {
                sql = sql + string.Format(@" and A.[Code] >= @StartCode ");
                parameters[0].Value = StartCode;
                Parcount[0].Value = StartCode;
            }

            if (!string.IsNullOrWhiteSpace(EndCode))
            {
                sql = sql + string.Format(@" and A.[Code] <= @EndCode ");
                parameters[1].Value = EndCode;
                Parcount[1].Value = EndCode;
            }

            if (!string.IsNullOrWhiteSpace(StartDate))
            {
                sql = sql + string.Format(@" and A.[Date] >= @StartDate ");
                parameters[2].Value = StartDate;
                Parcount[2].Value = StartDate;
            }

            if (!string.IsNullOrWhiteSpace(EndDate))
            {
                sql = sql + string.Format(@" and A.[Date] <= @EndDate ");
                parameters[3].Value = EndDate;
                Parcount[3].Value = EndDate;
            }

            if (!string.IsNullOrWhiteSpace(StartCustCode))
            {
                sql = sql + string.Format(@" and D.[Code] >= @StartCustCode ");
                parameters[4].Value = StartCustCode;
                Parcount[4].Value = StartCustCode;
            }

            if (!string.IsNullOrWhiteSpace(EndCustCode))
            {
                sql = sql + string.Format(@" and D.[Code] <= @EndCustCode ");
                parameters[5].Value = EndCustCode;
                Parcount[5].Value = EndCustCode;
            }

            if (!string.IsNullOrWhiteSpace(StartOrderCode))
            {
                sql = sql + string.Format(@" and E.[OrderNo] >= @StartOrderCode ");
                parameters[6].Value = StartOrderCode;
                Parcount[6].Value = StartOrderCode;
            }

            if (!string.IsNullOrWhiteSpace(EndOrderCode))
            {
                sql = sql + string.Format(@" and E.[OrderNo] <= @EndOrderCode ");
                parameters[7].Value = EndOrderCode;
                Parcount[7].Value = EndOrderCode;
            }

            if (!string.IsNullOrWhiteSpace(Status))
                sql = sql + string.Format(@" and E.[Status] in ('{0}') ", Status.Replace(",", "','"));

            count = UniversalService.getCount(sql, Parcount);

            String orderby = " A.[Code],E.[Sequence] ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, parameters, page, rows);

            return ToHashtableList(dt);
        }


        /// <summary>
        /// 客诉单的开窗
        /// SAM 2017年6月21日17:51:06
        /// </summary>
        /// <param name="Code"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> GetComplaintList(string Code,int page, int rows, ref int count)
        {
            string select = string.Format(@"select A.ComplaintID,A.Code,A.Date,A.CustomerID,A.CustomerName,A.Complaintor,
            A.ApplicantID,A.Status,A.Comments,D.Code as CustomerCode,
            B.UserName as Creator,A.CreateLocalTime as CreateTime,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else C.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ");

            string sql = string.Format(@"  from [QCS_Complaint] A 
            left join [SYS_MESUsers] B on A.[Creator] = B.[MESUserID]
            left join [SYS_MESUsers] C on A.[Modifier] = C.[MESUserID]
            left join [SYS_Customers] D on A.[CustomerID] = D.[CustomerID]
            where A.[SystemID]='{0}' and A.[Status] <> '{0}0201213000003' ", Framework.SystemID);

            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@Code",SqlDbType.VarChar)
            };
            parameters[0].Value = DBNull.Value;

            SqlParameter[] Parcount = new SqlParameter[]{
                new SqlParameter("@Code",SqlDbType.VarChar)
            };
            Parcount[0].Value = DBNull.Value;

            if (!string.IsNullOrWhiteSpace(Code))
            {
                Code = "%" + Code + "%";
                sql = sql + string.Format(@" and A.[Code] like @Code ");
                parameters[0].Value = Code;
                Parcount[0].Value = Code;
            }

            count = UniversalService.getCount(sql, Parcount);

            String orderby = " A.[Status],A.[Code] ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, parameters, page, rows);

            return ToHashtableList(dt);
        }

    }
}

