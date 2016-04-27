using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using TryCatch.Api.Models;
using TryCatch.Interfaces;
using TryCatch.Models;

namespace TryCatch.Api.Controllers
{
    public class ArticleController : ApiController
    {
        IArticleComponent _component;

        public ArticleController(IArticleComponent component)
        {
            _component = component;
        }

        // GET: api/Articles
        public IQueryable<Article> GetArticles()
        {
            return _component.GetMany().AsQueryable();
            //return db.Articles;
        }

        // GET: api/Article/Page/1
        [Route("api/Article/Page/{number:int?}")]
        [HttpGet]
        public IQueryable<Article> Page(int number = 1)
        {
            return _component.GetMany().Skip((number -1) * 10).Take(10).AsQueryable();
        }

        // GET: api/Articles/5
        [ResponseType(typeof(Article))]
        public IHttpActionResult GetArticle(int id)
        {
            var article = _component.Get(id);

            if (article == null)
            {
                return NotFound();
            }

            return Ok(article);
        }
        
    }
}