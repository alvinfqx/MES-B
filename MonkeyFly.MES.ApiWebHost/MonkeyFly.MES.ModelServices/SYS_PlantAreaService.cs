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
    public class SYS_PlantAreaService : SuperModel<SYS_PlantArea>
    {
        /// <summary>
        /// ����
        /// SAM 2017��4��26��14:36:03
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="Model"></param>
        /// <returns></returns>
        public static bool insert(string userId, SYS_PlantArea Model)
        {
            try
            {
                string sql = string.Format(@"insert[SYS_PlantArea]([PlantAreaID],[Code],[Name],[PlantID],[Status],
                [Comments],[Modifier],[ModifiedTime],[ModifiedLocalTime],[Creator],[CreateTime],[CreateLocalTime],[SystemID]) values
                 (@PlantAreaID,@Code,@Name,@PlantID,@Status,@Comments,'{0}','{1}','{2}','{0}','{1}','{2}','{3}')", 
                 userId, DateTime.Now, DateTime.Now, Framework.SystemID);

                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@PlantAreaID",SqlDbType.VarChar),
                    new SqlParameter("@Code",SqlDbType.VarChar),
                    new SqlParameter("@Name",SqlDbType.VarChar),
                    new SqlParameter("@PlantID",SqlDbType.VarChar),
                    new SqlParameter("@Status",SqlDbType.VarChar),
                    new SqlParameter("@Comments",SqlDbType.VarChar),
                    };

                parameters[0].Value = (Object)Model.PlantAreaID ?? DBNull.Value;
                parameters[1].Value = (Object)Model.Code ?? DBNull.Value;
                parameters[2].Value = (Object)Model.Name ?? DBNull.Value;
                parameters[3].Value = (Object)Model.PlantID ?? DBNull.Value;
                parameters[4].Value = (Object)Model.Status ?? DBNull.Value;
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
        /// ����
        /// SAM 2017��4��26��14:36:17
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="Model"></param>
        /// <returns></returns>
        public static bool update(string userId, SYS_PlantArea Model)
        {
            try
            {
                string sql = String.Format(@"update[SYS_PlantArea] set {0},
                [Name]=@Name,[PlantID]=@PlantID,[Status]=@Status,[Comments]=@Comments 
                where [PlantAreaID]=@PlantAreaID", UniversalService.getUpdateUTC(userId));

                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@PlantAreaID",SqlDbType.VarChar),
                    new SqlParameter("@Name",SqlDbType.VarChar),
                    new SqlParameter("@PlantID",SqlDbType.VarChar),
                    new SqlParameter("@Status",SqlDbType.VarChar),
                    new SqlParameter("@Comments",SqlDbType.VarChar),
                    };

                parameters[0].Value = Model.PlantAreaID;
                parameters[1].Value = (Object)Model.Name ?? DBNull.Value;
                parameters[2].Value = (Object)Model.PlantID ?? DBNull.Value;
                parameters[3].Value = (Object)Model.Status ?? DBNull.Value;
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
        /// ��ȡ��һʵ��
        /// SAM 2017��4��26��14:36:27
        /// </summary>
        /// <param name="PlantAreaID"></param>
        /// <returns></returns>
        public static SYS_PlantArea get(string PlantAreaID)
        {
            string sql = string.Format(@"select Top 1 * from [SYS_PlantArea] where [PlantAreaID] = '{0}'  and [SystemID] = '{1}' ", PlantAreaID, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return null;
            else
                return ToEntity(dt.Rows[0]);
        }

        /// <summary>
        /// ɾ��
        /// SAM 2017��4��26��14:36:35
        /// </summary>
        /// <param name="PlantAreaID"></param>
        /// <returns></returns>
        public static bool delete(string PlantAreaID)
        {
            try
            {
                string sql = string.Format(@"delete from [SYS_PlantArea] where [PlantAreaID] = '{0}'  and [SystemID] = '{1}' ", PlantAreaID, Framework.SystemID);

                return SQLHelper.ExecuteNonQuery(sql, CommandType.Text) > 0;
            }

            catch (Exception ex)
            {

                DataLogerService.writeerrlog(ex);
                return false;
            }
        }

        /// <summary>
        /// �жϴ����Ƿ��ظ�
        /// SAM 2017��4��26��14:51:26
        /// �ظ�����true�����ظ�����false
        /// </summary>
        /// <param name="Code">����</param>
        /// <param name="PlantAreaID"></param>
        /// <returns></returns>
        public static bool CheckCode(string Code, string PlantAreaID)
        {
            string sql = String.Format(@"select Top 1 * from [SYS_PlantArea] where [SystemID]='{0}' and [Status] <> '{0}0201213000003' ", Framework.SystemID);

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
            if (!string.IsNullOrWhiteSpace(PlantAreaID))
                sql = sql + String.Format(@" and [PlantAreaID] <> '{0}' ", PlantAreaID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text, parameters);

            if (dt.Rows.Count == 0)
                return false;
            else
                return true;
        }


        /// <summary>
        /// ��ȡ�����б�
        /// SAM 2017��4��26��15:37:35
        /// </summary>
        /// <param name="Code">����</param>
        /// <param name="page">ҳ��</param>
        /// <param name="rows">����</param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> Inf00001getPlantAreaList(string Code, int page, int rows, ref int count)
        {
            string select = string.Format(@"select A.PlantAreaID,A.Code,A.Name,A.Status,A.Comments,
            B.Code as PlantCode,B.Name as PlantName,A.PlantID,
            D.UserName as Creator,A.CreateLocalTime as CreateTime,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else E.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ");

            string sql = string.Format(@"  from [SYS_PlantArea] A 
            left join [SYS_Organization] B on A.PlantID= B.OrganizationID
            left join [SYS_MESUsers] D on A.Creator = D.MESUserID
            left join [SYS_MESUsers] E on A.Modifier = E.MESUserID
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
                sql = sql + String.Format(@" and A.[Code] collate Chinese_PRC_CI_AS like @Code ");
                parameters[0].Value = Code;
                Parcount[0].Value = Code;
            }

            count = UniversalService.getCount(sql, Parcount);

            String orderby = "A.[Code] ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, parameters, page, rows);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// ����Ҫ����������
        /// SAM 2017��4��26��16:57:24
        /// </summary>
        /// <param name="Code">����</param>
        /// <returns></returns>
        public static DataTable GetExportList(string Code)
        {
            string select = string.Format(@"select A.Code,A.Name,
            B.Code as PlantCode,B.Code as PlantName,
            A.Comments,C.Name as Status,
            D.UserName as Creator,A.CreateLocalTime as CreateTime,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else E.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ");

            string sql = string.Format(@"  from [SYS_PlantArea] A 
            left join [SYS_Organization] B on A.PlantID= B.OrganizationID
            left join [SYS_Parameters] C on A.Status=C.ParameterID
            left join [SYS_MESUsers] D on A.Creator = D.MESUserID
            left join [SYS_MESUsers] E on A.Modifier = E.MESUserID
            where A.[SystemID]='{0}' and A.[Status] <> '{0}0201213000003'", Framework.SystemID);

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

            string orderBy = "order By A.Code ";

            return SQLHelper.ExecuteDataTable(select + sql + orderBy,CommandType.Text, parameters);
        }

        /// <summary>
        /// �ж�ָ�������Ƿ���ڳ�����
        /// SAM  2017��4��28��17:32:33
        /// true ��ʾ���ڣ�FALSE��ʾ������
        /// </summary>
        /// <param name="PlantID"></param>
        /// <returns></returns>
        public static bool CheckPlant(string PlantID)
        {
            string sql = string.Format(@"select * from [SYS_PlantArea] where [PlantID] = '{0}' and [SystemID] = '{1}' and [Status] <> '{1}0201213000003'", PlantID, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return false;
            else
                return true;
        }

        /// <summary>
        /// ���ݳ������Ż�ȡ����
        /// SAM 2017��5��22��17:53:20
        /// </summary>
        /// <param name="Code">����</param>
        /// <returns></returns>
        public static SYS_PlantArea getByCode(string Code)
        {
            string sql = string.Format(@"select Top 1 * from [SYS_PlantArea] where [Code] = '{0}'  and [SystemID] = '{1}' and [Status] <> '{1}0201213000003'", Code, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return null;
            else
                return ToEntity(dt.Rows[0]);
        }


        /// <summary>
        /// �����ĵ���
        /// SAM 2017��5��24��14:59:41
        /// </summary>
        /// <param name="Code">����</param>
        /// <param name="page">ҳ��</param>
        /// <param name="rows">����</param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> GetPlantAreaList(string Code, int page, int rows, ref int count)
        {
            string select = string.Format(@"select A.PlantAreaID,A.Code,A.Name,A.Comments,C.Name as Status,
            B.Code as PlantCode,B.Name as PlantName,A.PlantID,
            D.UserName as Creator,A.CreateLocalTime as CreateTime,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else E.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ");

            string sql = string.Format(@"  from [SYS_PlantArea] A 
            left join [SYS_Organization] B on A.PlantID= B.OrganizationID
            left join [SYS_MESUsers] D on A.Creator = D.MESUserID
            left join [SYS_MESUsers] E on A.Modifier = E.MESUserID
            left join [SYS_Parameters] C on A.[Status] = C.[ParameterID]
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

            String orderby = "A.[Code] ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, parameters, page, rows);

            return ToHashtableList(dt);
        }
    }
}

