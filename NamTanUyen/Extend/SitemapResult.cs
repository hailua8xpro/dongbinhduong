using Library.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Xml;

namespace NamTanUyen.Extend
{
    public class SitemapResult : ActionResult
    {
        private readonly IEnumerable<SitemapItem> items;
        public SitemapResult(IEnumerable<SitemapItem> items)
        {
            this.items = items;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            var response = context.HttpContext.Response;

            response.ContentType = "text/xml";
            response.ContentEncoding = Encoding.UTF8;

            //using (var writer = new XmlTextWriter(response.Output))
            //{
            //    writer.Formatting = Formatting.Indented;
            //    var sitemap = SiteMapHelper.GenerateSiteMap(items);

            //    sitemap.WriteTo(writer);
            //}
        }
    }
}
