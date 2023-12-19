using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Naru_Shortner.BLL;
using Naru_Shortner.Context;
using Naru_Shortner.Helpers;
using Naru_Shortner.Migrations;
using Naru_Shortner.Models;

namespace Naru_Shortner.Controllers
{
    public class UrlController : Controller
    {
        private readonly IUrlBLL urlBll;
        public UrlController(IUrlBLL urlBll)
        {
            this.urlBll = urlBll;
        }
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
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(string newurl)
        {
            if (ShortUrlHelpers.CheckUrl(newurl))
            {
                // add to database
                if(await urlBll.Add(newurl))
                {
                    //impossible that stored urls is null cuz the check conditions 
                    //I made at ShortUrlHelpers and at urlBll.Add
                    var storedUrl = urlBll.GetByUrl(newurl);

                    var shortUrl = ShortUrlHelpers.Encode(storedUrl.Id);
                    string baseUrl = $"{this.Request.Scheme}://{this.Request.Host}/url/rdrto/";
                    baseUrl += shortUrl;

                    return RedirectToAction("Show",new { showUrl = baseUrl});
                }
                else
                {
                    ModelState.AddModelError("", "Something Went Wrong!\nPlease Try Again");
                    return View();
                }
            }
            else
            {
                ModelState.AddModelError("", "Invalid URL");
                return View();
            }

        }
        public IActionResult Show(string showUrl)
        {
            return View(model:showUrl);
        }
        public IActionResult RdrTo(string id)
        {
            int original = ShortUrlHelpers.Decode(id);
            var storedUrl = urlBll.GetById(original);
            if (storedUrl == null)
            {
                return NotFound(id);
            }
            return Redirect(storedUrl.LongUrl);
        }
    }
}
