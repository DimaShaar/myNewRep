using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplicationMVC.ViewModel;

namespace WebApplicationMVC.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;    

        public AccountController(UserManager<IdentityUser> userManager,
               SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager;
            _userManager = userManager; 

        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [AcceptVerbs("Get", "Post")]
        public async Task<IActionResult> IsEmailInUse(string email)
        {
           var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return Json(true);
            }
            else
            {
                return Json($"Email {email} is already used");
            }          
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model) 
        {
            if(ModelState.IsValid)
            {
                var user = new IdentityUser { UserName = model.Email,Email = model.Email};
                var result = await _userManager.CreateAsync(user,model.Password); 
                if (result.Succeeded)
                {
                     await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("index", "home");
                }
                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

            }
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("index", "home");
        }
        [HttpGet]
        public IActionResult Login(string? returnUrl)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();  
        }
        [HttpPost]                                                    //Query String "Model Binding: returnUrl" 
        public  async Task<IActionResult> Login(LoginViewModel model,string returnUrl)
        {
            if (ModelState.IsValid) 
            { 
                var result =  await _signInManager.PasswordSignInAsync(model.Email, model.Password,model.RememberMe,false);
                if(result.Succeeded) 
                {                                          // XSS   
                    if(!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                    {
                        // http://localhost:2712/Account/Login?ReturnUrl=https://facebook.com the system redirect after login to facebook 
                        // return Redirect(returnUrl); // Open Redirect Attack 
                        return LocalRedirect(returnUrl); 
                    }
                    else
                    {
                        return RedirectToAction("index", "home");
                    }
                }
                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
            }
            return View(model);
        }  
       
    }
}
