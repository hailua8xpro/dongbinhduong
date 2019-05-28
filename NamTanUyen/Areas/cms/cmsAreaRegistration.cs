using System.Web.Mvc;

namespace NamTanUyen.Areas.cms
{
    public class cmsAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "cms";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "cms_default",
                "cms/{controller}/{action}/{id}",
                new { controller = "News", action = "Index", id = UrlParameter.Optional },
                 new[] { "NamTanUyen.Areas.cms.Controllers" }
            );
        }
    }
}