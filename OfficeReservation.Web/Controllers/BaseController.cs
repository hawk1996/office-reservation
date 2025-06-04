using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace OfficeReservation.Web.Controllers
{
    [Authorize]
    public abstract class BaseController : Controller
    {
        protected int CurrentUserId
        {
            get
            {
                var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
                return userIdClaim != null ? int.Parse(userIdClaim) : 0;
            }
        }
    }
}
