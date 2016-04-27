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
            var result = await WebApiService.Instance.GetAsync<IEnumerable<Article>>("api/Article/Page/1");
            return View(result);
        }

        // GET: Article/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }
    }
}
