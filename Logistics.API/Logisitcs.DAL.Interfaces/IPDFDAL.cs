using System;
using System.Threading.Tasks;

namespace Logisitcs.DAL.Interfaces
{
    public interface IPDFDAL
    {
        Task<string> Create(object data);
        Task<byte[]> Create(string jsonData);
    }
}