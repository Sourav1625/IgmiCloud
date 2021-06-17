using Newtonsoft.Json;
using RestSharp;
using System;
using System.IO;
using System.Net;

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
            var client = new RestClient("https://igmiweb.com/ucloud/api/v2/file/upload");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);            
            request.AddParameter("access_token", access_token);
            request.AddParameter("account_id", account_id);
            request.AddParameter("folder_id", folder_id);
            request.AddFile("upload_file", filePath);
            IRestResponse response = client.Execute(request);
            Console.WriteLine(response.Content);
            return response.Content;
        }

        private static void AddRequestBoilerplate(ref RestRequest baseRequest)
        {
            baseRequest.AddHeader("content-type", "multipart/form-data");
            baseRequest.AddHeader("accept", "application/json");
        }

        private void NotifyRequestFailure(RestRequest theRequest, IRestResponse theResponse)
        {
            string message = theResponse.Content != String.Empty ? theResponse.Content : "missing";
        }
    }
}
