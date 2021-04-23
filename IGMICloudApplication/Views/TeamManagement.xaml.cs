using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace IGMICloudApplication.Views
{
    /// <summary>
    /// Interaction logic for TeamManagement.xaml
    /// </summary>
    public partial class TeamManagement : Window
    {
        public TeamManagement()
        {
            InitializeComponent();
        }

        private void Invite_Guest_Click(object sender, RoutedEventArgs e)
        {
            InvitePerson invite = new InvitePerson("Guest");
            invite.ShowDialog();
        }
        private void Invite_Member_Click(object sender, RoutedEventArgs e)
        {
            InvitePerson invite = new InvitePerson("Member");
            invite.ShowDialog();
        }
    }
}
