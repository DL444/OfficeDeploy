using System.Windows;
using System.Windows.Controls;

namespace OfficeDep
{
    /// <summary>
    /// Interaction logic for InstallPathway.xaml
    /// </summary>
    public partial class InstallPathway : Page
    {
        public InstallPathway()
        {
            InitializeComponent();
        }

        private void QuickBtn_Checked(object sender, RoutedEventArgs e)
        {
            Session.pathway = Session.Pathway.Quick;
        }

        private void CustomBtn_Checked(object sender, RoutedEventArgs e)
        {
            Session.pathway = Session.Pathway.Custom;
        }

        private void DownloadBtn_Checked(object sender, RoutedEventArgs e)
        {
            Session.pathway = Session.Pathway.Download;
        }

        private void PathwayPage_Loaded(object sender, RoutedEventArgs e)
        {
            if(Session.pathway == Session.Pathway.Quick)
            {
                QuickBtn.IsChecked = true;
            }
            else if(Session.pathway == Session.Pathway.Custom)
            {
                CustomBtn.IsChecked = true;
            }
            else
            {
                DownloadBtn.IsChecked = true;
            }
        }
    }
}
