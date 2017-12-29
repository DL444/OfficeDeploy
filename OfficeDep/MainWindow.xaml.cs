using System;
using System.Collections.Generic;
using System.Windows;

namespace OfficeDep
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string[] pageName = new string[8]
        {
            "ProductSelect.xaml", "InstallPathway.xaml", "ProductArch.xaml", "UpdateChannel.xaml", "InstallSource.xaml", "SuiteConfig.xaml", "LangConfig.xaml", "ConfirmInstall.xaml"
        };

        bool[] customPages = new bool[8]
        {
            true, true, true, true, true, true, true, true
        };
        bool[] quickPages = new bool[8]
        {
            true, true, false, false, false, false, false, true
        };
        bool[] downloadPages = new bool[8]
        {
            true, true, true, false, false, false, true, true
        };

        bool[] pageList = null;

        public MainWindow()
        {
            InitializeComponent();
            ContentFrame.Navigate(new System.Uri("ProductSelect.xaml", UriKind.RelativeOrAbsolute));
            for(int i = 0; i < 10; i++)
            {
                Session.appsMap[i] = true;
            }
            Session.confWriter = GenerateConfig; 
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void NextBtn_Click(object sender, RoutedEventArgs e)
        {
            BackBtn.IsEnabled = true;
            if(Session.pathway == Session.Pathway.Quick)
            {
                pageList = quickPages;
            }
            else if(Session.pathway == Session.Pathway.Custom)
            {
                pageList = customPages;
            }
            else
            {
                pageList = downloadPages;
            }
            if (Session.page == 7)
            {
                RunInstall(GenerateConfig());
                return;
            }
            else
            {
                do
                {
                    Session.page++;
                }
                while (pageList[Session.page] == false);
            }
            if(Session.pathway == Session.Pathway.Custom && Session.page == 5 && Session.suiteEnabled == false)
            {
                Session.page++;
            }
            ContentFrame.Navigate(new System.Uri(pageName[Session.page], UriKind.RelativeOrAbsolute));
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            if (Session.pathway == Session.Pathway.Quick)
            {
                pageList = quickPages;
            }
            else if (Session.pathway == Session.Pathway.Custom)
            {
                pageList = customPages;
            }
            else
            {
                pageList = downloadPages;
            }
            if(Session.page > 0)
            {
                do
                {
                    Session.page--;
                }
                while (pageList[Session.page] == false);
            }
            if (Session.pathway == Session.Pathway.Custom && Session.page == 5 && Session.suiteEnabled == false)
            {
                Session.page--;
            }
            if (Session.page <= 0)
            {
                BackBtn.IsEnabled = false;
            }
            ContentFrame.Navigate(new System.Uri(pageName[Session.page], UriKind.RelativeOrAbsolute));
        }

        Operation GenerateConfig(string path)
        {
            List<string> langList = new List<string>();
            string[] langs = Session.lang.Split(',');
            foreach (string lang in langs)
            {
                langList.Add(lang.Trim());
            }
            Config config = new Config(Session.installSource, (Session.arch == Session.Architect.x86 ? Config.Architecture.x86 : Config.Architecture.x64), (Config.Channel)Session.updChannel);
            if(Session.suiteEnabled)
            {
                List<Config.Product.Application> appList = new List<Config.Product.Application>();
                for(int i = 0; i < 10; i++)
                {
                    if(Session.appsMap[i] == true)
                    {
                        appList.Add((Config.Product.Application)i);
                    }
                }
                if(Session.sku == Session.SuiteSKU.Home)
                {
                    config.AddProduct(new Config.Product(Config.Product.ProductType.Home, langList, appList));
                }
                else
                {
                    config.AddProduct(new Config.Product(Config.Product.ProductType.ProPlus, langList, appList));
                }
            }
            if(Session.visioEnabled)
            {
                config.AddProduct(new Config.Product(Config.Product.ProductType.Visio, langList, new List<Config.Product.Application>()));
            }
            if (Session.projectEnabled)
            {
                config.AddProduct(new Config.Product(Config.Product.ProductType.Project, langList, new List<Config.Product.Application>()));
            }

            System.IO.FileStream stream = new System.IO.FileStream(path, System.IO.FileMode.Create);
            ConfigWriter.WriteConfig(config, stream);
            stream.Close();
            if (Session.pathway == Session.Pathway.Download)
            {
                return new Operation(config, Operation.OperationType.Download);
            }
            else
            {
                return new Operation(config, Operation.OperationType.Deploy);
            }
        }
        Operation GenerateConfig()
        {
            string tempDir = System.IO.Path.GetTempPath();
            return GenerateConfig(tempDir + "config.xml");
        }

        void RunInstall(Operation operation)
        {
            string workingDir = System.IO.Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);
            string tempDir = System.IO.Path.GetTempPath();
            if (operation.OpType == Operation.OperationType.Deploy)
            {
                System.Diagnostics.Process.Start(workingDir + "\\tool\\OfficeDeploy.exe", $"/configure \"{tempDir}config.xml\"");
            }
            else
            {
                System.Diagnostics.Process.Start(workingDir + "\\tool\\OfficeDeploy.exe", $"/download \"{tempDir}config.xml\"");
            }
            Application.Current.Shutdown();
        }
    }

}
