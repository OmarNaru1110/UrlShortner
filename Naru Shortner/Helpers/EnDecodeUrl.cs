using System.Text;

namespace Naru_Shortner.Helpers
{
    public static class EnDecodeUrl
    {

        private const string Alphabet = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-_";
        private static readonly int Base = Alphabet.Length;
        public static string Encode(int id)
        {
            List<int> digits = new List<int>();
            while (id > 0)
            {
                var remainder = id % Base;
                digits.Add(remainder);
                id = id / Base;
            }
            StringBuilder sb = new StringBuilder();
            for (int i = digits.Count - 1; i >= 0; i--)
            {
                sb.Append(Alphabet[digits[i]]);
            }
            return sb.ToString();
        }
        public static int Decode(string str)
        {
            var num = 0;
            for (var i = 0; i < str.Length; i++)
            {
                num = num * Base + Alphabet.IndexOf(str[i]);
            }
            return num;
        }
    }
}
