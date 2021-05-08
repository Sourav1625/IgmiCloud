using IGMICloudApplication.Models;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;

namespace IGMICloudApplication.APIs
{
    class IGMICloudFolderAPIs
    {
        private readonly RestClient _restClient;
        private readonly JsonSerializer _serializer;
        private string baseUrl = "https://igmigroup.com/ucloud/ucloud/api/v2";
        public IGMICloudFolderAPIs()
        {
            var serviceBaseUri = $"{baseUrl}";
            _restClient = new RestClient(serviceBaseUri);
            _serializer = new JsonSerializer();
        }

        public Folder CreateFolder(string endpoint, string access_token, int account_id, string folder_name, int parent_id, int is_public, string access_password)
        {            
            var request = new RestRequest($"{endpoint}");
            AddRequestBoilerplate(ref request);
            request.AddParameter("application/x-www-form-urlencoded", $"access_token={access_token}&account_id={account_id}&folder_name={folder_name}&parent_id={parent_id}&is_public={is_public}&access_password={access_password}", ParameterType.RequestBody);
            var response = _restClient.Post(request);
            if (!response.IsSuccessful)
            {
                NotifyRequestFailure(request, response);
                return null;
            }

            var folder = JsonConvert.DeserializeObject<Folder>(response.Content);           
            return folder;
        }

        public Folder EditFolder(string endpoint, string access_token, int folder_id, int account_id, string folder_name, int parent_id, int is_public, string access_password, int parent_folder_id)
        {           
            var request = new RestRequest($"{endpoint}");
            AddRequestBoilerplate(ref request);
            request.AddParameter("application/x-www-form-urlencoded", $"access_token={access_token}&folder_id={folder_id}&account_id={account_id}&folder_name={folder_name}&parent_id={parent_id}&is_public={is_public}&access_password={access_password}&parent_folder_id={parent_folder_id}", ParameterType.RequestBody);
            var response = _restClient.Post(request);
            if (!response.IsSuccessful)
            {
                NotifyRequestFailure(request, response);
                return null;
            }

            var folder = JsonConvert.DeserializeObject<Folder>(response.Content);         

            return folder;
        }

        public string GetFolderList(string endpoint, string access_token, int parent_folder_id)
        {           
            var request = new RestRequest($"{endpoint}");
            AddRequestBoilerplate(ref request);
            if (parent_folder_id > 0)
            {
                request.AddParameter("application/x-www-form-urlencoded", $"access_token={access_token}&parent_folder_id={parent_folder_id}&account_id={LoggedinProfile.accountId}", ParameterType.RequestBody);
            }
            else
            {
                request.AddParameter("application/x-www-form-urlencoded", $"access_token={access_token}&account_id={LoggedinProfile.accountId}", ParameterType.RequestBody);
            }
            var response = _restClient.Post(request);
            if (!response.IsSuccessful)
            {
                NotifyRequestFailure(request, response);
                return null;
            }

                     
           return response.Content;
        }
        public string DeleteFolder(string endpoint, string access_token, int folder_id)
        {
            var folder = new Folder();
            var request = new RestRequest($"{endpoint}");
            AddRequestBoilerplate(ref request);
            request.AddParameter("application/x-www-form-urlencoded", $"access_token={access_token}&folder_id={folder_id}", ParameterType.RequestBody);
            var response = _restClient.Post(request);
            if (!response.IsSuccessful)
            {
                NotifyRequestFailure(request, response);
                return null;
            }
            return  response.Content; 
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
