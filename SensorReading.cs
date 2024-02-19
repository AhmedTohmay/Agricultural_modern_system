using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace Agricultural_modern_system
{
    class SensorReading
    {
        public class SensorsR
        {
            public int id { get; set; }
            public int sensor_id { get; set; }
            public string sensor_name { get; set; }
            public float value { get; set; }
            public string timestamp { get; set; }
        }

        private static readonly string baseURL = "http://localhost:3002/";

        public static async Task<SensorsR> GetSensor(string sensor_id)
        {
            var inputData = new Dictionary<string, string>
            {
                {"sensor_id",sensor_id}
            };

            var input = new FormUrlEncodedContent(inputData);
            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage res = await client.PostAsync(baseURL + "getonesensor",input))
                {
                    using (HttpContent content = res.Content)
                    {
                        string data = await content.ReadAsStringAsync();
                        if (data != null)
                        {
                            
                            return JsonConvert.DeserializeObject<List<SensorsR>>(data)[0];
                        }
                        else
                            return JsonConvert.DeserializeObject<List<SensorsR>>(null)[0];
                    }
                }
            }
            return null;
        }

        public static async Task Edit(int sensor_id, string sensor_name, float value, string timestamp)
        {
            var inputData = new Dictionary<string, string>
            {
                { "id", sensor_id.ToString() },
                { "sensor_id", sensor_id.ToString() },
                { "sensor_name", sensor_name },
                { "value", value.ToString() },
                { "timestamp", timestamp }
            };

            var input = new FormUrlEncodedContent(inputData);
            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage res = await client.PostAsync(baseURL + "editsensorreading", input)) ;
            }
        }
    }
}
