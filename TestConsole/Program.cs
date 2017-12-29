using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OfficeDep;
using System.IO;

namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Config conf = new Config("", Config.Architecture.x64, Config.Channel.Monthly);
            Config.Product mainPd = new Config.Product(Config.Product.ProductType.Home);
            conf.AddProduct(mainPd);
            Config.Product visio = new Config.Product(Config.Product.ProductType.Visio, new List<string> { "en-us" }, new List<Config.Product.Application>());
            conf.AddProduct(visio);

            ConfigWriter.WriteConfig(conf, Console.OpenStandardOutput());
        }
    }
}
