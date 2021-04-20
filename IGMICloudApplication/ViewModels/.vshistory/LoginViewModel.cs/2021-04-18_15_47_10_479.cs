using IGMICloudApplication.APIs;
using IGMICloudApplication.Commands;
using RestSharp;
using System;

namespace IGMICloudApplication.ViewModels
{
    public class LoginViewModel
    {
        public string userName { get; set; }
        public string password { get; set; }
        public DelegateCommand LoginCommand { get; private set; }
        private string loginEndpoint = "/authorize";
        public LoginViewModel()
        {
            if (string.IsNullOrEmpty(password))
            {
                password = "Password";
            }
            if (string.IsNullOrEmpty(username))
            {
                password = "Username";
            }
            LoginCommand = new DelegateCommand(() =>
            {
                userName = "niloy.bauri";
                password = "igmi@123";
                if (string.IsNullOrEmpty(userName) )
                {
                    Console.WriteLine("Username is empty");
                }else if (string.IsNullOrEmpty(password) || password == "Password")
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
                        string access_token = (string)((JsonObject)jObj["data"])[0];
                        string account_id = (string)((JsonObject)jObj["data"])[1];
                    }
                }
            });
        }
    }
}
