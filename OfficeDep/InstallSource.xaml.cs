using System.Windows;
using System.Windows.Controls;

namespace OfficeDep
{
    /// <summary>
    /// Interaction logic for InstallSource.xaml
    /// </summary>
    public partial class InstallSource : Page
    {
        public InstallSource()
        {
            InitializeComponent();
        }

        private void BrowseBtn_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog pathPicker = new System.Windows.Forms.FolderBrowserDialog();
            System.Windows.Forms.DialogResult result = pathPicker.ShowDialog();
            if(result == System.Windows.Forms.DialogResult.OK)
            {
                PathBox.Text = pathPicker.SelectedPath;
            }
        }

        private void OfflineBtn_Checked(object sender, RoutedEventArgs e)
        {
            Session.installSource = PathBox.Text;
            BrowseBtn.Visibility = Visibility.Visible;
            PathBox.Visibility = Visibility.Visible;
        }

        private void PathBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(OfflineBtn.IsChecked == true)
            {
                Session.installSource = PathBox.Text;
            }
        }

        private void OnlineBtn_Checked(object sender, RoutedEventArgs e)
        {
            Session.installSource = "";
            BrowseBtn.Visibility = Visibility.Collapsed;
            PathBox.Visibility = Visibility.Collapsed;
        }

        private void SourcePage_Loaded(object sender, RoutedEventArgs e)
        {
            if(Session.installSource.Trim() == "")
            {
                OnlineBtn.IsChecked = true;
            }
            else
            {
                PathBox.Text = Session.installSource;
                OfflineBtn.IsChecked = true;
            }
        }
    }
}
