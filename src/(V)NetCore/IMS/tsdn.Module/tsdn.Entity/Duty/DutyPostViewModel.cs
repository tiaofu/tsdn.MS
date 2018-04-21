using tsdn.AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace tsdn.PIS.Entities
{
    ///<summary>
    ///岗位表
    ///</summary>
    [MapsTo(typeof(DutyPost)), MapsFrom(typeof(DutyPost))]
    public class DutyPostViewModel : DutyPost
    {
        /// <summary>
        /// 岗位的排班情况
        /// </summary>
        public List<PostPoliceOnduty> PostPoliceOnDutys { get; set; }
    }
}

