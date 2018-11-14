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
    public class SYS_AttachmentsService : SuperModel<SYS_Attachments>
    {
        /// <summary>
        /// 新增
        /// SAM 2017年5月22日11:59:44
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="Model"></param>
        /// <returns></returns>
        public static bool insert(string userId, SYS_Attachments Model)
        {
            try
            {
                string sql = string.Format(@"insert[SYS_Attachments]([AttachmentID],[ObjectID],[Name],[OriginalName],[Path],[UploadTime],
                [IsNotInit],[Default],[Type],[Status],[Sequence],[Comments],[Modifier],[ModifiedTime],[Creator],[CreateTime],[SystemID]) values
                 (@AttachmentID,@ObjectID,@Name,@OriginalName,@Path,@UploadTime,@IsNotInit,@Default,@Type,@Status,@Sequence,@Comments,'{0}','{1}','{0}','{1}','{2}')", userId, DateTime.Now, Framework.SystemID);

                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@AttachmentID",SqlDbType.VarChar),
                    new SqlParameter("@Name",SqlDbType.VarChar),
                    new SqlParameter("@OriginalName",SqlDbType.VarChar),
                    new SqlParameter("@Path",SqlDbType.VarChar),
                    new SqlParameter("@UploadTime",SqlDbType.DateTime),
                    new SqlParameter("@IsNotInit",SqlDbType.Bit),
                    new SqlParameter("@Default",SqlDbType.Bit),
                    new SqlParameter("@Status",SqlDbType.VarChar),
                    new SqlParameter("@Comments",SqlDbType.VarChar),
                    new SqlParameter("@ObjectID",SqlDbType.VarChar),
                    new SqlParameter("@Sequence",SqlDbType.Int),
                    new SqlParameter("@Type",SqlDbType.VarChar)
                    };

                parameters[0].Value = (Object)Model.AttachmentID ?? DBNull.Value;
                parameters[1].Value = (Object)Model.Name ?? DBNull.Value;
                parameters[2].Value = (Object)Model.OriginalName ?? DBNull.Value;
                parameters[3].Value = (Object)Model.Path ?? DBNull.Value;
                parameters[4].Value = (Object)Model.UploadTime ?? DBNull.Value;
                parameters[5].Value = (Object)Model.IsNotInit ?? DBNull.Value;
                parameters[6].Value = (Object)Model.Default ?? DBNull.Value;
                parameters[7].Value = (Object)Model.Status ?? DBNull.Value;
                parameters[8].Value = (Object)Model.Comments ?? DBNull.Value;
                parameters[9].Value = (Object)Model.ObjectID ?? DBNull.Value;
                parameters[10].Value = (Object)Model.Sequence ?? DBNull.Value;
                parameters[11].Value = (Object)Model.Type ?? DBNull.Value;

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
        /// SAM 2017年5月22日11:59:51
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="Model"></param>
        /// <returns></returns>
        public static bool update(string userId, SYS_Attachments Model)
        {
            try
            {
                string sql = String.Format(@"update[SYS_Attachments] set {0},
                [Name]=@Name,[OriginalName]=@OriginalName,[Path]=@Path,[IsNotInit]=@IsNotInit,[Default]=@Default, 
                [Status]=@Status
                where [AttachmentID]=@AttachmentID", UniversalService.getUpdate(userId));
                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@AttachmentID",SqlDbType.VarChar),
                    new SqlParameter("@Name",SqlDbType.VarChar),
                    new SqlParameter("@OriginalName",SqlDbType.VarChar),
                    new SqlParameter("@Path",SqlDbType.VarChar),
                    new SqlParameter("@IsNotInit",SqlDbType.Bit),
                    new SqlParameter("@Default",SqlDbType.Bit),
                    new SqlParameter("@Status",SqlDbType.VarChar)
                    };

                parameters[0].Value = Model.AttachmentID;
                parameters[1].Value = (Object)Model.Name ?? DBNull.Value;
                parameters[2].Value = (Object)Model.OriginalName ?? DBNull.Value;
                parameters[3].Value = (Object)Model.Path ?? DBNull.Value;
                parameters[4].Value = (Object)Model.IsNotInit ?? DBNull.Value;
                parameters[5].Value = (Object)Model.Default ?? DBNull.Value;
                parameters[6].Value = (Object)Model.Status ?? DBNull.Value;

                return SQLHelper.ExecuteNonQuery(sql, CommandType.Text, parameters) > 0;
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return false;
            }
        }

        /// <summary>
        /// 将指定设备下的附件的预设值改成false
        /// SAM 2017年5月31日17:01:42
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="Model"></param>
        /// <returns></returns>
        public static bool updateDefault( string EquipmentID)
        {
            try
            {
                string sql = string.Format(@"update [SYS_Attachments] set [Default]=0 where [ObjectID] = '{0}'", EquipmentID);

                return SQLHelper.ExecuteNonQuery(sql, CommandType.Text, null) > 0;
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return false;
            }
        }

        /// <summary>
        /// 获取单一实体
        /// SAM 2017年5月22日12:00:35
        /// </summary>
        /// <param name="AttachmentID"></param>
        /// <returns></returns>
        public static SYS_Attachments get(string AttachmentID)
        {
            string sql = string.Format(@"select Top 1 * from [SYS_Attachments] where [AttachmentID] = '{0}'  and [SystemID] = '{1}' ", AttachmentID, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return null;
            else
                return ToEntity(dt.Rows[0]);
        }

        /// <summary>
        /// 根据流水号+Name 获取实体
        /// SAM 2017年5月23日10:13:10
        /// </summary>
        /// <param name="AttachmentID"></param>
        /// <param name="Name"></param>
        /// <returns></returns>
        public static SYS_Attachments get(string AttachmentID,string Name)
        {
            string sql = string.Format(@"select Top 1 * from [SYS_Attachments] where [AttachmentID] = '{0}'  and [SystemID] = '{1}' and [Name] ='{2}' and [Status] <> '{1}0201213000003'", AttachmentID, Framework.SystemID, Name);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return null;
            else
                return ToEntity(dt.Rows[0]);
        }

        /// <summary>
        /// 获取一个设备对应的图样列表
        /// SAM 2017年5月23日09:44:35
        /// </summary>
        /// <param name="AttachmentID"></param>
        /// <param name="page">页码</param>
        /// <param name="rows">行数</param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> Ems00001GetPatternList(string EquipmentID, int page, int rows, ref int count)
        {
            string select = string.Format(@"select A.AttachmentID,A.Name,A.OriginalName,A.Path,A.[Default],
            B.UserName as Creator,A.CreateTime,
            (CASE WHEN A.CreateTime=A.ModifiedTime THEN NULL else C.UserName END) as Modifier,
            (CASE WHEN A.CreateTime=A.ModifiedTime THEN NULL else A.ModifiedTime END) as ModifiedTime ");

            string sql = string.Format(@"  from [SYS_Attachments] A 
            left join [SYS_MESUsers] B on A.[Creator] = B.[MESUserID]
            left join [SYS_MESUsers] C on A.[Modifier] = C.[MESUserID]
            where A.[SystemID]='{0}' and A.[Status] <> '{0}0201213000003' and A.[ObjectID] = '{1}' ", Framework.SystemID, EquipmentID);


            count = UniversalService.getCount(sql, null);

            String orderby = " A.[Name] ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, null, page, rows);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// 获取附件序号
        /// SAM 2016年7月20日14:16:31
        /// </summary>
        /// <param name="AttachmentID"></param>
        /// <returns></returns>
        public static int getCount(string ObjectID)
        {
            string sql = String.Format("select Count(*) from [SYS_Attachments] where [ObjectID] = '{0}'", ObjectID);

            //return MESSQLHelper.getValue<int>(sql);
            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return 0;
            else
                return int.Parse(dt.Rows[0][0].ToString());
        }

        /// <summary>
        /// 判断指定附件编号下是否已存在相同的名字
        /// SAM 2017年5月23日10:06:19 
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="AttachmentID"></param>
        /// <returns></returns>
        public static bool CheckName(string Name, string ObjectID, string AttachmentID)
        {
            string sql = string.Format(@"select Top 1 * from [SYS_Attachments] where [SystemID]='{0}' and [Status] <> '{0}0201213000003'", Framework.SystemID);
     
            sql = sql + string.Format(@" and [Name] = '{0}' ", Name);

            if (!String.IsNullOrWhiteSpace(ObjectID))
                sql = sql + string.Format(@" and [ObjectID] = '{0}' ", ObjectID);

            if (!String.IsNullOrWhiteSpace(AttachmentID))
                sql = sql + string.Format(@" and [AttachmentID] <> '{0}' ", AttachmentID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text, null);

            if (dt.Rows.Count == 0)
                return false;
            else
                return true;
        }

        /// <summary>
        /// 获取客诉明细附件列表
        /// SAM 2017年6月15日16:25:37
        /// </summary>
        /// <param name="ComplaintDetailID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> Qcs00009GetAttachmentList(string ComplaintDetailID, int page, int rows, ref int count)
        {
            string select = string.Format(@"select A.AttachmentID,A.Name,A.OriginalName,A.Path,
            B.Emplno+'-'+B.UserName as Creator,A.CreateTime,A.Sequence,
            (CASE WHEN A.CreateTime=A.ModifiedTime THEN NULL else C.Emplno+'-'+C.UserName END) as Modifier,
            (CASE WHEN A.CreateTime=A.ModifiedTime THEN NULL else A.ModifiedTime END) as ModifiedTime ");

            string sql = string.Format(@"  from [SYS_Attachments] A 
            left join [SYS_MESUsers] B on A.[Creator] = B.[MESUserID]
            left join [SYS_MESUsers] C on A.[Modifier] = C.[MESUserID]
            where A.[SystemID]='{0}' and A.[Status] = '{0}0201213000001' and A.[ObjectID] ='{1}' and A.[Type] ='{0}0201213000082'", Framework.SystemID, ComplaintDetailID);

            count = UniversalService.getCount(sql, null);

            string orderby = " A.[Sequence] ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, null, page, rows);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// 获取客诉明细的处理对策附件列表
        /// SAM 2017年6月22日14:15:57
        /// </summary>
        /// <param name="ComplaintDetailID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> Qcs00010GetAttachmentList(string ComplaintDetailID, int page, int rows, ref int count)
        {
            string select = string.Format(@"select A.AttachmentID,A.Name,A.OriginalName,A.Path,
            B.Emplno+'-'+B.UserName as Creator,A.CreateTime,A.Sequence,
            (CASE WHEN A.CreateTime=A.ModifiedTime THEN NULL else C.Emplno+'-'+C.UserName END) as Modifier,
            (CASE WHEN A.CreateTime=A.ModifiedTime THEN NULL else A.ModifiedTime END) as ModifiedTime ");

            string sql = string.Format(@"  from [SYS_Attachments] A 
            left join [SYS_MESUsers] B on A.[Creator] = B.[MESUserID]
            left join [SYS_MESUsers] C on A.[Modifier] = C.[MESUserID]
            where A.[SystemID]='{0}' and A.[Status] = '{0}0201213000001' and A.[ObjectID] ='{1}' and A.[Type] ='{0}0201213000083'", Framework.SystemID, ComplaintDetailID);

            count = UniversalService.getCount(sql, null);

            string orderby = " A.[Sequence] ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, null, page, rows);

            return ToHashtableList(dt);
        }


        /// <summary>
        /// 系统设置--上传底图
        /// Alvin 2017年10月19日10:36:44
        /// ObjectID = 10039007121300000A
        /// </summary>
        /// <param name="token"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> GetBaseMapList(string token, int page, int rows, ref int count)
        {
            string select = string.Format(@" select AttachmentID,Name,OriginalName,Path,Comments,Sequence,ObjectID ");
            string sql = string.Format(@" from [SYS_Attachments] where [SystemID] = '{0}' and 
                                          [ObjectID] = '{0}007121300000A' and [Status] = '{0}0201213000001' " ,Framework.SystemID);
            count = UniversalService.getCount(sql, null);
            string orderby = " [Sequence] ";
            DataTable dt = UniversalService.getTable(select, sql, orderby, null, page, rows);
            return ToHashtableList(dt);
        }

        /// <summary>
        /// 登录前获取底图
        /// Alvin 2017年10月19日19:02:13
        /// </summary>
        /// <returns></returns>
        public static IList<Hashtable> GetMap()
        {
            string select = string.Format(@" select AttachmentID,Name,Path,Comments,Sequence
                                             from [SYS_Attachments] where [SystemID] = '{0}' and 
                                             [ObjectID] = '{0}007121300000A' and [Status] = '{0}0201213000001' order by  [Sequence]", Framework.SystemID);
            DataTable dt = SQLHelper.ExecuteDataTable(select, CommandType.Text);
            return ToHashtableList(dt);
        }
    }
}

