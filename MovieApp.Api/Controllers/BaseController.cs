using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MovieApp.Api.Controllers
{
    public class BaseController : ControllerBase
    {
        [NonAction]
        public string GetUserId()
        {
            var req = (User.Identity as ClaimsIdentity).Claims.FirstOrDefault(e => e.Type == ClaimTypes.NameIdentifier);

            // null check
            if (req == null)
            {
                throw new Exception("Name identifier not found in claims");
            }

            // üstte yapılan işleme "error handling" deniyor
            // eğer hata alınabilecek kod "handle" edilmediyse, "unhandled exception" deniyor

            return req.Value;
        }

    }
}
