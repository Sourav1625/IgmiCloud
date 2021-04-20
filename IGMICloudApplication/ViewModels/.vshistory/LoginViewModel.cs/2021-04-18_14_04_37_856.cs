using IGMICloudApplication.Commands;

namespace IGMICloudApplication.ViewModels
{
    class LoginViewModel
    {
        public DelegateCommand LoginCommand { get; private set; }

        LoginCommand = new DelegateCommand((parameter) =>
        {
        });
    }
}
