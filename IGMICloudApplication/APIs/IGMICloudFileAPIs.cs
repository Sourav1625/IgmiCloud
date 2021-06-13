using Newtonsoft.Json;
using RestSharp;
using System;
using System.IO;

namespace IGMICloudApplication.APIs
{
    class IGMICloudFileAPIs
    {
        private readonly RestClient _restClient;
        private readonly JsonSerializer _serializer;
        private string baseUrl = "https://igmiweb.com/ucloud/api/v2/";
        public IGMICloudFileAPIs()
        {
            var serviceBaseUri = $"{baseUrl}";
            _restClient = new RestClient(serviceBaseUri);
            _serializer = new JsonSerializer();
        }

        public string UploadFile(string endpoint, string access_token, int account_id, int folder_id, string filePath)
        {
            FileStream upload_file = new FileStream(filePath, FileMode.Open);
            var request = new RestRequest($"{endpoint}");
            AddRequestBoilerplate(ref request);
            if (folder_id > 0)
            {
                request.AddParameter("application/form-data", $"access_token={access_token}&account_id={account_id}&folder_id={folder_id}&upload_file={upload_file}", ParameterType.RequestBody);
            }
            else
            {
                request.AddParameter("application/form-data", $"access_token={access_token}&account_id={account_id}&upload_file={upload_file}", ParameterType.RequestBody);
            }
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
            baseRequest.AddHeader("content-type", "application/form-data");
            baseRequest.AddHeader("accept", "application/json");
        }

        private void NotifyRequestFailure(RestRequest theRequest, IRestResponse theResponse)
        {
            string message = theResponse.Content != String.Empty ? theResponse.Content : "missing";
        }
    }
}
