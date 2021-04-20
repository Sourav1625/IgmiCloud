using RestSharp;
using RestSharp.Serialization.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGMICloudApplication.APIs
{
    class IGMICloudAPIs
    {
        private readonly RestClient _restClient;
        private readonly JsonSerializer _serializer;
        private string baseUrl = "https://igmigroup.com/ucloud/ucloud/api/v2";        
        private string userDetailsEndpoint = "/account/info";
        public IGMICloudAPIs(string endpoint)
        {
            var serviceBaseUri = $"{baseUrl}";
            _restClient = new RestClient(serviceBaseUri);
            _serializer = new JsonSerializer();

        }
        public bool Post()
        {
            var request = new RestRequest("/ping/");
            request.Timeout = 4000;
            AddRequestBoilerplate(ref request);
            var response = _restClient.Get(request);
            return response.IsSuccessful;
        }
        private static void AddRequestBoilerplate(ref RestRequest baseRequest)
        {
            baseRequest.AddHeader("accept", "application/json");
            baseRequest.AddHeader("User-Agent", "Apheleia/1.0");
        }
    }
}
