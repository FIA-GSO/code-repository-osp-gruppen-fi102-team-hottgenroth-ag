using System.Threading.Tasks;

namespace Logisitcs.BLL.Interfaces
{
    public interface IPDFBLL
    {
        Task<byte[]> Create(object data);
    }
}