using System.Windows;
using System.Windows.Controls;

namespace OfficeDep
{
    /// <summary>
    /// Interaction logic for ProductSelect.xaml
    /// </summary>
    public partial class ProductSelect : Page
    {
        public ProductSelect()
        {
            InitializeComponent();
        }

        private void SuiteCheck_Unchecked(object sender, RoutedEventArgs e)
        {
            Session.suiteEnabled = false;
            EditionHeader.Visibility = Visibility.Collapsed;
            SKUCombo.Visibility = Visibility.Collapsed;
            DesLabel.Visibility = Visibility.Collapsed;
        }

        private void SuiteCheck_Checked(object sender, RoutedEventArgs e)
        {
            Session.suiteEnabled = true;
            EditionHeader.Visibility = Visibility.Visible;
            SKUCombo.Visibility = Visibility.Visible;
            DesLabel.Visibility = Visibility.Visible;
        }

        private void SKUCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(SKUCombo.SelectedIndex == 0)
            {
                Session.sku = Session.SuiteSKU.Home;
                if (DesLabel != null)
                {
                    DesLabel.Content = Properties.Resources.SKUDescriptionHome;
                }
            }
            else
            {
                Session.sku = Session.SuiteSKU.ProPlus;
                DesLabel.Content = Properties.Resources.SKUDescriptionProPlus;
            }
        }

        private void VisioCheck_Checked(object sender, RoutedEventArgs e)
        {
            Session.visioEnabled = true;
        }

        private void VisioCheck_Unchecked(object sender, RoutedEventArgs e)
        {
            Session.visioEnabled = false;
        }

        private void ProjectCheck_Checked(object sender, RoutedEventArgs e)
        {
            Session.projectEnabled = true;
        }

        private void ProjectCheck_Unchecked(object sender, RoutedEventArgs e)
        {
            Session.projectEnabled = false;
        }

        private void ProductPage_Loaded(object sender, RoutedEventArgs e)
        {
            if(Session.suiteEnabled)
            {
                SuiteCheck.IsChecked = true;
            }
            if(Session.sku == Session.SuiteSKU.Home)
            {
                SKUCombo.SelectedIndex = 0;
            }
            else
            {
                SKUCombo.SelectedIndex = 1;
            }
            if(Session.visioEnabled)
            {
                VisioCheck.IsChecked = true;
            }
            if(Session.projectEnabled)
            {
                ProjectCheck.IsChecked = true;
            }
        }
    }
}
