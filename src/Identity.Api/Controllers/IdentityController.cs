using System.Threading.Tasks;
using Identity.Domain.Models;
using Identity.Domain.Commands;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Api.Controllers
{
    public class IdentityController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public IdentityController(
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }
        
        [HttpPost("~/identity")]
        public async Task<IActionResult> Create(CreatePasswordIdentityCommand command)
        {
            // TODO: Store Command Source

            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = command.Email, Email = command.Email };
                var result = await _userManager.CreateAsync(user, command.Password);
                if (result.Succeeded)
                {
                    // TODO: Store Event Source IdentityCreatedEvent

                    // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=532713
                    // Send an email with this link
                    //var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    //var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Context.Request.Scheme);
                    //await _emailSender.SendEmailAsync(model.Email, "Confirm your account",
                    //    "Please confirm your account by clicking this link: <a href=\"" + callbackUrl + "\">link</a>");
                    await _signInManager.SignInAsync(user, isPersistent: false);

                    // TODO: Store Event Source UserSignedInEvent

                    return Json(result);
                }
                AddErrors(result);
            }

            return BadRequest(ModelState);

            // If we got this far, something failed, redisplay form
            //return View(model);
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }
    }
}