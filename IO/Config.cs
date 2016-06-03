using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace MiniBillingServer.IO
{
    #region Business Logic
    public class Be_Config
    {
        #region binding
        public String Listen_Address { get; set; }
        public Int32 Listen_Port { get; set; }
        #endregion

        #region database
        public String HOST_DB { get; set; }
        public String ACC_DB { get; set; }
        public String USER_DB { get; set; }
        public String PW_DB { get; set; }
        #endregion

        #region security
        public List<string> Allowed_Hosts { get; set; }
        public List<string> Allowed_IPs { get; set; }
        #endregion
    }

    public class Dal_Config
    {
        #region Singleton
        private static readonly Dal_Config _instancia = new Dal_Config();
        public static Dal_Config Instancia
        {
            get { return Dal_Config._instancia; }
        }
        #endregion Singleton

        #region Methods

        #region Ini Methods
        public string PathToIni;

        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section,
            string key, string val, string filePath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section,
                 string key, string def, StringBuilder retVal,
            int size, string filePath);

        public void IniWriteValue(string Section, string Key, string Value)
        {
            WritePrivateProfileString(Section, Key, Value, PathToIni);
        }

        public string IniReadValue(string Section, string Key)
        {
            StringBuilder temp = new StringBuilder(255);
            int i = GetPrivateProfileString(Section, Key, "", temp, 255, PathToIni);
            return temp.ToString();
        }
        #endregion

        public Be_Config GetAllConfig(string file)
        {
            PathToIni = file;
            try
            {
                Be_Config Be_Config = new Be_Config();

                #region binding
                Be_Config.Listen_Address = IniReadValue("BINDING", "Listen_Address");
                Be_Config.Listen_Port = int.Parse(IniReadValue("BINDING", "Listen_Port"));
                #endregion

                #region database
                Be_Config.HOST_DB = IniReadValue("DATABASE", "HOST_DB");
                Be_Config.USER_DB = IniReadValue("DATABASE", "USER_DB");
                Be_Config.PW_DB = IniReadValue("DATABASE", "PASS_DB");
                Be_Config.ACC_DB = IniReadValue("DATABASE", "ACC_DB");

                #endregion

                #region security
                #region Allowed Hosts
                string tmp_AllowedHosts = IniReadValue("SECURITY", "Allowed_Hosts");
                string[] AllowedHosts = tmp_AllowedHosts.Split(',');
                List<string> tmp_AllowedHostList = new List<string>();
                foreach (string Host in AllowedHosts)
                {
                    if (Host != null && Host != string.Empty)
                    {
                        tmp_AllowedHostList.Add(Host);
                    }
                }
                Be_Config.Allowed_Hosts = tmp_AllowedHostList;
                #endregion

                #region Allowed IPs
                string tmp_AllowedIPs = IniReadValue("SECURITY", "Allowed_IPs");
                string[] AllowedIPs = tmp_AllowedIPs.Split(',');
                List<string> tmp_AllowedIPsList = new List<string>();
                foreach (string IP in AllowedIPs)
                {
                    if (IP != null && IP != string.Empty)
                    {
                        tmp_AllowedIPsList.Add(IP);
                        
                    }
                }
                Be_Config.Allowed_IPs = tmp_AllowedIPsList;
                #endregion
                #endregion

                return Be_Config;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }

    public class ConfigManager
    {
        #region Singleton
        private static readonly ConfigManager _instancia = new ConfigManager();
        public static ConfigManager Instancia
        {
            get { return ConfigManager._instancia; }
        }
        #endregion Singleton

        #region Methods
        public void IniWriteValue(string file, string Section, string Key, string Value)
        {
            Dal_Config.Instancia.IniWriteValue(Section, Key, Value);
        }

        public string IniReadValue(string file, string Section, string Key)
        {
            return Dal_Config.Instancia.IniReadValue(Section, Key);
        }

        public Be_Config GetAllConfig(string file)
        {
            try
            {
                return Dal_Config.Instancia.GetAllConfig(file);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
    #endregion

    public static class Config
    {
        public static Be_Config cfg = ConfigManager.Instancia.GetAllConfig("Settings/config.ini");
    }
}
