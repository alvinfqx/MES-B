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
    public class SYS_CustomersService : SuperModel<SYS_Customers>
    {
        /// <summary>
        /// 添加
        /// SAM 2017年4月27日10:21:57
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="Model"></param>
        /// <returns></returns>
        public static bool insert(string userId, SYS_Customers Model)
        {
            try
            {
                string sql = string.Format(@"insert[SYS_Customers]([CustomerID],[Code],[Name],[Contacts],[Email],[MESUserID],
                [ClassOne],[ClassTwo],[Status],
                [Comments],[Modifier],[ModifiedTime],[ModifiedLocalTime],
                [Creator],[CreateTime],[CreateLocalTime],[SystemID]) values
                (@CustomerID,@Code,@Name,@Contacts,@Email,@MESUserID,
                @ClassOne,@ClassTwo,@Status,@Comments,'{0}','{1}','{2}','{0}','{1}','{2}','{3}')", userId, DateTime.Now, DateTime.Now, Framework.SystemID);

                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@CustomerID",SqlDbType.VarChar),
                    new SqlParameter("@Code",SqlDbType.VarChar),
                    new SqlParameter("@Name",SqlDbType.VarChar),
                    new SqlParameter("@Contacts",SqlDbType.VarChar),
                    new SqlParameter("@Email",SqlDbType.VarChar),
                    new SqlParameter("@MESUserID",SqlDbType.VarChar),
                    new SqlParameter("@ClassOne",SqlDbType.VarChar),
                    new SqlParameter("@ClassTwo",SqlDbType.VarChar),
                    new SqlParameter("@Status",SqlDbType.VarChar),
                    new SqlParameter("@Comments",SqlDbType.VarChar),
                    };

                parameters[0].Value = (Object)Model.CustomerID ?? DBNull.Value;
                parameters[1].Value = (Object)Model.Code ?? DBNull.Value;
                parameters[2].Value = (Object)Model.Name ?? DBNull.Value;
                parameters[3].Value = (Object)Model.Contacts ?? DBNull.Value;
                parameters[4].Value = (Object)Model.Email ?? DBNull.Value;
                parameters[5].Value = (Object)Model.MESUserID ?? DBNull.Value;
                parameters[6].Value = (Object)Model.ClassOne ?? DBNull.Value;
                parameters[7].Value = (Object)Model.ClassTwo ?? DBNull.Value;
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
        /// SAM 2017年4月27日10:21:40
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="Model"></param>
        /// <returns></returns>
        public static bool update(string userId, SYS_Customers Model)
        {
            try
            {
                string sql = String.Format(@"update[SYS_Customers] set {0},
                [Contacts]=@Contacts,[Email]=@Email,[MESUserID]=@MESUserID,
                [ClassOne]=@ClassOne,[ClassTwo]=@ClassTwo,
                [Status]=@Status,[Name]=@Name,[Comments]=@Comments
                where [CustomerID]=@CustomerID", UniversalService.getUpdateUTC(userId));
                
                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@CustomerID",SqlDbType.VarChar),
                    new SqlParameter("@Contacts",SqlDbType.VarChar),
                    new SqlParameter("@Email",SqlDbType.VarChar),
                    new SqlParameter("@MESUserID",SqlDbType.VarChar),
                    new SqlParameter("@ClassOne",SqlDbType.VarChar),
                    new SqlParameter("@ClassTwo",SqlDbType.VarChar),
                    new SqlParameter("@Status",SqlDbType.VarChar),
                    new SqlParameter("@Name",SqlDbType.VarChar),
                    new SqlParameter("@Comments",SqlDbType.VarChar),
                    };

                parameters[0].Value = Model.CustomerID;
                parameters[1].Value = (Object)Model.Contacts ?? DBNull.Value;
                parameters[2].Value = (Object)Model.Email ?? DBNull.Value;
                parameters[3].Value = (Object)Model.MESUserID ?? DBNull.Value;
                parameters[4].Value = (Object)Model.ClassOne ?? DBNull.Value;
                parameters[5].Value = (Object)Model.ClassTwo ?? DBNull.Value;
                parameters[6].Value = (Object)Model.Status ?? DBNull.Value;
                parameters[7].Value = (Object)Model.Name ?? DBNull.Value;
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
        /// 获取单一实体
        /// SAM 2017年4月27日10:22:54
        /// </summary>
        /// <param name="CustomerID"></param>
        /// <returns></returns>
        public static SYS_Customers get(string CustomerID)
        {
            string sql = string.Format(@"select Top 1 * from [SYS_Customers] where [CustomerID] = '{0}'  and [SystemID] = '{1}' ", CustomerID, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return null;
            else
                return ToEntity(dt.Rows[0]);
        }

        /// <summary>
        /// 判断代号是否重复
        /// SAM 2017年4月27日10:23:52
        /// </summary>
        /// <param name="Code">代号</param>
        /// <param name="CustomerID"></param>
        /// <returns></returns>
        public static bool CheckCode(string Code, string CustomerID)
        {
            string sql = String.Format(@"select Top 1 * from [SYS_Customers] where [SystemID]='{0}' and [Status] <> '{0}0201213000003' ", Framework.SystemID);

            /*先定义Code，默认给DbNull,以免参数找不到*/
            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@Code",SqlDbType.VarChar),
            };
            parameters[0].Value = DBNull.Value;

            /*因为Code是通过手动输入的，所以需要用参数的形式去拼SQL*/
            if (!string.IsNullOrWhiteSpace(Code))
            {
                sql = sql + String.Format(@" and [Code] =@Code ");
                parameters[0].Value = Code;
            }

            /*CustomerID（参数流水号）用于在更新时，排除他自己*/
            if (!string.IsNullOrWhiteSpace(CustomerID))
                sql = sql + String.Format(@" and [CustomerID] <> '{0}' ", CustomerID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text, parameters);

            if (dt.Rows.Count == 0)
                return false;
            else
                return true;
        }

        /// <summary>
        /// 获取客户主档列表
        /// SAM 2017年4月27日10:09:29
        /// </summary>
        /// <param name="Code">代号</param>
        /// <param name="Name"></param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> Inf00007getList(string Code,string Name, int page, int rows, ref int count)
        {
            string select = string.Format(@"select A.CustomerID,A.Code,A.Name,A.Status,
            A.Comments,A.Contacts,A.Email,A.MESUserID,F.Code as ClassOneCode,G.Code as ClassTwoCode,
            A.ClassOne,A.ClassTwo,B.UserName as MESUserName,B.Emplno as MESUserCode,
            D.UserName as Creator,A.CreateLocalTime as CreateTime,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else E.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ");

            string sql = string.Format(@"  from [SYS_Customers] A 
            left join [SYS_MESUsers] B on A.MESUserID= B.MESUserID
            left join [SYS_MESUsers] D on A.Creator = D.MESUserID
            left join [SYS_MESUsers] E on A.Modifier = E.MESUserID
            left join [SYS_Parameters] F on A.ClassOne =F.ParameterID
            left join [SYS_Parameters] G on A.ClassTwo =G.ParameterID
            where A.[SystemID]='{0}' and A.[Status] <> '{0}0201213000003'", Framework.SystemID);

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

            count = UniversalService.getCount(sql, Parcount);

            String orderby = "A.[Code] ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, parameters, page, rows);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// 获取导出数据
        /// SAM 2017年4月27日11:18:10
        /// </summary>
        /// <param name="Code">代号</param>
        /// <param name="Name"></param>
        /// <returns></returns>
        public static DataTable GetExportList(string Code,string Name)
        {
            string select = string.Format(@"select ROW_NUMBER() OVER (ORDER BY A.Code),A.Code,A.Name,A.Contacts,
            A.Email,B.Emplno as MesCode,B.UserName as MesName,
            F.Code as ClassOneCode,G.Code as ClassTwoCode,A.Comments,
            H.Name as StatusName,  
            D.UserName as Creator,A.CreateLocalTime as CreateTime,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else E.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ");

            string sql = string.Format(@"  from [SYS_Customers] A 
            left join [SYS_MESUsers] B on A.MESUserID= B.MESUserID
            left join [SYS_MESUsers] D on A.Creator = D.MESUserID
            left join [SYS_MESUsers] E on A.Modifier = E.MESUserID
            left join [SYS_Parameters] F on A.ClassOne = F.ParameterID
            left join [SYS_Parameters] G on A.ClassTwo = G.ParameterID
            left join [SYS_Parameters] H on A.Status = H.ParameterID
            where A.[SystemID]='{0}' and A.[Status] <> '{0}0201213000003'", Framework.SystemID);

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

            string orderBy = "order By A.Code ";

            return SQLHelper.ExecuteDataTable(select + sql + orderBy,CommandType.Text, parameters);
        }

        /// <summary>
        /// 客户的弹窗
        /// SAM 2017年6月21日16:41:22
        /// </summary>
        /// <param name="Code"></param>
        /// <param name="Name"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> GetCustomerList(string Code, int page, int rows, ref int count)
        {
            string select = string.Format(@"select A.CustomerID,A.Code,A.Name,F.Name as Status,
            D.UserName as Creator,A.CreateLocalTime as CreateTime,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else E.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ");

            string sql = string.Format(@"  from [SYS_Customers] A 
            left join [SYS_MESUsers] D on A.Creator = D.MESUserID
            left join [SYS_MESUsers] E on A.Modifier = E.MESUserID
            left join [SYS_Parameters] F on A.Status =F.ParameterID
            where A.[SystemID]='{0}' and A.[Status] <> '{0}0201213000003'", Framework.SystemID);

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
                sql = sql + String.Format(@" and A.[Code] like @Code ");
                parameters[0].Value = Code;
                Parcount[0].Value = Code;
            }

            count = UniversalService.getCount(sql, Parcount);

            String orderby = "A.[Status],A.[Code] ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, parameters, page, rows);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// 获取客户列表
        /// SAM 2017年9月29日15:36:46
        /// </summary>
        /// <param name="Code"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> GetVaildList(string Code, int page, int rows, ref int count)
        {
            string select = string.Format(@"select A.CustomerID,A.Code,A.Name,A.Comments,B.Emplno as MesCode, B.UserName as MesName, A.MESUserID");

            string sql = string.Format(
                @"  from [SYS_Customers] A 
                    left join [SYS_MESUsers] B on A.MESUserID= B.MESUserID
                    where A.[SystemID]='{0}' and A.[Status] = '{0}0201213000001'", Framework.SystemID);

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
                sql = sql + String.Format(@" and A.[Code] like @Code ");
                parameters[0].Value = Code;
                Parcount[0].Value = Code;
            }

            count = UniversalService.getCount(sql, Parcount);

            String orderby = "A.[Code] asc";

            DataTable dt = UniversalService.getTable(select, sql, orderby, parameters, page, rows);

            return ToHashtableList(dt);
        }


        /// <summary>
        /// 判断用户是否在客户中使用
        /// SAM 2017年7月30日23:48:04
        /// </summary>
        /// <param name="MESUserID"></param>
        /// <returns></returns>
        public static bool CheckUser(string MESUserID)
        {
            string sql = string.Format(@"select * from [SYS_Customers] where [MESUserID] = '{0}' and [SystemID] = '{1}' and [Status] <> '{1}0201213000003'", MESUserID, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return false;
            else
                return true;
        }

        /// <summary>
        /// 根据代号获取客户信息
        /// SAM 2017年9月29日15:37:52
        /// </summary>
        /// <param name="Code"></param>
        /// <returns></returns>
        public static SYS_Customers getByCode(string Code)
        {
            string sql = string.Format(@"select Top 1 * from [SYS_Customers] where [Code] = '{0}'  and [SystemID] = '{1}'  and [Status]='{1}0201213000001' ", Code, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return null;
            else
                return ToEntity(dt.Rows[0]);
        }

        /// <summary>
        /// 获取正常的客户列表
        /// SAM 2017年10月4日00:11:54
        /// </summary>
        /// <returns></returns>
        public static IList<SYS_Customers> GetList()
        {
            string sql = string.Format(@"select * from [SYS_Customers] where [SystemID] = '{0}'  and [Status]='{0}0201213000001' ", Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return null;
            else
                return ToList(dt);
        }
    }
}

