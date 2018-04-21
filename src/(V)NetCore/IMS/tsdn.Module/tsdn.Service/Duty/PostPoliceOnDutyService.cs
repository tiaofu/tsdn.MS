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

namespace tsdn.Service
{
    /// <summary>
    /// 排班信息服务
    /// </summary>
    public class PostPoliceOnDutyService : IPostPoliceOnDutyService
    {
        /// <summary>
        /// 删除排班
        /// </summary>
        /// <param name="PPNId"></param>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        public ApiResult<bool> Delete(string PPNId, UserInfo userInfo)
        {
            ApiResult<bool> result = new ApiResult<bool>();
            try
            {
                using (CommonDbContext db = new CommonDbContext())
                {
                    if (!string.IsNullOrEmpty(PPNId))
                    {
                        //var model = db.Set<DutyPost>().Find(p => p.PostId == PostId);
                        //model.Updater = userInfo.UserCode;
                        //model.UpdateTime = DateTime.Now;
                        //db.Set<DutyPost>().Update(model);
                        result.Result = db.Set<PostPoliceOnduty>().Delete(p => p.PPNId == PPNId);
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
        /// 查询单个排班
        /// </summary>
        /// <param name="PPNId"></param>
        /// <returns></returns>
        public ApiResult<PostPoliceOnduty> QueryById(string PPNId)
        {
            ApiResult<PostPoliceOnduty> result = new ApiResult<PostPoliceOnduty>();
            try
            {
                using (CommonDbContext db = new CommonDbContext())
                {
                    if (!string.IsNullOrEmpty(PPNId))
                    {
                        result.Result = db.Set<PostPoliceOnduty>().Find(p => p.PPNId == PPNId);
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
        /// 查询岗位排班信息
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public ApiResult<List<PostPoliceOnduty>> QueryByPost(QueryCondition condition)
        {
            ApiResult<List<PostPoliceOnduty>> apiResult = new ApiResult<List<PostPoliceOnduty>>();
            using (CommonDbContext db = new CommonDbContext())
                try
                {
                    var result = db.PageQuery<PostPoliceOnduty>(condition);
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
        /// 保存排班
        /// </summary>
        /// <param name="DutyPost"></param>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        public ApiResult<bool> Save(PostPoliceOnduty model, UserInfo userInfo)
        {
            ApiResult<bool> result = new ApiResult<bool>();
            try
            {
                using (CommonDbContext db = new CommonDbContext())
                {
                    if (string.IsNullOrEmpty(model.PPNId))
                    {
                        model.CreatedTime = DateTime.Now;
                        model.Creater = userInfo.UserCode;
                        model.Updater = userInfo.UserCode;
                        model.UpdateTime = DateTime.Now;
                        db.Set<PostPoliceOnduty>().Insert(model);
                    }
                    else
                    {
                        model.UpdateTime = DateTime.Now;
                        model.Updater = userInfo.UserCode;
                        db.Set<PostPoliceOnduty>().Update(model);
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
        /// 查询警员信息
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public ApiResult<List<PoliceMan>> QueryPoliceMan(QueryCondition condition)
        {
            ApiResult<List<PoliceMan>> result = new ApiResult<List<PoliceMan>>();
            //得到警员信息
            var policeman = XmlCommand.From("PoliceOnDuty:GetPoliceMan").ToList<PoliceMan>();
            //得到警员排班信息
            var fileter = condition.FilterList.Where(p => p.FieldName == "DutyDate").FirstOrDefault();
            if (fileter != null)
            {
                DateTime Time;
                condition.FilterList.Remove(fileter);
                if (DateTime.TryParse(fileter.FieldValue, out Time))
                {
                    string whereSql = $"where to_char(ONDUTYSTARTTIME,'yyyy-MM-dd')='{Time.ToString("yyyy-MM-dd")}' or (PPNREPEATTYPE!='0' and EFFECTSTARTTIME<=to_date('{Time.ToString("yyyy-MM-dd HH:mm:ss")}','yyyy-MM-dd HH24:mi:ss') and (EFFECTENDTIME=null or (EFFECTENDTIME!=null and EFFECTENDTIME>=to_date('{Time.ToString("yyyy-MM-dd HH:mm:ss")}','yyyy-MM-dd HH24:mi:ss'))))";
                    var dic = new Dictionary<string, string>();
                    dic.Add("$Where", whereSql);
                    var list = XmlCommand.From("PoliceOnDuty:GetPoliceOnDuty", null, dic).ToList<PostPoliceOnduty>();
                    foreach (var item in policeman)
                    {
                        var dutyInfo = list.Where(p => p.PoliceManid == item.PoliceManID && p.PPNRepeatType == "0").FirstOrDefault();
                        if (dutyInfo != null)
                        {
                            item.IsOnDuty = true;
                        }
                        var dutyinfoList = list.Where(p => p.PoliceManid == item.PoliceManID).ToList();
                        foreach (var duty in dutyinfoList)
                        {
                            //判断重复类型
                        }
                    }
                }
            }
            result.Result = policeman;
            return result;
        }
    }
}
