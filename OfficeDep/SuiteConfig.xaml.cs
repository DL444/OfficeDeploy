using System.Windows;
using System.Windows.Controls;

namespace OfficeDep
{
    /// <summary>
    /// Interaction logic for SuiteConfig.xaml
    /// </summary>
    public partial class SuiteConfig : Page
    {
        CheckBox[] checks = new CheckBox[10];
        public SuiteConfig()
        {
            InitializeComponent();
        }

        private void ConfigPage_Loaded(object sender, RoutedEventArgs e)
        {
            checks[0] = Check0;
            checks[1] = Check1;
            checks[2] = Check2;
            checks[3] = Check3;
            checks[4] = Check4;
            checks[5] = Check5;
            checks[6] = Check6;
            checks[7] = Check7;
            checks[8] = Check8;
            checks[9] = Check9;
            for (int i = 0; i < 10; i++)
            {
                checks[i].Checked += Check_Checked;
                checks[i].Unchecked += Check_UnChecked;
                if(Session.appsMap[i] == true)
                {
                    checks[i].IsChecked = true;
                }
            }
            if(Session.sku == Session.SuiteSKU.Home)
            {
                for(int i = 7; i < 10; i++)
                {
                    checks[i].Visibility = Visibility.Collapsed;
                }
            }
        }

        private void Check_Checked(object sender, RoutedEventArgs e)
        {
            for(int i = 0; i < 10; i++)
            {
                if(sender == checks[i])
                {
                    Session.appsMap[i] = true;
                }
            }
        }
        private void Check_UnChecked(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < 10; i++)
            {
                if (sender == checks[i])
                {
                    Session.appsMap[i] = false;
                }
            }
        }
    }
}
