using System.Web.Mvc;
using Base.CQRS.Commands;
using Security.Interfaces.Commands;
using Security.Interfaces.Queries;
using Web.Models.Membership;

namespace Web.Controllers
{
    public class MembershipController : Controller
    {
        public IGate Gate { get; set; }
        public ISecurityUserReader SecurityUserReader { get; set; }

        [Authorize]
        public ActionResult LogOff()
        {
            Gate.Dispatch(new LogOffUserCommand());
            return RedirectToAction("Index", "Home");
        }

        public ActionResult LogIn()
        {
            return View(new LogInModel { IsSignUp = false });
        }
        
        public ActionResult SignUp()
        {
            return View("LogIn", new LogInModel { IsSignUp = true });
        }
        
        [HttpPost]
        public ActionResult LogIn(LogInModel model)
        {
            if (string.IsNullOrEmpty(model.LoginPassword))
                ModelState.AddModelError("LoginPassword", "Please enter a password.");
            if (ModelState.IsValid) {
                var email = model.Email;
                var pass = model.LoginPassword;
                var rememberMe = model.RememberMe;
                
                var user = SecurityUserReader.CheckUserCredentials(new CheckUserCredentialsQuery { Email = email, Password = pass });
                if (user != null) {
                    Gate.Dispatch(new LogInUserCommand { Email = email, UserId = user.UserId, RememberMe = rememberMe, Roles = user.Roles });
                    return RedirectToAction("Index", "Home", null);
                } else {
                    ModelState.AddModelError("LoginPassword", "Invalid username or password.");
                }
            }
            return View(model);
        }
        
        [HttpPost]
        public ActionResult SignUp(LogInModel model)
        {
        	if (model.SignUpPassword == null)
        		ModelState.AddModelError("Password", "Please enter a password.");
        	else if (model.ConfirmPassword == null)
        		ModelState.AddModelError("ConfirmPassword", "Please confirm your password.");
        	else if (model.SignUpPassword != model.ConfirmPassword)
        		ModelState.AddModelError("ConfirmPassword", "You typed two different passwords. Please retype them.");
        	if (ModelState.IsValid) {
        		var email = model.Email;
        		var password = model.SignUpPassword;
        		
        		Gate.Dispatch(new SignUpUserCommand(email, password));
        		var user = SecurityUserReader.CheckUserCredentials(new CheckUserCredentialsQuery { Email = email, Password = password });
        		Gate.Dispatch(new LogInUserCommand { Email = email, UserId = user.UserId, RememberMe = model.RememberMe, Roles = user.Roles });
        		return RedirectToAction("Index", "Home", null);
        	}
        	return View("LogIn", model);
        }
    }
}
