using System;
using System.Collections.Generic;
using System.Text;

namespace tsdn.PIS.Entities
{
    /// <summary>
    /// 岗位备案信息
    /// </summary>
    public class PostRecordViewModel
    {
        /// <summary>
        /// 岗位信息
        /// </summary>
        public DutyPost DutyPost { get; set; }
        /// <summary>
        /// 岗位警力排班
        /// </summary>
        public List<PostPoliceOnduty> PostPoliceOnduty { get; set; }
        /// <summary>
        /// 岗位警力
        /// </summary>
        public List<PoliceMan> PoliceMan { get; set; }
    }    
}
