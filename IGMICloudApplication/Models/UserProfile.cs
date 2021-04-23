namespace IGMICloudApplication.Models
{
    class UserProfile
    {
        public static string userName;

        public string UserName
        {
            get
            {
                return userName;
            }
            set
            {
                userName = value;                
            }
        }
    }
}
