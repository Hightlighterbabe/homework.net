using homework7._1.Models;
using Microsoft.AspNetCore.Mvc;

namespace homework7._1.Controllers
{
    [Route("UserProfile")]
    public class UserProfileController: Controller
    {
        [HttpGet]
        public IActionResult UserProfile() =>
            View();

        [HttpPost]
        public IActionResult UserProfile(UserProfile userProfile) =>
            View(userProfile);
    }
}