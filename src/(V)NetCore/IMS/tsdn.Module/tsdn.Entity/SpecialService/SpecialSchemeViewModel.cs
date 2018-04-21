using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tsdn.Entity
{
	///<summary>
	/// 特勤方案视图
	///</summary>	
	public class SpecialSchemeViewModel
    {
        /// <summary>
        /// 特勤方案基础信息
        /// </summary>
        public SpecialScheme SpecialScheme;

        /// <summary>
        /// 特勤线路
        /// </summary>
        public List<DutyLine> DutyLines;

        /// <summary>
        /// 特勤组信息
        /// </summary>
        public List<DutyUnit> DutyUnits;

        /// <summary>
        /// 特勤子组信息
        /// </summary>
        public List<SchemeDutyUnitPostViewModel> SchemeDutyUnits;
    }  
}

