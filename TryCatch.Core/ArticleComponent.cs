using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TryCatch.Interfaces;
using TryCatch.Models;

namespace TryCatch.Core
{
    public class ArticleComponent : IArticleComponent
    {
        private IXmlRepository _repository;

        public ArticleComponent(IXmlRepository repository)
        {
            _repository = repository;
        }

        public Article Get(int id)
        {
            return _repository.Articles.FirstOrDefault(a => a.Id == id);
        }

        public List<Article> GetMany()
        {
            return _repository.Articles;
        }
    }
}
