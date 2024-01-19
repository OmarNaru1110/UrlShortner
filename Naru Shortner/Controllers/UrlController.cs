using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Naru_Shortner.Helpers;
using Naru_Shortner.Migrations;
using Naru_Shortner.Models;
using Naru_Shortner.Repository.IRepository;
using Naru_Shortner.Services.IServices;

namespace Naru_Shortner.Controllers
{
    public class UrlController : Controller
    {
        private readonly IUrlService urlService;

        public UrlController(IUrlService urlService)
        {
            this.urlService = urlService;
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
            if (urlService.CheckUrl(newurl))
            {
                // add to database
                ////////////////////////////////////////
                if(await urlService.AddUrl(newurl))
                {
                    //impossible that stored urls is null cuz the check conditions 
                    //I made at UrlService
                    var storedUrl = urlService.GetUrlByUrl(newurl);

                    var shortUrl = urlService.Encode(storedUrl.Id);
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
            if(id == null)
            {
                return NotFound(id);
            }
            //what if they gave a string that it's character doesn't exist
            int original = urlService.Decode(id);
            var storedUrl = urlService.GetUrlById(original);
            if (storedUrl == null)
            {
                return NotFound(id);
            }
            return Redirect(storedUrl.LongUrl);
        }
    }
}
