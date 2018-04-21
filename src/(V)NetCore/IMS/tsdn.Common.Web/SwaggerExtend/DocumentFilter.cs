/*********************************************************
 * CopyRight: tiaoshuidenong. 
 * Author: tiaoshuidenong
 * Address: wuhan
 * Create: 2018-04-10 17:44:16
 * Modify: 2018-04-10 17:44:16
 * Blog: http://www.cnblogs.com/tiaoshuidenong/
 * Description: 尚未编写描述
 *********************************************************/
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Linq;
using Swashbuckle.AspNetCore.Swagger;

namespace tsdn.Common.SwaggerExtend
{
    /// <summary>
    /// Document过滤
    /// </summary>
    public class DocumentFilter : IDocumentFilter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="swaggerDoc"></param>
        /// <param name="context"></param>
        public void Apply(SwaggerDocument swaggerDoc, DocumentFilterContext context)
        {
            var hiddenType = typeof(SwaggerHiddenAttribute);
            foreach (var apiDescription in context.ApiDescriptionsGroups.Items.SelectMany(e => e.Items))
            {
                if (apiDescription.ControllerAttributes().FirstOrDefault(e => e.GetType() == hiddenType) == null
             && apiDescription.ActionAttributes().FirstOrDefault(e => e.GetType() == hiddenType) == null)
                {
                    continue;
                }
                var key = "/" + apiDescription.RelativePath.TrimEnd('/');
                if (swaggerDoc.Paths.ContainsKey(key))
                    swaggerDoc.Paths.Remove(key);
            }
        }
    }
}
