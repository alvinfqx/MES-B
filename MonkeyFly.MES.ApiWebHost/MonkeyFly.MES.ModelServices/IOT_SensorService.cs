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
    public class IOT_SensorService : SuperModel<IOT_Sensor>
    {
        /// <summary>
        /// ����
        /// SAM 2017��5��23��12:04:40
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="Model"></param>
        /// <returns></returns>
        public static bool insert(string userId, IOT_Sensor Model)
        {
            try
            {
                string sql = string.Format(@"insert[IOT_Sensor]([SensorID],[Code],[Name],[Status],[EnabledDate],[FailureDate],[Brand],
                [Type],[ManufacturerID],[IsWarning],[MaxAlarmTime],[MinAlarmTime],[MaxValue],[MinValue],
                [Comments],[Modifier],[ModifiedTime],[ModifiedLocalTime],[Creator],[CreateTime],[CreateLocalTime],[SystemID]) values
                (@SensorID,@Code,@Name,@Status,@EnabledDate,@FailureDate,@Brand,@Type,@ManufacturerID,
                @IsWarning,@MaxAlarmTime,@MinAlarmTime,@MaxValue,@MinValue,@Comments,'{0}','{1}','{2}','{0}','{1}','{2}','{3}')", userId, DateTime.Now, DateTime.Now, Framework.SystemID);

                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@SensorID",SqlDbType.VarChar),
                    new SqlParameter("@Code",SqlDbType.NVarChar),
                    new SqlParameter("@Name",SqlDbType.NVarChar),
                    new SqlParameter("@Status",SqlDbType.VarChar),
                    new SqlParameter("@EnabledDate",SqlDbType.DateTime),
                    new SqlParameter("@FailureDate",SqlDbType.DateTime),
                    new SqlParameter("@Brand",SqlDbType.VarChar),
                    new SqlParameter("@Type",SqlDbType.VarChar),
                    new SqlParameter("@ManufacturerID",SqlDbType.VarChar),
                    new SqlParameter("@IsWarning",SqlDbType.Bit),
                    new SqlParameter("@MaxAlarmTime",SqlDbType.Int),
                    new SqlParameter("@MinAlarmTime",SqlDbType.Int),
                    new SqlParameter("@Comments",SqlDbType.VarChar),
                    new SqlParameter("@MaxValue",SqlDbType.Decimal),
                    new SqlParameter("@MinValue",SqlDbType.Decimal),
                    };

                parameters[0].Value = (Object)Model.SensorID ?? DBNull.Value;
                parameters[1].Value = (Object)Model.Code ?? DBNull.Value;
                parameters[2].Value = (Object)Model.Name ?? DBNull.Value;
                parameters[3].Value = (Object)Model.Status ?? DBNull.Value;
                parameters[4].Value = (Object)Model.EnabledDate ?? DBNull.Value;
                parameters[5].Value = (Object)Model.FailureDate ?? DBNull.Value;
                parameters[6].Value = (Object)Model.Brand ?? DBNull.Value;
                parameters[7].Value = (Object)Model.Type ?? DBNull.Value;
                parameters[8].Value = (Object)Model.ManufacturerID ?? DBNull.Value;
                parameters[9].Value = (Object)Model.IsWarning ?? DBNull.Value;
                parameters[10].Value = (Object)Model.MaxAlarmTime ?? DBNull.Value;
                parameters[11].Value = (Object)Model.MinAlarmTime ?? DBNull.Value;
                parameters[12].Value = (Object)Model.Comments ?? DBNull.Value;
                parameters[13].Value = (Object)Model.MaxValue ?? DBNull.Value;
                parameters[14].Value = (Object)Model.MinValue ?? DBNull.Value;

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
        /// SAM 2017��5��23��12:04:36
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="Model"></param>
        /// <returns></returns>
        public static bool update(string userId, IOT_Sensor Model)
        {
            try
            {
                string sql = String.Format(@"update[IOT_Sensor] set {0},
                [Status]=@Status,[EnabledDate]=@EnabledDate,[FailureDate]=@FailureDate,[Brand]=@Brand,[Type]=@Type,
                [ManufacturerID]=@ManufacturerID,[IsWarning]=@IsWarning,[MaxAlarmTime]=@MaxAlarmTime,[MinAlarmTime]=@MinAlarmTime
                where [SensorID]=@SensorID", UniversalService.getUpdateUTC(userId));
                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@SensorID",SqlDbType.VarChar),
                    new SqlParameter("@Status",SqlDbType.VarChar),
                    new SqlParameter("@EnabledDate",SqlDbType.DateTime),
                    new SqlParameter("@FailureDate",SqlDbType.DateTime),
                    new SqlParameter("@Brand",SqlDbType.VarChar),
                    new SqlParameter("@Type",SqlDbType.VarChar),
                    new SqlParameter("@ManufacturerID",SqlDbType.VarChar),
                    new SqlParameter("@IsWarning",SqlDbType.Bit),
                    new SqlParameter("@MaxAlarmTime",SqlDbType.Int),
                    new SqlParameter("@MinAlarmTime",SqlDbType.Int),
                    };

                parameters[0].Value = Model.SensorID;
                parameters[1].Value = (Object)Model.Status ?? DBNull.Value;
                parameters[2].Value = (Object)Model.EnabledDate ?? DBNull.Value;
                parameters[3].Value = (Object)Model.FailureDate ?? DBNull.Value;
                parameters[4].Value = (Object)Model.Brand ?? DBNull.Value;
                parameters[5].Value = (Object)Model.Type ?? DBNull.Value;
                parameters[6].Value = (Object)Model.ManufacturerID ?? DBNull.Value;
                parameters[7].Value = (Object)Model.IsWarning ?? DBNull.Value;
                parameters[8].Value = (Object)Model.MaxAlarmTime ?? DBNull.Value;
                parameters[9].Value = (Object)Model.MinAlarmTime ?? DBNull.Value;

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
        /// SAM 2017��5��23��12:04:49
        /// </summary>
        /// <param name="SensorID"></param>
        /// <returns></returns>
        public static IOT_Sensor get(string SensorID)
        {
            string sql = string.Format(@"select Top 1 * from [IOT_Sensor] where [SensorID] = '{0}'  and [SystemID] = '{1}' ", SensorID, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return null;
            else
                return ToEntity(dt.Rows[0]);
        }

        /// <summary>
        /// ������ˮ�Ż�ȡ��һʵ��
        /// SAM 2017��5��23��12:05:05
        /// </summary>
        /// <param name="Code">����</param>
        /// <returns></returns>
        public static IOT_Sensor getByCode(string Code)
        {
            string sql = string.Format(@"select Top 1 * from [IOT_Sensor] where [Code] = '{0}'  and [SystemID] = '{1}' and [Status] ='{1}0201213000001' ", Code, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return null;
            else
                return ToEntity(dt.Rows[0]);
        }

        /// <summary>
        /// �жϴ����Ƿ��ظ�
        /// SAM 2017��5��23��12:06:26
        /// </summary>
        /// <param name="Code">����</param>
        /// <param name="SensorID"></param>
        /// <returns></returns>
        public static bool CheckCode(string Code, string SensorID)
        {
            string sql = String.Format(@"select Top 1 * from [IOT_Sensor] where [SystemID]='{0}' and [Status] <> '{0}0201213000003' ", Framework.SystemID);

            /*�ȶ���Code��Ĭ�ϸ�DbNull,��������Ҳ���*/
            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@Code",SqlDbType.VarChar),
            };
            parameters[0].Value = DBNull.Value;

            /*��ΪCode��ͨ���ֶ�����ģ�������Ҫ�ò�������ʽȥƴSQL*/
            if (!string.IsNullOrWhiteSpace(Code))
            {
                sql = sql + string.Format(@" and [Code] =@Code ");
                parameters[0].Value = Code;
            }

            /*SensorID��������ˮ�ţ������ڸ���ʱ���ų����Լ�*/
            if (!string.IsNullOrWhiteSpace(SensorID))
                sql = sql + string.Format(@" and [SensorID] <> '{0}' ", SensorID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text, parameters);

            if (dt.Rows.Count == 0)
                return false;
            else
                return true;
        }

        /// <summary>
        /// ��֪���б�
        /// SAM 2017��5��23��12:12:05
        /// </summary>
        /// <param name="Code">����</param>
        /// <param name="Status">״̬</param>
        /// <param name="page">ҳ��</param>
        /// <param name="rows">����</param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> lot00001GetList(string Code, string Status, int page, int rows, ref int count)
        {
            string select = string.Format(@"select A.SensorID,A.Code,A.Name,A.Status,A.MaxValue,A.MinValue,
            A.EnabledDate,A.FailureDate,A.Brand,A.Type,A.ManufacturerID,D.Code as ManufacturerCode,D.Name as ManufacturerName,
            A.IsWarning,A.MaxAlarmTime,A.MinAlarmTime,A.Comments,          
            B.UserName as Creator,A.CreateLocalTime as CreateTime,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else C.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ");

            string sql = string.Format(@"  from [IOT_Sensor] A 
            left join [SYS_MESUsers] B on A.[Creator] = B.[MESUserID]
            left join [SYS_MESUsers] C on A.[Modifier] = C.[MESUserID]
            left join [SYS_Manufacturers] D on A.[ManufacturerID] = D.[ManufacturerID]
            where A.[SystemID]='{0}' and A.[Status] <> '{0}0201213000003' ", Framework.SystemID);

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

            count = UniversalService.getCount(sql, Parcount);

            String orderby = " A.[Status],A.[Code] ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, parameters, page, rows);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// ��֪���ĵ�������
        /// SAM 2017��5��23��12:17:23
        /// </summary>
        /// <param name="Code">����</param>
        /// <param name="Status">״̬</param>
        /// <returns></returns>
        public static DataTable Iot00001Export(string Code, string Status)
        {
            string select = string.Format(@"select ROW_NUMBER() OVER (ORDER BY A.[Status],A.[Code]),
            A.Code,A.Name,A.Comments,E.Name as Status,
            A.EnabledDate,A.FailureDate,A.Brand,A.Type,D.Code as ManufacturerCode,D.Name as ManufacturerName,
            A.MaxValue,A.MinValue,
            (CASE WHEN A.IsWarning=1 THEN '��' ELSE '��' END),A.MaxAlarmTime,A.MinAlarmTime,   
            B.UserName as Creator,A.CreateLocalTime as CreateTime,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else C.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ");
           
             string sql = string.Format(@"  from [IOT_Sensor] A 
            left join [SYS_MESUsers] B on A.[Creator] = B.[MESUserID]
            left join [SYS_MESUsers] C on A.[Modifier] = C.[MESUserID]
            left join [SYS_Manufacturers] D on A.[ManufacturerID] = D.[ManufacturerID]
            left join[SYS_Parameters] E on A.[Status] = E.[ParameterID]
            where A.[SystemID]='{0}' and A.[Status] <> '{0}0201213000003' ", Framework.SystemID);

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

            string orderBy = "order By A.[Status],A.[Code] ";

            return SQLHelper.ExecuteDataTable(select + sql + orderBy, CommandType.Text, parameters);
        }

        /// <summary>
        /// ��֪���ĵ���
        /// SAM 2017��5��24��15:09:09
        /// </summary>
        /// <param name="Code">����</param>
        /// <param name="page">ҳ��</param>
        /// <param name="rows">����</param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> GetSensorList(string Code, int page, int rows, ref int count)
        {
            string select = string.Format(@"select A.SensorID,A.Code,A.Name,
            A.EnabledDate,A.FailureDate,A.Brand,A.Type,A.ManufacturerID,D.Code as ManufacturerCode,D.Name as ManufacturerName,
            A.IsWarning,A.MaxAlarmTime,A.MinAlarmTime,A.Comments,E.Name as Status,      
            B.UserName as Creator,A.CreateLocalTime as CreateTime,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else C.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ");

            string sql = string.Format(@"  from [IOT_Sensor] A 
            left join [SYS_MESUsers] B on A.[Creator] = B.[MESUserID]
            left join [SYS_MESUsers] C on A.[Modifier] = C.[MESUserID]
            left join [SYS_Manufacturers] D on A.[ManufacturerID] = D.[ManufacturerID]
            left join [SYS_Parameters] E on A.[Status] =E.[ParameterID]
            where A.[SystemID]='{0}' and A.[Status] = '{0}0201213000001' ", Framework.SystemID);

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

            String orderby = " A.[Status],A.[Code] ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, parameters, page, rows);

            return ToHashtableList(dt);
        }
    }
}

