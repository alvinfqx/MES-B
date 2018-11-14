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
    public class SYS_UserOrganizationMappingService : SuperModel<SYS_UserOrganizationMapping>
    {

        /// <summary>
        /// 添加用户机构关系
        /// SAM 2017年7月4日11:24:46
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="MFCUserID"></param>
        /// <param name="OrganizationID"></param>
        /// <returns></returns>
        public static bool add(string userid, string MFCUserID,string OrganizationID)
        {
            try
            {
                string sql = string.Format(@"insert into [SYS_UserOrganizationMapping]([UserID],[OrganizationID],[SystemID],[Modifier], [ModifiedTime], [Creator], [CreateTime]) VALUES (N'" + MFCUserID + "','" + OrganizationID + "',{0})", UniversalService.getInsert(userid));

                return SQLHelper.ExecuteNonQuery(sql) > 0;
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                return false;
            }
        }

        /// <summary>
        /// 删除用户机构关系
        /// SAM 2017年7月4日11:25:38
        /// </summary>
        /// <param name="MESUserID">用户编号</param>
        /// <returns></returns>
        public static bool Delete(string MESUserID)
        {
            string sql = string.Format(@"delete from [SYS_UserOrganizationMapping] where [UserID] = '{0}' and [SystemID] = '{1}'", MESUserID, Framework.SystemID);

            return SQLHelper.ExecuteNonQuery(sql) > 0;
        }

        /// <summary>
        /// 获取登入者的RoleID
        /// Mouse 2017年9月5日09:51:23
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public static object GetRoles(string UserID)
        {
            string sql = string.Format(@"select RoleID from [SYS_UserRoleMapping] where [UserID]='{0}' and [SystemID]='{1}'", UserID, Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return null;
            else
                return ToHashtableList(dt);
        }

        /// <summary>
        /// 判断当前用户是否为指定角色
        /// Sam 2017年9月29日09:48:06
        /// </summary>
        /// <param name="MESUserID">用户流水号</param>
        /// <param name="RoleID">角色流水号</param>
        /// <returns></returns>
        public static bool CheckUserRole(string MESUserID,string RoleID)
        {
            string sql = string.Format(@"select * from [SYS_UserRoleMapping] where [UserID] = '{0}' and [SystemID] = '{1}' and [RoleID] = '{2}'", MESUserID, Framework.SystemID, RoleID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return false;
            else
                return true;
        }

    }
}
