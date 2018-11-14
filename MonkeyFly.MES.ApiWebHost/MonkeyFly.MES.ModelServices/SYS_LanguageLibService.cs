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
    public class SYS_LanguageLibService : SuperModel<SYS_LanguageLib>
    {
        /// <summary>
        /// 获取指定的语序列表
        /// SAM 2016年10月16日23:04:27
        /// </summary>
        /// <param name="suserid"></param>
        /// <param name="tableID"></param>
        /// <param name="rowID"></param>
        /// <param name="field"></param>
        /// <returns></returns>
        public static IList<Hashtable> getList(string userid, string tableID, string rowID, string field)
        {
            string sql = String.Format(@"select A.*,B.Name as LanguageName from [SYS_LanguageLib] A 
            left join SYS_Parameters as B on A.LanguageCode= B.ParameterID  
            where A.[TableID] = @TableID and A.[RowID] = @RowID and A.[Field] = @Field and A.[SystemID]=@SystemID ");

            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@SystemID",SqlDbType.VarChar),
                new SqlParameter("@TableID",SqlDbType.VarChar),
                new SqlParameter("@RowID",SqlDbType.VarChar),
                new SqlParameter("@Field",SqlDbType.VarChar),
            };

            parameters[0].Value = Framework.SystemID;
            parameters[1].Value = tableID;
            parameters[2].Value = rowID;
            parameters[3].Value = field;

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text, parameters);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// 添加
        /// SAM 2017年4月26日15:44:38
        /// </summary>
        /// <param name="userid">当前登录用户</param>
        /// <param name="lan">数据</param>
        /// <returns></returns>
        public static bool insert(string userid, SYS_LanguageLib lan)
        {
            try
            {
                string sql = string.Format(@"insert [SYS_LanguageLib](LanguageLibID,TableID,RowID,Field,OriginalLanguage,OriginalContent,
               LanguageCode,LanguageContentOne,LanguageContentTwo,IsDefault,Tag,
               Comments,SystemID,Modifier,ModifiedTime,Creator,CreateTime) values
               (@LanguageLibID,@TableID,@RowID,@Field,@OriginalLanguage,@OriginalContent,@LanguageCode,@LanguageContentOne,@LanguageContentTwo,
               @IsDefault,@Tag,@Comments,{0})", UniversalService.getInsert(userid));

                SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@LanguageLibID",SqlDbType.VarChar),
                new SqlParameter("@TableID",SqlDbType.VarChar),
                new SqlParameter("@RowID",SqlDbType.VarChar),
                new SqlParameter("@Field",SqlDbType.VarChar),
                new SqlParameter("@OriginalLanguage",SqlDbType.VarChar),
                new SqlParameter("@OriginalContent",SqlDbType.VarChar),
                new SqlParameter("@LanguageCode",SqlDbType.VarChar),
                new SqlParameter("@LanguageContentOne",SqlDbType.VarChar),
                new SqlParameter("@LanguageContentTwo",SqlDbType.VarChar),
                new SqlParameter("@IsDefault",SqlDbType.Bit),
                new SqlParameter("@Tag",SqlDbType.VarChar),
                new SqlParameter("@Comments",SqlDbType.VarChar),
            };

                parameters[0].Value = lan.LanguageLibID;
                parameters[1].Value = lan.TableID;
                parameters[2].Value = lan.RowID;
                parameters[3].Value = lan.Field;
                parameters[4].Value = (object)lan.OriginalLanguage ?? DBNull.Value;
                parameters[5].Value = (object)lan.OriginalContent ?? DBNull.Value;
                parameters[6].Value = (object)lan.LanguageCode ?? DBNull.Value;
                parameters[7].Value = (object)lan.LanguageContentOne ?? DBNull.Value;
                parameters[8].Value = (object)lan.LanguageContentTwo ?? DBNull.Value;
                parameters[9].Value = lan.IsDefault;
                parameters[10].Value = (object)lan.Tag ?? DBNull.Value;
                parameters[11].Value = (object)lan.Comments ?? DBNull.Value;


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
        /// SAM 2016年9月24日18:59:04
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="lan"></param>
        /// <returns></returns>
        public static bool update(string userId, SYS_LanguageLib lan)
        {
            try
            {
                string sql = String.Format(@"update [SYS_LanguageLib] SET {0},
                [OriginalLanguage] =@OriginalLanguage,[OriginalContent]=@OriginalContent,
                [LanguageCode] =@LanguageCode,[LanguageContentOne]=@LanguageContentOne,[LanguageContentTwo]=@LanguageContentTwo,
                [IsDefault] =@IsDefault,[Tag]=@Tag,[Comments]=@Comments
                WHERE [LanguageLibID] = @LanguageLibID", UniversalService.getUpdate(userId));

                SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@LanguageLibID",SqlDbType.VarChar),
                new SqlParameter("@OriginalLanguage",SqlDbType.VarChar),
                new SqlParameter("@OriginalContent",SqlDbType.VarChar),
                new SqlParameter("@LanguageCode",SqlDbType.VarChar),
                new SqlParameter("@LanguageContentOne",SqlDbType.VarChar),
                new SqlParameter("@LanguageContentTwo",SqlDbType.VarChar),
                new SqlParameter("@IsDefault",SqlDbType.Bit),
                new SqlParameter("@Tag",SqlDbType.VarChar),
                new SqlParameter("@Comments",SqlDbType.VarChar),
                };

                parameters[0].Value = lan.LanguageLibID;
                parameters[1].Value = (object)lan.OriginalLanguage ?? DBNull.Value;
                parameters[2].Value = (object)lan.OriginalContent ?? DBNull.Value;
                parameters[3].Value = (object)lan.LanguageCode ?? DBNull.Value;
                parameters[4].Value = (object)lan.LanguageContentOne ?? DBNull.Value;
                parameters[5].Value = (object)lan.LanguageContentTwo ?? DBNull.Value;
                parameters[6].Value = lan.IsDefault;
                parameters[7].Value = (object)lan.Tag ?? DBNull.Value;
                parameters[8].Value = (object)lan.Comments ?? DBNull.Value;


                return SQLHelper.ExecuteNonQuery(sql, CommandType.Text, parameters) > 0;

            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return false;
            }
        }

        /// <summary>
        ///  删除
        ///  SAM 2016年9月24日18:59:01
        /// </summary>
        /// <param name="LanguageLibID">LanguageLibID</param>
        /// <returns></returns>
        public static bool delete(string LanguageLibID)
        {
            try
            {
                string sql = String.Format(@"delete from [SYS_LanguageLib] where [LanguageLibID] = @LanguageLibID");
                SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@LanguageLibID",SqlDbType.VarChar),
                };
                parameters[0].Value = LanguageLibID;

                return SQLHelper.ExecuteNonQuery(sql, CommandType.Text, parameters) > 0;
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return false;
            }
        }

        /// <summary>
        /// 删除语序（存在多个语序时）
        /// SAM 2016年10月24日10:06:07
        /// </summary>
        /// <param name="LanguageCode"></param>
        /// <param name="rowid"></param>
        /// <returns></returns>
        public static bool DeleteLanguage(string LanguageCode, String rowid)
        {
            try
            {
                string sql = String.Format(@"delete from [SYS_LanguageLib] where [LanguageCode] = @LanguageCode and RowID=@RowID");
                SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@LanguageCode",SqlDbType.VarChar),
                new SqlParameter("@RowID",SqlDbType.VarChar),
                };
                parameters[0].Value = LanguageCode;
                parameters[1].Value = rowid;

                return SQLHelper.ExecuteNonQuery(sql, CommandType.Text, parameters) > 0;
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return false;
            }
        }

        /// <summary>
        /// 更新语序
        /// SAM 2016年10月24日10:05:30
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="Content"></param>
        /// <param name="Code">代号</param>
        /// <param name="IsDefault"></param>
        /// <param name="rowid"></param>
        /// <param name="field"></param>
        /// <returns></returns>
        public static bool UpdateLanguage(string userId, string Content, string Code, bool IsDefault, string rowid, string field)
        {
            try
            {
                string sql = String.Format(@"update [SYS_LanguageLib] SET {0},[LanguageContentOne]=@LanguageContentOne,[IsDefault] =@IsDefault
                                            WHERE [LanguageCode] = @LanguageCode and RowID=@RowID and Field = @Field ", UniversalService.getUpdate(userId));

                SqlParameter[] parameters = new SqlParameter[]{
                 new SqlParameter("@RowID",SqlDbType.VarChar),
                 new SqlParameter("@Field",SqlDbType.VarChar),
                 new SqlParameter("@IsDefault",SqlDbType.Bit),
                 new SqlParameter("@LanguageCode",SqlDbType.VarChar),
                 new SqlParameter("@LanguageContentOne",SqlDbType.VarChar),
                };

                parameters[0].Value = rowid;
                parameters[1].Value = field;
                parameters[2].Value = IsDefault;
                parameters[3].Value = Code;
                parameters[4].Value = (object)Content ?? DBNull.Value;


                return SQLHelper.ExecuteNonQuery(sql, CommandType.Text, parameters) > 0;

            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return false;
            }
        }

        /// <summary>
        ///  根据rowid和LanguageCode判断此记录是否有指定的语序翻译
        ///  SAM 2016年10月9日16:46:20
        /// </summary>
        /// <param name="rowid">流水编号</param>
        /// <param name="LanguageCode">语序流水号</param>
        /// <returns></returns>
        public static bool checkLanguage(string rowid, string Field, string LanguageCode, string LanguageLibID)
        {
            String sql = String.Format(@"select * from [SYS_LanguageLib] where [RowID]='{0}' and [LanguageCode]='{1}' and [SystemID] = '{2}' and Field='{3}'", rowid, LanguageCode, Framework.SystemID, Field);

            if (!String.IsNullOrWhiteSpace(LanguageLibID))
                sql = sql + String.Format(@" and [LanguageLibID] <> '{0}' ", LanguageLibID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return false;
            else
                return true;
        }

        /// <summary>
        /// 根据语序流水号获取语序实体
        /// SAM 2016年10月13日17:39:56
        /// </summary>
        /// <param name="LanguageLibID">语序流水号</param>
        /// <returns></returns>
        public static SYS_LanguageLib getLanguage(string LanguageLibID)
        {
            string sql = string.Format(@"select * from [SYS_LanguageLib] where [LanguageLibID]='{0}' ", LanguageLibID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return null;

            return ToList(dt)[0];
        }





        /// <summary>
        /// 厂别主档-获取语序列表
        /// SAM 2017年4月26日15:52:33
        /// </summary>
        /// <param name="rowid">厂别流水号</param>
        /// <returns></returns>
        public static IList<Hashtable> Inf00001GetPlantLanguageList(string rowid)
        {
            string sql = String.Format(@"select A.LanguageLibID,A.LanguageCode,D.Name as LanguageName,A.LanguageContentOne as Name,A.IsDefault,A.RowID,
            (select LanguageContentOne from SYS_LanguageLib where A.RowID=RowID and A.LanguageCode=LanguageCode and Field='Comments') as Comments
            from SYS_LanguageLib as A left join SYS_Parameters as D on A.LanguageCode= D.ParameterID 
            where A.Field='Name' and A.SystemID='{0}' and A.RowID='{1}' and A.TableID=5 ", Framework.SystemID, rowid);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text, null);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// 厂区主档-获取语序列表
        /// SAM 2017年4月26日16:06:44
        /// </summary>
        /// <param name="rowid"></param>
        /// <returns></returns>
        public static IList<Hashtable> Inf00001GetPlantAreaLanguageList(string rowid)
        {
            string sql = String.Format(@"select A.LanguageLibID,A.LanguageCode,D.Name as LanguageName,A.LanguageContentOne as Name,A.IsDefault,A.RowID,
            (select LanguageContentOne from SYS_LanguageLib where A.RowID=RowID and A.LanguageCode=LanguageCode and Field='Comments') as Comments
            from SYS_LanguageLib as A left join SYS_Parameters as D on A.LanguageCode= D.ParameterID 
            where A.Field='Name' and A.SystemID='{0}' and A.RowID='{1}' and A.TableID=30 ", Framework.SystemID, rowid);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text, null);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// 获取指定客户的语序
        /// SAM 2017年4月27日10:30:02
        /// </summary>
        /// <param name="rowid"></param>
        /// <returns></returns>
        public static IList<Hashtable> Inf00007GetLanguageList(string rowid)
        {
            string sql = String.Format(@"select A.LanguageLibID,A.LanguageCode,D.Name as LanguageName,A.LanguageContentOne as Code,A.IsDefault,A.RowID,
            (select LanguageContentOne from SYS_LanguageLib where A.RowID=RowID and A.LanguageCode=LanguageCode and Field='Name') as Name,
            (select LanguageContentOne from SYS_LanguageLib where A.RowID=RowID and A.LanguageCode=LanguageCode and Field='Comments') as Comments
            from SYS_LanguageLib as A left join SYS_Parameters as D on A.LanguageCode= D.ParameterID 
            where A.Field='Code' and A.SystemID='{0}' and A.RowID='{1}' and A.TableID=31 ", Framework.SystemID, rowid);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text, null);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// 获取指定厂商的语序
        /// SAM 2017年4月27日11:34:42
        /// </summary>
        /// <param name="rowid"></param>
        /// <returns></returns>
        public static IList<Hashtable> Inf00008GetLanguageList(string rowid)
        {
            string sql = String.Format(@"select A.LanguageLibID,A.LanguageCode,D.Name as LanguageName,A.LanguageContentOne as Code,A.IsDefault,A.RowID,
            (select LanguageContentOne from SYS_LanguageLib where A.RowID=RowID and A.LanguageCode=LanguageCode and Field='Name') as Name,
            (select LanguageContentOne from SYS_LanguageLib where A.RowID=RowID and A.LanguageCode=LanguageCode and Field='Comments') as Comments
            from SYS_LanguageLib as A left join SYS_Parameters as D on A.LanguageCode= D.ParameterID 
            where A.Field='Code' and A.SystemID='{0}' and A.RowID='{1}' and A.TableID=32 ", Framework.SystemID, rowid);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text, null);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// 获取角色语系
        /// Tom 2017年4月27日16:45:20
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static object Inf00004GetPlantAreaLanguageList(string rowid)
        {
            string sql = String.Format(@"select A.LanguageLibID,A.LanguageCode,D.Name as LanguageName,A.LanguageContentOne as Name,A.IsDefault,A.RowID,
            (select LanguageContentOne from SYS_LanguageLib where A.RowID=RowID and A.LanguageCode=LanguageCode and Field='Description') as Description
            from SYS_LanguageLib as A left join SYS_Parameters as D on A.LanguageCode= D.ParameterID 
            where A.Field='Name' and A.SystemID='{0}' and A.RowID='{1}' and A.TableID=6 ", Framework.SystemID, rowid);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text, null);

            return ToHashtableList(dt);
        }


        /// <summary>
        /// 判断语序是否在使用中
        /// 在返回true 不在返回false
        /// </summary>
        /// <param name="LanguageCode"></param>
        /// <returns></returns>
        public static bool CheckLanguage(string LanguageCode)
        {
            string sql = string.Format(@"select * from [SYS_LanguageLib] where [LanguageCode] = '{0}' and [SystemID] = '{1}' ", LanguageCode, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return false;
            else
                return true;
        }


        /// <summary>
        /// 获取Name和Comments的双语序
        /// SAM 2017年5月4日11:22:46
        /// </summary>
        /// <param name="rowid"></param>
        /// <param name="TableID"></param>
        /// <returns></returns>
        public static IList<Hashtable> NCGetLanguageList(string rowid, string TableID)
        {
            string sql = String.Format(@"select A.LanguageLibID,A.LanguageCode,D.Name as LanguageName,A.LanguageContentOne as Name,A.IsDefault,A.RowID,A.TableID,
            (select LanguageContentOne from SYS_LanguageLib where A.RowID=RowID and A.LanguageCode=LanguageCode and Field='Comments') as Comments,
             B.UserName as Creator,A.CreateTime,
            (CASE WHEN A.CreateTime=A.ModifiedTime THEN NULL else C.UserName END) as Modifier,
            (CASE WHEN A.CreateTime=A.ModifiedTime THEN NULL else A.ModifiedTime END) as ModifiedTime 
            from SYS_LanguageLib as A 
            left join SYS_Parameters as D on A.LanguageCode= D.ParameterID 
            left join [SYS_MESUsers] B on A.[Creator] = B.[MESUserID]
            left join [SYS_MESUsers] C on A.[Modifier] = C.[MESUserID]
            where A.Field='Name' and A.SystemID='{0}' and A.RowID='{1}' and A.TableID={2} ", Framework.SystemID, rowid, TableID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text, null);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// 获取Code,Name和Comments的三语序
        /// SAM 2017年5月8日15:49:22
        /// </summary>
        /// <param name="rowid"></param>
        /// <param name="TableID"></param>
        /// <returns></returns>
        public static IList<Hashtable> CNCGetLanguageList(string rowid, string TableID)
        {
            string sql = String.Format(@"select A.LanguageLibID,A.LanguageCode,D.Name as LanguageName,A.LanguageContentOne as Code,A.IsDefault,A.RowID,A.TableID,
            (select LanguageContentOne from SYS_LanguageLib where A.RowID=RowID and A.LanguageCode=LanguageCode and Field='Name') as Name,
            (select LanguageContentOne from SYS_LanguageLib where A.RowID=RowID and A.LanguageCode=LanguageCode and Field='Comments') as Comments,
             B.UserName as Creator,A.CreateTime,
            (CASE WHEN A.CreateTime=A.ModifiedTime THEN NULL else C.UserName END) as Modifier,
            (CASE WHEN A.CreateTime=A.ModifiedTime THEN NULL else A.ModifiedTime END) as ModifiedTime 
            from SYS_LanguageLib as A 
            left join SYS_Parameters as D on A.LanguageCode= D.ParameterID 
            left join [SYS_MESUsers] B on A.[Creator] = B.[MESUserID]
            left join [SYS_MESUsers] C on A.[Modifier] = C.[MESUserID]
            where A.Field='Code' and A.SystemID='{0}' and A.RowID='{1}' and A.TableID={2} ", Framework.SystemID, rowid, TableID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text, null);

            return ToHashtableList(dt);
        }


        /// <summary>
        /// 获取料品语序
        /// SAM 2017年5月17日10:05:41
        /// </summary>
        /// <param name="rowid"></param>
        /// <returns></returns>
        public static IList<Hashtable> Inf00010GetLanguageList(string rowid)
        {
            string sql = String.Format(@"select A.LanguageLibID,A.LanguageCode,D.Name as LanguageName,A.LanguageContentOne as Code,A.IsDefault,A.RowID,A.TableID,
            (select LanguageContentOne from SYS_LanguageLib where A.RowID=RowID and A.LanguageCode=LanguageCode and Field='Name') as Name,
            (select LanguageContentOne from SYS_LanguageLib where A.RowID=RowID and A.LanguageCode=LanguageCode and Field='Specification') as Specification,
            (select LanguageContentOne from SYS_LanguageLib where A.RowID=RowID and A.LanguageCode=LanguageCode and Field='Comments') as Comments,
             B.UserName as Creator,A.CreateTime,
            (CASE WHEN A.CreateTime=A.ModifiedTime THEN NULL else C.UserName END) as Modifier,
            (CASE WHEN A.CreateTime=A.ModifiedTime THEN NULL else A.ModifiedTime END) as ModifiedTime 
            from SYS_LanguageLib as A 
            left join SYS_Parameters as D on A.LanguageCode= D.ParameterID 
            left join [SYS_MESUsers] B on A.[Creator] = B.[MESUserID]
            left join [SYS_MESUsers] C on A.[Modifier] = C.[MESUserID]
            where A.Field='Code' and A.SystemID='{0}' and A.RowID='{1}' and A.TableID=39 ", Framework.SystemID, rowid);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text, null);

            return ToHashtableList(dt);
        }





        /// <summary>
        /// 根据对应流水号获取对应字段的语序翻译
        /// SAM 2017年7月30日15:31:353
        /// </summary>
        /// <param name="RowID"></param>
        /// <param name="Lan"></param>
        /// <returns></returns>
        public static string GetLan(string RowID,string Field,int TableID)
        {
            string sql = String.Format(@"select LanguageContentOne
            from SYS_LanguageLib
            where [Field]='{3}' and [SystemID]='{0}' and [RowID]='{1}' 
            and [LanguageCode] = '{2}' and [TableID]='{4}' "
            , Framework.SystemID, RowID, UniversalService.GetLan(), Field, TableID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return null;

            return ToEntity(dt.Rows[0]).LanguageContentOne;
        }

        /// <summary>
        /// 根据对应流水号获取对应字段的语序翻译(实体)
        /// SAM 2017年9月21日14:42:21
        /// </summary>
        /// <param name="RowID"></param>
        /// <param name="Field"></param>
        /// <param name="TableID"></param>
        /// <returns></returns>
        public static SYS_LanguageLib Get(string RowID, string Field, int TableID)
        {
            string sql = String.Format(@"select *
            from [SYS_LanguageLib]
            where [Field]='{3}' and [SystemID]='{0}' and [RowID]='{1}' 
            and [LanguageCode] = '{2}' and [TableID]='{4}' "
            , Framework.SystemID, RowID, UniversalService.GetLan(), Field, TableID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return null;

            return ToEntity(dt.Rows[0]);
        }


        /// <summary>
        /// 系统参数-获取语序列表
        /// SAM 2017年8月24日10:52:04
        /// </summary>
        /// <param name="rowid"></param>
        /// <returns></returns>
        public static IList<Hashtable> Inf00020GetLanguageList(string rowid)
        {
            string sql = String.Format(@"select A.LanguageLibID,A.LanguageCode,D.Name as LanguageName,A.LanguageContentOne as Name,A.IsDefault,A.RowID,A.TableID,
            (select LanguageContentOne from SYS_LanguageLib where A.RowID=RowID and A.LanguageCode=LanguageCode and Field='Value') as Value,
            (select LanguageContentOne from SYS_LanguageLib where A.RowID=RowID and A.LanguageCode=LanguageCode and Field='Comments') as Comments,
             B.UserName as Creator,A.CreateTime,
            (CASE WHEN A.CreateTime=A.ModifiedTime THEN NULL else C.UserName END) as Modifier,
            (CASE WHEN A.CreateTime=A.ModifiedTime THEN NULL else A.ModifiedTime END) as ModifiedTime 
            from SYS_LanguageLib as A 
            left join SYS_Parameters as D on A.LanguageCode= D.ParameterID 
            left join [SYS_MESUsers] B on A.[Creator] = B.[MESUserID]
            left join [SYS_MESUsers] C on A.[Modifier] = C.[MESUserID]
            where A.Field='Name' and A.SystemID='{0}' and A.RowID='{1}' and A.TableID='110' ", Framework.SystemID, rowid);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text, null);

            return ToHashtableList(dt);
        }





    }
}
