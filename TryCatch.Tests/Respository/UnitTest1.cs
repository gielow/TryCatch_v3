using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TryCatch.Models;
using System.Collections.Generic;
using TryCatch.Data;

namespace TryCatch.Tests.Respository
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void GenerateArticles()
        {
            var articlesPath = @"E:\VSWorkspace\TryCatch_v3\TryCatch.Api\App_Data\articles.xml";

            var articles = new List<Article>();
            var random = new Random();

            for (int i = 1; i <= 50; i++)
            {
                var article = new Article();
                article.Id = i;
                article.Name = string.Format("Article {0}", i);
                article.Description = string.Format("Description of article {0}", i);
                article.Price = 10;

                var articleImages = new List<string>();
                articleImages.Add(string.Format("Content/Images/Articles/{0}.jpg", random.Next(1, 10)));
                articleImages.Add(string.Format("Content/Images/Articles/{0}.jpg", random.Next(1, 10)));
                article.Images = articleImages;

                articles.Add(article);
            }

            Serializer.SerializeObject<List<Article>>(articles, articlesPath);
            var fileArticles = Serializer.DeSerializeObject<List<Article>>(articlesPath);

            Assert.AreEqual(articles.Count, fileArticles.Count);
        }
    }
}
