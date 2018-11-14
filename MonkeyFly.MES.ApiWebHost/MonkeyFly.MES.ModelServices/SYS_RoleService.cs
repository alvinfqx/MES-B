using MonkeyFly.Core;
using MonkeyFly.MES.BasicService;
using MonkeyFly.MES.Models;
using MonkeyFly.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonkeyFly.MES.ModelServices
{
   public class SYS_RoleService:SuperModel<SYS_Roles>
    {
        /// <summary>
        /// 获取所有正常的角色数据
        /// SAM 2017年10月17日10:47:26
        /// </summary>
        /// <returns></returns>
        public static IList<SYS_Roles> GetList()
        {
            string sql = string.Format(@"select * from [SYS_Roles] where [SystemID] = '{0}'  and [Status]='{0}0201213000001' ", Framework.SystemID);

            DataTable dt = SQLHelper.ExecuteDataTable(sql, CommandType.Text);

            if (dt.Rows.Count == 0)
                return null;
            else
                return ToList(dt);
        }

        /// <summary>
        /// 拼接角色用户映射的SQL
        /// SAM 2017年10月17日10:55:22
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="MESUserID"></param>
        /// <param name="RoleID"></param>
        /// <returns></returns>
        public static String InsertSQL(string userId, string MESUserID,string RoleID)
        {
            try
            {
                string sql = string.Format(
                    @"insert [SYS_UserRoleMapping]([SystemID],[UserID],[RoleID],
                    [Modifier],[ModifiedTime],[Creator],[CreateTime]) values(
                    '{0}','{1}','{2}','{3}','{4}','{3}','{4}');",
                     Framework.SystemID, MESUserID, RoleID, userId, DateTime.Now);

                return sql;
            }
            catch (Exception ex)
            {
                DataLogerService.writeerrlog(ex);
                throw;
            }
        }
    }
}
