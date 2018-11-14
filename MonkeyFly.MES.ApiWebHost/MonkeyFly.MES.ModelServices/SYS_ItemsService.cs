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
    public class SYS_ItemsService : SuperModel<SYS_Items>
    {

        /// <summary>
        /// ����
        /// SAM 2017��5��16��14:44:27
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="Model"></param>
        /// <returns></returns>
        public static bool insert(string userId, SYS_Items Model)
        {
            try
            {
                string sql = string.Format(@"insert[SYS_Items]([ItemID],[Code],[Name],[Specification],[Status],
                [Unit],[ClassOne],[ClassTwo],[ClassThree],[ClassFour],[ClassFive],[AuxUnit],[AuxUnitRatio],
                [IsCutMantissa],[CutMantissa],[Type],[Drawing],[PartSource],[BarCord],[GroupID],[Lot],[OverRate],
                [Comments],[SerialPart],[KeyPart],[LotMethod],[LotClassID],[Modifier],[ModifiedTime],[ModifiedLocalTime],[Creator],[CreateTime],[CreateLocalTime],[SystemID]) values
                 (@ItemID,@Code,@Name,@Specification,@Status,
                @Unit,@ClassOne,@ClassTwo,@ClassThree,@ClassFour,@ClassFive,@AuxUnit,@AuxUnitRatio,
                @IsCutMantissa,@CutMantissa,@Type,@Drawing,@PartSource,@BarCord,@GroupID,@Lot,@OverRate,
                @Comments,@SerialPart,@KeyPart,@LotMethod,@LotClassID,'{0}','{1}','{2}','{0}','{1}','{2}','{3}')",
                userId, DateTime.Now, DateTime.Now, Framework.SystemID);

                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@ItemID",SqlDbType.VarChar),
                    new SqlParameter("@Code",SqlDbType.VarChar),
                    new SqlParameter("@Name",SqlDbType.VarChar),
                    new SqlParameter("@Specification",SqlDbType.VarChar),
                    new SqlParameter("@Status",SqlDbType.VarChar),
                    new SqlParameter("@Unit",SqlDbType.VarChar),
                    new SqlParameter("@ClassOne",SqlDbType.VarChar),
                    new SqlParameter("@ClassTwo",SqlDbType.VarChar),
                    new SqlParameter("@ClassThree",SqlDbType.VarChar),
                    new SqlParameter("@ClassFour",SqlDbType.VarChar),
                    new SqlParameter("@ClassFive",SqlDbType.VarChar),
                    new SqlParameter("@AuxUnit",SqlDbType.VarChar),
                    new SqlParameter("@AuxUnitRatio",SqlDbType.Decimal),
                    new SqlParameter("@IsCutMantissa",SqlDbType.Bit),
                    new SqlParameter("@CutMantissa",SqlDbType.VarChar),
                    new SqlParameter("@Type",SqlDbType.VarChar),
                    new SqlParameter("@Drawing",SqlDbType.VarChar),
                    new SqlParameter("@PartSource",SqlDbType.VarChar),
                    new SqlParameter("@BarCord",SqlDbType.VarChar),
                    new SqlParameter("@GroupID",SqlDbType.VarChar),
                    new SqlParameter("@Lot",SqlDbType.Bit),
                    new SqlParameter("@OverRate",SqlDbType.Decimal),
                    new SqlParameter("@Comments",SqlDbType.VarChar),
                    new SqlParameter("@SerialPart",SqlDbType.Bit),
                    new SqlParameter("@KeyPart",SqlDbType.Bit),
                    new SqlParameter("@LotMethod",SqlDbType.VarChar),
                    new SqlParameter("@LotClassID",SqlDbType.VarChar),
                    };

                parameters[0].Value = (Object)Model.ItemID ?? DBNull.Value;
                parameters[1].Value = (Object)Model.Code ?? DBNull.Value;
                parameters[2].Value = (Object)Model.Name ?? DBNull.Value;
                parameters[3].Value = (Object)Model.Specification ?? DBNull.Value;
                parameters[4].Value = (Object)Model.Status ?? DBNull.Value;
                parameters[5].Value = (Object)Model.Unit ?? DBNull.Value;
                parameters[6].Value = (Object)Model.ClassOne ?? DBNull.Value;
                parameters[7].Value = (Object)Model.ClassTwo ?? DBNull.Value;
                parameters[8].Value = (Object)Model.ClassThree ?? DBNull.Value;
                parameters[9].Value = (Object)Model.ClassFour ?? DBNull.Value;
                parameters[10].Value = (Object)Model.ClassFive ?? DBNull.Value;
                parameters[11].Value = (Object)Model.AuxUnit ?? DBNull.Value;
                parameters[12].Value = (Object)Model.AuxUnitRatio ?? DBNull.Value;
                parameters[13].Value = (Object)Model.IsCutMantissa ?? DBNull.Value;
                parameters[14].Value = (Object)Model.CutMantissa ?? DBNull.Value;
                parameters[15].Value = (Object)Model.Type ?? DBNull.Value;
                parameters[16].Value = (Object)Model.Drawing ?? DBNull.Value;
                parameters[17].Value = (Object)Model.PartSource ?? DBNull.Value;
                parameters[18].Value = (Object)Model.BarCord ?? DBNull.Value;
                parameters[19].Value = (Object)Model.GroupID ?? DBNull.Value;
                parameters[20].Value = (Object)Model.Lot ?? DBNull.Value;
                parameters[21].Value = (Object)Model.OverRate ?? DBNull.Value;
                parameters[22].Value = (Object)Model.Comments ?? DBNull.Value;
                parameters[23].Value = (Object)Model.SerialPart ?? DBNull.Value;
                parameters[24].Value = (Object)Model.KeyPart ?? DBNull.Value;
                parameters[25].Value = (Object)Model.LotMethod ?? DBNull.Value;
                parameters[26].Value = (Object)Model.LotClassID ?? DBNull.Value;

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
        /// SAM 2017��5��16��14:45:06
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="Model"></param>
        /// <returns></returns>
        public static bool update(string userId, SYS_Items Model)
        {
            try
            {
                string sql = String.Format(@"update[SYS_Items] set {0},
                [Name]=@Name,[Specification]=@Specification,[Status]=@Status,[Unit]=@Unit,[ClassOne]=@ClassOne,[ClassTwo]=@ClassTwo,
                [ClassThree]=@ClassThree,[ClassFour]=@ClassFour,[ClassFive]=@ClassFive,[AuxUnit]=@AuxUnit,[AuxUnitRatio]=@AuxUnitRatio,
                [IsCutMantissa]=@IsCutMantissa,[CutMantissa]=@CutMantissa,[Type]=@Type,[Drawing]=@Drawing,[PartSource]=@PartSource,
                [BarCord]=@BarCord,[GroupID]=@GroupID,[Lot]=@Lot,[OverRate]=@OverRate,[Comments]=@Comments,
                [SerialPart] = @SerialPart,[KeyPart] = @KeyPart,[LotMethod] = @LotMethod,[LotClassID] = @LotClassID
                where [ItemID]=@ItemID", UniversalService.getUpdateUTC(userId));

                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@ItemID",SqlDbType.VarChar),
                    new SqlParameter("@Name",SqlDbType.VarChar),
                    new SqlParameter("@Specification",SqlDbType.VarChar),
                    new SqlParameter("@Status",SqlDbType.VarChar),
                    new SqlParameter("@Unit",SqlDbType.VarChar),
                    new SqlParameter("@ClassOne",SqlDbType.VarChar),
                    new SqlParameter("@ClassTwo",SqlDbType.VarChar),
                    new SqlParameter("@ClassThree",SqlDbType.VarChar),
                    new SqlParameter("@ClassFour",SqlDbType.VarChar),
                    new SqlParameter("@ClassFive",SqlDbType.VarChar),
                    new SqlParameter("@AuxUnit",SqlDbType.VarChar),
                    new SqlParameter("@AuxUnitRatio",SqlDbType.Decimal),
                    new SqlParameter("@IsCutMantissa",SqlDbType.Bit),
                    new SqlParameter("@CutMantissa",SqlDbType.VarChar),
                    new SqlParameter("@Type",SqlDbType.VarChar),
                    new SqlParameter("@Drawing",SqlDbType.VarChar),
                    new SqlParameter("@PartSource",SqlDbType.VarChar),
                    new SqlParameter("@BarCord",SqlDbType.VarChar),
                    new SqlParameter("@GroupID",SqlDbType.VarChar),
                    new SqlParameter("@Lot",SqlDbType.Bit),
                    new SqlParameter("@OverRate",SqlDbType.Decimal),
                    new SqlParameter("@Comments",SqlDbType.VarChar),
                    new SqlParameter("@SerialPart",SqlDbType.Bit),
                    new SqlParameter("@KeyPart",SqlDbType.Bit),
                    new SqlParameter("@LotMethod",SqlDbType.VarChar),
                    new SqlParameter("@LotClassID",SqlDbType.VarChar),
                    };

                parameters[0].Value = Model.ItemID;
                parameters[1].Value = (Object)Model.Name ?? DBNull.Value;
                parameters[2].Value = (Object)Model.Specification ?? DBNull.Value;
                parameters[3].Value = (Object)Model.Status ?? DBNull.Value;
                parameters[4].Value = (Object)Model.Unit ?? DBNull.Value;
                parameters[5].Value = (Object)Model.ClassOne ?? DBNull.Value;
                parameters[6].Value = (Object)Model.ClassTwo ?? DBNull.Value;
                parameters[7].Value = (Object)Model.ClassThree ?? DBNull.Value;
                parameters[8].Value = (Object)Model.ClassFour ?? DBNull.Value;
                parameters[9].Value = (Object)Model.ClassFive ?? DBNull.Value;
                parameters[10].Value = (Object)Model.AuxUnit ?? DBNull.Value;
                parameters[11].Value = (Object)Model.AuxUnitRatio ?? DBNull.Value;
                parameters[12].Value = (Object)Model.IsCutMantissa ?? DBNull.Value;
                parameters[13].Value = (Object)Model.CutMantissa ?? DBNull.Value;
                parameters[14].Value = (Object)Model.Type ?? DBNull.Value;
                parameters[15].Value = (Object)Model.Drawing ?? DBNull.Value;
                parameters[16].Value = (Object)Model.PartSource ?? DBNull.Value;
                parameters[17].Value = (Object)Model.BarCord ?? DBNull.Value;
                parameters[18].Value = (Object)Model.GroupID ?? DBNull.Value;
                parameters[19].Value = (Object)Model.Lot ?? DBNull.Value;
                parameters[20].Value = (Object)Model.OverRate ?? DBNull.Value;
                parameters[21].Value = (Object)Model.Comments ?? DBNull.Value;
                parameters[22].Value = (Object)Model.SerialPart ?? DBNull.Value;
                parameters[23].Value = (Object)Model.KeyPart ?? DBNull.Value;
                parameters[24].Value = (Object)Model.LotMethod ?? DBNull.Value;
                parameters[25].Value = (Object)Model.LotClassID ?? DBNull.Value;

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
        /// SAM 2017��5��16��14:45:30
        /// </summary>
        /// <param name="ItemID"></param>
        /// <returns></returns>
        public static SYS_Items get(string ItemID)
        {
            string sql = string.Format(@"select Top 1 * from [SYS_Items] where [ItemID] = '{0}'  and [SystemID] = '{1}' ", ItemID, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return null;
            else
                return ToEntity(dt.Rows[0]);
        }

        /// <summary>
        /// ���ݴ��Ż�ȡʵ��
        /// SAM 2017��6��15��10:06:09
        /// </summary>
        /// <param name="Code"></param>
        /// <returns></returns>
        public static SYS_Items getByCode(string Code)
        {
            string sql = string.Format(@"select Top 1 * from [SYS_Items] where [Code] = '{0}'  and [SystemID] = '{1}' and [Status]='{1}0201213000001' ", Code, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return null;
            else
                return ToEntity(dt.Rows[0]);
        }

        /// <summary>
        /// ���ݴ��ţ���׼��ѯ��Ʒ��Ϣ
        /// SAM 2017��9��23��15:20:57
        /// </summary>
        /// <param name="Code"></param>
        /// <returns></returns>
        public static Hashtable GetItem(string Code)
        {
            string sql = string.Format(@"select Top 1 * from [SYS_Items] where [Code] = '{0}'  and [SystemID] = '{1}' and [Status]='{1}0201213000001' ", Code, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return null;
            else
                return ToHashtableList(dt)[0];
        }


        /// <summary>
        /// �жϴ����Ƿ��ظ�
        /// SAM 2017��5��16��15:38:13
        /// </summary>
        /// <param name="Code">����</param>
        /// <param name="ItemID"></param>
        /// <returns></returns>
        public static bool CheckCode(string Code, string ItemID)
        {
            string sql = String.Format(@"select Top 1 * from [SYS_Items] where [SystemID]='{0}' and Status <> '{0}0201213000003'", Framework.SystemID);

            /*�ȶ���Code��Ĭ�ϸ�DbNull,��������Ҳ���*/
            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@Code",SqlDbType.VarChar)
            };
            parameters[0].Value = DBNull.Value;

            /*��ΪCode��ͨ���ֶ�����ģ�������Ҫ�ò�������ʽȥƴSQL*/
            if (!string.IsNullOrWhiteSpace(Code))
            {
                sql = sql + String.Format(@" and [Code] =@Code ");
                parameters[0].Value = Code;
            }

            /*ItemID����ˮ�ţ������ڸ���ʱ���ų����Լ�*/
            if (!string.IsNullOrWhiteSpace(ItemID))
                sql = sql + String.Format(@" and [ItemID] <> '{0}' ", ItemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text, parameters);

            if (dt.Rows.Count == 0)
                return false;
            else
                return true;
        }


        /// <summary>
        /// ��Ʒ�����б�
        /// SAM 2017��5��16��15:44:33
        /// </summary>
        /// <param name="Code">����</param>
        /// <param name="Status">״̬</param>
        /// <param name="page">ҳ��</param>
        /// <param name="rows">����</param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> Inf00010GetList(string Code, string status, int page, int rows, ref int count)
        {
            string select = string.Format(@"select A.ItemID,A.Code,A.Name,A.Specification,A.Status,A.[SerialPart],A.[KeyPart],A.[LotMethod],A.[LotClassID],
            A.Unit,A.ClassOne,A.ClassTwo,A.ClassThree,A.ClassFour,A.ClassFive,A.AuxUnit,A.AuxUnitRatio,A.IsCutMantissa,A.CutMantissa,A.Type, K.Code as LotClass,
            A.Drawing,A.PartSource,A.BarCord,A.GroupID,A.Lot,A.OverRate,A.Comments,
            D.Code as UnitCode,J.Code as AuxUnitCode,
            B.UserName as Creator,A.CreateLocalTime as CreateTime,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else C.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ");

            string sql = string.Format(@" from [SYS_Items] A 
            left join [SYS_MESUsers] B on A.Creator = B.MESUserID
            left join [SYS_MESUsers] C on A.Modifier = C.MESUserID
            left join [SYS_Parameters] D on A.Unit = D.ParameterID
            left join [SYS_Parameters] J on A.AuxUnit = J.ParameterID
            left join [SYS_AutoNumber] K on A.LotClassID = K.AutoNumberID
            where A.[SystemID]='{0}' and A.Status <> '{0}0201213000003' ", Framework.SystemID);

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
                sql = sql + String.Format(@" and A.[Code] like @Code ");
                parameters[0].Value = Code;
                Parcount[0].Value = Code;
            }

            if (!string.IsNullOrWhiteSpace(status))
            {
                sql = sql + String.Format(@" and A.[Status] = @Status ");
                parameters[1].Value = status;
                Parcount[1].Value = status;
            }

            count = UniversalService.getCount(sql, Parcount);

            string orderby = "A.[Status],A.[Code] ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, parameters, page, rows);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// ��ȡ��Ʒ�ĵ�����Ϣ
        /// SAM 2017��5��17��09:17:50
        /// </summary>
        /// <param name="Code">����</param>
        /// <param name="Status">״̬</param>
        /// <returns></returns>
        public static DataTable Inf00010Export(string Code, string status)
        {
            string select = string.Format(@"select ROW_NUMBER() OVER (ORDER BY A.[Status],A.[Code]),A.Code,A.Name,A.Specification,
            N.Name as Status,
            D.Code as UnitCode,E.Code as ClassOneCode,F.Code as ClassTwoCode,G.Code as ClassThreeCode,H.Code as ClassFourCode,I.Code as ClassFiveCode,
            J.Code as AuxUnitCode,A.AuxUnitRatio,(CASE WHEN A.IsCutMantissa=1 THEN '��' ELSE '��' END),K.Name as CutMantissaName,
            L.Name as TypeName,A.Drawing,M.Name as PartSourceName,A.Comments,A.BarCord,O.Code as GroupCode,(CASE WHEN A.Lot=1 THEN '��' ELSE '��' END),
            P.Code as LotClassCode,Q.Name as LotMethod,
            convert(varchar(30),A.OverRate)+'%',
            (CASE WHEN A.SerialPart=1 THEN '��' ELSE '��' END),
            (CASE WHEN A.KeyPart=1 THEN '��' ELSE '��' END),
            B.UserName as Creator,A.CreateLocalTime as CreateTime,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else C.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ");

            string sql = string.Format(@" from [SYS_Items] A 
            left join [SYS_MESUsers] B on A.Creator = B.MESUserID
            left join [SYS_MESUsers] C on A.Modifier = C.MESUserID
            left join [SYS_Parameters] D on A.Unit = D.ParameterID
            left join [SYS_Parameters] E on A.ClassOne = E.ParameterID
            left join [SYS_Parameters] F on A.ClassTwo = F.ParameterID
            left join [SYS_Parameters] G on A.ClassThree = G.ParameterID
            left join [SYS_Parameters] H on A.ClassFour = H.ParameterID
            left join [SYS_Parameters] I on A.ClassFive = I.ParameterID
            left join [SYS_Parameters] J on A.AuxUnit = J.ParameterID
            left join [SYS_Parameters] K on A.CutMantissa = K.ParameterID
            left join [SYS_Parameters] L on A.Type = L.ParameterID
            left join [SYS_Parameters] M on A.PartSource = M.ParameterID
            left join [SYS_Parameters] N on A.Status = N.ParameterID
            left join [SYS_Parameters] O on A.GroupID = O.ParameterID
            left join [SYS_AutoNumber] P on A.LotClassID = P.AutoNumberID
            left join [SYS_Parameters] Q on A.LotMethod = Q.ParameterID


            where A.[SystemID]='{0}' and A.Status <> '{0}0201213000003' ", Framework.SystemID);

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
                sql = sql + String.Format(@" and A.[Code] like @Code ");
                parameters[0].Value = Code;
                Parcount[0].Value = Code;
            }

            if (!string.IsNullOrWhiteSpace(status))
            {
                sql = sql + String.Format(@" and A.[Status] = @Status ");
                parameters[1].Value = status;
                Parcount[1].Value = status;
            }


            string orderBy = "order By A.[Status],A.[Code] ";

            return SQLHelper.ExecuteDataTable(select + sql + orderBy, CommandType.Text, parameters);
        }

        /// <summary>
        /// ���ݼ���Ⱥ�����ȡ������������Ʒ�б�
        /// SAM 2017��5��26��15:16:36
        /// </summary>
        /// <param name="groupID"></param>
        /// <returns></returns>
        public static IList<Hashtable> Qcs00003ItemList(string groupID, string StartCode, string EndCode, int page, int rows,ref int count)
        {
            string select = string.Format(@"select null as GroupItemID,A.ItemID,A.Code,A.Name,B.Name as Type,A.Specification ");

            string sql = string.Format(@"  from [SYS_Items] A 
            left join [SYS_Parameters] B on A.Type = B.ParameterID
            where [ItemID] not in (select [ItemID] from [QCS_GroupItem] where [GroupID] ='{1}' and [Status] = '{0}0201213000001')
            and A.[SystemID]='{0}' and A.[Status] = '{0}0201213000001' ", Framework.SystemID, groupID);

            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@StartCode",SqlDbType.VarChar),
                new SqlParameter("@EndCode",SqlDbType.VarChar)
            };
            parameters[0].Value = DBNull.Value;
            parameters[1].Value = DBNull.Value;

            SqlParameter[] Parcount = new SqlParameter[]{
                new SqlParameter("@StartCode",SqlDbType.VarChar),
                new SqlParameter("@EndCode",SqlDbType.VarChar)
            };
            Parcount[0].Value = DBNull.Value;
            Parcount[1].Value = DBNull.Value;

            if (!string.IsNullOrWhiteSpace(StartCode))
            {
                sql = sql + string.Format(@" and A.[Code] >= @StartCode ");
                parameters[0].Value = StartCode;
                Parcount[0].Value = StartCode;
            }

            if (!string.IsNullOrWhiteSpace(EndCode))
            {
                sql = sql + string.Format(@" and A.[Code] <= @EndCode ");
                parameters[1].Value = EndCode;
                Parcount[1].Value = EndCode;
            }

            count = UniversalService.getCount(sql, Parcount);

            string orderBy = "A.[Code] ";

            //DataTable dt = SQLHelper.ExecuteDataTable(select + sql + orderBy, CommandType.Text, parameters);
            DataTable dt = UniversalService.getTable(select, sql, orderBy, parameters, page, rows);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// �ж�Ҫɾ���ķ����Ƿ�����Ʒ������ʹ��
        /// SAM 2017��6��13��16:46:45
        /// </summary>
        /// <param name="ClassID"></param>
        /// <returns></returns>
        public static bool CheckClass(string ClassID)
        {
            string sql = String.Format(@"select Top 1 * from [SYS_Items] 
            where [SystemID]='{0}' and Status <> '{0}0201213000003' 
            and ([ClassOne]='{1}' or [ClassTwo]='{1}' or [ClassThree]='{1}' or [ClassFour]='{1}' or [ClassFive]='{1}')", Framework.SystemID, ClassID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return false;
            else
                return true;
        }

        /// <summary>
        /// �жϵ�λ�Ƿ�����Ʒ��ʹ��
        /// SAM 2017��6��14��18:02:57
        /// </summary>
        /// <param name="UnitID"></param>
        /// <returns></returns>
        public static bool CheckUnit(string UnitID)
        {
            string sql = String.Format(@"select Top 1 * from [SYS_Items] 
            where [SystemID]='{0}' and Status <> '{0}0201213000003' 
            and ([Unit]='{1}' or [AuxUnit]='{1}')", Framework.SystemID, UnitID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return false;
            else
                return true;
        }

        /// <summary>
        /// ��ȡ��Ʒ�ĵ���
        /// SAM 2017��6��14��18:12:33
        /// </summary>
        /// <param name="Code"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> GetItemList(string Code,string Name,int page, int rows, ref int count)
        {
            string select = string.Format(@"select A.ItemID,A.Code,A.Name,
            A.Specification,D.Name as Unit,E.Name as Type,F.Name as Status,A.Comments,
            B.UserName as Creator,A.CreateLocalTime as CreateTime,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else C.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ");

            string sql = string.Format(@" from [SYS_Items] A 
            left join [SYS_MESUsers] B on A.Creator = B.MESUserID
            left join [SYS_MESUsers] C on A.Modifier = C.MESUserID
            left join [SYS_Parameters] D on A.Unit = D.ParameterID
            left join [SYS_Parameters] J on A.AuxUnit = J.ParameterID
            left join [SYS_Parameters] E on A.Type = E.ParameterID
            left join [SYS_Parameters] F on A.Status = F.ParameterID
            where A.[SystemID]='{0}' and A.Status='{0}0201213000001' ", Framework.SystemID);

            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@Code",SqlDbType.VarChar),
                new SqlParameter("@Name",SqlDbType.VarChar)
            };
            parameters[0].Value = DBNull.Value;
            parameters[1].Value = DBNull.Value;

            SqlParameter[] Parcount = new SqlParameter[]{
                new SqlParameter("@Code",SqlDbType.VarChar),
                new SqlParameter("@Name",SqlDbType.VarChar)
            };
            Parcount[0].Value = DBNull.Value;
            Parcount[1].Value = DBNull.Value;

            if (!string.IsNullOrWhiteSpace(Code))
            {
                Code = "%" + Code.Trim() + "%";
                sql = sql + String.Format(@" and A.[Code] collate Chinese_PRC_CI_AS like @Code ");
                parameters[0].Value = Code;
                Parcount[0].Value = Code;
            }

            if (!string.IsNullOrWhiteSpace(Name))
            {
                Name = "%" + Name.Trim() + "%";
                sql = sql + String.Format(@" and A.[Name] collate Chinese_PRC_CI_AS like @Name ");
                parameters[1].Value = Name;
                Parcount[1].Value = Name;
            }

            count = UniversalService.getCount(sql, Parcount);

            string orderby = "A.[Status],A.[Code] ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, parameters, page, rows);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// SFCר�û�ȡ��Ʒ�ĵ������ų������͑BΪ4����ُ�����ģ�
        /// SAM 2017��7��23��00:09:20
        /// </summary>
        /// <param name="Code"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> SfcGetItemList(string Code, string Type,string StartCode, string EndCode, int page, int rows, ref int count)
        {
            string select = string.Format(@"select A.ItemID,A.Code,A.Name,
            A.Specification,D.Name as Unit,E.Name as Type,F.Name as Status,A.Comments,A.OverRate,
            B.Emplno+'-'+B.UserName as Creator,A.CreateLocalTime as CreateTime,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else C.Emplno+'-'+C.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ");

            string sql = string.Format(@" from [SYS_Items] A 
            left join [SYS_MESUsers] B on A.Creator = B.MESUserID
            left join [SYS_MESUsers] C on A.Modifier = C.MESUserID
            left join [SYS_Parameters] D on A.Unit = D.ParameterID
            left join [SYS_Parameters] J on A.AuxUnit = J.ParameterID
            left join [SYS_Parameters] E on A.Type = E.ParameterID
            left join [SYS_Parameters] F on A.Status = F.ParameterID
            where A.[SystemID]='{0}' and A.Status='{0}0201213000001' and A.[Type] <> '{0}020121300000A'", Framework.SystemID);

            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@Code",SqlDbType.VarChar),
                new SqlParameter("@StartCode",SqlDbType.VarChar),
                new SqlParameter("@EndCode",SqlDbType.VarChar)
            };
            parameters[0].Value = DBNull.Value;
            parameters[1].Value = DBNull.Value;
            parameters[2].Value = DBNull.Value;

            SqlParameter[] Parcount = new SqlParameter[]{
                new SqlParameter("@Code",SqlDbType.VarChar),
                new SqlParameter("@StartCode",SqlDbType.VarChar),
                new SqlParameter("@EndCode",SqlDbType.VarChar)
            };
            Parcount[0].Value = DBNull.Value;
            Parcount[1].Value = DBNull.Value;
            Parcount[2].Value = DBNull.Value;

            if (!string.IsNullOrWhiteSpace(Code))
            {
                Code = "%" + Code + "%";
                sql = sql + String.Format(@" and A.[Code] collate Chinese_PRC_CI_AS like @Code ");
                parameters[0].Value = Code;
                Parcount[0].Value = Code;
            }

            if (!string.IsNullOrWhiteSpace(StartCode))
            {
                sql = sql + String.Format(@" and A.[Code] >= @StartCode ");
                parameters[1].Value = StartCode;
                Parcount[1].Value = StartCode;
            }

            if (!string.IsNullOrWhiteSpace(EndCode))
            {
                sql = sql + String.Format(@" and A.[Code] <= @EndCode ");
                parameters[2].Value = EndCode;
                Parcount[2].Value = EndCode;
            }

            if (!string.IsNullOrWhiteSpace(Type))
            {
                sql = sql + String.Format(@" and A.[Type] = '{0}' ", Type);
            }

            count = UniversalService.getCount(sql, Parcount);

            string orderby = "A.[Code] ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, parameters, page, rows);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// SFCר�û�ȡ��Ʒ�ĵ������ų������͑BΪ4����ُ�����ģ�
        /// SAM 2017��7��23��00:09:20
        /// </summary>
        /// <param name="Code"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> Sfc00023GetItemList(string Code, string Type, string StartCode, string EndCode, int page, int rows, ref int count)
        {
            string select = string.Format(@"select A.ItemID,A.Code,A.Name,
            A.Specification,D.Name as Unit,E.Name as Type,F.Name as Status,A.Comments,A.OverRate,
            B.Emplno+'-'+B.UserName as Creator,A.CreateLocalTime as CreateTime,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else C.Emplno+'-'+C.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ");

            string sql = string.Format(@" from [SYS_Items] A 
            left join [SYS_MESUsers] B on A.Creator = B.MESUserID
            left join [SYS_MESUsers] C on A.Modifier = C.MESUserID
            left join [SYS_Parameters] D on A.Unit = D.ParameterID
            left join [SYS_Parameters] J on A.AuxUnit = J.ParameterID
            left join [SYS_Parameters] E on A.Type = E.ParameterID
            left join [SYS_Parameters] F on A.Status = F.ParameterID
            where A.[SystemID]='{0}' and A.Status='{0}0201213000001' and A.[Type] <> '{0}020121300000A'", Framework.SystemID);

            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@Code",SqlDbType.VarChar),
                new SqlParameter("@StartCode",SqlDbType.VarChar),
                new SqlParameter("@EndCode",SqlDbType.VarChar)
            };
            parameters[0].Value = DBNull.Value;
            parameters[1].Value = DBNull.Value;
            parameters[2].Value = DBNull.Value;

            SqlParameter[] Parcount = new SqlParameter[]{
                new SqlParameter("@Code",SqlDbType.VarChar),
                new SqlParameter("@StartCode",SqlDbType.VarChar),
                new SqlParameter("@EndCode",SqlDbType.VarChar)
            };
            Parcount[0].Value = DBNull.Value;
            Parcount[1].Value = DBNull.Value;
            Parcount[2].Value = DBNull.Value;

            if (!string.IsNullOrWhiteSpace(Code))
            {
                sql = sql + String.Format(@" and A.[Code] collate Chinese_PRC_CI_AS > @Code ");
                parameters[0].Value = Code;
                Parcount[0].Value = Code;
            }

            if (!string.IsNullOrWhiteSpace(StartCode))
            {
                sql = sql + String.Format(@" and A.[Code] >= @StartCode ");
                parameters[1].Value = StartCode;
                Parcount[1].Value = StartCode;
            }

            if (!string.IsNullOrWhiteSpace(EndCode))
            {
                sql = sql + String.Format(@" and A.[Code] <= @EndCode ");
                parameters[2].Value = EndCode;
                Parcount[2].Value = EndCode;
            }

            if (!string.IsNullOrWhiteSpace(Type))
            {
                sql = sql + String.Format(@" and A.[Type] = '{0}' ", Type);
            }

            count = UniversalService.getCount(sql, Parcount);

            string orderby = "A.[Code] ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, parameters, page, rows);

            return ToHashtableList(dt);
        }


        /// <summary>
        /// ��Ʒ�б�
        /// ״̬Ϊ�����ģ����ǲɹ�������Ʒ
        /// SAM 2017��6��20��14:04:10
        /// </summary>
        /// <param name="StartCode"></param>
        /// <param name="EndCode"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> Sfc00001GetList(string Type, string StartCode, string EndCode, int page, int rows, ref int count)
        {
            string select = string.Format(@"select A.ItemID,A.Code,A.Name,A.OverRate,
            A.Specification,D.Name as Unit,A.Comments,
            B.UserName as Creator,A.CreateLocalTime as CreateTime,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else C.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ");

            string sql = string.Format(@" from [SYS_Items] A 
            left join [SYS_MESUsers] B on A.Creator = B.MESUserID
            left join [SYS_MESUsers] C on A.Modifier = C.MESUserID
            left join [SYS_Parameters] D on A.Unit = D.ParameterID
            where A.[SystemID]='{0}' and A.[Status]='{0}0201213000001' and A.[Type] <> '{0}020121300000A' ", Framework.SystemID);

            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@StartCode",SqlDbType.VarChar),
                new SqlParameter("@EndCode",SqlDbType.VarChar),
                new SqlParameter("@Type",SqlDbType.VarChar)
            };
            parameters[0].Value = DBNull.Value;
            parameters[1].Value = DBNull.Value;
            parameters[2].Value = DBNull.Value;

            SqlParameter[] Parcount = new SqlParameter[]{
                new SqlParameter("@StartCode",SqlDbType.VarChar),
                new SqlParameter("@EndCode",SqlDbType.VarChar),
                new SqlParameter("@Type",SqlDbType.VarChar)
            };
            Parcount[0].Value = DBNull.Value;
            Parcount[1].Value = DBNull.Value;
            Parcount[2].Value = DBNull.Value;

            if (!string.IsNullOrWhiteSpace(StartCode))
            {
                sql = sql + String.Format(@" and A.[Code] >= @StartCode ");
                parameters[0].Value = StartCode;
                Parcount[0].Value = StartCode;
            }

            if (!string.IsNullOrWhiteSpace(EndCode))
            {
                sql = sql + String.Format(@" and A.[Code] <= @EndCode ");
                parameters[1].Value = EndCode;
                Parcount[1].Value = EndCode;
            }

            if (!string.IsNullOrWhiteSpace(Type))
            {
                sql = sql + String.Format(@" and A.[Type] =@Type ");
                parameters[2].Value = Type;
                Parcount[2].Value = Type;
            }

            count = UniversalService.getCount(sql, Parcount);

            string orderby = "A.[Code] ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, parameters, page, rows);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// ��Ʒ�Ƴ̵���Ʒר������
        /// �����ģ���Ϊ��Ʒ����Ʒ
        /// SAM 2017��6��21��09:34:02
        /// </summary>
        /// <param name="StartCode"></param>
        /// <param name="EndCode"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> Sfc00001GetItemList(string StartCode,string EndCode, int page, int rows, ref int count)
        {
            string select = string.Format(@"select A.ItemID,A.Code,A.Name,A.OverRate,
            A.Specification,D.Name as Unit,E.Name as Type,F.Name as Status,A.Comments,A.Drawing,
            (Select Name from [SYS_Parameters] where [ParameterID] = A.[PartSource]) as PartSource,
            B.UserName as Creator,A.CreateLocalTime as CreateTime,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else C.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ");

            string sql = string.Format(@" from [SYS_Items] A 
            left join [SYS_MESUsers] B on A.Creator = B.MESUserID
            left join [SYS_MESUsers] C on A.Modifier = C.MESUserID
            left join [SYS_Parameters] D on A.Unit = D.ParameterID
            left join [SYS_Parameters] J on A.AuxUnit = J.ParameterID
            left join [SYS_Parameters] E on A.Type = E.ParameterID
            left join [SYS_Parameters] F on A.Status = F.ParameterID
            where A.[SystemID]='{0}' and A.Status='{0}0201213000001'  and A.[Type] <> '{0}0201213000008' ", Framework.SystemID);

            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@StartCode",SqlDbType.VarChar),
                 new SqlParameter("@EndCode",SqlDbType.VarChar)
            };
            parameters[0].Value = DBNull.Value;
            parameters[1].Value = DBNull.Value;

            SqlParameter[] Parcount = new SqlParameter[]{
                new SqlParameter("@StartCode",SqlDbType.VarChar),
                 new SqlParameter("@EndCode",SqlDbType.VarChar)
            };
            Parcount[0].Value = DBNull.Value;
            Parcount[1].Value = DBNull.Value;

            if (!string.IsNullOrWhiteSpace(StartCode))
            {
                sql = sql + String.Format(@" and A.[Code] >= @StartCode ");
                parameters[0].Value = StartCode;
                Parcount[0].Value = StartCode;
            }


            if (!string.IsNullOrWhiteSpace(EndCode))
            {
                sql = sql + String.Format(@" and A.[Code] <= @EndCode ");
                parameters[1].Value = EndCode;
                Parcount[1].Value = EndCode;
            }

            count = UniversalService.getCount(sql, Parcount);

            string orderby = "A.[Code] ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, parameters, page, rows);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// ��׼����淶�趨-��Ʒҳǩ�б�(��ȡ������Ʒ�Ƴ��趨���Ϻ�)
        /// SAM 2017��7��6��09:31:38
        /// </summary>
        /// <param name="Code"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> Qcs00004GetItemList(string Code, int page, int rows, ref int count)
        {
            string select = string.Format(@"select A.ItemID,A.Code,A.Name,E.Name as Type,
            A.Specification,D.Name as Unit,F.Name as Status,A.Comments,
            B.Emplno+'-'+B.UserName as Creator,A.CreateLocalTime as CreateTime,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else C.Emplno+'-'+C.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ");

            string sql = string.Format(@" from [SYS_Items] A 
            left join [SYS_MESUsers] B on A.Creator = B.MESUserID
            left join [SYS_MESUsers] C on A.Modifier = C.MESUserID
            left join [SYS_Parameters] D on A.Unit = D.ParameterID
            left join [SYS_Parameters] E on A.Type = E.ParameterID
            left join [SYS_Parameters] F on A.Status = F.ParameterID
            where A.[SystemID]='{0}' and A.Status='{0}0201213000001'  
            and A.[ItemID] in (select [ItemID] from [SFC_ItemProcess] where [Status] = '{0}0201213000001' and [SystemID] ='{0}') ", Framework.SystemID);

            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@Code",SqlDbType.VarChar)
            };
            parameters[0].Value = DBNull.Value;

            SqlParameter[] Parcount = new SqlParameter[]{
                new SqlParameter("@Code",SqlDbType.VarChar),
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

            string orderby = " A.[Status],A.[Code] ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, parameters, page, rows);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// ����ָ���Ƴ���ˮ�Ż���ָ���Ƴ̹�����ˮ�Ż�ȡ�����ڵ�Bom(����ҳ)
        /// SAM 2017��7��15��12:49:44
        /// </summary>
        /// <param name="FabMoProcessID"></param>
        /// <param name="FabMoOperationID"></param>
        /// <returns></returns>
        public static IList<Hashtable> Sfc00002GetNoBomList(string FabMoProcessID, string FabMoOperationID,int page, int rows, ref int count)
        {
            string select = string.Format(
                @"select A.ItemID,A.Code,A.Name,A.Specification,A.Name+'/'+A.Specification as NameSpecification,
                (select [Name] from [SYS_Parameters] where A.Type = ParameterID) as Type ");
            string sql = string.Format(@"from [SYS_Items] A
                  where A.[SystemID] = '{0}' and A.[Status] = '{0}0201213000001' and A.[Type] <> '{0}0201213000008'", Framework.SystemID);                     
            string orderby = "A.[Code]";

             if (!string.IsNullOrWhiteSpace(FabMoOperationID))
                sql += string.Format(@" and A.[ItemID] not in (Select ItemID From [SFC_FabMoItem] where [FabMoOperationID] = '{0}' and [SystemID]= '{1}' and [Status] ='{1}0201213000001')", FabMoOperationID, Framework.SystemID);
             else if (!string.IsNullOrWhiteSpace(FabMoProcessID))
                sql += string.Format(@" and A.[ItemID] not in (Select ItemID From [SFC_FabMoItem] where [FabMoProcessID] = '{0}' and [SystemID]= '{1}' and [Status] ='{1}0201213000001')", FabMoProcessID, Framework.SystemID);

            count = UniversalService.getCount(sql,null);
            DataTable dt = UniversalService.getTable(select, sql, orderby, null, page, rows);
           // DataTable dt = SQLHelper.ExecuteDataTable(sql + orderby, CommandType.Text);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// �����Ƴ���ˮ�Ż����Ƴ̹�����ˮ�Ż�ȡ�����ڵ�Bom(����ҳ)
        /// </summary>
        /// MOUSE 2017��8��2��16:22:34 �޸�Ϊ��ҳ
        /// <param name="ItemProcessID"></param>
        /// <param name="ItemOperationID"></param>
        /// <returns></returns>
        public static IList<Hashtable> Sfc00001GetNoBomList(string ItemProcessID, string ItemOperationID, int page, int rows, ref int count)
        {
            string select = string.Format(
                @"select A.ItemID,A.Code,A.Name,A.Specification,A.Name+'/'+A.Specification as NameSpecification,
                (select [Name] from [SYS_Parameters] where A.Type = ParameterID) as Type   ");
            string sql= string.Format(@"from [SYS_Items] A
                  where A.[SystemID] = '{0}' and A.[Status] = '{0}0201213000001' and A.[Type] <> '{0}0201213000008'", Framework.SystemID);

            string orderby = "A.[Code]";

            if (!string.IsNullOrWhiteSpace(ItemOperationID))
                sql += string.Format(@" and A.[ItemID] not in (Select [ItemID] From [SFC_ItemMaterial] where [ItemOperationID] = '{0}' and [SystemID]= '{1}' and [Status] ='{1}0201213000001')", ItemOperationID, Framework.SystemID);
            else if (!string.IsNullOrWhiteSpace(ItemProcessID))
                sql += string.Format(@" and A.[ItemID] not in (Select [ItemID] From [SFC_ItemMaterial] where [ItemProcessID] = '{0}' and [SystemID]= '{1}' and [Status] ='{1}0201213000001')", ItemProcessID, Framework.SystemID);

            count = UniversalService.getCount(sql, null);
            DataTable dt = UniversalService.getTable(select, sql, orderby, null, page, rows);

            return ToHashtableList(dt);
        }



        /// <summary>
        /// ��ȡ������Ʒ�Ƴ��趨�е���Ʒ�б�
        /// SAM 2017��7��18��15:30:20
        /// </summary>
        /// <param name="Code"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> Inf00010GetVaildList(string Code, int page, int rows, ref int count)
        {
            string select = string.Format(
                @"select DISTINCT A.ItemID, (case when D.Status = '{0}0201213000001' then 1 else 0 end) as LotStatus, 
                         (case when A.[LotMethod] = '{0}02012130000B5' then 'true' else 'false' end) as IsMLotMethod ,
                         A.[LotClassID],A.[Lot],C.Name as Type,A.Comments,A.Code,A.Name,A.Specification, 
                         A.Unit, E.Name as UnitName,A.OverRate ",
                Framework.SystemID);

            string sql = string.Format(@" from [SYS_Items] A 
            left join [SYS_Parameters] C on A.Type = C.ParameterID
            left join [SYS_AutoNumber] D on A.LotClassID = D.AutoNumberID
            left join [SYS_Parameters] E on A.Unit = E.ParameterID
            where A.[SystemID]='{0}' and A.[Status] = '{0}0201213000001' and A.[Type] <> '{0}020121300000A' ", Framework.SystemID);

            sql += string.Format(@"and A.[ItemID] in (select [ItemID] from [SFC_ItemProcess] where [Status]='{0}0201213000001')",Framework.SystemID);


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

            string orderby = "A.[Code],A.[Type] ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, parameters, page, rows);

            return ToHashtableList(dt);
        }


        /// <summary>
        /// ��ȡ������������Ʒ
        /// SAM 2017��10��4��00:21:07
        /// </summary>
        /// <returns></returns>
        public static IList<SYS_Items> GetList()
        {
            string sql = string.Format(@"select * from [SYS_Items] where [SystemID] = '{0}'  and [Status]='{0}0201213000001' ", Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return null;
            else
                return ToList(dt);
        }

        /// <summary>
        /// ��ȡ���е�δ�������Ⱥ�������Ʒ�б�
        /// SAM 2017��10��18��10:52:15
        /// </summary>
        /// <param name="StartCode"></param>
        /// <param name="EndCode"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> Qcs00003ItemListV2(string StartCode, string EndCode, int page, int rows, ref int count)
        {
            string select = string.Format(@"select A.ItemID,A.Code,A.Name,
            (Select Name from [SYS_Parameters] where [ParameterID] = A.[Type]) as Type,A.Specification ");

            string sql = string.Format(@"  from [SYS_Items] A 
            where A.[SystemID]='{0}' and A.[Status] = '{0}0201213000001' and A.[GroupID] is null", Framework.SystemID);

            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@StartCode",SqlDbType.VarChar),
                new SqlParameter("@EndCode",SqlDbType.VarChar)
            };
            parameters[0].Value = DBNull.Value;
            parameters[1].Value = DBNull.Value;

            SqlParameter[] Parcount = new SqlParameter[]{
                new SqlParameter("@StartCode",SqlDbType.VarChar),
                new SqlParameter("@EndCode",SqlDbType.VarChar)
            };
            Parcount[0].Value = DBNull.Value;
            Parcount[1].Value = DBNull.Value;

            if (!string.IsNullOrWhiteSpace(StartCode))
            {
                sql = sql + string.Format(@" and A.[Code] >= @StartCode ");
                parameters[0].Value = StartCode.Trim();
                Parcount[0].Value = StartCode.Trim();
            }

            if (!string.IsNullOrWhiteSpace(EndCode))
            {
                sql = sql + string.Format(@" and A.[Code] <= @EndCode ");
                parameters[1].Value = EndCode.Trim();
                Parcount[1].Value = EndCode.Trim();
            }

            count = UniversalService.getCount(sql, Parcount);

            string orderBy = "A.[Code] ";

            DataTable dt = UniversalService.getTable(select, sql, orderBy, parameters, page, rows);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// ���ݼ���Ⱥ�����ȡ������Ʒ�趨������ҳ��
        /// Sam 2017��10��18��14:08:51
        /// </summary>
        /// <param name="GroupID"></param>
        /// <returns></returns>
        public static IList<Hashtable> Qcs00003DetailsListV2(string GroupID)
        {
            string select = string.Format(@"select A.ItemID,A.Comments,A.GroupID,
            A.Code,A.Name,A.Specification,
            (Select Name from [SYS_Parameters] where [ParameterID] = A.[Type]) as Type,
            B.UserName as Creator,A.CreateLocalTime as CreateTime,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else C.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ");

            string sql = string.Format(@"  from [SYS_Items] A 
            left join [SYS_MESUsers] B on A.[Creator] = B.[MESUserID]
            left join [SYS_MESUsers] C on A.[Modifier] = C.[MESUserID]
            where A.[SystemID]='{0}' and A.[Status] = '{0}0201213000001' and A.[GroupID] = '{1}'", Framework.SystemID, GroupID);

            string orderBy = "order By A.[Code] ";

            DataTable dt = SQLHelper.ExecuteDataTable(select + sql + orderBy, CommandType.Text);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// ���ݼ���Ⱥ����ˮ��+��Ʒ��ˮ�ż��ϣ�Ϊ�����ڵ�������Ʒ�������ϼ���Ⱥ��š�
        /// SAM 2017��10��18��14:28:03
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="Items"></param>
        /// <param name="GroupID"></param>
        /// <returns></returns>
        public static bool Qcs00003UpdateAdd(string userId, string Items,string GroupID)
        {
            try
            {
                string sql = String.Format(@"update[SYS_Items] set {0},[GroupID]='{1}'
                where [ItemID] in ('{2}')", UniversalService.getUpdateUTC(userId), GroupID, Items);

                return SQLHelper.ExecuteNonQuery(sql, CommandType.Text) > 0;
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return false;
            }
        }

        /// <summary>
        /// ���ݼ���Ⱥ����ˮ��+��Ʒ��ˮ�ż��ϣ��������������Ⱥ���飬����ȴ���ڼ����ڵ���Ʒ�������Ⱥ���뽫���ó�null
        /// Sam 2017��10��18��14:29:47
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="Items"></param>
        /// <param name="GroupID"></param>
        /// <returns></returns>
        public static bool Qcs00003UpdateDelete(string userId, string Items, string GroupID)
        {
            try
            {
                string sql = String.Format(@"update[SYS_Items] set {0},[GroupID] = NULL
                where [ItemID] not in ('{2}') and [GroupID]='{1}'", UniversalService.getUpdateUTC(userId), GroupID,Items);

                return SQLHelper.ExecuteNonQuery(sql, CommandType.Text) > 0;
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return false;
            }
        }

        /// <summary>
        /// Sfc00017����Ʒ����
        /// SAM 2017��10��11��17:40:16
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="Code"></param>
        /// <param name="Name"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public static IList<Hashtable> Sfc00017GetItemList(string Code, string Name, int page, int rows, ref int count)
        {
            string select = string.Format(@"select A.ItemID,A.Code,A.Name,
            A.Specification,D.Name as Unit,E.Name as Type,F.Name as Status,A.Comments,
            B.UserName as Creator,A.CreateLocalTime as CreateTime,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else C.UserName END) as Modifier,
            (CASE WHEN A.CreateLocalTime=A.ModifiedLocalTime THEN NULL else A.ModifiedLocalTime END) as ModifiedTime ");

            string sql = string.Format(@" from [SYS_Items] A 
            left join [SYS_MESUsers] B on A.Creator = B.MESUserID
            left join [SYS_MESUsers] C on A.Modifier = C.MESUserID
            left join [SYS_Parameters] D on A.Unit = D.ParameterID
            left join [SYS_Parameters] J on A.AuxUnit = J.ParameterID
            left join [SYS_Parameters] E on A.Type = E.ParameterID
            left join [SYS_Parameters] F on A.Status = F.ParameterID
            where A.[SystemID]='{0}' and A.Status='{0}0201213000001' and A.[Type] <> '{0}020121300000A' ", Framework.SystemID);

            SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@Code",SqlDbType.VarChar),
                new SqlParameter("@Name",SqlDbType.VarChar)
            };
            parameters[0].Value = DBNull.Value;
            parameters[1].Value = DBNull.Value;

            SqlParameter[] Parcount = new SqlParameter[]{
                new SqlParameter("@Code",SqlDbType.VarChar),
                new SqlParameter("@Name",SqlDbType.VarChar)
            };
            Parcount[0].Value = DBNull.Value;
            Parcount[1].Value = DBNull.Value;

            if (!string.IsNullOrWhiteSpace(Code))
            {
                sql = sql + String.Format(@" and A.[Code] collate Chinese_PRC_CI_AS > @Code ");
                parameters[0].Value = Code.Trim();
                Parcount[0].Value = Code.Trim();
            }

            if (!string.IsNullOrWhiteSpace(Name))
            {
                Name = "%" + Name.Trim() + "%";
                sql = sql + String.Format(@" and A.[Name] collate Chinese_PRC_CI_AS like @Name ");
                parameters[1].Value = Name;
                Parcount[1].Value = Name;
            }

            count = UniversalService.getCount(sql, Parcount);

            string orderby = "A.[Status],A.[Code] ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, parameters, page, rows);

            return ToHashtableList(dt);
        }

        /// <summary>
        /// ��ȡ��Ʒ�б�
        /// SAM 2017��10��20��15:38:10
        /// ��Ӧ��̬Ϊ�ɹ������ϺŲ�ץȡ
        /// CodeΪ������ʼ�����ѯ������������
        /// </summary>
        /// <param name="Code"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<Hashtable> SfcItemList(string Code, int page, int rows, ref int count)
        {
            string select = string.Format(@"select A.ItemID,A.Code,A.Name,A.OverRate,
            A.Specification,D.Name as Unit,E.Name as Type,F.Name as Status,A.Comments,A.Drawing,
            (Select Name from [SYS_Parameters] where [ParameterID] = A.[PartSource]) as PartSource ");

            string sql = string.Format(@" from [SYS_Items] A 
            left join [SYS_Parameters] D on A.Unit = D.ParameterID
            left join [SYS_Parameters] J on A.AuxUnit = J.ParameterID
            left join [SYS_Parameters] E on A.Type = E.ParameterID
            left join [SYS_Parameters] F on A.Status = F.ParameterID
            where A.[SystemID]='{0}' and A.Status='{0}0201213000001' and A.[Type] <> '{0}020121300000A' ", Framework.SystemID);

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
                sql = sql + String.Format(@" and A.[Code] collate Chinese_PRC_CI_AS > @Code ");
                parameters[0].Value = Code.Trim();
                Parcount[0].Value = Code.Trim();
            }

            count = UniversalService.getCount(sql, Parcount);

            string orderby = "A.[Code] ";

            DataTable dt = UniversalService.getTable(select, sql, orderby, parameters, page, rows);

            return ToHashtableList(dt);
        }    }

}

