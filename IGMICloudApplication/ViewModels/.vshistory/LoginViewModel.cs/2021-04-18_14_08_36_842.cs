using IGMICloudApplication.Commands;

namespace IGMICloudApplication.ViewModels
{
    class LoginViewModel
    {
        public string userName { get; private set; }
        public string password { get; private set; }
        public DelegateCommand LoginCommand { get; private set; }
        public LoginViewModel()
        {
            LoginCommand = new DelegateCommand((parameter) =>
            {
                if(string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
                {
                    Console.WriteLine("Lobby is successfully created.The lobby ID: " + callbackInfo.LobbyId);
                }
            });
        }
    }
}
