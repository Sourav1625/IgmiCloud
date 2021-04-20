using System.Windows;
using System.Windows.Input;

using IGMICloudApplication.Commands;

namespace IGMICloudApplication.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private DelegateCommand exitCommand;

        #region Constructor

        public LoginViewModel LoginViewModel { get; private set; }

        public MainViewModel()
        {
            LoginViewModel = new LoginViewModel();
        }

        #endregion

        public ICommand ExitCommand
        {
            get
            {
                if (exitCommand == null)
                {
                    exitCommand = new DelegateCommand(Exit);
                }
                return exitCommand;
            }
        }

        private void Exit()
        {
            Application.Current.Shutdown();
        }
    }
}
