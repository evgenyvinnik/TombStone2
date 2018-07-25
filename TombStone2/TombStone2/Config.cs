using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Windows.Storage;
using Newtonsoft.Json;

namespace TombStone2
{
    public class Config
    {
        public class ConfigData
        {
            public string Password { get; set; }
        }

        private static Config _instance;
        public ConfigData Data;

        private Config()
        {
            var json = Task.Run( () => File.ReadAllText($"{Windows.ApplicationModel.Package.Current.InstalledLocation.Path}\\Assets\\config.json")).Result;
            Data = JsonConvert.DeserializeObject<ConfigData>(json);
        }

        public static Config Get => _instance ?? (_instance = new Config());        
    }
}