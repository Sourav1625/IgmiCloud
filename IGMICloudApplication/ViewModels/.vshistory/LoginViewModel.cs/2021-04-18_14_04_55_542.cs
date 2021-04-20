using IGMICloudApplication.Commands;

namespace IGMICloudApplication.ViewModels
{
    class LoginViewModel
    {
        public DelegateCommand LoginCommand { get; private set; }
        public LoginViewModel()
        {
            LoginCommand = new DelegateCommand((parameter) =>
        {
        });
        }
    }
}
