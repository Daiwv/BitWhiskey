﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using System.Threading;
using System.IO;
using Newtonsoft.Json;


namespace BitWhiskey
{
    public class CustomDateTimeConverter : Newtonsoft.Json.Converters.IsoDateTimeConverter
    { public CustomDateTimeConverter() { base.DateTimeFormat = "yyyy-MM-ddTHH:mm:ss"; } }


    public class AppSettings<T> where T : new()
    {
        public void Save(string filePath)
        {
//            string file = Path.Combine(pricedir, ticker + ".txt");
            File.WriteAllText(filePath, Newtonsoft.Json.JsonConvert.SerializeObject(this, new CustomDateTimeConverter()));
//            File.WriteAllText(fileName, (new JavaScriptSerializer()).Serialize(this));
        }
        public static T Load(string filePath)
        {
            //            T t = new T();
            //            if (File.Exists(fileName))
            //            t = (new JavaScriptSerializer()).Deserialize<T>(File.ReadAllText(fileName));
            //            return t;
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(File.ReadAllText(filePath), new CustomDateTimeConverter());
       }
    }
    public class AppSettingsManager 
    {
        public string pathProfiles;
        public string pathStandart ;
        public AppSettingsManager()
        {
            pathProfiles = Path.Combine(ApplicationPath.directory, @"AppBin\Profiles");
        }
        public string  GetProfileDir(string profileName)
        {
            return Path.Combine(pathProfiles, profileName);
        }
        public void CreateProfileDir(string profileName)
        {
            string newProfilePath=Path.Combine(pathProfiles, profileName);
            Directory.CreateDirectory(newProfilePath);
        }
        public string GetSettingsFilePath(string profileName, string fileName)
        {
            return Path.Combine(GetProfileDir(profileName), fileName);
        }
        public string GetStandartProfileDir()
        {
            return Path.Combine(pathProfiles,"standart");
        }
        public List<string> GetProfiles()
        {
           return Directory.EnumerateDirectories(pathProfiles).Select(d => new DirectoryInfo(d).Name).ToList(); 
        }
    }

    public class MySettings : AppSettings<MySettings>
    {
        public string poloniexkey = "";
        public string poloniexsecret = "";
        public string bittrexkey = "";
        public string bittrexsecret = "";
        public bool defaultlimitorders = true;
    }
    public class SettingsInit : AppSettings<SettingsInit>
    {
        public string currentprofile = "Standart";
    }
}