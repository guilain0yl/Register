using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Register.Controllers
{
    public class RegisterController : Controller
    {
        //
        // GET: /Resister/

        public ActionResult Register()
        {
            return View();
        }
        public JsonResult Validate(string UserID, string UserPassword)
        {
            if(BIZ.BizReg.Validate(UserID, UserPassword))
            {
                var re = new
                {
                    info = "已经成功注册",
                    status = "y"
                };
                return Json(re);
            }else
            {
                var re = new
                {
                    info = "用户名已存在",
                    status = "n"
                };
                return Json(re);
            }
        }
    }
}
