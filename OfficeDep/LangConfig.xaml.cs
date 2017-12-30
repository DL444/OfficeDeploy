﻿using System.Windows;
using System.Windows.Controls;
using System.Diagnostics;
using System.Windows.Navigation;

namespace OfficeDep
{
    /// <summary>
    /// Interaction logic for LangConfig.xaml
    /// </summary>
    public partial class LangConfig : Page
    {
        public LangConfig()
        {
            InitializeComponent();
        }

        private void MatchOSBtn_Checked(object sender, RoutedEventArgs e)
        {
            Session.lang = "";
            //LangBox.Visibility = Visibility.Collapsed;
            LangHint.Visibility = Visibility.Collapsed;
            //HelpLink.Visibility = Visibility.Collapsed;
            LanguageBox.Visibility = Visibility.Collapsed;
        }

        private void CustomLangBtn_Checked(object sender, RoutedEventArgs e)
        {
            for(int i = 0; i < Session.langSelection.Length; i++)
            {
                if(Session.langSelection[i] == true)
                {
                    (LanguageBox.Items.GetItemAt(i) as ListBoxItem).IsSelected = true;
                }
            }
            //Session.lang = LangBox.Text;
            //LangBox.Visibility = Visibility.Visible;
            LangHint.Visibility = Visibility.Visible;
            //HelpLink.Visibility = Visibility.Visible;
            LanguageBox.Visibility = Visibility.Visible;
        }

        private void LangBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            //if(CustomLangBtn.IsChecked == true)
            //{
            //    Session.lang = LangBox.Text;
            //}
        }

        private void LangPage_Loaded(object sender, RoutedEventArgs e)
        {
            if(Session.lang.Trim() == "")
            {
                MatchOSBtn.IsChecked = true;
            }
            else
            {
                //LangBox.Text = Session.lang;
                CustomLangBtn.IsChecked = true;
            }
        }

        private void HelpHLink_RequestNavigate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }

        private void LanguageBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            for(int i = 0; i < LanguageBox.Items.Count; i++)
            {
                if((LanguageBox.Items.GetItemAt(i) as ListBoxItem).IsSelected == true)
                {
                    Session.langSelection[i] = true;
                }
                else
                {
                    Session.langSelection[i] = false;
                }
            }
        }
    }
}
