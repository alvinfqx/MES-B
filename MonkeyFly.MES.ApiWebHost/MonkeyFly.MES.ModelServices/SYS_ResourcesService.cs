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
    public class SYS_ResourcesService : SuperModel<SYS_Resources>
    {
        /// <summary>
        /// 新增
        /// SAM 2017年5月12日11:10:15
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="Model"></param>
        /// <returns></returns>
        public static bool insert(string userId, SYS_Resources Model)
        {
            try
            {
                string sql = string.Format(@"insert[SYS_Resources]([ResourceID],[Code],[Description],[ClassID],[GroupID],[Quantity],[Status],
                [Comments],[Modifier],[ModifiedTime],[ModifiedLocalTime],[Creator],[CreateTime],[CreateLocalTime],[SystemID]) values
                (@ResourceID,@Code,@Description,@ClassID,@GroupID,@Quantity,@Status,@Comments,'{0}','{1}','{2}','{0}','{1}','{2}','{3}')", 
                userId, DateTime.Now, DateTime.Now, Framework.SystemID);

                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@ResourceID",SqlDbType.VarChar),
                    new SqlParameter("@Code",SqlDbType.VarChar),
                    new SqlParameter("@Description",SqlDbType.VarChar),
                    new SqlParameter("@ClassID",SqlDbType.VarChar),
                    new SqlParameter("@GroupID",SqlDbType.VarChar),
                    new SqlParameter("@Quantity",SqlDbType.Decimal),
                    new SqlParameter("@Status",SqlDbType.VarChar),
                    new SqlParameter("@Comments",SqlDbType.VarChar),
                    };

                parameters[0].Value = (Object)Model.ResourceID ?? DBNull.Value;
                parameters[1].Value = (Object)Model.Code ?? DBNull.Value;
                parameters[2].Value = (Object)Model.Description ?? DBNull.Value;
                parameters[3].Value = (Object)Model.ClassID ?? DBNull.Value;
                parameters[4].Value = (Object)Model.GroupID ?? DBNull.Value;
                parameters[5].Value = (Object)Model.Quantity ?? DBNull.Value;
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
        /// 更新
        /// SAM 2017年5月12日11:10:36
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="Model"></param>
        /// <returns></returns>
        public static bool update(string userId, SYS_Resources Model)
        {
            try
            {
                string sql = String.Format(@"update[SYS_Resources] set {0},
                [Description]=@Description,[ClassID]=@ClassID,[GroupID]=@GroupID,
                [Quantity]=@Quantity,[Status]=@Status,[Comments]=@Comments 
                where [ResourceID]=@ResourceID", UniversalService.getUpdateUTC(userId));

                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@ResourceID",SqlDbType.VarChar),
                    new SqlParameter("@Description",SqlDbType.VarChar),
                    new SqlParameter("@ClassID",SqlDbType.VarChar),
                    new SqlParameter("@GroupID",SqlDbType.VarChar),
                    new SqlParameter("@Quantity",SqlDbType.Decimal),
                    new SqlParameter("@Status",SqlDbType.VarChar),
                    new SqlParameter("@Comments",SqlDbType.VarChar),
                    };

                parameters[0].Value = Model.ResourceID;
                parameters[1].Value = (Object)Model.Description ?? DBNull.Value;
                parameters[2].Value = (Object)Model.ClassID ?? DBNull.Value;
                parameters[3].Value = (Object)Model.GroupID ?? DBNull.Value;
                parameters[4].Value = (Object)Model.Quantity ?? DBNull.Value;
                parameters[5].Value = (Object)Model.Status ?? DBNull.Value;
                parameters[6].Value = (Object)Model.Comments ?? DBNull.Value;

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
        /// SAM 2017年5月12日11:10:50
        /// </summary>
        /// <param name="ResourceID"></param>
        /// <returns></returns>
        public static SYS_Resources get(string ResourceID)
        {
            string sql = string.Format(@"select Top 1 * from [SYS_Resources] where [ResourceID] = '{0}'  and [SystemID] = '{1}' ", ResourceID, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return null;
            else
                return ToEntity(dt.Rows[0]);
        }
        

        /// <summary>
        /// 判断资源类别是否被使用着
        /// SAM 2017年5月12日11:39:37
        /// </summary>
        /// <param name="ClassID"></param>
        /// <returns></returns>
        public static bool CheckClass(string ClassID)
        {
            string sql = string.Format(@"select * from [SYS_Resources] where [ClassID] = '{0}' and [SystemID] = '{1}' and [Status] <> '{1}0201213000003'", ClassID, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return false;
            else
                return true;
        }

        /// <summary>
        /// 判断资源群组码是否被使用着
        /// SAM 2017年5月12日11:40:37
        /// </summary>
        /// <param name="GroupID"></param>
        /// <returns></returns>
        public static bool CheckGroup(string GroupID)
        {
            string sql = string.Format(@"select * from [SYS_Resources] where [GroupID] = '{0}' and [SystemID] = '{1}' and [Status] <> '{1}0201213000003'", GroupID, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return false;
            else
                return true;
        }

        /// <summary>
        /// 判断代号是否重复
        /// SAM 2017年5月12日11:45:50
        /// </summary>
        /// <param name="Code">代号</param>
        /// <param name="ResourceID"></param>
        /// <returns></returns>
        public static bool CheckCode(string Code, string ResourceID)
        {
            string sql = String.Format(@"select Top 1 * from [SYS_Resources] where [SystemID]='{0}' and Status <> '{0}0201213000003'", Framework.SystemID);

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

            /*ResourceID（流水号）用于在更新时，排除他自己*/
            if (!string.IsNullOrWhiteSpace(ResourceID))
                sql = sql + String.Format(@" and [ResourceID] <> '{0}' ", ResourceID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text, parameters);

            if (dt.Rows.Count == 0)
                return false;
            else
                return true;
        }


        /// <summary>
        /// 资源列表
        /// SAM 2017年5月12日11:30:01
        /// </summary>
        /// <param name="Code">代号</param>
        /// <param name="Status">状态</param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> Inf00015GetList(string Code, string status, int page, int rows, ref int count)
        {
            string select = string.Format(@"select A.ResourceID,A.Code,A.Description,A.ClassID,A.GroupID,
            A.Quantity,A.Status,A.Comments,B.Code as ClassCode,C.Code as GroupCode,
            (CASE WHEN D.Emplno is null or D.Emplno = '' THEN D.Account else D.Emplno END)+'-'+D.UserName as Creator,A.CreateLocalTime as CreateTime,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else (CASE WHEN E.Emplno is null or E.Emplno = '' THEN E.Account else E.Emplno END)+'-'+E.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ");

            string sql = string.Format(@" from [SYS_Resources] A 
            left join [SYS_Parameters] B on A.ClassID = B.ParameterID
            left join [SYS_Parameters] C on A.GroupID = C.ParameterID
            left join [SYS_MESUsers] D on A.Creator = D.MESUserID
            left join [SYS_MESUsers] E on A.Modifier = E.MESUserID
            where A.[SystemID]='{0}' and A.Status <> '{0}0201213000003' ", Framework.SystemID);

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
                sql = sql + String.Format(@" and A.[Status] = @Status ");
                parameters[1].Value = status;
                Parcount[1].Value = status;
            }

            count = UniversalService.getCount(sql, Parcount);

            string orderby = "A.[Status],A.[Code] ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, parameters, page, rows);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// 资源导出
        /// SAM 2017年5月12日14:42:11
        /// </summary>
        /// <param name="Code">代号</param>
        /// <param name="Status">状态</param>
        /// <returns></returns>
        public static DataTable Inf00015Export(string Code, string status)
        {
            string select = string.Format(@"select ROW_NUMBER() OVER (ORDER BY A.[Status],A.[Code]),B.Code+'-'+B.Name as ClassCode,
            A.Code,A.Description,A.Quantity,C.Code+'-'+C.Name as GroupCode,A.Comments,F.Name as Status,
            (CASE WHEN D.Emplno is null or D.Emplno = '' THEN D.Account else D.Emplno END)+'-'+D.UserName as Creator,A.CreateLocalTime as CreateTime,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else (CASE WHEN E.Emplno is null or E.Emplno = '' THEN E.Account else E.Emplno END)+'-'+E.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ");

            string sql = string.Format(@" from [SYS_Resources] A 
            left join [SYS_Parameters] B on A.ClassID = B.ParameterID
            left join [SYS_Parameters] C on A.GroupID = C.ParameterID
            left join [SYS_MESUsers] D on A.Creator = D.MESUserID
            left join [SYS_MESUsers] E on A.Modifier = E.MESUserID
            left join [SYS_Parameters] F on A.Status = F.ParameterID
            where A.[SystemID]='{0}' and A.Status <> '{0}0201213000003' ", Framework.SystemID);

            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@Code",SqlDbType.VarChar),
                new SqlParameter("@Status",SqlDbType.VarChar)
            };
            parameters[0].Value = DBNull.Value;
            parameters[1].Value = DBNull.Value;

            if (!string.IsNullOrWhiteSpace(Code))
            {
                Code = "%" + Code.Trim() + "%";
                sql = sql + string.Format(@" and A.[Code] collate Chinese_PRC_CI_AS like @Code ");
                parameters[0].Value = Code;
            }

            if (!string.IsNullOrWhiteSpace(status))
            {
                sql = sql + String.Format(@" and A.[Status] = @Status ");
                parameters[1].Value = status;
            }

            string orderBy = "order By A.[Status],A.[Code] ";

            return SQLHelper.ExecuteDataTable(select + sql + orderBy, CommandType.Text, parameters);
        }



        /// <summary>
        /// 获取资源的弹窗
        /// SAM 2017年5月27日16:15:24
        /// </summary>
        /// <param name="Code">代号</param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> GetResourceList(string Code, int page, int rows, ref int count)
        {
            string select = string.Format(@"select A.ResourceID,A.Code,A.Description,
            A.Quantity,F.Name as Status,A.Comments,B.Code+'-'+B.Name as Class,C.Code+'-'+C.Name  as [Group],
            D.UserName as Creator,A.CreateLocalTime as CreateTime,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else E.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ");
             
            string sql = string.Format(@" from [SYS_Resources] A 
            left join [SYS_Parameters] B on A.ClassID = B.ParameterID
            left join [SYS_Parameters] C on A.GroupID = C.ParameterID
            left join [SYS_MESUsers] D on A.Creator = D.MESUserID
            left join [SYS_MESUsers] E on A.Modifier = E.MESUserID
            left join [SYS_Parameters] F on A.Status = F.ParameterID
            where A.[SystemID]='{0}' and A.Status = '{0}0201213000001' ", Framework.SystemID);

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
        /// 根据资源类别获取资源
        /// Tom 2017年7月24日17:30:06
        /// </summary>
        /// <param name="classID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> SfcGetResourceByClass(string classID, int page, int rows, ref int count)
        {
            string select = string.Format(@"select A.ResourceID,A.Code,A.Description,
            A.Quantity,F.Name as Status,A.Comments,B.Code+'-'+B.Name as Class,C.Code+'-'+C.Name  as [Group],
            D.UserName as Creator,A.CreateLocalTime as CreateTime,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else E.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ");

            string sql = string.Format(@" from [SYS_Resources] A 
            left join [SYS_Parameters] B on A.ClassID = B.ParameterID
            left join [SYS_Parameters] C on A.GroupID = C.ParameterID
            left join [SYS_MESUsers] D on A.Creator = D.MESUserID
            left join [SYS_MESUsers] E on A.Modifier = E.MESUserID
            left join [SYS_Parameters] F on A.Status = F.ParameterID
            where A.[SystemID]='{0}' and A.Status = '{0}0201213000001' ", Framework.SystemID);

            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@ClassID",SqlDbType.VarChar)
            };
            parameters[0].Value = DBNull.Value;

            SqlParameter[] Parcount = new SqlParameter[]{
                new SqlParameter("@ClassID",SqlDbType.VarChar)
            };
            Parcount[0].Value = DBNull.Value;

            if (!string.IsNullOrWhiteSpace(classID))
            {
                sql = sql + String.Format(@" and A.[ClassID] = @ClassID ");
                parameters[0].Value = classID;
                Parcount[0].Value = classID;
            }

            count = UniversalService.getCount(sql, Parcount);

            string orderby = "A.[Code] ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, parameters, page, rows);

            return ToHashtableList(dt);
        }
   
        /// <summary>
        /// 制品制程的资源群组专属开窗
        /// SAM 2017年6月21日10:01:58
        /// </summary>
        /// <param name="WorkCenterID"></param>
        /// <param name="GroupCode"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> Sfc00001GetGroupList(string WorkCenterID, string GroupCode,string Type, int page, int rows, ref int count)
        {
            string select = string.Format(@"select A.ResourceID,A.Code,A.Description,A.ClassID,A.GroupID,
            A.Quantity,A.Status,A.Comments,B.Code as ClassCode,C.Code as GroupCode,C.Name as GroupName,
            D.UserName as Creator,A.CreateLocalTime as CreateTime,B.Name as ClassName,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else E.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ");

            string sql = string.Format(@" from [SYS_Resources] A 
            left join [SYS_Parameters] B on A.ClassID = B.ParameterID
            left join [SYS_Parameters] C on A.GroupID = C.ParameterID
            left join [SYS_MESUsers] D on A.Creator = D.MESUserID
            left join [SYS_MESUsers] E on A.Modifier = E.MESUserID
            where A.[SystemID]='{0}' and A.Status = '{0}0201213000001' ", Framework.SystemID);

            sql += string.Format("and A.[ResourceID] in (Select [ResourceID] From [SYS_WorkCenterResources] where [WorkCenterID]='{0}' and [Status] ='{1}0201213000001' ) ", WorkCenterID, Framework.SystemID);

            if(string.IsNullOrWhiteSpace(Type))
                sql+= string.Format("and B.[Code] <> 'M' and B.[Code] <> 'L' ");
            else if (Type=="M")
                sql += string.Format("and B.[Code] = 'M' ");
            else if (Type == "L")
                sql += string.Format("and B.[Code] = 'L' ");

            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@GroupCode",SqlDbType.VarChar)
            };
            parameters[0].Value = DBNull.Value;

            SqlParameter[] Parcount = new SqlParameter[]{
                new SqlParameter("@GroupCode",SqlDbType.VarChar)
            };
            Parcount[0].Value = DBNull.Value;

            if (!string.IsNullOrWhiteSpace(GroupCode))
            {
                GroupCode = "%" + GroupCode + "%";
                sql = sql + string.Format(@" and C.[Code] like @GroupCode ");
                parameters[0].Value = GroupCode;
                Parcount[0].Value = GroupCode;
            }

            count = UniversalService.getCount(sql, Parcount);

            string orderby = "A.[Code] ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, parameters, page, rows);

            return ToHashtableList(dt);
        }


        /// <summary>
        /// 根据指令制程流水号或者指令制程工序流水号获取不存在的资源(不分页)
        /// SAM 2017年7月15日13:18:37
        /// </summary>
        /// <param name="FabMoProcessID"></param>
        /// <param name="FabMoOperationID"></param>
        /// <returns></returns>
        public static IList<Hashtable> Sfc00002GetNoResourceList(string WorkCenterID, string FabMoProcessID, string FabMoOperationID, int page, int rows, ref int count)
        {
            string select = string.Format(
                @"select A.ResourceID,A.Code,A.Description as Name,A.Quantity,
                (select [Name] from [SYS_Parameters] where A.ClassID = ParameterID) as ClassName,
                (select [Code] from [SYS_Parameters] where A.ClassID = ParameterID) as ClassCode");
            string sql = string.Format(@"from [SYS_Resources] A
                  where A.[SystemID] = '{0}' and A.[Status] = '{0}0201213000001' ", Framework.SystemID);


            string orderby = "A.[Code]";

            sql += string.Format("and A.[ResourceID] in (Select [ResourceID] From [SYS_WorkCenterResources] where [WorkCenterID]='{0}' and [Status] ='{1}0201213000001' ) ", WorkCenterID, Framework.SystemID);

            if (!string.IsNullOrWhiteSpace(FabMoProcessID))
                sql += string.Format(@" and A.[ResourceID] not in (Select [ResourceID] From [SFC_FabMoResource] where [FabMoProcessID] = '{0}' and [SystemID]= '{1}' and [Status] ='{1}0201213000001')", FabMoProcessID, Framework.SystemID);

            if (!string.IsNullOrWhiteSpace(FabMoOperationID))
                sql += string.Format(@" and A.[ResourceID] not in (Select [ResourceID] From [SFC_FabMoResource] where [FabMoOperationID] = '{0}' and [SystemID]= '{1}' and [Status] ='{1}0201213000001')", FabMoOperationID, Framework.SystemID);


            count = UniversalService.getCount(sql, null);

            DataTable dt = UniversalService.getTable(select, sql, orderby, null, page, rows);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// 根据制程流水号或者制程工序流水号获取不存在的资源(不分页)
        /// SAM 2017年7月27日12:13:27
        /// </summary>
        /// <param name="WorkCenterID"></param>
        /// <param name="ItemProcessID"></param>
        /// <param name="ItemOperationID"></param>
        /// <returns></returns>
        public static IList<Hashtable> Sfc00001GetNoResourceList(string WorkCenterID, string ItemProcessID, string ItemOperationID, int page, int rows, ref int count)
        {
            string select = string.Format(
                @"select A.ResourceID,A.Code,A.Description as Name,A.Quantity,
                (select [Name] from [SYS_Parameters] where A.ClassID = ParameterID) as ClassName,
                (select [Code] from [SYS_Parameters] where A.ClassID = ParameterID) as ClassCode   ");
            string sql = string.Format(@"from [SYS_Resources] A
                  where A.[SystemID] = '{0}' and A.[Status] = '{0}0201213000001' ", Framework.SystemID);

            string orderby = "A.[Code]";

            sql += string.Format("and A.[ResourceID] in (Select [ResourceID] From [SYS_WorkCenterResources] where [WorkCenterID]='{0}' and [Status] ='{1}0201213000001' ) ", WorkCenterID, Framework.SystemID);

            if (!string.IsNullOrWhiteSpace(ItemProcessID))
                sql += string.Format(@" and A.[ResourceID] not in (Select [ResourceID] From [SFC_ItemResource] where [ItemProcessID] = '{0}' and [SystemID]= '{1}' and [Status] ='{1}0201213000001')", ItemProcessID, Framework.SystemID);

            if (!string.IsNullOrWhiteSpace(ItemOperationID))
                sql += string.Format(@" and A.[ResourceID] not in (Select [ResourceID] From [SFC_ItemResource] where [ItemOperationID] = '{0}' and [SystemID]= '{1}' and [Status] ='{1}0201213000001')", ItemOperationID, Framework.SystemID);

            count = UniversalService.getCount(sql, null);

            DataTable dt = UniversalService.getTable(select, sql, orderby, null, page, rows);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// 根据工作中心获取不属于它的资源列表（不分页）
        /// Joint 2017年7月31日15:15:04
        /// </summary>
        /// <param name="WorkCenterID"></param>
        /// <returns></returns>
        public static IList<Hashtable> Inf00018GetNoWorkCenterResourceList(string WorkCenterID)
        {
            string sql = string.Format(
                @"select null as WorkCenterResourcesID,A.ResourceID,A.Code,A.Description as Name,A.Quantity,A.Status,
                (select [Name] from [SYS_Parameters] where A.ClassID = ParameterID) as ClassName,
                (select [Code] from [SYS_Parameters] where A.ClassID = ParameterID) as ClassCode,0 as IfMain      
                from [SYS_Resources] A
                  where A.[SystemID] = '{0}' and A.[Status] = '{0}0201213000001' ", Framework.SystemID);

            string orderby = " order by A.[Code]";

            sql += string.Format("and A.[ResourceID] not in (Select [ResourceID] From [SYS_WorkCenterResources] where [WorkCenterID]='{0}' and [Status] ='{1}0201213000001' ) ", WorkCenterID, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql + orderby, CommandType.Text);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// 根据代号获取资源
        /// SAM 2017年9月5日14:27:22
        /// </summary>
        /// <param name="Code"></param>
        /// <returns></returns>
        public static SYS_Resources getByCode(string Code)
        {
            string sql = string.Format(@"select Top 1 * from [SYS_Resources] where [Code] = '{0}'  and [SystemID] = '{1}' and A.[Status] = '{1}0201213000001' ", Code, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return null;
            else
                return ToEntity(dt.Rows[0]);
        }

        /// <summary>
        /// 获取所有正常的资源实体集合
        /// SAM 2017年10月26日15:26:22
        /// </summary>
        /// <returns></returns>
        public static IList<SYS_Resources> GetList()
        {
            string sql = string.Format(
                @"select * from [SYS_Resources]       
                  where [SystemID] = '{0}' and [Status] = '{0}0201213000001' ", Framework.SystemID);

            string orderby = " order by [Code]";

            DataTable dt = SQLHelper.ExecuteDataTable(sql + orderby, CommandType.Text);

            return ToList(dt);
        }
    }
}

