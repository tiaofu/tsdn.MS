/*********************************************************
 * CopyRight: tiaoshuidenong. 
 * Author: tiaoshuidenong
 * Address: wuhan
 * Create: 2018-04-10 17:44:16
 * Modify: 2018-04-10 17:44:16
 * Blog: http://www.cnblogs.com/tiaoshuidenong/
 * Description: 尚未编写描述
 *********************************************************/
using tsdn.Service.Contract;
using System;
using System.Collections.Generic;
using System.Text;
using tsdn.Common;
using tsdn.Entity;
using tsdn.AutoMapper;
using tsdn.Common.Utility;
using tsdn.PIS.Entities;
using System.Linq;
using tsdn.Utility;

namespace tsdn.Service
{
    /// <summary>
    /// 特勤信息
    /// </summary>
    public class SpecialServiceService : ISpecialServiceService
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
                        var model = db.Set<SpecialScheme>().Find(p => p.SSID == Id);
                        model.IsDdel = true;
                        result.Result = db.Set<SpecialScheme>().Update(model);
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
        /// 查询接口
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public ApiResult<List<SpecialSchemeViewModel>> PostQuery(QueryCondition condition)
        {
            ApiResult<List<SpecialSchemeViewModel>> apiResult = new ApiResult<List<SpecialSchemeViewModel>>();
            apiResult.Result = new List<SpecialSchemeViewModel>();
            try
            {
                string querySql = XmlCommandManager.GetCommand("SpecialService:QueryList").CommandText;
                var result = Pagination.QueryBase<SpecialScheme>(querySql, condition);
                SpecialSchemeViewModel viewModel;
                foreach (var item in result.Data)
                {
                    viewModel = new SpecialSchemeViewModel();
                    viewModel.SpecialScheme = item;
                    apiResult.Result.Add(viewModel);
                }
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
        /// 单个查询
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ApiResult<SpecialSchemeViewModel> QueryById(string Id)
        {
            ApiResult<SpecialSchemeViewModel> result = new ApiResult<SpecialSchemeViewModel>();
            SpecialSchemeViewModel viewModel = new SpecialSchemeViewModel();
            try
            {
                using (CommonDbContext db = new CommonDbContext())
                {
                    if (!string.IsNullOrEmpty(Id))
                    {
                        viewModel.SpecialScheme = db.Set<SpecialScheme>().Find(p => p.SSID == Id);
                        viewModel.DutyLines = db.Set<DutyLine>().FindAll(p => p.SSID == Id).ToList();
                        viewModel.DutyUnits = db.Set<DutyUnit>().FindAll(p => p.SSID == Id).ToList();
                        var dutyUnits = db.Set<SchemeDutyUnit>().FindAll(p => p.SSID == Id).ToList();
                        List<SchemeDutyUnitPostViewModel> UnitPosts = new List<SchemeDutyUnitPostViewModel>();
                        foreach (var item in dutyUnits)
                        {
                            var viewUnitModel = item.MapTo<SchemeDutyUnitPostViewModel>();
                            viewUnitModel.Posts = db.Set<Post>().FindAll(p => p.SDID == item.SDID).ToList();
                            UnitPosts.Add(viewUnitModel);
                        }
                        viewModel.SchemeDutyUnits = UnitPosts;
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
        /// 保存接口
        /// </summary>
        /// <param name="SpecialScheme"></param>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        public ApiResult<bool> Save(SpecialSchemeViewModel SpecialScheme, UserInfo userInfo)
        {
            ApiResult<bool> result = new ApiResult<bool>();
            using (CommonDbContext db = new CommonDbContext())
            {
                using (var transaction = db.BeginTransaction())
                {
                    try
                    {
                        if (string.IsNullOrEmpty(SpecialScheme.SpecialScheme.SSID))
                        {
                            SpecialScheme.SpecialScheme.CreateTime = DateTime.Now;
                            SpecialScheme.SpecialScheme.Creater = userInfo.UserCode;
                            SpecialScheme.SpecialScheme.Updater = userInfo.UserCode;
                            SpecialScheme.SpecialScheme.UpdateTime = DateTime.Now;
                            db.Set<SpecialScheme>().Insert(SpecialScheme.SpecialScheme);
                        }
                        else
                        {
                            SpecialScheme.SpecialScheme.UpdateTime = DateTime.Now;
                            SpecialScheme.SpecialScheme.Updater = userInfo.UserCode;

                            var ids = db.Set<SchemeDutyUnit>().FindAll(p => p.SSID == SpecialScheme.SpecialScheme.SSID).Select(p => p.SDID).ToList();
                            db.Set<SpecialScheme>().Update(SpecialScheme.SpecialScheme);
                            db.Set<DutyLine>().Delete(p => p.SSID == SpecialScheme.SpecialScheme.SSID);
                            db.Set<DutyUnit>().Delete(p => p.SSID == SpecialScheme.SpecialScheme.SSID);
                            db.Set<SchemeDutyUnit>().Delete(p => p.SSID == SpecialScheme.SpecialScheme.SSID);
                            db.Set<Post>().Delete(p => ids.Contains(p.SDID));
                        }
                        foreach (var item in SpecialScheme.DutyLines)
                        {
                            if (string.IsNullOrEmpty(item.SDLID))
                                item.SDLID = GuidHelper.GetSeqGUID();
                        }
                        db.Set<DutyLine>().AddRange(SpecialScheme.DutyLines);
                        foreach (var item in SpecialScheme.DutyUnits)
                        {
                            if (string.IsNullOrEmpty(item.DUID))
                                item.DUID = GuidHelper.GetSeqGUID();
                        }
                        db.Set<DutyUnit>().AddRange(SpecialScheme.DutyUnits);
                        List<SchemeDutyUnit> DutyUnits = new List<SchemeDutyUnit>();
                        foreach (var item in SpecialScheme.SchemeDutyUnits)
                        {
                            if (string.IsNullOrEmpty(item.SDID))
                                item.SDID = GuidHelper.GetSeqGUID();
                            DutyUnits.Add(item.MapTo<SchemeDutyUnit>());
                            List<Post> unitPosts = new List<Post>();
                            foreach (var post in item.Posts)
                            {
                                post.SDID = item.SDID;
                                if (string.IsNullOrEmpty(post.SPID))
                                {
                                    post.SPID = GuidHelper.GetSeqGUID();
                                }                              
                            }
                            db.Set<Post>().AddRange(item.Posts);
                        }
                        db.Set<SchemeDutyUnit>().AddRange(DutyUnits);

                        transaction.Commit();
                        result.Result = db.SaveChanges() > 0;
                        result.Code = ResultCode.Success;
                    }
                    catch (Exception e)
                    {
                        transaction.Rollback();
                        result.Code = ResultCode.SystemError;
                        result.Message = e.ToString();
                    }
                }
            }
            return result;
        }
    }
}
