using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TryCatch.Models;

namespace TryCatch.Controllers
{
    public class ArticleController : Controller
    {
        // GET: Article
        public async Task<ActionResult> Index(int? pageNumber)
        {
            var result = await GetArticles(pageNumber.HasValue ? pageNumber.Value : 1);
            return View(result);
        }

        public async Task<ActionResult> Page(int number)
        {
            var result = await GetArticles(number);
            return View(result);
        }
        
        [HttpGet]
        public async Task<JsonResult> PageJson(int number)
        {
            var result = await GetArticles(number);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        private async Task<IEnumerable<Article>> GetArticles(int pageNumber)
        {
            return await WebApiClient3.Instance.GetAsync<IEnumerable<Article>>(
                string.Format("api/Article/Page/{0}", pageNumber));
        }

        // GET: Article/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var article = await WebApiClient3.Instance.GetAsync<Article>(string.Format("api/Article/{0}", id));
            return View(article);
        }
    }
}
