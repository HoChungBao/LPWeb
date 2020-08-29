using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace LienPhatERP.Helper
{
    public static class StringUtils
    {
        public static string CreateUrl(string name)
        {
            return CreateUrl(name, "-");
        }
        #region Urls / Webpages
        /// <summary>
        /// Downloads a web page and returns the HTML as a string
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static HttpWebResponse DownloadWebPage(string url)
        {
            var ub = new UriBuilder(url);
            var request = (HttpWebRequest)WebRequest.Create(ub.Uri);
            request.Proxy = null;
            return (HttpWebResponse)request.GetResponse();
        }

        /// <summary>
        /// Creates a URL freindly string, good for SEO
        /// </summary>
        /// <param name="strInput"></param>
        /// <param name="replaceWith"></param>
        /// <returns></returns>
        public static string CreateUrl(string strInput, string replaceWith)
        {
            // Doing this to stop the urls having amp from &amp;
            strInput = HttpUtility.HtmlDecode(strInput);
            // Doing this to stop the urls getting encoded
            var url = RemoveAccents(strInput);
            return StripNonAlphaNumeric(url, replaceWith).ToLower();
        }

        public static string RemoveAccents(string input)
        {
            // Replace accented characters for the closest ones:
            //var from = "ÂÃÄÀÁÅÇÈÉÊËÌÍÎÏÐÑÒÓÔÕÖØÙÚÛÜÝàáâãäåçèéêëìíîïðñòóôõöøùúûüýÿ".ToCharArray();
            //var to = "AAAAAACEEEEIIIIDNOOOOOOUUUUYaaaaaaceeeeiiiidnoooooouuuuyy".ToCharArray();
            //for (var i = 0; i < from.Length; i++)
            //{
            //    input = input.Replace(from[i], to[i]);
            //}

            //// Thorn http://en.wikipedia.org/wiki/%C3%9E
            //input = input.Replace("Þ", "TH");
            //input = input.Replace("þ", "th");

            //// Eszett http://en.wikipedia.org/wiki/%C3%9F
            //input = input.Replace("ß", "ss");

            //// AE http://en.wikipedia.org/wiki/%C3%86
            //input = input.Replace("Æ", "AE");
            //input = input.Replace("æ", "ae");

            //return input;


            var stFormD = input.Normalize(NormalizationForm.FormD);
            var sb = new StringBuilder();

            foreach (var t in stFormD)
            {
                var uc = CharUnicodeInfo.GetUnicodeCategory(t);
                if (uc != UnicodeCategory.NonSpacingMark)
                {
                    sb.Append(t);
                }
            }

            return (sb.ToString().Normalize(NormalizationForm.FormC));

        }
        /// <summary>
        /// Strips all non alpha/numeric charators from a string
        /// </summary>
        /// <param name="strInput"></param>
        /// <param name="replaceWith"></param>
        /// <returns></returns>
        public static string StripNonAlphaNumeric(string strInput, string replaceWith)
        {
            strInput = Regex.Replace(strInput, "[^\\w]", replaceWith);
            strInput = strInput.Replace(string.Concat(replaceWith, replaceWith, replaceWith), replaceWith)
                .Replace(string.Concat(replaceWith, replaceWith), replaceWith)
                .TrimStart(Convert.ToChar(replaceWith))
                .TrimEnd(Convert.ToChar(replaceWith));
            return strInput;
        }
        #endregion
        private static readonly Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        //HÀM TẠO DÃY RANDOM
        public static string GetRandomCode( string areacode)
        {
            var prefix = RandomString(4);
            var postfix = RandomString(4);
            var preArea = areacode.Replace(" ","");
            string today = DateTime.Now.ToString("yyMMdd");
            string creatDay = today;
            return $"MG_{preArea}_{creatDay}_{prefix}_{postfix}";
        }

        /// <summary>
        /// B - Eliminates extra whitespace.
        /// </summary>
       public static string ReplaceDataOutlet(string p)
        {
            StringBuilder b = new StringBuilder(p);
            b.Replace("  ", string.Empty);
            b.Replace(Environment.NewLine, string.Empty);
            b.Replace("NHA THUOC", string.Empty);
            b.Replace("NHÀ THUỐC", string.Empty);
            b.Replace("NT", string.Empty);
            b.Replace("QUẦY THUỐC", string.Empty);
            b.Replace("QT", string.Empty);
            b.Replace("Q.THUỐC", string.Empty);
            b.Replace("QUAY THUOC", string.Empty);
            b.Replace("HIEU THUOC", string.Empty);
          
            return b.ToString().Trim();
        }
        public static string RemoveVietNam(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return "";
            }
            string result = text;
            result = Regex.Replace(result, "à|á|ạ|ả|ã|â|ầ|ấ|ậ|ẩ|ẫ|ă|ằ|ắ|ặ|ẳ|ẵ|/g", "a");
            result = Regex.Replace(result, "è|é|ẹ|ẻ|ẽ|ê|ề|ế|ệ|ể|ễ|/g", "e");
            result = Regex.Replace(result, "ì|í|ị|ỉ|ĩ|/g", "i");
            result = Regex.Replace(result, "ò|ó|ọ|ỏ|õ|ô|ồ|ố|ộ|ổ|ỗ|ơ|ờ|ớ|ợ|ở|ỡ|/g", "o");
            result = Regex.Replace(result, "ù|ú|ụ|ủ|ũ|ư|ừ|ứ|ự|ử|ữ|/g", "u");
            result = Regex.Replace(result, "ỳ|ý|ỵ|ỷ|ỹ|/g", "y");
            result = Regex.Replace(result, "đ", "d");
            //result = Regex.Replace(result ,"!|@|%|^|*|(|)|+|=|<|>|?|,|.|:|;|'|\"|&|#|[|]|~|$|_|`|-|{|}", " ");
            //result = Regex.Replace(result ,"+", " ");
            result = Regex.Replace(result ,"À|Á|Ạ|Ả|Ã|Â|Ầ|Ấ|Ậ|Ẩ|Ẫ|Ă|Ằ|Ắ|Ặ|Ẳ|Ẵ|/g", "A");
            result = Regex.Replace(result ,"È|É|Ẹ|Ẻ|Ẽ|Ê|Ề|Ế|Ệ|Ể|Ễ/g", "E");
            result = Regex.Replace(result ,"Ì|Í|Ị|Ỉ|Ĩ/g", "I");
            result = Regex.Replace(result ,"Ò|Ó|Ọ|Ỏ|Õ|Ô|Ồ|Ố|Ộ|Ổ|Ỗ|Ơ|Ờ|Ớ|Ợ|Ở|Ỡ/g", "O");
            result = Regex.Replace(result ,"Ù|Ú|Ụ|Ủ|Ũ|Ư|Ừ|Ứ|Ự|Ử|Ữ/g", "U");
            result = Regex.Replace(result ,"Ỳ|Ý|Ỵ|Ỷ|Ỹ/g", "Y");
            result = Regex.Replace(result, "Đ/g", "D");
            return result;
        }
    }
}
