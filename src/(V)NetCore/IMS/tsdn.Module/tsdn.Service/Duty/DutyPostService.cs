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
using tsdn.Utility;
using tsdn.AutoMapper;
using System.Linq;

namespace tsdn.Service
{
    /// <summary>
    /// 岗位信息服务
    /// </summary>
    public class DutyPostService : IPostService
    {
        /// <summary>
        /// 删除岗位信息
        /// </summary>
        /// <param name="PostId"></param>
        /// <returns></returns>
        public ApiResult<bool> Delete(string PostId, UserInfo userInfo)
        {
            ApiResult<bool> result = new ApiResult<bool>();
            try
            {
                using (CommonDbContext db = new CommonDbContext())
                {
                    if (!string.IsNullOrEmpty(PostId))
                    {
                        //var model = db.Set<DutyPost>().Find(p => p.PostId == PostId);
                        //model.Updater = userInfo.UserCode;
                        //model.UpdateTime = DateTime.Now;
                        //db.Set<DutyPost>().Update(model);
                        result.Result = db.Set<DutyPost>().Delete(p => p.PostId == PostId);
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
        /// 单个岗位信息查询
        /// </summary>
        /// <returns></returns>
        public ApiResult<DutyPost> QueryById(string PostId)
        {
            ApiResult<DutyPost> result = new ApiResult<DutyPost>();
            try
            {
                using (CommonDbContext db = new CommonDbContext())
                {
                    if (!string.IsNullOrEmpty(PostId))
                    {
                        result.Result = db.Set<DutyPost>().Find(p => p.PostId == PostId);
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
        /// 查询岗位列表信息
        /// </summary>
        /// <returns></returns>
        public ApiResult<List<DutyPostViewModel>> PostQuery(QueryCondition condition)
        {
            ApiResult<List<DutyPostViewModel>> apiResult = new ApiResult<List<DutyPostViewModel>>();
            //using (CommonDbContext db = new CommonDbContext())
            //{
            //    //var result = db.PageQuery<Entity.FrontendDevice>(condition);         
            //    //apiResult.Result = result.Data;
            //    //apiResult.TotalCount = result.Total;
            //    //apiResult.TotalPage = result.TotalPage;
            //}
            //var list = XmlCommand.From("AAA:CCC").ToList<Entity.FrontendDevice>();            
            try
            {
                List<DutyPostViewModel> viewModels = new List<DutyPostViewModel>();
                var result = Pagination.QueryBase<DutyPost>("select * from DUTY_POST", condition);
                var dic = new Dictionary<string, string>();
                if (result.Data.Count == 0)
                    dic.Add("$PostIds", "''");
                else
                    dic.Add("$PostIds", string.Join(",", result.Data.Select(p => "'" + p.PostId + "'")));
                var onDutys = XmlCommand.From("PoliceRecord:GetPoliceOnDuty", null, dic).ToList<PostPoliceOnduty>();
                foreach (var item in result.Data)
                {
                    var viewModel = item.MapTo<DutyPostViewModel>();
                    viewModel.PostPoliceOnDutys = onDutys.Where(p => p.PostId == item.PostId).ToList();
                    viewModels.Add(viewModel);
                }
                apiResult.Result = viewModels;
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
        /// 保存岗位信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ApiResult<bool> Save(DutyPost model, UserInfo userInfo)
        {
            ApiResult<bool> result = new ApiResult<bool>();
            try
            {
                using (CommonDbContext db = new CommonDbContext())
                {
                    if (string.IsNullOrEmpty(model.PostId))
                    {
                        model.PostId = GuidHelper.GetSeqGUID();
                        model.CreatedTime = DateTime.Now;
                        model.Creater = userInfo.UserCode;
                        model.Updater = userInfo.UserCode;
                        model.UpdateTime = DateTime.Now;
                        db.Set<DutyPost>().Insert(model);
                    }
                    else
                    {
                        model.UpdateTime = DateTime.Now;
                        model.Updater = userInfo.UserCode;
                        db.Set<DutyPost>().Update(model);
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
