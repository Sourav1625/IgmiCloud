using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using IGMICloudApplication.Commands;

namespace IGMICloudApplication.ViewModels
{
    public class MainViewModel
    {
        private DelegateCommand exitCommand;
        static MainViewModel s_Instance = null;       

        public static MainViewModel Instance
        {
            get
            {
                if (s_Instance == null)
                {
                    s_Instance = new MainViewModel();                    
                }
                return s_Instance;
            }
        }
        #region Constructor

        public LoginViewModel LoginViewModel { get; private set; }

        public FolderViewModel FolderViewModel { get; private set; }

        public MainViewModel()
        {
            LoginViewModel = new LoginViewModel();
            FolderViewModel = new FolderViewModel();
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
