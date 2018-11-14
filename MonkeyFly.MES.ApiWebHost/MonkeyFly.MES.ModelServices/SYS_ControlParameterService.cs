using MonkeyFly.Core;
using MonkeyFly.MES.BasicService;
using MonkeyFly.MES.Models;
using MonkeyFly.Model;
using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace MonkeyFly.MES.ModelServices
{
    public class SYS_ControlParameterService : SuperModel<SYS_ControlParameters>
    {
        /// <summary>
        /// 根据参数流水号获取单一的记录
        /// SAM 2017年7月5日10:18:35
        /// </summary>
        /// <param name="ParameterID"></param>
        /// <returns></returns>
        public static Hashtable Trn00001Get(string ParameterID)
        {
            String select = String.Format(@"select Top 1 A.[value] as System,
            (select [value] from [SYS_ControlParameters] where [ParameterTypeID] = '{0}' and [Key]='Dbms' and [Name] = '{1}' ) as Dbms,
            (select [value] from [SYS_ControlParameters] where [ParameterTypeID] = '{0}' and [Key]='DBName' and [Name] = '{1}' ) as DBName,
            (select [value] from [SYS_ControlParameters] where [ParameterTypeID] = '{0}' and [Key]='DBUser' and [Name] = '{1}' ) as DBUser,
            (select [value] from [SYS_ControlParameters] where [ParameterTypeID] = '{0}' and [Key]='DBPassword' and [Name] = '{1}' ) as DBPassword,
            (select [value] from [SYS_ControlParameters] where [ParameterTypeID] = '{0}' and [Key]='RetriveTime' and [Name] = '{1}' ) as RetriveTime,
            (select [value] from [SYS_ControlParameters] where [ParameterTypeID] = '{0}' and [Key]='Remark' and [Name] = '{1}' ) as Remark,
            (select [value] from [SYS_ControlParameters] where [ParameterTypeID] = '{0}' and [Key]='StartExchange' and [Name] = '{1}' ) as StartExchange", Framework.SystemID + "0191213000020", ParameterID);

            String sql = String.Format(@" from [SYS_ControlParameters] A 
            where [ParameterTypeID] = '{0}' and [PageNumber]='TRN00001' and [Key]='System' and [Name] = '{1}' ", Framework.SystemID + "0191213000020", ParameterID);

            DataTable dt = SQLHelper.ExecuteDataTable(select + sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return null;

            return ToHashtableList(dt)[0];
        }


        /// <summary>
        /// 添加SQL 
        /// SAM 2016年10月10日19:49:49
        /// TODO
        /// </summary>
        /// <returns></returns>
        public static string insertSQL(string userid, SYS_ControlParameters par, string ControlParameterID, string key, string value, int DateType)
        {
            string sql = string.Format(@"insert [SYS_ControlParameters](ControlParameterID,
            Name,Description,DescriptionOne,[Key],value,DateType,ParameterTypeID,PageNumber,IsEnable,IsDefault,Sequence,
            Comments,SystemID,Modifier,ModifiedTime,ModifiedLocalTime,Creator,CreateTime,CreateLocalTime) values 
            ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}',{13});",
           ControlParameterID,
           par.Name,
           par.Description,
           par.DescriptionOne,
           key,
           value,
           DateType,
           par.ParameterTypeID,
           par.PageNumber,
           par.IsEnable,
           par.IsDefault,
           par.Sequence,
           par.Comments,
           UniversalService.getInsertnew(userid));

            return sql;
        }

        /// <summary>
        /// 更新SQL
        /// SAM 2016年10月10日23:19:23
        /// TODO 待完善
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="OrganizationID"></param>
        /// <param name="value"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string updateSQL(string userid,string Name,string key, string value)
        {
            string sql = String.Format(@"update [SYS_ControlParameters] set {0},[value]='{1}'
                              where [Name]='{2}' and [Key]='{3}';", UniversalService.getUpdate(userid), value, Name, key);

            return sql;
        }


        /// <summary>
        ///  更新SQL（NEW）（Name=Key）
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="OrganizationID"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string updateSQLNew(string userid, string OrganizationID, string TypeID, string key, string value)
        {
            string sql = String.Format(@"update [SYS_ControlParameters] set {0},[value]='{1}'
                              where [OrganizationID]='{2}' and [Name]='{3}' and [Key]='{3}' and [ParameterTypeID]='{4}';", UniversalService.getUpdate(userid), value, OrganizationID, key, TypeID);

            return sql;
        }

        /// <summary>
        /// 根据Name和pageNumber删除控制参数
        /// SAM 2016年10月11日15:43:00 
        /// </summary>
        /// <param name="Name">编号</param>
        /// <param name="pageNumber">页面代码</param>
        /// <returns></returns>
        public static bool delete(string Name, string pageNumber)
        {
            try
            {
                string sql = String.Format(@"delete from [SYS_ControlParameters] where [Name] = @Name and [pageNumber] =@pageNumber and SystemID='{0}' ", Framework.SystemID);
                SqlParameter[] parameters = new SqlParameter[]{
                new SqlParameter("@Name",SqlDbType.VarChar),
                new SqlParameter("@pageNumber",SqlDbType.VarChar),
                };
                parameters[0].Value = Name;
                parameters[1].Value = pageNumber;

                return SQLHelper.ExecuteNonQuery(sql, CommandType.Text, parameters) > 0;
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return false;
            }
        }

        /// <summary>
        /// 检查指定公司的指定参数类型是否存在数据
        /// SAM 2016年10月30日21:12:17
        /// </summary>
        /// <param name="CompanyID"></param>
        /// <param name="ParameterTypeID"></param>
        /// <returns></returns>
        public static bool CheckControlParameter(String CompanyID, string ParameterTypeID)
        {
            String sql = String.Format(@"select * from [SYS_ControlParameters] where [OrganizationID]='{0}' and [ParameterTypeID]='{1}'  and [SystemID]='{2}' ", CompanyID, ParameterTypeID, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text, null);

            if (dt.Rows.Count == 0)
                return false;
            else
                return true;
        }

        /// <summary>
        /// 执行SQL，返回执行结果
        /// SAM 2016年10月10日19:49:04
        /// TODO
        /// </summary>
        /// <param name="SQL"></param>
        /// <returns></returns>
        public static bool RunSQL(string SQL)
        {
            try
            {
                return SQLHelper.ExecuteNonQuery(SQL, CommandType.Text) > 0;
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return false;
            }
        }


        /// <summary>
        /// 根据公司别，参数别，字段，页码获取指定控制参数
        /// SAM 2017年1月6日23:32:08
        /// </summary>
        /// <param name="CompanyID"></param>
        /// <param name="ParameterTypeID"></param>
        /// <param name="Name"></param>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        public static SYS_ControlParameters get(string CompanyID, string ParameterTypeID,string Name, string pageNumber)
        {
            String sql = String.Format(@"select * from [SYS_ControlParameters] where [OrganizationID]='{0}' and [ParameterTypeID]='{1}'  and [SystemID]='{2}' and [PageNumber] = '{3}' and [Name] = '{4}'", CompanyID, ParameterTypeID, Framework.SystemID, pageNumber, Name);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text, null);

            if (dt.Rows.Count == 0)
                return null;
            else
                return ToEntity(dt.Rows[0]);
        }


        /// <summary>
        /// 获取供应系统参数设定
        /// SAM 2016年10月22日17:24:49
        /// </summary>
        /// <param name="company"></param>
        /// <returns></returns>
        public static Hashtable Sys20010GetArgs(string company)
        {
            String select = String.Format(@"select Top 1 A.[value] as PurchaseStandAlone,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='PurchaseDate') as PurchaseDate,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='PAPDNP') as PAPDNP,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='DOTMP') as DOTMP,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='DSTPOUPO') as DSTPOUPO,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='IsUnitPrice') as IsUnitPrice,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='IsNum') as IsNum,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='IsAmt') as IsAmt,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='NeedUnitPrice') as NeedUnitPrice,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='IsPurchaseNum') as IsPurchaseNum,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='IsPurchaseUnitPrice') as IsPurchaseUnitPrice,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='PurchaseIssued') as PurchaseIssued,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='NumDecimal') as NumDecimal,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='UnitPriceDecimal') as UnitPriceDecimal,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='PAAARPM') as PAAARPM,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='PAAUPQ') as PAAUPQ,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='PRTTNOP') as PRTTNOP,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='PurCP') as PurCP,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='Intrate') as Intrate,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='ICSP') as ICSP,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='ICC') as ICC,
            (select [Code] from [SYS_Organization] Where [OrganizationID]=(select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='ICC')) as ICCCode,
            (select [Name] from [SYS_Organization] Where [OrganizationID]=(select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='ICC')) as ICCName ", company, Framework.SystemID + "0191213000018");

            String sql = String.Format(@" from [SYS_ControlParameters] A where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [PageNumber]='SYS20010' and [Name]='PurchaseStandAlone' ", company, Framework.SystemID + "0191213000018");

            DataTable dt = SQLHelper.ExecuteDataTable(select + sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return null;

            return ToHashtableList(dt)[0];
        }

       /// <summary>
       /// 供应系统参数设定-库存管理
       /// SAM 2016年12月22日14:29:39
       /// </summary>
       /// <param name="company"></param>
       /// <returns></returns>
        public static Hashtable Sys20010GetInventory(string company)
        {
            String select = String.Format(@"select Top 1 A.[value] as Inventory,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='SetPrinciple') as SetPrinciple,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='Classification') as Classification,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='CostOption') as CostOption,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='ProportionA') as ProportionA,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='ProportionB') as ProportionB,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='ProportionC') as ProportionC,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='SAFGNCIOEAGMN') as SAFGNCIOEAGMN,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='ICTSEMDREIA') as ICTSEMDREIA,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='AIAASetingSystEntry') as AIAASetingSystEntry,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='AIAASetingSystPosting') as AIAASetingSystPosting,
           
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='AllowTaking') as AllowTaking,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='IsInvByBin') as IsInvByBin,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='InventoryTaking') as InventoryTaking,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='IsPass') as IsPass,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='MethodBy') as MethodBy ", company, Framework.SystemID + "0191213000119");

            String sql = String.Format(@" from [SYS_ControlParameters] A where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [PageNumber]='SYS20010' and [Name]='Inventory' ", company, Framework.SystemID + "0191213000119");

            DataTable dt = SQLHelper.ExecuteDataTable(select + sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return null;

            return ToHashtableList(dt)[0];
        }

        //TODO
        public static Hashtable Cac10100GetSubjectNumber(string company)
        {
            String select = String.Format(@"select Top 1 A.[value] as macclen,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='dacclen') as dacclen,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='codelev') as codelev ", company, Framework.SystemID + "0191213000022");

            String sql = String.Format(@" from [SYS_ControlParameters] A where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [PageNumber]='CAC10100' and [Name]='macclen' ", company, Framework.SystemID + "0191213000022");

            DataTable dt = SQLHelper.ExecuteDataTable(select + sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return null;

            return ToHashtableList(dt)[0];
        }

        /// <summary>
        /// 获取系統共用參數設定-料品代号
        /// SAM 2016年11月24日21:29:23
        /// </summary>
        /// <param name="company"></param>
        /// <returns></returns>
        public static Hashtable Sys20000GetList(string company)
        {
            String select = String.Format(@"select Top 1 A.[value] as ItemArchiveWay,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='PhantomMaintAndChg') as PhantomMaintAndChg,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='SubMaintAndChg') as SubMaintAndChg,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='MGMaintAndChg') as MGMaintAndChg,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='ItemAdoptMultFTYMaint') as ItemAdoptMultFTYMaint,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='ItemDigitCountSetup') as ItemDigitCountSetup,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='FGUnitSNCtl') as FGUnitSNCtl,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='SelectStructureToMaintainNegativeValue') as SelectStructureToMaintainNegativeValue,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='ItemStdDmgCtl') as ItemStdDmgCtl,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='IsRdDnSetupPURItem') as IsRdDnSetupPURItem,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='RdDnSetupPURItemBits') as RdDnSetupPURItemBits,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='IsRdDnSetupSUBItem') as IsRdDnSetupSUBItem,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='RdDnSetupSUBItemBits') as RdDnSetupSUBItemBits,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='IsRdDnSetupSelfMadeItem') as IsRdDnSetupSelfMadeItem,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='RdDnSetupSelfMadeItemBits') as RdDnSetupSelfMadeItemBits,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='IsSADPolicySetupPURItem') as IsSADPolicySetupPURItem,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='IsSADPolicySetupPURItemOTM') as IsSADPolicySetupPURItemOTM,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='IsSADPolicySetupSUBItem') as IsSADPolicySetupSUBItem,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='IsSADPolicySetupSUBItemOTM') as IsSADPolicySetupSUBItemOTM,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='IsSADPolicySetupSelfMadeItem') as IsSADPolicySetupSelfMadeItem,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='IsSADPolicySetupSelfMadeItemOTM') as IsSADPolicySetupSelfMadeItemOTM,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='IsSADPolicySetupFG') as IsSADPolicySetupFG,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='IsSADPolicySetupFGOTM') as IsSADPolicySetupFGOTM,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='ItemPkgUnitOfMeasClSetup') as ItemPkgUnitOfMeasClSetup,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='ItemCharDefaultTRDBusiness') as ItemCharDefaultTRDBusiness,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='ChildAltMustBeExistedIn') as ChildAltMustBeExistedIn,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='AllowMdseInput') as AllowMdseInput,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='FinProdItem') as FinProdItem,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='MFGItem') as MFGItem,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='SUBItem') as SUBItem ", company, Framework.SystemID + "0191213000100");

            String sql = String.Format(@" from [SYS_ControlParameters] A where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [PageNumber]='SYS20000' and [Name]='ItemArchiveWay' ", company, Framework.SystemID + "0191213000100");

            DataTable dt = SQLHelper.ExecuteDataTable(select + sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return null;

            return ToHashtableList(dt)[0];
        }

        /// <summary>
        /// 获取系統共用參數設定-规划作业
        /// SAM 2016年11月24日21:50:06
        /// </summary>
        /// <param name="company"></param>
        /// <returns></returns>
        public static Hashtable Sys20000GetOperationsList(string company)
        {
            String select = String.Format(@"select Top 1 A.[value] as MRP,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='MCL') as MCL,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='DPALAAM') as DPALAAM, 
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='MPMRPSADOM') as MPMRPSADOM,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='COCC') as COCC 
", company, Framework.SystemID + "0191213000101");

            String sql = String.Format(@" from [SYS_ControlParameters] A where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [PageNumber]='SYS20000' and [Name]='MRP' ", company, Framework.SystemID + "0191213000101");

            DataTable dt = SQLHelper.ExecuteDataTable(select + sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return null;

            return ToHashtableList(dt)[0];
        }

        /// <summary>
        /// 获取营销系统参数设定-营销共用
        /// SAM 2016年12月2日23:39:01 
        /// </summary>
        /// <param name="company"></param>
        /// <returns></returns>
        public static Hashtable Sys10610Marketingsharing(string company)
        {
            String select = String.Format(@"select Top 1 A.[value] as Marketingsharing,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='CustCrLineCtlPt') as CustCrLineCtlPt,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='OrdRLSDirectActiveProdUnitAlLocOrdMat') as OrdRLSDirectActiveProdUnitAlLocOrdMat,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='SalesIncBusShipMode') as SalesIncBusShipMode,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='OrderChangeWay') as OrderChangeWay,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='TransUPSelBase') as TransUPSelBase,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='ActiveCustSpecAppSellPr') as ActiveCustSpecAppSellPr,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='CoMatNoCorrCustMatNo') as CoMatNoCorrCustMatNo,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='AOrdAvailableFSISDlvyPeriod') as AOrdAvailableFSISDlvyPeriod,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='OrderItemSelectDefault') as OrderItemSelectDefault,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='OrderPackingSet') as OrderPackingSet,
           
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='AllowSPartBatchShip') as AllowSPartBatchShip,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='ORADIProjControlSetup') as ORADIProjControlSetup,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='SPartInc') as SPartInc,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='ActiveCombSalesFunc') as ActiveCombSalesFunc,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='AllowChangeOrderGroupDetailedUnitUsage') as AllowChangeOrderGroupDetailedUnitUsage,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='AllowNewOrderGroupDetail') as AllowNewOrderGroupDetail,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='EnableTheShippingNotPro') as EnableTheShippingNotPro,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='AdviceDetAllowVarWHOp') as AdviceDetAllowVarWHOp,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='ShipAdviceReqOutbFTYCtl') as ShipAdviceReqOutbFTYCtl,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='ShipAdviceBWDftVal') as ShipAdviceBWDftVal,
            (select [Code] from [SYS_Organization] Where [OrganizationID]=(select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='ShipAdviceBWDftVal')) as ShipAdviceBWDftValCode,
           
            (select [Code] from [SYS_Organization] Where [OrganizationID]=(select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='ShipAdviceWHDftVal')) as ShipAdviceWHDftValCode,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='ShipAdviceWHDftVal') as ShipAdviceWHDftVal,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='ShipAdviceOpAmtReveal') as ShipAdviceOpAmtReveal,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='NotOpeConProConGenShippingOrder') as NotOpeConProConGenShippingOrder,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='ShippingAdvSndShippingOrderAllowSPart') as ShippingAdvSndShippingOrderAllowSPart,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='HandBalShortageGenTfrOrd') as HandBalShortageGenTfrOrd,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='ShipDetCount') as ShipDetCount,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='ShipAdviceConvToShipChg') as ShipAdviceConvToShipChg,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='ForTRDShipActivateCustomsExchRate') as ForTRDShipActivateCustomsExchRate,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='ForTRDShipReqInvIss') as ForTRDShipReqInvIss,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='InvGenByDept') as InvGenByDept,
           
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='ComputerIssInvDSel') as ComputerIssInvDSel,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='TradeDataConToInDeclarationFileBy') as TradeDataConToInDeclarationFileBy,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='TASSOCTOTUSExchangeRate') as TASSOCTOTUSExchangeRate,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='SOISCLForTRDDociAallowedTCchange') as SOISCLForTRDDociAallowedTCchange,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='CustPOSaveSet') as CustPOSaveSet,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='RepetitiveCustPO') as RepetitiveCustPO,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='RepetitiveShipCustPO') as RepetitiveShipCustPO,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='TimePmtConvToInvMethod') as TimePmtConvToInvMethod,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='BeforeConToRecTimePaymentsMustInvoice') as BeforeConToRecTimePaymentsMustInvoice ", company, Framework.SystemID + "0191213000102");

            String sql = String.Format(@" from [SYS_ControlParameters] A where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [PageNumber]='SYS10610' and [Name]='Marketingsharing' ", company, Framework.SystemID + "0191213000102");

            DataTable dt = SQLHelper.ExecuteDataTable(select + sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return null;

            return ToHashtableList(dt)[0];
        }

        /// <summary>
        /// 获取营销系统参数设定-单价数量金额设定
        /// SAM 2016年12月3日17:58:58
        /// </summary>
        /// <param name="company"></param>
        /// <returns></returns>
        public static Hashtable Sys10610UnitPriceSetting(string company)
        {
            String select = String.Format(@"select Top 1 A.[value] as UnitPriceSetting,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='ShipOrdChgUP') as ShipOrdChgUP,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='ShipOrdChgAmt') as ShipOrdChgAmt,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='QtyDeciPlEntrySetup') as QtyDeciPlEntrySetup,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='UPDeciPlEntrySetup') as UPDeciPlEntrySetup,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='DetTaxRateToBeTheSame') as DetTaxRateToBeTheSame,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='History') as History,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='App') as App,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='Std') as Std,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='ConsignSRUPAcquiredWay') as ConsignSRUPAcquiredWay ", company, Framework.SystemID + "0191213000103");

            String sql = String.Format(@" from [SYS_ControlParameters] A where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [PageNumber]='SYS10610' and [Name]='UnitPriceSetting' ", company, Framework.SystemID + "0191213000103");

            DataTable dt = SQLHelper.ExecuteDataTable(select + sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return null;

            return ToHashtableList(dt)[0];
        }

        /// <summary>
        /// 获取营销系统参数设定-销售分析
        /// SAM 2016年12月4日21:32:41
        /// </summary>
        /// <param name="company"></param>
        /// <returns></returns>
        public static Hashtable Sys10610SalesAnalysis(string company)
        {
            String select = String.Format(@"select Top 1 A.[value] as SalesAnalysis,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='UpateSAAmout') as UpateSAAmout,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='UpateSAMethod') as UpateSAMethod ", company, Framework.SystemID + "0191213000106");

            String sql = String.Format(@" from [SYS_ControlParameters] A where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [PageNumber]='SYS10610' and [Name]='SalesAnalysis' ", company, Framework.SystemID + "0191213000106");

            DataTable dt = SQLHelper.ExecuteDataTable(select + sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return null;

            return ToHashtableList(dt)[0];
        }

        /// <summary>
        /// 開票/匯款參數檔-参数设定
        /// SAM 2016年12月3日15:01:52
        /// </summary>
        /// <param name="company"></param>
        /// <returns></returns>
        public static Hashtable ANP10100GetPAPSetupData(string company)
        {
            String select = String.Format(@"select Top 1 A.[value] as PAPSetupData,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='ATTBRBP') as ATTBRBP,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='UsanceRemDSel') as UsanceRemDSel,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='FixedD') as FixedD,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='BankDepRecSNL') as BankDepRecSNL,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='PostalRemEntry') as PostalRemEntry,
            (select [Name] from [SYS_Parameters] where ParameterID = (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='PostalRemEntry')) as PostalRemEntryCode,
            (select [Description] from [SYS_Parameters] where ParameterID = (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='PostalRemEntry')) as PostalRemEntryCodeDesp,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='MMSBDFTPS') as MMSBDFTPS,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='PostalRemDept') as PostalRemDept,
            (select [Code] from [SYS_Organization] where OrganizationID = (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='PostalRemDept')) as PostalRemDeptCode,
            (select [Name] from [SYS_Organization] where OrganizationID = (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='PostalRemDept')) as PostalRemDeptName,      
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='RemittanceSlip') as RemittanceSlip,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='LengthOfPay') as LengthOfPay,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='RemittanceReversalEntry') as RemittanceReversalEntry,
            (select [Name] from [SYS_Parameters] where ParameterID = (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='RemittanceReversalEntry')) as RemittanceReversalEntryCode,
            (select [Description] from [SYS_Parameters] where ParameterID = (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='RemittanceReversalEntry')) as RRECDescription,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='PayableRemittanceRntry') as PayableRemittanceRntry,
            (select [Name] from [SYS_Parameters] where ParameterID = (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='PayableRemittanceRntry')) as PayableRemittanceRntryCode,
            (select [Description] from [SYS_Parameters] where ParameterID = (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='PayableRemittanceRntry')) as PRRCDescription
    
            ", company, Framework.SystemID + "0191213000104");

            String sql = String.Format(@" from [SYS_ControlParameters] A where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [PageNumber]='ANP10100' and [Name]='PAPSetupData' ", company, Framework.SystemID + "0191213000104");

            DataTable dt = SQLHelper.ExecuteDataTable(select + sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return null;

            return ToHashtableList(dt)[0];
        }

        /// <summary>
        /// 获取用品系統參數設定-基本设定
        /// SAM 2016年12月7日10:02:04
        /// </summary>
        /// <param name="company"></param>
        /// <returns></returns>
        public static Hashtable Gap10000BasicSetting(string company)
        {
            String select = String.Format(@"select Top 1 A.[value] as BasicSetting,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='APRRSConfirmProcedure') as APRRSConfirmProcedure,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='PRCTPURRequirePricingConfirm') as PRCTPURRequirePricingConfirm,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='IICPURRAPricingUPCanBeChg') as IICPURRAPricingUPCanBeChg,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='PURRAPricingCTPURMayChgUP') as PURRAPricingCTPURMayChgUP,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='PURRAPCTPURSUIDiffVendors') as PURRAPCTPURSUIDiffVendors,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='WPOIRwriteUP') as WPOIRwriteUP,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='UPColumnChg') as UPColumnChg,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='POChgUPLow') as POChgUPLow,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='AmtColumnChg') as AmtColumnChg,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='QtyColumnAdjustable') as QtyColumnAdjustable,
            
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='POChgQtyLow') as POChgQtyLow,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='QtyDeciPlEntrySetup') as QtyDeciPlEntrySetup,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='UPDeciPlEntrySetup') as UPDeciPlEntrySetup,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='PURChgProcMethod') as PURChgProcMethod,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='PODetJoinProcAcptPolicy') as PODetJoinProcAcptPolicy,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='NPURItemRecOpExpiryDate') as NPURItemRecOpExpiryDate,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='DesigJoinProcAcpt') as DesigJoinProcAcpt,
          
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='DesigDept') as DesigDept,
            (select [Code] from [SYS_Organization] where OrganizationID = (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='DesigDept')) as DesigDeptCode,
            (select [Name] from [SYS_Organization] where OrganizationID = (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='DesigDept')) as DesigDeptName,      
           
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='DesigPer') as DesigPer,
            (select [Emplno] from [SYS_EMOUsers] where EMOUserID = (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='DesigPer')) as DesigPerCode,
            (select [UserName] from [SYS_EMOUsers] where EMOUserID = (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='DesigPer')) as DesigPerName,          

            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='ActRetWODocFunc') as ActRetWODocFunc,

            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='InfoNeedCk') as InfoNeedCk,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='AREADBCTAPRWorkTicketConfirm') as AREADBCTAPRWorkTicketConfirm,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='ItemInvJudgePolicy') as ItemInvJudgePolicy
       
            ", company, Framework.SystemID + "019121300010E");

            String sql = String.Format(@" from [SYS_ControlParameters] A where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [PageNumber]='GAP10000' and [Name]='BasicSetting' ", company, Framework.SystemID + "019121300010E");

            DataTable dt = SQLHelper.ExecuteDataTable(select + sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return null;

            return ToHashtableList(dt)[0];
        }

        /// <summary>
        /// 财务系统参数设定-应收系统
        /// SAM 2016年12月13日10:02:59
        /// </summary>
        /// <param name="company"></param>
        /// <returns></returns>
        public static Hashtable Sys20300ReceivableSystem(string company)
        {
            String select = String.Format(@"select Top 1 A.[value] as ReceivableSystem,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='ARAcctMethod') as ARAcctMethod,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='AcctOffset1stOut1stOffset') as AcctOffset1stOut1stOffset,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='MustBeWithInvNoForCollection') as MustBeWithInvNoForCollection,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='GenRtnOrdData') as GenRtnOrdData,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='ARSyst') as ARSyst,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='RecTraProInvBatProcInvoMaiMode') as RecTraProInvBatProcInvoMaiMode,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='RecTraTheCashTabAllowsYouToMaiMulAccounts') as RecTraTheCashTabAllowsYouToMaiMulAccounts
       
            ", company, Framework.SystemID + "0191213000112");

            String sql = String.Format(@" from [SYS_ControlParameters] A where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [PageNumber]='SYS20300' and [Name]='ReceivableSystem' ", company, Framework.SystemID + "0191213000112");

            DataTable dt = SQLHelper.ExecuteDataTable(select + sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return null;

            return ToHashtableList(dt)[0];
        }

        /// <summary>
        /// 财务系统参数设定-应付系统
        /// SAM 2016年12月13日10:05:52
        /// </summary>
        /// <param name="company"></param>
        /// <returns></returns>
        public static Hashtable Sys20300CopingSystem(string company)
        {
            String select = String.Format(@"select Top 1 A.[value] as CopingSystem,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='EnaDeptHeadToTransSubDeptExp') as EnaDeptHeadToTransSubDeptExp,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='AccountingMethed') as AccountingMethed,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='AcctOffset') as AcctOffset,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='AcptRtnDataToBeChecked') as AcptRtnDataToBeChecked,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='AcptRtnConvToAPSystInstUpDBal') as AcptRtnConvToAPSystInstUpDBal,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='SUBAcptRtnEntryCodConvToAP') as SUBAcptRtnEntryCodConvToAP,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='AcptReturnAmtConToAP') as AcptReturnAmtConToAP,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='InvNoReqForPmtReq') as InvNoReqForPmtReq,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='TaxBalEntryNo') as TaxBalEntryNo,
            (select [Name] from [SYS_Parameters] where ParameterID = (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='TaxBalEntryNo')) as TaxBalEntryNoCode,
            (select [Description] from [SYS_Parameters] where ParameterID = (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='TaxBalEntryNo')) as TaxBalEntryNoDesc,   
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='AmtBalEntryNo') as AmtBalEntryNo,
            (select [Name] from [SYS_Parameters] where ParameterID = (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='AmtBalEntryNo')) as AmtBalEntryNoCode,
            (select [Description] from [SYS_Parameters] where ParameterID = (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='AmtBalEntryNo')) as AmtBalEntryNoDesc,   

            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='PmtReqDirectBroughtIntoPmtAcctSVC') as PmtReqDirectBroughtIntoPmtAcctSVC,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='PmtReqNeedConfirmProc') as PmtReqNeedConfirmProc,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='APRecProcTaxEntryNoToBeModified') as APRecProcTaxEntryNoToBeModified,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='APSyst') as APSyst,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='SummIsModify') as SummIsModify,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='ActiveTentativeAP') as ActiveTentativeAP,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='IntConToAPCode') as IntConToAPCode,
            (select [Name] from [SYS_Parameters] where ParameterID = (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='IntConToAPCode')) as IntConToAPCodeCode,
            (select [Description] from [SYS_Parameters] where ParameterID = (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='IntConToAPCode')) as IntConToAPCodeDesc
       
            ", company, Framework.SystemID + "0191213000114");

            String sql = String.Format(@" from [SYS_ControlParameters] A where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [PageNumber]='SYS20300' and [Name]='CopingSystem' ", company, Framework.SystemID + "0191213000114");

            DataTable dt = SQLHelper.ExecuteDataTable(select + sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return null;

            return ToHashtableList(dt)[0];
        }

        /// <summary>
        /// 财务系统参数设定-总账介面
        /// SAM 2016年12月13日10:05:52
        /// </summary>
        /// <param name="company"></param>
        /// <returns></returns>
        public static Hashtable Sys20300GeneralLedgerParameters(string company)
        {
            String select = String.Format(@"select Top 1 A.[value] as GeneralLedgerParameters,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='GenWitBankAccountNoCanBeSetupAtDetailedEntry') as GenWitBankAccountNoCanBeSetupAtDetailedEntry,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='CanInsuffAcctBalBePosting') as CanInsuffAcctBalBePosting,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='IsChinaDrawOpSetup') as IsChinaDrawOpSetup,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='LoanWDLPmtReqAmtToBeChg') as LoanWDLPmtReqAmtToBeChg,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='DiscAddPay') as DiscAddPay,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='ExchRateSel') as ExchRateSel,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='PropmtTaxDeductWhilePayInt') as PropmtTaxDeductWhilePayInt,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='IntRate') as IntRate,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='TheExchangeRateFieldVisible') as TheExchangeRateFieldVisible,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='IsExchangeMonthProcessing') as IsExchangeMonthProcessing,
           
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='ExchPLEntryNoSetupInt') as ExchPLEntryNoSetupInt,
            (select [Name] from [SYS_Parameters] where ParameterID = (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='ExchPLEntryNoSetupInt')) as ExchPLEntryNoSetupIntCode,
            (select [Description] from [SYS_Parameters] where ParameterID = (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='ExchPLEntryNoSetupInt')) as ExchPLEntryNoSetupIntDesc,
       
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='ExchPLEntryNoSetupIntLoss') as ExchPLEntryNoSetupIntLoss,
            (select [Name] from [SYS_Parameters] where ParameterID = (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='ExchPLEntryNoSetupIntLoss')) as ExchPLEntryNoSetupIntLossCode,
            (select [Description] from [SYS_Parameters] where ParameterID = (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='ExchPLEntryNoSetupIntLoss')) as ExchPLEntryNoSetupIntLossDesc,
       
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='CkSetupEntryCheckingProcess') as CkSetupEntryCheckingProcess,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='CkSetupDebitEqualCredit') as CkSetupDebitEqualCredit,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='CkSetupLoanAmtEqualZero') as CkSetupLoanAmtEqualZero,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='IsAPVendor') as IsAPVendor,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='IsARCust') as IsARCust,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='TentativeSUBProcessExpEntryCode') as TentativeSUBProcessExpEntryCode,
            (select [Name] from [SYS_Parameters] where ParameterID = (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='TentativeSUBProcessExpEntryCode')) as TentativeSUBProcessExpEntryCodeCode,
            (select [Description] from [SYS_Parameters] where ParameterID = (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='TentativeSUBProcessExpEntryCode')) as TentativeSUBProcessExpEntryCodeDesc,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='ReqToDoBankCrLineSetup') as ReqToDoBankCrLineSetup,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='BankHeadOfficeCodeNumberSet') as BankHeadOfficeCodeNumberSet,
          
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='BankBranchCodeNumberSet') as BankBranchCodeNumberSet
       
            ", company, Framework.SystemID + "0191213000116");

            String sql = String.Format(@" from [SYS_ControlParameters] A where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [PageNumber]='SYS20300' and [Name]='GeneralLedgerParameters' ", company, Framework.SystemID + "0191213000116");

            DataTable dt = SQLHelper.ExecuteDataTable(select + sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return null;

            return ToHashtableList(dt)[0];
        }

        /// <summary>
        /// 财务系统参数设定-固定资产
        /// SAM 2016年12月13日10:05:52
        /// </summary>
        /// <param name="company"></param>
        /// <returns></returns>
        public static Hashtable Sys20300FixedAssets(string company)
        {
            String select = String.Format(@"select Top 1 A.[value] as FixedAssets,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='POGenMethod') as POGenMethod,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='POOpenEntrySuppExpiryDate') as POOpenEntrySuppExpiryDate,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='ResidValSetupMethod') as ResidValSetupMethod,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='MustbeBuildAPInAdvance') as MustbeBuildAPInAdvance,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='UPColumnChg') as UPColumnChg,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='WPOChangingUPLower') as WPOChangingUPLower,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='ChgAmtColumn') as ChgAmtColumn,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='QtyColumnAdjustable') as QtyColumnAdjustable,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='POChgOnlyForQtyAdjLow') as POChgOnlyForQtyAdjLow,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='QtyDeciPlEntrySetup') as QtyDeciPlEntrySetup,
            
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='UPDeciPlEntrySetup') as UPDeciPlEntrySetup,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='PURPricingConfirm') as PURPricingConfirm,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='AfterPriUPChg') as AfterPriUPChg,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='AfterPriConToPURMayChgUP') as AfterPriConToPURMayChgUP,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='PAfterPriConToPURMayChgVend') as PAfterPriConToPURMayChgVend,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='PODetJoinProcAcptPolicy') as PODetJoinProcAcptPolicy,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='DesigJoinProcAcpt') as DesigJoinProcAcpt,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='DesigDept') as DesigDept,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='DesigPer') as DesigPer,
            (select [Code] from [SYS_Organization] where OrganizationID = (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='DesigDept')) as DesigDeptCode,
            (select [Name] from [SYS_Organization] where OrganizationID = (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='DesigDept')) as DesigDeptName,      
            (select [Emplno] from [SYS_EMOUsers] where EMOUserID = (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='DesigPer')) as DesigPerCode,
            (select [UserName] from [SYS_EMOUsers] where EMOUserID = (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='DesigPer')) as DesigPerName,          
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='WhilePOReleasingWrite') as WhilePOReleasingWrite,
            
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='PURChgProcMethod') as PURChgProcMethod,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='AssetSpecDescMaintMethod') as AssetSpecDescMaintMethod,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='AssetName') as AssetName,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='Spec') as Spec,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='AcceptanceSystemPolicy') as AcceptanceSystemPolicy

       
            ", company, Framework.SystemID + "0191213000117");

            String sql = String.Format(@" from [SYS_ControlParameters] A where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [PageNumber]='SYS20300' and [Name]='FixedAssets' ", company, Framework.SystemID + "0191213000117");

            DataTable dt = SQLHelper.ExecuteDataTable(select + sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return null;

            return ToHashtableList(dt)[0];
        }

        /// <summary>
        /// 机型产销参数设定-其他设定
        /// SAM 2016年12月28日09:47:35
        /// </summary>
        /// <param name="company"></param>
        /// <returns></returns>
        public static Hashtable Mds10000OtherSetting(string company)
        {
            string select = string.Format(@"select Top 1 A.[value] as OtherSetting,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='MacSerNumMethod') as MacSerNumMethod,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='MPIRConfirmed') as MPIRConfirmed,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='DftPrefix') as DftPrefix
       
            ", company, Framework.SystemID + "0191213000121");

            string sql = string.Format(@" from [SYS_ControlParameters] A where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [PageNumber]='MDS10000' and [Name]='OtherSetting' ", company, Framework.SystemID + "0191213000121");

            DataTable dt = SQLHelper.ExecuteDataTable(select + sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return null;

            return ToHashtableList(dt)[0];
        }

        /// <summary>
        /// 產系統參數設定-委外管理
        /// SAM 2017年1月5日14:14:223
        /// </summary>
        /// <param name="company"></param>
        /// <returns></returns>
        public static Hashtable Sys20040SUBMGT(string company)
        {
            String select = String.Format(@"select Top 1 A.[value] as SUBMGT,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='SAPONPolicy') as SAPONPolicy,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='DOCTSCPolicy') as DOCTSCPolicy,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='SCAID') as SCAID,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='ACNMBARGC') as ACNMBARGC,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='SUFITaking') as SUFITaking,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='UPCChangeable') as UPCChangeable,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='COQCBChange') as COQCBChange,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='CAChangeable') as CAChangeable,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='WSIRIPTHNDEW') as WSIRIPTHNDEW,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='QDPEntrySetup') as QDPEntrySetup,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='UPDPEntrySetup') as UPDPEntrySetup,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='SUBBFS') as SUBBFS,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='SUBRModel') as SUBRModel,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='SUBRNMethod') as SUBRNMethod,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='DRTBIIRQty') as DRTBIIRQty,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='SUBRONPolicy') as SUBRONPolicy,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='ASUBAMC') as ASUBAMC,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='CASCRate') as CASCRate,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='SUBUECPolicy') as SUBUECPolicy,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='SUBCPMethod') as SUBCPMethod 
            ", company, Framework.SystemID + "0191213000126");

            String sql = String.Format(@" from [SYS_ControlParameters] A where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [PageNumber]='SYS20040' and [Name]='SUBMGT' ", company, Framework.SystemID + "0191213000126");

            DataTable dt = SQLHelper.ExecuteDataTable(select + sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return null;

            return ToHashtableList(dt)[0];
        }

        /// <summary>
        /// 產系統參數設定-生产系统公用参数
        /// SAM 2017年1月5日14:14:36
        /// </summary>
        /// <param name="company"></param>
        /// <returns></returns>
        public static Hashtable Sys20040PSCP(string company)
        {
            String select = String.Format(@"select Top 1 A.[value] as PSCP,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='ARAOCSetup') as ARAOCSetup,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='IMFGUOCSetup') as IMFGUOCSetup,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='CAMBEIAStruc') as CAMBEIAStruc,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='MPCMethod') as MPCMethod,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='DIPSetup') as DIPSetup
            ", company, Framework.SystemID + "0191213000127");

            String sql = String.Format(@" from [SYS_ControlParameters] A where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [PageNumber]='SYS20040' and [Name]='PSCP' ", company, Framework.SystemID + "0191213000127");

            DataTable dt = SQLHelper.ExecuteDataTable(select + sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return null;

            return ToHashtableList(dt)[0];
        }

        /// <summary>
        ///  成本系统参数设定-参数设定
        ///  SAM 2017年1月6日22:58:51
        /// </summary>
        /// <param name="company"></param>
        /// <returns></returns>
        public static Hashtable Cos17000SystemSetting(string company)
        {
            String select = String.Format(@"select Top 1 A.[value] as SystemSetting,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='AMFGFAMODMode') as AMFGFAMODMode,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='AACTLCBase') as AACTLCBase,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='ACTMFGEABase') as ACTMFGEABase,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='ACTOEABase') as ACTOEABase,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='PLMFGEMMethod') as PLMFGEMMethod,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='ExpDataSRC') as ExpDataSRC,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='LastTimeYear') as LastTimeYear,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='LastTimeMonth') as LastTimeMonth,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='IQATPBPIEPFNPURI') as IQATPBPIEPFNPURI,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='BOCC') as BOCC,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='ICWCPTCAPTBCTPS') as ICWCPTCAPTBCTPS,

            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='WIPOpenEntry') as WIPOpenEntry,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='MatWtCalcProcTime') as MatWtCalcProcTime,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='MatMthlyCloseProcTime') as MatMthlyCloseProcTime,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='FrozenProcTime') as FrozenProcTime,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='WkHrStatsTime') as WkHrStatsTime,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='ConsumeTimeCalc') as ConsumeTimeCalc,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='ALCR') as ALCR,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='ExpCostAlLoc') as ExpCostAlLoc,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='WtCalcTime') as WtCalcTime,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='MthlyCloseProcTime') as MthlyCloseProcTime
            ", company, Framework.SystemID + "0191213000149");

            String sql = String.Format(@" from [SYS_ControlParameters] A where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [PageNumber]='COS17000' and [Name]='SystemSetting' ", company, Framework.SystemID + "0191213000149");

            DataTable dt = SQLHelper.ExecuteDataTable(select + sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return null;

            return ToHashtableList(dt)[0];
        }

        /// <summary>
        /// 檢驗參數設定
        /// SAM 2017年1月9日23:27:10
        /// </summary>
        /// <param name="company"></param>
        /// <returns></returns>
        public static Hashtable Qcs10000InspPARSetup(string company)
        {
            String select = String.Format(@"select Top 1 A.[value] as InspPARSetup,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='NormalPlusOne') as NormalPlusOne,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='NormalPlusTwo') as NormalPlusTwo,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='NormalReductionOne') as NormalReductionOne,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='NormalReductionTwo') as NormalReductionTwo,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='LessQtyPlusOne') as LessQtyPlusOne,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='LessQtyPlusTwo') as LessQtyPlusTwo,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='LessQtyReductionOne') as LessQtyReductionOne,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='LessQtyReductionTwo') as LessQtyReductionTwo,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='MoreStrictOne') as MoreStrictOne,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='MoreStrictTwo') as MoreStrictTwo,


            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='QltyJudgeMethod') as QltyJudgeMethod,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='InspItemJudgeMethod') as InspItemJudgeMethod,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='APURAcpt') as APURAcpt,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='ASUBAcpt') as ASUBAcpt,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='AIPQC') as AIPQC,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='AMOStking') as AMOStking,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='ASalesShipProc') as ASalesShipProc,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='AIIFDTCPDDate') as AIIFDTCPDDate,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='IPURAcpt') as IPURAcpt,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='ISUBAcpt') as ISUBAcpt,

            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='IIPQC') as IIPQC,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='IMOStking') as IMOStking,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='ISalesShipProc') as ISalesShipProc,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='ATTDDate') as ATTDDate,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='ReplenishDeduct') as ReplenishDeduct,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='SPSC') as SPSC,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='BNIUnit') as BNIUnit,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='SETDate') as SETDate,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='PURRecAInsp') as PURRecAInsp,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='PURRecAInspNum') as PURRecAInspNum,

            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='SUBRecAInsp') as SUBRecAInsp,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='SUBRecAInspNum') as SUBRecAInspNum,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='StkingAInsp') as StkingAInsp,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='StkingAInspNum') as StkingAInspNum,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='ShipAInsp') as ShipAInsp,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='ShipAInspNum') as ShipAInspNum,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='PURROCGIQCO') as PURROCGIQCO,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='SUBROCGIQCO') as SUBROCGIQCO,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='PTOGIPQCO') as PTOGIPQCO,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='MOSEGFQCO') as MOSEGFQCO,

            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='SOGOQCT') as SOGOQCT,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='PURROACBI') as PURROACBI,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='SUBRTACBI') as SUBRTACBI,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='PTOACBI') as PTOACBI,
            (select [value] from [SYS_ControlParameters] where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [Key]='MOSEACBI') as MOSEACBI
            ", company, Framework.SystemID + "0191213000151");

            String sql = String.Format(@" from [SYS_ControlParameters] A where [OrganizationID]='{0}' and  [ParameterTypeID] = '{1}' and [PageNumber]='QCS10000' and [Name]='InspPARSetup' ", company, Framework.SystemID + "0191213000151");

            DataTable dt = SQLHelper.ExecuteDataTable(select + sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return null;

            return ToHashtableList(dt)[0];
        }
    }
}
