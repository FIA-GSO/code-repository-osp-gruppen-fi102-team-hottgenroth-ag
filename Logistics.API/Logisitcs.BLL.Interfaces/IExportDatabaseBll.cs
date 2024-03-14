using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logisitcs.BLL.Interfaces
{
   public interface IExportDatabaseBll
   {
      Task<MemoryStream> GetStreamLogisticsDb();
   }
}
