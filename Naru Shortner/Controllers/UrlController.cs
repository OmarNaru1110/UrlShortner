using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Naru_Shortner.Context;
using Naru_Shortner.Helpers;
using Naru_Shortner.Migrations;
using Naru_Shortner.Models;

namespace Naru_Shortner.Controllers
{
    public class UrlController : Controller
    {
        public IActionResult Index()
        {
            return RedirectToAction(nameof(Create));
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(string newurl)
        {
            Uri uriResult;
            bool result = Uri.TryCreate(newurl, UriKind.Absolute, out uriResult)
                && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
            
            if (result)
            {
                var shortUrl = EnDecodeUrl.Encode(newurl);
            }
            else
            {
                ModelState.AddModelError("", "Invalid URL");
                return View(newurl);
            }

        }
    }
}
