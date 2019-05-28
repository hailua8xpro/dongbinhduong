using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace NamTanUyen
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
             name: "Video",
             url: "video",
             defaults: new { controller = "Project", action = "Video", id = UrlParameter.Optional }
         );
            routes.MapRoute(
             name: "Image",
             url: "hinh-anh",
             defaults: new { controller = "Project", action = "Image", id = UrlParameter.Optional }
         );
            routes.MapRoute(
             name: "Search",
             url: "tim-kiem",
             defaults: new { controller = "Product", action = "Search", id = UrlParameter.Optional }
         );
            routes.MapRoute(
          name: "BuildPC",
          url: "build-pc",
          defaults: new { controller = "Product", action = "BuildPC", id = UrlParameter.Optional }
      );
            routes.MapRoute(
               name: "Ajax",
               url: "aj/{controller}/{action}/{id}",
               defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
               namespaces: new[] { "NamTanUyen.Controllers" }
           );
            routes.MapRoute(
                name: "WishList",
                url: "wishList",
                defaults: new { controller = "Account", action = "WishList", id = UrlParameter.Optional }
            );
            routes.MapRoute(
              name: "Contact",
              url: "lien-he",
              defaults: new { controller = "Common", action = "Contact", id = UrlParameter.Optional },
              namespaces: new[] { "NamTanUyen.Controllers" }
          );
            routes.MapRoute(
                name: "Account",
                url: "tai-khoan",
                defaults: new { controller = "Account", action = "MyAccount", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "ForgotPass",
                url: "quen-mat-khau",
                defaults: new { controller = "Account", action = "ForgotPass", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "ChangePass",
                url: "doi-mat-khau",
                defaults: new { controller = "Account", action = "ChangePassword", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Register",
                url: "dang-ky",
                defaults: new { controller = "Account", action = "Register", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Login",
                url: "dang-nhap",
                defaults: new { controller = "Account", action = "Login", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Cart",
                url: "gio-hang",
                defaults: new { controller = "Order", action = "Cart", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Order",
                url: "dat-hang",
                defaults: new { controller = "Order", action = "Index", id = UrlParameter.Optional },
               namespaces: new[] { "NamTanUyen.Controllers" }
            );
            routes.MapRoute(
               name: "StaticPage",
               url: "cong-quynh/{pageUrl}",
               defaults: new { controller = "StaticPage", action = "Index", id = UrlParameter.Optional },
               namespaces: new[] { "NamTanUyen.Controllers" }
           );
            routes.MapRoute(
               name: "NewsDetail",
               url: "tin-tuc/{cateUrl}/{newsUrl}-{newsId}",
               defaults: new { controller = "News", action = "Detail", id = UrlParameter.Optional },
               namespaces: new[] { "NamTanUyen.Controllers" }
           );
            routes.MapRoute(
               name: "NewsCategory",
               url: "tin-tuc/{cateUrl}",
               defaults: new { controller = "News", action = "Category", id = UrlParameter.Optional },
               namespaces: new[] { "NamTanUyen.Controllers" }
           );
            routes.MapRoute(
               name: "News",
               url: "tin-tuc",
               defaults: new { controller = "News", action = "Index", id = UrlParameter.Optional },
               namespaces: new[] { "NamTanUyen.Controllers" }
           );
            routes.MapRoute(
               name: "ProductDetail",
               url: "{objectUrl}/{cateUrl}/{groupUrl}/{productId}/{productUrl}",
               defaults: new { controller = "Product", action = "Detail", id = UrlParameter.Optional },
               namespaces: new[] { "NamTanUyen.Controllers" }
           );
            routes.MapRoute(
               name: "ProductDetail1",
               url: "{objectUrl}/{cateUrl}/{productId}/{productUrl}",
               defaults: new { controller = "Product", action = "Detail", id = UrlParameter.Optional },
               namespaces: new[] { "NamTanUyen.Controllers" }
           );
            routes.MapRoute(
               name: "ProductDetail2",
               url: "{objectUrl}/{productId}/{productUrl}",
               defaults: new { controller = "Product", action = "Detail", id = UrlParameter.Optional },
               namespaces: new[] { "NamTanUyen.Controllers" },
               constraints: new { productId = @"\d+" }
           );
            routes.MapRoute(
               name: "ProductGroup",
               url: "{objectUrl}/{cateUrl}/{groupUrl}",
               defaults: new { controller = "Product", action = "ProductGroup", id = UrlParameter.Optional },
               namespaces: new[] { "NamTanUyen.Controllers" }
           );
            routes.MapRoute(
               name: "ProductCategory",
               url: "{objectUrl}/{cateUrl}",
               defaults: new { controller = "Product", action = "ProductCategory", id = UrlParameter.Optional },
               namespaces: new[] { "NamTanUyen.Controllers" }
           );
            routes.MapRoute(
               name: "ProductObject",
               url: "{objectUrl}",
               defaults: new { controller = "Product", action = "ProductObject", id = UrlParameter.Optional },
               namespaces: new[] { "NamTanUyen.Controllers" }
           );


            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "NamTanUyen.Controllers" }
            );
        }
    }
}
