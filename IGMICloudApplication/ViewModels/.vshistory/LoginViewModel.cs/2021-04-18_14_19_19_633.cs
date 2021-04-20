using IGMICloudApplication.APIs;
using IGMICloudApplication.Commands;
using System;

namespace IGMICloudApplication.ViewModels
{
    class LoginViewModel
    {
        public string userName { get; private set; }
        public string password { get; private set; }
        public DelegateCommand LoginCommand { get; private set; }
        private string loginEndpoint = "/authorize";
        public LoginViewModel()
        {
            LoginCommand = new DelegateCommand(() =>
            {
                if(string.IsNullOrEmpty(userName) )
                {
                    Console.WriteLine("Username is empty");
                }else if (string.IsNullOrEmpty(password))
                {
                    Console.WriteLine("Password is empty");
                }
                //Now Calling Login API
                var cloudAPIObj = new IGMICloudAPIs();
                cloudAPIObj.DoLogin(loginEndpoint, userName, password);
            });
        }
    }
}
