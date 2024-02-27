using Logisitcs.BLL.Interfaces;
using Logisitcs.DAL.Interfaces;
using System.Text.Json;
using System.Threading.Tasks;

namespace Logisitcs.BLL
{
    public class PDFBLL : IPDFBLL
    {
        private IPDFDAL _DAL;

        public PDFBLL(IPDFDAL dal)
        {
            _DAL = dal;
        }

        public async Task<byte[]> Create(object data)
        {
            // Serialisierung des Objekts in einen JSON-String
            var jsonData = JsonSerializer.Serialize(data);

            var result = await _DAL.Create(jsonData);

            return result;
        }
    }
}