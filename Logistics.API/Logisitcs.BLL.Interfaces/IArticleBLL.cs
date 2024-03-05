using Logisitcs.BLL.Interfaces.ModelInterfaces;
using System.Collections.Generic;

namespace Logisitcs.BLL.Interfaces
{
    public interface IArticleBll
    {
        IEnumerable<IArticleData> GetAllArticlesByBoxId(string boxId);
    }
}