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
    public class IOT_DAQ_EVENT_VALUEService : SuperModel<IOT_DAQ_EVENT_VALUE>
    {

        public static bool insert(string userId, IOT_DAQ_EVENT_VALUE Model)
        {
            try
            {
                string sql = string.Format(@"insert[IOT_DAQ_EVENT_VALUE]([RecNo],[Sensor],[Measurement],[SensorDesc]) values
                     (@RecNo,@Sensor,
                    @Measurement,@SensorDesc,'{0}','{1}','{2}','{0}','{1}','{2}','{3}')", userId, DateTime.UtcNow, DateTime.Now, Framework.SystemID);

                SqlParameter[] parameters = new SqlParameter[]{
                                        new SqlParameter("@RecNo",SqlDbType.NVarChar),
                                        new SqlParameter("@Sensor",SqlDbType.NVarChar),
                                        new SqlParameter("@Measurement",SqlDbType.NVarChar),
                                        new SqlParameter("@SensorDesc",SqlDbType.NVarChar),
                                    };

                parameters[0].Value = (Object)Model.RecNo ?? DBNull.Value;
                parameters[1].Value = (Object)Model.Sensor ?? DBNull.Value;
                parameters[2].Value = (Object)Model.Measurement ?? DBNull.Value;
                parameters[3].Value = (Object)Model.SensorDesc ?? DBNull.Value;
                return SQLHelper.ExecuteNonQuery(sql, CommandType.Text, parameters) > 0;
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return false;
            }
        }

        public static bool update(string userId, IOT_DAQ_EVENT_VALUE Model)
        {
            try
            {
                string sql = String.Format(@"update[IOT_DAQ_EVENT_VALUE] set {0},
                [RecNo]=@RecNo,[Sensor]=@Sensor,[Measurement]=@Measurement,
                [SensorDesc]=@SensorDesc where [RecNo]=@RecNo", UniversalService.getUpdateUTC(userId));
                SqlParameter[] parameters = new SqlParameter[]{
                                    new SqlParameter("@RecNo",SqlDbType.VarChar),
                                    new SqlParameter("@Sensor",SqlDbType.NVarChar),
                                    new SqlParameter("@Measurement",SqlDbType.NVarChar),
                                    new SqlParameter("@MachID",SqlDbType.NVarChar),
                                    };

                parameters[0].Value = Model.RecNo;
                parameters[1].Value = (Object)Model.Sensor ?? DBNull.Value;
                parameters[2].Value = (Object)Model.Measurement ?? DBNull.Value;
                parameters[3].Value = (Object)Model.SensorDesc ?? DBNull.Value;

                return SQLHelper.ExecuteNonQuery(sql, CommandType.Text, parameters) > 0;
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return false;
            }
        }

        public static IOT_DAQ_EVENT_VALUE get(string RecNo)
        {
            string sql = string.Format(@"select Top 1 * from [IOT_DAQ_EVENT_VALUE] where [RecNo] = '{0}'  and [SystemID] = '{1}' ", RecNo, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return null;
            else
                return ToEntity(dt.Rows[0]);
        }

        public static bool delete(string RecNo)
        {
            try
            {
                string sql = string.Format(@"delete from [IOT_DAQ_EVENT_VALUE] where [RecNo] = '{0}'  and [SystemID] = '{1}' ", RecNo, Framework.SystemID);

                return SQLHelper.ExecuteNonQuery(sql, CommandType.Text) > 0;
            }

            catch (Exception ex)
            {

                DataLogerService.writeerrlog(ex);
                return false;
            }
        }
    }
}
