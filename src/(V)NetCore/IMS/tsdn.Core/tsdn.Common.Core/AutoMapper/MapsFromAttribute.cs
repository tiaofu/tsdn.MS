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
using AutoMapper;
using tsdn.Collections.Extensions;

namespace tsdn.AutoMapper
{
    /// <summary>
    /// MapsFrom属性用来标记ViewModel类来源类型 eg Person PersonViewModel
    /// </summary>
    /// <example>
    ///     [MapsFrom(typeof(Person))]
    ///     public class PersonViewModel : Person
    /// </example>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class MapsFromAttribute : AutoMapAttributeBase
    {
        public MemberList MemberList { get; set; } = MemberList.Destination;

        /// <summary>
        /// 来源类型
        /// </summary>
        /// <param name="sourceTypes"></param>
        public MapsFromAttribute(params Type[] sourceTypes)
            : base(sourceTypes)
        {

        }

        public MapsFromAttribute(MemberList memberList, params Type[] sourceTypes)
            : this(sourceTypes)
        {
            MemberList = memberList;
        }

        public override void CreateMap(IMapperConfigurationExpression configuration, Type type)
        {
            if (TargetTypes.IsNullOrEmpty())
            {
                return;
            }

            foreach (var targetType in TargetTypes)
            {
                configuration.CreateMap(targetType, type, MemberList);
                configuration.CreateMap(type, targetType, MemberList);
            }
        }
    }
}