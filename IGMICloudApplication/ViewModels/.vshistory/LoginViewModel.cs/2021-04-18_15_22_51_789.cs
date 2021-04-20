using IGMICloudApplication.APIs;
using IGMICloudApplication.Commands;
using RestSharp;
using System;

namespace IGMICloudApplication.ViewModels
{
    public class LoginViewModel
    {
        public string userName { get; private set; }
        public string password { get; private set; }
        public DelegateCommand LoginCommand { get; private set; }
        private string loginEndpoint = "/authorize";
        public LoginViewModel()
        {
            LoginCommand = new DelegateCommand(() =>
            {
                userName = "niloy.bauri";
                password = "igmi@123";
                if (string.IsNullOrEmpty(userName) )
                {
                    Console.WriteLine("Username is empty");
                }else if (string.IsNullOrEmpty(password))
                {
                    Console.WriteLine("Password is empty");
                }
                //Now Calling Login API
                var cloudAPIObj = new IGMICloudAPIs();
                string loginResponse = cloudAPIObj.DoLogin(loginEndpoint, userName, password);
                if (loginResponse!=null)
                {
                    var responseJson = SimpleJson.DeserializeObject(loginResponse);
                    if (responseJson is JsonObject jObj)
                    {
                        string access_token = jObj["data"][0];
                    }
                    //string access_token = loginResponse
                }
            });
        }
    }
}
