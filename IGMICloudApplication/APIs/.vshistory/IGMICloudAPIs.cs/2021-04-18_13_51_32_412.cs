using RestSharp;
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
        private string baseUrl = "https://igmigroup.com/ucloud/ucloud/api/v2";
        public IGMICloudAPIs()
        {
            var baseUrl = "https://" + serviceIp;
            var serviceBaseUri = $"{baseUrl}/";
            _restClient = new RestClient(serviceBaseUri);
            _serializer = new JsonSerializer();

        }
        public bool CheckConnection()
        {
            var request = new RestRequest("/ping/");
            request.Timeout = 4000;
            AddRequestBoilerplate(ref request);
            var response = _restClient.Get(request);
            return response.IsSuccessful;
        }
    }
}
