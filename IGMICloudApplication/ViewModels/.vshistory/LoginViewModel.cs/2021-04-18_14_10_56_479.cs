using IGMICloudApplication.Commands;
using System;

namespace IGMICloudApplication.ViewModels
{
    class LoginViewModel
    {
        public string userName { get; private set; }
        public string password { get; private set; }
        public DelegateCommand LoginCommand { get; private set; }
        public LoginViewModel()
        {
            LoginCommand = new DelegateCommand(() =>
            {
                if(string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
                {
                    Console.WriteLine("User name is empty");
                }
            });
        }
    }
}
