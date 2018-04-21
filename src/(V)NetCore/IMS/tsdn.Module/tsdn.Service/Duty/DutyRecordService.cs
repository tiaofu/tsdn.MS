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
using tsdn.Common;
using tsdn.PIS.Entities;
using tsdn.Common.Utility;
using tsdn.Service.Contract;
namespace tsdn.Service
{
    /// <summary>
    /// 考核记录服务
    /// </summary>
    public class DutyRecordService : IDutyRecordService
    {
        /// <summary>
        /// 删除信息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ApiResult<bool> Delete(string Id, UserInfo userInfo)
        {
            ApiResult<bool> result = new ApiResult<bool>();
            try
            {
                using (CommonDbContext db = new CommonDbContext())
                {
                    if (!string.IsNullOrEmpty(Id))
                    {
                        //var model = db.Set<DutyPost>().Find(p => p.PostId == PostId);
                        //model.Updater = userInfo.UserCode;
                        //model.UpdateTime = DateTime.Now;
                        //db.Set<DutyPost>().Update(model);
                        result.Result = db.Set<DutyRecord>().Delete(p => p.DutyRecordId == Id);
                    }
                    result.Code = ResultCode.Success;
                }
            }
            catch (Exception e)
            {
                result.Code = ResultCode.SystemError;
                result.Message = e.ToString();
            }
            return result;
        }
        /// <summary>
        /// 单个信息查询
        /// </summary>
        /// <returns></returns>
        public ApiResult<DutyRecord> QueryById(string Id)
        {
            ApiResult<DutyRecord> result = new ApiResult<DutyRecord>();
            try
            {
                using (CommonDbContext db = new CommonDbContext())
                {
                    if (!string.IsNullOrEmpty(Id))
                    {
                        result.Result = db.Set<DutyRecord>().Find(p => p.DutyRecordId == Id);
                    }
                    result.Code = ResultCode.Success;
                }
            }
            catch (Exception e)
            {
                result.Code = ResultCode.SystemError;
                result.Message = e.ToString();
            }
            return result;
        }
        /// <summary>
        /// 查询列表信息
        /// </summary>
        /// <returns></returns>
        public ApiResult<List<DutyRecord>> PostQuery(QueryCondition condition)
        {
            ApiResult<List<DutyRecord>> apiResult = new ApiResult<List<DutyRecord>>();
            //{
            //    //var result = db.PageQuery<Entity.FrontendDevice>(condition);         
            //    //apiResult.Result = result.Data;
            //    //apiResult.TotalCount = result.Total;
            //    //apiResult.TotalPage = result.TotalPage;
            //}
            //var list = XmlCommand.From("AAA:CCC").ToList<Entity.FrontendDevice>();
            try
            {
                var result = Pagination.QueryBase<DutyRecord>("select * from DUTY_DUTYRECORD", condition);
                apiResult.Result = result.Data;
                apiResult.TotalCount = result.Total;
                apiResult.TotalPage = result.TotalPage;
                apiResult.Code = ResultCode.Success;
            }
            catch (Exception e)
            {
                apiResult.Code = ResultCode.SystemError;
                apiResult.Message = e.ToString();
            }
            return apiResult;
        }
        /// <summary>
        /// 保存信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ApiResult<bool> Save(DutyRecord model, UserInfo userInfo)
        {
            ApiResult<bool> result = new ApiResult<bool>();
            try
            {
                using (CommonDbContext db = new CommonDbContext())
                {
                    if (string.IsNullOrEmpty(model.DutyRecordId))
                    {                       
                        db.Set<DutyRecord>().Insert(model);
                    }
                    else
                    {
                        db.Set<DutyRecord>().Update(model);
                    }
                    result.Result = db.SaveChanges() > 0;
                    result.Code = ResultCode.Success;
                }
            }
            catch (Exception e)
            {
                result.Code = ResultCode.SystemError;
                result.Message = e.ToString();
            }
            return result;
        }
    }
}
