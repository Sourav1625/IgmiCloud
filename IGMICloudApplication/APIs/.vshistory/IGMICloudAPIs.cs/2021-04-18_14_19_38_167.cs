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
        public IGMICloudAPIs()
        {
            var serviceBaseUri = $"{baseUrl}";
            _restClient = new RestClient(serviceBaseUri);
            _serializer = new JsonSerializer();

        }
        public bool DoLogin(string endpoint, string username, string password)
        {
            var request = new RestRequest($"{endpoint}");
            object dtObj = new
            {
                username = username,
                password = password
            };
            var bodyJson = _serializer.Serialize(dtObj);
            request.AddJsonBody(bodyJson);
            AddRequestBoilerplate(ref request);

            var response = _restClient.Post(request);
            if (!response.IsSuccessful)
            {
                NotifyRequestFailure(request, response);
                return false;
            }

            return true;
        }
        private static void AddRequestBoilerplate(ref RestRequest baseRequest)
        {
            baseRequest.AddHeader("accept", "application/json");
        }
        private void NotifyRequestFailure(RestRequest theRequest, IRestResponse theResponse)
        {
            string message = theResponse.Content != String.Empty ? theResponse.Content : "missing";
            Logger.Error("Failed with request to resource {resource}. Code: {code}. Full Response {message}.",
                theRequest.Resource, theResponse.StatusCode, message);
            Logger.Error("More details: Error Message: {error}, Error Exception {except}. Raw: {raw}",
                theResponse.ErrorMessage, theResponse.ErrorException, theResponse.RawBytes != null ? theResponse.RawBytes.ToString() : "");
        }
    }
}
