using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
namespace Agricultural_modern_system
{
    public static class AllOwners
    {
        public class OwnerData
        {
            public int id { get; set; }
            public string owner_name { get; set; }
            public string email { get; set; }
            public int passcode { get; set; }
        }

        private static readonly string baseURL = "http://localhost:3002/";

        public static async Task<string> GetAll()
        {
            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage res = await client.GetAsync(baseURL + "allowners"))
                {
                    using (HttpContent content = res.Content)
                    {
                        string data = await content.ReadAsStringAsync();
                        if (data != null)
                        {
                            return data;
                        }
                    }
                }
            }
            return string.Empty;
        }


        public static async Task<OwnerData> Getone(string id)
        {
            var inputData=new Dictionary<string,string> 
            {
                {"id",id}
            };

            var input = new FormUrlEncodedContent(inputData);
            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage res = await client.PostAsync(baseURL + "getowner",input))
                {
                    using (HttpContent content = res.Content)
                    {
                        string data = await content.ReadAsStringAsync();
                        if (data != null)
                        {
                            return JsonConvert.DeserializeObject<List<OwnerData>>(data)[0];
                        }
                        else
                            return JsonConvert.DeserializeObject<List<OwnerData>>(data)[0];

                    }
                }
            }
            return  JsonConvert.DeserializeObject<List<OwnerData>>(null)[0];
        }
                           
        public static async Task<string> Post(string username, string email, string passcode)
        {
            var inputData = new Dictionary<string, string>
            {
                {"owner_name", username},
                {"email", email},
                {"passcode", passcode}
            };

            var input = new FormUrlEncodedContent(inputData);
            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage res = await client.PostAsync(baseURL + "addnewowner", input))
                {
                    using (HttpContent content = res.Content)
                    {
                        string data = await content.ReadAsStringAsync();
                        if (data != null)
                        {
                            return data ;
                        }
                    }
                }
            }
            return string.Empty;
        }
    }
}
