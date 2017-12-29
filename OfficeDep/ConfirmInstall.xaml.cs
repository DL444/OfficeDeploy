using System.Windows.Controls;

namespace OfficeDep
{
    /// <summary>
    /// Interaction logic for ComfirmInstall.xaml
    /// </summary>
    public partial class ComfirmInstall : Page
    {
        public ComfirmInstall()
        {
            InitializeComponent();
        }

        private void ConfirmPage_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            if(Session.pathway == Session.Pathway.Download)
            {
                ConfirmText.Text = Properties.Resources.ReadyDescription_Layout;
            }
            else
            {
                ConfirmText.Text = Properties.Resources.ReadyDescription_Install;
            }
            if (Session.pathway != Session.Pathway.Quick)
            {
                ExportPath.Visibility = System.Windows.Visibility.Visible;
                ExportText.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                ExportPath.Visibility = System.Windows.Visibility.Collapsed;
                ExportText.Visibility = System.Windows.Visibility.Collapsed;
            }
        }

        private void ExportPath_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog saveDiag = new Microsoft.Win32.SaveFileDialog
            {
                DefaultExt = ".xml",
                Filter = "Configuration File|*.xml",
                FileName = "configuration.xml"
            };
            bool? saved = saveDiag.ShowDialog();
            string path = saveDiag.FileName;
            if(saved == true)
            {
                Session.confWriter(path);
            }
        }
    }
}
