using Microsoft.EntityFrameworkCore.Query.Internal;
using Naru_Shortner.Models;
using Naru_Shortner.Repository.IRepository;
using Naru_Shortner.Services.IServices;
using System.Text;
/*
    - The whole idea of this app is based on Bijection or what we call "Bijective Function"
    if I have f(x) = y, then there must be g(y) = x
    that is bijection in brief which means if I have 2 differnt variables
    for example x,y
    then there is no way that f(x) == f(y) that's impossible in bijecton
    
    - the approach I took here to implement bijection is to encode url's id stored in 
    the database using 64 characters (a-z,A-Z,0-9,-_),
    then the decoding is done by passing the encoded url to Decode(str)
    which decodes the encoded url back to it's original id 
    and with that I can get the original url from the database using id
    
*/


namespace Naru_Shortner.Helpers
{
    public class UrlService : IUrlService
    {
        private const string Alphabet = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-_";
        private static readonly int Base = Alphabet.Length;
        
        private readonly IUrlRepository urlRepository;
        public UrlService(IUrlRepository urlRepository)
        {
            this.urlRepository = urlRepository;
        }

        //converting from base 10 to base 64
        public string Encode(int id)
        {
            //store the coefficient of base 64
            List<int> digits = new List<int>();
            while (id > 0)
            {
                var remainder = id % Base;
                digits.Add(remainder);
                id = id / Base;
            }
            
            if (digits.Count == 0)
                return "a";

            StringBuilder sb = new StringBuilder();
            for (int i = digits.Count - 1; i >= 0; i--)
            {
                sb.Append(Alphabet[digits[i]]);
            }
            return sb.ToString();
        }
        //converting from base 64 to base 10
        public int Decode(string str)
        {
            var id = 0;
            for (var i = 0; i < str.Length; i++)
            {
                id = id * Base + Alphabet.IndexOf(str[i]);
            }
            return id;
        }
        public bool CheckUrl(string url)
        {
            Uri uriResult;
            
            //checking if url scheme is correct or not
            bool result =
                Uri.TryCreate(url, UriKind.Absolute, out uriResult) &&
                (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
            return result;
        }
        public Url GetUrlById(int id)
        {
            return urlRepository.GetById(id);
        }
        public Url GetUrlByUrl(string url)
        {
            return urlRepository.GetByUrl(url);
        }
        public Task<bool> AddUrl(string url)
        {
            return urlRepository.Add(url);
        }
    }
}
