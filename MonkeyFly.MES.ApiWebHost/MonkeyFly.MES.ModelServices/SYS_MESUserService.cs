using MonkeyFly.Core;
using MonkeyFly.MES.Models;
using MonkeyFly.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Data.SqlClient;
using MonkeyFly.MES.BasicService;

namespace MonkeyFly.MES.ModelServices
{
    public class SYS_MESUserService : SuperModel<SYS_MESUsers>
    {
        /// <summary>
        /// 根据代号获取账户
        /// SAM 2017年4月27日10:50:33
        /// </summary>
        /// <param name="Code">代号</param>
        /// <returns></returns>
        public static SYS_MESUsers getByCode(string Code)
        {
            string sql = string.Format(@"select Top 1 * from [SYS_MESUsers] where [Emplno] = '{0}'  and [SystemID] = '{1}'  and Status <> 2 ", Code, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return null;
            else
                return ToEntity(dt.Rows[0]);
        }

        /// <summary>
        /// 根据姓名获取账号
        /// Joint 2017年8月3日10:06:38
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        public static SYS_MESUsers getByName(string Name)
        {
            string sql = string.Format(@"select Top 1 * from [SYS_MESUsers] where [UserName] = '{0}'  and [SystemID] = '{1}'  and Status <> 2 ",Name, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return null;
            else
                return ToEntity(dt.Rows[0]);
        }

        /// <summary>
        /// 修改密码
        /// Tom 2017年4月27日11:24:40
        /// </summary>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        public static bool ResetPassword(string userID, string newPassword)
        {
            string sql = string.Format(
                @"update SYS_MESUsers
                  set Password = '{0}',
                      {2}
                  where MESUserID = '{1}'",
                newPassword, userID, UniversalService.getUpdateUTC(userID));

            //if (MESSQLHelper.exec(sql) > 0)
            if (SQLHelper.ExecuteNonQuery(sql) > 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 账户弹窗
        /// SAM 2017年4月28日14:47:03
        /// Mouse 2017年9月18日10:30:13  修改字段查询不分大小写
        /// </summary>
        /// <param name="Code">代号</param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> GetUserList(string Account,string Code, string UserName, string StartCode, string EndCode, int page, int rows, ref int count)
        {
            string select = string.Format(@"select A.MESUserID,A.Emplno,A.UserName,A.Status,A.Account,
            (select top 1 OrganizationID from SYS_UserOrganizationMapping where UserID =A.MESUserID) as OrganizationID,
            (select Code from [SYS_Organization] where OrganizationID = (select top 1 OrganizationID from SYS_UserOrganizationMapping where UserID =A.MESUserID))  as OrganizationCode,
            (select Name from [SYS_Organization] where OrganizationID = (select top 1 OrganizationID from SYS_UserOrganizationMapping where UserID =A.MESUserID))  as OrganizationName ");

            string sql = string.Format(@"  from [SYS_MESUsers] A 
            where A.[SystemID]='{0}' and A.[Status] = 1  and A.[UserType]=10 ", Framework.SystemID);

            //SqlParameter[] parameters = new SqlParameter[]{
            //    new SqlParameter("@Code",SqlDbType.VarChar),
            //    new SqlParameter("@StartCode",SqlDbType.VarChar),
            //    new SqlParameter("@EndCode",SqlDbType.VarChar),
            //    new SqlParameter("@Account",SqlDbType.VarChar),
            //    new SqlParameter("@UserName",SqlDbType.VarChar)
            //};
            //parameters[0].Value = DBNull.Value;
            //parameters[1].Value = DBNull.Value;
            //parameters[2].Value = DBNull.Value;
            //parameters[3].Value = DBNull.Value;
            //parameters[4].Value = DBNull.Value;

            //SqlParameter[] Parcount = new SqlParameter[]{
            //    new SqlParameter("@Code",SqlDbType.VarChar),
            //    new SqlParameter("@StartCode",SqlDbType.VarChar),
            //    new SqlParameter("@EndCode",SqlDbType.VarChar),
            //    new SqlParameter("@Account",SqlDbType.VarChar),
            //    new SqlParameter("@UserName",SqlDbType.VarChar)
            //};
            //Parcount[0].Value = DBNull.Value;
            //Parcount[1].Value = DBNull.Value;
            //Parcount[2].Value = DBNull.Value;
            //Parcount[3].Value = DBNull.Value;
            //Parcount[4].Value = DBNull.Value;

            //if (!string.IsNullOrWhiteSpace(Code))
            //{
            //    Code = "%" + Code + "%";
            //    sql = sql + string.Format(@" and A.[Emplno]  like @Code ");
            //    parameters[0].Value = Code;
            //    Parcount[0].Value = Code;
            //}

            //if (!string.IsNullOrWhiteSpace(UserName))
            //{
            //    UserName = "%" + UserName + "%";
            //    sql = sql + string.Format(@" and A.[UserName]  like @UserName ");
            //    parameters[4].Value = UserName;
            //    Parcount[4].Value = UserName;
            //}

            //if (!string.IsNullOrWhiteSpace(StartCode))
            //{
            //    sql = sql + string.Format(@" and A.[Emplno] >= @StartCode ");
            //    parameters[1].Value = StartCode;
            //    Parcount[1].Value = StartCode;
            //}

            //if (!string.IsNullOrWhiteSpace(EndCode))
            //{
            //    sql = sql + string.Format(@" and A.[Emplno] <= @EndCode ");
            //    parameters[2].Value = EndCode;
            //    Parcount[2].Value = EndCode;
            //}

            //if (!string.IsNullOrWhiteSpace(Account))
            //{
            //    Account = "%" + Account + "%";
            //    sql = sql + string.Format(@" and A.[Account]  like @Account ");
            //    parameters[3].Value = Account;
            //    Parcount[3].Value = Account;
            //}

            //count = UniversalService.getCount(sql, Parcount);

            List<SqlParameter> parameters = new List<SqlParameter>();
            if (!string.IsNullOrWhiteSpace(Code))
            {
                sql += " and A.Emplno collate Chinese_PRC_CI_AS like @Code";
                parameters.Add(new SqlParameter("@Code", "%" + Code.Trim() + "%"));

            }

            if (!string.IsNullOrWhiteSpace(UserName))
            {
                sql += " and A.UserName collate Chinese_PRC_CI_AS like @UserName";
                parameters.Add(new SqlParameter("@UserName", "%" + UserName.Trim() + "%"));

            }

            if (!string.IsNullOrWhiteSpace(StartCode))
            {
                sql += " and A.[Emplno] >= @StartCode ";
                parameters.Add(new SqlParameter("@StartCode", "%" + StartCode + "%"));

            }
            if (!string.IsNullOrWhiteSpace(EndCode))
            {
                sql += " and A.[Emplno] <= @EndCode ";
                parameters.Add(new SqlParameter("@EndCode", "%" + EndCode + "%"));

            }
            if (!string.IsNullOrWhiteSpace(Account))
            {
                sql += " and A.Account collate Chinese_PRC_CI_AS like @Account";
                parameters.Add(new SqlParameter("@Account", "%" + Account.Trim() + "%"));

            }
            SqlParameter[] paramArray = parameters.ToArray();
            count = UniversalService.getCount(sql, paramArray);

            string orderby = "A.[Emplno] ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, paramArray, page, rows);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// 新增
        /// SAM 2017年5月2日17:40:00
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="Model"></param>
        /// <returns></returns>
        public static bool insert(string userId, SYS_MESUsers Model)
        {
            try
            {
                string sql = string.Format(@"insert[SYS_MESUsers]([MESUserID],[Account],[Password],[UserType],[Status],[UserName],[EnglishName],[Emplno],
            [CardCode],[JobTitle],[Sex],[Email],[Brith],[IDcard],[InTime],[Type],[Mobile],[ConfigJSON],[Language],
            [Sequence],[Comments],[Modifier],[ModifiedTime],[ModifiedLocalTime],[Creator],[CreateTime],[CreateLocalTime],[SystemID]) values
            (@MESUserID,@Account,@Password,@UserType,@Status,@UserName,@EnglishName,@Emplno,@CardCode,@JobTitle,@Sex,@Email,@Brith,
            @IDcard,@InTime,@Type,@Mobile,@ConfigJSON,@Language,@Sequence,@Comments,'{0}','{1}','{2}','{0}','{1}','{2}','{3}')", userId, DateTime.Now, DateTime.Now, Framework.SystemID);

                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@MESUserID",SqlDbType.VarChar),
                    new SqlParameter("@Account",SqlDbType.VarChar),
                    new SqlParameter("@Password",SqlDbType.VarChar),
                    new SqlParameter("@UserType",SqlDbType.TinyInt),
                    new SqlParameter("@Status",SqlDbType.TinyInt),
                    new SqlParameter("@UserName",SqlDbType.VarChar),
                    new SqlParameter("@EnglishName",SqlDbType.VarChar),
                    new SqlParameter("@Emplno",SqlDbType.VarChar),
                    new SqlParameter("@CardCode",SqlDbType.VarChar),
                    new SqlParameter("@JobTitle",SqlDbType.VarChar),
                    new SqlParameter("@Sex",SqlDbType.Bit),
                    new SqlParameter("@Email",SqlDbType.VarChar),
                    new SqlParameter("@Brith",SqlDbType.VarChar),
                    new SqlParameter("@IDcard",SqlDbType.VarChar),
                    new SqlParameter("@InTime",SqlDbType.VarChar),
                    new SqlParameter("@Type",SqlDbType.VarChar),
                    new SqlParameter("@Mobile",SqlDbType.VarChar),
                    new SqlParameter("@ConfigJSON",SqlDbType.VarChar),
                    new SqlParameter("@Sequence",SqlDbType.Int),
                    new SqlParameter("@Comments",SqlDbType.VarChar),
                    new SqlParameter("@Language",SqlDbType.Int),
                    };

                parameters[0].Value = (Object)Model.MESUserID ?? DBNull.Value;
                parameters[1].Value = (Object)Model.Account ?? DBNull.Value;
                parameters[2].Value = (Object)Model.Password ?? DBNull.Value;
                parameters[3].Value = (Object)Model.UserType ?? DBNull.Value;
                parameters[4].Value = (Object)Model.Status ?? DBNull.Value;
                parameters[5].Value = (Object)Model.UserName ?? DBNull.Value;
                parameters[6].Value = (Object)Model.EnglishName ?? DBNull.Value;
                parameters[7].Value = (Object)Model.Emplno ?? DBNull.Value;
                parameters[8].Value = (Object)Model.CardCode ?? DBNull.Value;
                parameters[9].Value = (Object)Model.JobTitle ?? DBNull.Value;
                parameters[10].Value = (Object)Model.Sex ?? DBNull.Value;
                parameters[11].Value = (Object)Model.Email ?? DBNull.Value;
                parameters[12].Value = (Object)Model.Brith ?? DBNull.Value;
                parameters[13].Value = (Object)Model.IDcard ?? DBNull.Value;
                parameters[14].Value = (Object)Model.InTime ?? DBNull.Value;
                parameters[15].Value = (Object)Model.Type ?? DBNull.Value;
                parameters[16].Value = (Object)Model.Mobile ?? DBNull.Value;
                parameters[17].Value = (Object)Model.ConfigJSON ?? DBNull.Value;
                parameters[18].Value = (Object)Model.Sequence ?? DBNull.Value;
                parameters[19].Value = (Object)Model.Comments ?? DBNull.Value;
                parameters[20].Value = (Object)Model.Language ?? DBNull.Value;
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
        /// SAM 2017年5月2日17:40:43
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="Model"></param>
        /// <returns></returns>
        public static bool update(string userId, SYS_MESUsers Model)
        {
            try
            {
                string sql = String.Format(@"update[SYS_MESUsers] set {0},
                [Account]=@Account,[Status]=@Status,[UserName]=@UserName,[EnglishName]=@EnglishName,
                [Emplno]=@Emplno,[CardCode]=@CardCode,[JobTitle]=@JobTitle,[Sex]=@Sex,[Email]=@Email,
                [Brith]=@Brith,[IDcard]=@IDcard,[InTime]=@InTime,[Type]=@Type,[Mobile]=@Mobile,[Language]=@Language,
                [ConfigJSON]=@ConfigJSON,[Sequence]=@Sequence,[Comments]=@Comments where [MESUserID]=@MESUserID", UniversalService.getUpdateUTC(userId));

                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@MESUserID",SqlDbType.VarChar),
                    new SqlParameter("@Account",SqlDbType.VarChar),
                    new SqlParameter("@Status",SqlDbType.TinyInt),
                    new SqlParameter("@UserName",SqlDbType.VarChar),
                    new SqlParameter("@EnglishName",SqlDbType.VarChar),
                    new SqlParameter("@Emplno",SqlDbType.VarChar),
                    new SqlParameter("@CardCode",SqlDbType.VarChar),
                    new SqlParameter("@JobTitle",SqlDbType.VarChar),
                    new SqlParameter("@Sex",SqlDbType.Bit),
                    new SqlParameter("@Email",SqlDbType.VarChar),
                    new SqlParameter("@Brith",SqlDbType.VarChar),
                    new SqlParameter("@IDcard",SqlDbType.VarChar),
                    new SqlParameter("@InTime",SqlDbType.VarChar),
                    new SqlParameter("@Type",SqlDbType.VarChar),
                    new SqlParameter("@Mobile",SqlDbType.VarChar),
                    new SqlParameter("@ConfigJSON",SqlDbType.VarChar),
                    new SqlParameter("@Sequence",SqlDbType.Int),
                    new SqlParameter("@Comments",SqlDbType.VarChar),
                    new SqlParameter("@Language",SqlDbType.Int)
                    };

                parameters[0].Value = Model.MESUserID;
                parameters[1].Value = (Object)Model.Account ?? DBNull.Value;
                parameters[2].Value = (Object)Model.Status ?? DBNull.Value;
                parameters[3].Value = (Object)Model.UserName ?? DBNull.Value;
                parameters[4].Value = (Object)Model.EnglishName ?? DBNull.Value;
                parameters[5].Value = (Object)Model.Emplno ?? DBNull.Value;
                parameters[6].Value = (Object)Model.CardCode ?? DBNull.Value;
                parameters[7].Value = (Object)Model.JobTitle ?? DBNull.Value;
                parameters[8].Value = (Object)Model.Sex ?? DBNull.Value;
                parameters[9].Value = (Object)Model.Email ?? DBNull.Value;
                parameters[10].Value = (Object)Model.Brith ?? DBNull.Value;
                parameters[11].Value = (Object)Model.IDcard ?? DBNull.Value;
                parameters[12].Value = (Object)Model.InTime ?? DBNull.Value;
                parameters[13].Value = (Object)Model.Type ?? DBNull.Value;
                parameters[14].Value = (Object)Model.Mobile ?? DBNull.Value;
                parameters[15].Value = (Object)Model.ConfigJSON ?? DBNull.Value;
                parameters[16].Value = (Object)Model.Sequence ?? DBNull.Value;
                parameters[17].Value = (Object)Model.Comments ?? DBNull.Value;
                parameters[18].Value = (Object)Model.Language ?? DBNull.Value;
                return SQLHelper.ExecuteNonQuery(sql, CommandType.Text, parameters) > 0;
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return false;
            }
        }

        /// <summary>
        /// 获取生管列表
        /// Tom
        /// </summary>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> GetProductManagerList(string Account, int page, int rows, ref int count)
        {
            string select = string.Format(@"select A.MESUserID, A.Account, A.UserName, A.Comments ");

            string sql = string.Format(
                @"from [SYS_MESUsers] A 
                  where A.SystemID = '{0}' and
                        A.Status = 1 and 
                        exists(select * from SYS_UserRoleMapping where UserID = A.MESUserID and RoleID = '{0}0601213000001')", 
                Framework.SystemID);

            List<SqlParameter> parameters = new List<SqlParameter>();
            if (!string.IsNullOrWhiteSpace(Account))
            {
                sql += " and A.Emplno like '%' + @Account + '%'";
                SqlParameter sp = new SqlParameter("@Account", Account);
                parameters.Add(sp);
            }

            string orderBy = "order By A.[Emplno] asc";

            DataTable dt = SQLHelper.ExecuteDataTable(select + sql + orderBy, CommandType.Text, parameters.ToArray());
            if (dt == null)
                return null;

            return ToHashtableList(dt);
        }


        /// <summary>
        /// 获取单一实体
        /// SAM 2017年5月2日17:41:47
        /// </summary>
        /// <param name="MESUserID"></param>
        /// <returns></returns>
        public static SYS_MESUsers get(string MESUserID)
        {
            string sql = string.Format(@"select Top 1 * from [SYS_MESUsers] where [MESUserID] = '{0}'  and [SystemID] = '{1}' ", MESUserID, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return null;
            else
                return ToEntity(dt.Rows[0]);
        }

        /// <summary>
        /// 账号管理列表
        /// SAM 2017年5月4日14:34:32
        /// </summary>
        /// <param name="Code">代号</param>
        /// <param name="Status">状态</param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> Inf00003GetList(string OrganizationID, string Code, string Status, int page, int rows, ref int count)
        {
            string select = string.Format(@"select A.MESUserID,A.Account,A.UserName,A.EnglishName,A.Sex,A.Brith,A.IDcard,A.Status,A.Type,
             A.InTime,A.Emplno,A.CardCode,A.Email,A.Mobile,A.Comments,
            (select Top 1 [OrganizationID] from [SYS_UserOrganizationMapping] where [UserID] =A.[MESUserID]) as OrganizationID,
             B.UserName as Creator,A.CreateLocalTime as CreateTime,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else C.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ");

            string sql = string.Format(@"  from [SYS_MESUsers] A 
            left join [SYS_MESUsers] B on A.[Creator] = B.[MESUserID]
            left join [SYS_MESUsers] C on A.[Modifier] = C.[MESUserID]
            where A.[SystemID]='{0}' and A.[Status] <> 2 and A.[UserType]=10 ",
            Framework.SystemID);

            if (!string.IsNullOrWhiteSpace(OrganizationID))
                sql += string.Format(@" and A.[MESUserID] in (select [UserID] from [SYS_UserOrganizationMapping] where [OrganizationID] in (select [OrganizationID] from [SYS_Organization] where ([OrganizationID]='{1}' or [ParentOrganizationID]='{1}') and [Type]='{0}020121300001D')) ", Framework.SystemID, OrganizationID);

            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@Code",SqlDbType.VarChar),
                new SqlParameter("@Status",SqlDbType.VarChar)
            };
            parameters[0].Value = DBNull.Value;
            parameters[1].Value = DBNull.Value;

            SqlParameter[] Parcount = new SqlParameter[]{
                new SqlParameter("@Code",SqlDbType.VarChar),
                new SqlParameter("@Status",SqlDbType.VarChar)
            };
            Parcount[0].Value = DBNull.Value;
            Parcount[1].Value = DBNull.Value;
            if (!string.IsNullOrWhiteSpace(Code))
            {
                Code = "%" + Code + "%";
                sql = sql + string.Format(@" and A.[Emplno] like @Code ");
                parameters[0].Value = Code;
                Parcount[0].Value = Code;
            }

            if (!string.IsNullOrWhiteSpace(Status))
            {
                sql = sql + string.Format(@" and A.[Status] = @Status ");
                parameters[1].Value = Status;
                Parcount[1].Value = Status;
            }

            count = UniversalService.getCount(sql, Parcount);

            string orderby = "A.[Status] desc,A.[Emplno] ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, parameters, page, rows);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// 账号管理列表
        /// SAM 2017年5月4日14:34:32
        /// </summary>
        /// <param name="Code">代号</param>
        /// <param name="Status">状态</param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> Inf00003GetListV1(string OrganizationID, string Code, string Status, string Account, string UserName, string DeptID, int page, int rows, ref int count)
        {
            string select = string.Format(@"select A.MESUserID,A.Account,A.UserName,A.EnglishName,A.Sex,A.Brith,A.IDcard,A.Status,A.Type,
             A.InTime,A.Emplno,A.CardCode,A.Email,A.Mobile,A.Comments,
            (select Top 1 [OrganizationID] from [SYS_UserOrganizationMapping] where [UserID] =A.[MESUserID]) as OrganizationID,
             B.UserName as Creator,A.CreateLocalTime as CreateTime,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else C.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ");

            string sql = string.Format(@"  from [SYS_MESUsers] A 
            left join [SYS_MESUsers] B on A.[Creator] = B.[MESUserID]
            left join [SYS_MESUsers] C on A.[Modifier] = C.[MESUserID]
            left join [SYS_UserOrganizationMapping] D on A.[MESUserID] = D.[UserID]
            where A.[SystemID]='{0}' and A.[Status] <> 2 and A.[UserType]=10 ",
            Framework.SystemID);

            if (!string.IsNullOrWhiteSpace(OrganizationID))
                sql += string.Format(@" and A.[MESUserID] in (select [UserID] from [SYS_UserOrganizationMapping] where [OrganizationID] in (select [OrganizationID] from [SYS_Organization] where ([OrganizationID]='{1}' or [ParentOrganizationID]='{1}') and [Type]='{0}020121300001D')) ", Framework.SystemID, OrganizationID);
            if (!string.IsNullOrWhiteSpace(DeptID))
            {
                sql = sql + string.Format(@" and D.[OrganizationID] ='{0}'", DeptID);
            }
            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@Code",SqlDbType.VarChar),
                new SqlParameter("@Status",SqlDbType.VarChar),
                new SqlParameter("@Account",SqlDbType.VarChar),
                new SqlParameter("@UserName",SqlDbType.VarChar),
            };
            parameters[0].Value = DBNull.Value;
            parameters[1].Value = DBNull.Value;
            parameters[2].Value = DBNull.Value;
            parameters[3].Value = DBNull.Value;
            SqlParameter[] Parcount = new SqlParameter[]{
                new SqlParameter("@Code",SqlDbType.VarChar),
                new SqlParameter("@Status",SqlDbType.VarChar),
                new SqlParameter("@Account",SqlDbType.VarChar),
                new SqlParameter("@UserName",SqlDbType.VarChar),
            };
            Parcount[0].Value = DBNull.Value;
            Parcount[1].Value = DBNull.Value;
            Parcount[2].Value = DBNull.Value;
            Parcount[3].Value = DBNull.Value;
            if (!string.IsNullOrWhiteSpace(Code))
            {
                Code = "%" + Code + "%";
                sql = sql + string.Format(@" and A.[Emplno] like @Code ");
                parameters[0].Value = Code;
                Parcount[0].Value = Code;
            }

            if (!string.IsNullOrWhiteSpace(Status))
            {
                sql = sql + string.Format(@" and A.[Status] = @Status ");
                parameters[1].Value = Status;
                Parcount[1].Value = Status;
            }

            if (!string.IsNullOrWhiteSpace(Account))
            {
                Account = "%" + Account + "%";
                sql = sql + string.Format(@" and A.[Account] collate Chinese_PRC_CI_AS like @Account ");
                parameters[2].Value = Account;
                Parcount[2].Value = Account;
            }

            if (!string.IsNullOrWhiteSpace(UserName))
            {
                UserName = "%" + UserName + "%";
                sql = sql + string.Format(@" and A.[UserName] collate Chinese_PRC_CI_AS like @UserName ");
                parameters[3].Value = UserName;
                Parcount[3].Value = UserName;
            }

            count = UniversalService.getCount(sql, Parcount);

            string orderby = "A.[Status] desc,A.[Emplno] ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, parameters, page, rows);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// 账号管理列表--用于个人设定的修改版---未完成 差语系字段（不要查询功能）
        /// MOUSE 2017年8月7日09:15:06
        /// </summary>
        /// <param name="Account">账号</param>
        /// <param name="UserName">姓名</param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> Inf00002GetList( string OrganizationID, string Account, string Username, int page, int rows, ref int count)
        {
            string select = string.Format(@"select A.MESUserID,A.Sequence,A.Account,A.UserName,A.EnglishName,A.JobTitle,A.Emplno
             (select Code From [SYS_Organization] where OrganizationID =(select OrganizationID from [SYS_UserOrganizationMapping] where [UserID]=A.[MESUserID]))as A.Code,
             (select Name From [SYS_Organization] where OrganizationID =(select OrganizationID from [SYS_UserOrganizationMapping] where [UserID]=A.[MESUserID]))as A.Name,
             A.Email,A.CardCode,A.Comments,A.Status,

            (select Top 1 [OrganizationID] from [SYS_UserOrganizationMapping] where [UserID] =A.[MESUserID]) as OrganizationID,
             B.UserName as Creator,A.CreateLocalTime as CreateTime,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else C.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ");

            string sql = string.Format(@"  from [SYS_MESUsers] A 
            left join [SYS_MESUsers] B on A.[Creator] = B.[MESUserID]
            left join [SYS_MESUsers] C on A.[Modifier] = C.[MESUserID]
            where A.[SystemID]='{0}' and A.[Status] <> 2 and A.[UserType]=10 ",
            Framework.SystemID);

            if (!string.IsNullOrWhiteSpace(OrganizationID))
                sql += string.Format(@" and A.[MESUserID] in (select [UserID] from [SYS_UserOrganizationMapping] where [OrganizationID] in (select [OrganizationID] from [SYS_Organization] where ([OrganizationID]='{1}' or [ParentOrganizationID]='{1}') and [Type]='{0}020121300001D')) ", Framework.SystemID, OrganizationID);

            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@Account",SqlDbType.VarChar),
                new SqlParameter("@Username",SqlDbType.VarChar)
            };
            parameters[0].Value = DBNull.Value;
            parameters[1].Value = DBNull.Value;

            SqlParameter[] Parcount = new SqlParameter[]{
                new SqlParameter("@Account",SqlDbType.VarChar),
                new SqlParameter("@Username",SqlDbType.VarChar)
            };
            Parcount[0].Value = DBNull.Value;
            Parcount[1].Value = DBNull.Value;
            if (!string.IsNullOrWhiteSpace(Account))
            {
                Account = "%" + Account + "%";
                sql = sql + string.Format(@" and A.[Account] like @Account ");
                parameters[0].Value = Account;
                Parcount[0].Value = Account;
            }

            if (!string.IsNullOrWhiteSpace(Username))
            {
                sql = sql + string.Format(@" and A.[Username] = @Username ");
                parameters[1].Value = Username;
                Parcount[1].Value = Username;
            }

            count = UniversalService.getCount(sql, Parcount);

            string orderby = "A.[Username] desc,A.[Account] ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, parameters, page, rows);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// 导出的数据
        /// SAM 2017年5月8日17:47:27
        /// </summary>
        /// <param name="Code">代号</param>
        /// <param name="Status">状态</param>
        /// <returns></returns>
        public static DataTable Inf00003GetExportList(string Code, string Status)
        {
            string select = string.Format(@"select ROW_NUMBER() OVER (ORDER BY A.[Status] desc,A.[Emplno]),A.Account,A.UserName,
            A.EnglishName,(CASE WHEN A.Sex=1 THEN '男' ELSE '女' END),
            A.Brith,A.Emplno, 
            (select Code from SYS_Organization where OrganizationID = (select Top 1 [OrganizationID] from [SYS_UserOrganizationMapping] where [UserID] =A.[MESUserID])) as OrganizationCode,
            A.IDcard,A.Mobile,A.Email,B.Name as TypeName,A.InTime,A.CardCode,
            A.Comments,(CASE WHEN A.Status=1 THEN '正常' ELSE '作废' END)");

            string sql = string.Format(@"  from [SYS_MESUsers] A 
            left join SYS_Parameters B on A.Type = B.ParameterID
            where A.[SystemID]='{0}' and A.[Status] <> 2 and A.[UserType]=10 ", Framework.SystemID);

            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@Code",SqlDbType.VarChar),
                new SqlParameter("@Status",SqlDbType.VarChar)
            };
            parameters[0].Value = DBNull.Value;
            parameters[1].Value = DBNull.Value;

            if (!string.IsNullOrWhiteSpace(Code))
            {
                Code = "%" + Code + "%";
                sql = sql + string.Format(@" and A.[Emplno] like @Code ");
                parameters[0].Value = Code;
            }

            if (!string.IsNullOrWhiteSpace(Status))
            {
                sql = sql + string.Format(@" and A.[Status] = @Status ");
                parameters[1].Value = Status;
            }

            string orderBy = "order By A.[Status] desc,A.[Emplno] ";

            return SQLHelper.ExecuteDataTable(select + sql + orderBy, CommandType.Text, parameters);
        }


        /// <summary>
        /// 判断是否重复
        /// SAM 2017年5月4日14:50:06
        /// </summary>
        /// <param name="Account"></param>
        /// <param name="Code">代号</param>
        /// <param name="CardCode"></param>
        /// <param name="MESUserID"></param>
        /// <returns></returns>
        public static bool CheckCode(string Account, string Code, string CardCode, string MESUserID)
        {
            string sql = string.Format(@"select Top 1 * from [SYS_MESUsers] where [SystemID]='{0}' and [Status] <> 2  and [UserType]=10  ", Framework.SystemID);

            /*先定义Code，默认给DbNull,以免参数找不到*/
            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@Account",SqlDbType.VarChar),
                new SqlParameter("@Code",SqlDbType.VarChar),
                new SqlParameter("@CardCode",SqlDbType.VarChar)
            };
            parameters[0].Value = DBNull.Value;
            parameters[1].Value = DBNull.Value;
            parameters[2].Value = DBNull.Value;

            if (!string.IsNullOrWhiteSpace(Account))
            {
                sql = sql + string.Format(@" and [Account] =@Account ");
                parameters[0].Value = Account;
            }
            if (!string.IsNullOrWhiteSpace(Code))
            {
                sql = sql + string.Format(@" and [Emplno] =@Code ");
                parameters[1].Value = Code;
            }
            if (!string.IsNullOrWhiteSpace(CardCode))
            {
                sql = sql + string.Format(@" and [CardCode] =@CardCode ");
                parameters[2].Value = CardCode;
            }

            /*MESUserID（参数流水号）用于在更新时，排除他自己*/
            if (!string.IsNullOrWhiteSpace(MESUserID))
                sql = sql + String.Format(@" and [MESUserID] <> '{0}' ", MESUserID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text, parameters);

            if (dt.Rows.Count == 0)
                return false;
            else
                return true;
        }


        /// <summary>
        /// 根据类别代号获取不归属于他的数据
        /// SAM 2017年6月1日16:53:19
        /// </summary>
        /// <param name="ClassID"></param>
        /// <param name="Code">代号</param>
        /// <returns></returns>
        public static object Inf00016GetNotAuthorityList(string ClassID, string Code)
        {
            string select = string.Format(@"select null as DocumentAuthorityID,A.MESUserID as AuthorityID,A.Emplno as Code,A.UserName as Name ");

            string sql = string.Format(@"  from [SYS_MESUsers] A 
            where [MESUserID] not in (select [AuthorityID] from [SYS_DocumentAuthority] where [ClassID] ='{1}' and [Status] = '{0}0201213000001' and [Attribute]=1)
            and A.[SystemID]='{0}' and A.[Status] =1 and A.[UserType] =10 ", Framework.SystemID, ClassID);

            if (!string.IsNullOrWhiteSpace(Code))
                sql += string.Format(@"and A.Emplno like '%{0}%' ", Code);

            string orderBy = "order By A.[Emplno] ";

            DataTable dt = SQLHelper.ExecuteDataTable(select + sql + orderBy, CommandType.Text, null);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// 获取当前登录用户的个人信息
        /// SAM 2017年6月14日00:44:39
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public static Hashtable GetUser(string userid)
        {
            string select = string.Format(@"select A.MESUserID,A.Account,A.UserName,A.EnglishName,A.Emplno,A.Comments,
            (select Top 1 [OrganizationID] from [SYS_UserOrganizationMapping] where [UserID] = A.[MESUserID]) as OrganizationID,
            (select [Code] from [SYS_Organization] where [OrganizationID] = (select Top 1 [OrganizationID] from [SYS_UserOrganizationMapping] where [UserID] = A.[MESUserID])) as DeptCode,
            (select [Name] from [SYS_Organization] where [OrganizationID] = (select Top 1 [OrganizationID] from [SYS_UserOrganizationMapping] where [UserID] = A.[MESUserID])) as DeptName ");

            string sql = string.Format(@"  from [SYS_MESUsers] A 
            where A.[SystemID]='{0}' and A.[MESUserID]='{1}' ", Framework.SystemID, userid);

            DataTable dt = SQLHelper.ExecuteDataTable(select+sql, CommandType.Text);

            return ToHashtableList(dt)[0];
        }

        /// <summary>
        /// 获取当前登录用户的个人信息
        ///  Mouse 2017年9月5日10:35:44
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public static SYS_MESUsers GetUser2(string userid)
        {
            string select = string.Format(@"select A.MESUserID,A.Account,A.UserName,A.EnglishName,A.Emplno,A.Comments,
            (select Top 1 [OrganizationID] from [SYS_UserOrganizationMapping] where [UserID] = A.[MESUserID]) as OrganizationID,

            (select [Code] from [SYS_Organization] where [OrganizationID] = (select Top 1 [OrganizationID] from [SYS_UserOrganizationMapping] where [UserID] = A.[MESUserID])) as DeptCode,
            (select [Name] from [SYS_Organization] where [OrganizationID] = (select Top 1 [OrganizationID] from [SYS_UserOrganizationMapping] where [UserID] = A.[MESUserID])) as DeptName ");

            string sql = string.Format(@"  from [SYS_MESUsers] A 
            where A.[SystemID]='{0}' and A.[MESUserID]='{1}' ", Framework.SystemID, userid);

            DataTable dt = SQLHelper.ExecuteDataTable(select + sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return null;
            else
                return ToEntity(dt.Rows[0]);
        }

        /// <summary>
        /// 获取当前登录用户的个人信息-修改版
        /// MOUSE 2017年8月4日15:16:12
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public static Hashtable Inf00002GetUser(string userid)
        {
            string select = string.Format(@"select A.MESUserID,A.Sequence,A.Account,A.UserName,A.EnglishName,A.JobTitle,A.Emplno,A.Language,

             (select Code From [SYS_Organization] where OrganizationID =(select Top 1 OrganizationID from [SYS_UserOrganizationMapping] where [UserID]=A.[MESUserID]) )as OrganizationCode,
             (select Name From [SYS_Organization] where OrganizationID =(select Top 1 OrganizationID from [SYS_UserOrganizationMapping] where [UserID]=A.[MESUserID]) )as OrganizationName,
             A.Email,A.CardCode,A.Comments,A.Status as StatusName,

            (select Top 1 [OrganizationID] from [SYS_UserOrganizationMapping] where [UserID] =A.[MESUserID]) as OrganizationID");

            string sql = string.Format(@"  from [SYS_MESUsers] A 
            where A.[SystemID]='{0}' and A.[MESUserID]='{1}' ", Framework.SystemID, userid);

            DataTable dt = SQLHelper.ExecuteDataTable(select + sql, CommandType.Text);

            return ToHashtableList(dt)[0];
        }

        /// <summary>
        /// 获取全职和销售的用户列表
        /// Tom 2017年6月28日19点38分
        /// </summary>
        /// <param name="account"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> Sfc00002GetList(string Account, int page, int rows, ref int count)
        {
            string select = string.Format(@"select A.MESUserID, A.Emplno as Account, A.UserName, A.EnglishName, A.Comments ");

            string sql = string.Format(
                @"from [SYS_MESUsers] A 
                  where A.SystemID = '{0}' and
                        A.Status = 1 and 
                        exists(select * from SYS_UserRoleMapping 
                               where UserID = A.MESUserID and 
                                     (RoleID = '{0}0601213000002' or 
                                      RoleID = '{0}0601213000003'))",
                Framework.SystemID);

            List<SqlParameter> parameters = new List<SqlParameter>();
            if (!string.IsNullOrWhiteSpace(Account))
            {
                sql += " and A.Emplno like '%' + @Account + '%'";
                SqlParameter sp = new SqlParameter("@Account", Account);
                parameters.Add(sp);
            }
            SqlParameter[] paramArray = parameters.ToArray();

            count = UniversalService.getCount(sql, paramArray);

            string orderBy = "order By A.[Emplno] asc";

            DataTable dt = SQLHelper.ExecuteDataTable(select + sql + orderBy, CommandType.Text, paramArray);
            if (dt == null)
                return null;

            return ToHashtableList(dt);
        }



        /// <summary>
        /// Ems00009的检修人弹窗
        /// SAM 2017年7月12日14:43:22
        /// </summary>
        /// <param name="OrganizationCode"></param>
        /// <param name="OrganizationName"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> Ems00009GetUserList(string OrganizationCode, string OrganizationName, int page, int rows, ref int count)
        {
            string select = string.Format(@"select A.MESUserID,A.Emplno,A.UserName,A.Status,A.Account,
            C.OrganizationID,C.Code as OrganizationCode,C.Name as OrganizationName ");

            string sql = string.Format(@"  from [SYS_MESUsers] A 
            left join [SYS_UserOrganizationMapping] B on A.[MESUserID] = B.[UserID]
            left join [SYS_Organization] C on B.[OrganizationID] = C.[OrganizationID]
            where A.[SystemID]='{0}' and A.[Status] = 1  and A.[UserType]=10  and C.[OrganizationID] is not null ", Framework.SystemID);

            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@OrganizationCode",SqlDbType.VarChar),
                new SqlParameter("@OrganizationName",SqlDbType.VarChar)
            };
            parameters[0].Value = DBNull.Value;
            parameters[1].Value = DBNull.Value;

            SqlParameter[] Parcount = new SqlParameter[]{
                new SqlParameter("@OrganizationCode",SqlDbType.VarChar),
                new SqlParameter("@OrganizationName",SqlDbType.VarChar)
            };
            Parcount[0].Value = DBNull.Value;
            Parcount[1].Value = DBNull.Value;

            if (!string.IsNullOrWhiteSpace(OrganizationCode))
            {
                OrganizationCode = "%" + OrganizationCode + "%";
                sql = sql + string.Format(@" and C.[Code] like @OrganizationCode ");
                parameters[0].Value = OrganizationCode;
                Parcount[0].Value = OrganizationCode;
            }

            if (!string.IsNullOrWhiteSpace(OrganizationName))
            {
                OrganizationName = "%" + OrganizationName + "%";
                sql = sql + string.Format(@" and C.[Name] like @OrganizationName ");
                parameters[1].Value = OrganizationName;
                Parcount[1].Value = OrganizationName;
            }

            count = UniversalService.getCount(sql, Parcount);

            string orderby = "C.[Code],A.[Emplno] ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, parameters, page, rows);

            return ToHashtableList(dt);
        }



        /// <summary>
        /// 完工單回報作業-資源報工人工开窗
        /// SAM 2017年7月20日10:42:00
        /// </summary>
        /// <param name="TaskDispatchID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> GetMachineOrManList(string TaskDispatchID, int page, int rows, ref int count)
        {
            string select = string.Format(@"select A.MESUserID as EquipmentID,A.Emplno as DisplayCode,A.UserName as DisplayName,
            'L' as ResourceClass,
            (select Top 1 [ParameterID] from [SYS_Parameters] where Code='L' and [ParameterTypeID]= '{0}0191213000013' and IsEnable = 1) as ResourceClassID ", Framework.SystemID);

            string sql = string.Format(@"  from [SYS_MESUsers] A 
            where A.[SystemID]='{0}' and A.[Status] = 1  and A.[UserType]=10  
            and A.[MESUserID] in (select [EquipmentID] from [SFC_TaskDispatchResource] where 
            [TaskDispatchID] = '{1}' 
            and ResourceClassID = (select Top 1 [ParameterID] from [SYS_Parameters] where Code='L' and [ParameterTypeID]= '{0}0191213000013' and IsEnable = 1)) ", Framework.SystemID, TaskDispatchID);

  
            count = UniversalService.getCount(sql, null);

            string orderby = "A.[Emplno] ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, null, page, rows);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// 判断用户是否在角色用户中存在设定
        /// SAM 2017年7月30日23:46:04
        /// </summary>
        /// <param name="MESUserID"></param>
        /// <returns></returns>
        public static bool CheckRoleUser(string MESUserID)
        {
            string sql = string.Format(@"select * from [SYS_UserRoleMapping] where [UserID] = '{0}' and [SystemID] = '{1}' ", MESUserID, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return false;
            else
                return true;
        }
    }
}
