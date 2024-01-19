using Microsoft.EntityFrameworkCore;
using Naru_Shortner.Models;
using Naru_Shortner.Repository.Context;
using Naru_Shortner.Repository.IRepository;

namespace Naru_Shortner.BLL
{
    public class UrlRepository : IUrlRepository
    {
        private readonly UrlContext context;
        public UrlRepository(UrlContext context)
        {
            this.context = context;
        }
        public async Task<bool> Add(string str)
        {
            if(str == null)
            {
                return false;
            }
            var url = new Url { LongUrl = str };
            await context.URLs.AddAsync(url);
            context.SaveChanges();
            return true;
        }

        public Url GetById(int id)
        {
            var url = context.URLs.AsNoTracking().FirstOrDefault(x => x.Id == id);
            return url;
        }

        public Url GetByUrl(string url)
        {
            var storedUrl = context.URLs.AsNoTracking().FirstOrDefault(x => x.LongUrl == url);
            return storedUrl;
        }
    }
}
