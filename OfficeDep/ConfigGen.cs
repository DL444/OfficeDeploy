using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;


namespace OfficeDep
{
    // For more information regarding Office Click-to-Run Configuration File, 
    // Please refer to https://docs.microsoft.com/en-us/DeployOffice/overview-of-the-office-2016-deployment-tool
    // And https://docs.microsoft.com/en-us/DeployOffice/configuration-options-for-the-office-2016-deployment-tool

    public class Operation
    {
        // The Deployment Tool supports two operation mode, which will
        // respectively deploy Office on local device and create a offline layout. 
        public Config Config { get; private set; }
        public OperationType OpType { get; private set; }
        public Operation(Config config, OperationType opType)
        {
            Config = config;
            OpType = opType;
        }

        public enum OperationType
        {
            Download, Deploy
        }
    }
    public class Config
    {
        public string OfflineSource { get; private set; } = "";
        public Architecture Arch { get; private set; } = Architecture.x86;
        public List<Product> Products { get; private set; } = new List<Product>();
        public Channel UpdateChannel { get; private set; } = Channel.Monthly;
        public enum Architecture
        {
            x86 = 32, x64 = 64
            // Only one version of Office products can be installed at any given time. 
            // Therefore this setting does not apply at product level. 
        }
        public enum Channel
        {
            Monthly = 0, Broad = 1, Targeted = 2
            // Broad and Targeted channel both recieve feature updates every 6 months. 
            // However, Target channel recieves these updates 2 months later in March and September. 
        }
        public class Product
        {
            public List<string> Language { get; private set; } = new List<string>();
            public ProductType Type { get; private set; } = ProductType.Home;
            public List<Application> ExcludeApp { get; private set; } = new List<Application>();

            public enum ProductType
            {
                Home, ProPlus, Visio, Project
                // Home and ProPlus are two SKUs of Office Suite and are mutually exclusive. 
                // Visio and Project are two individual products. 
                // More types exist, but are not supported by this tool. 
            }
            public enum Application
            {
                Access = 0, Excel = 1, Groove = 7,
                Lync = 8, OneDrive = 9, OneNote = 2,
                Outlook = 3, PowerPoint = 4, Publisher = 5,
                Word = 6
                // Available apps in Home SKU: Access, Excel, OneNote, Outlook, PowerPoint, Publisher, Word. 
                // For individual products such as Visio and Project, this setting does not apply. 
            }

            public Product(ProductType type, List<string> lang, List<Application> apps)
            {
                Type = type;
                Language = lang;
                ExcludeApp = new List<Application>();
                if (type == ProductType.ProPlus)
                {
                    for (int i = 0; i < 10; i++)
                    {
                        ExcludeApp.Add((Application)i);
                    }
                }
                else if (type == ProductType.Home)
                {
                    for (int i = 0; i < 7; i++)
                    {
                        ExcludeApp.Add((Application)i);
                    }
                }
                else
                {
                    return;
                }
                foreach (Application app in apps)
                {
                    for (int i = 0; i < ExcludeApp.Count; i++)
                    {
                        if (ExcludeApp[i] == app) { ExcludeApp.RemoveAt(i); }
                    }
                }
            }
            public Product(ProductType type) : this(type, new List<string> { "MatchOS" }, new List<Application>())
            {
                ExcludeApp.Clear();
            }

            public static bool operator ==(Product lhs, Product rhs)
            {
                // This operator checks if two products are equivalent in type.
                // Thus, Home and ProPlus will be considered the same since they are 
                // different SKUs of the same product. 

                if (lhs.Type == rhs.Type)
                {
                    return true;
                }
                else
                {
                    if (lhs.Type == ProductType.Home && rhs.Type == ProductType.ProPlus)
                    {
                        return true;
                    }
                    else if (lhs.Type == ProductType.ProPlus && rhs.Type == ProductType.Home)
                    {
                        return true;
                    }
                }
                return false;
            }
            public static bool operator !=(Product lhs, Product rhs)
            {
                return !(lhs == rhs);
            }
        }

        public Config(string offlineScr, Architecture arch, Channel channel)
        {
            OfflineSource = offlineScr;
            Arch = arch;
            UpdateChannel = channel;
        }
        public Config() : this("", Architecture.x86, Channel.Monthly) { }

        public bool AddProduct(Product product)
        {
            // If a equivalent product was added before, the new one will be ignored. 
            foreach (Product prod in Products)
            {
                if (prod == product)
                {
                    return false;
                }
            }
            Products.Add(product);
            return true;
        }
        public bool RemoveProduct(Product.ProductType type)
        {
            for(int i = 0; i < Products.Count; i++)
            {
                if(Products[i].Type == type)
                {
                    Products.RemoveAt(i);
                    return true;
                }
            }
            return false;
        }
    }
    public static class ConfigWriter
    {
        public static void WriteConfig(Config config, Stream targetStr)
        {
            XmlDocument configDoc = new XmlDocument();

            XmlNode root = configDoc.CreateElement("Configuration");

            XmlNode generalNode = configDoc.CreateElement("Add");

            if (config.OfflineSource != "")
            {
                XmlAttribute sourceAttr = configDoc.CreateAttribute("SourcePath");
                sourceAttr.Value = config.OfflineSource;
                generalNode.Attributes.Append(sourceAttr);
            }

            XmlAttribute archAttr = configDoc.CreateAttribute("OfficeClientEdition");
            archAttr.Value = ((int)config.Arch).ToString();
            generalNode.Attributes.Append(archAttr);

            XmlAttribute channelAttr = configDoc.CreateAttribute("Channel");
            channelAttr.Value = config.UpdateChannel.ToString();
            generalNode.Attributes.Append(channelAttr);

            foreach (Config.Product product in config.Products)
            {
                XmlElement prodNode = configDoc.CreateElement("Product");
                string prodID = "";
                switch (product.Type)
                {
                    case Config.Product.ProductType.Home:
                        prodID = "O365HomePremRetail";
                        break;
                    case Config.Product.ProductType.ProPlus:
                        prodID = "O365ProPlusRetail";
                        break;
                    case Config.Product.ProductType.Visio:
                        prodID = "VisioProRetail";
                        break;
                    case Config.Product.ProductType.Project:
                        prodID = "ProjectProRetail";
                        break;
                    default:
                        throw new NotImplementedException("Corresponding writer not implemented.");
                }
                XmlAttribute prodIDAttr = configDoc.CreateAttribute("ID");
                prodIDAttr.Value = prodID;
                prodNode.Attributes.Append(prodIDAttr);
                foreach (string lang in product.Language)
                {
                    XmlElement langNode = configDoc.CreateElement("Language");
                    XmlAttribute idAttr = configDoc.CreateAttribute("ID");
                    idAttr.Value = (lang == "") ? "MatchOS" : lang;
                    langNode.Attributes.Append(idAttr);
                    prodNode.AppendChild(langNode);
                }
                if (product.Type == Config.Product.ProductType.Home || product.Type == Config.Product.ProductType.ProPlus)
                {
                    foreach (Config.Product.Application app in product.ExcludeApp)
                    {
                        XmlElement exAppNode = configDoc.CreateElement("ExcludeApp");
                        XmlAttribute idAttr = configDoc.CreateAttribute("ID");
                        idAttr.Value = app.ToString();
                        exAppNode.Attributes.Append(idAttr);
                        prodNode.AppendChild(exAppNode);
                    }
                }
                generalNode.AppendChild(prodNode);
            }
            root.AppendChild(generalNode);
            configDoc.AppendChild(root);

            configDoc.Save(targetStr);
        }
    }
}
