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
    public class SYS_DocumentTypeSettingService : SuperModel<SYS_DocumentTypeSetting>
    {
        /// <summary>
        /// 新增
        /// SAM 2017年6月1日11:49:20
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="Model"></param>
        /// <returns></returns>
        public static bool insert(string userId, SYS_DocumentTypeSetting Model)
        {
            try
            {
                string sql = string.Format(@"insert[SYS_DocumentTypeSetting]([DTSID],[Code],[Name],
                [Status],[TypeID],[IfDefault],[GiveWay],[YearLength],
                [MonthLength],[DateLength],[Attribute],[Length],[CodeLength],
                [YearType],[Comments],[Modifier],[ModifiedTime],[ModifiedLocalTime],[Creator],[CreateTime],[CreateLocalTime],[SystemID]) values
                 (@DTSID,@Code,@Name,@Status,@TypeID,@IfDefault,@GiveWay,@YearLength,
                @MonthLength,@DateLength,@Attribute,@Length,@CodeLength,@YearType,@Comments,'{0}','{1}','{2}','{0}','{1}','{2}','{3}')", userId, DateTime.Now, DateTime.Now, Framework.SystemID);

                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@DTSID",SqlDbType.VarChar),
                    new SqlParameter("@Code",SqlDbType.NVarChar),
                    new SqlParameter("@Name",SqlDbType.NVarChar),
                    new SqlParameter("@Status",SqlDbType.VarChar),
                    new SqlParameter("@TypeID",SqlDbType.VarChar),
                    new SqlParameter("@IfDefault",SqlDbType.Bit),
                    new SqlParameter("@GiveWay",SqlDbType.VarChar),
                    new SqlParameter("@YearLength",SqlDbType.TinyInt),
                    new SqlParameter("@MonthLength",SqlDbType.TinyInt),
                    new SqlParameter("@DateLength",SqlDbType.TinyInt),
                    new SqlParameter("@Attribute",SqlDbType.Bit),
                    new SqlParameter("@Length",SqlDbType.TinyInt),
                    new SqlParameter("@YearType",SqlDbType.VarChar),
                    new SqlParameter("@Comments",SqlDbType.NVarChar),
                    new SqlParameter("@CodeLength",SqlDbType.TinyInt)
                    };

                parameters[0].Value = (Object)Model.DTSID ?? DBNull.Value;
                parameters[1].Value = (Object)Model.Code ?? DBNull.Value;
                parameters[2].Value = (Object)Model.Name ?? DBNull.Value;
                parameters[3].Value = (Object)Model.Status ?? DBNull.Value;
                parameters[4].Value = (Object)Model.TypeID ?? DBNull.Value;
                parameters[5].Value = (Object)Model.IfDefault ?? DBNull.Value;
                parameters[6].Value = (Object)Model.GiveWay ?? DBNull.Value;
                parameters[7].Value = (Object)Model.YearLength ?? DBNull.Value;
                parameters[8].Value = (Object)Model.MonthLength ?? DBNull.Value;
                parameters[9].Value = (Object)Model.DateLength ?? DBNull.Value;
                parameters[10].Value = (Object)Model.Attribute ?? DBNull.Value;
                parameters[11].Value = (Object)Model.Length ?? DBNull.Value;
                parameters[12].Value = (Object)Model.YearType ?? DBNull.Value;
                parameters[13].Value = (Object)Model.Comments ?? DBNull.Value;
                parameters[14].Value = (Object)Model.CodeLength ?? DBNull.Value;
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
        /// SAM 2017年6月1日11:49:15
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="Model"></param>
        /// <returns></returns>
        public static bool update(string userId, SYS_DocumentTypeSetting Model)
        {
            try
            {
                string sql = String.Format(@"update[SYS_DocumentTypeSetting] set {0},
                [Name]=@Name,[Status]=@Status,[CodeLength]=@CodeLength,
                [TypeID]=@TypeID,[IfDefault]=@IfDefault,[GiveWay]=@GiveWay,[YearLength]=@YearLength,
                [MonthLength]=@MonthLength,[DateLength]=@DateLength,[Attribute]=@Attribute,[Length]=@Length,
                [YearType]=@YearType,[Comments]=@Comments where [DTSID]=@DTSID", UniversalService.getUpdateUTC(userId));

                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@DTSID",SqlDbType.VarChar),
                    new SqlParameter("@Name",SqlDbType.NVarChar),
                    new SqlParameter("@Status",SqlDbType.VarChar),
                    new SqlParameter("@TypeID",SqlDbType.VarChar),
                    new SqlParameter("@IfDefault",SqlDbType.Bit),
                    new SqlParameter("@GiveWay",SqlDbType.VarChar),
                    new SqlParameter("@YearLength",SqlDbType.TinyInt),
                    new SqlParameter("@MonthLength",SqlDbType.TinyInt),
                    new SqlParameter("@DateLength",SqlDbType.TinyInt),
                    new SqlParameter("@Attribute",SqlDbType.Bit),
                    new SqlParameter("@Length",SqlDbType.TinyInt),
                    new SqlParameter("@YearType",SqlDbType.VarChar),
                    new SqlParameter("@Comments",SqlDbType.NVarChar),
                    new SqlParameter("@CodeLength",SqlDbType.TinyInt)
                    };

                parameters[0].Value = Model.DTSID;
                parameters[1].Value = (Object)Model.Name ?? DBNull.Value;
                parameters[2].Value = (Object)Model.Status ?? DBNull.Value;
                parameters[3].Value = (Object)Model.TypeID ?? DBNull.Value;
                parameters[4].Value = (Object)Model.IfDefault ?? DBNull.Value;
                parameters[5].Value = (Object)Model.GiveWay ?? DBNull.Value;
                parameters[6].Value = (Object)Model.YearLength ?? DBNull.Value;
                parameters[7].Value = (Object)Model.MonthLength ?? DBNull.Value;
                parameters[8].Value = (Object)Model.DateLength ?? DBNull.Value;
                parameters[9].Value = (Object)Model.Attribute ?? DBNull.Value;
                parameters[10].Value = (Object)Model.Length ?? DBNull.Value;
                parameters[11].Value = (Object)Model.YearType ?? DBNull.Value;
                parameters[12].Value = (Object)Model.Comments ?? DBNull.Value;
                parameters[13].Value = (Object)Model.CodeLength ?? DBNull.Value;

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
        /// SAM 2017年6月1日11:49:06
        /// </summary>
        /// <param name="DTSID"></param>
        /// <returns></returns>
        public static SYS_DocumentTypeSetting get(string DTSID)
        {
            string sql = string.Format(@"select Top 1 * from [SYS_DocumentTypeSetting] where [DTSID] = '{0}'  and [SystemID] = '{1}' ", DTSID, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return null;
            else
                return ToEntity(dt.Rows[0]);
        }



        /// <summary>
        /// 根据代号获取正常的单据自动编号
        /// SAM  2017年5月24日11:30:32
        /// </summary>
        /// <param name="Code">代号</param>
        /// <returns></returns>
        public static SYS_DocumentTypeSetting getByCode(string Code)
        {
            string sql = string.Format(@"select Top 1 * from [SYS_DocumentTypeSetting] where [Code] = '{0}'  and [SystemID] = '{1}' and [Status]='{1}0201213000001'", Code, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return null;
            else
                return ToEntity(dt.Rows[0]);
        }

        /// <summary>
        /// 根据单据种别获取第一个单据类别(以代号排序)
        /// 不需要檢核單據部門員工權限表，预设为Y
        /// SAM 2017年8月3日23:39:25
        /// </summary>
        /// <param name="Code"></param>
        /// <returns></returns>
        public static SYS_DocumentTypeSetting getByType(string Type)
        {
            string sql = string.Format(@"select Top 1 * from [SYS_DocumentTypeSetting] where [TypeID] = '{0}'  and [SystemID] = '{1}' and [Status]='{1}0201213000001' and [IfDefault]=1 ", Type, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return null;
            else
                return ToEntity(dt.Rows[0]);
        }


        /// <summary>
        /// 判断代号是否重复
        /// SAM 2017年5月16日15:38:13
        /// </summary>
        /// <param name="Code">代号</param>
        /// <param name="ItemID"></param>
        /// <returns></returns>
        public static bool CheckCode(string Code, string DTSID)
        {
            string sql = String.Format(@"select Top 1 * from [SYS_DocumentTypeSetting] where [SystemID]='{0}' and Status <> '{0}0201213000003'", Framework.SystemID);

            /*先定义Code，默认给DbNull,以免参数找不到*/
            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@Code",SqlDbType.VarChar)
            };
            parameters[0].Value = DBNull.Value;

            /*因为Code是通过手动输入的，所以需要用参数的形式去拼SQL*/
            if (!string.IsNullOrWhiteSpace(Code))
            {
                sql = sql + String.Format(@" and [Code] =@Code ");
                parameters[0].Value = Code;
            }

            /*DTSID（流水号）用于在更新时，排除他自己*/
            if (!string.IsNullOrWhiteSpace(DTSID))
                sql = sql + String.Format(@" and [DTSID] <> '{0}' ", DTSID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text, parameters);

            if (dt.Rows.Count == 0)
                return false;
            else
                return true;
        }

        /// <summary>
        /// 单据类别设定表
        /// SAM 2017年6月1日15:27:12
        /// </summary>
        /// <param name="TypeCode"></param>
        /// <param name="Code">代号</param>
        /// <param name="Status">状态</param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> Inf00016GetList(string TypeCode, string Code, string Status, int page, int rows, ref int count)
        {
            string select = string.Format(@"select A.DTSID,A.Code,A.Name,A.Status,A.TypeID,A.IfDefault,A.GiveWay,A.YearLength,A.MonthLength,A.DateLength,
            A.Attribute,A.Length,A.YearType,A.Comments,A.CodeLength, 
            D.Code as TypeCode,D.Name as TypeName,  
            B.UserName as Creator,A.CreateLocalTime as CreateTime,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else C.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ");

            string sql = string.Format(@" from [SYS_DocumentTypeSetting] A 
            left join [SYS_MESUsers] B on A.Creator = B.MESUserID
            left join [SYS_MESUsers] C on A.Modifier = C.MESUserID
            left join [SYS_Parameters] D on A.TypeID = D.ParameterID
            where A.[SystemID]='{0}' and A.Status <> '{0}0201213000003' ", Framework.SystemID);

            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@Code",SqlDbType.VarChar),
                new SqlParameter("@Status",SqlDbType.VarChar),
                new SqlParameter("@TypeCode",SqlDbType.VarChar)
            };
            parameters[0].Value = DBNull.Value;
            parameters[1].Value = DBNull.Value;
            parameters[2].Value = DBNull.Value;

            SqlParameter[] Parcount = new SqlParameter[]{
                new SqlParameter("@Code",SqlDbType.VarChar),
                new SqlParameter("@Status",SqlDbType.VarChar),
                new SqlParameter("@TypeCode",SqlDbType.VarChar)
            };
            Parcount[0].Value = DBNull.Value;
            Parcount[1].Value = DBNull.Value;
            Parcount[2].Value = DBNull.Value;

            if (!string.IsNullOrWhiteSpace(Code))
            {
                Code = "%" + Code + "%";
                sql = sql + string.Format(@" and A.[Code] like @Code ");
                parameters[0].Value = Code;
                Parcount[0].Value = Code;
            }

            if (!string.IsNullOrWhiteSpace(Status))
            {
                sql = sql + string.Format(@" and A.[Status] = @Status ");
                parameters[1].Value = Status;
                Parcount[1].Value = Status;
            }

            if (!string.IsNullOrWhiteSpace(TypeCode))
            {
                TypeCode = "%" + TypeCode + "%";
                sql = sql + string.Format(@" and D.[Code] like @TypeCode ");
                parameters[2].Value = TypeCode;
                Parcount[2].Value = TypeCode;
            }

            count = UniversalService.getCount(sql, Parcount);

            string orderby = "A.[Status],A.[Code] ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, parameters, page, rows);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// 单据子轨设定的导出
        /// SAM 2017年6月2日14:55:52
        /// </summary>
        /// <param name="TypeCode"></param>
        /// <param name="Code">代号</param>
        /// <param name="Status">状态</param>
        /// <returns></returns>
        public static DataTable Inf00016Export(string TypeCode, string Code, string Status)
        {
            string select = string.Format(@"select ROW_NUMBER() OVER (ORDER BY A.[Status],A.[Code]),
             D.Code as TypeCode,D.Name as TypeName,A.Code,A.Name,F.Name as GiveWay,A.YearLength,A.MonthLength,A.DateLength,
            A.CodeLength,A.Length,G.Name as YearType,
            (CASE WHEN A.IfDefault=1 THEN '是' ELSE '否' END),E.Name as Status,(CASE WHEN A.Attribute=1 THEN '专属' ELSE '共用' END),A.Comments,
            B.UserName as Creator,A.CreateLocalTime as CreateTime,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else C.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ");

            string sql = string.Format(@" from [SYS_DocumentTypeSetting] A 
            left join [SYS_MESUsers] B on A.Creator = B.MESUserID
            left join [SYS_MESUsers] C on A.Modifier = C.MESUserID
            left join [SYS_Parameters] D on A.TypeID = D.ParameterID
            left join [SYS_Parameters] E on A.Status = E.ParameterID
            left join [SYS_Parameters] F on A.GiveWay = F.ParameterID
            left join [SYS_Parameters] G on A.YearType = G.ParameterID
            where A.[SystemID]='{0}' and A.Status <> '{0}0201213000003' ", Framework.SystemID);

            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@Code",SqlDbType.VarChar),
                new SqlParameter("@Status",SqlDbType.VarChar),
                new SqlParameter("@TypeCode",SqlDbType.VarChar)
            };
            parameters[0].Value = DBNull.Value;
            parameters[1].Value = DBNull.Value;
            parameters[2].Value = DBNull.Value;

            SqlParameter[] Parcount = new SqlParameter[]{
                new SqlParameter("@Code",SqlDbType.VarChar),
                new SqlParameter("@Status",SqlDbType.VarChar),
                new SqlParameter("@TypeCode",SqlDbType.VarChar)
            };
            Parcount[0].Value = DBNull.Value;
            Parcount[1].Value = DBNull.Value;
            Parcount[2].Value = DBNull.Value;

            if (!string.IsNullOrWhiteSpace(Code))
            {
                Code = "%" + Code + "%";
                sql = sql + string.Format(@" and A.[Code] like @Code ");
                parameters[0].Value = Code;
                Parcount[0].Value = Code;
            }

            if (!string.IsNullOrWhiteSpace(Status))
            {
                sql = sql + string.Format(@" and A.[Status] = @Status ");
                parameters[1].Value = Status;
                Parcount[1].Value = Status;
            }

            if (!string.IsNullOrWhiteSpace(TypeCode))
            {
                TypeCode = "%" + TypeCode + "%";
                sql = sql + string.Format(@" and D.[Code] like @TypeCode ");
                parameters[2].Value = TypeCode;
                Parcount[2].Value = TypeCode;
            }

            string orderBy = "order By A.[Status],A.[Code] ";

            return SQLHelper.ExecuteDataTable(select + sql + orderBy, CommandType.Text, parameters);
        }


        /// <summary>
        /// 用户及单据种别，获取该用户在该种别下，拥有的单据类别下拉框
        /// SAM 2017年7月30日20:16:56
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="Type"></param>
        /// <returns></returns>
        public static IList<Hashtable> GetTypeList(string userID, string Type)
        {
            string select = string.Format(
                @"select A.[DTSID] as value,A.Code,A.Name,A.Code as text,A.Code+'-'+A.Name as NewValue,A.IfDefault ");

            string sql = string.Format(
                @"from [SYS_DocumentTypeSetting] A 
                  where A.[SystemID]='{0}' and A.[Status] ='{0}0201213000001' and A.[TypeID] = '{1}' 
                    and (A.[DTSID] in (select [ClassID] from [SYS_DocumentAuthority] where [Status] ='{0}0201213000001' and [AuthorityID]='{2}' and [Attribute]=1) 
                    or A.[DTSID] in (select [ClassID] from [SYS_DocumentAuthority] where [Status] ='{0}0201213000001' and [Attribute]=0 and [AuthorityID] =(select Top 1 [OrganizationID] from [SYS_UserOrganizationMapping] where [UserID]='{2}')))
                   ", Framework.SystemID, Type, userID);

            string orderby = " order by A.IfDefault Desc,A.Code ";

            DataTable dt = SQLHelper.ExecuteDataTable(select + sql + orderby, CommandType.Text);

            return ToHashtableList(dt);
        }


    }
}

