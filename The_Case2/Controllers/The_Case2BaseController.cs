using Microsoft.AspNetCore.Mvc;

namespace The_Case2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class The_Case2BaseController : Controller
    {
        /// <summary>
        /// JWT içindeki "UserId" claim'inden oturum açan kullanıcının ID'sini döner.
        /// İstemci başka bir kullanıcı adına işlem yapamaz; kimlik daima token'dan alınır.
        /// </summary>
        protected int CurrentUserId()
        {
            var claim = User?.FindFirst("UserId");
            return claim != null && int.TryParse(claim.Value, out var id) ? id : 0;
        }
    }
}
