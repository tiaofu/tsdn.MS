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
    /// MapsTo属性用来标记ViewModel类目标类型 eg Person PersonViewModel
    /// </summary>
    /// <example>
    ///     [MapsTo(typeof(PersonViewModel))]
    ///     public class Person
    /// </example>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class MapsToAttribute : AutoMapAttributeBase
    {
        public MemberList MemberList { get; set; } = MemberList.Source;

        public MapsToAttribute(params Type[] targetTypes)
            : base(targetTypes)
        {

        }

        public MapsToAttribute(MemberList memberList, params Type[] targetTypes)
            : this(targetTypes)
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
                configuration.CreateMap(type, targetType, MemberList);
                configuration.CreateMap(targetType, type, MemberList);
            }
        }
    }
}