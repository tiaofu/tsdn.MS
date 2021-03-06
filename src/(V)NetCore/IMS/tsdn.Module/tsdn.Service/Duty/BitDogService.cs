﻿/*********************************************************
 * CopyRight: tiaoshuidenong. 
 * Author: tiaoshuidenong
 * Address: wuhan
 * Create: 2018-04-10 17:44:16
 * Modify: 2018-04-10 17:44:16
 * Blog: http://www.cnblogs.com/tiaoshuidenong/
 * Description: 尚未编写描述
 *********************************************************/
using Autofac.Extras.DynamicProxy;
using tsdn.Common.Aop;
using tsdn.Service.Contract;

namespace tsdn.Service
{
    [Intercept(typeof(AOPTest))]
    public class BitDogService : IDogService
    {
        /// <summary>
        /// 吃狗粮
        /// </summary>
        /// <param name="dogFood">重量</param>
        /// <returns></returns>
        public string Eat(double dogFood)
        {
            return $"我是{SayName()},今天心情{(dogFood > 1 ? "格外好":"有点小糟糕")}，我要吃{dogFood}kg狗粮";
        }

        public string SayName()
        {
            return "金毛";
        }
    }
}
