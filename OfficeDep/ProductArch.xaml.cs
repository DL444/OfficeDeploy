using System.Windows;
using System.Windows.Controls;

namespace OfficeDep
{
    /// <summary>
    /// Interaction logic for ProductArch.xaml
    /// </summary>
    public partial class ProductArch : Page
    {
        public ProductArch()
        {
            InitializeComponent();
            if(System.Environment.Is64BitOperatingSystem == false)
            {
                X64NotSupport.Visibility = Visibility.Visible;
                if (Session.pathway != Session.Pathway.Download)
                {
                    x86Btn.IsChecked = true;
                    x64Btn.IsEnabled = false;
                }
            }
        }

        private void x86Btn_Checked(object sender, RoutedEventArgs e)
        {
            Session.arch = Session.Architect.x86;
        }

        private void x64Btn_Checked(object sender, RoutedEventArgs e)
        {
            Session.arch = Session.Architect.x64;
        }

        private void ArchPage_Loaded(object sender, RoutedEventArgs e)
        {
            if(Session.arch == Session.Architect.x86)
            {
                x86Btn.IsChecked = true;
            }
            else
            {
                x64Btn.IsChecked = true;
            }
        }
    }
}
