using MonkeyFly.Core;
using MonkeyFly.MES.BasicService;
using MonkeyFly.MES.Models;
using MonkeyFly.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace MonkeyFly.MES.ModelServices
{
    public class SYS_ParameterService : SuperModel<SYS_Parameters>
    {
        /// <summary>
        /// 根据代号和参数类型判断是否存在
        /// SAM 2017年4月27日11:07:11
        /// </summary>
        /// <param name="Code">代号</param>
        /// <param name="Type"></param>
        /// <returns></returns>
        public static SYS_Parameters getByCode(string Code, string Type)
        {
            string sql = string.Format(@"select Top 1 * from [SYS_Parameters] where [Code] = '{0}' and [ParameterTypeID]='{2}'  and [SystemID] = '{1}'  and IsEnable=1 ", Code, Framework.SystemID, Type);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return null;
            else
                return ToEntity(dt.Rows[0]);
        }

        /// <summary>
        /// 根据名称和参数类型判断是否存在
        /// SAM 2017年5月4日16:09:43
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Type"></param>
        /// <returns></returns>
        public static SYS_Parameters getByName(string Name, string Type)
        {
            string sql = string.Format(@"select Top 1 * from [SYS_Parameters] where [Name] = '{0}' and [ParameterTypeID]='{2}'  and [SystemID] = '{1}'  and IsEnable=1 ", Name, Framework.SystemID, Type);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return null;
            else
                return ToEntity(dt.Rows[0]);
        }

        /// <summary>
        /// 根据代号以及用途别获取分类
        /// SAM 2017年8月14日23:04:12
        /// </summary>
        /// <param name="Code"></param>
        /// <param name="Type"></param>
        /// <returns></returns>
        public static SYS_Parameters getClass(string Code, string Type)
        {
            string sql = string.Format(@"select Top 1 * from [SYS_Parameters] where [Code] = '{0}' and [ParameterTypeID]='{2}'  and [SystemID] = '{1}'  and IsEnable=1 and [Description] ='{3}' ", Code, Framework.SystemID, Framework.SystemID + "019121300000B", Type);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return null;
            else
                return ToEntity(dt.Rows[0]);
        }


        /// <summary>
        /// 添加类别
        /// Jack 2015年8月2日17:41:13
        /// </summary>
        /// <param name="jObject"></param>
        /// <returns></returns>
        public static bool insert(string userid, SYS_Parameters par)
        {
            try
            {
                string sql = string.Format(@"insert [SYS_Parameters](ParameterID,ParentParameterID,
                Name,ParameterTypeID,Description,DescriptionOne,IsEnable,IsDefault,UsingType,Sequence,Code,
                Comments,SystemID,Modifier,ModifiedTime,ModifiedLocalTime,Creator,CreateTime,CreateLocalTime) values 
               (@ParameterID,@ParentParameterID,@Name,@ParameterTypeID,@Description,@DescriptionOne,
                @IsEnable,@IsDefault,@UsingType,@Sequence,@Code,@Comments,{0})", UniversalService.getInsertnew(userid));

                SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@ParameterID",SqlDbType.VarChar),
                new SqlParameter("@ParentParameterID",SqlDbType.VarChar),
                new SqlParameter("@Name",SqlDbType.VarChar),
                new SqlParameter("@ParameterTypeID",SqlDbType.VarChar),
                new SqlParameter("@Description",SqlDbType.VarChar),
                new SqlParameter("@DescriptionOne",SqlDbType.VarChar),
                new SqlParameter("@IsEnable",SqlDbType.Bit),
                new SqlParameter("@IsDefault",SqlDbType.Bit),
                new SqlParameter("@UsingType",SqlDbType.TinyInt),
                new SqlParameter("@Sequence",SqlDbType.Int),
                new SqlParameter("@Code",SqlDbType.VarChar),
                new SqlParameter("@Comments",SqlDbType.VarChar),
            };

                parameters[0].Value = par.ParameterID;
                if (String.IsNullOrWhiteSpace(par.ParentParameterID))
                    parameters[1].Value = DBNull.Value;
                else
                    parameters[1].Value = par.ParentParameterID;
                parameters[2].Value = (object)par.Name ?? DBNull.Value;
                if (String.IsNullOrWhiteSpace(par.ParameterTypeID))
                    parameters[3].Value = DBNull.Value;
                else
                    parameters[3].Value = par.ParameterTypeID;

                if (String.IsNullOrWhiteSpace(par.Description))
                    parameters[4].Value = DBNull.Value;
                else
                    parameters[4].Value = par.Description;

                if (String.IsNullOrWhiteSpace(par.DescriptionOne))
                    parameters[5].Value = DBNull.Value;
                else
                    parameters[5].Value = par.DescriptionOne;

                parameters[6].Value = par.IsEnable;
                parameters[7].Value = par.IsDefault;
                parameters[8].Value = par.UsingType;
                parameters[9].Value = par.Sequence;
                parameters[10].Value = (Object)par.Code ?? DBNull.Value;
                if (String.IsNullOrWhiteSpace(par.Comments))
                    parameters[11].Value = DBNull.Value;
                else
                    parameters[11].Value = par.Comments;

                return SQLHelper.ExecuteNonQuery(sql, CommandType.Text, parameters) > 0;
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return false;
            }
        }

        /// <summary>
        /// 修改类别
        /// Tom 2015年8月2日17:50:07
        /// </summary>
        /// <param name="jObject"></param>
        /// <returns></returns>
        public static bool update(string userId, SYS_Parameters par)
        {
            try
            {
                string sql = String.Format(
                    @"update [SYS_Parameters] set {0},
                    [Name]=@Name,
                    [Description]=@Description,
                    [DescriptionOne]=@DescriptionOne,
                    [Code]=@Code,
                    [Comments]=@Comments,
                    [Sequence]=@Sequence,
                    [IsEnable]=@IsEnable,
                    [IsDefault]=@IsDefault
                    where [ParameterID]=@ParameterID",
                    UniversalService.getUpdateUTC(userId));

                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@Name", SqlDbType.VarChar),
                    new SqlParameter("@Description", SqlDbType.VarChar),
                    new SqlParameter("@DescriptionOne", SqlDbType.VarChar),
                    new SqlParameter("@ParameterID", SqlDbType.VarChar),
                    new SqlParameter("@Code", SqlDbType.VarChar),
                    new SqlParameter("@Comments", SqlDbType.VarChar),
                    new SqlParameter("@Sequence", SqlDbType.VarChar),
                    new SqlParameter("@IsEnable", SqlDbType.TinyInt),
                    new SqlParameter("@IsDefault", SqlDbType.Bit)
                };

                parameters[0].Value = (object)par.Name ?? DBNull.Value;
                parameters[1].Value = (object)par.Description ?? DBNull.Value;
                parameters[2].Value = (object)par.DescriptionOne ?? DBNull.Value;
                parameters[3].Value = par.ParameterID;
                parameters[4].Value = (object)par.Code ?? DBNull.Value;
                parameters[5].Value = (object)par.Comments ?? DBNull.Value;
                parameters[6].Value = (object)par.Sequence ?? DBNull.Value;
                parameters[7].Value = (object)par.IsEnable ?? DBNull.Value;
                parameters[8].Value = (object)par.IsDefault ?? DBNull.Value;

                return SQLHelper.ExecuteNonQuery(sql, CommandType.Text, parameters) > 0;
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return false;
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool delete(string userId, string id)
        {
            string sql = string.Format(
                @"update SYS_Parameters
                  set IsEnable = 2,
                      {0}
                  where [ParameterID]=@ParameterID", UniversalService.getUpdateUTC(userId));

            SqlParameter sp = new SqlParameter("@ParameterID", SqlDbType.VarChar, 30);
            sp.Value = id;

            try
            {
                bool Ex = SQLHelper.ExecuteNonQuery(sql, CommandType.Text, new SqlParameter[] { sp }) > 0;
                return Ex;
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return false;
            }
        }

        /// <summary>
        /// 判断是否存在参数
        /// SAM 2016年11月10日11:21:07
        /// 逻辑：
        /// 1.首先，最原始的，SystemID是必须的。所以他放在了源SQL中。
        /// 2.Name和Code是主要判定是否存在的字段，有可能是Name,有可能是Code,也有可能是Name+code
        /// 3.ParentParameterID上级参数流水号
        ///   TypeID参数类型
        ///   companyID公式别
        ///   都是各种情况下有可能用到的判断唯一条件。
        /// 4.最后得到查询后的DataTable
        /// 5.判定是否存在数据，如果存在，返回True。
        ///   如果不存在，返回False。
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Code">代号</param>
        /// <param name="ParentParameterID"></param>
        /// <param name="typeID"></param>
        /// <param name="companyID"></param>
        /// <param name="ParameterID"></param>
        /// <returns></returns>
        public static bool CheckParameter(string Code, string Name, string ParentParameterID, string TypeID, string ParameterID)
        {
            string sql = String.Format(@"select Top 1 * from [SYS_Parameters] where [SystemID]='{0}' and IsEnable <> 2", Framework.SystemID);

            /*先定义Name和Code，默认给DbNull,以免参数找不到*/
            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@Name",SqlDbType.VarChar),
                new SqlParameter("@Code",SqlDbType.VarChar)
            };
            parameters[0].Value = DBNull.Value;
            parameters[1].Value = DBNull.Value;

            /*因为Name和Code是通过手动输入的，所以需要用参数的形式去拼SQL*/
            if (!string.IsNullOrWhiteSpace(Name))
            {
                sql = sql + String.Format(@" and [Name] =@Name ");
                parameters[0].Value = Name;
            }

            if (!string.IsNullOrWhiteSpace(Code))
            {
                sql = sql + String.Format(@" and [Code] =@Code ");
                parameters[1].Value = Code;
            }

            /*参数类型，上级参数，公司别都是内定的流水号，所以可以直接用String.Format拼*/
            if (!string.IsNullOrWhiteSpace(ParentParameterID))
                sql = sql + String.Format(@" and [ParentParameterID] = '{0}' ", ParentParameterID);

            if (!string.IsNullOrWhiteSpace(TypeID))
                sql = sql + String.Format(@" and [ParameterTypeID] = '{0}' ", TypeID);

            /*ParameterID（参数流水号）用于在更新时，排除他自己*/
            if (!string.IsNullOrWhiteSpace(ParameterID))
                sql = sql + String.Format(@" and [ParameterID] <> '{0}' ", ParameterID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text, parameters);

            if (dt.Rows.Count == 0)
                return false;
            else
                return true;
        }

        /// <summary>
        /// 根据参数编号获取参数实体
        /// SAM 2016年10月11日15:33:01
        /// </summary>
        /// <param name="ParameterID">参数编号</param>
        /// <returns></returns>
        public static SYS_Parameters get(string ParameterID)
        {
            string sql = string.Format(@"select * from [SYS_Parameters] where [ParameterID]='{0}' ", ParameterID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return null;

            return ToList(dt)[0];
        }

        /// <summary>
        /// 根据参数类型集合获取参数
        /// SAM 2017年4月28日11:23:54
        /// </summary>
        /// <param name="CompanyID"></param>
        /// <param name="typeIDs"></param>
        /// <returns></returns>
        public static IList<Hashtable> GetLists(string typeIDs)
        {
            string sql = string.Format(@"select ParameterID as value,Name as text,ParameterTypeID,ParentParameterID,
            IsDefault,Description,Code+'-'+Name as Newvalue,Code
            from [SYS_Parameters] 
            where [SystemID]='{1}' and [ParameterTypeID] in ('{0}') and [IsEnable]=1 
            order by Sequence asc ", typeIDs, Framework.SystemID);

            DataTable result = SQLHelper.ExecuteDataTable(sql, CommandType.Text, null);

            return ToHashtableList(result);
        }

        /// <summary>
        /// 参数表的通用获取列表函数
        /// SAM 2017年5月24日15:48:35
        /// </summary>
        /// <param name="Code">代号</param>
        /// <param name="Status">状态</param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> GeneralGetList(string Type, string Code, string status, int page, int rows, ref int count)
        {
            string select = string.Format(@"select A.ParameterID,A.Code,A.Name,A.IsEnable,A.Comments,A.IsDefault,
            D.UserName as Creator,A.CreateLocalTime as CreateTime,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else E.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ");

            string sql = string.Format(@" from [SYS_Parameters] A 
            left join [SYS_MESUsers] D on A.Creator = D.MESUserID
            left join [SYS_MESUsers] E on A.Modifier = E.MESUserID
            where A.[SystemID]='{0}' and A.[ParameterTypeID]='{0}{1}' and A.IsEnable <> 2 ", Framework.SystemID, Type);

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
                sql = sql + String.Format(@" and A.[Code] like @Code ");
                parameters[0].Value = Code;
                Parcount[0].Value = Code;
            }

            if (!string.IsNullOrWhiteSpace(status))
            {
                sql = sql + String.Format(@" and A.[IsEnable] = @Status ");
                parameters[1].Value = status;
                Parcount[1].Value = status;
            }

            count = UniversalService.getCount(sql, Parcount);

            string orderby = "A.[IsEnable] desc,A.[Code] ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, parameters, page, rows);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// 参数表的通用导出数据函数
        /// SAM 2017年5月24日15:50:32
        /// </summary>
        /// <param name="Code">代号</param>
        /// <param name="Status">状态</param>
        /// <returns></returns>
        public static DataTable GeneralExport(string Type, string Code, string status)
        {
            string select = string.Format(@"select ROW_NUMBER() OVER (ORDER BY A.[IsEnable] desc,A.[Code]),A.Code,A.Name,
            A.Comments,(CASE WHEN A.IsEnable=1 THEN '正常' ELSE '作废' END),
            D.UserName as Creator,A.CreateLocalTime as CreateTime,
           (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else E.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ");

            string sql = string.Format(@" from [SYS_Parameters] A 
            left join [SYS_MESUsers] D on A.Creator = D.MESUserID
            left join [SYS_MESUsers] E on A.Modifier = E.MESUserID
            where A.[SystemID]='{0}' and A.[ParameterTypeID]='{0}{1}' and A.IsEnable <> 2 ", Framework.SystemID, Type);


            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@Code",SqlDbType.VarChar),
                new SqlParameter("@Status",SqlDbType.VarChar)
            };
            parameters[0].Value = DBNull.Value;
            parameters[1].Value = DBNull.Value;

            if (!string.IsNullOrWhiteSpace(Code))
            {
                Code = "%" + Code + "%";
                sql = sql + String.Format(@" and A.[Code] like @Code ");
                parameters[0].Value = Code;
            }

            if (!string.IsNullOrWhiteSpace(status))
            {
                sql = sql + String.Format(@" and A.[IsEnable] = @Status ");
                parameters[1].Value = status;
            }

            string orderBy = "order By A.[IsEnable] desc,A.[Code] ";

            return SQLHelper.ExecuteDataTable(select + sql + orderBy, CommandType.Text, parameters);
        }

        /// <summary>
        /// 语序资料维护-获取列表
        /// SAM 2016年10月18日10:01:05
        /// </summary>
        /// <param name="token">授权码</param>
        /// <param name="name">代号-查询条件</param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <returns></returns>
        public static IList<Hashtable> GetLanguageList(string name, int page, int rows, ref int count)
        {
            string select = string.Format(@"select A.ParameterID,A.Name,A.Code,A.Description,A.DescriptionOne,A.ParameterTypeID,
                A.IsEnable,A.IsDefault,A.Sequence,A.Comments ");

            string sql = string.Format(@" from [SYS_Parameters] A where A.[SystemID]='{0}' and [ParameterTypeID]='{0}{1}' and IsEnable=1 ", Framework.SystemID, "019121300000A");

            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@Name",SqlDbType.VarChar),
            };
            parameters[0].Value = DBNull.Value;

            SqlParameter[] Parcount = new SqlParameter[]{
                new SqlParameter("@Name",SqlDbType.VarChar),
            };
            Parcount[0].Value = DBNull.Value;

            if (!string.IsNullOrWhiteSpace(name))
            {
                name = "%" + name + "%";
                sql = sql + String.Format(@" and A.[Name] like @name ");
                parameters[0].Value = name;
                Parcount[0].Value = name;
            }

            count = UniversalService.getCount(sql, Parcount);

            String orderby = "A.[CreateTime] desc";

            DataTable dt = UniversalService.getTable(select, sql, orderby, parameters, page, rows);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// 获取分类群码列表
        /// Tom 2017年4月28日14:05:01
        /// </summary>
        /// <param name="Code">代号</param>
        /// <param name="Status">状态</param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> Inf00009GroupCodeList(string code, int? status, int page, int rows, ref int count)
        {
            string select = string.Format(
                @"select A.ParameterID, A.Code, A.Name, A.IsEnable as Status, A.Comments, 
                         C.UserName as Creator,A.CreateLocalTime as CreateTime,
                         (CASE WHEN A.CreateLocalTime = A.ModifiedLocalTime THEN NULL else B.UserName END) as Modifier,
                         (CASE WHEN A.CreateLocalTime = A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ");

            string sql = string.Format(
                @" from [SYS_Parameters] A
                   left join SYS_MESUsers B on A.Modifier = B.MESUserID
                   left join SYS_MESUsers C on A.Creator = C.MESUserID
                   where A.[SystemID]='{0}' and A.[ParameterTypeID]='{0}{1}' and A.IsEnable <> 2",
                Framework.SystemID, "019121300000E");

            List<SqlParameter> paramList = new List<SqlParameter>();
            if (!string.IsNullOrWhiteSpace(code))
            {
                sql += string.Format(" and A.Code like @Code ");
                SqlParameter sp = new SqlParameter("@Code", SqlDbType.VarChar);
                sp.Value = "%" + code + "%";
                paramList.Add(sp);
            }
            if (status != null)
            {
                sql += string.Format(" and A.IsEnable = @IsEnable ");
                SqlParameter sp2 = new SqlParameter("@IsEnable", SqlDbType.TinyInt);
                sp2.Value = status;
                paramList.Add(sp2);
            }

            count = UniversalService.getCount(sql, paramList.ToArray());

            String orderby = "A.[IsEnable] desc,A.[Code] ";

            DataTable dt = UniversalService.getTable(select, sql, orderby,
                paramList.Select(p =>
                {
                    SqlParameter sp = new SqlParameter(p.ParameterName, p.DbType);
                    sp.Value = p.Value;
                    return sp;
                }).ToArray(), page, rows);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// 获取分类的弹窗
        /// SAM 2017年5月2日15:24:54
        /// </summary>
        /// <param name="type"></param>
        /// <param name="Code">代号</param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> getClassList(string type, string Code, int page, int rows, ref int count)
        {
            string select = string.Format(@"select A.ParameterID,A.Code,A.Name,A.Description,A.DescriptionOne,A.IsEnable ");

            string sql = string.Format(@" from [SYS_Parameters] A 
            left join SYS_Parameters B on A.Description = B.ParameterID
            where A.[SystemID]='{0}' and A.[ParameterTypeID]='{0}019121300000B' and  A.[IsEnable]=1", Framework.SystemID);

            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@Code",SqlDbType.VarChar),
                new SqlParameter("@Description",SqlDbType.VarChar)
            };
            parameters[0].Value = DBNull.Value;
            parameters[1].Value = DBNull.Value;

            SqlParameter[] Parcount = new SqlParameter[]{
                new SqlParameter("@Code",SqlDbType.VarChar),
                new SqlParameter("@Description",SqlDbType.VarChar)
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

            if (!string.IsNullOrWhiteSpace(type))
            {
                sql = sql + string.Format(@" and B.[Code] = @Description ");
                parameters[1].Value = type;
                Parcount[1].Value = type;
            }

            count = UniversalService.getCount(sql, Parcount);

            string orderby = " A.[CreateTime] desc";

            DataTable dt = UniversalService.getTable(select, sql, orderby, parameters, page, rows);

            return ToHashtableList(dt);
        }



        /// <summary>
        /// 获取存在于参数表的数据
        /// SAM 2017年5月2日14:59:54
        /// </summary>
        /// <param name="type"></param>
        /// <param name="Code">代号</param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> getParameterList(string type, string Code, string Name, int page, int rows, ref int count)
        {
            string select = string.Format(@"select A.ParameterID,A.Code,A.Name,A.Description,A.DescriptionOne,A.IsEnable,A.Comments,A.IsDefault ");

            string sql = string.Format(@" from [SYS_Parameters] A where A.[SystemID]='{0}' and [ParameterTypeID]='{1}' and [IsEnable]=1", Framework.SystemID, type);

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
                Code = "%" + Code.Trim() + "%";
                sql = sql + string.Format(@" and A.[Code] collate Chinese_PRC_CI_AS  like @Code ");
                parameters[0].Value = Code;
                Parcount[0].Value = Code;
            }

            if (!string.IsNullOrWhiteSpace(Name))
            {
                Name = "%" + Name.Trim() + "%";
                sql = sql + string.Format(@" and A.[Name] collate Chinese_PRC_CI_AS  like @Name ");
                parameters[1].Value = Name;
                Parcount[1].Value = Name;
            }

            count = UniversalService.getCount(sql, Parcount);

            string orderby = " A.[Code] ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, parameters, page, rows);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// 获取单位列表
        /// Tom 2017年5月2日17:25:14
        /// </summary>
        /// <param name="Code">代号</param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> Inf00011List(string code, int page, int rows, ref int count)
        {
            string select = string.Format(
                @"select A.ParameterID, A.Code, A.Name, A.Comments, A.IsEnable as Status,
                          C.UserName as Creator,A.CreateLocalTime as CreateTime,
                         (CASE WHEN A.CreateLocalTime = A.ModifiedLocalTime THEN NULL else B.UserName END) as Modifier,
                         (CASE WHEN A.CreateLocalTime = A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ");

            string sql = string.Format(
                @" from [SYS_Parameters] A
                   left join SYS_MESUsers B on A.Modifier = B.MESUserID
                   left join SYS_MESUsers C on A.Creator = C.MESUserID
                   where A.[SystemID]='{0}' and A.[ParameterTypeID]='{0}{1}' and A.IsEnable <> 2",
                Framework.SystemID, "019121300000C");

            List<SqlParameter> paramList = new List<SqlParameter>();
            if (!string.IsNullOrWhiteSpace(code))
            {
                sql += string.Format(" and A.Code like @Code ");
                SqlParameter sp = new SqlParameter("@Code", SqlDbType.VarChar);
                sp.Value = "%" + code + "%";
                paramList.Add(sp);
            }

            count = UniversalService.getCount(sql, paramList.ToArray());

            String orderby = "A.[IsEnable] desc,A.[Code]";

            DataTable dt = UniversalService.getTable(select, sql, orderby,
                paramList.Select(p =>
                {
                    SqlParameter sp = new SqlParameter(p.ParameterName, p.DbType);
                    sp.Value = p.Value;
                    return sp;
                }).ToArray(), page, rows);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// 获取分类列表
        /// Tom 2017年5月5日10:51:02
        /// </summary>
        /// <param name="useCode"></param>
        /// <param name="Code">代号</param>
        /// <param name="Status">状态</param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> Inf00009ClassList(string useCode, string code, int? status, int page, int rows, ref int count)
        {
            string select = string.Format(
                @"select A.ParameterID, E.ParameterID as UseParameterID, E.Code + E.Name as UseDescription, A.Code, A.Name as Description, A.Comments, A.IsEnable as Status,
                         D.Code as GroupCode, D.Name as GroupName, D.ParameterID as GroupParameterID,
                          C.UserName as Creator,A.CreateLocalTime as CreateTime,
                         (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else B.UserName END) as Modifier,
                         (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ");

            string sql = string.Format(
                @" from [SYS_Parameters] A
                   left join SYS_MESUsers B on A.Modifier = B.MESUserID
                   left join SYS_MESUsers C on A.Creator = C.MESUserID
                   left join SYS_Parameters D on A.DescriptionOne = D.ParameterID
                   left join SYS_Parameters E on A.Description = E.ParameterID
                   where A.[SystemID]='{0}' and A.[ParameterTypeID]='{0}{1}' and A.IsEnable <> 2",
                Framework.SystemID, "019121300000B");

            List<SqlParameter> paramList = new List<SqlParameter>();
            if (!string.IsNullOrWhiteSpace(code))
            {
                sql += string.Format(" and A.Code like @Code ");
                SqlParameter sp = new SqlParameter("@Code", SqlDbType.VarChar);
                sp.Value = "%" + code + "%";
                paramList.Add(sp);
            }
            if (!string.IsNullOrWhiteSpace(useCode))
            {
                sql += string.Format(" and E.ParameterID = @UseCode ");
                SqlParameter sp = new SqlParameter("@UseCode", SqlDbType.VarChar);
                sp.Value = useCode;
                paramList.Add(sp);
            }

            count = UniversalService.getCount(sql, paramList.ToArray());

            String orderby = "A.[IsEnable] desc,A.[Code] ";

            DataTable dt = UniversalService.getTable(select, sql, orderby,
                paramList.Select(p =>
                {
                    SqlParameter sp = new SqlParameter(p.ParameterName, p.DbType);
                    sp.Value = p.Value;
                    return sp;
                }).ToArray(), page, rows);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// 获取语序主档列表
        /// SAM 2017年5月3日10:42:00
        /// </summary>
        /// <param name="Code">代号</param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> Lan00000GetList(string Code, int page, int rows, ref int count)
        {
            string select = string.Format(@"select A.ParameterID,A.Name,A.Code,A.IsEnable,A.Comments,
            D.UserName as Creator,A.CreateLocalTime as CreateTime,
           (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else E.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ");

            string sql = string.Format(@" from [SYS_Parameters] A 
            left join [SYS_MESUsers] D on A.Creator = D.MESUserID
            left join [SYS_MESUsers] E on A.Modifier = E.MESUserID
            where A.[SystemID]='{0}' and A.[ParameterTypeID]='{0}{1}' and A.IsEnable <> 2 ", Framework.SystemID, "019121300000A");

            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@Code",SqlDbType.VarChar),
            };
            parameters[0].Value = DBNull.Value;

            SqlParameter[] Parcount = new SqlParameter[]{
                new SqlParameter("@Code",SqlDbType.VarChar),
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

            String orderby = "A.[IsEnable] desc,A.Code ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, parameters, page, rows);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// 获取语序的导出数据
        /// SAM 2017年5月3日11:27:05
        /// </summary>
        /// <param name="Code">代号</param>
        /// <returns></returns>
        public static DataTable GetLan00000ExportList(string Code)
        {
            string select = string.Format(@"select ROW_NUMBER() OVER (ORDER BY A.[IsEnable] desc,A.Code),A.Code,A.Name,
            (CASE WHEN A.IsEnable=1 THEN '正常' ELSE '作废' END),A.Comments,
            D.UserName as Creator,A.CreateLocalTime as CreateTime,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else E.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ");

            string sql = string.Format(@" from [SYS_Parameters] A 
            left join [SYS_MESUsers] D on A.Creator = D.MESUserID
            left join [SYS_MESUsers] E on A.Modifier = E.MESUserID
            where A.[SystemID]='{0}' and A.[ParameterTypeID]='{0}{1}' and A.IsEnable <> 2 ", Framework.SystemID, "019121300000A");

            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@Code",SqlDbType.VarChar),
            };
            parameters[0].Value = DBNull.Value;

            SqlParameter[] Parcount = new SqlParameter[]{
                new SqlParameter("@Code",SqlDbType.VarChar),
            };
            Parcount[0].Value = DBNull.Value;

            if (!string.IsNullOrWhiteSpace(Code))
            {
                Code = "%" + Code + "%";
                sql = sql + String.Format(@" and A.[Code] like @Code ");
                parameters[0].Value = Code;
                Parcount[0].Value = Code;
            }

            string orderBy = "order By A.[IsEnable] desc,A.Code  ";

            return SQLHelper.ExecuteDataTable(select + sql + orderBy, CommandType.Text, parameters);

        }

        /// <summary>
        /// 分类群码导出
        /// Tom 2017年5月3日22:38:30
        /// </summary>
        /// <param name="Code">代号</param>
        /// <param name="Status">状态</param>
        /// <returns></returns>
        public static DataTable Inf00009GroupCodeExportList(string code, int? status)
        {
            string sql = string.Format(
                @" select ROW_NUMBER() OVER (ORDER BY A.[IsEnable] desc,A.[Code]),A.Code, A.Name, A.Comments, (case when A.IsEnable=1 then '正常' else '作废' end),  
                          C.UserName as Creator,A.CreateLocalTime as CreateTime,
                         (CASE WHEN A.CreateLocalTime = A.ModifiedLocalTime THEN NULL else B.UserName END) as Modifier,
                         (CASE WHEN A.CreateLocalTime = A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime  
                   from [SYS_Parameters] A
                   left join SYS_MESUsers B on A.Modifier = B.MESUserID
                   left join SYS_MESUsers C on A.Creator = C.MESUserID
                   where A.[SystemID]='{0}' and A.[ParameterTypeID]='{0}{1}' and A.IsEnable <> 2",
                Framework.SystemID, "019121300000E");

            List<SqlParameter> paramList = new List<SqlParameter>();
            if (!string.IsNullOrWhiteSpace(code))
            {
                sql += string.Format(" and A.Code like @Code ");
                SqlParameter sp = new SqlParameter("@Code", SqlDbType.VarChar);
                sp.Value = "%" + code + "%";
                paramList.Add(sp);
            }
            if (status != null)
            {
                sql += string.Format(" and A.IsEnable = @IsEnable ");
                SqlParameter sp2 = new SqlParameter("@IsEnable", SqlDbType.TinyInt);
                sp2.Value = status;
                paramList.Add(sp2);
            }

            sql += " ORDER BY A.[IsEnable] desc,A.[Code] ";

            return SQLHelper.ExecuteDataTable(sql, CommandType.Text, paramList.Select(p =>
            {
                SqlParameter sp = new SqlParameter(p.ParameterName, p.DbType);
                sp.Value = p.Value;
                return sp;
            }).ToArray());
        }

        /// <summary>
        /// 分类导出
        /// Tom 2017年5月3日22:54:42
        /// </summary>
        /// <param name="Code">代号</param>
        /// <param name="useCode"></param>
        /// <returns></returns>
        public static DataTable Inf00009ClassExport(string code, string useCode)
        {
            string sql = string.Format(
                @" select ROW_NUMBER() OVER (ORDER BY A.[IsEnable] desc,A.[Code]),E.Code, A.Code, A.Name as Description, A.Comments, (case when A.IsEnable=1 then '正常' else '作废' end),
                          D.Code as GroupCode, D.Name as GroupName,
                            C.UserName as Creator,A.CreateLocalTime as CreateTime,
                         (CASE WHEN A.CreateLocalTime = A.ModifiedLocalTime THEN NULL else B.UserName END) as Modifier,
                         (CASE WHEN A.CreateLocalTime = A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime  
                   from [SYS_Parameters] A
                   left join SYS_MESUsers B on A.Modifier = B.MESUserID
                   left join SYS_MESUsers C on A.Creator = C.MESUserID
                   left join SYS_Parameters D on A.DescriptionOne = D.ParameterID
                   left join SYS_Parameters E on A.Description = E.ParameterID
                   where A.[SystemID]='{0}' and A.[ParameterTypeID]='{0}{1}' and A.IsEnable <> 2",
                Framework.SystemID, "019121300000B");

            List<SqlParameter> paramList = new List<SqlParameter>();
            if (!string.IsNullOrWhiteSpace(code))
            {
                sql += string.Format(" and A.Code like @Code ");
                SqlParameter sp = new SqlParameter("@Code", SqlDbType.VarChar);
                sp.Value = "%" + code + "%";
                paramList.Add(sp);
            }
            if (!string.IsNullOrWhiteSpace(useCode))
            {
                sql += string.Format(" and E.ParameterID = @UseCode ");
                SqlParameter sp = new SqlParameter("@UseCode", SqlDbType.VarChar);
                sp.Value = useCode;
                paramList.Add(sp);
            }

            sql += " ORDER BY A.[IsEnable] desc,A.[Code] ";

            return SQLHelper.ExecuteDataTable(sql, CommandType.Text, paramList.Select(p =>
            {
                SqlParameter sp = new SqlParameter(p.ParameterName, p.DbType);
                sp.Value = p.Value;
                return sp;
            }).ToArray());
        }

        /// <summary>
        /// 单位导出
        /// Tom 2017年5月3日23:11:54
        /// </summary>
        /// <param name="Code">代号</param>
        /// <returns></returns>
        public static DataTable Inf00011ExportList(string code)
        {
            string sql = string.Format(
                @" select ROW_NUMBER() OVER (ORDER BY A.[IsEnable] desc,A.[Code]),A.Code, A.Name, A.Comments, (case when A.IsEnable=1 then '正常' else '作废' end),
                           C.UserName as Creator,A.CreateLocalTime as CreateTime,
                         (CASE WHEN A.CreateLocalTime = A.ModifiedLocalTime THEN NULL else B.UserName END) as Modifier,
                         (CASE WHEN A.CreateLocalTime = A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime  
                   from [SYS_Parameters] A
                   left join SYS_MESUsers B on A.Modifier = B.MESUserID
                   left join SYS_MESUsers C on A.Creator = C.MESUserID
                   where A.[SystemID]='{0}' and A.[ParameterTypeID]='{0}{1}' and A.IsEnable <> 2",
                Framework.SystemID, "019121300000C");

            List<SqlParameter> paramList = new List<SqlParameter>();
            if (!string.IsNullOrWhiteSpace(code))
            {
                sql += string.Format(" and A.Code like @Code ");
                SqlParameter sp = new SqlParameter("@Code", SqlDbType.VarChar);
                sp.Value = "%" + code + "%";
                paramList.Add(sp);
            }

            sql += " order by A.[IsEnable] desc,A.[Code]";

            return SQLHelper.ExecuteDataTable(sql, CommandType.Text, paramList.Select(p =>
            {
                SqlParameter sp = new SqlParameter(p.ParameterName, p.DbType);
                sp.Value = p.Value;
                return sp;
            }).ToArray());
        }

        /// <summary>
        /// 料品属性列表
        /// SAM 2017年5月10日10:07:15
        /// </summary>
        /// <param name="Code">代号</param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> Inf00024GetList(string Code, int page, int rows, ref int count)
        {
            string select = string.Format(@"select A.ParameterID,A.Code,A.Name,A.IsEnable,A.Comments,A.IsDefault,
            D.UserName as Creator,A.CreateLocalTime as CreateTime,
          (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else E.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ");

            string sql = string.Format(@" from [SYS_Parameters] A 
            left join [SYS_MESUsers] D on A.Creator = D.MESUserID
            left join [SYS_MESUsers] E on A.Modifier = E.MESUserID
            where A.[SystemID]='{0}' and A.[ParameterTypeID]='{0}{1}' and A.IsEnable <> 2 ", Framework.SystemID, "019121300000F");

            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@Code",SqlDbType.VarChar),
            };
            parameters[0].Value = DBNull.Value;

            SqlParameter[] Parcount = new SqlParameter[]{
                new SqlParameter("@Code",SqlDbType.VarChar),
            };
            Parcount[0].Value = DBNull.Value;

            if (!string.IsNullOrWhiteSpace(Code))
            {
                Code = "%" + Code.Trim() + "%";
                sql = sql + String.Format(@" and A.[Code] like @Code ");
                parameters[0].Value = Code;
                Parcount[0].Value = Code;
            }

            count = UniversalService.getCount(sql, Parcount);

            String orderby = "A.[IsEnable],A.[Code] ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, parameters, page, rows);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// 料品属性导出
        /// SAM 2017年5月10日11:07:05
        /// </summary>
        /// <param name="Code">代号</param>
        /// <returns></returns>
        public static DataTable Inf00024Export(string Code)
        {
            string select = string.Format(@"select ROW_NUMBER() OVER (ORDER BY A.Code),A.Code,A.Name,
            (CASE WHEN A.IsDefault=1 THEN '是' ELSE '否' END),A.Comments,
            D.UserName as Creator,A.CreateLocalTime as CreateTime,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else E.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ");

            string sql = string.Format(@" from [SYS_Parameters] A 
            left join [SYS_MESUsers] D on A.Creator = D.MESUserID
            left join [SYS_MESUsers] E on A.Modifier = E.MESUserID
            where A.[SystemID]='{0}' and A.[ParameterTypeID]='{0}{1}' and A.IsEnable <> 2 ", Framework.SystemID, "019121300000F");

            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@Code",SqlDbType.VarChar),
            };
            parameters[0].Value = DBNull.Value;

            if (!string.IsNullOrWhiteSpace(Code))
            {
                Code = "%" + Code + "%";
                sql = sql + String.Format(@" and A.[Code] like @Code ");
                parameters[0].Value = Code;
            }

            string orderBy = "order By A.[IsEnable] desc,A.[Code]  ";

            return SQLHelper.ExecuteDataTable(select + sql + orderBy, CommandType.Text, parameters);
        }

        /// <summary>
        /// 获取属性的资料值列表
        /// SAM 2017年5月10日10:57:55
        /// </summary>
        /// <param name="ParameterID"></param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> Inf00024DetailsGetList(string ParameterID, int page, int rows, ref int count)
        {
            string select = string.Format(@"select A.ParameterID,A.Name,A.Comments,A.IsEnable,
            D.Emplno+'-'+D.UserName as Creator,A.CreateLocalTime as CreateTime,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else E.Emplno+'-'+E.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ");

            string sql = string.Format(@" from [SYS_Parameters] A 
            left join [SYS_MESUsers] D on A.Creator = D.MESUserID
            left join [SYS_MESUsers] E on A.Modifier = E.MESUserID
            where A.[SystemID]='{0}' and A.[ParameterTypeID]='{0}{1}' and A.[IsEnable] <> 2 and A.[ParentParameterID] = '{2}' ", Framework.SystemID, "0191213000010", ParameterID);

            count = UniversalService.getCount(sql, null);

            String orderby = "A.[CreateTime] ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, null, page, rows);

            return ToHashtableList(dt);
        }


        /// <summary>
        /// 根据料品属性获取料品资料值（只拿正常）
        /// SAM 2017年7月20日15:35:44
        /// </summary>
        /// <param name="AttributeID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> Sfc00007GetAttributeLList(string AttributeID, int page, int rows, ref int count)
        {
            string select = string.Format(@"select A.ParameterID,A.Name,A.Comments,A.IsEnable,
            D.Emplno+'-'+D.UserName as Creator,A.CreateLocalTime as CreateTime,
          (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else E.Emplno+'-'+E.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ");

            string sql = string.Format(@" from [SYS_Parameters] A 
            left join [SYS_MESUsers] D on A.Creator = D.MESUserID
            left join [SYS_MESUsers] E on A.Modifier = E.MESUserID
            where A.[SystemID]='{0}' and A.[ParameterTypeID]='{0}{1}' and A.[IsEnable] =1 and A.[ParentParameterID] = '{2}' ", Framework.SystemID, "0191213000010", AttributeID);

            count = UniversalService.getCount(sql, null);

            String orderby = "A.[CreateTime] ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, null, page, rows);

            return ToHashtableList(dt);
        }



        /// <summary>
        /// 原因群码列表
        /// SAM 2017年5月11日10:14:05
        /// </summary>
        /// <param name="Code">代号</param>
        /// <param name="Status">状态</param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> Inf00017GetGroupList(string Code, string status, int page, int rows, ref int count)
        {
            string select = string.Format(@"select A.ParameterID,A.Code,A.Name,A.IsEnable,A.Comments,
            D.UserName as Creator,A.CreateLocalTime as CreateTime,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else E.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ");

            string sql = string.Format(@" from [SYS_Parameters] A 
            left join [SYS_MESUsers] D on A.Creator = D.MESUserID
            left join [SYS_MESUsers] E on A.Modifier = E.MESUserID
            where A.[SystemID]='{0}' and A.[ParameterTypeID]='{0}{1}' and A.IsEnable <> 2 ", Framework.SystemID, "0191213000011");

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
                sql = sql + String.Format(@" and A.[Code] like @Code ");
                parameters[0].Value = Code;
                Parcount[0].Value = Code;
            }

            if (!string.IsNullOrWhiteSpace(status))
            {
                sql = sql + String.Format(@" and A.[IsEnable] = @Status ");
                parameters[1].Value = status;
                Parcount[1].Value = status;
            }

            count = UniversalService.getCount(sql, Parcount);

            string orderby = "A.[IsEnable] desc,A.[Code] ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, parameters, page, rows);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// 原因群码导出
        /// SAM 2017年5月11日10:52:46
        /// </summary>
        /// <param name="Code">代号</param>
        /// <param name="Status">状态</param>
        /// <returns></returns>
        public static DataTable Inf00017GroupExport(string Code, string status)
        {
            string select = string.Format(@"select ROW_NUMBER() OVER (ORDER BY A.[IsEnable] desc,A.[Code]),A.Code,A.Name,
            A.Comments,(CASE WHEN A.IsEnable=1 THEN '正常' ELSE '作废' END),
            D.UserName as Creator,A.CreateLocalTime as CreateTime,
           (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else E.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ");

            string sql = string.Format(@" from [SYS_Parameters] A 
            left join [SYS_MESUsers] D on A.Creator = D.MESUserID
            left join [SYS_MESUsers] E on A.Modifier = E.MESUserID
            where A.[SystemID]='{0}' and A.[ParameterTypeID]='{0}{1}' and A.IsEnable <> 2 ", Framework.SystemID, "0191213000011");


            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@Code",SqlDbType.VarChar),
                new SqlParameter("@Status",SqlDbType.VarChar)
            };
            parameters[0].Value = DBNull.Value;
            parameters[1].Value = DBNull.Value;

            if (!string.IsNullOrWhiteSpace(Code))
            {
                Code = "%" + Code + "%";
                sql = sql + String.Format(@" and A.[Code] like @Code ");
                parameters[0].Value = Code;
            }

            if (!string.IsNullOrWhiteSpace(status))
            {
                sql = sql + String.Format(@" and A.[IsEnable] = @Status ");
                parameters[1].Value = status;
            }

            string orderBy = "order By A.[IsEnable] desc,A.[Code] ";

            return SQLHelper.ExecuteDataTable(select + sql + orderBy, CommandType.Text, parameters);
        }

        /// <summary>
        /// 原因码列表
        /// SAM 2017年5月11日10:46:41
        /// </summary>
        /// <param name="Code">代号</param>
        /// <param name="Status">状态</param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> Inf00017GetList(string Code, string status, int page, int rows, ref int count)
        {
            string select = string.Format(@"select A.ParameterID,A.Code,A.Name,A.IsEnable,A.Comments,A.Description,A.DescriptionOne,
            B.Code as DescriptionCode,C.Code as DescriptionOneCode,
            D.UserName as Creator,A.CreateLocalTime as CreateTime,
           (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else E.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ");

            string sql = string.Format(@" from [SYS_Parameters] A 
            left join [SYS_MESUsers] D on A.Creator = D.MESUserID
            left join [SYS_MESUsers] E on A.Modifier = E.MESUserID
            left join [SYS_Parameters] B on A.Description = B.ParameterID
            left join [SYS_Parameters] C on A.DescriptionOne = C.ParameterID
            where A.[SystemID]='{0}' and A.[ParameterTypeID]='{0}{1}' and A.IsEnable <> 2 ", Framework.SystemID, "0191213000012");

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
                sql = sql + String.Format(@" and A.[Code] like @Code ");
                parameters[0].Value = Code;
                Parcount[0].Value = Code;
            }

            if (!string.IsNullOrWhiteSpace(status))
            {
                sql = sql + String.Format(@" and A.[IsEnable] = @Status ");
                parameters[1].Value = status;
                Parcount[1].Value = status;
            }

            count = UniversalService.getCount(sql, Parcount);

            string orderby = "A.[IsEnable] desc,A.[Code] ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, parameters, page, rows);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// 原因码导出
        /// SAM 2017年5月11日11:35:01
        /// </summary>
        /// <param name="Code">代号</param>
        /// <param name="Status">状态</param>
        /// <returns></returns>
        public static DataTable Inf00017Export(string Code, string status)
        {
            string select = string.Format(@"select ROW_NUMBER() OVER (ORDER BY A.[IsEnable] desc,A.[Code]),B.Code as Description,A.Code,A.Name,
            C.Code as DescriptionOne,A.Comments,(CASE WHEN A.IsEnable=1 THEN '正常' ELSE '作废' END),
            D.UserName as Creator,A.CreateLocalTime as CreateTime,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else E.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ");

            string sql = string.Format(@" from [SYS_Parameters] A 
            left join [SYS_MESUsers] D on A.Creator = D.MESUserID
            left join [SYS_MESUsers] E on A.Modifier = E.MESUserID
            left join [SYS_Parameters] B on A.Description = B.ParameterID
            left join [SYS_Parameters] C on A.DescriptionOne = C.ParameterID
            where A.[SystemID]='{0}' and A.[ParameterTypeID]='{0}{1}' and A.IsEnable <> 2 ", Framework.SystemID, "0191213000012");


            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@Code",SqlDbType.VarChar),
                new SqlParameter("@Status",SqlDbType.VarChar)
            };
            parameters[0].Value = DBNull.Value;
            parameters[1].Value = DBNull.Value;

            if (!string.IsNullOrWhiteSpace(Code))
            {
                Code = "%" + Code + "%";
                sql = sql + String.Format(@" and A.[Code] like @Code ");
                parameters[0].Value = Code;
            }

            if (!string.IsNullOrWhiteSpace(status))
            {
                sql = sql + String.Format(@" and A.[IsEnable] = @Status ");
                parameters[1].Value = status;
            }

            string orderBy = "order By A.[IsEnable] desc,A.[Code] ";

            return SQLHelper.ExecuteDataTable(select + sql + orderBy, CommandType.Text, parameters);
        }


        /// <summary>
        /// 获取原因码的弹窗
        /// SAM 2017年5月29日23:35:50
        /// </summary>
        /// <param name="type"></param>
        /// <param name="Code">代号</param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> getReasonList(string type, string GroupID, string Code, string GroupDescription, string Description, int page, int rows, ref int count)
        {
            string select = string.Format(@"select A.ParameterID,A.Code,A.Name,A.Description,A.DescriptionOne,A.IsEnable,C.ParameterID as ReasonGroupID,
             B.Code as UseCode,C.Code as GroupCode,C.Name as GroupName,A.Comments ");

            string sql = string.Format(@" from [SYS_Parameters] A 
            left join [SYS_Parameters] B on A.[Description] = B.[ParameterID]
            left join [SYS_Parameters] C on A.[DescriptionOne] = C.[ParameterID]
            where A.[SystemID]='{0}' and A.[ParameterTypeID]='{0}0191213000012' and  A.[IsEnable]=1", Framework.SystemID);

            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@type",SqlDbType.VarChar),
                new SqlParameter("@GroupDescription",SqlDbType.VarChar),
                new SqlParameter("@Description",SqlDbType.VarChar),
                new SqlParameter("@Code",SqlDbType.VarChar),
                new SqlParameter("@GroupID",SqlDbType.VarChar)
            };
            parameters[0].Value = DBNull.Value;
            parameters[1].Value = DBNull.Value;
            parameters[2].Value = DBNull.Value;
            parameters[3].Value = DBNull.Value;
            parameters[4].Value = DBNull.Value;
            SqlParameter[] Parcount = new SqlParameter[]{
                new SqlParameter("@type",SqlDbType.VarChar),
                new SqlParameter("@GroupDescription",SqlDbType.VarChar),
                new SqlParameter("@Description",SqlDbType.VarChar),
                new SqlParameter("@Code",SqlDbType.VarChar),
                new SqlParameter("@GroupID",SqlDbType.VarChar)
            };
            Parcount[0].Value = DBNull.Value;
            Parcount[1].Value = DBNull.Value;
            Parcount[2].Value = DBNull.Value;
            Parcount[3].Value = DBNull.Value;
            Parcount[4].Value = DBNull.Value;

            if (!string.IsNullOrWhiteSpace(type))
            {
                sql = sql + string.Format(@" and B.[Code] = @type ");
                parameters[0].Value = type;
                Parcount[0].Value = type;
            }

            if (!string.IsNullOrWhiteSpace(GroupDescription))
            {
                GroupDescription = "%" + GroupDescription.Trim() + "%";
                sql = sql + string.Format(@" and C.[Name] collate Chinese_PRC_CI_AS like @GroupDescription ");
                parameters[1].Value = GroupDescription;
                Parcount[1].Value = GroupDescription;
            }


            if (!string.IsNullOrWhiteSpace(Description))
            {
                Description = "%" + Description.Trim() + "%";
                sql = sql + string.Format(@" and A.[Name] collate Chinese_PRC_CI_AS like @Description ");
                parameters[2].Value = Description;
                Parcount[2].Value = Description;
            }

            if (!string.IsNullOrWhiteSpace(Code))
            {
                Code = "%" + Code.Trim() + "%";
                sql = sql + string.Format(@" and A.[Code] collate Chinese_PRC_CI_AS like @Code ");
                parameters[3].Value = Code;
                Parcount[3].Value = Code;
            }

            if (!string.IsNullOrWhiteSpace(GroupID))
            {
                sql = sql + string.Format(@" and A.[DescriptionOne] = @GroupID ");
                parameters[4].Value = GroupID;
                Parcount[4].Value = GroupID;
            }


            count = UniversalService.getCount(sql, Parcount);

            string orderby = " A.[Code] ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, parameters, page, rows);

            return ToHashtableList(dt);
        }


        /// <summary>
        /// 用途别列表
        /// SAM 2017年5月12日10:15:41
        /// </summary>
        /// <param name="Code">代号</param>
        /// <param name="Status">状态</param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> USE00001List(string Code, string status, int page, int rows, ref int count)
        {
            string select = string.Format(@"select A.ParameterID,A.Code,A.Name,A.IsEnable,A.Comments,
            D.UserName as Creator,A.CreateLocalTime as CreateTime,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else E.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ");

            string sql = string.Format(@" from [SYS_Parameters] A 
            left join [SYS_MESUsers] D on A.Creator = D.MESUserID
            left join [SYS_MESUsers] E on A.Modifier = E.MESUserID
            where A.[SystemID]='{0}' and A.[ParameterTypeID]='{0}{1}' and A.IsEnable <> 2 ", Framework.SystemID, "019121300000D");

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
                sql = sql + String.Format(@" and A.[Code] like @Code ");
                parameters[0].Value = Code;
                Parcount[0].Value = Code;
            }

            if (!string.IsNullOrWhiteSpace(status))
            {
                sql = sql + String.Format(@" and A.[IsEnable] = @Status ");
                parameters[1].Value = status;
                Parcount[1].Value = status;
            }

            count = UniversalService.getCount(sql, Parcount);

            string orderby = "A.[IsEnable] desc,A.[Code] ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, parameters, page, rows);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// 用途别导出
        /// SAM 2017年5月12日10:16:08
        /// </summary>
        /// <param name="Code">代号</param>
        /// <param name="Status">状态</param>
        /// <returns></returns>
        public static DataTable USE00001Export(string Code, string status)
        {
            string select = string.Format(@"select ROW_NUMBER() OVER (ORDER BY A.[IsEnable] desc,A.[Code]),A.Code,A.Name,
            A.Comments,(CASE WHEN A.IsEnable=1 THEN '正常' ELSE '作废' END),
            D.UserName as Creator,A.CreateLocalTime as CreateTime,
           (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else E.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ");

            string sql = string.Format(@" from [SYS_Parameters] A 
            left join [SYS_MESUsers] D on A.Creator = D.MESUserID
            left join [SYS_MESUsers] E on A.Modifier = E.MESUserID
            where A.[SystemID]='{0}' and A.[ParameterTypeID]='{0}{1}' and A.IsEnable <> 2 ", Framework.SystemID, "019121300000D");


            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@Code",SqlDbType.VarChar),
                new SqlParameter("@Status",SqlDbType.VarChar)
            };
            parameters[0].Value = DBNull.Value;
            parameters[1].Value = DBNull.Value;

            if (!string.IsNullOrWhiteSpace(Code))
            {
                Code = "%" + Code + "%";
                sql = sql + String.Format(@" and A.[Code] like @Code ");
                parameters[0].Value = Code;
            }

            if (!string.IsNullOrWhiteSpace(status))
            {
                sql = sql + String.Format(@" and A.[IsEnable] = @Status ");
                parameters[1].Value = status;
            }

            string orderBy = "order By A.[IsEnable] desc,A.[Code] ";

            return SQLHelper.ExecuteDataTable(select + sql + orderBy, CommandType.Text, parameters);
        }


        /// <summary>
        /// 资源类别列表
        /// SAM 2017年5月12日10:42:08
        /// </summary>
        /// <param name="Code">代号</param>
        /// <param name="Status">状态</param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> Inf00015ClassList(string Code, string status, int page, int rows, ref int count)
        {
            string select = string.Format(@"select A.ParameterID,A.Code,A.Name,A.IsEnable,A.Comments,
            (CASE WHEN B.Emplno is null or B.Emplno = '' THEN B.Account else B.Emplno END)+'-'+B.UserName as Creator,A.CreateLocalTime as CreateTime,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else (CASE WHEN C.Emplno is null or C.Emplno = '' THEN C.Account else C.Emplno END)+'-'+C.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ");

            string sql = string.Format(@" from [SYS_Parameters] A 
            left join [SYS_MESUsers] B on A.Creator = B.MESUserID
            left join [SYS_MESUsers] C on A.Modifier = C.MESUserID
            where A.[SystemID]='{0}' and A.[ParameterTypeID]='{0}0191213000013' and A.IsEnable <> 2 ", Framework.SystemID);

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
                Code = "%" + Code.Trim() + "%";
                sql = sql + String.Format(@" and A.[Code] collate Chinese_PRC_CI_AS like @Code ");
                parameters[0].Value = Code;
                Parcount[0].Value = Code;
            }

            if (!string.IsNullOrWhiteSpace(status))
            {
                sql = sql + String.Format(@" and A.[IsEnable] = @Status ");
                parameters[1].Value = status;
                Parcount[1].Value = status;
            }

            count = UniversalService.getCount(sql, Parcount);

            string orderby = "A.[IsEnable] desc,A.[Code] ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, parameters, page, rows);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// 资源类别导出
        /// SAM 2017年5月12日10:42:05
        /// </summary>
        /// <param name="Code">代号</param>
        /// <param name="Status">状态</param>
        /// <returns></returns>
        public static DataTable Inf00015ClassExport(string Code, string status)
        {
            string select = string.Format(@"select ROW_NUMBER() OVER (ORDER BY A.[IsEnable] desc,A.[Code]),A.Code,A.Name,
            A.Comments,(CASE WHEN A.IsEnable=1 THEN '正常' ELSE '作废' END),
            (CASE WHEN B.Emplno is null or B.Emplno = '' THEN B.Account else B.Emplno END)+'-'+B.UserName as Creator,A.CreateLocalTime as CreateTime,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else (CASE WHEN C.Emplno is null or C.Emplno = '' THEN C.Account else C.Emplno END)+'-'+C.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ");

            string sql = string.Format(@" from [SYS_Parameters] A 
            left join [SYS_MESUsers] B on A.Creator = B.MESUserID
            left join [SYS_MESUsers] C on A.Modifier = C.MESUserID
            where A.[SystemID]='{0}' and A.[ParameterTypeID]='{0}{1}' and A.IsEnable <> 2 ", Framework.SystemID, "0191213000013");


            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@Code",SqlDbType.VarChar),
                new SqlParameter("@Status",SqlDbType.VarChar)
            };
            parameters[0].Value = DBNull.Value;
            parameters[1].Value = DBNull.Value;

            if (!string.IsNullOrWhiteSpace(Code))
            {
                Code = "%" + Code.Trim() + "%";
                sql = sql + String.Format(@" and A.[Code] collate Chinese_PRC_CI_AS @Code ");
                parameters[0].Value = Code;
            }

            if (!string.IsNullOrWhiteSpace(status))
            {
                sql = sql + String.Format(@" and A.[IsEnable] = @Status ");
                parameters[1].Value = status;
            }

            string orderBy = "order By A.[IsEnable] desc,A.[Code] ";

            return SQLHelper.ExecuteDataTable(select + sql + orderBy, CommandType.Text, parameters);
        }

        /// <summary>
        /// 资源群组列表
        /// SAM 2017年5月12日11:00:03
        /// </summary>
        /// <param name="Code">代号</param>
        /// <param name="Status">状态</param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> Inf00015GroupList(string Code, string status, int page, int rows, ref int count)
        {
            string select = string.Format(@"select A.ParameterID,A.Code,A.Name,A.IsEnable,A.Comments,
            (CASE WHEN B.Emplno is null or B.Emplno = '' THEN B.Account else B.Emplno END)+'-'+B.UserName as Creator,A.CreateLocalTime as CreateTime,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else (CASE WHEN C.Emplno is null or C.Emplno = '' THEN C.Account else C.Emplno END)+'-'+C.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ");

            string sql = string.Format(@" from [SYS_Parameters] A 
            left join [SYS_MESUsers] B on A.Creator = B.MESUserID
            left join [SYS_MESUsers] C on A.Modifier = C.MESUserID
            where A.[SystemID]='{0}' and A.[ParameterTypeID]='{0}{1}' and A.IsEnable <> 2 ", Framework.SystemID, "0191213000014");

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
                Code = "%" + Code.Trim() + "%";
                sql = sql + String.Format(@" and A.[Code] collate Chinese_PRC_CI_AS like @Code ");
                parameters[0].Value = Code;
                Parcount[0].Value = Code;
            }

            if (!string.IsNullOrWhiteSpace(status))
            {
                sql = sql + String.Format(@" and A.[IsEnable] = @Status ");
                parameters[1].Value = status;
                Parcount[1].Value = status;
            }

            count = UniversalService.getCount(sql, Parcount);

            string orderby = "A.[IsEnable] desc,A.[Code] ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, parameters, page, rows);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// 资源群组导出
        /// SAM 2017年5月12日11:00:27
        /// </summary>
        /// <param name="Code">代号</param>
        /// <param name="Status">状态</param>
        /// <returns></returns>
        public static DataTable Inf00015GroupExport(string Code, string status)
        {
            string select = string.Format(@"select ROW_NUMBER() OVER (ORDER BY A.[IsEnable] desc,A.[Code]),A.Code,A.Name,
            A.Comments,(CASE WHEN A.IsEnable=1 THEN '正常' ELSE '作废' END),
            (CASE WHEN B.Emplno is null or B.Emplno = '' THEN B.Account else B.Emplno END)+'-'+B.UserName as Creator,A.CreateLocalTime as CreateTime,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else (CASE WHEN C.Emplno is null or C.Emplno = '' THEN C.Account else C.Emplno END)+'-'+C.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ");

            string sql = string.Format(@" from [SYS_Parameters] A 
            left join [SYS_MESUsers] B on A.Creator = B.MESUserID
            left join [SYS_MESUsers] C on A.Modifier = C.MESUserID
            where A.[SystemID]='{0}' and A.[ParameterTypeID]='{0}{1}' and A.IsEnable <> 2 ", Framework.SystemID, "0191213000014");


            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@Code",SqlDbType.VarChar),
                new SqlParameter("@Status",SqlDbType.VarChar)
            };
            parameters[0].Value = DBNull.Value;
            parameters[1].Value = DBNull.Value;

            if (!string.IsNullOrWhiteSpace(Code))
            {
                Code = "%" + Code.Trim() + "%";
                sql = sql + String.Format(@" and A.[Code] collate Chinese_PRC_CI_AS like @Code ");
                parameters[0].Value = Code;
            }

            if (!string.IsNullOrWhiteSpace(status))
            {
                sql = sql + String.Format(@" and A.[IsEnable] = @Status ");
                parameters[1].Value = status;
            }

            string orderBy = "order By A.[IsEnable] desc,A.[Code] ";

            return SQLHelper.ExecuteDataTable(select + sql + orderBy, CommandType.Text, parameters);
        }

        /// <summary>
        /// 判断原因群码是否被原因码使用着
        /// SAM 2017年5月12日11:23:29
        /// </summary>
        /// <param name="ParameterID"></param>
        /// <returns></returns>
        public static bool Inf00017CheckGroup(string ParameterID)
        {
            string sql = string.Format(@"select * from [SYS_Parameters] where [DescriptionOne] = '{0}' and [SystemID] = '{1}' and [IsEnable] <> 2 ", ParameterID, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return false;
            else
                return true;
        }

        /// <summary>
        /// 判断原因码是否被其他档使用
        /// MOUSE 2017年7月27日14:50:49
        /// </summary>
        /// <param name="ParameterID"></param>
        /// <returns></returns>
        public static bool Inf00017Check(string ParameterID)
        {
            //表SFC_AbnormalDetails是否有对应的ReasonID
            string sql1 = string.Format(@"select * from [SFC_AbnormalDetails] where [ReasonID] = '{0}' and [SystemID] = '{1}'  ", ParameterID, Framework.SystemID);
            DataTable dt1 = SQLHelper.ExecuteDataTable(sql1, CommandType.Text);
            if (dt1.Rows.Count != 0)
                return true;
            //表SFC_AbnormalQuantity是否有对应的ReasonID
            string sql2 = string.Format(@"select * from [SFC_AbnormalQuantity] where [ReasonID] = '{0}' and [SystemID] = '{1}'  ", ParameterID, Framework.SystemID);
            DataTable dt2 = SQLHelper.ExecuteDataTable(sql2, CommandType.Text);
            if (dt2.Rows.Count != 0)
                return true;

            //表SFC_AbnormalHour是否有对应的ReasonID
            string sql3 = string.Format(@"select * from [SFC_AbnormalHour] where [ReasonID] = '{0}' and [SystemID] = '{1}'  ", ParameterID, Framework.SystemID);
            DataTable dt3 = SQLHelper.ExecuteDataTable(sql3, CommandType.Text);
            if (dt3.Rows.Count != 0)
                return true;

            //表EMS_CalledRepairReason是否有对应的ReasonID
            string sql4 = string.Format(@"select * from [EMS_CalledRepairReason] where [ReasonID] = '{0}' and [SystemID] = '{1}'  ", ParameterID, Framework.SystemID);
            DataTable dt4 = SQLHelper.ExecuteDataTable(sql4, CommandType.Text);
            if (dt4.Rows.Count != 0)
                return true;

            //表EMS_ServiceReasonLog是否有对应的ReasonID
            string sql5 = string.Format(@"select * from [EMS_ServiceReasonLog] where [ReasonID] = '{0}' and [SystemID] = '{1}'  ", ParameterID, Framework.SystemID);
            DataTable dt5 = SQLHelper.ExecuteDataTable(sql5, CommandType.Text);
            if (dt5.Rows.Count != 0)
                return true;

            //表QCS_InspectionDocumentReason是否有对应的ReasonID
            string sql6 = string.Format(@"select * from [QCS_InspectionDocumentReason] where [ReasonID] = '{0}' and [SystemID] = '{1}'  ", ParameterID, Framework.SystemID);
            DataTable dt6 = SQLHelper.ExecuteDataTable(sql6, CommandType.Text);
            if (dt6.Rows.Count != 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 判断用于是否被其他主档使用着
        /// SAM 2017年5月12日11:26:56
        /// </summary>
        /// <param name="ParameterID"></param>
        /// <returns></returns>
        public static bool USE00001Check(string ParameterID)
        {
            string sql = string.Format(@"select * from [SYS_Parameters] where [Description] = '{0}' and [SystemID] = '{1}' and [IsEnable] <> 2 ", ParameterID, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return false;
            else
                return true;
        }



        /// <summary>
        /// 判断分类群码是否在分类中使用
        /// SAM 2017年5月17日10:59:16
        /// </summary>
        /// <param name="ParameterID"></param>
        /// <returns></returns>
        public static bool inf00009Check(string ParameterID)
        {
            string sql = string.Format(@"select * from [SYS_Parameters] where [DescriptionOne] = '{0}' and [SystemID] = '{1}' and [IsEnable] <> 2 and [ParameterTypeID]='{1}019121300000B' ", ParameterID, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return false;
            else
                return true;
        }

        /// <summary>
        /// 制程替代群组列表
        /// SAM 2017年5月23日09:29:42
        /// </summary>
        /// <param name="Code">代号</param>
        /// <param name="Status">状态</param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> Inf00021GetList(string Code, string status, int page, int rows, ref int count)
        {
            string select = string.Format(@"select A.ParameterID,A.Code,A.Name,A.IsEnable,A.Comments,
            D.UserName as Creator,A.CreateLocalTime as CreateTime,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else E.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ");

            string sql = string.Format(@" from [SYS_Parameters] A 
            left join [SYS_MESUsers] D on A.Creator = D.MESUserID
            left join [SYS_MESUsers] E on A.Modifier = E.MESUserID
            where A.[SystemID]='{0}' and A.[ParameterTypeID]='{0}{1}' and A.IsEnable <> 2 ", Framework.SystemID, "0191213000015");

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
                sql = sql + String.Format(@" and A.[Code] like @Code ");
                parameters[0].Value = Code;
                Parcount[0].Value = Code;
            }

            if (!string.IsNullOrWhiteSpace(status))
            {
                sql = sql + String.Format(@" and A.[IsEnable] = @Status ");
                parameters[1].Value = status;
                Parcount[1].Value = status;
            }

            count = UniversalService.getCount(sql, Parcount);

            string orderby = "A.[IsEnable] desc,A.[Code] ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, parameters, page, rows);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// 制程替代群组导出
        /// SAM 2017年5月23日09:30:21
        /// </summary>
        /// <param name="Code">代号</param>
        /// <param name="Status">状态</param>
        /// <returns></returns>
        public static DataTable Inf00021Export(string Code, string status)
        {
            string select = string.Format(@"select ROW_NUMBER() OVER (ORDER BY A.[IsEnable] desc,A.[Code]),A.Code,A.Name,
            A.Comments,(CASE WHEN A.IsEnable=1 THEN '正常' ELSE '作废' END),
            D.UserName as Creator,A.CreateLocalTime as CreateTime,
           (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else E.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ");

            string sql = string.Format(@" from [SYS_Parameters] A 
            left join [SYS_MESUsers] D on A.Creator = D.MESUserID
            left join [SYS_MESUsers] E on A.Modifier = E.MESUserID
            where A.[SystemID]='{0}' and A.[ParameterTypeID]='{0}{1}' and A.IsEnable <> 2 ", Framework.SystemID, "0191213000015");


            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@Code",SqlDbType.VarChar),
                new SqlParameter("@Status",SqlDbType.VarChar)
            };
            parameters[0].Value = DBNull.Value;
            parameters[1].Value = DBNull.Value;

            if (!string.IsNullOrWhiteSpace(Code))
            {
                Code = "%" + Code + "%";
                sql = sql + String.Format(@" and A.[Code] like @Code ");
                parameters[0].Value = Code;
            }

            if (!string.IsNullOrWhiteSpace(status))
            {
                sql = sql + String.Format(@" and A.[IsEnable] = @Status ");
                parameters[1].Value = status;
            }

            string orderBy = "order By A.[IsEnable] desc,A.[Code] ";

            return SQLHelper.ExecuteDataTable(select + sql + orderBy, CommandType.Text, parameters);
        }


        /// <summary>
        /// 制程的导出
        /// SAM 2017年5月25日10:30:33
        /// </summary>
        /// <param name="Code">代号</param>
        /// <param name="Status">状态</param>
        /// <returns></returns>
        public static DataTable Inf00018OProcessExport(string Code, string status)
        {
            string select = string.Format(@"select ROW_NUMBER() OVER (ORDER BY A.[IsEnable] desc,A.[Code]),A.Code,A.Name,
            (CASE WHEN A.IsDefault=1 THEN 'Y' ELSE 'N' END),
            (CASE WHEN A.IsEnable=1 THEN '正常' ELSE '作废' END),A.Comments,
            D.UserName as Creator,A.CreateLocalTime as CreateTime,
           (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else E.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ");

            string sql = string.Format(@" from [SYS_Parameters] A 
            left join [SYS_MESUsers] D on A.Creator = D.MESUserID
            left join [SYS_MESUsers] E on A.Modifier = E.MESUserID
            where A.[SystemID]='{0}' and A.[ParameterTypeID]='{0}{1}' and A.IsEnable <> 2 ", Framework.SystemID, "0191213000017");


            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@Code",SqlDbType.VarChar),
                new SqlParameter("@Status",SqlDbType.VarChar)
            };
            parameters[0].Value = DBNull.Value;
            parameters[1].Value = DBNull.Value;

            if (!string.IsNullOrWhiteSpace(Code))
            {
                Code = "%" + Code + "%";
                sql = sql + String.Format(@" and A.[Code] like @Code ");
                parameters[0].Value = Code;
            }

            if (!string.IsNullOrWhiteSpace(status))
            {
                sql = sql + String.Format(@" and A.[IsEnable] = @Status ");
                parameters[1].Value = status;
            }

            string orderBy = "order By A.[IsEnable] desc,A.[Code] ";

            return SQLHelper.ExecuteDataTable(select + sql + orderBy, CommandType.Text, parameters);
        }

        /// <summary>
        /// 根据替代群组获取不属于他的制程列表
        /// SAM 2017年5月25日14:37:01
        /// </summary>
        /// <param name="groupID"></param>
        /// <returns></returns>
        public static object Inf00021ProcessList(string groupID, string Code)
        {
            string select = string.Format(@"select null as AGDetailID,A.ParameterID as DetailID,A.Code,A.Name,  C.Code as Workcenter, C.Name as WorkcenterName ");

            string sql = string.Format(@"
            from [SYS_Parameters] A 
            left join [SYS_WorkCenterProcess] B on A.ParameterID = B.ProcessID
            left join [SYS_WorkCenter] C on B.WorkCenterID = C.WorkCenterID
            where [ParameterID] not in (select DetailID from SYS_AlternativeGroupDetails where [GroupID] ='{1}' and [Status] = '{0}0201213000001')
            and A.[SystemID]='{0}' and A.[IsEnable] =1 and [ParameterTypeID] = '{0}0191213000017'", Framework.SystemID, groupID);

            if (!string.IsNullOrWhiteSpace(Code))
                sql += string.Format(@"and A.Code like '%{0}%' ", Code);


            string orderBy = "order By A.[Code] ";

            DataTable dt = SQLHelper.ExecuteDataTable(select + sql + orderBy, CommandType.Text, null);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// 检验类别的导出
        /// SAM 2017年6月9日11:37:00
        /// TODO 未完成
        /// </summary>
        /// <param name="Code"></param>
        /// <returns></returns>
        public static DataTable Qcs00002TypeExport(string Code)
        {
            string select = string.Format(@"select ROW_NUMBER() OVER (ORDER BY A.[IsEnable] desc,A.[Code]),A.Code,A.Name,
            A.Comments,
            B.UserName as Creator,A.CreateLocalTime as CreateTime,
           (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else C.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ");

            string sql = string.Format(@" from [SYS_Parameters] A 
            left join [SYS_MESUsers] B on A.Creator = B.MESUserID
            left join [SYS_MESUsers] C on A.Modifier = C.MESUserID
            where A.[SystemID]='{0}' and A.[ParameterTypeID]='{0}{1}' and A.IsEnable <> 2 ", Framework.SystemID, "0191213000017");


            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@Code",SqlDbType.VarChar)
            };
            parameters[0].Value = DBNull.Value;

            if (!string.IsNullOrWhiteSpace(Code))
            {
                Code = "%" + Code + "%";
                sql = sql + String.Format(@" and A.[Code] like @Code ");
                parameters[0].Value = Code;
            }

            string orderBy = "order By A.[IsEnable] desc,A.[Code] ";

            return SQLHelper.ExecuteDataTable(select + sql + orderBy, CommandType.Text, parameters);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="ProcessID"></param>
        /// <param name="Code"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> Sfc00001GetOperationList(string ProcessID, string Code, int page, int rows, ref int count)
        {
            string select = string.Format(@"select A.ParameterID,A.Code,A.Name,A.IsEnable,A.Comments,
            D.UserName as Creator,A.CreateLocalTime as CreateTime,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else E.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ");

            string sql = string.Format(@" from [SYS_Parameters] A 
            left join [SYS_MESUsers] D on A.Creator = D.MESUserID
            left join [SYS_MESUsers] E on A.Modifier = E.MESUserID
            where A.[SystemID]='{0}' and A.[ParameterTypeID]='{0}{1}' and A.IsEnable = 1 
            and A.ParameterID in (select [OperationID] from [SYS_ProcessOperation] where [ProcessID] = '{2}' and  [Status] = '{0}0201213000001')
            ", Framework.SystemID, "0191213000016", ProcessID);

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

            string orderby = "A.[Code] ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, parameters, page, rows);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// 資料拋轉參數設定列表
        /// SAM 2017年7月5日10:03:15
        /// </summary>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> Trn00001GetList(int page, int rows, ref int count)
        {
            string select = string.Format(@"select A.ParameterID ");

            string sql = string.Format(@" from [SYS_Parameters] A 
           where A.[SystemID]='{0}' and A.[ParameterTypeID]='{0}0191213000020' and A.IsEnable=1", Framework.SystemID);

            count = UniversalService.getCount(sql, null);

            string orderby = "A.[CreateTime] desc";

            DataTable dt = UniversalService.getTable(select, sql, orderby, null, page, rows);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// 保养项目主档列表
        /// SAM 2017年7月5日14:29:05
        /// </summary>
        /// <param name="Code"></param>
        /// <param name="Name"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> Ems00008GetProjectList(string Code, string Name, int page, int rows, ref int count)
        {
            string select = string.Format(@"select A.ParameterID,A.Code,A.Name,A.IsEnable,A.Comments,A.Description as Attribute,
           (CASE WHEN B.Emplno is null or B.Emplno = '' THEN B.Account else B.Emplno END)+'-'+B.UserName as Creator,A.CreateLocalTime as CreateTime,
           (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else (CASE WHEN C.Emplno is null or C.Emplno = '' THEN C.Account else C.Emplno END)+'-'+C.UserName END) as Modifier,
           (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ");

            string sql = string.Format(@" from [SYS_Parameters] A 
            left join [SYS_MESUsers] B on A.Creator = B.MESUserID
            left join [SYS_MESUsers] C on A.Modifier = C.MESUserID
            where A.[SystemID]='{0}' and A.[ParameterTypeID]='{0}0191213000022' and A.IsEnable <> 2 ", Framework.SystemID);

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

            string orderby = "A.[IsEnable] desc,A.[Code] ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, parameters, page, rows);

            return ToHashtableList(dt);
        }

        /// <summary>
        ///  保养项目主档导出
        ///  SAM 2017年7月5日15:53:20
        /// </summary>
        /// <param name="Code"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public static DataTable Ems00008ProjectExport(string Code, string Name)
        {
            string select = string.Format(@"select ROW_NUMBER() OVER (ORDER BY A.[IsEnable] desc,A.[Code]),A.Code,A.Name,
            (Select [Name] From [SYS_Parameters] where ParameterID = A.Description ) as Attribute,
            A.Comments,(CASE WHEN A.IsEnable=1 THEN '正常' ELSE '作废' END),
           (CASE WHEN B.Emplno is null or B.Emplno = '' THEN B.Account else B.Emplno END)+'-'+B.UserName as Creator,A.CreateLocalTime as CreateTime,
           (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else (CASE WHEN C.Emplno is null or C.Emplno = '' THEN C.Account else C.Emplno END)+'-'+C.UserName END) as Modifier,
           (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ");

            string sql = string.Format(@" from [SYS_Parameters] A 
             left join [SYS_MESUsers] B on A.Creator = B.MESUserID
            left join [SYS_MESUsers] C on A.Modifier = C.MESUserID
            where A.[SystemID]='{0}' and A.[ParameterTypeID]='{0}0191213000022' and A.IsEnable <> 2 ", Framework.SystemID);

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

            string orderBy = "order By A.[IsEnable] desc,A.[Code] ";

            return SQLHelper.ExecuteDataTable(select + sql + orderBy, CommandType.Text, parameters);
        }



        /// <summary>
        /// 获取保养类型列表
        /// SAM 2017年7月5日14:43:13
        /// </summary>
        /// <param name="Code"></param>
        /// <param name="Name"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> Ems00008GetTypeList(string Code, string Name, int page, int rows, ref int count)
        {
            string select = string.Format(@"select A.ParameterID,A.Code,A.Name,A.IsEnable,A.Comments,
           (CASE WHEN B.Emplno is null or B.Emplno = '' THEN B.Account else B.Emplno END)+'-'+B.UserName as Creator,A.CreateLocalTime as CreateTime,
           (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else (CASE WHEN C.Emplno is null or C.Emplno = '' THEN C.Account else C.Emplno END)+'-'+C.UserName END) as Modifier,
           (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ");

            string sql = string.Format(@" from [SYS_Parameters] A 
            left join [SYS_MESUsers] B on A.Creator = B.MESUserID
            left join [SYS_MESUsers] C on A.Modifier = C.MESUserID
            where A.[SystemID]='{0}' and A.[ParameterTypeID]='{0}0191213000023' and A.IsEnable <> 2 ", Framework.SystemID);

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

            string orderby = "A.[IsEnable] desc,A.[Code] ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, parameters, page, rows);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// 保养类型导出
        /// SAM 2017年7月5日15:54:23
        /// </summary>
        /// <param name="Code"></param>
        /// <param name="Name"></param>
        /// <returns></returns>
        public static DataTable Ems00008TypeExport(string Code, string Name)
        {
            string select = string.Format(@"select ROW_NUMBER() OVER (ORDER BY A.[IsEnable] desc,A.[Code]),A.Code,A.Name,
            A.Comments,(CASE WHEN A.IsEnable=1 THEN '正常' ELSE '作废' END),
            (CASE WHEN B.Emplno is null or B.Emplno = '' THEN B.Account else B.Emplno END)+'-'+B.UserName as Creator,A.CreateLocalTime as CreateTime,
           (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else (CASE WHEN C.Emplno is null or C.Emplno = '' THEN C.Account else C.Emplno END)+'-'+C.UserName END) as Modifier,
           (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ");

            string sql = string.Format(@" from [SYS_Parameters] A 
             left join [SYS_MESUsers] B on A.Creator = B.MESUserID
            left join [SYS_MESUsers] C on A.Modifier = C.MESUserID
            where A.[SystemID]='{0}' and A.[ParameterTypeID]='{0}0191213000023' and A.IsEnable <> 2 ", Framework.SystemID);

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

            string orderBy = "order By A.[IsEnable] desc,A.[Code] ";

            return SQLHelper.ExecuteDataTable(select + sql + orderBy, CommandType.Text, parameters);
        }

        /// <summary>
        ///  标准检验规范设定-检验群码页签列表
        ///  SAM 2017年7月6日14:05:01
        /// </summary>
        /// <param name="Code"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> Qcs00004GetGroupList(string Code, int page, int rows, ref int count)
        {
            string select = string.Format(@"select A.ParameterID,A.Code,A.Name,A.IsEnable,A.Comments,
            B.Emplno+'-'+B.UserName as Creator,A.CreateLocalTime as CreateTime,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else C.Emplno+'-'+C.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ");

            string sql = string.Format(@" from [SYS_Parameters] A 
            left join [SYS_MESUsers] B on A.Creator = B.MESUserID
            left join [SYS_MESUsers] C on A.Modifier = C.MESUserID
            where A.[SystemID]='{0}' and A.[ParameterTypeID]='{0}0191213000018' and A.IsEnable = 1 ", Framework.SystemID);

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

            string orderby = "A.[Code] ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, parameters, page, rows);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// 不存在于保养工单的保养项目列表
        /// SAM 2017年7月9日17:01:29
        /// </summary>
        /// <param name="MaintenanceOrderID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> EMSGetEquMaiProjectList(string MaintenanceOrderID)
        {
            string select = string.Format(@"select A.ParameterID,A.Code,A.Name,A.IsEnable,A.Comments,D.Name as Attribute,
            B.Emplno+'-'+B.UserName as Creator,A.CreateLocalTime as CreateTime,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else C.Emplno+'-'+C.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ");

            string sql = string.Format(@" from [SYS_Parameters] A 
            left join [SYS_MESUsers] B on A.Creator = B.MESUserID
            left join [SYS_MESUsers] C on A.Modifier = C.MESUserID
            left join [SYS_Parameters] D on A.Description = D.ParameterID
            where A.[SystemID]='{0}' and A.[ParameterTypeID]='{0}0191213000022' and A.IsEnable = 1 ", Framework.SystemID);

            sql += string.Format(@"and A.[ParameterID] not in (select [MaiProjectID] from [EMS_MaiOrderProject] where [MaintenanceOrderID] = '{0}' and [Status]='{1}0201213000001')", MaintenanceOrderID, Framework.SystemID);

            string orderby = " order by A.[Code] ";

            DataTable dt = SQLHelper.ExecuteDataTable(select + sql + orderby, CommandType.Text);

            return ToHashtableList(dt);
        }


        /// <summary>
        /// 获取指定清单未设定的保养项目列表
        /// SAM 2017年7月14日11:33:39
        /// </summary>
        /// <param name="EquipmentMaintenanceListID"></param>
        /// <param name="Code"></param>
        /// <param name="Name"></param>
        /// <returns></returns>
        public static IList<Hashtable> EMS00008ListDetailAdd(string EquipmentMaintenanceListID, string Code, string Name)
        {
            string select = string.Format(@"select null as EquipmentMaintenanceListDetailID,A.ParameterID as DetailID,A.Code,A.Name,
            (select Name from SYS_Parameters where ParameterID = A.Description) as Attribute,A.Comments  ");

            string sql = string.Format(@"  from [SYS_Parameters] A 
            where [ParameterID] not in (select [DetailID] from [EMS_EquipmentMaintenanceListDetails] where [EquipmentMaintenanceListID] ='{1}' and [Status] = '{0}0201213000001')
            and A.[SystemID]='{0}' and A.[IsEnable] =1 and [ParameterTypeID] = '{0}0191213000022'", Framework.SystemID, EquipmentMaintenanceListID);

            if (!string.IsNullOrWhiteSpace(Code))
                sql += string.Format(@"and A.Code like '%{0}%' ", Code);

            if (!string.IsNullOrWhiteSpace(Name))
                sql += string.Format(@"and A.Name like '%{0}%' ", Name);

            string orderBy = "order By A.[Code] ";

            DataTable dt = SQLHelper.ExecuteDataTable(select + sql + orderBy, CommandType.Text, null);

            return ToHashtableList(dt);
        }


        /// <summary>
        /// 根据工序流水号获取不属于他的制程(不分页)
        /// Joint 2017年7月26日16:01:34
        /// </summary>
        /// <param name="OperationID"></param>
        /// <returns></returns>
        public static object Inf00018GetNoProcessList(string OperationID)
        {
            string select = string.Format(@"select null as ProcessOperationID,A.ParameterID as ProcessID,A.Code as ProcessNo,A.Name as ProcessDescription,A.IsDefault as EnableProcess,
            (CASE WHEN A.IsEnable=1 THEN '正常' ELSE '作废' END) as Status");

            string sql = string.Format(@"  from [SYS_Parameters] A 
            where [ParameterID] not in (select [ProcessID] from [SYS_ProcessOperation] where [OperationID]='{1}' and [Status]='{0}0201213000001')        
            and A.[SystemID]='{0}' and A.[ParameterTypeID]='{0}0191213000017' and A.[IsEnable]='1' ", Framework.SystemID, OperationID);

            string orderBy = "order By A.[Code] ";

            DataTable dt = SQLHelper.ExecuteDataTable(select + sql + orderBy, CommandType.Text);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// 根据工作中心流水号获取不属于他的制程(不分页)
        /// Joint 2017年7月28日17:22:36
        /// </summary>
        /// <param name="WorkCenterID"></param>
        /// <returns></returns>
        public static object Inf00018GetNoProcessWorkCenterList(string WorkCenterID)
        {
            string select = string.Format(@"select null as WorkCenterProcessID,A.ParameterID as ProcessID,A.Code as ProcessNo,A.Name as ProcessDescription,A.IsDefault as EnableProcess,
            (CASE WHEN A.IsEnable=1 THEN '正常' ELSE '作废' END) as Status");

            string sql = string.Format(@"  from [SYS_Parameters] A 
            where [ParameterID] not in (select [ProcessID] from [SYS_WorkCenterProcess] where [WorkCenterID]='{1}' and [Status]='{0}0201213000001')        
            and A.[SystemID]='{0}' and A.[ParameterTypeID]='{0}0191213000017' and A.[IsEnable]='1' ", Framework.SystemID, WorkCenterID);

            string orderBy = "order By A.[Code] ";

            DataTable dt = SQLHelper.ExecuteDataTable(select + sql + orderBy, CommandType.Text);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// 根据制程流水号获取不属于他的工序(不分页)
        /// Joint 2017年7月27日14:58:51
        /// </summary>
        /// <param name="ProcessID"></param>
        /// <returns></returns>
        public static object Inf00018GetNoOperationList(string ProcessID)
        {
            string select = string.Format(@"select null as ProcessOperationID,A.ParameterID as OperationID,A.Code as WorkOrderNo,A.Name as WorkOrderDescription,
            (CASE WHEN A.IsEnable=1 THEN '正常' ELSE '作废' END) as Status");

            string sql = string.Format(@"  from [SYS_Parameters] A 
            where [ParameterID] not in (select [OperationID] from [SYS_ProcessOperation] where [ProcessID]='{1}' and [Status]='{0}0201213000001')        
            and A.[SystemID]='{0}' and A.[ParameterTypeID]='{0}0191213000016' and A.[IsEnable]='1' ", Framework.SystemID, ProcessID);

            string orderBy = "order By A.[Code] ";

            DataTable dt = SQLHelper.ExecuteDataTable(select + sql + orderBy, CommandType.Text);

            return ToHashtableList(dt);
        }


        /// <summary>
        /// 获取机况设定
        /// SAM 2017年7月31日17:05:03
        /// </summary>
        /// <param name="Code"></param>
        /// <param name="Name"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> Ems00001GetConditionList(string Code, string Name, int page, int rows, ref int count)
        {
            string select = string.Format(@"select A.ParameterID,A.Code,A.Name,A.IsEnable,A.Comments,A.Description,
            B.Emplno+'-'+B.UserName as Creator,A.CreateLocalTime as CreateTime,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else C.Emplno+'-'+C.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ");

            string sql = string.Format(@" from [SYS_Parameters] A 
            left join [SYS_MESUsers] B on A.Creator = B.MESUserID
            left join [SYS_MESUsers] C on A.Modifier = C.MESUserID
            where A.[SystemID]='{0}' and A.[ParameterTypeID]='{0}0191213000055' and A.IsEnable = 1 ", Framework.SystemID);

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

            string orderby = "A.[Code] ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, parameters, page, rows);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// 获取语序下拉框
        /// Sam 2017年9月28日14:19:14
        /// ObjectID字段为所选中需要做语序的记录的流水号
        /// 用于过滤掉已经设置好的语序
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="ObjectID"></param>
        /// <returns></returns>
        public static IList<Hashtable> GetLanguageList(string ObjectID)
        {
            string select = string.Format(@"select A.[ParameterID],A.[Code],A.[Name],A.[Code]+'-'+A.[Name] as Value ");

            string sql = string.Format(@" from [SYS_Parameters] A where 
            A.[SystemID]='{0}' and [ParameterTypeID]='{0}019121300000A' and [IsEnable]=1 
            and A.[ParameterID] not in (select [LanguageCode] from [SYS_LanguageLib] where [RowID] ='{1}')", Framework.SystemID, ObjectID);

            String orderby = " order by A.[Code]";

            DataTable dt = SQLHelper.ExecuteDataTable(select + sql + orderby, CommandType.Text);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// 获取实体集合
        /// SAM 2017年10月6日16:24:09
        /// </summary>
        /// <param name="typeIDs"></param>
        /// <returns></returns>
        public static IList<SYS_Parameters> GetList(string typeIDs)
        {
            string sql = string.Format(@"select *
            from [SYS_Parameters] 
            where [SystemID]='{1}' and [ParameterTypeID] in ('{0}') and [IsEnable]=1 
            order by Sequence asc ", typeIDs, Framework.SystemID);

            DataTable result = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            return ToList(result);
        }

        /// <summary>
        /// 获取制程的列表
        /// Sam 2017年10月30日14:35:29
        /// Code为单个起始区间查询，不包括本身
        /// Name为单个起始区间查询，不包括本身
        /// </summary>
        /// <param name="Code"></param>
        /// <param name="Name"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> SfcProcessList(string Code, string Name, int page, int rows, ref int count)
        {
            string select = string.Format(@"select A.ParameterID,A.Code,A.Name,A.Comments ");

            string sql = string.Format(@" from [SYS_Parameters] A 
            where A.[SystemID]='{0}' and [ParameterTypeID]='{0}{1}' and [IsEnable]=1", Framework.SystemID, "0191213000017");

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
                sql = sql + string.Format(@" and A.[Code] collate Chinese_PRC_CI_AS > @Code ");
                parameters[0].Value = Code.Trim();
                Parcount[0].Value = Code.Trim();
            }

            if (!string.IsNullOrWhiteSpace(Name))
            {
                Name = "%" + Name.Trim() + "%";
                sql = sql + string.Format(@" and A.[Name] collate Chinese_PRC_CI_AS like @Name ");
                parameters[1].Value = Name.Trim();
                Parcount[1].Value = Name.Trim();
            }

            count = UniversalService.getCount(sql, Parcount);

            string orderby = " A.[Code] ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, parameters, page, rows);

            return ToHashtableList(dt);
        }
    }
}
