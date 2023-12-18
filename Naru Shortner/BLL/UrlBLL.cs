using Microsoft.EntityFrameworkCore;
using Naru_Shortner.Context;
using Naru_Shortner.Models;

namespace Naru_Shortner.BLL
{
    public class UrlBLL : IUrlBLL
    {
        private readonly UrlContext context = new UrlContext();
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
