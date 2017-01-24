using System.IO;
using System.Net.Http;

namespace GetReposList.HttpWeb
{
    public class HttpManager : IHttpManager
    {
        private const string url = @"http://api.github.com/orgs/gopangea/repos";

        public string PullData()
        {
            //return GetTestString();
            var response = GetResponseString();
            var result = response.Result;
            return result;
        }

        private string GetTestString()
        {
            var stream = new FileStream(@"HttpWeb\repos.json", FileMode.Open, FileAccess.Read);
            var reader = new StreamReader(stream);
            return reader.ReadToEnd();
        }

        async System.Threading.Tasks.Task<string> GetResponseString()
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.TryAddWithoutValidation("Accept", "*/*");
            client.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:19.0) Gecko/20100101 Firefox/19.0");
            client.DefaultRequestHeaders.TryAddWithoutValidation("Accept-Charset", "ISO-8859-1");

            HttpResponseMessage response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();
            return result;
        }
    }
}
