using Naru_Shortner.BLL;
using Naru_Shortner.Models;
using System.Text;

namespace Naru_Shortner.Services.IServices
{
    public interface IUrlService
    {
        string Encode(int id);
        int Decode(string str);
        bool CheckUrl(string url);
        Url GetUrlById(int id);
        Url GetUrlByUrl(string url);
        Task<bool> AddUrl(string url);
    }
}
