using Microsoft.EntityFrameworkCore;
using Naru_Shortner.Models;

namespace Naru_Shortner.BLL
{
    public interface IUrlBLL
    {
        public Url GetById(int id);
        public Url GetByUrl(string url);
        public Task<bool> Add(string url);
    }
}
