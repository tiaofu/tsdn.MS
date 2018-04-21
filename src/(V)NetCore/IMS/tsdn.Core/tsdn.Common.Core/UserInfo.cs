/*********************************************************
 * CopyRight: tiaoshuidenong. 
 * Author: tiaoshuidenong
 * Address: wuhan
 * Create: 2018-04-10 17:44:16
 * Modify: 2018-04-10 17:44:16
 * Blog: http://www.cnblogs.com/tiaoshuidenong/
 * Description: 尚未编写描述
 *********************************************************/
using System;
using System.Collections.Generic;

namespace tsdn.Common
{
    /// <summary>
    /// 用户信息
    /// </summary>
    [Serializable]
    public class UserInfo
    {
        /// <summary>
        /// 当前登录用户GUID
        /// </summary>
        public string UserGUID { get; set; }

        /// <summary>
        /// 当前登录用户Code
        /// </summary>
        public string UserCode { get; set; }

        /// <summary>
        /// 当前登录用户名称
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 是否超级管理员
        /// </summary>
        public bool IsAdmin { get; set; }

        /// <summary>
        /// 当前登录用户部门GUID
        /// </summary>
        public string DepartmentId { get; set; }

        /// <summary>
        /// 运维单位Id
        /// </summary>
        public string YwCompanyId { get; set; }

        /// <summary>
        /// 用户拥有的权限
        /// </summary>
        public List<Permission> PermissionList { get; set; }
    }

    /// <summary>
    /// 权限明细
    /// </summary>
    [Serializable]
    public class Permission
    {
        /// <summary>
        /// 动作点Code
        /// </summary>
        public string ActionCode
        {
            get;
            set;
        }

        /// <summary>
        /// 功能点Code
        /// </summary>
        public string FunctionCode
        {
            get;
            set;
        }
    }
}
