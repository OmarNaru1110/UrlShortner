using Naru_Shortner.Models;

namespace Naru_Shortner.Context
{
    public interface IUrlBLL
    {
        public Url GetById(int id);
        public Url GetByUrl(string url);
        
    }
}
