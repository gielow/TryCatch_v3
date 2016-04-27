using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TryCatch.Models;

namespace TryCatch.Interfaces
{
    public interface IArticleComponent
    {
        Article Get(int id);
        List<Article> GetMany();
    }
}
