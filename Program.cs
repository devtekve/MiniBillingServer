using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

using System.Net;
using MiniBillingServer.IO;

namespace MiniBillingServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Be_Config _Config = Config.cfg;
            Console.Title = "MiniBillingServer - by florian0";
            Console.WriteLine("You should set billing server address to this one: ");
            Console.WriteLine("");
            Console.WriteLine("http://" + _Config.Listen_Address + ":" + _Config.Listen_Port + @"/");
            Console.WriteLine("");
            Console.WriteLine("-------------------------------------------------------------------");

            Http.HttpServer server = new Http.HttpServer();

            server.Prefixes.Add("http://" + _Config.Listen_Address + ":" + _Config.Listen_Port + "/");

            server.Handlers.Add(new Handlers.ServerStateHandler());
            server.Handlers.Add(new Handlers.SilkDataCallHandler());

            Model.SilkDB.Instance.Init();

            server.Start();
            
            Console.Read();
        }
    }
}
