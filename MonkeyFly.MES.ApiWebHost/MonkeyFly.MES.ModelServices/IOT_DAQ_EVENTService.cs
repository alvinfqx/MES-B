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
    public class IOT_DAQ_EVENTService : SuperModel<IOT_DAQ_EVENT>
    {

        public static bool insert(string userId, IOT_DAQ_EVENT Model)
        {
            try
            {
                string sql = string.Format(@"insert[IOT_DAQ_EVENT]([PlantID],[CtrlID],
                    [EventID],[MachID],[DeviceID],
                    [MeasureTime],[CtrlDesc],[EventDesc],
                    [DeviceDesc],[SystemID]) values
                     (@PlantID,@CtrlID,
                    @EventID,'{0}','{1}','{2}','{0}','{1}','{2}','{3}')", userId, DateTime.UtcNow, DateTime.Now, Framework.SystemID);

                SqlParameter[] parameters = new SqlParameter[]{
                                        new SqlParameter("@PlantID",SqlDbType.NVarChar),
                                        new SqlParameter("@CtrlID",SqlDbType.NVarChar),
                                        new SqlParameter("@EventID",SqlDbType.NVarChar),
                                    };

                parameters[0].Value = (Object)Model.PlantID ?? DBNull.Value;
                parameters[1].Value = (Object)Model.CtrlID ?? DBNull.Value;
                parameters[2].Value = (Object)Model.EventID ?? DBNull.Value;

                return SQLHelper.ExecuteNonQuery(sql, CommandType.Text, parameters) > 0;
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return false;
            }
        }

        public static bool update(string userId, IOT_DAQ_EVENT Model)
        {
            try
            {
                string sql = String.Format(@"update[IOT_DAQ_EVENT] set {0},
                [CtrlID]=@CtrlID,[EventID]=@EventID,[MachID]=@MachID,
                [DeviceID]=@DeviceID where [PlantID]=@PlantID", UniversalService.getUpdateUTC(userId));
                SqlParameter[] parameters = new SqlParameter[]{
                                    new SqlParameter("@PlantID",SqlDbType.VarChar),
                                    new SqlParameter("@CtrlID",SqlDbType.NVarChar),
                                    new SqlParameter("@EventID",SqlDbType.NVarChar),
                                    new SqlParameter("@MachID",SqlDbType.NVarChar),
                                    new SqlParameter("@DeviceID",SqlDbType.NVarChar),
                                    };

                parameters[0].Value = Model.PlantID;
                parameters[1].Value = (Object)Model.CtrlID ?? DBNull.Value;
                parameters[2].Value = (Object)Model.EventID ?? DBNull.Value;
                parameters[3].Value = (Object)Model.MachID ?? DBNull.Value;
                parameters[4].Value = (Object)Model.DeviceID ?? DBNull.Value;

                return SQLHelper.ExecuteNonQuery(sql, CommandType.Text, parameters) > 0;
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return false;
            }
        }

        public static IOT_DAQ_EVENT get(string PlantID)
        {
            string sql = string.Format(@"select Top 1 * from [IOT_DAQ_EVENT] where [PlantID] = '{0}'  and [SystemID] = '{1}' ", PlantID, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return null;
            else
                return ToEntity(dt.Rows[0]);
        }

        public static bool delete(string PlantID)
        {
            try
            {
                string sql = string.Format(@"delete from [IOT_DAQ_EVENT] where [PlantID] = '{0}'  and [SystemID] = '{1}' ", PlantID, Framework.SystemID);

                return SQLHelper.ExecuteNonQuery(sql, CommandType.Text) > 0;
            }

            catch (Exception ex)
            {

                DataLogerService.writeerrlog(ex);
                return false;
            }
        }

        /// <summary>
        /// Iot00003设备事件按钮
        /// Mouse 2017年9月13日16:50:03
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="EquipmentID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public static IList<Hashtable> Iot00003GetDaqEvent(string EquipmentID, int page, int rows, ref int count)
        {
            DateTime TimeEnd = DateTime.Now;

            DateTime TimeStar = DateTime.Now.AddDays(-1);

            string select = string.Format(@"select A.[RecNo],A.[MeasureTime],A.[EventID],A.[EventDesc],A.[DeviceDesc]");
            string sql = string.Format(@" from [IOT_DAQ_EVENT] A 
                        left join [EMS_Equipment] B on '{0}'=B.[EquipmentID]
                        where A.[MachID]=B.[DAQMachID] and A.[MeasureTime] between '{1}' and '{2}'", EquipmentID, TimeStar, TimeEnd);

            count = UniversalService.getCount(sql, null);

            string orderby = "A.MeasureTime Desc";

            DataTable dt = UniversalService.getTable(select, sql, orderby, null, page, rows);

            return ToHashtableList(dt);
        }
    }
}

