using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace CaraLuggage
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            //USER SECTION:
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "Default",
                url: "",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "Home",
                url: "trang-chu",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "Shop",
                url: "danh-sach-san-pham",
                defaults: new { controller = "Home", action = "Shop", id = UrlParameter.Optional }
            );

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "Product Detail",
                url: "chi-tiet-san-pham-{id}",
                defaults: new { controller = "ProductDetail", action = "Index", id = UrlParameter.Optional }
            );

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "Contact",
                url: "lien-he",
                defaults: new { controller = "Home", action = "Contact", id = UrlParameter.Optional }
            );

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "About",
                url: "thong-tin-cua-hang",
                defaults: new { controller = "Home", action = "About", id = UrlParameter.Optional }
            );

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "Blog",
                url: "blog",
                defaults: new { controller = "Home", action = "Blog", id = UrlParameter.Optional }
            );

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "User Order",
                url: "don-hang",
                defaults: new { controller = "Home", action = "UserOrder", id = UrlParameter.Optional }
            );

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "User Order Detail",
                url: "chi-tiet-don-hang-{id}",
                defaults: new { controller = "Home", action = "UserOrderDetail", id = UrlParameter.Optional }
            );

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "User Profile",
                url: "thong-tin-khach-hang-{accountName}",
                defaults: new { controller = "Home", action = "UserProfile", accountName = UrlParameter.Optional }
            );

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "Cart",
                url: "gio-hang",
                defaults: new { controller = "Cart", action = "Index" }
            );

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "Login",
                url: "dang-nhap",
                defaults: new { controller = "LoginRegister", action = "LoginSection", id = UrlParameter.Optional }
            );

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "Register",
                url: "dang-ky",
                defaults: new { controller = "LoginRegister", action = "RegisterSection", id = UrlParameter.Optional }
            );

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "LogOut",
                url: "dang-xuat",
                defaults: new { controller = "LoginRegister", action = "LogOut", id = UrlParameter.Optional }
            );



            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "LoginFacebook",
                url: "dang-nhap-facebook",
                defaults: new { controller = "LoginRegister", action = "LoginFacebook", id = UrlParameter.Optional }
            );



            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "FacebookRedirect",
                url: "FacebookRedirect",
                defaults: new { controller = "LoginRegister", action = "FacebookRedirect", id = UrlParameter.Optional }
            );


            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "GoogleLoginCallBack",
                url: "dang-nhap-google",
                defaults: new { controller = "LoginRegister", action = "GoogleLoginCallBack", id = UrlParameter.Optional }
            );


            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "Add to Cart",
                url: "them-san-pham-{productId}",
                defaults: new { controller = "Cart", action = "AddToCart", productId = UrlParameter.Optional}
            );

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "Total Cart",
                url: "cap-nhat-gio-hang",
                defaults: new { controller = "Cart", action = "TotalCart", id = UrlParameter.Optional }
            );

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "PayForProductView",
                url: "xac-nhan-thanh-toan",
                defaults: new { controller = "Cart", action = "PayForProductView", id = UrlParameter.Optional }
            );

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "Order Success",
                url: "thanh-toan-thanh-cong",
                defaults: new { controller = "Cart", action = "OrderSuccess", id = UrlParameter.Optional }
            );

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "PayConfirm",
                url: "thanh-toan-thanh-cong",
                defaults: new { controller = "Cart", action = "PayConfirm", id = UrlParameter.Optional }
            );

            //ADMIN SECTION:
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "AdminHome",
                url: "trang-chu-admin",
                defaults: new { controller = "AdminDashboard", action = "Index", id = UrlParameter.Optional }
            );

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "Products",
                url: "admin-danh-sach-san-pham",
                defaults: new { controller = "Products", action = "Index", id = UrlParameter.Optional }
            );

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "Create Product",
                url: "tao-san-pham-moi",
                defaults: new { controller = "Products", action = "Create", id = UrlParameter.Optional }
            );

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "Edit Product",
                url: "sua-san-pham-{id}",
                defaults: new { controller = "Products", action = "Edit", id = UrlParameter.Optional }
            );

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "Delete Product",
                url: "xoa-san-pham-{id}",
                defaults: new { controller = "Products", action = "Delete", id = UrlParameter.Optional }
            );

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "Categories",
                url: "admin-danh-sach-loai-san-pham",
                defaults: new { controller = "Categories", action = "Index", id = UrlParameter.Optional }
            );

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "Create Categories",
                url: "tao-loai-san-pham-moi",
                defaults: new { controller = "Categories", action = "Create", id = UrlParameter.Optional }
            );

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "Edit Categories",
                url: "sua-loai-san-pham-{id}",
                defaults: new { controller = "Categories", action = "Edit", id = UrlParameter.Optional }
            );

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "Delete Categories",
                url: "xoa-loai-san-pham-{id}",
                defaults: new { controller = "Categories", action = "Delete", id = UrlParameter.Optional }
            );

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "Brands",
                url: "admin-danh-sach-thuong-hieu",
                defaults: new { controller = "Brands", action = "Index", id = UrlParameter.Optional }
            );

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "Create Brands",
                url: "tao-thuong-hieu-moi",
                defaults: new { controller = "Brands", action = "Create", id = UrlParameter.Optional }
            );

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "Edit Brands",
                url: "sua-thuong-hieu-{id}",
                defaults: new { controller = "Brands", action = "Edit", id = UrlParameter.Optional }
            );

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "Delete Brands",
                url: "xoa-thuong-hieu-{id}",
                defaults: new { controller = "Brands", action = "Delete", id = UrlParameter.Optional }
            );

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "Materials",
                url: "admin-danh-sach-chat-lieu",
                defaults: new { controller = "Materials", action = "Index", id = UrlParameter.Optional }
            );

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "Create Materials",
                url: "tao-chat-lieu-moi",
                defaults: new { controller = "Materials", action = "Create", id = UrlParameter.Optional }
            );

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "Edit Materials",
                url: "sua-chat-lieu-{id}",
                defaults: new { controller = "Materials", action = "Edit", id = UrlParameter.Optional }
            );

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "Delete Materials",
                url: "xoa-chat-lieu-{id}",
                defaults: new { controller = "Materials", action = "Delete", id = UrlParameter.Optional }
            );

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "Colors",
                url: "admin-danh-sach-mau-sac",
                defaults: new { controller = "Colors", action = "Index", id = UrlParameter.Optional }
            );

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "Create Colors",
                url: "tao-mau-sac-moi",
                defaults: new { controller = "Colors", action = "Create", id = UrlParameter.Optional }
            );

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "Edit Colors",
                url: "sua-mau-sac-{id}",
                defaults: new { controller = "Colors", action = "Edit", id = UrlParameter.Optional }
            );

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "Delete Colors",
                url: "xoa-mau-sac-{id}",
                defaults: new { controller = "Colors", action = "Delete", id = UrlParameter.Optional }
            );

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "Customers",
                url: "admin-danh-sach-khach-hang",
                defaults: new { controller = "Customers", action = "Index", id = UrlParameter.Optional }
            );

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "Create Customers",
                url: "tao-khach-hang-moi",
                defaults: new { controller = "Customers", action = "Create", id = UrlParameter.Optional }
            );

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "Edit Customers",
                url: "sua-khach-hang-{id}",
                defaults: new { controller = "Customers", action = "Edit", id = UrlParameter.Optional }
            );

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "Delete Customers",
                url: "xoa-khach-hang-{id}",
                defaults: new { controller = "Customers", action = "Delete", id = UrlParameter.Optional }
            );

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "Orders",
                url: "admin-danh-sach-don-hang",
                defaults: new { controller = "Orders", action = "Index", id = UrlParameter.Optional }
            );

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "Create Orders",
                url: "tao-don-hang-moi",
                defaults: new { controller = "Orders", action = "Create", id = UrlParameter.Optional }
            );

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "Edit Orders",
                url: "sua-don-hang-{id}",
                defaults: new { controller = "Orders", action = "Edit", id = UrlParameter.Optional }
            );

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "Delete Orders",
                url: "xoa-don-hang-{id}",
                defaults: new { controller = "Orders", action = "Delete", id = UrlParameter.Optional }
            );

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "Accounts",
                url: "admin-danh-sach-tai-khoan",
                defaults: new { controller = "Accounts", action = "Index", id = UrlParameter.Optional }
            );

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "Create Accounts",
                url: "tao-tai-khoan-moi",
                defaults: new { controller = "Accounts", action = "Create", id = UrlParameter.Optional }
            );

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "Edit Accounts",
                url: "sua-tai-khoan-{id}",
                defaults: new { controller = "Accounts", action = "Edit", id = UrlParameter.Optional }
            );

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "Delete Accounts",
                url: "xoa-tai-khoan-{id}",
                defaults: new { controller = "Accounts", action = "Delete", id = UrlParameter.Optional }
            );

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "Staffs",
                url: "admin-danh-sach-nhan-vien",
                defaults: new { controller = "Staffs", action = "Index", id = UrlParameter.Optional }
            );

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "Create Staffs",
                url: "tao-nhan-vien-moi",
                defaults: new { controller = "Staffs", action = "Create", id = UrlParameter.Optional }
            );

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "Edit Staffs",
                url: "sua-nhan-vien-{id}",
                defaults: new { controller = "Staffs", action = "Edit", id = UrlParameter.Optional }
            );

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "Delete Staffs",
                url: "xoa-nhan-vien-{id}",
                defaults: new { controller = "Staffs", action = "Delete", id = UrlParameter.Optional }
            );
        }
    }
}
