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
    public class MES_ParameterService : SuperModel<MES_Parameter>
    {
        /// <summary>
        /// 添加
        /// SAM2017年7月31日00:15:34
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="Model"></param>
        /// <returns></returns>
        public static bool insert(string userId, MES_Parameter Model)
        {
            try
            {
                string sql = string.Format(@"insert[MES_Parameter]([ParameterID],[Module],
                [Code],[Name],[Setting],[Option],[Value],[Status],
                [Comments],[Modifier],[ModifiedTime],[ModifiedLocalTime],[Creator],[CreateTime],[CreateLocalTime],[SystemID]) values
                 (@ParameterID,@Module,@Code,@Name,@Setting,@Option,@Value,@Status,
                @Comments,'{0}','{1}','{2}','{0}','{1}','{2}','{3}')", userId, DateTime.Now, DateTime.Now, Framework.SystemID);

                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@ParameterID",SqlDbType.VarChar),
                    new SqlParameter("@Module",SqlDbType.NVarChar),
                    new SqlParameter("@Code",SqlDbType.NVarChar),
                    new SqlParameter("@Name",SqlDbType.NVarChar),
                    new SqlParameter("@Setting",SqlDbType.VarChar),
                    new SqlParameter("@Option",SqlDbType.NVarChar),
                    new SqlParameter("@Value",SqlDbType.NVarChar),
                    new SqlParameter("@Status",SqlDbType.VarChar),
                    new SqlParameter("@Comments",SqlDbType.NVarChar),
                    };

                parameters[0].Value = (Object)Model.ParameterID ?? DBNull.Value;
                parameters[1].Value = (Object)Model.Module ?? DBNull.Value;
                parameters[2].Value = (Object)Model.Code ?? DBNull.Value;
                parameters[3].Value = (Object)Model.Name ?? DBNull.Value;
                parameters[4].Value = (Object)Model.Setting ?? DBNull.Value;
                parameters[5].Value = (Object)Model.Option ?? DBNull.Value;
                parameters[6].Value = (Object)Model.Value ?? DBNull.Value;
                parameters[7].Value = (Object)Model.Status ?? DBNull.Value;
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
        /// 更新
        /// SAM 2017年7月31日00:15:121
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="Model"></param>
        /// <returns></returns>
        public static bool update(string userId, MES_Parameter Model)
        {
            try
            {
                string sql = String.Format(@"update[MES_Parameter] set {0},
                [Setting]=@Setting,[Option]=@Option,[Value]=@Value,[Status]=@Status,
                [Comments]=@Comments where [ParameterID]=@ParameterID", UniversalService.getUpdateUTC(userId));
                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@ParameterID",SqlDbType.VarChar),
                    new SqlParameter("@Setting",SqlDbType.VarChar),
                    new SqlParameter("@Option",SqlDbType.NVarChar),
                    new SqlParameter("@Value",SqlDbType.NVarChar),
                    new SqlParameter("@Status",SqlDbType.VarChar),
                    new SqlParameter("@Comments",SqlDbType.NVarChar),
                    };

                parameters[0].Value = Model.ParameterID;
                parameters[1].Value = (Object)Model.Setting ?? DBNull.Value;
                parameters[2].Value = (Object)Model.Option ?? DBNull.Value;
                parameters[3].Value = (Object)Model.Value ?? DBNull.Value;
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
        /// 获取单一实体
        /// SAM 2017年7月31日00:15:41
        /// </summary>
        /// <param name="ParameterID"></param>
        /// <returns></returns>
        public static MES_Parameter get(string ParameterID)
        {
            string sql = string.Format(@"select Top 1 * from [MES_Parameter] where [ParameterID] = '{0}'  and [SystemID] = '{1}' ", ParameterID, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return null;
            else
                return ToEntity(dt.Rows[0]);
        }

        /// <summary>
        /// 系统参数-列表
        /// SAM 2017年7月31日00:18:59
        /// </summary>
        /// <param name="Module"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> Inf00020GetList(string Module, int page, int rows, ref int count)
        {
            string select = string.Format(@"select A.ParameterID,A.Module,A.Code,A.Name,A.Setting,A.[Option],
            E.Name as SettingName,D.Code as ModuleCode,
            A.Value,A.Comments,D.Code+'_'+D.Name as ModuleValue,
            B.Emplno+'-'+B.UserName as Creator,A.CreateLocalTime as CreateTime,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else C.Emplno+'-'+C.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ");

            string sql = string.Format(@"  from [MES_Parameter] A 
            left join [SYS_MESUsers] B on A.[Creator] = B.[MESUserID]
            left join [SYS_MESUsers] C on A.[Modifier] = C.[MESUserID]
            left join [SYS_Parameters] D on A.[Module] = D.[ParameterID]
            left join [SYS_Parameters] E on A.[Setting] = E.[ParameterID]
            where A.[SystemID]='{0}' and A.[Status] = '{0}0201213000001' ", Framework.SystemID);

            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@Module",SqlDbType.VarChar)
            };
            parameters[0].Value = DBNull.Value;

            SqlParameter[] Parcount = new SqlParameter[]{
                new SqlParameter("@Module",SqlDbType.VarChar)
            };
            Parcount[0].Value = DBNull.Value;
            if (!string.IsNullOrWhiteSpace(Module))
            {
                sql = sql + string.Format(@" and A.[Module] = @Module ");
                parameters[0].Value = Module;
                Parcount[0].Value = Module;
            }
            count = UniversalService.getCount(sql, Parcount);

            String orderby = " A.[Status],A.[Code] ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, parameters, page, rows);

            return ToHashtableList(dt);
        }
    }
}

