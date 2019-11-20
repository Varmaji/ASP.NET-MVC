using MVCApplication.Models;
using System.Web.Mvc;
using System.Web.Security;

namespace MVCApplication.Controllers
{
    public class AccountController : Controller
    {
        private string pwd;

        // GET: Account
        public ActionResult Login()
        {
            var model = new LoginViewModel();
            return View(model: model);
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            string User = "admin", pwd = "admin";
            if (model.Username == User && model.Password == pwd)
            {
                //??-NullCohelescing operator
                string returnUrl = Request.QueryString["ReturnUrl"] ?? "~/";//returning the url 
                FormsAuthentication.SetAuthCookie(model.Username, model.Rememberme);
                Session["user"] = model;
                return Redirect(Url.Action("Index", "Home"));
            }
            else
            {
                ModelState.AddModelError("", "Bad Username/password");
                return View(model);

            }
        }
    }
}