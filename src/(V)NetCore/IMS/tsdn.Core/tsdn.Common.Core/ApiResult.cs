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
using System.ComponentModel;

namespace tsdn.Common
{
    /// <summary>
    /// 接口调用返回码
    /// </summary>
    public enum ResultCode
    {
        /// <summary>
        /// 成功
        /// </summary>
        [Description("成功")]
        Success = 0,

        /// <summary>
        /// 系统错误
        /// </summary>
        [Description("系统错误")]
        SystemError = 10001,

        /// <summary>
        /// 服务端资源不可用
        /// </summary>
        [Description("服务端资源不可用")]
        Server_Resources_Unavailable = 10002,

        /// <summary>
        /// 远程服务出错
        /// </summary>
        [Description("远程服务出错")]
        Remote_Service_Error = 10003,

        /// <summary>
        /// 参数错误，请参考API文档
        /// </summary>
        [Description("参数错误，请参考API文档")]
        Parameter_Error = 10008,

        /// <summary>
        /// 非法请求
        /// </summary>
        [Description("非法请求")]
        Bad_Request = 10012,

        /// <summary>
        /// 请求的HTTP METHOD不支持
        /// </summary>
        [Description("请求的HTTP METHOD不支持")]
        HttpMethd_Nonsupport = 10021,

        /// <summary>
        /// 该接口已经废弃
        /// </summary>
        [Description("该接口已经废弃")]
        Api_Abandoned = 10026
    }

    public static class ResultCodeExtend
    {
        /// <summary>
        /// 是否成功请求
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static bool IsSuccessCode(this ResultCode code)
        {
            return code == ResultCode.Success;
        }
    }

    /// <summary>
    /// 定义调用 WebApi 返回结果的格式
    /// </summary>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    public class ApiResult<T>
    {
        /// <summary>
        /// 错误码，不同的 api 接口自己定义，调用方需要根据具体接口的 来进行处理，后期会定义一系列标准错误码.
        /// 
        /// 通用错误码
        /// 错误码         错误信息
        /// 
        /// </summary>
        public ResultCode Code { get; set; }

        /// <summary>
        /// 接口返回码 说明
        /// </summary>
        public string CodeRemark
        {
            get
            {
                return EnumHelper.GetDescription(Code);
            }
        }

        /// <summary>
        /// 执行返回消息
        /// </summary>
        /// <remarks></remarks>
        public string Message { get; set; }

        /// <summary>
        /// 异常信息
        /// </summary>
        public Exception Exception { get; set; }

        /// <summary>
        /// 返回的主要内容.
        /// </summary>
        public T Result { get; set; }

        /// <summary>
        /// 数据总条数
        /// </summary>
        public int? TotalCount { get; set; }

        /// <summary>
        /// 数据总页数
        /// </summary>
        public int? TotalPage { get; set; }

        public ApiResult()
        {
            Result = default(T);
        }
    }
}
