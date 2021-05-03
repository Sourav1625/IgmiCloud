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
        public IGMICloudAPIs()
        {
            var serviceBaseUri = $"{baseUrl}";
            _restClient = new RestClient(serviceBaseUri);
            _serializer = new JsonSerializer();

        }
        public string DoLogin(string endpoint, string username, string password)
        {
            var request = new RestRequest($"{endpoint}");
            AddRequestBoilerplate(ref request);
            request.AddParameter("application/x-www-form-urlencoded", $"username={username}&password={password}", ParameterType.RequestBody);
            var response = _restClient.Post(request);
            if (!response.IsSuccessful)
            {
                NotifyRequestFailure(request, response);
                return null;
            }

            return response.Content;
        }
        public string FetchUserDetails(string endpoint, string access_token, int account_id)
        {
            var request = new RestRequest($"{endpoint}");
            AddRequestBoilerplate(ref request);
            request.AddParameter("application/x-www-form-urlencoded", $"access_token={access_token}&account_id={account_id}", ParameterType.RequestBody);
            var response = _restClient.Post(request);
            if (!response.IsSuccessful)
            {
                NotifyRequestFailure(request, response);
                return null;
            }

            return response.Content;
        }
        private static void AddRequestBoilerplate(ref RestRequest baseRequest)
        {
            baseRequest.AddHeader("content-type", "application/x-www-form-urlencoded");
            baseRequest.AddHeader("accept", "application/json");
        }
        private void NotifyRequestFailure(RestRequest theRequest, IRestResponse theResponse)
        {
            string message = theResponse.Content != String.Empty ? theResponse.Content : "missing";
            /*Logger.Error("Failed with request to resource {resource}. Code: {code}. Full Response {message}.",
                theRequest.Resource, theResponse.StatusCode, message);
            Logger.Error("More details: Error Message: {error}, Error Exception {except}. Raw: {raw}",
                theResponse.ErrorMessage, theResponse.ErrorException, theResponse.RawBytes != null ? theResponse.RawBytes.ToString() : "");*/
        }
    }
}
