using Logisitcs.BLL.Interfaces;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Logisitcs.BLL
{
   public class ExportDatabaseBll: IExportDatabaseBll
   {
      public async Task<MemoryStream> GetStreamLogisticsDb()
      {
         return await Task.Run(() =>
         {
            try
            {
               var databasePath = $"{Environment.CurrentDirectory}\\Fileserver\\logisticsDB.sqlite";

               MemoryStream ms = new MemoryStream();
               using (FileStream file = new FileStream(databasePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                  file.CopyTo(ms);

               return ms;
            }
            catch (Exception ex)
            {
               Console.WriteLine(ex);
            }
            
            return null;
         });
      }
   }
}
