using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace LanguageFeatures.Models
{
    /// <summary>
    /// 비동기화 메서드 사용
    /// </summary>
    public class MyAsyncMethods
    {
        //public static Task<long?> GetPageLength() {

        //    HttpClient client = new HttpClient();

        //    var httpTask = client.GetAsync("http://appress.com");

        //    return httpTask.ContinueWith((Task<HttpResponseMessage> antecedent) => {
        //        return antecedent.Result.Content.Headers.ContentLength;
        //    }); //연속작업
        //}

        public async static Task<long?> GetPageLength()
        {

            HttpClient client = new HttpClient();

            var httpMessage = await client.GetAsync("http://appress.com"); //await 대기

            return httpMessage.Content.Headers.ContentLength;
            
        }
    }
}