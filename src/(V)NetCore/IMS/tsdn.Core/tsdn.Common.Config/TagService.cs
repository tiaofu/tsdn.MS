/*********************************************************
 * CopyRight: tiaoshuidenong. 
 * Author: tiaoshuidenong
 * Address: wuhan
 * Create: 2018-04-10 17:44:16
 * Modify: 2018-04-10 17:44:16
 * Blog: http://www.cnblogs.com/tiaoshuidenong/
 * Description: 尚未编写描述
 *********************************************************/
using tsdn.Common.Utility;
using tsdn.Dependency;
using tsdn.Utility;
using System;
using System.Collections.Generic;
using System.Linq;

namespace tsdn.Common.Config
{
    /// <summary>
    /// 自定义标签服务接口
    /// </summary>
    public interface ITagService : ITransientDependency
    {
        /// <summary>
        /// 通过来源类型获取自定义标签
        /// </summary>
        /// <param name="SourceType">来源类型 <seealso cref="TagsSourceType"/></param>
        /// <returns>自定义标签</returns>
        List<Tags> GetTags(string SourceType);

        /// <summary>
        /// 通过来源类型获取自定义标签
        /// </summary>
        /// <param name="SourceType">来源类型 <seealso cref="TagsSourceType"/></param>
        /// <param name="SourceId">来源Id</param>
        /// <returns>自定义标签</returns>
        List<Tags> GetTags(string SourceType, string SourceId);

        /// <summary>
        /// 保存同一类标签
        /// </summary>
        /// <param name="tagsList">标签列表</param>
        /// <param name="SourceType">来源类型 <seealso cref="TagsSourceType"/></param>
        /// <param name="operatorCode">操作人</param>
        /// <returns>影响条数</returns>
        int SaveTags(List<Tags> tagsList, string SourceType, string operatorCode);

        /// <summary>
        /// 保存同一类标签
        /// </summary>
        /// <param name="tagsList">标签列表</param>
        /// <param name="SourceType">来源类型 <seealso cref="TagsSourceType"/></param>
        /// <param name="SourceId">来源Id</param>
        /// <param name="operatorCode">操作人</param>
        /// <returns>影响条数</returns>
        int SaveTags(List<Tags> tagsList, string SourceType, string SourceId, string operatorCode);

        /// <summary>
        /// 删除某个类别所有标签
        /// </summary>
        /// <param name="SourceType">来源类型 <seealso cref="TagsSourceType"/></param>
        /// <returns>影响条数</returns>
        int DeleteTags(string SourceType);

        /// <summary>
        /// 删除某个类别所有标签
        /// </summary>
        /// <param name="SourceType">来源类型 <seealso cref="TagsSourceType"/></param>
        /// <param name="SourceId">来源Id</param>
        /// <returns>影响条数</returns>
        int DeleteTags(string SourceType, string SourceId);

        /// <summary>
        /// 更新标签热度
        /// </summary>
        /// <param name="TagGUID">Tag主键</param>
        /// <returns>更新结果</returns>
        int UpdateHeat(string TagGUID);
    }

    /// <summary>
    /// 自定义标签服务
    /// </summary>
    public class TagService : ITagService
    {
        /// <summary>
        /// 通过来源类型获取自定义标签
        /// </summary>
        /// <param name="SourceType">来源类型 <seealso cref="TagsSourceType"/></param>
        /// <returns>自定义标签</returns>
        public List<Tags> GetTags(string SourceType)
        {
            return GetTags(SourceType, null);
        }

        /// <summary>
        /// 通过来源类型获取自定义标签
        /// </summary>
        /// <param name="SourceType">来源类型 <seealso cref="TagsSourceType"/></param>
        /// <param name="SourceId">来源Id</param>
        /// <returns>自定义标签</returns>
        public List<Tags> GetTags(string SourceType, string SourceId)
        {
            if (string.IsNullOrEmpty(SourceType))
            {
                return new List<Tags>();
            }
            using (CommonDbContext cdb = new CommonDbContext())
            {
                var query = cdb.Set<Tags>().FindAll(e => e.TagType == SourceType);
                if (!string.IsNullOrEmpty(SourceId))
                {
                    query = query.Where(e => e.SourceId == SourceId);
                }
                return query.OrderByDescending(e => e.TagHeat).ToList();
            }
        }

        /// <summary>
        /// 保存同一类标签
        /// </summary>
        /// <param name="tagsList">标签列表</param>
        /// <param name="SourceType">来源类型 <seealso cref="TagsSourceType"/></param>
        /// <param name="operatorCode">操作人</param>
        public int SaveTags(List<Tags> tagsList, string SourceType, string operatorCode)
        {
            return SaveTags(tagsList, SourceType, null, operatorCode);
        }

        /// <summary>
        /// 保存同一类标签
        /// </summary>
        /// <param name="tagsList">标签列表</param>
        /// <param name="SourceType">来源类型 <seealso cref="TagsSourceType"/></param>
        /// <param name="SourceId">来源Id</param>
        /// <param name="operatorCode">操作人</param>
        public int SaveTags(List<Tags> tagsList, string SourceType, string SourceId, string operatorCode)
        {
            if (string.IsNullOrEmpty(SourceType) || tagsList == null || tagsList.Count == 0)
            {
                return -1;
            }
            using (CommonDbContext cdb = new CommonDbContext())
            {
                var dbSet = cdb.Set<Tags>();
                var query = dbSet.FindAll(e => e.TagType == SourceType);
                var allTagList = query.ToList();
                if (!string.IsNullOrEmpty(SourceId))
                {
                    query = query.Where(e => e.SourceId == SourceId);
                }
                var list = query.ToList();

                //删除的数据
                var queryRemove = (from p in list
                                   where !(from q in tagsList select q.TagGUID).Contains(p.TagGUID)
                                   select p);
                //新增的数据
                var queryAdd = tagsList.Where(e => string.IsNullOrEmpty(e.TagGUID)).ToList();
                var dict = new Dictionary<string, int>();
                foreach (var item in allTagList)
                {
                    dict[item.TagName] = item.TagHeat.Value;
                }
                int TagHeat = 0;
                foreach (var item in queryAdd)
                {
                    if (!dict.TryGetValue(item.TagName, out TagHeat))
                    {
                        TagHeat = 0;
                    }
                    item.TagGUID = GuidHelper.GetSeqGUID();
                    item.TagType = SourceType;
                    item.SourceId = SourceId;
                    item.TagHeat = TagHeat;
                    item.CreatedTime = DateTime.Now;
                    item.Creator = operatorCode;
                }
                dbSet.AddRange(queryAdd);
                dbSet.RemoveRange(queryRemove);
                return cdb.SaveChanges();
            }
        }

        /// <summary>
        /// 删除某个类别所有标签
        /// </summary>
        /// <param name="SourceType">来源类型 <seealso cref="TagsSourceType"/></param>
        /// <returns>影响条数</returns>
        public int DeleteTags(string SourceType)
        {
            return DeleteTags(SourceType, null);
        }

        /// <summary>
        /// 删除某个类别所有标签
        /// </summary>
        /// <param name="SourceType">来源类型 <seealso cref="TagsSourceType"/></param>
        /// <param name="SourceId">来源Id</param>
        /// <returns>影响条数</returns>
        public int DeleteTags(string SourceType, string SourceId)
        {
            if (string.IsNullOrEmpty(SourceType))
            {
                return -1;
            }
            using (CommonDbContext cdb = new CommonDbContext())
            {
                var dbSet = cdb.Set<Tags>();
                var query = dbSet.FindAll(e => e.TagType == SourceType);
                if (!string.IsNullOrEmpty(SourceId))
                {
                    query = query.Where(e => e.SourceId == SourceId);
                }
                dbSet.RemoveRange(query);
                return cdb.SaveChanges();
            }
        }

        /// <summary>
        /// 更新标签热度
        /// </summary>
        /// <param name="TagGUID">Tag主键</param>
        /// <returns>更新结果</returns>
        public int UpdateHeat(string TagGUID)
        {
            return 1;
        }
    }
}
