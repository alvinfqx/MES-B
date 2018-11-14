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
    public class EMS_EquipmentService : SuperModel<EMS_Equipment>
    {
        /// <summary>
        /// ����
        /// SAM 2017��5��22��11:55:43
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="Model"></param>
        /// <returns></returns>
        public static bool insert(string userId, EMS_Equipment Model)
        {
            try
            {
                string sql = string.Format(@"insert[EMS_Equipment]([EquipmentID],[Code],[Name],[ResourceCategory],
                [PlantAreaID],[FixedAssets],[PurchaseDate],[ManufacturerID],[Model],[MachineNo],
                [ClassOne],[ClassTwo],[OrganizationID],[Status],[Condition],[ExpireDate],[StdCapacity],
                [MaintenanceTime],[MaintenanceNum],[TotalOutput],[UsedTime],[UsableTime],[CavityMold],
                [UsedTimes],[UsableTimes],[StatisticsFlag],[DescriptionOne],[DescriptionTwo],[DateOne],[DateTwo],[NumOne],[NumTwo],[AbnormalStatus],
                [AttachmentID],[PlantID],[Comments],[DAQMachID],[Modifier],[ModifiedTime],[ModifiedLocalTime],[Creator],[CreateTime],[CreateLocalTime],[SystemID]) values
                (@EquipmentID,@Code,@Name,@ResourceCategory,
                @PlantAreaID,@FixedAssets,@PurchaseDate,@ManufacturerID,@Model,@MachineNo,
                @ClassOne,@ClassTwo,@OrganizationID,@Status,@Condition,@ExpireDate,@StdCapacity,
                @MaintenanceTime,@MaintenanceNum,@TotalOutput,@UsedTime,@UsableTime,@CavityMold,
                @UsedTimes,@UsableTimes,@StatisticsFlag,@DescriptionOne,@DescriptionTwo,@DateOne,@DateTwo,@NumOne,@NumTwo,@AbnormalStatus,
                @AttachmentID,@PlantID,@Comments,@DAQMachID,'{0}','{1}','{2}','{0}','{1}','{2}','{3}')", userId, DateTime.Now, DateTime.Now, Framework.SystemID);

                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@EquipmentID",SqlDbType.VarChar),
                    new SqlParameter("@Code",SqlDbType.NVarChar),
                    new SqlParameter("@Name",SqlDbType.NVarChar),
                    new SqlParameter("@ResourceCategory",SqlDbType.VarChar),
                    new SqlParameter("@PlantAreaID",SqlDbType.VarChar),
                    new SqlParameter("@FixedAssets",SqlDbType.NVarChar),
                    new SqlParameter("@PurchaseDate",SqlDbType.DateTime),
                    new SqlParameter("@ManufacturerID",SqlDbType.VarChar),
                    new SqlParameter("@Model",SqlDbType.NVarChar),
                    new SqlParameter("@MachineNo",SqlDbType.NVarChar),
                    new SqlParameter("@ClassOne",SqlDbType.VarChar),
                    new SqlParameter("@ClassTwo",SqlDbType.VarChar),
                    new SqlParameter("@OrganizationID",SqlDbType.VarChar),
                    new SqlParameter("@Status",SqlDbType.VarChar),
                    new SqlParameter("@Condition",SqlDbType.VarChar),
                    new SqlParameter("@ExpireDate",SqlDbType.DateTime),
                    new SqlParameter("@StdCapacity",SqlDbType.Decimal),
                    new SqlParameter("@MaintenanceTime",SqlDbType.Decimal),
                    new SqlParameter("@MaintenanceNum",SqlDbType.Decimal),
                    new SqlParameter("@TotalOutput",SqlDbType.Decimal),
                    new SqlParameter("@UsedTime",SqlDbType.Decimal),
                    new SqlParameter("@UsableTime",SqlDbType.Decimal),
                    new SqlParameter("@CavityMold",SqlDbType.Decimal),
                    new SqlParameter("@UsedTimes",SqlDbType.Decimal),
                    new SqlParameter("@UsableTimes",SqlDbType.Decimal),
                    new SqlParameter("@StatisticsFlag",SqlDbType.Bit),
                    new SqlParameter("@DescriptionOne",SqlDbType.VarChar),
                    new SqlParameter("@DescriptionTwo",SqlDbType.VarChar),
                    new SqlParameter("@DateOne",SqlDbType.DateTime),
                    new SqlParameter("@DateTwo",SqlDbType.DateTime),
                    new SqlParameter("@NumOne",SqlDbType.Decimal),
                    new SqlParameter("@NumTwo",SqlDbType.Decimal),
                    new SqlParameter("@AttachmentID",SqlDbType.VarChar),
                    new SqlParameter("@Comments",SqlDbType.VarChar),
                    new SqlParameter("@PlantID",SqlDbType.VarChar),
                    new SqlParameter("@AbnormalStatus",SqlDbType.VarChar),
                    new SqlParameter("@DAQMachID",SqlDbType.VarChar),
                    };

                parameters[0].Value = (Object)Model.EquipmentID ?? DBNull.Value;
                parameters[1].Value = (Object)Model.Code ?? DBNull.Value;
                parameters[2].Value = (Object)Model.Name ?? DBNull.Value;
                parameters[3].Value = (Object)Model.ResourceCategory ?? DBNull.Value;
                parameters[4].Value = (Object)Model.PlantAreaID ?? DBNull.Value;
                parameters[5].Value = (Object)Model.FixedAssets ?? DBNull.Value;
                parameters[6].Value = (Object)Model.PurchaseDate ?? DBNull.Value;
                parameters[7].Value = (Object)Model.ManufacturerID ?? DBNull.Value;
                parameters[8].Value = (Object)Model.Model ?? DBNull.Value;
                parameters[9].Value = (Object)Model.MachineNo ?? DBNull.Value;
                parameters[10].Value = (Object)Model.ClassOne ?? DBNull.Value;
                parameters[11].Value = (Object)Model.ClassTwo ?? DBNull.Value;
                parameters[12].Value = (Object)Model.OrganizationID ?? DBNull.Value;
                parameters[13].Value = (Object)Model.Status ?? DBNull.Value;
                parameters[14].Value = (Object)Model.Condition ?? DBNull.Value;
                parameters[15].Value = (Object)Model.ExpireDate ?? DBNull.Value;
                parameters[16].Value = (Object)Model.StdCapacity ?? DBNull.Value;
                parameters[17].Value = (Object)Model.MaintenanceTime ?? DBNull.Value;
                parameters[18].Value = (Object)Model.MaintenanceNum ?? DBNull.Value;
                parameters[19].Value = (Object)Model.TotalOutput ?? DBNull.Value;
                parameters[20].Value = (Object)Model.UsedTime ?? DBNull.Value;
                parameters[21].Value = (Object)Model.UsableTime ?? DBNull.Value;
                parameters[22].Value = (Object)Model.CavityMold ?? DBNull.Value;
                parameters[23].Value = (Object)Model.UsedTimes ?? DBNull.Value;
                parameters[24].Value = (Object)Model.UsableTimes ?? DBNull.Value;
                parameters[25].Value = (Object)Model.StatisticsFlag ?? DBNull.Value;
                parameters[26].Value = (Object)Model.DescriptionOne ?? DBNull.Value;
                parameters[27].Value = (Object)Model.DescriptionTwo ?? DBNull.Value;
                parameters[28].Value = (Object)Model.DateOne ?? DBNull.Value;
                parameters[29].Value = (Object)Model.DateTwo ?? DBNull.Value;
                parameters[30].Value = (Object)Model.NumOne ?? DBNull.Value;
                parameters[31].Value = (Object)Model.NumTwo ?? DBNull.Value;
                parameters[32].Value = (Object)Model.AttachmentID ?? DBNull.Value;
                parameters[33].Value = (Object)Model.Comments ?? DBNull.Value;
                parameters[34].Value = (Object)Model.PlantID ?? DBNull.Value;
                parameters[35].Value = (Object)Model.AbnormalStatus ?? DBNull.Value;
                parameters[36].Value = (Object)Model.DAQMachID ?? DBNull.Value;
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
        /// SAM 2017��5��22��11:55:33
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="Model"></param>
        /// <returns></returns>
        public static bool update(string userId, EMS_Equipment Model)
        {
            try
            {
                string sql = String.Format(@"update[EMS_Equipment] set {0},
                [ResourceCategory]=@ResourceCategory,[PlantAreaID]=@PlantAreaID,[FixedAssets]=@FixedAssets,[PlantID]=@PlantID,
                [PurchaseDate]=@PurchaseDate,[ManufacturerID]=@ManufacturerID,[Model]=@Model,[MachineNo]=@MachineNo,
                [ClassOne]=@ClassOne,[ClassTwo]=@ClassTwo,[OrganizationID]=@OrganizationID,[Status]=@Status,[AbnormalStatus]=@AbnormalStatus,
                [Condition]=@Condition,[ExpireDate]=@ExpireDate,[StdCapacity]=@StdCapacity,[MaintenanceTime]=@MaintenanceTime,
                [MaintenanceNum]=@MaintenanceNum,[UsableTime]=@UsableTime,[CavityMold]=@CavityMold,[UsableTimes]=@UsableTimes,
                [StatisticsFlag]=@StatisticsFlag,[DescriptionOne]=@DescriptionOne,[DescriptionTwo]=@DescriptionTwo,
                [DAQMachID]=@DAQMachID,
                [DateOne]=@DateOne,[DateTwo]=@DateTwo,[NumOne]=@NumOne,[NumTwo]=@NumTwo where [EquipmentID]=@EquipmentID", UniversalService.getUpdateUTC(userId));
                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@EquipmentID",SqlDbType.VarChar),
                    new SqlParameter("@ResourceCategory",SqlDbType.VarChar),
                    new SqlParameter("@PlantAreaID",SqlDbType.VarChar),
                    new SqlParameter("@FixedAssets",SqlDbType.NVarChar),
                    new SqlParameter("@PurchaseDate",SqlDbType.DateTime),
                    new SqlParameter("@ManufacturerID",SqlDbType.VarChar),
                    new SqlParameter("@Model",SqlDbType.NVarChar),
                    new SqlParameter("@MachineNo",SqlDbType.NVarChar),
                    new SqlParameter("@ClassOne",SqlDbType.VarChar),
                    new SqlParameter("@ClassTwo",SqlDbType.VarChar),
                    new SqlParameter("@OrganizationID",SqlDbType.VarChar),
                    new SqlParameter("@Status",SqlDbType.VarChar),
                    new SqlParameter("@Condition",SqlDbType.VarChar),
                    new SqlParameter("@ExpireDate",SqlDbType.DateTime),
                    new SqlParameter("@StdCapacity",SqlDbType.Decimal),
                    new SqlParameter("@MaintenanceTime",SqlDbType.Decimal),
                    new SqlParameter("@MaintenanceNum",SqlDbType.Decimal),
                    new SqlParameter("@UsableTime",SqlDbType.Decimal),
                    new SqlParameter("@CavityMold",SqlDbType.Decimal),
                    new SqlParameter("@UsableTimes",SqlDbType.Decimal),
                    new SqlParameter("@StatisticsFlag",SqlDbType.Bit),
                    new SqlParameter("@DescriptionOne",SqlDbType.VarChar),
                    new SqlParameter("@DescriptionTwo",SqlDbType.VarChar),
                    new SqlParameter("@DateOne",SqlDbType.DateTime),
                    new SqlParameter("@DateTwo",SqlDbType.DateTime),
                    new SqlParameter("@NumOne",SqlDbType.Decimal),
                    new SqlParameter("@NumTwo",SqlDbType.Decimal),
                   new SqlParameter("@PlantID",SqlDbType.VarChar),
                   new SqlParameter("@AbnormalStatus",SqlDbType.VarChar),
                   new SqlParameter("@DAQMachID",SqlDbType.VarChar),
                    };


                parameters[0].Value = Model.EquipmentID;
                parameters[1].Value = (Object)Model.ResourceCategory ?? DBNull.Value;
                parameters[2].Value = (Object)Model.PlantAreaID ?? DBNull.Value;
                parameters[3].Value = (Object)Model.FixedAssets ?? DBNull.Value;
                parameters[4].Value = (Object)Model.PurchaseDate ?? DBNull.Value;
                parameters[5].Value = (Object)Model.ManufacturerID ?? DBNull.Value;
                parameters[6].Value = (Object)Model.Model ?? DBNull.Value;
                parameters[7].Value = (Object)Model.MachineNo ?? DBNull.Value;
                parameters[8].Value = (Object)Model.ClassOne ?? DBNull.Value;
                parameters[9].Value = (Object)Model.ClassTwo ?? DBNull.Value;
                parameters[10].Value = (Object)Model.OrganizationID ?? DBNull.Value;
                parameters[11].Value = (Object)Model.Status ?? DBNull.Value;
                parameters[12].Value = (Object)Model.Condition ?? DBNull.Value;
                parameters[13].Value = (Object)Model.ExpireDate ?? DBNull.Value;
                parameters[14].Value = (Object)Model.StdCapacity ?? DBNull.Value;
                parameters[15].Value = (Object)Model.MaintenanceTime ?? DBNull.Value;
                parameters[16].Value = (Object)Model.MaintenanceNum ?? DBNull.Value;
                parameters[17].Value = (Object)Model.UsableTime ?? DBNull.Value;
                parameters[18].Value = (Object)Model.CavityMold ?? DBNull.Value;
                parameters[19].Value = (Object)Model.UsableTimes ?? DBNull.Value;
                parameters[20].Value = (Object)Model.StatisticsFlag ?? DBNull.Value;
                parameters[21].Value = (Object)Model.DescriptionOne ?? DBNull.Value;
                parameters[22].Value = (Object)Model.DescriptionTwo ?? DBNull.Value;
                parameters[23].Value = (Object)Model.DateOne ?? DBNull.Value;
                parameters[24].Value = (Object)Model.DateTwo ?? DBNull.Value;
                parameters[25].Value = (Object)Model.NumOne ?? DBNull.Value;
                parameters[26].Value = (Object)Model.NumTwo ?? DBNull.Value;
                parameters[27].Value = (Object)Model.PlantID ?? DBNull.Value;
                parameters[28].Value = (Object)Model.AbnormalStatus ?? DBNull.Value;
                parameters[29].Value = (Object)Model.DAQMachID ?? DBNull.Value;
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
        /// SAM 2017��5��22��11:55:57
        /// </summary>
        /// <param name="EquipmentID"></param>
        /// <returns></returns>
        public static EMS_Equipment get(string EquipmentID)
        {
            string sql = string.Format(@"select Top 1 * from [EMS_Equipment] where [EquipmentID] = '{0}'  and [SystemID] = '{1}' ", EquipmentID, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return null;
            else
                return ToEntity(dt.Rows[0]);
        }

        /// <summary>
        /// ����������Ƿ�ʹ��
        /// Joint 2017��7��24��18:11:15
        /// </summary>
        /// <param name="ParameterID"></param>
        /// <returns></returns>
        public static EMS_Equipment checkInf00009(string ParameterID)
        {
            string sql = string.Format(@"select Top 1 * from [EMS_Equipment] where ([ClassOne] = '{0}' or [ClassTwo]= '{0}') and [SystemID] = '{1}' ", ParameterID, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return null;
            else
                return ToEntity(dt.Rows[0]);
        }
        /// <summary>
        /// �жϴ����Ƿ��ظ�
        /// SAM 2017��5��22��14:43:50
        /// </summary>
        /// <param name="Code">����</param>
        /// <param name="EquipmentID"></param>
        /// <returns></returns>
        public static bool CheckCode(string Code, string EquipmentID)
        {
            string sql = String.Format(@"select Top 1 * from [EMS_Equipment] where [SystemID]='{0}' and [Status] <> '{0}0201213000003' ", Framework.SystemID);

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

            /*EquipmentID��������ˮ�ţ������ڸ���ʱ���ų����Լ�*/
            if (!string.IsNullOrWhiteSpace(EquipmentID))
                sql = sql + string.Format(@" and [EquipmentID] <> '{0}' ", EquipmentID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text, parameters);

            if (dt.Rows.Count == 0)
                return false;
            else
                return true;
        }

        /// <summary>
        /// ���ݴ��Ż�ȡ�豸
        /// SAM 2017��5��23��11:17:17
        /// </summary>
        /// <param name="Code">����</param>
        /// <returns></returns>
        public static EMS_Equipment getByCode(string Code)
        {
            string sql = string.Format(@"select Top 1 * from [EMS_Equipment] where [Code] = '{0}'  and [SystemID] = '{1}'  and [Status]='{1}0201213000001' ", Code, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return null;
            else
                return ToEntity(dt.Rows[0]);
        }



        /// <summary>
        /// �豸�����б�
        /// SAM 2017��5��22��14:10:59
        /// </summary>
        /// <param name="Code">����</param>
        /// <param name="Status">״̬</param>
        /// <param name="page">ҳ��</param>
        /// <param name="rows">����</param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> Ems00001GetList(string Code, string Status, int page, int rows, ref int count)
        {
            string select = string.Format(@"select A.EquipmentID,A.Code,A.Name,A.ResourceCategory,D.Code as ResourceCategoryCode,A.AbnormalStatus,
            A.Status,A.Condition,A.PlantAreaID,E.Code as PlantAreaCode,E.Name as PlantAreaName,A.PlantID,
            (Select [Code] from [SYS_Organization] where OrganizationID = E.PlantID) as PlantCode,
            (Select [Name] from [SYS_Organization] where OrganizationID = E.PlantID) as PlantName,
            A.FixedAssets,A.PurchaseDate,A.Model,A.MachineNo,A.ManufacturerID,F.Code as ManufacturerCode,F.Name as ManufacturerName,
            A.ClassOne,A.ClassTwo,G.Code as ClassOneCode,H.Code as ClassTwoCode,A.OrganizationID,I.Code as OrganizationCode,I.Name as OrganizationName,
            A.ExpireDate,A.StdCapacity,A.MaintenanceTime,A.MaintenanceNum,
            A.TotalOutput,A.UsedTime,A.UsableTime,A.CavityMold,A.UsedTimes,A.UsableTimes,A.StatisticsFlag,
            A.DescriptionOne,A.DescriptionTwo,A.DateOne,A.DateTwo,A.NumOne,A.NumTwo,A.AttachmentID,A.Comments,          
            B.UserName as Creator,A.CreateLocalTime as CreateTime,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else C.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ");

            string sql = string.Format(@"  from [EMS_Equipment] A 
            left join [SYS_MESUsers] B on A.[Creator] = B.[MESUserID]
            left join [SYS_MESUsers] C on A.[Modifier] = C.[MESUserID]
            left join [SYS_Parameters] D on A.[ResourceCategory] = D.[ParameterID]
            left join [SYS_PlantArea] E on A.[PlantAreaID] = E.[PlantAreaID]
            left join [SYS_Manufacturers] F on A.[ManufacturerID] = F.[ManufacturerID]
            left join [SYS_Parameters] G on A.[ClassOne] = G.[ParameterID]
            left join [SYS_Parameters] H on A.[ClassTwo] = H.[ParameterID]
            left join [SYS_Organization] I on A.[OrganizationID] = I.[OrganizationID]
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
        /// �豸�ĵ�����Ϣ
        /// SAM 2017��5��22��17:32:33
        /// </summary>
        /// <param name="Code">����</param>
        /// <param name="Status">״̬</param>
        /// <returns></returns>
        public static DataTable Ems00001Export(string Code, string Status)
        {
            string select = string.Format(@"select ROW_NUMBER() OVER (ORDER BY A.[Status],A.[Code]),
            A.Code,A.Name,D.Code as ResourceCategoryCode,
            J.Name as Status,K.Name as Condition,
            (CASE WHEN A.AbnormalStatus=1 THEN '�쳣' ELSE '����' END),
            A.FixedAssets,E.Code as PlantAreaCode,E.Name as PlantAreaName,
            (Select [Code] from [SYS_Organization] where OrganizationID = E.PlantID) as PlantCode,
            (Select [Name] from [SYS_Organization] where OrganizationID = E.PlantID) as PlantName,
            A.Comments,CONVERT(varchar(10), A.PurchaseDate, 20),F.Code as ManufacturerCode,F.Name as ManufacturerName,
            CONVERT(varchar(10), A.ExpireDate, 20),A.Model,A.MachineNo,
            G.Code as ClassOneCode,H.Code as ClassTwoCode,A.StdCapacity,A.TotalOutput,A.UsedTime,A.UsableTime,A.CavityMold,A.UsedTimes,A.UsableTimes,
            (CASE WHEN A.StatisticsFlag=1 THEN '��' ELSE '��' END),
            I.Code as OrganizationCode,I.Name as OrganizationName,A.MaintenanceTime,A.MaintenanceNum,          
            A.DescriptionOne,A.DescriptionTwo,A.DateOne,A.DateTwo,A.NumOne,A.NumTwo,       
            B.UserName as Creator,A.CreateLocalTime as CreateTime,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else C.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ");

            string sql = string.Format(@"  from [EMS_Equipment] A 
            left join [SYS_MESUsers] B on A.[Creator] = B.[MESUserID]
            left join [SYS_MESUsers] C on A.[Modifier] = C.[MESUserID]
            left join [SYS_Parameters] D on A.[ResourceCategory] = D.[ParameterID]
            left join [SYS_PlantArea] E on A.[PlantAreaID] = E.[PlantAreaID]
            left join [SYS_Manufacturers] F on A.[ManufacturerID] = F.[ManufacturerID]
            left join [SYS_Parameters] G on A.[ClassOne] = G.[ParameterID]
            left join [SYS_Parameters] H on A.[ClassTwo] = H.[ParameterID]
            left join [SYS_Organization] I on A.[OrganizationID] = I.[OrganizationID]
            left join [SYS_Parameters] J on A.[Status] = J.[ParameterID]
            left join [SYS_Parameters] K on A.[Condition] = K.[ParameterID]
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
        ///  ��ȡ�������豸�б������豸������������ҳǩ��
        ///  SAM 2017��5��22��22:33:27 
        /// </summary>
        /// <param name="Code">����</param>
        /// <returns></returns>
        public static IList<Hashtable> Ems00001List(string Code)
        {
            string select = string.Format(@"select A.EquipmentID,A.Code,A.Name,A.Comments,A.AttachmentID,     
            B.UserName as Creator,A.CreateLocalTime as CreateTime,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else C.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ");

            string sql = string.Format(@"  from [EMS_Equipment] A 
            left join [SYS_MESUsers] B on A.[Creator] = B.[MESUserID]
            left join [SYS_MESUsers] C on A.[Modifier] = C.[MESUserID]
            where A.[SystemID]='{0}' and A.[Status] = '{0}0201213000001' ", Framework.SystemID);

            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@Code",SqlDbType.VarChar)
            };
            parameters[0].Value = DBNull.Value;

            SqlParameter[] Parcount = new SqlParameter[]{
                new SqlParameter("@Code",SqlDbType.VarChar)
            };

            if (!string.IsNullOrWhiteSpace(Code))
            {
                Code = "%" + Code + "%";
                sql = sql + string.Format(@" and A.[Code] like @Code ");
                parameters[0].Value = Code;
                Parcount[0].Value = Code;
            }

            string orderBy = "order By A.[Status],A.[Code] ";

            DataTable dt = SQLHelper.ExecuteDataTable(select + sql + orderBy, CommandType.Text, parameters);

            return ToHashtableList(dt);
        }

        /// <summary>
        ///  ��ȡ���������ΪM���豸�б�Ems00002��ȡ�б�
        ///  MOUSE 2017��8��1��16:11:07
        /// </summary>
        /// <param name="Code">����</param>
        /// <returns></returns>
        public static IList<Hashtable> Ems00002List(string Code)
        {
            string select = string.Format(@"select A.EquipmentID,A.Code,A.Name,A.Comments,A.AttachmentID,     
            B.UserName as Creator,A.CreateLocalTime as CreateTime,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else C.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ");

            string sql = string.Format(@"  from [EMS_Equipment] A 
            left join [SYS_MESUsers] B on A.[Creator] = B.[MESUserID]
            left join [SYS_MESUsers] C on A.[Modifier] = C.[MESUserID]
            where A.[SystemID]='{0}' and A.[Status] = '{0}0201213000001' and A.[ResourceCategory]= '{0}0201213000048' ", Framework.SystemID);

            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@Code",SqlDbType.VarChar)
            };
            parameters[0].Value = DBNull.Value;

            SqlParameter[] Parcount = new SqlParameter[]{
                new SqlParameter("@Code",SqlDbType.VarChar)
            };

            if (!string.IsNullOrWhiteSpace(Code))
            {
                Code = "%" + Code + "%";
                sql = sql + string.Format(@" and A.[Code] like @Code ");
                parameters[0].Value = Code;
                Parcount[0].Value = Code;
            }

            string orderBy = "order By A.[Status],A.[Code] ";

            DataTable dt = SQLHelper.ExecuteDataTable(select + sql + orderBy, CommandType.Text, parameters);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// �豸�ĵ���
        /// SAM 2017��5��26��14:25:33
        /// </summary>
        /// <param name="StartCode"></param>
        /// <param name="EndCode"></param>
        /// <param name="Status">״̬</param>
        /// <param name="page">ҳ��</param>
        /// <param name="rows">����</param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> GetEquipmentList(string Code, string StartCode, string EndCode, string Category, string Name, int page, int rows, ref int count)
        {
            string select = string.Format(@"select A.EquipmentID,A.Code,A.Name,A.ResourceCategory,D.Name as Status,     
            F.Code as ManufacturerCode,F.Name as ManufacturerName,H.Name as ResourceCategoryName,
            E.Name as PlantAreaName,G.Name as PlantName,A.Comments,
            B.UserName as Creator,A.CreateLocalTime as CreateTime,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else C.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ");

            string sql = string.Format(@"  from [EMS_Equipment] A 
            left join [SYS_MESUsers] B on A.[Creator] = B.[MESUserID]
            left join [SYS_MESUsers] C on A.[Modifier] = C.[MESUserID]
            left join [SYS_Parameters] D on A.[Status] = D.[ParameterID]
            left join [SYS_Manufacturers] F on A.[ManufacturerID] = F.[ManufacturerID]
            left join [SYS_PlantArea] E on A.[PlantAreaID] = E.[PlantAreaID]
            left join [SYS_Organization] G on A.[PlantID] = G.[OrganizationID]
            left join [SYS_Parameters] H on A.[ResourceCategory] = H.[ParameterID]
            where A.[SystemID]='{0}' and A.[Status] = '{0}0201213000001' ", Framework.SystemID);

            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@Code",SqlDbType.VarChar),
                new SqlParameter("@StartCode",SqlDbType.VarChar),
                 new SqlParameter("@EndCode",SqlDbType.VarChar),
                 new SqlParameter("@Name",SqlDbType.VarChar)
            };
            parameters[0].Value = DBNull.Value;
            parameters[1].Value = DBNull.Value;
            parameters[2].Value = DBNull.Value;
            parameters[3].Value = DBNull.Value;

            SqlParameter[] Parcount = new SqlParameter[]{
                 new SqlParameter("@Code",SqlDbType.VarChar),
                new SqlParameter("@StartCode",SqlDbType.VarChar),
                 new SqlParameter("@EndCode",SqlDbType.VarChar),
                 new SqlParameter("@Name",SqlDbType.VarChar)
            };
            Parcount[0].Value = DBNull.Value;
            Parcount[1].Value = DBNull.Value;
            Parcount[2].Value = DBNull.Value;
            Parcount[3].Value = DBNull.Value;

            if (!string.IsNullOrWhiteSpace(Code))
            {
                Code = "%" + Code + "%";
                sql = sql + string.Format(@" and A.[Code] like @Code ");
                parameters[0].Value = Code;
                Parcount[0].Value = Code;
            }

            if (!string.IsNullOrWhiteSpace(StartCode))
            {
                sql = sql + string.Format(@" and A.[Code] >= @StartCode ");
                parameters[1].Value = StartCode;
                Parcount[1].Value = StartCode;
            }

            if (!string.IsNullOrWhiteSpace(EndCode))
            {
                sql = sql + string.Format(@" and A.[Code] <= @EndCode ");
                parameters[2].Value = EndCode;
                Parcount[2].Value = EndCode;
            }

            if (!string.IsNullOrWhiteSpace(Name))
            {
                Name = "%" + Name + "%";
                sql = sql + string.Format(@" and A.[Name] like @Name ");
                parameters[3].Value = Name;
                Parcount[3].Value = Name;
            }


            if (!string.IsNullOrWhiteSpace(Category))
                sql = sql + string.Format(@" and H.[Code] ='{0}' ", Category);


            count = UniversalService.getCount(sql, Parcount);

            string orderby = " A.[Code] ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, parameters, page, rows);

            return ToHashtableList(dt);
        }



        /// <summary>
        /// �������ڹ������嵥�е��豸����
        /// SAM 2017��7��9��16:59:29
        /// </summary>
        /// <param name="MaintenanceOrderID"></param>
        /// <param name="EquipmentMaintenanceListID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> EMSGetEquMaiEquipmentList(string MaintenanceOrderID, string EquipmentMaintenanceListID ,string Code, string Name)
        {
            string select = string.Format(@"select A.EquipmentID,A.Code,A.Name,
            H.Name as ResourceCategory,D.Name as Status,A.Model,
            G.Name as OrganizationName,G.Code as OrganizationCode,
            F.Code as ClassOneCode,F.Name as ClassOneName,
            E.Code as ClassTwoCode,E.Name as ClassTwoName,
            B.Emplno+'-'+B.UserName as Creator,A.CreateLocalTime as CreateTime,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else C.Emplno+'-'+C.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ");

            string sql = string.Format(@"  from [EMS_Equipment] A 
            left join [SYS_MESUsers] B on A.[Creator] = B.[MESUserID]
            left join [SYS_MESUsers] C on A.[Modifier] = C.[MESUserID]
            left join [SYS_Parameters] D on A.[Status] = D.[ParameterID]
            left join [SYS_Parameters] H on A.[ResourceCategory] = H.[ParameterID]
            left join [SYS_Organization] G on A.[OrganizationID] = G.[OrganizationID]
            left join [SYS_Parameters] F on A.[ClassOne] = F.[ParameterID]
            left join [SYS_Parameters] E on A.[ClassTwo] = E.[ParameterID]
            
            where A.[SystemID]='{0}' and A.[Status] = '{0}0201213000001' ", Framework.SystemID);

            sql += string.Format(@"and A.[EquipmentID] not in (select [EquipmentID] from [EMS_MaiOrderEquipment] where [MaintenanceOrderID] = '{0}' and [Status] <> '{1}0201213000003')", MaintenanceOrderID, Framework.SystemID);

            sql += string.Format(@"and A.[EquipmentID] not in (select [DetailID] from [EMS_EquipmentMaintenanceListDetails] where [EquipmentMaintenanceListID] = '{0}' and [Status] ='{1}0201213000001' and [Type]=2)", EquipmentMaintenanceListID,Framework.SystemID);

            if(!string.IsNullOrWhiteSpace(Code))
                sql += string.Format(@"and A.[Code] like '%{0}%'", Code);


            if (!string.IsNullOrWhiteSpace(Name))
                sql += string.Format(@"and A.[Name] like '%{0}%'", Name);

            string orderby = " order by A.[Code] ";

            DataTable dt = SQLHelper.ExecuteDataTable(select+ sql + orderby, CommandType.Text);

            return ToHashtableList(dt);
        }

        /// <summary>
        ///  ���B���:�O���_��-δ�x���Y��
        ///  SAM 2017��7��14��15:12:00
        /// </summary>
        /// <param name="EquipmentMaintenanceListID"></param>
        /// <param name="Code"></param>
        /// <param name="Name"></param>
        /// <returns></returns>
        public static IList<Hashtable> EMS00008ListDeviceAdd(string EquipmentMaintenanceListID, string Code, string Name)
        {
            string select = string.Format(@"select null as EquipmentMaintenanceListDetailID,A.EquipmentID as DetailID,A.Code as EquipmentCode,
                A.Name as EquipmentName,
                (Select [Code] from [SYS_Parameters] where [ParameterID] = A.[ResourceCategory]) as ResourceCategory,
                (Select [Code] from [SYS_Parameters] where [ParameterID] = A.[ClassOne]) as ClassOne,
                (Select [Code] from [SYS_Parameters] where [ParameterID] = A.[ClassTwo]) as ClassTwo,
                (Select [Name] from [SYS_Organization] where [OrganizationID] = A.[OrganizationID]) as OrganizationName,
                A.Comments  ");

            string sql = string.Format(@"  from [EMS_Equipment] A 
            where [EquipmentID] not in 
            (select [DetailID] from [EMS_EquipmentMaintenanceListDetails] where [EquipmentMaintenanceListID] ='{1}' and [Status] = '{0}0201213000001')
            and A.[SystemID]='{0}' and A.[Status] ='{0}0201213000001' ", Framework.SystemID, EquipmentMaintenanceListID);

            if (!string.IsNullOrWhiteSpace(Code))
                sql += string.Format(@"and A.Code like '%{0}%' ", Code);

            if (!string.IsNullOrWhiteSpace(Name))
                sql += string.Format(@"and A.Name like '%{0}%' ", Name);

            string orderBy = "order By A.[Code] ";

            DataTable dt = SQLHelper.ExecuteDataTable(select + sql + orderBy, CommandType.Text, null);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// �깤�λ؈����I-�YԴ���豸����
        /// SAM 2017��7��20��10:42:19
        /// </summary>
        /// <param name="TaskDispatchID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> GetMachineOrManList(string TaskDispatchID, int page, int rows, ref int count)
        {
            string select = string.Format(@"select A.EquipmentID,A.Code as DisplayCode,A.Name as DisplayName,'M' as ResourceClass,
(select Top 1 [ParameterID] from [SYS_Parameters] where Code='M' and [ParameterTypeID]= '{0}0191213000013' and [IsEnable]=1) as ResourceClassID
", Framework.SystemID);

            string sql = string.Format(@"  from [EMS_Equipment] A 
            where A.[SystemID]='{0}' and A.[Status] = '{0}0201213000001'
            and A.[EquipmentID] in (select [EquipmentID] from [SFC_TaskDispatchResource] where 
            [TaskDispatchID] = '{1}' 
            and ResourceClassID = (select Top 1 [ParameterID] from [SYS_Parameters] where Code='M' and [ParameterTypeID]= '{0}0191213000013' and [IsEnable]=1))", Framework.SystemID, TaskDispatchID);

            count = UniversalService.getCount(sql, null);

            string orderby = "A.[Code] ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, null, page, rows);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// �жϲ����Ƿ�Ϊһ���豸�ı��ܲ���
        /// SAM 2017��7��25��11:18:01
        /// </summary>
        /// <param name="OrganizationID"></param>
        /// <returns></returns>
        public static bool CheckOrganization(string OrganizationID)
        {
            string sql = string.Format(@"select * from [EMS_Equipment] where [OrganizationID]='{0}' and [SystemID] = '{1}' and [Status] <> '{1}0201213000003' ", OrganizationID, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return false;
            else
                return true;
        }

        /// <summary>
        /// ��̨�豸��� �豸�б�
        /// Mouse2017��9��6��11:28:06
        /// </summary>
        /// <param name="Code"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> Iot00003GetEquipmentList(string EquipmentID, int page,int rows,ref int count)
        {
            string select = string.Format(@"select A.EquipmentID,B.Name as PlantAreaName,C.Name PlantName,D.Name as ConditionName,D.Description,E.Path");
            string sql = string.Format(@"
                         from [EMS_Equipment] A
                         left join [SYS_PlantArea] B on A.[PlantAreaID]=B.[PlantAreaID] 
                         left join [SYS_Organization] C on B.[PlantID]=C.[OrganizationID]
                         left join [SYS_Parameters] D on A.[Condition]=D.[ParameterID]
                         left join [SYS_Attachments] E on '{1}'=E.[ObjectID]
                         where A.[SystemID]='{0}' and A.[Status]='{0}0201213000001' and A.[ResourceCategory]='{0}0201213000048'
                         and A.[EquipmentID]='{1}'", Framework.SystemID,EquipmentID);
            count = UniversalService.getCount(sql, null);

            string orderby = "A.EquipmentID";

            DataTable dt = UniversalService.getTable(select, sql, orderby, null, page, rows);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// ��̨�豸��� �豸�б� V1
        /// Mouse2017��9��6��11:28:06
        /// 2017��11��10��10:44:17
        /// </summary>
        /// <param name="Code"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> Iot00003GetEquipmentListV1(string EquipmentID, int page, int rows, ref int count)
        {
            string select = string.Format(@"select A.EquipmentID,B.Name as PlantAreaName,C.Name PlantName,D.Name as ConditionName,D.Description,E.Path
                        ,E.[Default]");
            string sql = string.Format(@"
                         from [EMS_Equipment] A
                         left join [SYS_PlantArea] B on A.[PlantAreaID]=B.[PlantAreaID] 
                         left join [SYS_Organization] C on B.[PlantID]=C.[OrganizationID]
                         left join [SYS_Parameters] D on A.[Condition]=D.[ParameterID]
                         left join [SYS_Attachments] E on A.[EquipmentID]=E.[ObjectID] and E.[Status] <> '{0}0201213000003'
                         where A.[SystemID]='{0}' and A.[Status]='{0}0201213000001' and A.[ResourceCategory]='{0}0201213000048'
                         and A.[EquipmentID]='{1}'  ", Framework.SystemID, EquipmentID);
            count = UniversalService.getCount(sql, null);

            string orderby = "A.EquipmentID,E.[Default] desc";

            DataTable dt = UniversalService.getTable(select, sql, orderby, null, page, rows);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// Iot00003�豸����
        /// Mouse 2017��9��6��18:03:53
        /// </summary>
        /// <param name="EquipmentID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> Iot00003GetEquipment(string Code, int page,int rows,ref int count)
        {
            string select = string.Format(@"select A.EquipmentID,A.Code,A.Name,B.Code as ResourceCategoryCode, B.Name as ResourceCategoryName,C.Name as ConditionName");
            string sql = string.Format(@"
                         from [EMS_Equipment] A
                         left join [SYS_Parameters] B on A.[ResourceCategory]=B.[ParameterID]
                         left join [SYS_Parameters] C on A.[Condition]=C.[ParameterID]
                         where A.[SystemID]='{0}' and A.[ResourceCategory]='{0}0201213000048' and A.[Status]='{0}0201213000001'",Framework.SystemID);

            SqlParameter[] parameter = new SqlParameter[] {
                new SqlParameter("@Code",SqlDbType.NVarChar),
            };
            parameter[0].Value = DBNull.Value;

            SqlParameter[] parcount = new SqlParameter[] {
                new SqlParameter("@Code",SqlDbType.NVarChar),
            };
            parcount[0].Value = DBNull.Value;

            if (!string.IsNullOrWhiteSpace(Code))
            {
                Code = "%" + Code + "%";
                sql = sql + string.Format(@" and A.Code collate Chinese_PRC_CI_AS like '{0}'",Code);
                parameter[0].Value = Code;
                parcount[0].Value = Code;
            }
            count = UniversalService.getCount(sql, parcount);

            string orderby = "A.Code";

            DataTable dt = UniversalService.getTable(select, sql, orderby, null, page, rows);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// Iot00003�豸����
        /// Mouse 2017��9��6��18:03:53
        /// </summary>
        /// <param name="EquipmentID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> Iot00003GetEquipmentV1(string EquipmentID,string Code, int page, int rows, ref int count)
        {
            string select = string.Format(@"select A.EquipmentID,A.Code,A.Name,B.Name as ResourceCategoryName,C.Name as ConditionName");
            string sql = string.Format(@"
                         from [EMS_Equipment] A
                         left join [SYS_Parameters] B on A.[ResourceCategory]=B.[ParameterID]
                         left join [SYS_Parameters] C on A.[Condition]=C.[ParameterID]
                         where A.[SystemID]='{0}' and A.[ResourceCategory]='{0}0201213000048' and A.[Status]='{0}0201213000001'", Framework.SystemID);
            SqlParameter[] parameter = new SqlParameter[] {
                new SqlParameter("@Code",SqlDbType.VarChar),
                
            };
            parameter[0].Value = DBNull.Value;
            SqlParameter[] parcount = new SqlParameter[] {
                new SqlParameter("@Code",SqlDbType.VarChar),

            };
            parcount[0].Value = DBNull.Value;
            if (!string.IsNullOrWhiteSpace(Code))
            {
                sql = sql + string.Format(@" and A.Code Collate CHINESE_PRC_CI_AS like '%{0}%'",Code);
                parameter[0].Value = Code;
                parcount[0].Value = Code;
            }
            count = UniversalService.getCount(sql, parcount);
            
            string orderby = "A.EquipmentID";

            DataTable dt = UniversalService.getTable(select, sql, orderby, parameter, page, rows);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// Iot00003�������İ�ť�б�
        /// Mouse 2017��9��12��16:36:32
        /// </summary>
        /// <param name="EquipmentID"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> Iot00003GetCenter(string EquipmentID,int page,int rows,ref int count)
        {
            string select = string.Format(@"select DISTINCT D.WorkCenterID,D.Code as CenterCode,D.Name as CenterName,
                            F.Name as InoutMarkName,E.Code as DepartmentCode ,E.Name as DepartmentName ");
            string sql = string.Format(@"from [EMS_Equipment] A
                        left join [SYS_ResourceDetails] B on A.EquipmentID=B.DetailID 
                        left join [SYS_WorkCenterResources] C on B.ResourceID=C.ResourceID
                        left join [SYS_WorkCenter] D on C.WorkCenterID=D.WorkCenterID
                        left join [SYS_Organization] E on D.DepartmentID=E.OrganizationID
                        left join [SYS_Parameters] F on D.InoutMark=F.ParameterID
                        where A.[EquipmentID]='{0}' and D.[Status]='{1}0201213000001'", EquipmentID,Framework.SystemID);

            count = UniversalService.getCount(sql, null);

            string orderby = " order by D.WorkCenterID";

            DataTable dt = SQLHelper.ExecuteDataTable(select + sql + orderby, CommandType.Text);

            return ToHashtableList(dt);
        }



        /// <summary>
        /// �ж�һ����������Ӧ����Դ����Ӧ����ϸ�е��豸�Ƿ��ڱ���״̬
        /// SAM 2017��9��19��17:03:06
        /// </summary>
        /// <param name="FabMoOperationID"></param>
        /// <returns></returns>
        public static bool Sfc00006CheckEquipment(string TaskDispatchID)
        {
            string sql = string.Format(@" select * from [EMS_Equipment] A 
            where [EquipmentID] in 
            (select [EquipmentID] from [SFC_TaskDispatchResource] where [TaskDispatchID] ='{1}' and [Status] = '{0}0201213000001') 
            and A.[SystemID]='{0}' and A.[Status] ='{0}0201213000001' and A.[Condition]='{0}0201213000024' ", Framework.SystemID, TaskDispatchID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return false;
            else
                return true;
        }

        /// <summary>
        /// �豸�����ȡ�б�
        /// Alvin 2017��9��4��17:56:14
        /// </summary>
        /// <param name="token"></param>
        /// <param name="plantCode"></param>
        /// <param name="plantAreaCode"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> Iot00002GetList(string token, string plantCode, string plantAreaCode)
        {
            string select = string.Format(@"select A.EquipmentID,A.Code,A.Name,A.AbnormalStatus,C.Name as ConditionName,
             C.Description as ColorName,(select Path from [SYS_Attachments] where [Default]='1' and 
             [ObjectID] = A.EquipmentID  and  [Status] <> '{0}0201213000003') as Path,
             E.Name as PlantAreaName,F.Name as PlantName ", Framework.SystemID);

            string sql = string.Format(@" from [EMS_Equipment] A 
            left join [SYS_Parameters] B on A.[ResourceCategory] = B.[ParameterID]
            left join [SYS_Parameters] C on A.[Condition] = C.[ParameterID]
            left join [SYS_PlantArea] E on A.[PlantAreaID] = E.[PlantAreaID]
            left join [SYS_Organization] F on E.[PlantID] = F.[OrganizationID]
            where A.[SystemID]='{0}' and A.[Status] = '{0}0201213000001' and B.[Code] = 'M' ", Framework.SystemID);

            List<SqlParameter> parameters = new List<SqlParameter>();
            if (!string.IsNullOrWhiteSpace(plantCode))
            {
                sql += string.Format(@" and F.Code collate Chinese_PRC_CI_AS like '%{0}%' ", plantCode);
           
            }
            if (!string.IsNullOrWhiteSpace(plantAreaCode))
            {
                sql += string.Format(@" and E.Code collate Chinese_PRC_CI_AS like '%{0}%' ", plantAreaCode);
              
            }
           
            SqlParameter[] paramArray = parameters.ToArray();
            
            string orderby = " order by A.[Code] ";

            DataTable dt = SQLHelper.ExecuteDataTable(select + sql + orderby,CommandType.Text);

            return ToHashtableList(dt);
        }
    }
}