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
        public async Task<ActionResult> Index()
        {
            // Fixed without page because the articles are loaded by ajax directly in the api.
            var result = await WebApiClient3.Instance.GetAsync<IEnumerable<Article>>("api/Article/Page/");
            return View(result);
        }

        // GET: Article/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var result = await WebApiClient3.Instance.GetAsync<Article>(string.Format("api/Article/Page/{0}", id));
            return View(result);
        }
    }
}
