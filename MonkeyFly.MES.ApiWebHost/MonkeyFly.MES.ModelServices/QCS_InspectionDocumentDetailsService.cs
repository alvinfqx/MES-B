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
    public class QCS_InspectionDocumentDetailsService : SuperModel<QCS_InspectionDocumentDetails>
    {
        /// <summary>
        /// 新增
        /// Joint
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="Model"></param>
        /// <returns></returns>
        public static bool insert(string userId, QCS_InspectionDocumentDetails Model)
        {
            try
            {
                string sql = string.Format(@"insert[QCS_InspectionDocumentDetails]([InspectionDocumentDetailID],[InspectionDocumentID],[Sequence],[InspectionMethod],
                    [InspectionClassID],[InspectionMethodID],[InspectionItemID],[InspectionLevelID],[InspectionFaultID],[SampleQuantity],[Aql],[AcQuantity],[ReQuantity],
                    [NGquantity],[Attribute],[QualityControlDecision],[Status],[Comments],[InspectionStandard],[AttributeType],[Modifier],[ModifiedTime],[ModifiedLocalTime],[Creator],[CreateTime],[CreateLocalTime],[SystemID]) values
                    (
                    @InspectionDocumentDetailID,@InspectionDocumentID,
                    @Sequence,@InspectionMethod,@InspectionClassID,
                    @InspectionMethodID,@InspectionItemID,@InspectionLevelID,
                    @InspectionFaultID,@SampleQuantity,@Aql,
                    @AcQuantity,@ReQuantity,@NGquantity,
                    @Attribute,@QualityControlDecision,@Status,
                    @Comments,@InspectionStandard,@AttributeType,'{0}','{1}','{2}','{0}','{1}','{2}','{3}')", userId, DateTime.Now, DateTime.Now, Framework.SystemID);

                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@InspectionDocumentDetailID",SqlDbType.VarChar),
                    new SqlParameter("@InspectionDocumentID",SqlDbType.VarChar),
                    new SqlParameter("@Sequence",SqlDbType.Int),
                    new SqlParameter("@InspectionMethod",SqlDbType.VarChar),
                    new SqlParameter("@InspectionClassID",SqlDbType.VarChar),
                    new SqlParameter("@InspectionMethodID",SqlDbType.VarChar),
                    new SqlParameter("@InspectionItemID",SqlDbType.VarChar),
                    new SqlParameter("@InspectionLevelID",SqlDbType.VarChar),
                    new SqlParameter("@InspectionFaultID",SqlDbType.VarChar),
                    new SqlParameter("@SampleQuantity",SqlDbType.Decimal),
                    new SqlParameter("@Aql",SqlDbType.VarChar),
                    new SqlParameter("@AcQuantity",SqlDbType.Decimal),
                    new SqlParameter("@ReQuantity",SqlDbType.Decimal),
                    new SqlParameter("@NGquantity",SqlDbType.Decimal),
                    new SqlParameter("@Attribute",SqlDbType.VarChar),
                    new SqlParameter("@QualityControlDecision",SqlDbType.VarChar),
                    new SqlParameter("@Status",SqlDbType.VarChar),
                    new SqlParameter("@Comments",SqlDbType.NVarChar),
                    new SqlParameter("@InspectionStandard",SqlDbType.VarChar),
                    new SqlParameter("@AttributeType",SqlDbType.VarChar),
                    };

                parameters[0].Value = (Object)Model.InspectionDocumentDetailID ?? DBNull.Value;
                parameters[1].Value = (Object)Model.InspectionDocumentID ?? DBNull.Value;
                parameters[2].Value = (Object)Model.Sequence ?? DBNull.Value;
                parameters[3].Value = (Object)Model.InspectionMethod ?? DBNull.Value;
                parameters[4].Value = (Object)Model.InspectionClassID ?? DBNull.Value;
                parameters[5].Value = (Object)Model.InspectionMethodID ?? DBNull.Value;
                parameters[6].Value = (Object)Model.InspectionItemID ?? DBNull.Value;
                parameters[7].Value = (Object)Model.InspectionLevelID ?? DBNull.Value;
                parameters[8].Value = (Object)Model.InspectionFaultID ?? DBNull.Value;
                parameters[9].Value = (Object)Model.SampleQuantity ?? DBNull.Value;
                parameters[10].Value = (Object)Model.Aql ?? DBNull.Value;
                parameters[11].Value = (Object)Model.AcQuantity ?? DBNull.Value;
                parameters[12].Value = (Object)Model.ReQuantity ?? DBNull.Value;
                parameters[13].Value = (Object)Model.NGquantity ?? DBNull.Value;
                parameters[14].Value = (Object)Model.Attribute ?? DBNull.Value;
                parameters[15].Value = (Object)Model.QualityControlDecision ?? DBNull.Value;
                parameters[16].Value = (Object)Model.Status ?? DBNull.Value;
                parameters[17].Value = (Object)Model.Comments ?? DBNull.Value;
                parameters[18].Value = (Object)Model.InspectionStandard ?? DBNull.Value;
                parameters[19].Value = (Object)Model.AttributeType ?? DBNull.Value;

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
        /// Joint
        /// 2017年8月11日17:06:55
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="Model"></param>
        /// <returns></returns>
        public static bool update(string userId, QCS_InspectionDocumentDetails Model)
        {
            try
            {
                string sql = String.Format(@"update[QCS_InspectionDocumentDetails] set {0},
                    [InspectionDocumentID]=@InspectionDocumentID,[Sequence]=@Sequence,[InspectionMethod]=@InspectionMethod,
                    [InspectionClassID]=@InspectionClassID,[InspectionMethodID]=@InspectionMethodID,[InspectionItemID]=@InspectionItemID,[InspectionLevelID]=@InspectionLevelID,
                    [InspectionFaultID]=@InspectionFaultID,[SampleQuantity]=@SampleQuantity,[Aql]=@Aql,[AcQuantity]=@AcQuantity,
                    [ReQuantity]=@ReQuantity,[NGquantity]=@NGquantity,[Attribute]=@Attribute,[QualityControlDecision]=@QualityControlDecision,
                    [Status]=@Status,[Comments]=@Comments,[InspectionStandard]=@InspectionStandard where [InspectionDocumentDetailID]=@InspectionDocumentDetailID", UniversalService.getUpdateUTC(userId));
                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@InspectionDocumentDetailID",SqlDbType.VarChar),
                    new SqlParameter("@InspectionDocumentID",SqlDbType.VarChar),
                    new SqlParameter("@Sequence",SqlDbType.Int),
                    new SqlParameter("@InspectionMethod",SqlDbType.VarChar),
                    new SqlParameter("@InspectionClassID",SqlDbType.VarChar),
                    new SqlParameter("@InspectionMethodID",SqlDbType.VarChar),
                    new SqlParameter("@InspectionItemID",SqlDbType.VarChar),
                    new SqlParameter("@InspectionLevelID",SqlDbType.VarChar),
                    new SqlParameter("@InspectionFaultID",SqlDbType.VarChar),
                    new SqlParameter("@SampleQuantity",SqlDbType.Decimal),
                    new SqlParameter("@Aql",SqlDbType.VarChar),
                    new SqlParameter("@AcQuantity",SqlDbType.Decimal),
                    new SqlParameter("@ReQuantity",SqlDbType.Decimal),
                    new SqlParameter("@NGquantity",SqlDbType.Decimal),
                    new SqlParameter("@Attribute",SqlDbType.VarChar),
                    new SqlParameter("@QualityControlDecision",SqlDbType.VarChar),
                    new SqlParameter("@Status",SqlDbType.VarChar),
                    new SqlParameter("@Comments",SqlDbType.NVarChar),
                    new SqlParameter("@InspectionStandard",SqlDbType.VarChar),
                    };

                parameters[0].Value = Model.InspectionDocumentDetailID;
                parameters[1].Value = (Object)Model.InspectionDocumentID ?? DBNull.Value;
                parameters[2].Value = (Object)Model.Sequence ?? DBNull.Value;
                parameters[3].Value = (Object)Model.InspectionMethod ?? DBNull.Value;
                parameters[4].Value = (Object)Model.InspectionClassID ?? DBNull.Value;
                parameters[5].Value = (Object)Model.InspectionMethodID ?? DBNull.Value;
                parameters[6].Value = (Object)Model.InspectionItemID ?? DBNull.Value;
                parameters[7].Value = (Object)Model.InspectionLevelID ?? DBNull.Value;
                parameters[8].Value = (Object)Model.InspectionFaultID ?? DBNull.Value;
                parameters[9].Value = (Object)Model.SampleQuantity ?? DBNull.Value;
                parameters[10].Value = (Object)Model.Aql ?? DBNull.Value;
                parameters[11].Value = (Object)Model.AcQuantity ?? DBNull.Value;
                parameters[12].Value = (Object)Model.ReQuantity ?? DBNull.Value;
                parameters[13].Value = (Object)Model.NGquantity ?? DBNull.Value;
                parameters[14].Value = (Object)Model.Attribute ?? DBNull.Value;
                parameters[15].Value = (Object)Model.QualityControlDecision ?? DBNull.Value;
                parameters[16].Value = (Object)Model.Status ?? DBNull.Value;
                parameters[17].Value = (Object)Model.Comments ?? DBNull.Value;
                parameters[18].Value = (Object)Model.InspectionStandard ?? DBNull.Value;
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
        /// Joint
        /// </summary>
        /// <param name="InspectionDocumentDetailID"></param>
        /// <returns></returns>
        public static QCS_InspectionDocumentDetails get(string InspectionDocumentDetailID)
        {
            string sql = string.Format(@"select Top 1 * from [QCS_InspectionDocumentDetails] where [InspectionDocumentDetailID] = '{0}'  and [SystemID] = '{1}' ", InspectionDocumentDetailID, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return null;
            else
                return ToEntity(dt.Rows[0]);
        }

        /// <summary>
        /// 制程检验明细
        /// Joint
        /// </summary>
        /// <param name="InspectionDocumentID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> Qcs00005GetDetailsList(string InspectionDocumentID, int page, int rows, ref int count)
        {
            string select = string.Format(
                @"select A.InspectionDocumentDetailID,A.Sequence,A.InspectionItemID,A.InspectionStandard ,
                         A.InspectionMethod,A.InspectionMethodID,A.InspectionClassID,A.InspectionLevelID,A.Status,
                         A.InspectionDocumentID,
                        (select Value from [MES_Parameter] where [ParameterID]='{0}1101213000003') as Value,
                         G.Disadvantages as InspectionFaultID,(select [Name] from [SYS_Parameters] where [ParameterID]=G.Disadvantages) as FaultDesc,
                         A.SampleQuantity,A.Aql,(select [Name] from [SYS_Parameters] where [ParameterID]=A.Aql) as AqlName,
                         A.AcQuantity,A.ReQuantity,A.NGquantity,
                         G.Code as InspectionItemCode,G.Name as InspectionItemName,
                         A.AttributeType,A.Attribute,
                         A.QualityControlDecision as QcDecision,A.Comments,
                         (CASE WHEN C.Emplno is null or C.Emplno = '' THEN C.Account else C.Emplno END)+'-'+C.UserName as Creator,A.CreateLocalTime as CreateTime,
                    (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else (CASE WHEN D.Emplno is null or D.Emplno = '' THEN D.Account else D.Emplno END)+'-'+D.UserName END) as Modifier,
                    (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime 
                         ", Framework.SystemID);

            string sql = string.Format(
                @"from [QCS_InspectionDocumentDetails] A                  
                  left join [SYS_MESUsers] C on A.Modifier = C.MESUserID
                  left join [SYS_MESUsers] D on A.Creator = D.MESUserID
                  left join [QCS_InspectionDocument] E on A.InspectionDocumentID=E.InspectionDocumentID
                  left join [QCS_InspectionProject] G on A.InspectionItemID=G.InspectionProjectID
                  where A.[SystemID] = '{0}' and A.[Status] <> '{0}0201213000003' 
                  and A.[InspectionDocumentID]='{1}'  ", Framework.SystemID, InspectionDocumentID);

            List<SqlParameter> parameters = new List<SqlParameter>();
            if (!string.IsNullOrWhiteSpace(InspectionDocumentID))
            {
                sql += " and A.InspectionDocumentID like @InspectionDocumentID";
                parameters.Add(new SqlParameter("@InspectionDocumentID", "%" + InspectionDocumentID + "%"));

            }

            SqlParameter[] paramArray = parameters.ToArray();
            count = UniversalService.getCount(sql, paramArray);

            string orderby = "A.[Sequence]";

            DataTable dt = UniversalService.getTable(select, sql, orderby, paramArray, page, rows);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// 制程检验确认-檢驗明細
        /// SAM 2017年7月9日21:22:39
        /// </summary>
        /// <param name="InspectionDocumentID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> QCS00006GetDetailList(string InspectionDocumentID, int page, int rows, ref int count)
        {
            string select = string.Format(@"select A.InspectionDocumentDetailID,
            D.Code as ItemCode,D.Name as ItemDesc,D.InspectionStandard as StandardDesc,
            E.Name as FaultDesc,A.SampleQuantity,A.Aql,A.AcQuantity,A.ReQuantity,A.NGquantity,A.Attribute,
            F.Name as QualityControlDecision,
            B.Emplno+'-'+B.UserName as Creator,A.CreateLocalTime as CreateTime,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else C.Emplno+'-'+C.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ");

            string sql = string.Format(@"  from [QCS_InspectionDocumentDetails] A 
            left join [SYS_MESUsers] B on A.[Creator] = B.[MESUserID]
            left join [SYS_MESUsers] C on A.[Modifier] = C.[MESUserID]
            left join [QCS_InspectionProject] D on A.[InspectionItemID] = D.[InspectionProjectID]
            left join [SYS_Parameters] E on A.[InspectionFaultID] = E.[ParameterID]
            left join [SYS_Parameters] F on A.[QualityControlDecision] = F.[ParameterID]
            where A.[SystemID]='{0}' and A.[Status] <> '{0}0201213000003' and A.[InspectionDocumentID] ='{1}'", Framework.SystemID, InspectionDocumentID);


            count = UniversalService.getCount(sql, null);

            string orderby = " A.[Status],A.[Sequence] ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, null, page, rows);

            return ToHashtableList(dt);
        }

        /// <summary>
        ///  制程检验单明细更新（移动端）
        ///  Alvin  2017年9月11日15:59:58
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool Qcs00005DetailUpdate(string userid, QCS_InspectionDocumentDetails Model)
        {
            try
            {               
                string sql = String.Format(@"update[QCS_InspectionDocumentDetails] set {0},
                    [InspectionDocumentID]=@InspectionDocumentID,
                    [NGquantity]=@NGquantity,[Attribute]=@Attribute,
                    [QualityControlDecision]=@QualityControlDecision,
                    [Comments]=@Comments where [InspectionDocumentDetailID]=@InspectionDocumentDetailID", UniversalService.getUpdateUTC(userid));
                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@InspectionDocumentDetailID",SqlDbType.VarChar),
                    new SqlParameter("@InspectionDocumentID",SqlDbType.VarChar),
                    new SqlParameter("@NGquantity",SqlDbType.Decimal),
                    new SqlParameter("@Attribute",SqlDbType.VarChar),
                    new SqlParameter("@QualityControlDecision",SqlDbType.VarChar),
                    new SqlParameter("@Comments",SqlDbType.NVarChar)
                    };

                parameters[0].Value = Model.InspectionDocumentDetailID;
                parameters[1].Value = (Object)Model.InspectionDocumentID ?? DBNull.Value;                
                parameters[2].Value = (Object)Model.NGquantity ?? DBNull.Value;
                parameters[3].Value = (Object)Model.Attribute ?? DBNull.Value;
                parameters[4].Value = (Object)Model.QualityControlDecision ?? DBNull.Value;
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
        /// 制程巡检--明细修改
        /// ALvin 2017年9月14日16:27:46
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool Qcs00008DetailUpdate(string userid, QCS_InspectionDocumentDetails Model)
        {
            try
            {
                string sql = String.Format(@"update[QCS_InspectionDocumentDetails] set {0},
                    [InspectionDocumentID]=@InspectionDocumentID,
                    [Attribute]=@Attribute,
                    [QualityControlDecision]=@QualityControlDecision,
                    [Comments]=@Comments where [InspectionDocumentDetailID]=@InspectionDocumentDetailID", UniversalService.getUpdateUTC(userid));
                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@InspectionDocumentDetailID",SqlDbType.VarChar),
                    new SqlParameter("@InspectionDocumentID",SqlDbType.VarChar),
                    new SqlParameter("@Attribute",SqlDbType.VarChar),
                    new SqlParameter("@QualityControlDecision",SqlDbType.VarChar),
                    new SqlParameter("@Comments",SqlDbType.NVarChar)
                    };

                parameters[0].Value = Model.InspectionDocumentDetailID;
                parameters[1].Value = (Object)Model.InspectionDocumentID ?? DBNull.Value;
                parameters[2].Value = (Object)Model.Attribute ?? DBNull.Value;
                parameters[3].Value = (Object)Model.QualityControlDecision ?? DBNull.Value;
                parameters[4].Value = (Object)Model.Comments ?? DBNull.Value;
                return SQLHelper.ExecuteNonQuery(sql, CommandType.Text, parameters) > 0;
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return false;
            }
        }


        /// <summary>
        /// 判断指定检验项目是否被使用中
        /// SAM 2017年10月9日14:26:14
        /// </summary>
        /// <param name="InspectionProjectID"></param>
        /// <returns></returns>
        public static bool CheckQcs00002Project(string InspectionProjectID)
        {
            string sql = string.Format(@"select Top 1 * from [QCS_InspectionDocumentDetails] where [SystemID]='{0}' and [Status] <> '{0}0201213000003' ", Framework.SystemID);

            /*先定义Name和Code，默认给DbNull,以免参数找不到*/
            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@InspectionItemID",SqlDbType.VarChar)
            };
            parameters[0].Value = DBNull.Value;

            if (!string.IsNullOrWhiteSpace(InspectionProjectID))
            {
                sql = sql + String.Format(@" and [InspectionItemID] =@InspectionItemID ");
                parameters[0].Value = InspectionProjectID;
            }

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text, parameters);

            if (dt.Rows.Count == 0)
                return false;
            else
                return true;
        }

        /// <summary>
        /// 判断制定的检验类别是否已被使用
        /// Sam 2017年10月17日16:26:18
        /// </summary>
        /// <param name="InspectionClassID"></param>
        /// <returns></returns>
        public static bool CheckQcs00002Type(string InspectionClassID)
        {
            string sql = string.Format(@"select Top 1 * from [QCS_InspectionDocumentDetails] where [SystemID]='{0}' and [Status] <> '{0}0201213000003' ", Framework.SystemID);

            /*先定义Name和Code，默认给DbNull,以免参数找不到*/
            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@InspectionClassID",SqlDbType.VarChar)
            };
            parameters[0].Value = DBNull.Value;

            if (!string.IsNullOrWhiteSpace(InspectionClassID))
            {
                sql = sql + String.Format(@" and [InspectionClassID] =@InspectionClassID ");
                parameters[0].Value = InspectionClassID;
            }

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text, parameters);

            if (dt.Rows.Count == 0)
                return false;
            else
                return true;
        }

        /// <summary>
        /// 根据表头流水号获取它所有的明细流水号
        /// Mouse 2017年11月1日17:06:09
        /// </summary>
        /// <param name="InspectionDocumentID"></param>
        /// <returns></returns>
        public static IList<Hashtable> GetAllDetail(string InspectionDocumentID)
        {
            string sql = string.Format(@"select InspectionDocumentDetailID from [QCS_InspectionDocumentDetails] 
                            where SystemID='{0}' and Status='{0}0201213000001' and InspectionDocumentID='{1}'", Framework.SystemID, InspectionDocumentID);
            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);
            return ToHashtableList(dt);
        }
    }
}

