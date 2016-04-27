using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TryCatch.Interfaces;
using TryCatch.Models;

namespace TryCatch.Data
{
    public class XmlRepository : IXmlRepository
    {
        public List<Article> Articles
        {
            get
            {
                return XmlDataProvider.Instance.Get<List<Article>>("articles.xml");
            }
        }
    }
}
