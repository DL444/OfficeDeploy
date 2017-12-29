using System.Windows;
using System.Windows.Controls;

namespace OfficeDep
{
    /// <summary>
    /// Interaction logic for UpdateChannel.xaml
    /// </summary>
    public partial class UpdateChannel : Page
    {
        public UpdateChannel()
        {
            InitializeComponent();
        }

        private void MonthlyBtn_Checked(object sender, RoutedEventArgs e)
        {
            Session.updChannel = Session.UpdateCycle.Monthly;
        }

        private void BroadBtn_Checked(object sender, RoutedEventArgs e)
        {
            Session.updChannel = Session.UpdateCycle.Broad;
        }

        private void TargetedBtn_Checked(object sender, RoutedEventArgs e)
        {
            Session.updChannel = Session.UpdateCycle.Targeted;
        }

        private void UpdatePage_Loaded(object sender, RoutedEventArgs e)
        {
            if(Session.updChannel == Session.UpdateCycle.Monthly)
            {
                MonthlyBtn.IsChecked = true;
            }
            else if(Session.updChannel == Session.UpdateCycle.Broad)
            {
                BroadBtn.IsChecked = true;
            }
            else
            {
                TargetedBtn.IsChecked = true;
            }
        }
    }
}
