using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using Alipig.Framework.Entities;
using Alipig.Framework.Util;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate.Cfg;

namespace Alipig.Framework.NHHelper
{
    public class ConfigurationBuilder
    {
        static string str3 = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
        private string SERIALIZED_CFG_DIRECTORY = str3 + "config";
        private string SERIALIZED_CFG = str3 + "config\\" + "configuration.bin";
        //private string _ConnectionString = System.Configuration.ConfigurationManager.AppSettings["HNConnstring"].ToString();

        public Configuration Build()
        {
            Configuration cfg = LoadConfigurationFromFile();
            if (cfg == null)
            {
                HibernatingRhinos.Profiler.Appender.NHibernate.NHibernateProfiler.Initialize();
                cfg = Fluently.Configure()
                .Database(MySQLConfiguration.Standard
                            .ConnectionString(s => s.FromConnectionStringWithKey("HNConnstring")))
                    //.Username("DB_98B238_tests_admin")
                    //.Password("shenrx888")
                    //.Database("DB_98B238_tests")
                    //.TrustedConnection()))
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Entity>())
                .BuildConfiguration();
                SaveConfigurationToFile(cfg);
            }
            return cfg;
        }

        private Configuration LoadConfigurationFromFile()
        {
            DirectoryUtil.AssertDirExist(SERIALIZED_CFG_DIRECTORY);
            if (!IsConfigurationFileValid())
                return null;
            try
            {
                using (var file = File.Open(SERIALIZED_CFG, FileMode.Open))
                {
                    var bf = new BinaryFormatter();
                    return bf.Deserialize(file) as Configuration;
                }
            }
            catch (Exception)
            {
                // Something went wrong
                // Just build a new one
                return null;
            }
        }

        private bool IsConfigurationFileValid()
        {
            // If we don't have a cached config,
            // force a new one to be built
            if (!File.Exists(SERIALIZED_CFG))
                return false;
            var configInfo = new FileInfo(SERIALIZED_CFG);
            var asm = Assembly.GetExecutingAssembly();
            if (asm.Location == null)
                return false;
            // If the assembly is newer,
            // the serialized config is stale
            var asmInfo = new FileInfo(asm.Location);
            if (asmInfo.LastWriteTime > configInfo.LastWriteTime)
                return false;
            // If the app.config is newer,
            // the serialized config is stale
            var appDomain = AppDomain.CurrentDomain;
            var appConfigPath = appDomain.SetupInformation.
            ConfigurationFile;
            var appConfigInfo = new FileInfo(appConfigPath);
            if (appConfigInfo.LastWriteTime > configInfo.LastWriteTime)
                return false;
            // It's still fresh
            return true;
        }

        private void SaveConfigurationToFile(Configuration cfg)
        {
            using (var file = File.Open(SERIALIZED_CFG, FileMode.Create))
            {
                var bf = new BinaryFormatter();
                bf.Serialize(file, cfg);
            }
        }
    }
}
