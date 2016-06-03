using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

using System.Net;
using MiniBillingServer.IO;
using System.IO;

namespace MiniBillingServer
{
    class Program
    {
        static string err = null;
        static void Main(string[] args)
        {
            Console.Title = "MiniBillingServer";
            Console.WriteLine("Visit https://github.com/florian0/MiniBillingServer more information and updates");
            err = CheckConfig();
            if (err != null)
            {
                Console.WriteLine("##################  ERROR #####################");
                Console.WriteLine("");
                Console.WriteLine(err);
                Console.WriteLine("");
                Console.WriteLine("##################  ERROR #####################");
            }
            else
            {
                StartBillingServer();
            }             
            Console.Read();
        }

        static void StartBillingServer()
        {
            Be_Config _Config = Config.cfg;
            Http.HttpServer server = new Http.HttpServer();
            Console.WriteLine("");
            Console.WriteLine("You should set billing server address to this one: ");
            Console.WriteLine("");
            Console.WriteLine("http://" + _Config.Listen_Address + ":" + _Config.Listen_Port + @"/");
            Console.WriteLine("");
            Console.WriteLine("-------------------------------------------------------------------");
            server.Prefixes.Add("http://" + _Config.Listen_Address + ":" + _Config.Listen_Port + "/");
            server.Handlers.Add(new Handlers.ServerStateHandler());
            server.Handlers.Add(new Handlers.SilkDataCallHandler());
            Model.SilkDB.Instance.Init();
            server.Start();
        }
        
        static string CheckConfig()
        {
            if (!Directory.Exists("Settings"))
            {
                return "Settings folder doesn't exist, you should have it to make your config, please, download this tool again.";
            }
            else if (!File.Exists("Settings/config.ini"))
            {
                if (File.Exists("Settings/config.ini.dist"))
                {
                    return "You have to set your config in [Settings/config.ini.dist] and then rename it to [config.ini]";
                }
                else
                {
                    return "Couldn't find [Settings/config.ini.dist], you should have it to make your config, please, download this tool again.";
                }
            }
            else
            {
                return null;
            }
        }

    }
}
