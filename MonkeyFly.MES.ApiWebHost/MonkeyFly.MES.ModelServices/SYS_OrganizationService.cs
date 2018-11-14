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
    public class SYS_OrganizationService : SuperModel<SYS_Organization>
    {
        /// <summary>
        /// ���
        /// SAM 2017��4��26��15:10:07
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="Model"></param>
        /// <returns></returns>
        public static bool insert(string userId, SYS_Organization Model)
        {
            try
            {
                string sql = string.Format(@"insert[SYS_Organization]([OrganizationID],[ParentOrganizationID],[Name],[Code],[Type],[Status],[Sequence],[PlantID],[IfTop],
                [Comments],[Modifier],[ModifiedTime],[ModifiedLocalTime],[Creator],[CreateTime],[CreateLocalTime],[SystemID]) values
                 (@OrganizationID,@ParentOrganizationID,@Name,@Code,@Type,@Status,@Sequence,@PlantID,@IfTop,@Comments,'{0}','{1}','{2}','{0}','{1}','{2}','{3}')"
                , userId, DateTime.Now, DateTime.Now, Framework.SystemID);

                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@OrganizationID",SqlDbType.VarChar),
                    new SqlParameter("@ParentOrganizationID",SqlDbType.VarChar),
                    new SqlParameter("@Name",SqlDbType.VarChar),
                    new SqlParameter("@Code",SqlDbType.VarChar),
                    new SqlParameter("@Type",SqlDbType.VarChar),
                    new SqlParameter("@Status",SqlDbType.TinyInt),
                    new SqlParameter("@Sequence",SqlDbType.Int),
                    new SqlParameter("@Comments",SqlDbType.VarChar),
                    new SqlParameter("@PlantID",SqlDbType.VarChar),
                    new SqlParameter("@IfTop",SqlDbType.Bit)
                    };

                parameters[0].Value = (object)Model.OrganizationID ?? DBNull.Value;
                parameters[1].Value = (object)Model.ParentOrganizationID ?? DBNull.Value;
                parameters[2].Value = (object)Model.Name ?? DBNull.Value;
                parameters[3].Value = (object)Model.Code ?? DBNull.Value;
                parameters[4].Value = (object)Model.Type ?? DBNull.Value;
                parameters[5].Value = (object)Model.Status ?? DBNull.Value;
                parameters[6].Value = (object)Model.Sequence ?? DBNull.Value;
                parameters[7].Value = (object)Model.Comments ?? DBNull.Value;
                parameters[8].Value = (object)Model.PlantID ?? DBNull.Value;
                parameters[9].Value = (object)Model.IfTop ?? DBNull.Value;

                return SQLHelper.ExecuteNonQuery(sql, CommandType.Text, parameters) > 0;
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return false;
            }
        }

        /// <summary>
        /// ����
        /// SAM 2017��4��26��15:10:23
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="Model"></param>
        /// <returns></returns>
        public static bool update(string userId, SYS_Organization Model)
        {
            try
            {
                string sql = String.Format(@"update[SYS_Organization] set {0},
                [ParentOrganizationID]=@ParentOrganizationID,[Name]=@Name,[Code]=@Code,[Status]=@Status,[Sequence]=@Sequence,[Comments]=@Comments,
                [PlantID]=@PlantID,[IfTop]=@IfTop
                where [OrganizationID]=@OrganizationID", UniversalService.getUpdateUTC(userId));
                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@OrganizationID",SqlDbType.VarChar),
                    new SqlParameter("@ParentOrganizationID",SqlDbType.VarChar),
                    new SqlParameter("@Name",SqlDbType.VarChar),
                    new SqlParameter("@Code",SqlDbType.VarChar),
                    new SqlParameter("@Status",SqlDbType.TinyInt),
                    new SqlParameter("@Sequence",SqlDbType.Int),
                    new SqlParameter("@Comments",SqlDbType.VarChar),
                    new SqlParameter("@PlantID",SqlDbType.VarChar),
                    new SqlParameter("@IfTop",SqlDbType.Bit)
                    };

                parameters[0].Value = Model.OrganizationID;
                parameters[1].Value = (Object)Model.ParentOrganizationID ?? DBNull.Value;
                parameters[2].Value = (Object)Model.Name ?? DBNull.Value;
                parameters[3].Value = (Object)Model.Code ?? DBNull.Value;
                parameters[4].Value = (Object)Model.Status ?? DBNull.Value;
                parameters[5].Value = (Object)Model.Sequence ?? DBNull.Value;
                parameters[6].Value = (Object)Model.Comments ?? DBNull.Value;
                parameters[7].Value = (object)Model.PlantID ?? DBNull.Value;
                parameters[8].Value = (object)Model.IfTop ?? DBNull.Value;

                return SQLHelper.ExecuteNonQuery(sql, CommandType.Text, parameters) > 0;
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return false;
            }
        }

        /// <summary>
        /// ��ȡ�ֿⵯ���б�
        /// Tom 2017��6��29��01��23��
        /// </summary>
        /// <param name="code"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> GetWarehouseList(string Code, int page, int rows, ref int count)
        {
            string select = string.Format(@"select A.OrganizationID,A.Code,A.Name,A.ParentOrganizationID,A.Status,A.Comments,A.Sequence,
             B.UserName as Creator,A.CreateLocalTime as CreateTime,D.Code as PlantCode,D.Name as PlantName,
           (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else C.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ");

            string sql = string.Format(@"  from [SYS_Organization] A 
            left join [SYS_MESUsers] B on A.[Creator] = B.[MESUserID]
            left join [SYS_MESUsers] C on A.[Modifier] = C.[MESUserID]
            left join [SYS_Organization] D on A.[ParentOrganizationID] = D.[OrganizationID]
            where A.[SystemID]='{0}' and A.[Status] = 1 and A.[Type]='{0}020121300001F'  ", Framework.SystemID);

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

            String orderby = "A.[Code] asc";

            DataTable dt = UniversalService.getTable(select, sql, orderby, parameters, page, rows);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// ��ȡ��һʵ��
        /// SAM 2017��4��26��15:10:55
        /// </summary>
        /// <param name="OrganizationID"></param>
        /// <returns></returns>
        public static SYS_Organization get(string OrganizationID)
        {
            string sql = string.Format(@"select Top 1 * from [SYS_Organization] where [OrganizationID] = '{0}'  and [SystemID] = '{1}' ", OrganizationID, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return null;
            else
                return ToEntity(dt.Rows[0]);
        }

        /// <summary>
        /// �жϴ����Ƿ��ظ�
        /// SAM 2017��4��26��14:51:26
        /// �ظ�����true�����ظ�����false
        /// </summary>
        /// <param name="Code">����</param>
        /// <param name="OrganizationID"></param>
        /// <returns></returns>
        public static bool CheckCode(string Code, string type, string OrganizationID)
        {
            string sql = String.Format(@"select Top 1 * from [SYS_Organization] where [SystemID]='{0}' and [Status] <> 2  and [Type]= '{1}' ", Framework.SystemID, type);

            /*�ȶ���Code��Ĭ�ϸ�DbNull,��������Ҳ���*/
            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@Code",SqlDbType.VarChar),
            };
            parameters[0].Value = DBNull.Value;

            /*��ΪCode��ͨ���ֶ�����ģ�������Ҫ�ò�������ʽȥƴSQL*/
            if (!string.IsNullOrWhiteSpace(Code))
            {
                sql = sql + String.Format(@" and [Code] =@Code ");
                parameters[0].Value = Code;
            }

            /*MaterialStructureID��������ˮ�ţ������ڸ���ʱ���ų����Լ�*/
            if (!string.IsNullOrWhiteSpace(OrganizationID))
                sql = sql + String.Format(@" and [OrganizationID] <> '{0}' ", OrganizationID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text, parameters);

            if (dt.Rows.Count == 0)
                return false;
            else
                return true;
        }

        /// <summary>
        /// ���ݴ��Ż�ȡ����
        /// SAM 2017��4��26��16:40:14
        /// </summary>
        /// <param name="Code">����</param>
        /// <returns></returns>
        public static SYS_Organization getByCode(string Code, string Tpye)
        {
            string sql = string.Format(@"select Top 1 * from [SYS_Organization] where [Code] = '{0}'  and [SystemID] = '{1}'  and Status <> 2 and Type = '{2}'", Code, Framework.SystemID, Tpye);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return null;
            else
                return ToEntity(dt.Rows[0]);
        }

        /// <summary>
        /// ����ĵ���
        /// SAM 2017��5��4��11:36:29
        /// </summary>
        /// <param name="Code">����</param>
        /// <param name="page">ҳ��</param>
        /// <param name="rows">����</param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> GetPlantList(string Code, int page, int rows, ref int count)
        {
            string select = string.Format(@"select A.OrganizationID,A.Code,A.Name,A.Comments,A.Status,A.Type,
             A.Code+'-'+A.Name as NewName,A.PlantID,
             B.UserName as Creator,A.CreateLocalTime as CreateTime,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else C.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ");

            string sql = string.Format(@"  from [SYS_Organization] A 
            left join SYS_MESUsers B on A.Creator = B.MESUserID
            left join SYS_MESUsers C on A.Modifier = C.MESUserID
            where A.[SystemID]='{0}' and A.[Status]=1 and A.[Type]='{0}020121300001E' ", Framework.SystemID);

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

            string orderby = "A.[Status] desc,A.[Code] ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, parameters, page, rows);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// ���ŵĵ���
        /// SAM 2017��5��25��23:07:50
        /// </summary>
        /// <param name="Code">����</param>
        /// <param name="IsPlant"></param>
        /// <param name="page">ҳ��</param>
        /// <param name="rows">����</param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> GetDeptList(string Code, string Plant, int page, int rows, ref int count)
        {
            string select = string.Format(@"select A.OrganizationID,A.Code,A.Name,A.Comments,A.Status,A.IfTop,A.Type,
             A.Code+'-'+A.Name as NewName,A.PlantID,
             B.UserName as Creator,A.CreateLocalTime as CreateTime,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else C.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ");

            string sql = string.Format(@"  from [SYS_Organization] A 
            left join SYS_MESUsers B on A.Creator = B.MESUserID
            left join SYS_MESUsers C on A.Modifier = C.MESUserID
            where A.[SystemID]='{0}' and A.[Status]=1 and A.[Type]='{0}020121300001D' ", Framework.SystemID);

            if (!String.IsNullOrWhiteSpace(Plant))
                sql += string.Format(@" and A.[PlantID] = '{0}' ", Plant);

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

            string orderby = "A.[Code] ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, parameters, page, rows);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// ��ȡ������б�
        /// SAM 2017��4��26��15:20:03
        /// </summary>
        /// <param name="Code">����</param>
        /// <param name="page">ҳ��</param>
        /// <param name="rows">����</param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> Inf00001getPlantList(string Code, int page, int rows, ref int count)
        {
            string select = string.Format(@"select A.OrganizationID,A.Code,A.Name,A.Status,A.Comments,A.Sequence,
             B.UserName as Creator,A.CreateLocalTime as CreateTime,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else C.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ");

            string sql = string.Format(@"  from [SYS_Organization] A 
            left join SYS_MESUsers B on A.Creator = B.MESUserID
            left join SYS_MESUsers C on A.Modifier = C.MESUserID
            where A.[SystemID]='{0}' and A.[Status] <> 2 and A.[Type]='{0}020121300001E' ", Framework.SystemID);

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
                sql = sql + String.Format(@" and A.[Code] collate Chinese_PRC_CI_AS like @Code ");
                parameters[0].Value = Code;
                Parcount[0].Value = Code;
            }

            count = UniversalService.getCount(sql, Parcount);

            String orderby = "A.[Status] desc,A.[Code] ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, parameters, page, rows);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// ��ȡ����ĵ�����Ϣ
        /// SAM 2017��4��26��16:47:03
        /// </summary>
        /// <param name="organizationID"></param>
        /// <param name="yearMonth"></param>
        /// <returns></returns>
        public static DataTable GetExportList(string Code)
        {
            string select = string.Format(@"select A.Code,A.Name,A.Comments,
            (CASE WHEN A.Status=1 THEN '����' ELSE '����' END),
             B.UserName as Creator,A.CreateLocalTime as CreateTime,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else C.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ");

            string sql = string.Format(@"  from [SYS_Organization] A 
            left join SYS_MESUsers B on A.Creator = B.MESUserID
            left join SYS_MESUsers C on A.Modifier = C.MESUserID
            where A.[SystemID]='{0}' and A.[Status] <> 2 and A.[Type]='{0}020121300001E' ", Framework.SystemID);

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

            string orderBy = "order By A.[Status] desc,A.[Code] ";

            return SQLHelper.ExecuteDataTable(select + sql + orderBy, CommandType.Text, parameters);
        }

        /// <summary>
        /// ��ȡ�ֿ���б�
        /// SAM 2017��5��4��11:15:25
        /// </summary>
        /// <param name="Code">����</param>
        /// <param name="Status">״̬</param>
        /// <param name="page">ҳ��</param>
        /// <param name="rows">����</param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> Inf00012GetList(string Code, string Status, int page, int rows, ref int count)
        {
            string select = string.Format(@"select A.OrganizationID,A.Code,A.Name,A.ParentOrganizationID,A.Status,A.Comments,A.Sequence,
             B.UserName as Creator,A.CreateLocalTime as CreateTime,D.Code as PlantCode,D.Name as PlantName,
           (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else C.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ");

            string sql = string.Format(@"  from [SYS_Organization] A 
            left join [SYS_MESUsers] B on A.[Creator] = B.[MESUserID]
            left join [SYS_MESUsers] C on A.[Modifier] = C.[MESUserID]
            left join [SYS_Organization] D on A.[ParentOrganizationID] = D.[OrganizationID]
            where A.[SystemID]='{0}' and A.[Status] <> 2 and A.[Type]='{0}020121300001F'  ", Framework.SystemID);

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

            if (!string.IsNullOrWhiteSpace(Status))
                sql = sql + string.Format(@" and A.[Status] ={0} ", Status);

            count = UniversalService.getCount(sql, Parcount);

            String orderby = "A.[Status] desc,A.[Code] ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, parameters, page, rows);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// ��ȡ�ֿ�ĵ�����Ϣ
        /// SAM 2017��5��4��11:42:36
        /// </summary>
        /// <param name="Code">����</param>
        /// <param name="Status">״̬</param>
        /// <returns></returns>
        public static DataTable Inf00012GetExportList(string Code, string Status)
        {
            string select = string.Format(@"select ROW_NUMBER() OVER (ORDER BY A.[Status] desc,A.[Code]),A.Code,A.Name,D.Code as PlantCode,A.Comments,
            (CASE WHEN A.Status=1 THEN '����' ELSE '����' END),
             B.UserName as Creator,A.CreateLocalTime as CreateTime,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else C.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ");

            string sql = string.Format(@"  from [SYS_Organization] A 
            left join SYS_MESUsers B on A.Creator = B.MESUserID
            left join SYS_MESUsers C on A.Modifier = C.MESUserID
            left join [SYS_Organization] D on A.[ParentOrganizationID] = D.[OrganizationID]
            where A.[SystemID]='{0}' and A.[Status] <> 2 and A.[Type]='{0}020121300001F' ", Framework.SystemID);

            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@Code",SqlDbType.VarChar)
            };
            parameters[0].Value = DBNull.Value;

            if (!string.IsNullOrWhiteSpace(Code))
            {
                Code = "%" + Code + "%";
                sql = sql + string.Format(@" and A.[Code] like @Code ");
                parameters[0].Value = Code;
            }

            if (!string.IsNullOrWhiteSpace(Status))
                sql = sql + string.Format(@" and A.[Status] ={0} ", Status);

            string orderBy = "order By A.[Status] desc,A.[Code] ";

            return SQLHelper.ExecuteDataTable(select + sql + orderBy, CommandType.Text, parameters);
        }

        /// <summary>
        /// �жϳ����Ƿ��ڲ�����ʹ��
        /// SAM 2017��5��18��23:04:10
        /// </summary>
        /// <param name="OrganizationID"></param>
        /// <returns></returns>
        public static bool Inf00001Check(string OrganizationID)
        {
            string sql = string.Format(@"select * from [SYS_Organization] where [PlantID] = '{0}' and [SystemID] = '{1}' and [Status] <> 2 and [Type]='{1}020121300001D' ", OrganizationID, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return false;
            else
                return true;
        }

        /// <summary>
        /// �жϳ����Ƿ��ڲֿ���ʹ��
        /// SAM 2017��7��19��22:25:47
        /// </summary>
        /// <param name="OrganizationID"></param>
        /// <returns></returns>
        public static bool Inf00012Check(string OrganizationID)
        {
            string sql = string.Format(@"select * from [SYS_Organization] where [ParentOrganizationID] = '{0}' and [SystemID] = '{1}' and [Status] <> 2 and [Type]='{1}020121300001F' ", OrganizationID, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return false;
            else
                return true;
        }

        /// <summary>
        /// ���������б�
        /// SAM 2017��5��18��23:44:24
        /// </summary>
        /// <param name="Code">����</param>
        /// <param name="Status">״̬</param>
        /// <param name="page">ҳ��</param>
        /// <param name="rows">����</param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> Inf00005GetList(string Code, string Status, int page, int rows, ref int count)
        {
            string select = string.Format(@"select A.OrganizationID,A.Code,A.Name,A.ParentOrganizationID,A.Status,A.Comments,A.Sequence,A.PlantID,A.IfTop,
                D.Code as ParentCode,D.Name as ParentName,A.Type,
                (CASE WHEN B.Emplno is null or B.Emplno = '' THEN B.Account else B.Emplno END)+'-'+B.UserName as Creator,A.CreateLocalTime as CreateTime,
                (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else (CASE WHEN C.Emplno is null or C.Emplno = '' THEN C.Account else C.Emplno END)+'-'+C.UserName END) as Modifier,
                (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ");

            string sql = string.Format(@"  from [SYS_Organization] A 
            left join [SYS_MESUsers] B on A.[Creator] = B.[MESUserID]
            left join [SYS_MESUsers] C on A.[Modifier] = C.[MESUserID]
            left join [SYS_Organization] D on A.[ParentOrganizationID] = D.[OrganizationID]
            where A.[SystemID]='{0}' and A.[Status] <> 2 and A.[Type] = '{0}020121300001D' ", Framework.SystemID);

            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@Code",SqlDbType.VarChar),
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

            if (!string.IsNullOrWhiteSpace(Status))
                sql = sql + string.Format(@" and A.[Status] ={0} ", Status);

            count = UniversalService.getCount(sql, Parcount);

            String orderby = "A.[Status] desc,A.[Code] ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, parameters, page, rows);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// ���ŵĵ���
        /// </summary>
        /// <param name="Code">����</param>
        /// <param name="Status">״̬</param>
        /// <returns></returns>
        public static DataTable Inf00005Export(string Code, string Status)
        {
            string select = string.Format(@"select ROW_NUMBER() OVER (ORDER BY A.[Status] desc,A.[Code]),
            A.Code,A.Name,A.Comments,F.Code+'-'+F.Name as Plant,(CASE WHEN A.IfTop=1 THEN 'Y' ELSE 'N' END),D.Code as ParentCode,
           (CASE WHEN A.Status=1 THEN '����' ELSE '����' END),
           (CASE WHEN B.Emplno is null or B.Emplno = '' THEN B.Account else B.Emplno END)+'-'+B.UserName as Creator,A.CreateLocalTime as CreateTime,
           (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else (CASE WHEN C.Emplno is null or C.Emplno = '' THEN C.Account else C.Emplno END)+'-'+C.UserName END) as Modifier,
           (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ");

            string sql = string.Format(@"  from [SYS_Organization] A 
            left join [SYS_MESUsers] B on A.[Creator] = B.[MESUserID]
            left join [SYS_MESUsers] C on A.[Modifier] = C.[MESUserID]
            left join [SYS_Organization] D on A.[ParentOrganizationID] = D.[OrganizationID]
            left join [SYS_Parameters] E on A.[Type] = E.[ParameterID]
            left join [SYS_Organization] F on A.[PlantID] = F.[OrganizationID]
            where A.[SystemID]='{0}' and A.[Status] <> 2 and A.[Type] = '{0}020121300001D'", Framework.SystemID);

            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@Code",SqlDbType.VarChar),
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

            if (!string.IsNullOrWhiteSpace(Status))
                sql = sql + string.Format(@" and A.[Status] ={0} ", Status);

            string orderBy = "order By A.[Status] desc,A.[Code] ";

            return SQLHelper.ExecuteDataTable(select + sql + orderBy, CommandType.Text, parameters);
        }

        /// <summary>
        /// �ж�ָ�������Ƿ��Ѿ��������������ϼ�����
        /// SAM 2017��5��19��09:31:18
        /// </summary>
        /// <param name="OrganizationID"></param>
        /// <returns></returns>
        public static bool CheckIfTop(string PlantID, string OrganizationID)
        {
            string sql = string.Format(@"select * from [SYS_Organization] where [IfTop]=1 and [PlantID] = '{0}' and [SystemID] = '{1}' and [Status] <> 2 and [Type]='{1}020121300001D' and [OrganizationID] <> '{2}'", PlantID, Framework.SystemID, OrganizationID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return false;
            else
                return true;
        }

        /// <summary>
        /// �жϲ����Ƿ��Ѿ����������ŵ��ϲ㲿��
        /// SAM 2017��5��19��10:22:43
        /// </summary>
        /// <param name="OrganizationID"></param>
        /// <returns></returns>
        public static bool CheckParent(string OrganizationID)
        {
            string sql = string.Format(@"select * from [SYS_Organization] where [ParentOrganizationID]='{0}' and [OrganizationID] <> '{0}' and [SystemID] = '{1}' and [Status] <> 2 and [Type]='{1}020121300001D' ", OrganizationID, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return false;
            else
                return true;
        }

        /// <summary>
        /// �жϲ����Ƿ�ӳ�����û�
        /// SAM 2017��5��19��10:25:29
        /// </summary>
        /// <param name="OrganizationID"></param>
        /// <returns></returns>
        public static bool CheckUserMapping(string OrganizationID)
        {
            string sql = string.Format(@"select * from [SYS_UserOrganizationMapping] where  [OrganizationID] = '{0}' and [SystemID] = '{1}' ", OrganizationID, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return false;
            else
                return true;
        }

        /// <summary>
        /// �жϲ����Ƿ�ӳ���˽�ɫ
        /// SAM 2017��5��19��10:27:18
        /// </summary>
        /// <param name="OrganizationID"></param>
        /// <returns></returns>
        public static bool CheckRoleMapping(string OrganizationID)
        {
            string sql = string.Format(@"select * from [SYS_OrganizationRoleMapping] where [OrganizationID] = '{0}' and [SystemID] = '{1}' ", OrganizationID, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return false;
            else
                return true;
        }

        /// <summary>
        /// ���ݳ����ȡ���������������Ĳ���
        /// SAM 2017��5��19��10:53:55
        /// </summary>
        /// <param name="plantID"></param>
        /// <returns></returns>
        public static IList<Hashtable> Inf00005GetDeptList(string PlantCode)
        {
            string select = string.Format(@"select A.OrganizationID,A.Code,A.Name,A.Code+'-'+A.Name as NewName,
            D.Code as ParentCode,D.Name as ParentName,
            (CASE WHEN  A.OrganizationID=A.ParentOrganizationID THEN '0' ELSE A.ParentOrganizationID END) as ParentOrganizationID ");

            string sql = string.Format(@"  from [SYS_Organization] A 
            left join [SYS_Organization] D on A.[PlantID] = D.[OrganizationID]
            where A.[SystemID]='{0}' and A.[Status] =1 and A.[Type]='{0}020121300001D' and D.[Code]='{1}'", Framework.SystemID, PlantCode);

            DataTable dt = SQLHelper.ExecuteDataTable(select + sql, CommandType.Text, null);

            return ToHashtableList(dt);
        }


        /// <summary>
        /// �����û���ȡ������֯�ṹ���
        /// SAM 2017��5��25��16:45:56
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public static string getUserOrg(string userID)
        {
            string sql = string.Format(@"select Top 1 * from [SYS_UserOrganizationMapping] where [UserID] = '{0}' and [SystemID] = '{1}' ", userID, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return null;
            else
                return ToEntity(dt.Rows[0]).OrganizationID;
        }

        /// <summary>
        /// ���������Ż�ȡ����������������
        /// SAM 2017��6��1��16:52:041
        /// </summary>
        /// <param name="groupID"></param>
        /// <param name="Code">����</param>
        /// <returns></returns>
        public static object Inf00016GetNotAuthorityList(string ClassID, string Code)
        {
            string select = string.Format(@"select null as DocumentAuthorityID,A.OrganizationID as AuthorityID,A.Code,A.Name ");

            string sql = string.Format(@"  from [SYS_Organization] A 
            where [OrganizationID] not in (select [AuthorityID] from [SYS_DocumentAuthority] where [ClassID] ='{1}' and [Status] = '{0}0201213000001' and [Attribute]=0)
            and A.[SystemID]='{0}' and A.[Status] =1 ", Framework.SystemID, ClassID);

            if (!string.IsNullOrWhiteSpace(Code))
                sql += string.Format(@"and A.Code like '%{0}%' ", Code);

            string orderBy = "order By A.[Code] ";

            DataTable dt = SQLHelper.ExecuteDataTable(select + sql + orderBy, CommandType.Text, null);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// �˺Ź���-��ȡ�����г���Ĳ���
        /// SAM 2017��7��4��11:15:34
        /// </summary>
        /// <returns></returns>
        public static IList<Hashtable> Inf00003GetOrganization()
        {
            string select = string.Format(@"select A.OrganizationID,A.Code,A.Name,A.Code+'-'+A.Name as NewName ");

            string sql = string.Format(@"  from [SYS_Organization] A 
            where A.[SystemID]='{0}' and A.[Status] =1 and A.[Type]='{0}020121300001D' and A.[PlantID] is not null ", Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(select + sql, CommandType.Text);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// ��ȡ���е������Ĳ��ŵ�ʵ�弯
        /// Sam 2017��10��17��14:58:32
        /// </summary>
        /// <returns></returns>
        public static IList<SYS_Organization> GetList()
        {
            string sql = string.Format(@"select * from [SYS_Organization] where [SystemID] = '{0}' and [Type]='{0}020121300001D'  and [Status]=1 ", Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return null;
            else
                return ToList(dt);
        }
    }
}

