using IGMICloudApplication.Commands;

namespace IGMICloudApplication.ViewModels
{
    public DelegateCommand LoginCommand { get; private set; }
    class LoginViewModel
    {
        LoginCommand = new DelegateCommand((parameter) =>
        {
        });
    }
}
