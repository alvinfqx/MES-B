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
    public class IOT_DAQ_VALUEService : SuperModel<IOT_DAQ_VALUE>
    {

        public static bool insert(string userId, IOT_DAQ_VALUE Model)
        {
            try
            {
                string sql = string.Format(@"insert[IOT_DAQ_VALUE]([PlantID],[CtrlID],
                [EventID],[MachID],[DeviceID],
                [MeasureTime],[Sensor],[Measurement],
                [CtrlDesc],[EventDesc],[DeviceDesc],
                [SensorDesc],[SystemID]) values
                 (@PlantID,@CtrlID,
                @EventID,@MachID,@DeviceID,
                @MeasureTime,'{0}','{1}','{2}','{0}','{1}','{2}','{3}')", userId,DateTime.UtcNow,DateTime.Now,Framework.SystemID); 

                 SqlParameter[] parameters = new SqlParameter[]{
					                new SqlParameter("@PlantID",SqlDbType.NVarChar),
					                new SqlParameter("@CtrlID",SqlDbType.NVarChar),
					                new SqlParameter("@EventID",SqlDbType.NVarChar),
					                new SqlParameter("@MachID",SqlDbType.NVarChar),
					                new SqlParameter("@DeviceID",SqlDbType.NVarChar),
					                new SqlParameter("@MeasureTime",SqlDbType.DateTime),
					                };

                parameters[0].Value = (Object)Model.PlantID ?? DBNull.Value;
                parameters[1].Value = (Object)Model.CtrlID ?? DBNull.Value;
                parameters[2].Value = (Object)Model.EventID ?? DBNull.Value;
                parameters[3].Value = (Object)Model.MachID ?? DBNull.Value;
                parameters[4].Value = (Object)Model.DeviceID ?? DBNull.Value;
                parameters[5].Value = (Object)Model.MeasureTime ?? DBNull.Value;

                return SQLHelper.ExecuteNonQuery(sql, CommandType.Text, parameters) > 0;
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return false;
            }
        }

        public static bool update(string userId, IOT_DAQ_VALUE Model)
        {
            try
            {
                string sql = String.Format(@"update[IOT_DAQ_VALUE] set {0},
                [CtrlID]=@CtrlID,[EventID]=@EventID,[MachID]=@MachID,
                [DeviceID]=@DeviceID,[MeasureTime]=@MeasureTime,[Sensor]=@Sensor,[Measurement]=@Measurement where [PlantID]=@PlantID", UniversalService.getUpdateUTC(userId));
                 SqlParameter[] parameters = new SqlParameter[]{
					                new SqlParameter("@PlantID",SqlDbType.VarChar),
					                new SqlParameter("@CtrlID",SqlDbType.NVarChar),
					                new SqlParameter("@EventID",SqlDbType.NVarChar),
					                new SqlParameter("@MachID",SqlDbType.NVarChar),
					                new SqlParameter("@DeviceID",SqlDbType.NVarChar),
					                new SqlParameter("@MeasureTime",SqlDbType.DateTime),
					                new SqlParameter("@Sensor",SqlDbType.NVarChar),
					                new SqlParameter("@Measurement",SqlDbType.Decimal),
					                };

                parameters[0].Value =Model.PlantID;
                parameters[1].Value = (Object)Model.CtrlID ?? DBNull.Value;
                parameters[2].Value = (Object)Model.EventID ?? DBNull.Value;
                parameters[3].Value = (Object)Model.MachID ?? DBNull.Value;
                parameters[4].Value = (Object)Model.DeviceID ?? DBNull.Value;
                parameters[5].Value = (Object)Model.MeasureTime ?? DBNull.Value;
                parameters[6].Value = (Object)Model.Sensor ?? DBNull.Value;
                parameters[7].Value = (Object)Model.Measurement ?? DBNull.Value;

                return SQLHelper.ExecuteNonQuery(sql, CommandType.Text, parameters) > 0;
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return false;
            }
        }

        public static IOT_DAQ_VALUE get(string PlantID)
        {
            string sql = string.Format(@"select Top 1 * from [IOT_DAQ_VALUE] where [PlantID] = '{0}'  and [SystemID] = '{1}' ", PlantID, Framework.SystemID);

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
                string sql = string.Format(@"delete from [IOT_DAQ_VALUE] where [PlantID] = '{0}'  and [SystemID] = '{1}' ", PlantID, Framework.SystemID);

                return SQLHelper.ExecuteNonQuery(sql, CommandType.Text) > 0;
            }

            catch (Exception ex)
            {

                 DataLogerService.writeerrlog(ex);
                 return false;
            }
        }

        /// <summary>
        /// Iot00003设备监控资料根据设备流水号设备项目代号
        /// Mouse 2017年9月14日10:27:45
        /// </summary>
        /// <param name="EquipmentID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static Hashtable Iot00003GetDaqValue(string EquipmentID,string Sensor)
        {
            string select = string.Format(@"select A.Sensor,A.SensorDesc,A.Measurement,C.MaxValue,C.MinValue,C.MaxAlarmValue,C.MinAlarmValue,A.MeasureTime ");
            string sql = string.Format(@" from [IOT_DAQ_VALUE] A
                        left join [EMS_Equipment] B on '{0}'=B.[EquipmentID]
                        left join [EMS_EquipmentProject] C on '{0}'=C.[EquipmentID]
                        left join [SYS_Projects] D on C.ProjectID=D.ProjectID
                        where A.[MachID]=B.[DAQMachID] and D.Code='{1}' and A.Sensor='{1}' ", EquipmentID,Sensor);
            string orderby = "order by A.MeasureTime";
            string sqll = string.Format(@"select TOP 1 * from ({0}{1}) A order by A.MeasureTime Desc",select,sql,orderby);
            DataTable dt = SQLHelper.ExecuteDataTable(sqll, CommandType.Text);
            if (dt.Rows.Count != 0)
            {
                return ToHashtableList(dt)[0];//只会返回一条
            }
            else
            {
                return null;//查询数据为空
            }
        }


        /// <summary>
        /// Iot00003DAQ现值趋势图 一个时间区间内的数据
        /// Mouse 2017年9月14日11:09:36 时间与现值
        /// </summary>
        /// <param name="EquipmentID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static Hashtable Iot00003GetMeasurementQSTTime(string EquipmentID,DateTime timestar, DateTime timeend,string Sensor)
        {
            string select = string.Format(@"select A.Measurement as y,A.MeasureTime as x");
            string sql = string.Format(@" from [IOT_DAQ_VALUE] A
                        left join [EMS_Equipment] B on '{0}'=B.[EquipmentID]
                        left join [EMS_EquipmentProject] C on '{0}'=C.[EquipmentID]
                        left join SYS_Projects D on C.ProjectID=D.ProjectID
                        where A.[MachID]=B.[DAQMachID] and A.[MeasureTime] between '{1}' and '{2}' and D.Code='{3}' ", EquipmentID,timestar,timeend, Sensor);
            string orderby = " order by A.MeasureTime";
            DataTable dt = SQLHelper.ExecuteDataTable(select+sql+ orderby,CommandType.Text);

            if (dt.Rows.Count != 0)
            {
                return ToHashtableList(dt)[0];//有多条数据时也只返回第一条数据
            }
            else
            {
                return null;//查询数据为空
            }
        }
        /// <summary>
        /// Iot00003DAQ现值趋势图 一个时间区间内的数据
        /// Mouse 2017年9月14日11:09:36
        /// </summary>
        /// <param name="EquipmentID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static Hashtable Iot00003GetMeasurementQSTValue(string EquipmentID, DateTime timestar, DateTime timeend, string Sensor)
        {
            string select = string.Format(@"select C.MaxValue,C.MinValue");
            string sql = string.Format(@" from [IOT_DAQ_VALUE] A
                        left join [EMS_Equipment] B on '{0}'=B.[EquipmentID]
                        left join [EMS_EquipmentProject] C on '{0}'=C.[EquipmentID]
                        left join SYS_Projects D on C.ProjectID=D.ProjectID
                        where A.[MachID]=B.[DAQMachID] and A.[MeasureTime] between '{1}' and '{2}' and D.Code='{3}' ", EquipmentID, timestar, timeend, Sensor);
            string orderby = " order by A.MeasureTime";
            DataTable dt = SQLHelper.ExecuteDataTable(select + sql + orderby, CommandType.Text);

            if (dt.Rows.Count != 0)
            {
                return ToHashtableList(dt)[0];//有多条数据时也只返回第一条数据
            }
            else
            {
                return null;//查询数据为空
            }
        }

    }
}

