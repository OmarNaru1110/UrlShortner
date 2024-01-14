using Microsoft.EntityFrameworkCore;
using Naru_Shortner.Models;

/*
    using repository pattern and dependency injection
    so I can add mock data to test with no bother of methods naming 
*/

namespace Naru_Shortner.Repository.IRepository
{
    public interface IUrlRepository
    {
        public Url GetById(int id);
        public Url GetByUrl(string url);
        public Task<bool> Add(string url);
    }
}
