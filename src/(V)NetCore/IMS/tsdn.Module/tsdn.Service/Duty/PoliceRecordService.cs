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
using System.Linq;
using tsdn.Utility;

namespace tsdn.Service
{
    /// <summary>
    /// 警力备案服务
    /// </summary>
    public class PoliceRecordService : IPoliceRecordService
    {
        /// <summary>
        /// 删除信息
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="userInfo"></param>
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
                        result.Result = db.Set<PoliceRecord>().Delete(p => p.PoliceRecordId == Id);
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
        public ApiResult<PoliceRecord> QueryById(string Id)
        {
            ApiResult<PoliceRecord> result = new ApiResult<PoliceRecord>();
            try
            {
                using (CommonDbContext db = new CommonDbContext())
                {
                    if (!string.IsNullOrEmpty(Id))
                    {
                        result.Result = db.Set<PoliceRecord>().Find(p => p.PoliceRecordId == Id);
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
        public ApiResult<List<PoliceRecord>> PostQuery(QueryCondition condition)
        {
            ApiResult<List<PoliceRecord>> apiResult = new ApiResult<List<PoliceRecord>>();
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
                var result = Pagination.QueryBase<PoliceRecord>("select * from DUTY_POLICERECORD", condition);
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
        /// <param name="model">备案记录</param>
        /// /// <param name="userInfo">用户信息</param>
        /// <returns></returns>
        public ApiResult<bool> Save(PoliceRecord model, UserInfo userInfo)
        {
            ApiResult<bool> result = new ApiResult<bool>();
            try
            {
                using (CommonDbContext db = new CommonDbContext())
                {     
                    var record = new List<PostRecordViewModel>();
                    //提取当前用户部分的岗位，生成部门的报备信息
                    List<DutyPost> posts = db.Set<DutyPost>().FindAll(p => p.PostLiabledep == userInfo.DepartmentId).ToList();
                    foreach (var item in posts)
                    {
                        var pm = new PostRecordViewModel
                        {
                            DutyPost = item,
                            PostPoliceOnduty = db.Set<PostPoliceOnduty>().FindAll(p => p.PostId == item.PostId).ToList()
                        };
                        var plist = pm.PostPoliceOnduty.Select(p => p.PoliceManid).ToList();
                        pm.PoliceMan = db.Set<PoliceMan>().FindAll(p => plist.Contains(p.PoliceManID)).ToList();
                        record.Add(pm);
                    }
                    model.Record = JsonHelper.ToJson(record);
                    model.DepartmentId = userInfo.DepartmentId;
                    if (string.IsNullOrEmpty(model.PoliceRecordId))
                    {
                        model.CreatedTime = DateTime.Now;
                        model.Creater = userInfo.UserCode;
                        model.Updater = userInfo.UserCode;
                        model.UpdateTime = DateTime.Now;
                        db.Set<PoliceRecord>().Insert(model);
                    }
                    else
                    {
                        model.UpdateTime = DateTime.Now;
                        model.Updater = userInfo.UserCode;
                        db.Set<PoliceRecord>().Update(model);
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
        /// <summary>
        /// 查询最新的备案信息
        /// </summary>
        /// <returns></returns>
        public ApiResult<List<PostRecordViewModel>> QueryLastTimeRecord()
        {
            ApiResult<List<PostRecordViewModel>> result = new ApiResult<List<PostRecordViewModel>>();
            result.Result = new List<PostRecordViewModel>();
            var list = XmlCommand.From("PoliceRecord:GetLastTimeRecord").ToList<PoliceRecord>();
            list.ForEach((e) =>
            {
                result.Result.Add(JsonHelper.ToJson<PostRecordViewModel>(e.Record));
            });
            return result;
        }
    }
}
